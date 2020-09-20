using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Models.RequestModels
{
    public class PaymentFilterModel
    {
        public int PaymentId { get; set; }
        public int TransactionId { get; set; }
    }
}
