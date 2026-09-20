using InterviewTest.Domain.Entities;
using InterviewTest.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InterviewTest.Infrastructure.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Customers
            .Include(i => i.Orders)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
