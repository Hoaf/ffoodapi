using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Models.RequestModels.PutRequest
{
    public class OrderPutRequestModel
    {
        public int Orderid { get; set; }
        public int Status { get; set; }
        public String payment { get; set;}
    }
}
