using BillSale.BLL.Services.Contracts.Models.Product;
using BillSale.BLL.Services.Validators.Constraints;
using FluentValidation;

namespace BillSale.BLL.Services.Validators.Product
{
    /// <summary>
    /// Валидатор для <see cref="ProductCreateModel"/>
    /// </summary>
    public class ProductUpdateModelValidator : AbstractValidator<ProductUpdateModel>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ProductUpdateModelValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Идентификатор компании обязателен");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Наименование продукта обязательно")
                .MaximumLength(ProductConstraints.NameMaxLength)
                .WithMessage($"Длина наименования не может быть больше {ProductConstraints.NameMaxLength}");

            RuleFor(x => x.MeasureUnit)
                .NotEmpty()
                .WithMessage("Единица измерения обязательна")
                .MaximumLength(ProductConstraints.MeasureUnitMaxLength)
                .WithMessage($"Длина единицы измерения не может быть больше {ProductConstraints.MeasureUnitMaxLength}");
        }
    }
}
