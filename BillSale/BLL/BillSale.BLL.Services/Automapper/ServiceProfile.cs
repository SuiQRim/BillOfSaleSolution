using AutoMapper;
using BillSale.BLL.Services.Contracts.Models.Certificate;
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

            CreateMap<CertificateCreateModel, TransferCertificate>();
        }
    }
}
