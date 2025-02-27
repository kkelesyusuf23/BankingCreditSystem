using MediatR;
using AutoMapper;
using BankingCreditSystem.Domain.Entities;
using BankingCreditSystem.Application.Services.Repositories;
using BankingCreditSystem.Application.Features.CreditApplications.Dtos.Requests;
using BankingCreditSystem.Application.Features.CreditApplications.Dtos.Responses;

namespace BankingCreditSystem.Application.Features.CreditApplications.Commands.Create
{
    public class CreateCreditApplicationCommand : IRequest<CreditApplicationResponse>
    {
        public CreateCreditApplicationRequest Request { get; set; } = default!;

        public class CreateCreditApplicationCommandHandler : IRequestHandler<CreateCreditApplicationCommand, CreditApplicationResponse>
        {
            private readonly ICreditApplicationRepository _creditApplicationRepository;
            private readonly ICreditTypeRepository _creditTypeRepository;
            private readonly IMapper _mapper;

            public CreateCreditApplicationCommandHandler(
                ICreditApplicationRepository creditApplicationRepository,
                ICreditTypeRepository creditTypeRepository,
                IMapper mapper)
            {
                _creditApplicationRepository = creditApplicationRepository;
                _creditTypeRepository = creditTypeRepository;
                _mapper = mapper;
            }

            public async Task<CreditApplicationResponse> Handle(CreateCreditApplicationCommand command, CancellationToken cancellationToken)
            {
                var creditType = await _creditTypeRepository.GetAsync(c => c.Id == command.Request.CreditTypeId);

                var application = _mapper.Map<CreditApplication>(command.Request);
                application.Status = CreditApplicationStatus.Pending;
                application.ApprovedAmount = command.Request.RequestedAmount;
                application.ApprovedTerm = command.Request.RequestedTerm;
                application.InterestRate = creditType.BaseInterestRate;

                // Basit bir aylık ödeme hesaplaması
                var monthlyInterestRate = (double)(application.InterestRate / 1200m);
                application.MonthlyPayment = (decimal)(
                    (double)application.ApprovedAmount * monthlyInterestRate *
                    Math.Pow(1 + monthlyInterestRate, (double)application.ApprovedTerm)) /
                    (decimal)(Math.Pow(1 + monthlyInterestRate, (double)application.ApprovedTerm) - 1);

                application.TotalPayment = application.MonthlyPayment * application.ApprovedTerm;

                var createdApplication = await _creditApplicationRepository.AddAsync(application);
                return _mapper.Map<CreditApplicationResponse>(createdApplication);
            }
        }
    }
}