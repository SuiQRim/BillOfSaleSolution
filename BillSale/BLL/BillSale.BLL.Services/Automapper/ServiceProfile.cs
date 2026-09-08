using AutoMapper;
using BillSale.BLL.Services.Contracts.Models.Certificate;
using BillSale.BLL.Services.Contracts.Models.Certificate.ProductItem;
using BillSale.BLL.Services.Contracts.Models.Company;
using BillSale.Entities;

namespace BillSale.BLL.Services.Automapper
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<TransferCertificate, CertificateModel>()
                .ForMember(
                    dest => dest.SellerName,
                    opt => opt.MapFrom(src => src.Seller.OrganizationName))
                .ForMember(
                    dest => dest.PurchaserName,
                    opt => opt.MapFrom(src => src.Purchaser.OrganizationName));

            CreateMap<TransferCertificate, CertificateDetailModel>();
            CreateMap<Seller, CompanyModel>();
            CreateMap<Purchaser, CompanyModel>();
            CreateMap<TransferCertificateProduct, CertificateProductDetailsModel>()
                .ForMember(
                    dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(
                    dest => dest.MeasureUnit,
                    opt => opt.MapFrom(src => src.Product.MeasureUnit));
            CreateMap<TransferCertificate, CertificateDetailModel>()
                .ForMember(
                    dest => dest.Products,
                    opt => opt.MapFrom(src => src.ProductItems));

            CreateMap<CertificateCreateModel, TransferCertificate>()
                   .ForMember(
                       dest => dest.ProductItems,
                       opt => opt.MapFrom(src => src.Products));

            CreateMap<CertificateProductCreateModel, TransferCertificateProduct>();

            CreateMap<CertificateUpdateModel, TransferCertificate>();
        }
    }
}
