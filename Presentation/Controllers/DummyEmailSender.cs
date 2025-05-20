
using Domain_Layer.Entidades;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

public class DummyEmailSender : IEmailSender<UsuarioRegistrado>

{
    public Task SendConfirmationLinkAsync(IdentityUser user, string email, string confirmationLink) => Task.CompletedTask;
    public Task SendPasswordResetLinkAsync(IdentityUser user, string email, string resetLink) => Task.CompletedTask;
    public Task SendPasswordResetCodeAsync(IdentityUser user, string email, string resetCode) => Task.CompletedTask;

    public Task SendConfirmationLinkAsync(UsuarioRegistrado user, string email, string confirmationLink)
    {
        // Simulo mandar un mail    
        
        Console.WriteLine("conf. link: " + confirmationLink);
        return Task.CompletedTask;
    }

    public Task SendPasswordResetLinkAsync(UsuarioRegistrado user, string email, string resetLink)
    {
        Console.WriteLine("reset link: " + resetLink);
        return Task.CompletedTask;
    }

    public Task SendPasswordResetCodeAsync(UsuarioRegistrado user, string email, string resetCode)
    {
        Console.WriteLine("reset code: " + resetCode);
        return Task.CompletedTask;
    }
}
