using BillSale.BLL.Services.Contracts.Models.Certificate;
using FluentValidation;

namespace BillSale.BLL.Services.Validators.Certificate
{
    public   class CertificateUpdateModelValidator : AbstractValidator<CertificateCreateModel>
    {
        public CertificateUpdateModelValidator()
        {
            RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage("Описание объязательно")
                .MaximumLength(100)
                .WithMessage("Длинна названия не может быть больше 100");
        }
    }
}
