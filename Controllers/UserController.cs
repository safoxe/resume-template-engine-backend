using Microsoft.AspNetCore.Mvc;
using engine_plugin_backend.Services;
using engine_plugin_backend.Models;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace engine_plugin_backend.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signUpUser")]
        public ActionResult<string> SignUpUser([FromBody] UserModel userModel)
        {
            return _userService.SignUpUser(userModel);
        }

        [HttpPost("signInUser")]
        public IActionResult SignInUser([FromBody] UserModel userModel)
        {
            var userIdentity = _userService.GetIdentity(userModel.Email, userModel.Password);
            if (userIdentity == null)
            {
                return BadRequest(new { errorText = "Invalid email or password" });
            }
            var timeNow = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthServerOptions.ISSUER,
                audience: AuthServerOptions.AUDIENCE,
                claims: userIdentity.Claims, notBefore: timeNow,
                expires: timeNow.Add(TimeSpan.FromMinutes(AuthServerOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthServerOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new  {
                access_token = encodedJwt,
                username = userIdentity.Name,
            };

            return Json(response);
        }
    }
}