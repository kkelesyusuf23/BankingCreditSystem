using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using BankingCreditSystem.Domain.Entities;
using BankingCreditSystem.Application.Features.CorporateCustomers.Rules;

namespace BankingCreditSystem.Application.Features.CorporateCustomers.Commands.Create
{
    public class CreateCorporateCustomerCommand : IRequest<CreatedCorporateCustomerResponse>
    {
        public CreateCorporateCustomerRequest Request { get; set; } = default!;

        public class CreateCorporateCustomerCommandHandler : IRequestHandler<CreateCorporateCustomerCommand, CreatedCorporateCustomerResponse>
        {
            private readonly ICorporateCustomerRepository _corporateCustomerRepository;
            private readonly IMapper _mapper;
            private readonly CorporateCustomerBusinessRules _businessRules;

            public CreateCorporateCustomerCommandHandler(
                ICorporateCustomerRepository corporateCustomerRepository,
                IMapper mapper,
                CorporateCustomerBusinessRules businessRules)
            {
                _corporateCustomerRepository = corporateCustomerRepository;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<CreatedCorporateCustomerResponse> Handle(CreateCorporateCustomerCommand command, CancellationToken cancellationToken)
            {
                await _businessRules.TaxNumberCannotBeDuplicatedWhenInserted(command.Request.TaxNumber);

                var corporateCustomer = _mapper.Map<CorporateCustomer>(command.Request);
                var createdCustomer = await _corporateCustomerRepository.AddAsync(corporateCustomer);

                var response = _mapper.Map<CreatedCorporateCustomerResponse>(createdCustomer);
                response.Message = CorporateCustomerMessages.CustomerCreated;

                return response;
            }
        }
    }
}