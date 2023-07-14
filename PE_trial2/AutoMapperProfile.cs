using AutoMapper;

using Microsoft.Extensions.Options;

using PE_trial2.Models;

using static PE_trial2.DTOs;

namespace PE_trial2 {
    public class AutoMapperProfile : Profile {
        public AutoMapperProfile()
        {
            CreateMap<Order, OrderDTO>().ForMember(x => x.CustomerName, opt => opt.MapFrom(source => source.Customer.CompanyName));
        }
    }
}
