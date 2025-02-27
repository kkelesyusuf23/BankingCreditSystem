using BankingCreditSystem.Core.Repositories;
using BankingCreditSystem.Domain.Entities;

public interface ICustomerRepository<T> : IAsyncRepository<T, Guid> where T : Customer
{
}