using Domain_Layer.Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{

    //[ApiController]
    //[Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<UsuarioRegistrado> _signInManager;
        private readonly UserManager<UsuarioRegistrado> _userManager;

        public AuthController(SignInManager<UsuarioRegistrado> singInManager, UserManager<UsuarioRegistrado> userManager)
        {
            _signInManager = singInManager;
            _userManager = userManager;
        }
    

        //[HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest) {

            var email = await _userManager.FindByNameAsync(loginRequest.Email);

            if (email == null) return Unauthorized("usuario no encontrado");

            var result = await _signInManager.PasswordSignInAsync(email, loginRequest.Password, false, false);

            if (result.Succeeded)
            {
                return Ok("Login successful");
            }

            return Unauthorized("Invalid credentials");
        }

        
        //[HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }


        //[HttpGet("me")]
        public IActionResult Me()
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();
            return Ok(new { username = User.Identity.Name });
        }

    }
}