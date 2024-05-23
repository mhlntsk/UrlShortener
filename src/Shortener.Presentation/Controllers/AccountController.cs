using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.AccountViewModels;
using MVC.Services;
using MVC.Services.Validation;
using Shortener.Business.Services;
using Shortener.Data.Entities;
using Shortener.Presentation.Services;
using System.Security.Claims;

namespace Shortener.Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<User> signInManager;
        private readonly AccountService accountService;
        private readonly JwtTokenService jwtTokenService;

        public AccountController(SignInManager<User> signInManager, AccountService accountService, JwtTokenService jwtTokenService)
        {
            this.signInManager = signInManager;
            this.accountService = accountService;
            this.jwtTokenService = jwtTokenService;
        }

        [HttpGet("checkEmail")]
        public async Task<IActionResult> CheckEmail([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new { Detail = "Email is required." });
            }

            bool emailExists = await accountService.CheckIfEmailExists(email);

            return Ok(new { Exists = emailExists });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, [FromServices] IValidator<RegisterViewModel> validator)
        {
            try
            {
                var result = await validator.ValidateAsync(registerViewModel);

                if (!result.IsValid)
                {
                    result.AddToModelState(ModelState);

                    return BadRequest(ModelState);
                }
                
                var registrationResult = await accountService.RegisterUser(registerViewModel, this.ModelState);

                if (registrationResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, [FromServices] IValidator<LoginViewModel> validator)
        {
            try
            {
                var result = await validator.ValidateAsync(loginViewModel);

                if (!result.IsValid)
                {
                    result.AddToModelState(this.ModelState);
                    return BadRequest(ModelState);
                }

                var resultOfAuth = await accountService.LoginUser(loginViewModel, this.ModelState);

                if (resultOfAuth == Microsoft.AspNetCore.Identity.SignInResult.Success)
                {
                    var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
                    var userRole = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value ?? "user";

                    if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(userEmail) && !string.IsNullOrEmpty(userRole))
                    {
                        var token = jwtTokenService.GenerateToken(userId, userEmail, userRole);
                        return Ok(new { Token = token, UserId = userId, UserRole = userRole });

                    }
                    else
                    {
                        return BadRequest("Failed to retrieve user information");
                    }
                }

                return BadRequest("User is not authenticated");

            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("changePassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel, [FromServices] IValidator<ChangePasswordViewModel> validator)
        {
            try
            {
                var result = await validator.ValidateAsync(changePasswordViewModel);

                if (!result.IsValid)
                {
                    result.AddToModelState(this.ModelState);

                    return BadRequest(ModelState);
                }

                return await accountService.ChangePassword(changePasswordViewModel, this.ModelState);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
