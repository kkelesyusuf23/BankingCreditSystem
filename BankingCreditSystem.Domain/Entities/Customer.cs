namespace BankingCreditSystem.Domain.Entities;

using BankingCreditSystem.Core.Repositories;

public abstract class Customer : Entity<Guid>
{
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Address { get; set; } = default!;
    public bool IsActive { get; set; } = true;

    protected Customer()
    {
        IsActive = true;
    }
} 