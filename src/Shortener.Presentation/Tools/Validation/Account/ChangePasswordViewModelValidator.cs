﻿using FluentValidation;
using MVC.Models.AccountViewModels;

namespace MVC.Tools.Validation.Account
{
    public class ChangePasswordViewModelValidator : AbstractValidator<ChangePasswordViewModel>
    {
        public ChangePasswordViewModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Enter a valid email address.")
                .MinimumLength(8).WithMessage("Min length is 8")
                .MaximumLength(100).WithMessage("Max length is 100");

            RuleFor(x => x.OldPassword)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Min length is 8")
                .MaximumLength(100).WithMessage("Max length is 100")
                .Matches("[A-Z]").WithMessage("The password must contain at least one capital letter")
                .Matches("[a-z]").WithMessage("The password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("The password must contain at least one number")
                .Matches("[_\\-!@#$%^&*(),.?\":{}|<>]").WithMessage("The password must contain at least one special character");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Min length is 8")
                .MaximumLength(100).WithMessage("Max length is 100")
                .Matches("[A-Z]").WithMessage("The password must contain at least one capital letter")
                .Matches("[a-z]").WithMessage("The password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("The password must contain at least one number")
                .Matches("[_\\-!@#$%^&*(),.?\":{}|<>]").WithMessage("The password must contain at least one special character")
                .Must((model, NewPassword) => NewPassword != model.OldPassword);
        }
    }
}
