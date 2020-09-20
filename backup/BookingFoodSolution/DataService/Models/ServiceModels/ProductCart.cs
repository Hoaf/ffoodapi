using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Models.ServiceModels
{
    public class ProductCart
    {
        public int OrderId { get; set; }

        public int ProId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double? Price { get; set; }

        public int? Quantity { get; set; }


    }
}
