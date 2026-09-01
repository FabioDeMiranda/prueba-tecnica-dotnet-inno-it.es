using InterviewTest.Application.Customers.Dtos;
using MediatR;

namespace InterviewTest.Application.Customers.Queries.GetAllCustomers;

public record GetAllCustomersQuery : IRequest<IReadOnlyList<CustomerDto>>;
