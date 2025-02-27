using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingCreditSystem.Application.Features.CreditTypes.Dtos.Responses
{
    public class CreditTypeResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public CustomerType CustomerType { get; set; }
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
        public int MinTerm { get; set; }
        public int MaxTerm { get; set; }
        public decimal BaseInterestRate { get; set; }
        public Guid? ParentCreditTypeId { get; set; }
        public List<CreditTypeResponse>? SubCreditTypes { get; set; }
    }
}
