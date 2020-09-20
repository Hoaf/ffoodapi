using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Models.RequestModels.PutRequests
{
    public class AccountPutRequestModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int? SexId { get; set; }
        public int? PhoneNumber { get; set; }
        public string Gmail { get; set; }
        public long? Birthday { get; set; }
        public string UrlImage { get; set; }

    }
}
