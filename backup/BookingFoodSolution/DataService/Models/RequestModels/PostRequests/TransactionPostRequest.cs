using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Models.RequestModels.PostRequests
{
    public class TransactionPostRequest
    {
        public int Orderid { get; set; }
        public string payment { get; set; }
    }
}
