using AutoMapper;
using InterviewTest.Application.Orders.Dtos;
using InterviewTest.Domain.Entities;

namespace InterviewTest.Application.Common.Mapping;

// Perfil de AutoMapper usado por las Queries de pedidos (Entity -> DTO de lectura).
public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDto>();
    }
}
