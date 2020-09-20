using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Models.RequestModels.PostRequests
{
    public class UserFeedBackPostRequest
    {
        public int OrderId { get; set; }
        public int? StoreId { get; set; }
        public string FeedBack { get; set; }
        public DateTime? Created { get; set; }
        public int? IsDelete { get; set; }
        public int? Rating { get; set; }
    }
}
