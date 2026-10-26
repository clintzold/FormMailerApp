using FluentValidation;

namespace FormMailer;

// Form Data Model
public class ContactForm
{
  public string Name { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string Message { get; set; } = string.Empty;
}

// Contact Form Validator
public class ContactFormValidator : AbstractValidator<ContactForm>
{
  public ContactFormValidator()
  {
    RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
    RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email address is required.");
    RuleFor(x => x.Message).NotEmpty().Length(10, 500).WithMessage("Message must be between 10 and 500 characters.");
  }
}
