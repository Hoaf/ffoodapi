using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Models.RequestModels
{
    public class UserFeedBackFilterModel
    {
        public int OrderId { get; set; }
        public int? StoreId { get; set; }
    }
}
