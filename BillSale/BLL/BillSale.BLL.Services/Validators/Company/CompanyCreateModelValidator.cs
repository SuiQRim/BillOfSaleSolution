using BillSale.BLL.Services.Contracts.Models.Company;
using BillSale.BLL.Services.Validators.Constraints;
using FluentValidation;

namespace BillSale.BLL.Services.Validators.Company
{
    /// <summary>
    /// Валидатор для <see cref="CompanyCreateModel"/>
    /// </summary>
    public class CompanyCreateModelValidator : AbstractValidator<CompanyCreateModel>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public CompanyCreateModelValidator()
        {
            RuleFor(x => x.OrganizationName)
                .NotEmpty()
                .WithMessage("Название организации обязательно")
                .MaximumLength(CompanyConstraints.OrganizationNameMaxLength)
                .WithMessage(
                    $"Длина названия организации не может быть больше {CompanyConstraints.OrganizationNameMaxLength}");

            RuleFor(x => x.Post)
                .NotEmpty()
                .WithMessage("Должность обязательна")
                .MaximumLength(CompanyConstraints.PostMaxLength)
                .WithMessage(
                    $"Длина должности не может быть больше {CompanyConstraints.PostMaxLength}");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Имя обязательно")
                .MaximumLength(CompanyConstraints.FirstNameMaxLength)
                .WithMessage(
                    $"Длина имени не может быть больше {CompanyConstraints.FirstNameMaxLength}");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Фамилия обязательна")
                .MaximumLength(CompanyConstraints.LastNameMaxLength)
                .WithMessage(
                    $"Длина фамилии не может быть больше {CompanyConstraints.LastNameMaxLength}");

            RuleFor(x => x.MiddleName)
                .NotEmpty()
                .WithMessage("Отчество обязательно")
                .MaximumLength(CompanyConstraints.MiddleNameMaxLength)
                .WithMessage(
                    $"Длина отчества не может быть больше {CompanyConstraints.MiddleNameMaxLength}");

            RuleFor(x => x.DocumentName)
                .NotEmpty()
                .WithMessage("Название подтверждающего документа обязательно")
                .MaximumLength(CompanyConstraints.DocumentNameMaxLength)
                .WithMessage(
                    $"Длина названия документа не может быть больше {CompanyConstraints.DocumentNameMaxLength}");
        }
    }
}
