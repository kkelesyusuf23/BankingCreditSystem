using System;
using System.Threading;
using System.Threading.Tasks;
using BankingCreditSystem.Application.Features.CorporateCustomers.Rules;
using MediatR;


namespace BankingCreditSystem.Application.Features.CorporateCustomers.Commands.Delete
{
    public class DeleteCorporateCustomerCommand : IRequest<DeletedCorporateCustomerResponse>
    {
        public Guid Id { get; set; }

        public class DeleteCorporateCustomerCommandHandler : IRequestHandler<DeleteCorporateCustomerCommand, DeletedCorporateCustomerResponse>
        {
            private readonly ICorporateCustomerRepository _corporateCustomerRepository;
            private readonly CorporateCustomerBusinessRules _businessRules;

            public DeleteCorporateCustomerCommandHandler(
                ICorporateCustomerRepository corporateCustomerRepository,
                CorporateCustomerBusinessRules businessRules)
            {
                _corporateCustomerRepository = corporateCustomerRepository;
                _businessRules = businessRules;
            }

            public async Task<DeletedCorporateCustomerResponse> Handle(DeleteCorporateCustomerCommand command, CancellationToken cancellationToken)
            {
                await _businessRules.CustomerShouldExistWhenRequested(command.Id);

                var corporateCustomer = await _corporateCustomerRepository.GetAsync(c => c.Id == command.Id);
                await _corporateCustomerRepository.DeleteAsync(corporateCustomer);

                return new DeletedCorporateCustomerResponse
                {
                    Id = command.Id,
                    Message = CorporateCustomerMessages.CustomerDeleted
                };
            }
        }
    }
}