using FluentValidation;
using Store.Contracts.V1.Requests;

namespace Store.Contracts.V1.Validators
{
    public class CategoryRequestValidator : AbstractValidator<CategoryRequest>
    {
        public CategoryRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Matches("^[A-Za-z]+$");
        }
    }
}
