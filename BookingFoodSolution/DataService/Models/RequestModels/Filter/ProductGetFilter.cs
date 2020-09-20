using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Models.RequestModels.Filter
{
    public class ProductGetFilter
    {
        public int StoreId { get; set; }
        public int ProductCategory { get; set; }
        public string SearchValue { get; set; }
    }
}
