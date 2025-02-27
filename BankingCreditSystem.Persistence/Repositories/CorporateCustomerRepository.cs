namespace BankingCreditSystem.Persistence.Repositories;


using BankingCreditSystem.Domain.Entities;

public class CorporateCustomerRepository : EfRepositoryBase<CorporateCustomer, Guid, BaseDbContext>, ICorporateCustomerRepository
{
    public CorporateCustomerRepository(BaseDbContext context) : base(context)
    {
    }
} 