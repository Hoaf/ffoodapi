using DataService.Models.Repositories.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataService.Models.Repositories.Create
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Product GetProductById(int id);
        IEnumerable<Product> GetAllProduct();
    }
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(BookingFoodContext Context) : base(Context)
        {
        }

        public IEnumerable<Product> GetAllProduct()
        {
            return Get().Where(p => p.IsDelete == 0).OrderByDescending(p => p.IsHot);
        }
   
 

        public Product GetProductById(int id)
        {
            return Get().Where(p => p.Id == id && p.IsDelete == 0).FirstOrDefault();

        }
    }
}
