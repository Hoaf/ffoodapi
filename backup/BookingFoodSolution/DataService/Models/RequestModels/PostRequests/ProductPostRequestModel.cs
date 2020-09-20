using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Models.RequestModels.PostRequests
{
    public class ProductPostRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? StoreId { get; set; }
        public int? CategoryId { get; set; }
        public double? Price { get; set; }
    }
}
