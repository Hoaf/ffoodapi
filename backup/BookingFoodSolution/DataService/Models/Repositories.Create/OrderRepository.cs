using DataService.Models.Repositories.Basic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataService.Models.Repositories.Create
{
    public partial interface IOrderRepository : IBaseRepository<Order>
    {
        Order GetUnpaid(int accountid);
        List<Order> GetAll();
        Order GetOrderById(int? id);
        bool IsValidOrderUnpaid(int accountid);
    }
    public partial class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(BookingFoodContext Context) : base(Context)
        {
        }
        
        public Order GetUnpaid(int accountid)
        {
            return Get().Where(o => o.IsDelete==0 && o.Status == 0 && o.AccountId == accountid).FirstOrDefault();
        }
        public Order GetOrderById(int? id)
        {
            return Get().Where(o => o.Id == id && o.IsDelete == 0).Include(t => t.Transaction).FirstOrDefault();
        }

        public List<Order> GetAll()
        {
            return Get().Where(o => o.IsDelete==0).ToList();
        }

        public bool IsValidOrderUnpaid(int accountid)
        {
            Order order = Get().Where(o => o.AccountId == accountid && o.Status == 0 && o.IsDelete == 0).FirstOrDefault();
            if(order == null)
            {
                return true;
            }
            return false;
        }
    }
}
