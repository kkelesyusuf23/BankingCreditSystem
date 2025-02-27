using BankingCreditSystem.Core.CrossCuttingConcerns.Exceptions.Types;
using System;
using System.Threading.Tasks;

namespace BankingCreditSystem.Application.Features.CorporateCustomers.Rules
{
    public class CorporateCustomerBusinessRules
    {
        private readonly ICorporateCustomerRepository _corporateCustomerRepository;

        public CorporateCustomerBusinessRules(ICorporateCustomerRepository corporateCustomerRepository)
        {
            _corporateCustomerRepository = corporateCustomerRepository;
        }

        public async Task TaxNumberCannotBeDuplicatedWhenInserted(string taxNumber)
        {
            var result = await _corporateCustomerRepository.AnyAsync(c => c.TaxNumber == taxNumber);
            if (result)
                throw new BusinessException(CorporateCustomerMessages.TaxNumberAlreadyExists);
        }

        public async Task CustomerShouldExistWhenRequested(Guid id)
        {
            var result = await _corporateCustomerRepository.GetAsync(c => c.Id == id);
            if (result == null)
                throw new BusinessException(CorporateCustomerMessages.CustomerNotFound);
        }
    }
}