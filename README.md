# Minimal API Form Validator & Mailer

This app is intended to run a simple backend API for validating and mailing form data from static sites.

## An open source, bare-bones alternative to paid services

I can't bring myself to pay for an API service that provides more than I need for simple spam filtering on form submissions.
Over time, I hope to add features and improve this and my C# skills.

### Details
  - **Language:** C#
  - **Validation Library:** FluentValidation
  - **Email Service:** System.Net.Mail
  - **Testing Library:** Xunit

#### Note!
**System.Net.Mail** has been rendered obsolete by Microsoft.
I will be refactoring the Email Service class to use **MailKit** in the future.
