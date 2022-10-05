using System.Drawing;
using BookStore.Models.Requests;
using FluentValidation;

namespace BookStore.Validators
{
    public class AddAuthorRequestValidator : AbstractValidator<AddAuthorRequest>
    {
        public AddAuthorRequestValidator()
        {
            RuleFor(expression: x => x.Age)
                .GreaterThan(0).LessThanOrEqualTo(120)
                .WithMessage("My custom message for age");

            RuleFor(x => x.Name).NotEmpty().Length(2, 10);

            When(x => !string.IsNullOrEmpty(x.NickName), () =>
            {
                RuleFor(x => x.NickName)
                    .MinimumLength(2).MaximumLength(10);
            });

            RuleFor(x => x.DateOfBirth)
                .GreaterThan(DateTime.MinValue).LessThanOrEqualTo(DateTime.MaxValue);
        }
    }
}
