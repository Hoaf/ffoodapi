using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Models.RequestModels.PostRequests
{
    public class OrderDetailRequestModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public double? UnitPrice { get; set; }

        public int updateCart { get; set; }
    }
}
