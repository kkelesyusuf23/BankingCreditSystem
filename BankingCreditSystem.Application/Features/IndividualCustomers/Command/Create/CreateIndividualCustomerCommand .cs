using MediatR;
using AutoMapper;
using BankingCreditSystem.Application.Features.IndividualCustomers.Rules;
using BankingCreditSystem.Application.Features.IndividualCustomers.Constants;
using BankingCreditSystem.Domain.Entities;

namespace BankingCreditSystem.Application.Features.IndividualCustomers.Commands.Create
{
    public class CreateIndividualCustomerCommand : IRequest<CreatedIndividualCustomerResponse>
    {
        public CreateIndividualCustomerRequest Request { get; set; } = default!;

        public class CreateIndividualCustomerCommandHandler : IRequestHandler<CreateIndividualCustomerCommand, CreatedIndividualCustomerResponse>
        {
            private readonly IIndividualCustomerRepository _individualCustomerRepository;
            private readonly IMapper _mapper;
            private readonly IndividualCustomerBusinessRules _businessRules;

            public CreateIndividualCustomerCommandHandler(
                IIndividualCustomerRepository individualCustomerRepository,
                IMapper mapper,
                IndividualCustomerBusinessRules businessRules)
            {
                _individualCustomerRepository = individualCustomerRepository;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<CreatedIndividualCustomerResponse> Handle(CreateIndividualCustomerCommand command, CancellationToken cancellationToken)
            {
                await _businessRules.NationalIdCannotBeDuplicatedWhenInserted(command.Request.NationalId);

                var individualCustomer = _mapper.Map<IndividualCustomer>(command.Request);
                var createdCustomer = await _individualCustomerRepository.AddAsync(individualCustomer);

                var response = _mapper.Map<CreatedIndividualCustomerResponse>(createdCustomer);
                response.Message = IndividualCustomerMessages.CustomerCreated;

                return response;
            }
        }
    }
}