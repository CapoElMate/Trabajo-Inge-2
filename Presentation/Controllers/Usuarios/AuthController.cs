using System.ComponentModel.DataAnnotations;
using System.Text;
using Bussines_Logic_Layer.DTOs.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace API_Layer.Controllers.Usuarios
{
    [ApiController]
    [Route("[controller]")] // O "api/[controller]" si lo prefieres
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AuthController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender<IdentityUser> _emailSender; // Necesario para enviar emails

        public AuthController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ILogger<AuthController> logger,
            RoleManager<IdentityRole> roleManager,
            IEmailSender<IdentityUser> emailSender) // Inyecta IEmailSender
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }

        // --- MODELOS DE DATOS (Puedes ponerlos en un archivo Models/AuthModels.cs) ---

        // Modelo para el registro de usuario
        public class RegisterModel
        {
            [Required(ErrorMessage = "El email es obligatorio.")]
            [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "La contraseña es obligatoria.")]
            [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} y un máximo de {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "La contraseña y la confirmación no coinciden.")]
            public string ConfirmPassword { get; set; }

            // Campo para el rol, con valor por defecto
            public string Role { get; set; } = "Cliente";
        }

        // Modelo para el login
        public class LoginModel
        {
            [Required(ErrorMessage = "El email es obligatorio.")]
            [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "La contraseña es obligatoria.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            public bool RememberMe { get; set; } = false;
        }

        // Modelo para solicitar restablecimiento de contraseña
        public class ForgotPasswordModel
        {
            [Required(ErrorMessage = "El email es obligatorio.")]
            [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
            public string Email { get; set; }
        }

        // Modelo para restablecer contraseña
        public class ResetPasswordModel
        {
            [Required(ErrorMessage = "El email es obligatorio.")]
            [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "El token de restablecimiento es obligatorio.")]
            public string Token { get; set; } // Token enviado por email

            [Required(ErrorMessage = "La nueva contraseña es obligatoria.")]
            [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} y un máximo de {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Compare("NewPassword", ErrorMessage = "La nueva contraseña y la confirmación no coinciden.")]
            public string ConfirmNewPassword { get; set; }
        }

        // --- ENDPOINTS ---

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                _logger.LogWarning("Intento de login fallido para el email {Email} (usuario no encontrado).", model.Email);
                return Unauthorized(new { message = "Credenciales inválidas." });
            }

            var result = await _signInManager.PasswordSignInAsync(
                user,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                _logger.LogInformation("Usuario '{Email}' inició sesión exitosamente.", model.Email);
                return Ok(new { message = "Inicio de sesión exitoso." });
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("Cuenta de usuario '{Email}' bloqueada.", model.Email);
                return Forbid(); // Removed the anonymous type and used Forbid() without arguments.  
            }
            else if (result.IsNotAllowed)
            {
                _logger.LogWarning("Inicio de sesión para '{Email}' no permitido (no confirmado o no habilitado).", model.Email);
                return Unauthorized(new { message = "Tu cuenta no está permitida para iniciar sesión." });
            }
            else
            {
                _logger.LogWarning("Intento de login fallido para el email '{Email}' (contraseña incorrecta).", model.Email);
                return Unauthorized(new { message = "Credenciales inválidas." });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar que el rol sea uno de los permitidos
            string[] allowedRoles = { "Dueño", "Cliente", "Empleado" };
            if (!allowedRoles.Contains(model.Role))
            {
                ModelState.AddModelError("Role", "El rol especificado no es válido.");
                return BadRequest(ModelState);
            }

            // Asegurarse de que el rol exista en el sistema
            if (!await _roleManager.RoleExistsAsync(model.Role))
            {
                _logger.LogWarning("Intento de registro con rol '{Role}' que no existe en el sistema.", model.Role);
                ModelState.AddModelError("Role", $"El rol '{model.Role}' no existe.");
                return BadRequest(ModelState);
            }
            // IMPORTANTE: Si solo quieres que el registro sea para un rol específico (ej. Cliente),
            // elimina la validación anterior y fuerza el rol aquí:
            // model.Role = "Cliente";


            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("Usuario '{Email}' creado exitosamente.", model.Email);

                // Asignar el rol al nuevo usuario Identity
                var addToRoleResult = await _userManager.AddToRoleAsync(user, model.Role);
                if (!addToRoleResult.Succeeded)
                {
                    _logger.LogError("Error al asignar el rol '{Role}' al usuario '{Email}': {Errors}",
                                     model.Role, model.Email, string.Join(", ", addToRoleResult.Errors.Select(e => e.Description)));
                    // Opcional: Eliminar usuario si falla la asignación de rol
                    await _userManager.DeleteAsync(user);
                    return StatusCode(500, new { message = "Error al asignar rol al usuario." });
                }
                _logger.LogInformation("Usuario '{Email}' asignado al rol '{Role}' exitosamente.", model.Email, model.Role);
                return Ok(new { message = "Registro exitoso. Se ha enviado un email de confirmación." });
            }

            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Identity Error - Código: {error.Code}, Mensaje: {error.Description}");
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        

        [HttpGet("SendEmailConfirmationRequest")] 
        [AllowAnonymous]
        public async Task<IActionResult> EmailConfirmationRequest(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            // URL de tu frontend a la que el usuario será redirigido para confirmar
            var callbackUrl = Url.Action(
                "ConfirmEmail", // Nombre de la acción en tu AuthController
                "Auth",         // Nombre del controlador
                new { userId = user.Id, code = code },
                protocol: HttpContext.Request.Scheme); // O usa "https" en producción

            await _emailSender.SendConfirmationLinkAsync(user, user.Email, callbackUrl);
            _logger.LogInformation("Enlace de confirmación de email enviado a '{Email}'.", user.Email);
            return Ok(new { message = "Email de solicitud de confirmación enviado." });
        }


        [HttpGet("ConfirmEmail")] // Endpoint para la confirmación de email
        [AllowAnonymous] // No requiere autenticación previa
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return BadRequest(new { message = "ID de usuario o código de confirmación inválido." });
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("Intento de confirmación de email para ID de usuario no encontrado: {UserId}", userId);
                return NotFound(new { message = $"No se encontró un usuario con ID '{userId}'." });
            }

            // El código recibido de la URL debe ser decodificado
            var decodedCode = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, decodedCode);

            if (result.Succeeded)
            {
                _logger.LogInformation("Email para el usuario '{Email}' confirmado exitosamente.", user.Email);
                return Ok(new { message = "Email confirmado exitosamente. Ya puedes iniciar sesión." });
            }
            else
            {
                _logger.LogError("Error al confirmar email para el usuario '{Email}': {Errors}",
                                 user.Email, string.Join(", ", result.Errors.Select(e => e.Description)));
                return BadRequest(new { message = "Error al confirmar el email.", errors = result.Errors.Select(e => e.Description) });
            }
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            // No reveles si el usuario existe o no por razones de seguridad
            if (user == null || !await _userManager.IsEmailConfirmedAsync(user)) // Descomenta si necesitas email confirmado
            {
                _logger.LogInformation("Solicitud de restablecimiento de contraseña para email '{Email}' (usuario no encontrado o no confirmado, pero se envía OK para evitar enumeración).", model.Email);
                // Siempre devuelve OK para evitar que un atacante sepa si un email está registrado o no.
                return Ok(new { message = "Si tu email está registrado, recibirás un enlace para restablecer tu contraseña." });
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var callbackUrl = Url.Action(
                "ResetPassword", // Nombre de la acción en tu AuthController
                "Auth",          // Nombre del controlador
                new { userId = user.Id, code = code },
                protocol: HttpContext.Request.Scheme); // O "https" en producción

            await _emailSender.SendPasswordResetLinkAsync(user, user.Email, callbackUrl);
            _logger.LogInformation("Enlace de restablecimiento de contraseña enviado a '{Email}'.", user.Email);

            return Ok(new { message = "Si tu email está registrado, recibirás un enlace para restablecer tu contraseña." });
        }

        [HttpPost("reset-password")] // Endpoint para cambiar la contraseña
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // De nuevo, no reveles si el usuario existe para evitar enumeración.
                _logger.LogWarning("Intento de restablecimiento de contraseña fallido para email '{Email}' (usuario no encontrado).", model.Email);
                return BadRequest(new { message = "El restablecimiento de contraseña falló." });
            }

            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Token));
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, model.NewPassword);

            if (result.Succeeded)
            {
                _logger.LogInformation("Contraseña para el usuario '{Email}' restablecida exitosamente.", user.Email);
                return Ok(new { message = "Tu contraseña ha sido restablecida exitosamente. Ya puedes iniciar sesión." });
            }
            else
            {
                _logger.LogError("Error al restablecer contraseña para el usuario '{Email}': {Errors}",
                                 user.Email, string.Join(", ", result.Errors.Select(e => e.Description)));
                return BadRequest(new { message = "Error al restablecer la contraseña.", errors = result.Errors.Select(e => e.Description) });
            }
        }

        [HttpPost("logout")]
         [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Usuario cerró sesión.");
            return Ok(new { message = "Cierre de sesión exitoso." });
        }

        [HttpGet("me")]
        [Authorize] // Este endpoint requiere que el usuario esté autenticado.
        public IActionResult GetCurrentUser()
        {
            var userName = HttpContext.User.Identity?.Name;
            var userId = _userManager.GetUserId(HttpContext.User);

            if (userName == null)
            {
                return Unauthorized("No autenticado.");
            }

            // Obtener roles del usuario
            var userRoles = _userManager.GetRolesAsync(_userManager.FindByIdAsync(userId).Result).Result;

            return Ok(new { UserName = userName, UserId = userId, Roles = userRoles, Message = "Estás autenticado." });
        }
    }
}
