using DataService.Enum;
using DataService.Models.RequestModels.Filter;
using DataService.Models.RequestModels.PostRequests;
using DataService.Models.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataService.Models.Services
{
    public interface IProductService
    {
        Product Get(int id);
        IEnumerable<Product> Get(ProductGetFilter filter);
        Product Create(ProductPostRequestModel model);       
        Product getProductById(int id);

    }
    public class ProductService : BaseUnitOfWork<UnitOfWork>, IProductService
    {
        public ProductService(UnitOfWork uow) : base(uow)
        {
        }

        public Product Create(ProductPostRequestModel model)
        {
            Product product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                StoreId = model.StoreId,
                Price = model.Price,
                CategoryId = model.CategoryId,
                ConfirmAdmin = false,
                Created = DateTime.Now,
                IsDelete = 0,
            };
            _uow.Product.Create(product);
            _uow.Commit();
            return _uow.Product.GetProductById(product.Id);
        }

 

        public IEnumerable<Product> Get(ProductGetFilter filter)
        {
            var result = _uow.Product.GetAllProduct();          
            if (result == null)
            {
                return null;
            }
            else
            {
               if(filter.StoreId > 0)
                {
                    result = result.Where(p => p.StoreId == filter.StoreId);
                }
               if (filter.ProductCategory > 0)
                {
                    result = result.Where(p => p.CategoryId == filter.ProductCategory);
                }
               if(filter.SearchValue != null)
                {
                    result = result.Where(p => p.Name.Contains(filter.SearchValue));
                }
                return result;
            }
        }

        public Product Get(int id)
        {
            var result = _uow.Product.GetProductById(id);
            if (result == null)
            {
                return null;
            }
            else
            {
                return result;
            }
        }
        public bool DeleteProduct(int id)
        {
            var product = _uow.Product.GetProductById(id);
            if (product == null)
            {
                return false;
            }
            else
            {
                product.IsDelete = 1;
                _uow.Product.Update(product);
                _uow.Commit();
                return true;
            }

        }
        public Product getProductById(int id)
        {
            return _uow.Product.GetProductById(id);
        }
    }
}
