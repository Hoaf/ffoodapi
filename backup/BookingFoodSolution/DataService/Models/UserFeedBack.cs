using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class UserFeedBack
    {
        public int OrderId { get; set; }
        public int? StoreId { get; set; }
        public string FeedBack { get; set; }
        public DateTime? Created { get; set; }
        public int? IsDelete { get; set; }
        public int? Rating { get; set; }

        public virtual Order Order { get; set; }
    }
}
