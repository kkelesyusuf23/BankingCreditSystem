using AutoMapper;
using BankingCreditSystem.Application.Features.CreditTypes.Dtos.Responses;
using BankingCreditSystem.Application.Services.Repositories;
using BankingCreditSystem.Domain.Entities;
using MediatR;

namespace BankingCreditSystem.Application.Features.CreditTypes.Commands.Create
{
    public class CreateCreditTypeCommand : IRequest<CreditTypeResponse>
    {
        public CreateCreditTypeRequest Request { get; set; } = default!;

        public class CreateCreditTypeCommandHandler : IRequestHandler<CreateCreditTypeCommand, CreditTypeResponse>
        {
            private readonly ICreditTypeRepository _creditTypeRepository;
            private readonly IMapper _mapper;

            public CreateCreditTypeCommandHandler(ICreditTypeRepository creditTypeRepository, IMapper mapper)
            {
                _creditTypeRepository = creditTypeRepository;
                _mapper = mapper;
            }

            public async Task<CreditTypeResponse> Handle(CreateCreditTypeCommand command, CancellationToken cancellationToken)
            {
                var creditType = _mapper.Map<CreditType>(command.Request);
                var createdCreditType = await _creditTypeRepository.AddAsync(creditType);
                return _mapper.Map<CreditTypeResponse>(createdCreditType);
            }
        }
    }
}
