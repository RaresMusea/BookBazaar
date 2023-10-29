using Microsoft.AspNetCore.Identity.UI.Services;

namespace BookBazaar.Misc.Email;

public class EmailEmitter : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        return Task.CompletedTask;
    }
}