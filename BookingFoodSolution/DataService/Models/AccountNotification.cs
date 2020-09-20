using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class AccountNotification
    {
        public int AccountId { get; set; }
        public int NotificationId { get; set; }
        public int? Seen { get; set; }
        public DateTime? Created { get; set; }
        public int? IsDelete { get; set; }

        public virtual Account Account { get; set; }
        public virtual Notification Notification { get; set; }
    }
}
