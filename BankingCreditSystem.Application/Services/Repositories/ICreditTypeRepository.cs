using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankingCreditSystem.Application.Services.Repositories;
using BankingCreditSystem.Domain.Entities;

namespace BankingCreditSystem.Application.Services.Repositories
{
    public interface ICreditTypeRepository : IAsyncRepository<CreditType, Guid>
    {
        Task<IList<CreditType>> GetByCustomerTypeAsync(CustomerType customerType);
        Task<IList<CreditType>> GetSubCreditTypesAsync(Guid parentCreditTypeId);
    }
}