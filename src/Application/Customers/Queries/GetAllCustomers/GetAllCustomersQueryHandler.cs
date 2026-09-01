using AutoMapper;
using InterviewTest.Application.Customers.Dtos;
using InterviewTest.Domain.Repositories;
using MediatR;

namespace InterviewTest.Application.Customers.Queries.GetAllCustomers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IReadOnlyList<CustomerDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetAllCustomersQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAllAsync(cancellationToken);

        var result = new List<CustomerDto>();
        foreach (var customer in customers)
        {
            var dto = _mapper.Map<CustomerDto>(customer);
            dto.OrderCount = customer.Orders.Count;
            result.Add(dto);
        }

        return result;
    }
}
