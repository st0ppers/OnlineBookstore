using BookStore.Models.Requests;
using FluentValidation;

namespace BookStore.Validators
{
    public class AddPersonRequestValidation : AbstractValidator<AddPersonRequest>
    {
        public AddPersonRequestValidation()
        {
            RuleFor(x => x.Age).GreaterThanOrEqualTo(0);
            RuleFor(x => x.BirthOfDate).GreaterThan(DateTime.MaxValue).LessThanOrEqualTo(DateTime.MinValue);
            RuleFor(x => x.Name).NotEmpty().Length(2, 30);
        }
    }
}
