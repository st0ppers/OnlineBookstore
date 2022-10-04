using BookStore.Models.Requests;
using FluentValidation;

namespace BookStore.Validators
{
    public class AddBookRequestValidations : AbstractValidator<AddBookRequest>
    {
        public AddBookRequestValidations()
        {
            RuleFor(x => x.Title).Length(1, 50).NotEmpty().NotNull();
            RuleFor(x => x.AuthorId).NotEmpty();
        }
    }
}
