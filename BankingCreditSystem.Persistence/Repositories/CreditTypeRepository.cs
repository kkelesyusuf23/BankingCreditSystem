
using Microsoft.EntityFrameworkCore;
using BankingCreditSystem.Application.Services.Repositories;
using BankingCreditSystem.Domain.Entities;

namespace BankingCreditSystem.Persistence.Repositories
{
    public class CreditTypeRepository : EfRepositoryBase<CreditType, Guid, BaseDbContext>, ICreditTypeRepository
    {
        public CreditTypeRepository(BaseDbContext context) : base(context)
        {
        }

        public async Task<IList<CreditType>> GetByCustomerTypeAsync(CustomerType customerType)
        {
            return await Context.CreditTypes
                .Where(c => c.CustomerType == customerType)
                .ToListAsync();
        }

        public async Task<IList<CreditType>> GetSubCreditTypesAsync(Guid parentCreditTypeId)
        {
            return await Context.CreditTypes
                .Where(c => c.ParentCreditTypeId == parentCreditTypeId)
                .ToListAsync();
        }
    }
}