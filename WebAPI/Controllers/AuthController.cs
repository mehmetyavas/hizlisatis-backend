using Business.Abstract;
using Business.Services.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using Entities.DTOs.UserDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService,IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(new ErrorResult(userToLogin.Message));
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                var loginResult = new loginResDTO
                {
                    AccessToken = result.Data,
                    Name = _userService.GetByUsername(userForLoginDto.Username).FirstName!,
                    Surname = _userService.GetByUsername(userForLoginDto.Username).LastName!,
                    UserId = _userService.GetByUsername(userForLoginDto.Username).Id
                };
                return Ok(new SuccessDataResult<loginResDTO>(loginResult));
            }

            return BadRequest(new ErrorResult(result.Message));
        }

        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Username);
            if (!userExists.Success)
            {
                return BadRequest(new ErrorResult(userExists.Message));
            }

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(new SuccessDataResult<AccessToken>(result.Data));
            }

            return BadRequest(new ErrorResult(result.Message));
        }
    }
}
