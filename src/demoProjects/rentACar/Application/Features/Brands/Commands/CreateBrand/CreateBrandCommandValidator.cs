using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.CreateBrand
{
    // AbstractValidator, FluentValidationun aracıdır.
    // hangi command için validation kullanılacağı belirtmek gerek
    public class CreateBrandCommandValidator:AbstractValidator<CreateBrandCommand>
    {
        // add işlemi için kullanılan validator.
        public CreateBrandCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Name).MinimumLength(2);
        }
    }
}
