using DataService.Models.Repositories.Basic;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace DataService.Models.Repositories.Create
{
    public partial interface IOrderDetailRepository : IBaseRepository<OrderDetail>
    {
        OrderDetail FindByOrderIdAndProId(int orderId, int proId);
        List<OrderDetail> FindByOrderId(int orderId);
        double? CalculateTotalMoney(int orderId);

        IEnumerable<OrderDetail> GetOrderDetailByStoreId(int? storeid);
    }
    public partial class OrderDetailRepository : BaseRepository<OrderDetail>,IOrderDetailRepository
    {
        public OrderDetailRepository (BookingFoodContext Context) : base(Context)
        {

        }

        public OrderDetail FindByOrderIdAndProId(int orderId, int proId)
        {
            OrderDetail orderDetail = Get().Where(od => od.OrderId == orderId && od.ProductId == proId).FirstOrDefault();
            if(orderDetail == null)
            {
                return null;
            }
            return orderDetail;
        }
        public List<OrderDetail> FindByOrderId(int orderId)
        {
            List<OrderDetail> orderDetail = Get().Where(od => od.OrderId == orderId && od.IsDelete == 0).ToList();
            if (orderDetail.Count == 0)
            {
                return null;
            }
            return orderDetail;
        }
        public double? CalculateTotalMoney(int orderId)
        {
            List<OrderDetail> orderDetails = FindByOrderId(orderId);
            double? total = 0;
            if (orderDetails != null)
            {
                foreach (OrderDetail orderDetail in orderDetails)
                {
                    total += orderDetail.UnitPrice * orderDetail.Quantity;
                }
            }
            return total;
        }

        public IEnumerable<OrderDetail> GetOrderDetailByStoreId(int? storeid)
        {
            return Get().Where(od => od.Product.StoreId == storeid && od.IsDelete == 0);
        }
    }
}
