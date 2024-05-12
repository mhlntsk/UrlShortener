using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.AccountViewModels;
using MVC.Services;
using MVC.Services.Validation;
using Shortener.Data.Entities;

namespace Shortener.Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AccountApiController : ControllerBase
    {
        private readonly SignInManager<User> signInManager;
        private readonly AccountService accountService;
        public AccountApiController(SignInManager<User> signInManager, AccountService accountService)
        {
            this.signInManager = signInManager;
            this.accountService = accountService;
        }

        [HttpPost]
        [Route("register")]
        [ValidateAntiForgeryToken]
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
                    return Redirect("http://localhost:4200/");
                }
                else
                {
                    return BadRequest(ModelState);
                }


            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("login")]
        [ValidateAntiForgeryToken]
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

                return await accountService.LoginUser(loginViewModel, this.ModelState);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("changePassword")]
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
                return BadRequest();
            }
        }
    }
}
