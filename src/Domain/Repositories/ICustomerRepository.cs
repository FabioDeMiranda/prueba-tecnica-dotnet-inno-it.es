using InterviewTest.Domain.Entities;

namespace InterviewTest.Domain.Repositories;

public interface ICustomerRepository
{
    Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken cancellationToken = default);
}
