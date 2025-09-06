using ANDON_Application.Auth;
using ANDON_Application.Interface.Auth;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ANDON.Controllers.Auth
{
    [Route("/api/v1/auth")]
    public class AuthController:ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService userService)
        {
            _authService = userService ?? throw new ArgumentNullException(nameof(userService));

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await _authService.AuthenticationAsync(loginDTO.UserName, loginDTO.Password);
            return Ok(new
            {
                status = 200,
                message = "Đăng nhập thành công",
                user = user
            });
        }

        
    }
}
