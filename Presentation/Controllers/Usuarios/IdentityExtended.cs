using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers.Usuarios
{
    [ApiController]
    [Route("api/[controller]")] // O la ruta que prefieras, por ejemplo, "api/auth"
    public class IdentityExtended : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager; // Si necesitas acceder al usuario actual

        public IdentityExtended(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            // Identity se encargará de borrar la cookie de autenticación.
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Sesión cerrada correctamente." });
        }

        [HttpGet("me")]
        [Authorize] // Asegúrate de que solo usuarios autenticados puedan acceder
        public async Task<IActionResult> GetCurrentUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    return Ok(new { Email = user.Email, Roles = await _userManager.GetRolesAsync(user) });
                }
            }
            return Unauthorized();
        }
    }
}
