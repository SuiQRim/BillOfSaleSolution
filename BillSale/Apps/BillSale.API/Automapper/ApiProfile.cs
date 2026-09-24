using AutoMapper;
using BillSale.API.Models.Certificate;
using BillSale.API.Models.Certificate.ProductItem;
using BillSale.API.Models.Company;
using BillSale.API.Models.Product;
using BillSale.BLL.Services.Contracts.Models.Certificate;
using BillSale.BLL.Services.Contracts.Models.Certificate.ProductItem;
using BillSale.BLL.Services.Contracts.Models.Company;
using BillSale.BLL.Services.Contracts.Models.Product;

namespace BillSale.API.Automapper
{
    /// <summary>
    /// Профиль маппера
    /// </summary>
    public class ApiProfile : Profile
    {
        /// <summary>
        /// ctor профиль
        /// </summary>
        public ApiProfile()
        {
            CreateMap<CertificateModel, CertificateApiModel>();
            CreateMap<CertificateCreateApiModel, CertificateCreateModel>();
            CreateMap<ProductCreateApiModel, ProductCreateModel>();
            CreateMap<CertificateProductCreateApiModel, CertificateProductCreateModel>();

            CreateMap<CertificateDetailModel, CertificateDetailsApiModel>();
            CreateMap<CertificateProductDetailsModel, CertificateProductDetailsApiModel>();
            CreateMap<ProductModel, ProductApiModel>();
            CreateMap<CompanyModel, CompanyDetailsApiModel>();
            CreateMap<CertificateUpdateApiModel, CertificateUpdateModel>();
            CreateMap<CertificateProductUpdateApiModel, CertificateProductUpdateModel>();
        }
    }
}
