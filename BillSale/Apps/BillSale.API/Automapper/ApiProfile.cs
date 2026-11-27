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
            CreateMap<CertificateProductCreateApiModel, CertificateProductCreateModel>();

            CreateMap<CertificateDetailModel, CertificateDetailsApiModel>();
            CreateMap<CertificateProductDetailsModel, CertificateProductDetailsApiModel>();
            CreateMap<CertificateUpdateApiModel, CertificateUpdateModel>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore());
            CreateMap<CertificateProductUpdateApiModel, CertificateProductUpdateModel>();

            CreateMap<ProductModel, ProductApiModel>();
            CreateMap<ProductCreateApiModel, ProductCreateModel>();
            CreateMap<ProductUpdateApiModel, ProductUpdateModel>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore());

            CreateMap<CompanyModel, CompanyApiModel>();
            CreateMap<CompanyCreateApiModel, CompanyCreateModel>();
            CreateMap<CompanyUpdateApiModel, CompanyUpdateModel>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore());
        }
    }
}
