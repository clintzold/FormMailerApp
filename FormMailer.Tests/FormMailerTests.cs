using Xunit;
using FormMailer;

namespace FormMailer.Tests;

public class FormMailerTests
{
  [Fact]
  public void ContactForm_HasDefault_EmptyStringPropertiesFor_Email_Name_Message()
  {
    var form = new ContactForm();

    Assert.Equal("", form.Email);
    Assert.Equal("", form.Name);
    Assert.Equal("", form.Message);
  }

  [Fact]
  public void PropertySetters_ShouldUpdateValues()
  {
    var form = new ContactForm();

    form.Name = "Test User";
    form.Email = "test@unit-test.com";
    form.Message = "This is a unit test.";

    Assert.Equal("Test User", form.Name);
    Assert.Equal("test@unit-test.com", form.Email);
    Assert.Equal("This is a unit test.", form.Message);
  }

  [Fact]
  public async Task Validator_Rejects_ContactFormWith_EmptyStringProps()
  {
    // Initialize form with default empty string fields
    var emptyForm = new ContactForm();

    // Initialize validator
    var validator = new ContactFormValidator();

    // Inject form into validator
    var result = await validator.ValidateAsync(emptyForm);

    // Form should be invalid
    Assert.False(result.IsValid);

    // Verify each empty field generated an error
    Assert.Contains(result.Errors, e => e.PropertyName == nameof(ContactForm.Name));
    Assert.Contains(result.Errors, e => e.PropertyName == nameof(ContactForm.Email));
    Assert.Contains(result.Errors, e => e.PropertyName == nameof(ContactForm.Message));
  }

  [Fact]
  public async Task Validator_Accepts_ContactForm_WithCorrectInput()
  {
    // Initalize a complete form
    var completeForm = new ContactForm
    {
      Name = "Test User",
      Email = "test@unit-test.com",
      Message = "This is a unit test."
    };

    // Initialize Validator
    var validator = new ContactFormValidator();

    // Inject form into validator
    var result = await validator.ValidateAsync(completeForm);

    // Form should be valid
    Assert.True(result.IsValid);
  }
}
