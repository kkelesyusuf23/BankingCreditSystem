using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using BankingCreditSystem.Application.Features.CorporateCustomers.Rules;

namespace BankingCreditSystem.Application.Features.CorporateCustomers.Commands.Update
{
    public class UpdateCorporateCustomerCommand : IRequest<UpdatedCorporateCustomerResponse>
    {
        public UpdateCorporateCustomerRequest Request { get; set; } = default!;

        public class UpdateCorporateCustomerCommandHandler : IRequestHandler<UpdateCorporateCustomerCommand, UpdatedCorporateCustomerResponse>
        {
            private readonly ICorporateCustomerRepository _corporateCustomerRepository;
            private readonly IMapper _mapper;
            private readonly CorporateCustomerBusinessRules _businessRules;

            public UpdateCorporateCustomerCommandHandler(
                ICorporateCustomerRepository corporateCustomerRepository,
                IMapper mapper,
                CorporateCustomerBusinessRules businessRules)
            {
                _corporateCustomerRepository = corporateCustomerRepository;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<UpdatedCorporateCustomerResponse> Handle(UpdateCorporateCustomerCommand command, CancellationToken cancellationToken)
            {
                await _businessRules.CustomerShouldExistWhenRequested(command.Request.Id);

                var corporateCustomer = await _corporateCustomerRepository.GetAsync(c => c.Id == command.Request.Id);
                _mapper.Map(command.Request, corporateCustomer);

                var updatedCustomer = await _corporateCustomerRepository.UpdateAsync(corporateCustomer);
                var response = _mapper.Map<UpdatedCorporateCustomerResponse>(updatedCustomer);
                response.Message = CorporateCustomerMessages.CustomerUpdated;

                return response;
            }
        }
    }
}