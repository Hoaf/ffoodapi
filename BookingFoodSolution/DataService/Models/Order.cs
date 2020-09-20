using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetail = new HashSet<OrderDetail>();
            Transaction = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public int? AccountId { get; set; }
        public string Note { get; set; }
        public double? TotalMoney { get; set; }
        public int? PaymentId { get; set; }
        public int? Status { get; set; }
        public DateTime? Created { get; set; }
        public int? IsDelete { get; set; }

        public virtual Account Account { get; set; }
        public virtual UserFeedBack UserFeedBack { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}
