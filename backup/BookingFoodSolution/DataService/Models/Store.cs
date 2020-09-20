using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class Store
    {
        public Store()
        {
            Banner = new HashSet<Banner>();
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int? IsHot { get; set; }
        public int? ConfirmAdmin { get; set; }
        public int? AccountId { get; set; }
        public string UrlImageDefault { get; set; }
        public DateTime? Created { get; set; }
        public int? IsDelete { get; set; }

        public virtual Account Account { get; set; }
        public virtual ICollection<Banner> Banner { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
