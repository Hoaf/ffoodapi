using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Models.RequestModels.PutRequests
{
    public class WalletPutRequestModel
    {
        public int AcountId { get; set; }
        public double? Balance { get; set; }
    }
}
