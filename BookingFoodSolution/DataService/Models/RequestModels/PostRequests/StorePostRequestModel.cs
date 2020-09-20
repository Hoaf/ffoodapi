using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Models.RequestModels.PostRequests
{
   public class StorePostRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string Address { get; set; }
        public string IdAddress { get; set; }
        public string UrlImageDefault { get; set; }
        public int TimeOpen { get; set; }
        public int TimeClose { get; set; }
        public int AccountId { get; set; }
    }
}
