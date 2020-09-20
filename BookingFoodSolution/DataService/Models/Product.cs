using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetail = new HashSet<OrderDetail>();
            ProductImage = new HashSet<ProductImage>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? StoreId { get; set; }
        public int? CategoryId { get; set; }
        public double? Price { get; set; }
        public int? IsHot { get; set; }
        public bool? Status { get; set; }
        public bool? ConfirmAdmin { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string DefaultUrlImage { get; set; }
        public int? IsDelete { get; set; }

        public virtual Category Category { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        public virtual ICollection<ProductImage> ProductImage { get; set; }
    }
}
