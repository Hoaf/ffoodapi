
using DataService.Models.RequestModels.Filter;
using DataService.Models.RequestModels.PostRequests;
using DataService.Models.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataService.Models.Services
{
    public interface IStoreService
    {
        Store Get(int id);
        IEnumerable<Store> Get(StoreFilter filter);
        Store Create(StorePostRequestModel model);
        bool DeleteStore(int id);
        bool ComfirmAdmin(int id, int module);
    }
    public class StoreService : BaseUnitOfWork<UnitOfWork>, IStoreService
    {
        public StoreService(UnitOfWork uow) : base(uow)
        {
        }
        public Store Create(StorePostRequestModel model)
        {
            var checkStore = _uow.Store.Get().Where(p => p.IsDelete == 0 && p.AccountId == model.AccountId).FirstOrDefault();
            if(checkStore != null)
            {
                return null;
            }

            else
            {
                Store store = new Store
                {
                    Name = model.Name,
                    Description = model.Description,
                    IdAddress = model.IdAddress,
                    UrlImageDefault = model.UrlImageDefault,
                    TimeClose = model.TimeClose,
                    TimeOpen = model.TimeOpen,
                    Address = model.Address,
                    AccountId = model.AccountId,
                    IsDelete = 0,
                    IsHot = 0,
                    ConfirmAdmin = 0,
                    Created = DateTime.Now
                };
                _uow.Store.Create(store);
                _uow.Commit();
                return _uow.Store.GetStoreById(store.Id);
            }
        }

    


    public IEnumerable<Store> Get(StoreFilter filter)
    {
        var result = _uow.Store.GetAllStore();
        if (result == null)
        {
            return null;
        }
        else
        {
            if (filter.AccountId > 0)
            {
                result = result.Where(p => p.AccountId == filter.AccountId);
            }
            if (filter.ConfirmAdmin > 0)
            {
                result = result.Where(p => p.ConfirmAdmin == filter.ConfirmAdmin);
            }
            if (filter.IsHot != null)
            {
                if (filter.IsHot == true)
                {
                    result = result.OrderByDescending(p => p.IsHot);
                }
            }
            if (filter.Created != null)
            {
                if (filter.Created == true)
                {
                    result = result.OrderByDescending(p => p.Created);
                }
            }
            return result;
        }
    }

    public Store Get(int id)
    {
        var result = _uow.Store.GetStoreByIdConfirm(id);
        if (result == null)
        {
            return null;
        }
        else
        {
            return result;
        }
    }
    public bool DeleteStore(int id)
    {
        var store = _uow.Store.GetStoreById(id);
        if (store == null)
        {
            return false;
        }
        else
        {
            store.IsDelete = 1;
            _uow.Store.Update(store);
            _uow.Commit();
            return true;
        }
    }

    public bool ComfirmAdmin(int id, int module)
    {
        var store = _uow.Store.GetStoreById(id);
        if (store == null)
        {
            return false;
        }
        else
        {
            if (module == 1)
            {
                store.ConfirmAdmin = 1;
            }
            if (module == 2)
            {
                store.ConfirmAdmin = 2;
            }
            _uow.Store.Update(store);
            _uow.Commit();
            return true;
        }
    }


}
}



