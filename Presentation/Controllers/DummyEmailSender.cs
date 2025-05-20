
using Domain_Layer.Entidades;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

public class DummyEmailSender : IEmailSender<UsuarioRegistrado>

{

    public Task SendConfirmationLinkAsync(UsuarioRegistrado user, string email, string confirmationLink)
    {
        // Simulo mandar un mail    
        
        Debug.WriteLine("conf. link: " + confirmationLink);
        return Task.CompletedTask;
    }

    public Task SendPasswordResetLinkAsync(UsuarioRegistrado user, string email, string resetLink)
    {
        Debug.WriteLine("reset link: " + resetLink);
        return Task.CompletedTask;
    }

    public Task SendPasswordResetCodeAsync(UsuarioRegistrado user, string email, string resetCode)
    {
        Debug.WriteLine("reset code: " + resetCode);
        return Task.CompletedTask;
    }
}
