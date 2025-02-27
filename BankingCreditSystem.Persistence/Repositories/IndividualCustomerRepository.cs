namespace BankingCreditSystem.Persistence.Repositories;


using BankingCreditSystem.Domain.Entities;

public class IndividualCustomerRepository : EfRepositoryBase<IndividualCustomer, Guid, BaseDbContext>, IIndividualCustomerRepository
{
    public IndividualCustomerRepository(BaseDbContext context) : base(context)
    {
    }
} 