using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Models.ServiceModels
{
   public class AccountServiceModel
    {
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
        public int? WalletId { get; set; }
        public int RoleId { get; set; }
        public int? FeedbackId { get; set; }
        public string UrlImage { get; set; }
        public string DeviceToken { get; set; }
        public int? IsDelete { get; set; }
        public string Token { get; set; }
        

    }
}
