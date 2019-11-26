using FluentValidation;
using Store.Contracts.V1.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Contracts.V1.Validators
{
    public class ProductRequestValidator : AbstractValidator<ProductRequest>
    {
        public ProductRequestValidator()
        {
            RuleFor(x => x.Price)
                .InclusiveBetween(1, 1000);

            RuleFor(x => x.Name)
                .Matches("^[A-Za-z0-9]+$");
        }
    }
}
