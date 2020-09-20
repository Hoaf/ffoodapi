using DataService.Models.RequestModels.PostRequests;
using DataService.Models.ServiceModels;
using DataService.Models.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Models.Services
{
    public interface IOrderDetailService
    {
        OrderDetail CreateOrUpdateOrderDetail(OrderDetailRequestModel orderDetail);
        OrderDetail DeleteOrderDetail(int orderId, int ProductId);
        List<OrderDetail> GetOrderDetailsByOrderId(int orderId);
    }
    public class OrderDetailService : BaseUnitOfWork<UnitOfWork>, IOrderDetailService
    {
        public OrderDetailService(UnitOfWork uow) : base(uow)
        {

        }
        public List<OrderDetail> GetOrderDetailsByOrderId(int orderId)
        {
            return _uow.OrderDetail.FindByOrderId(orderId);
        }
 
        public IEnumerable<ProductCart> GetProducstInCart(int accountid)
        {
            if(accountid == 0)
            {
                return null;
            }
            List<ProductCart> result = null;
            Order order = _uow.Order.GetUnpaid(accountid);
            if(order != null)
            {
                IEnumerable<OrderDetail> orderDetails = GetOrderDetailsByOrderId(order.Id);
                if (orderDetails != null)
                {
                    result = new List<ProductCart>();
                    foreach (var orderDetail in orderDetails)
                    {
                        Product product = _uow.Product.GetProductById(orderDetail.ProductId);
                        result.Add(new ProductCart
                        {
                            OrderId = order.Id,
                            ProId = product.Id,
                            Name = product.Name,
                            Description = product.Description,
                            Quantity = orderDetail.Quantity,
                            Price = orderDetail.UnitPrice
                        });
                    }
                }
            }
          
            return result;
        }
        public OrderDetail DeleteOrderDetail(int orderId,int productId)
        {
            OrderDetail orderDetail = _uow.OrderDetail.FindByOrderIdAndProId(orderId, productId);
            orderDetail.IsDelete = 1;
            _uow.OrderDetail.Update(orderDetail);
            _uow.Commit();
            return orderDetail;
        }

        public OrderDetail CreateOrUpdateOrderDetail(OrderDetailRequestModel orderDetail)
        {
            OrderDetail oldOrderDetail = _uow.OrderDetail.FindByOrderIdAndProId(orderDetail.OrderId, orderDetail.ProductId);
            if (oldOrderDetail == null)
            {
                oldOrderDetail = new OrderDetail
                {
                    OrderId = orderDetail.OrderId,
                    ProductId = orderDetail.ProductId,
                    Quantity = orderDetail.Quantity,
                    UnitPrice = orderDetail.UnitPrice,
                    IsDelete = 0
                };
                _uow.OrderDetail.Create(oldOrderDetail);
                _uow.Commit();
            }
            else
            {
                if(orderDetail.updateCart == 1)
                {
                    int? oldQuantity = oldOrderDetail.Quantity;
                    oldOrderDetail.Quantity = oldQuantity + orderDetail.Quantity;
                }
                else if(orderDetail.updateCart == 0)
                {
                    int? oldQuantity = oldOrderDetail.Quantity;
                    oldOrderDetail.Quantity = oldQuantity- orderDetail.Quantity;
                }
                _uow.OrderDetail.Update(oldOrderDetail);
                _uow.Commit();
            }
            
            return oldOrderDetail;
        }
    }
}
