using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class Wallet
    {
        public Wallet()
        {
            Transaction = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public double? Balance { get; set; }
        public int AccountId { get; set; }
        public int? IsDelete { get; set; }

        public virtual Account Account { get; set; }
        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}
