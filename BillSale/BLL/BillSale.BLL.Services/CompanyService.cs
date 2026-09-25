using AutoMapper;
using BillSale.BLL.Services.Contracts;
using BillSale.BLL.Services.Contracts.Exceptions;
using BillSale.BLL.Services.Contracts.Models.Company;
using BillSale.DAL.Contracts.Repositories;
using BillSale.DAL.Repositories.Contracts;
using BillSale.Entities;

namespace BillSale.BLL.Services
{
    /// <summary>
    /// Сервис для работы с <see cref="Company"/>
    /// </summary>
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository companyRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="companyRepository">Репозиторий сущности</param>
        /// <param name="unitOfWork">Обьект еденицы работы</param>
        /// <param name="mapper">маппер</param>
        public CompanyService(ICompanyRepository companyRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.companyRepository = companyRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<CompanyModel>> GetCompaniesAsync(CancellationToken cancellationToken)
        {
            var companies = await companyRepository.GetCompaniesAsync(cancellationToken);
            return mapper.Map<IReadOnlyCollection<CompanyModel>>(companies);
        }

        /// <inheritdoc/>
        public async Task<CompanyModel> GetCompanyByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await companyRepository.GetCompanyByIdAsync(id, cancellationToken);
            if (entity is null)
            {
                throw new EntityNotFoundException<Company>(id);
            }

            return mapper.Map<CompanyModel>(entity);
        }

        /// <inheritdoc/>
        public async Task<CompanyModel> AddCompanyAsync(CompanyCreateModel companyModel, CancellationToken cancellationToken)
        {
            var company = mapper.Map<Company>(companyModel);
            companyRepository.Add(company);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<CompanyModel>(company);
        }

        /// <inheritdoc/>
        public async Task UpdateCompanyAsync(CompanyModel companyModel, CancellationToken cancellationToken)
        {
            var entity = await companyRepository.GetCompanyByIdAsync(companyModel.Id, cancellationToken);
            if (entity is null)
            {
                throw new EntityNotFoundException<Company>(companyModel.Id);
            }

            mapper.Map(companyModel, entity);
            companyRepository.Update(entity);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task DeleteCompanyAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await companyRepository.GetCompanyByIdAsync(id, cancellationToken);
            if (entity is null)
            {
                throw new EntityNotFoundException<Company>(id);
            }

            companyRepository.Delete(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
