using AutoMapper;
using BillSale.API.Models;
using BillSale.BLL.Services.Contracts.Models.Certificate;

namespace BillSale.API.Automapper
{
    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<CertificateModel, CertificateApiModel>();
        }
    }
}
