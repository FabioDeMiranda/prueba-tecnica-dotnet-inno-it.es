using AutoMapper;
using InterviewTest.Application.Customers.Dtos;
using InterviewTest.Domain.Entities;

namespace InterviewTest.Application.Common.Mapping;

// Perfil de AutoMapper para clientes. OrderCount se calcula en el handler.
public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.OrderCount, opt => opt.Ignore());
    }
}
