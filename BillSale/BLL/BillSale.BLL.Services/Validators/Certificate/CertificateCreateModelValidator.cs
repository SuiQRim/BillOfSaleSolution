using BillSale.BLL.Services.Contracts.Models.Certificate;
using FluentValidation;

namespace BillSale.BLL.Services.Validators.Certificate
{
    /// <summary>
    /// Валидатор для <see cref="CertificateCreateModel"/>
    /// </summary>
    public class CertificateCreateModelValidator : AbstractValidator<CertificateCreateModel>
    {
        private const int CityNameMaxLength = 100;
        private const int MinCount = 1;
        private const int MaxCount = 100000;

        /// <summary>
        /// ctor
        /// </summary>

        public CertificateCreateModelValidator()
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
                .MaximumLength(CityNameMaxLength)
                .WithMessage($"Длина названия не может быть больше {CityNameMaxLength}");

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
                        .InclusiveBetween(MinCount, MaxCount)
                        .WithMessage(
                            $"Количество продуктов в позиции должно быть больше {MinCount - 1} и не более {MaxCount}");

                    product.RuleFor(x => x.Price)
                        .GreaterThan(0)
                        .WithMessage("Цена должна быть больше 0");
                });
        }
    }
}
