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
}
