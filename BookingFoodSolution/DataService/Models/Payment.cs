using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class Payment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? IsDelete { get; set; }
        public int? TransactionId { get; set; }

        public virtual Transaction IdNavigation { get; set; }
    }
}
