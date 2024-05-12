using FluentValidation;
using MVC.Models.AccountViewModels;
using Shortener.Business.Models;

namespace Shortener.Presentation.Tools.Validation.Url
{
    public class UrlShortenerModelValidator : AbstractValidator<UrlShortenerModel>
    {
        public UrlShortenerModelValidator()
        {
            RuleFor(x => x.FullUrl)
                .NotEmpty().WithMessage("FullUrl is required.")
                .MaximumLength(5000).WithMessage("Max length is 5000");

            RuleFor(x => x.CreatedDate)
                .GreaterThanOrEqualTo(new DateTime(year: 2024, month: 1, day: 1, hour: 0, minute: 0, second: 0, kind: DateTimeKind.Utc))
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("The date must be less than current");

            RuleFor(x => x.LastAppeal)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("The date must be less than current");

            RuleFor(x => x.NumberOfAppeals)
                .GreaterThan(-1)
                .WithMessage("Invalid NumberOfAppeals");

            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("Invalid userId");
        }
    }
}
