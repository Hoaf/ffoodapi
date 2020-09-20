using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class Banner
    {
        public int Id { get; set; }
        public int? StoreId { get; set; }
        public int? IsHot { get; set; }
        public DateTime? Created { get; set; }
        public string CollectionDetail { get; set; }
        public string Description { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? ConfirmAdmin { get; set; }
        public string UrlImage { get; set; }
        public int? IsDelete { get; set; }

        public virtual Store Store { get; set; }
    }
}
