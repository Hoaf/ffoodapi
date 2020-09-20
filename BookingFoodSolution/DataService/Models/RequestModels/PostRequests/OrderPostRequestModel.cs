using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Models.RequestModels.PostRequests
{
    public class OrderPostRequestModel
    {
        public int AccountId { get; set; }
        public string Note { get; set; }
    }
}
