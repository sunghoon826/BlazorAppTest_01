using Microsoft.AspNetCore.Identity.UI.Services;

namespace BlazorAppTest_01.Areas.Identity.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}
