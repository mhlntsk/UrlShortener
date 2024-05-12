using FluentValidation;
using MVC.Models.AccountViewModels;

namespace MVC.Tools.Validation.Account
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Username or Email is required.")
                .MinimumLength(2).WithMessage("Min length is 2")
                .MaximumLength(100).WithMessage("Max length is 100");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Min length is 8")
                .MaximumLength(100).WithMessage("Max length is 100")
                .Matches("[A-Z]").WithMessage("The password must contain at least one capital letter")
                .Matches("[a-z]").WithMessage("The password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("The password must contain at least one number")
                .Matches("[_\\-!@#$%^&*(),.?\":{}|<>]").WithMessage("The password must contain at least one special character");
        }
    }
}
