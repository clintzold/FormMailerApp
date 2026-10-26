using FormMailer;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Creates an array from sites listed in config. Allows for multiple sites to use the FormMailer.
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();

// Add CORS policies
builder.Services.AddCors(options =>
{
  foreach (var origin in allowedOrigins)
  {
    options.AddPolicy(origin, policy =>
    {
      policy.WithOrigins(origin)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
  }
});

// Register my custom services
builder.Services.AddValidatorsFromAssemblyContaining<ContactFormValidator>();
builder.Services.AddTransient<EmailService>();

var app = builder.Build();

// Apply the policies based on the request header info
app.UseCors(policyBuilder =>
{
  // Match origin header against allowable array of sites
  policyBuilder.SetIsOriginAllowed(origin => allowedOrigins.Contains(origin))
                .AllowAnyHeader()
                .AllowAnyMethod();
});

app.MapPost("/submit-form", async (ContactForm model, IValidator<ContactForm> validator, EmailService emailService) =>
{
  // Validate the serialized form data
  var validationResult = await validator.ValidateAsync(model);

  if (!validationResult.IsValid)
  {
    return Results.ValidationProblem(validationResult.ToDictionary());
  }

  try
  {
    await emailService.SendEmailAsync(model);
    return Results.Ok(new { message = "Form submitted successfully!" });
  }
  catch (Exception e)
  {
    return Results.Problem("An error occurred while sending your message: " + e);
  }
});

app.Run();
