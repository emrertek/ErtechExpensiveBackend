using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Interfaces;
using DataAccessLayer.DTOs;

namespace PresentationLayer.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("AuthenticateUser")]
        public IActionResult Login(LoginAuthDTO loginDTO)
        {
            var result = _loginService.AuthenticateUser(loginDTO);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}
