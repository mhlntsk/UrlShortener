using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MVC.Models.AccountViewModels;
using Shortener.Data.Entities;

namespace MVC.Services
{
    public class AccountService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<bool> CheckIfEmailExists(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return false;
            }

            return true;
        }
        public async Task<IdentityResult> RegisterUser(RegisterViewModel model, ModelStateDictionary ModelState)
        {
            User user = new User { Email = model.Email, UserName = model.Email, FirstName = model.FirstName!, LastName = model.LastName };

            var resultOfCreate = await userManager.CreateAsync(user, model.Password!);

            if (resultOfCreate.Succeeded)
            {
                await signInManager.SignInAsync(user, false);

                return resultOfCreate;
            }
            else
            {
                foreach (var error in resultOfCreate.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return resultOfCreate;
            }
        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> LoginUser(LoginViewModel model, ModelStateDictionary ModelState)
        {
            var user = await userManager.FindByEmailAsync(model.Email!);

            if (user != null)
            {
                var resultOfSignIn = await signInManager.PasswordSignInAsync(user, model.Password!, model.RememberMe, lockoutOnFailure: false);

                if (resultOfSignIn.Succeeded)
                {
                    return Microsoft.AspNetCore.Identity.SignInResult.Success;
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect email and (or) password");
                }
            }

            ModelState.AddModelError("", "Incorrect email");
            return Microsoft.AspNetCore.Identity.SignInResult.Failed;
        }

        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model, ModelStateDictionary modelState)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                IdentityResult resultOfChangePass =
                    await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                if (resultOfChangePass.Succeeded)
                {
                    await userManager.UpdateSecurityStampAsync(user);
                    return new RedirectToActionResult("Login", "Account", null, false);
                }
                else
                {
                    foreach (var error in resultOfChangePass.Errors)
                    {
                        modelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            else
            {
                modelState.AddModelError(string.Empty, "No user found");
            }

            return new ViewResult();
        }
    }
}
