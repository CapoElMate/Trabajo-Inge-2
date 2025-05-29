using Bussines_Logic_Layer.DTOs.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers.Usuarios
{
    [ApiController]
    [Route("api/[controller]")] // O la ruta que prefieras, por ejemplo, "api/auth"
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager; // Usa IdentityUser o tu ApplicationUser
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ILogger<AuthController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost("login")] // Endpoint será /Auth/login
        public async Task<IActionResult> Login([FromBody] LoginDto model)
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
                _logger.LogInformation("Usuario {Email} inició sesión exitosamente.", model.Email);
                return Ok(new { message = "Inicio de sesión exitoso." });
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("Cuenta de usuario {Email} bloqueada.", model.Email);
                return Forbid("Tu cuenta ha sido bloqueada. Por favor, inténtalo de nuevo más tarde.");
            }
            else if (result.IsNotAllowed)
            {
                _logger.LogWarning("Inicio de sesión para {Email} no permitido (no confirmado o no habilitado).", model.Email);
                return Unauthorized(new { message = "Tu cuenta no está permitida para iniciar sesión." });
            }
            else
            {
                _logger.LogWarning("Intento de login fallido para el email {Email} (contraseña incorrecta).", model.Email);
                return Unauthorized(new { message = "Credenciales inválidas." });
            }
        }

        [HttpPost("logout")]
        // [Authorize] // Opcional, si quieres que solo los autenticados puedan cerrar sesión.
        // Si el front-end solo llama a logout cuando el usuario ya cree estar autenticado,
        // no es estrictamente necesario, pero es buena práctica.
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Usuario cerró sesión.");
            return Ok(new { message = "Cierre de sesión exitoso." });
        }

        [HttpGet("me")]
        [Authorize] // Este endpoint requiere que el usuario esté autenticado.
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return Unauthorized("No autenticado.");
            }

            var userName = user.UserName;
            var userId = await _userManager.GetUserIdAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            return Ok(new { UserName = userName, UserId = userId, Roles = userRoles, Message = "Estás autenticado." });
        }
    }
}
