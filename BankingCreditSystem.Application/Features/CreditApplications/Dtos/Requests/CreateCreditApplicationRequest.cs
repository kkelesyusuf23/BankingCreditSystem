using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingCreditSystem.Application.Features.CreditApplications.Dtos.Requests
{
    public class CreateCreditApplicationRequest
    {
        public Guid CreditTypeId { get; set; }
        public Guid CustomerId { get; set; }
        public decimal RequestedAmount { get; set; }
        public int RequestedTerm { get; set; }
    }
}
