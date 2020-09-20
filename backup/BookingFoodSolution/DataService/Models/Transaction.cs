using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public double? Money { get; set; }
        public DateTime? Created { get; set; }
        public int? OrderId { get; set; }
        public int? IsDelete { get; set; }
        public int? WalletId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Wallet Wallet { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
