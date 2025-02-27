using BankingCreditSystem.Application.Features.IndividualCustomers.Constants;
using BankingCreditSystem.Core.CrossCuttingConcerns.Exceptions.Types;

namespace BankingCreditSystem.Application.Features.IndividualCustomers.Rules
{
    public class IndividualCustomerBusinessRules
    {
        private readonly IIndividualCustomerRepository _individualCustomerRepository;

        public IndividualCustomerBusinessRules(IIndividualCustomerRepository individualCustomerRepository)
        {
            _individualCustomerRepository = individualCustomerRepository;
        }

        public async Task NationalIdCannotBeDuplicatedWhenInserted(string nationalId)
        {
            var result = await _individualCustomerRepository.AnyAsync(c => c.NationalId == nationalId);
            if (result)
                throw new BusinessException(IndividualCustomerMessages.NationalIdAlreadyExists);
        }

        public async Task CustomerShouldExistWhenRequested(Guid id)
        {
            var result = await _individualCustomerRepository.GetAsync(c => c.Id == id);
            if (result == null)
                throw new BusinessException(IndividualCustomerMessages.CustomerNotFound);
        }
    }
}