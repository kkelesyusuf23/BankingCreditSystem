using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using BankingCreditSystem.Application.Features.IndividualCustomers.Rules;

namespace BankingCreditSystem.Application.Features.IndividualCustomers.Queries.GetById
{
    public class GetByIdIndividualCustomerQuery : IRequest<IndividualCustomerResponse>
    {
        public Guid Id { get; set; }

        public class GetByIdIndividualCustomerQueryHandler : IRequestHandler<GetByIdIndividualCustomerQuery, IndividualCustomerResponse>
        {
            private readonly IIndividualCustomerRepository _individualCustomerRepository;
            private readonly IMapper _mapper;
            private readonly IndividualCustomerBusinessRules _businessRules;

            public GetByIdIndividualCustomerQueryHandler(
                IIndividualCustomerRepository individualCustomerRepository,
                IMapper mapper,
                IndividualCustomerBusinessRules businessRules)
            {
                _individualCustomerRepository = individualCustomerRepository;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<IndividualCustomerResponse> Handle(GetByIdIndividualCustomerQuery request, CancellationToken cancellationToken)
            {
                await _businessRules.CustomerShouldExistWhenRequested(request.Id);

                var customer = await _individualCustomerRepository.GetAsync(c => c.Id == request.Id);
                return _mapper.Map<IndividualCustomerResponse>(customer);
            }
        }
    }
}