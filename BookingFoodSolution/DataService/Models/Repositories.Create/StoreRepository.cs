using DataService.Models.Repositories.Basic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataService.Models.Repositories.Create
{
    public interface IStoreRepository : IBaseRepository<Store>
    {
        Store GetStoreByIdConfirm(int id);
        IEnumerable<Store> GetAllStore();
        Store GetStoreById(int? id);
    }
    public class StoreRepository : BaseRepository<Store>, IStoreRepository
    {
        public StoreRepository(BookingFoodContext Context) : base(Context)
        {
        }


        public IEnumerable<Store> GetAllStore()
        {
            return Get().Where(p => p.IsDelete == 0);
        }

      

        public Store GetStoreById(int? id)
        {
            return Get().Where(p => p.IsDelete == 0 && p.Id == id).FirstOrDefault();
        }

        public Store GetStoreByIdConfirm(int id)
        {
            return Get().Where(p => p.IsDelete == 0 && p.Id == id && p.ConfirmAdmin == 1).FirstOrDefault();
        }
    }
}



