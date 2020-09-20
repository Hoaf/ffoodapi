using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Models.RequestModels.PostRequests
{
   public class StorePostRequestModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int AccountId { get; set; }
    }
}
