using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BankingCreditSystem.Application.Features.IndividualCustomers.Rules;
using BankingCreditSystem.Application.Features.IndividualCustomers.Constants;

namespace BankingCreditSystem.Application.Features.IndividualCustomers.Commands.Delete
{
   public class DeleteIndividualCustomerCommand : IRequest<DeletedIndividualCustomerResponse>
   {
       public Guid Id { get; set; }

       public class DeleteIndividualCustomerCommandHandler : IRequestHandler<DeleteIndividualCustomerCommand, DeletedIndividualCustomerResponse>
       {
           private readonly IIndividualCustomerRepository _individualCustomerRepository;
           private readonly IndividualCustomerBusinessRules _businessRules;

           public DeleteIndividualCustomerCommandHandler(
               IIndividualCustomerRepository individualCustomerRepository,
               IndividualCustomerBusinessRules businessRules)
           {
               _individualCustomerRepository = individualCustomerRepository;
               _businessRules = businessRules;
           }

           public async Task<DeletedIndividualCustomerResponse> Handle(DeleteIndividualCustomerCommand command, CancellationToken cancellationToken)
           {
               await _businessRules.CustomerShouldExistWhenRequested(command.Id);

               var individualCustomer = await _individualCustomerRepository.GetAsync(c => c.Id == command.Id);
               individualCustomer.IsActive = false;
               await _individualCustomerRepository.DeleteAsync(individualCustomer);

               return new DeletedIndividualCustomerResponse
               {
                   Id = command.Id,
                   Message = IndividualCustomerMessages.CustomerDeleted
               };
           }
       }
   }
}