using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using BankingCreditSystem.Application.Features.IndividualCustomers.Rules;
using BankingCreditSystem.Application.Features.IndividualCustomers.Constants;

namespace BankingCreditSystem.Application.Features.IndividualCustomers.Commands.Update
{
   public class UpdateIndividualCustomerCommand : IRequest<UpdatedIndividualCustomerResponse>
   {
       public UpdateIndividualCustomerRequest Request { get; set; } = default!;

       public class UpdateIndividualCustomerCommandHandler : IRequestHandler<UpdateIndividualCustomerCommand, UpdatedIndividualCustomerResponse>
       {
           private readonly IIndividualCustomerRepository _individualCustomerRepository;
           private readonly IMapper _mapper;
           private readonly IndividualCustomerBusinessRules _businessRules;

           public UpdateIndividualCustomerCommandHandler(
               IIndividualCustomerRepository individualCustomerRepository,
               IMapper mapper,
               IndividualCustomerBusinessRules businessRules)
           {
               _individualCustomerRepository = individualCustomerRepository;
               _mapper = mapper;
               _businessRules = businessRules;
           }

           public async Task<UpdatedIndividualCustomerResponse> Handle(UpdateIndividualCustomerCommand command, CancellationToken cancellationToken)
           {
               await _businessRules.CustomerShouldExistWhenRequested(command.Request.Id);

               var individualCustomer = await _individualCustomerRepository.GetAsync(c => c.Id == command.Request.Id);
               _mapper.Map(command.Request, individualCustomer);

               var updatedCustomer = await _individualCustomerRepository.UpdateAsync(individualCustomer);
               var response = _mapper.Map<UpdatedIndividualCustomerResponse>(updatedCustomer);
               response.Message = IndividualCustomerMessages.CustomerUpdated;

               return response;
           }
       }
   }
}