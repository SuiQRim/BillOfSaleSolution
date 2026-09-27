using BillSale.BLL.Services.Contracts.Models.Certificate;
using BillSale.BLL.Services.Validators.Constraints;
using FluentValidation;

namespace BillSale.BLL.Services.Validators.Certificate
{
    /// <summary>
    /// Валидатор для <see cref="CertificateUpdateModel"/>
    /// </summary>
    public class CertificateUpdateModelValidator : AbstractValidator<CertificateUpdateModel>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public CertificateUpdateModelValidator()
        {
            RuleFor(x => x.SellerId)
                .NotEmpty()
                .WithMessage("Продавец обязателен");

            RuleFor(x => x.PurchaserId)
                .NotEmpty()
                .WithMessage("Покупатель обязателен");

            RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage("Город обязателен")
                .MaximumLength(CertificateConstraints.CityNameMaxLength)
                .WithMessage($"Длина названия не может быть больше {CertificateConstraints.CityNameMaxLength}");

            RuleFor(x => x.Products)
                .NotEmpty()
                .WithMessage("В акте должен быть хотя бы один продукт");

            RuleForEach(x => x.Products)
                .ChildRules(product =>
                {
                    product.RuleFor(x => x.ProductId)
                        .NotEmpty()
                        .WithMessage("Продукт обязателен");

                    product.RuleFor(x => x.Count)
                        .InclusiveBetween(CertificateConstraints.MinCount, CertificateConstraints.MaxCount)
                        .WithMessage(
                            $"Количество продуктов в позиции должно быть больше {CertificateConstraints.MinCount - 1} " +
                            $"и не более {CertificateConstraints.MaxCount}");

                    product.RuleFor(x => x.Price)
                        .GreaterThan(0)
                        .WithMessage("Цена должна быть больше 0");
                });
        }
    }
}
