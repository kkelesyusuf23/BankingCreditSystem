using System;
using System.Collections.Generic;
using BankingCreditSystem.Core.Repositories;
using BankingCreditSystem.Domain.Entities;

namespace BankingCreditSystem.Domain.Entities
{
    public class CreditType : Entity<Guid>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public CustomerType CustomerType { get; set; } // Enum: Individual, Corporate
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
        public int MinTerm { get; set; } // Ay cinsinden
        public int MaxTerm { get; set; } // Ay cinsinden
        public decimal BaseInterestRate { get; set; }

        public Guid? ParentCreditTypeId { get; set; }
        public virtual CreditType? ParentCreditType { get; set; }
        public virtual ICollection<CreditType> SubCreditTypes { get; set; }
        public virtual ICollection<CreditApplication> CreditApplications { get; set; }
    }
}