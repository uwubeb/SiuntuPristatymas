using AutoMapper;
using SiuntuPristatymas.Data.Dtos;
using SiuntuPristatymas.Data.Models;

namespace SiuntuPristatymas.Models
{
    public class DeliveryProfile : Profile
    {
        public DeliveryProfile()
        {
            CreateMap<Delivery,DeliveryDto>();
            CreateMap<DeliveryDto, Delivery>();
        }
    }
}
