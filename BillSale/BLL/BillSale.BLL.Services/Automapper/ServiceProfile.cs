using AutoMapper;
using BillSale.BLL.Services.Contracts.Models.Certificate;
using BillSale.BLL.Services.Contracts.Models.Certificate.ProductItem;
using BillSale.BLL.Services.Contracts.Models.Company;
using BillSale.Entities;

namespace BillSale.BLL.Services.Automapper
{
    /// <summary>
    /// Профиль маппера сервиса
    /// </summary>
    public class ServiceProfile : Profile
    {
        /// <summary>
        /// ctor профиль
        /// </summary>
        public ServiceProfile()
        {
            CreateMap<Certificate, CertificateModel>()
                .ForMember(
                    dest => dest.SellerName,
                    opt => opt.MapFrom(src => src.Seller.OrganizationName))
                .ForMember(
                    dest => dest.PurchaserName,
                    opt => opt.MapFrom(src => src.Purchaser.OrganizationName));

            CreateMap<Certificate, CertificateDetailModel>();
            CreateMap<Company, CompanyModel>();

            CreateMap<CertificateProduct, CertificateProductDetailsModel>()
                .ForMember(
                    dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(
                    dest => dest.MeasureUnit,
                    opt => opt.MapFrom(src => src.Product.MeasureUnit));

            CreateMap<Certificate, CertificateDetailModel>()
                .ForMember(
                    dest => dest.Products,
                    opt => opt.MapFrom(src => src.ProductItems));

            CreateMap<CertificateCreateModel, Certificate>()
                .ForMember(
                    dest => dest.ProductItems,
                    opt => opt.MapFrom(src => src.Products));

            CreateMap<CertificateProductCreateModel, CertificateProduct>();

            CreateMap<CertificateUpdateModel, Certificate>()
                .ForMember(
                    dest => dest.ProductItems,
                    opt => opt.Ignore());

            CreateMap<CertificateProductUpdateModel, CertificateProduct>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CertificateId, opt => opt.Ignore());
        }
    }
}
