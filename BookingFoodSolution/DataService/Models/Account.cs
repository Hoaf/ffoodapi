using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class Account
    {
        public Account()
        {
            AccountNotification = new HashSet<AccountNotification>();
            Order = new HashSet<Order>();
            Store = new HashSet<Store>();
            Wallet = new HashSet<Wallet>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? SexId { get; set; }
        public int? PhoneNumber { get; set; }
        public string Gmail { get; set; }
        public long? Birthday { get; set; }
        public int RoleId { get; set; }
        public int? FeedbackId { get; set; }
        public string UrlImage { get; set; }
        public string DeviceToken { get; set; }
        public int? IsDelete { get; set; }

        public virtual ICollection<AccountNotification> AccountNotification { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<Store> Store { get; set; }
        public virtual ICollection<Wallet> Wallet { get; set; }
    }
}
