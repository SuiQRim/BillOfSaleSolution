using AutoMapper;
using BillSale.BLL.Services.Contracts.Models.Certificate;
using BillSale.Entities;

namespace BillSale.BLL.Services.Automapper
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<TransferCertificate, CertificateModel>().ReverseMap();
            CreateMap<CertificateCreateModel, TransferCertificate>();
        }
    }
}
