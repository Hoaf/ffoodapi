using DataService.Models.Repositories.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataService.Models.Repositories.Create
{
    public partial interface IUserFeedBackRepository : IBaseRepository<UserFeedBack>
    {
        IEnumerable<UserFeedBack> GetAll();

        List<UserFeedBack> GetByStoreId(int? storeid);
        UserFeedBack GetByOrderId(int orderId);
    }
    public partial class UserFeedBackRepository : BaseRepository<UserFeedBack>, IUserFeedBackRepository
    {
        public UserFeedBackRepository(BookingFoodContext Context) : base(Context)
        {

        }

        public IEnumerable<UserFeedBack> GetAll()
        {
            return Get().Where(uf => uf.IsDelete == 0);
        }

        public UserFeedBack GetByOrderId(int orderId)
        {
            return Get().Where(uf => uf.IsDelete == 0 && uf.OrderId == orderId).FirstOrDefault();
        }

        public List<UserFeedBack> GetByStoreId(int? storeid)
        {
            return Get().Where(uf => uf.IsDelete == 0 && uf.StoreId == storeid).ToList();
        }
    }
}
