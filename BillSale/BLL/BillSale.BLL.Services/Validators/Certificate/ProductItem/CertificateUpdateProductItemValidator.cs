using BillSale.BLL.Services.Contracts.Models.Certificate.ProductItem;
using FluentValidation;

namespace BillSale.BLL.Services.Validators.Certificate.ProductItem
{
    public class CertificateUpdateProductItemValidator : AbstractValidator<CertificateProductCreateModel>
    {
        public CertificateUpdateProductItemValidator()
        {
            RuleFor(x => x.Count)
                .InclusiveBetween(1, 100000)
                .WithMessage("Количество продуктов в позиции должно быть больше 0 и не более 1000000");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Цена должна приносить прибыль");
        }
    }
}
