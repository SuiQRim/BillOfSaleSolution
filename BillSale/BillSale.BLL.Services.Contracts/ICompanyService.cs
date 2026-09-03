using BillSale.BLL.Services.Contracts.Models.Company;

namespace BillSale.BLL.Services.Contracts
{
    /// <summary>
    /// Сервис для работы с компаниями
    /// </summary>
    public interface ICompanyService
    {
        /// <summary>
        /// Получение списка компаний
        /// </summary>
        /// <param name="cancellationToken">Токен отслеживания</param>
        /// <returns>Список компаний</returns>
        Task<IReadOnlyCollection<CompanyModel>> GetCompaniesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получение компании по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken">Токен отслеживания</param>
        /// <returns>Компания</returns>
        Task<CompanyModel> GetCompanyByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавление компании
        /// </summary>
        /// <param name="companyModel">Компания</param>
        /// <param name="cancellationToken">Токен отслеживания</param>
        Task AddCompanyAsync(CompanyCreateModel companyModel, CancellationToken cancellationToken);

        /// <summary>
        /// Обновление компании
        /// </summary>
        /// <param name="companyModel">Компания</param>
        /// <param name="cancellationToken">Токен отслеживания</param>
        Task UpdateCompanyAsync(CompanyModel companyModel, CancellationToken cancellationToken);

        /// <summary>
        /// Удаление компании
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken">Токен отслеживания</param>
        Task DeleteCompanyAsync(Guid id, CancellationToken cancellationToken);
    }
}
