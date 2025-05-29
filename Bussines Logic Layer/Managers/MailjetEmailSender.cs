using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Mailjet.Client.TransactionalEmails;
using Mailjet.Client;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;

namespace Bussines_Logic_Layer.Managers
{
    // Clase para almacenar las claves de Mailjet
    public class MailjetOptions
    {
        public string? ApiKey { get; set; }
        public string? SecretKey { get; set; }
        public string? SenderEmail { get; set; } // El correo electrónico verificado en Mailjet
        public string? SenderName { get; set; } // El nombre del remitente
    }

    public class MailjetEmailSender : IEmailSender<IdentityUser>
    {
        private readonly MailjetOptions _mailjetOptions;
        private readonly IMailjetClient _mailjetClient;

        public MailjetEmailSender(IOptions<MailjetOptions> mailjetOptions)
        {
            _mailjetOptions = mailjetOptions.Value;
            // Configura el cliente de Mailjet con tus claves API
            _mailjetClient = new MailjetClient(_mailjetOptions.ApiKey, _mailjetOptions.SecretKey);
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Valida que las opciones necesarias estén configuradas
            if (string.IsNullOrEmpty(_mailjetOptions.SenderEmail))
            {
                throw new ArgumentNullException(nameof(MailjetOptions.SenderEmail), "Mailjet sender email is not configured.");
            }
            if (string.IsNullOrEmpty(_mailjetOptions.ApiKey) || string.IsNullOrEmpty(_mailjetOptions.SecretKey))
            {
                throw new ArgumentNullException("Mailjet API keys are not configured.");
            }

            var senderEmail = _mailjetOptions.SenderEmail;
            var senderName = _mailjetOptions.SenderName ?? "BobElAlquilador"; // Nombre por defecto si no se configura

            var emailMessage = new TransactionalEmailBuilder()
                .WithFrom(new SendContact(senderEmail, senderName))
                .WithTo(new SendContact(email))
                .WithSubject(subject)
                .WithHtmlPart(htmlMessage)
                .WithTextPart(htmlMessage) // Es buena práctica incluir una versión de texto plano
                .Build();

            try
            {
                var response = await _mailjetClient.SendTransactionalEmailAsync(emailMessage);

                if (response.Messages != null && response.Messages.Count() > 0 && response.Messages[0].Status == "success")
                {
                    Console.WriteLine($"Correo enviado a {email} con éxito a través de Mailjet.");
                }
                else
                {
                    // Manejo de errores detallado
                    Console.Error.WriteLine($"Error al enviar correo a {email} con Mailjet.");
                    if (response.Messages != null)
                    {
                        foreach (var message in response.Messages)
                        {
                            Console.Error.WriteLine($"Status: {message.Status}, Errors: {string.Join(", ", message.Errors?.Select(e => e.ErrorMessage) ?? new string[0])}");
                        }
                    }
                    else
                    {
                        Console.Error.WriteLine("No messages in Mailjet response.");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                // Captura errores de red o HTTP
                Console.Error.WriteLine($"HTTP Request Exception: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción
                Console.Error.WriteLine($"Error general al enviar correo: {ex.Message}");
            }
        }

        public Task SendPasswordResetCodeAsync(IdentityUser user, string email, string resetCode)
        {
            string appName = "Bob El Alquilador";
            string primaryRed = "#DC3545"; // Rojo para acentos
            string textColor = "#333333"; // Gris oscuro para el texto
            string bgColor = "#f4f4f4"; // Fondo general del email
            string containerBg = "#ffffff"; // Fondo del contenedor de contenido
            string codeBoxBg = "#f8d7da"; // Un rojo muy claro para el código (fondo de Bootstrap danger light)
            string codeBoxBorder = "#f5c6cb"; // Borde para el código (Bootstrap danger light border)
            string codeBoxText = "#721c24"; // Texto oscuro para el código (Bootstrap danger dark text)


            string htmlMessage = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset=""utf-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
                    <title>Código de Restablecimiento de Contraseña - {appName}</title>
                    <style type=""text/css"">
                        body {{ margin: 0; padding: 0; background-color: {bgColor}; font-family: Arial, sans-serif; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; }}
                        table {{ border-spacing: 0; }}
                        img {{ border: 0; line-height: 100%; outline: none; text-decoration: none; }}
                        a {{ text-decoration: none; color: {primaryRed}; }}
                        .container {{ width: 100%; max-width: 600px; background-color: {containerBg}; border-radius: 8px; box-shadow: 0 4px 8px rgba(0,0,0,0.05); margin: 20px auto; padding: 20px; }}
                        .code-box {{
                            background-color: {codeBoxBg};
                            border: 1px dashed {codeBoxBorder};
                            padding: 15px;
                            margin: 20px 0;
                            font-size: 20px;
                            font-weight: bold;
                            color: {codeBoxText};
                            text-align: center;
                            border-radius: 5px;
                            letter-spacing: 2px;
                        }}
                        @media screen and (max-width: 600px) {{ .container {{ width: 95% !important; padding: 15px !important; }} }}
                    </style>
                </head>
                <body style=""margin: 0; padding: 0; background-color: {bgColor};"">
                    <center style=""width: 100%; background-color: {bgColor};"">
                        <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" class=""container"" style=""max-width: 600px; background-color: {containerBg}; border-radius: 8px; box-shadow: 0 4px 8px rgba(0,0,0,0.05); margin: 20px auto;"">
                            <tr>
                                <td style=""padding: 20px; text-align: center;"">
                                    <h1 style=""color: {primaryRed}; margin-bottom: 20px; font-size: 24px;"">Tu código para restablecer la contraseña</h1>
                                    <p style=""color: {textColor}; line-height: 1.6; font-size: 16px; margin-bottom: 20px;"">
                                        Hola **{user.UserName}**,
                                        <br><br>Hemos recibido una solicitud para restablecer la contraseña de tu cuenta en **{appName}**.
                                        Usa el siguiente código para completar el proceso:
                                    </p>
                                    <div class=""code-box"" style=""background-color: {codeBoxBg}; border: 1px dashed {codeBoxBorder}; padding: 15px; margin: 20px 0; font-size: 20px; font-weight: bold; color: {codeBoxText}; text-align: center; border-radius: 5px; letter-spacing: 2px;"">
                                        {resetCode}
                                    </div>
                                    <p style=""color: {textColor}; line-height: 1.6; font-size: 14px; margin-top: 20px;"">
                                        Este código es válido por un tiempo limitado. Si no solicitaste un restablecimiento de contraseña, puedes ignorar este correo.
                                    </p>
                                    <p style=""color: #777777; font-size: 12px; margin-top: 30px;"">
                                        Saludos,<br>El equipo de {appName}
                                    </p>
                                </td>
                            </tr>
                        </table>
                    </center>
                </body>
                </html>";

            return SendEmailAsync(email, "Código para restablecer contraseña de " + appName, htmlMessage);
        }
        public Task SendPasswordResetLinkAsync(IdentityUser user, string email, string resetLink)
        {
            string appName = "Bob El Alquilador";
            string primaryRed = "#DC3545"; // Rojo para los botones y acentos
            string textColor = "#333333"; // Gris oscuro para el texto
            string bgColor = "#f4f4f4"; // Fondo general del email
            string containerBg = "#ffffff"; // Fondo del contenedor de contenido

            string htmlMessage = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset=""utf-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
                    <title>Restablecer Contraseña - {appName}</title>
                    <style type=""text/css"">
                        body {{ margin: 0; padding: 0; background-color: {bgColor}; font-family: Arial, sans-serif; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; }}
                        table {{ border-spacing: 0; }}
                        img {{ border: 0; line-height: 100%; outline: none; text-decoration: none; }}
                        a {{ text-decoration: none; color: {primaryRed}; }}
                        .container {{ width: 100%; max-width: 600px; background-color: {containerBg}; border-radius: 8px; box-shadow: 0 4px 8px rgba(0,0,0,0.05); margin: 20px auto; padding: 20px; }}
                        .button {{ display: inline-block; padding: 12px 25px; margin: 20px 0; background-color: {primaryRed}; color: #ffffff; text-decoration: none; border-radius: 5px; font-weight: bold; }}
                        @media screen and (max-width: 600px) {{ .container {{ width: 95% !important; padding: 15px !important; }} .button {{ display: block !important; width: 100% !important; text-align: center !important; padding: 10px 0 !important; }} }}
                    </style>
                </head>
                <body style=""margin: 0; padding: 0; background-color: {bgColor};"">
                    <center style=""width: 100%; background-color: {bgColor};"">
                        <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" class=""container"" style=""max-width: 600px; background-color: {containerBg}; border-radius: 8px; box-shadow: 0 4px 8px rgba(0,0,0,0.05); margin: 20px auto;"">
                            <tr>
                                <td style=""padding: 20px; text-align: center;"">
                                    <h1 style=""color: {primaryRed}; margin-bottom: 20px; font-size: 24px;"">Restablece tu contraseña de {appName}</h1>
                                    <p style=""color: {textColor}; line-height: 1.6; font-size: 16px; margin-bottom: 20px;"">
                                        Hola **{user.UserName}**,
                                        <br><br>Hemos recibido una solicitud para restablecer la contraseña de tu cuenta en **{appName}**.
                                        Para continuar con el restablecimiento, por favor haz clic en el siguiente botón:
                                    </p>
                                    <a href=""{resetLink}"" class=""button"" style=""display: inline-block; padding: 12px 25px; margin: 20px 0; background-color: {primaryRed}; color: #ffffff; text-decoration: none; border-radius: 5px; font-weight: bold;"">Restablecer Contraseña</a>
                                    <p style=""color: {textColor}; line-height: 1.6; font-size: 14px; margin-top: 20px;"">
                                        Si tienes problemas para hacer clic en el botón, copia y pega la siguiente URL en tu navegador:
                                        <br>
                                        <a href=""{resetLink}"" style=""color: {primaryRed}; word-break: break-all;"">{resetLink}</a>
                                    </p>
                                    <p style=""color: #777777; font-size: 12px; margin-top: 30px;"">
                                        Si no solicitaste un restablecimiento de contraseña, puedes ignorar este correo.
                                        <br>Saludos,<br>El equipo de {appName}
                                    </p>
                                </td>
                            </tr>
                        </table>
                    </center>
                </body>
                </html>";

            return SendEmailAsync(email, "Restablecer contraseña de " + appName, htmlMessage);
        }
        public Task SendConfirmationLinkAsync(IdentityUser user, string email, string confirmationLink)
        {
            string appName = "Bob El Alquilador";
            string primaryRed = "#DC3545"; // Rojo para los botones y acentos
            string textColor = "#333333"; // Gris oscuro para el texto
            string bgColor = "#f4f4f4"; // Fondo general del email
            string containerBg = "#ffffff"; // Fondo del contenedor de contenido

            string htmlMessage = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset=""utf-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
                    <title>Confirma tu cuenta en {appName}</title>
                    <style type=""text/css"">
                        /* Estilos generales */
                        body {{ margin: 0; padding: 0; background-color: {bgColor}; font-family: Arial, sans-serif; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; }}
                        table {{ border-spacing: 0; }}
                        img {{ border: 0; line-height: 100%; outline: none; text-decoration: none; }}
                        a {{ text-decoration: none; color: {primaryRed}; }}

                        /* Estilos para el contenedor principal */
                        .container {{
                            width: 100%;
                            max-width: 600px;
                            background-color: {containerBg};
                            border-radius: 8px;
                            box-shadow: 0 4px 8px rgba(0,0,0,0.05);
                            margin: 20px auto;
                            padding: 20px;
                        }}

                        /* Estilos para el botón */
                        .button {{
                            display: inline-block;
                            padding: 12px 25px;
                            margin: 20px 0;
                            background-color: {primaryRed};
                            color: #ffffff;
                            text-decoration: none;
                            border-radius: 5px;
                            font-weight: bold;
                        }}

                        /* Estilos responsivos */
                        @media screen and (max-width: 600px) {{
                            .container {{
                                width: 95% !important;
                                padding: 15px !important;
                            }}
                            .button {{
                                display: block !important;
                                width: 100% !important;
                                text-align: center !important;
                                padding: 10px 0 !important;
                            }}
                        }}
                    </style>
                </head>
                <body style=""margin: 0; padding: 0; background-color: {bgColor};"">
                    <center style=""width: 100%; background-color: {bgColor};"">
                        <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" class=""container"" style=""max-width: 600px; background-color: {containerBg}; border-radius: 8px; box-shadow: 0 4px 8px rgba(0,0,0,0.05); margin: 20px auto;"">
                            <tr>
                                <td style=""padding: 20px; text-align: center;"">
                                    <h1 style=""color: {primaryRed}; margin-bottom: 20px; font-size: 24px;"">¡Confirma tu cuenta en {appName}!</h1>
                                    <p style=""color: {textColor}; line-height: 1.6; font-size: 16px; margin-bottom: 20px;"">
                                        Hola **{user.UserName}**,
                                        <br><br>Gracias por registrarte en **{appName}**. Para activar tu cuenta y comenzar a usar nuestros servicios, por favor haz clic en el botón de abajo para confirmar tu dirección de correo electrónico:
                                    </p>
                                    <a href=""{confirmationLink}"" class=""button"" style=""display: inline-block; padding: 12px 25px; margin: 20px 0; background-color: {primaryRed}; color: #ffffff; text-decoration: none; border-radius: 5px; font-weight: bold;"">Confirmar Email</a>
                                    <p style=""color: {textColor}; line-height: 1.6; font-size: 14px; margin-top: 20px;"">
                                        Si tienes problemas para hacer clic en el botón, copia y pega la siguiente URL en tu navegador:
                                        <br>
                                        <a href=""{confirmationLink}"" style=""color: {primaryRed}; word-break: break-all;"">{confirmationLink}</a>
                                    </p>
                                    <p style=""color: #777777; font-size: 12px; margin-top: 30px;"">
                                        Si no te registraste en {appName}, puedes ignorar este correo.
                                        <br>Saludos,<br>El equipo de {appName}
                                    </p>
                                </td>
                            </tr>
                        </table>
                    </center>
                </body>
                </html>";

            return SendEmailAsync(email, "Confirma tu cuenta en " + appName, htmlMessage);
        }
    }
}
