using DataService.Models.RequestModels;
using DataService.Models.RequestModels.PostRequests;
using DataService.Models.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Models.Services
{

    public partial interface IUserFeedBackService
    {
        IEnumerable<UserFeedBack> GetAll();
        List<UserFeedBack> GetByOrderOrStore(UserFeedBackFilterModel userFeedBackFilter);
        UserFeedBack Create(UserFeedBackPostRequest userFeedBack);
        UserFeedBack Delete(int orderid);

    }
    public class UserFeedBackService : BaseUnitOfWork<UnitOfWork>, IUserFeedBackService
    {
        public UserFeedBackService(UnitOfWork uow) : base(uow) { }

        public UserFeedBack Create(UserFeedBackPostRequest userFeedBack)
        {
            UserFeedBack newFeedBack = new UserFeedBack
            {
                Created = DateTime.Now,
                OrderId = userFeedBack.OrderId,
                IsDelete = 0,
                Rating = userFeedBack.Rating,
                StoreId = userFeedBack.StoreId,
                FeedBack = userFeedBack.FeedBack
            };

            UserFeedBack result =_uow.UserFeedBack.Create(newFeedBack).Entity;
            _uow.Commit();

            return result;
        }

        public UserFeedBack Delete(int orderid)
        {
            UserFeedBack userFeedBack = _uow.UserFeedBack.GetByOrderId(orderid);
            if (userFeedBack != null)
            {
                userFeedBack.IsDelete = 1;
                _uow.UserFeedBack.Update(userFeedBack);
            }

            return userFeedBack;
        }

        public IEnumerable<UserFeedBack> GetAll()
        {
            return _uow.UserFeedBack.GetAll();
        }

        public List<UserFeedBack> GetByOrderOrStore(UserFeedBackFilterModel userFeedBackFilter)
        {
            List<UserFeedBack> result = new List<UserFeedBack>();
            if (userFeedBackFilter.OrderId > 0 && userFeedBackFilter.StoreId > 0)
            {
                result.Add(_uow.UserFeedBack.GetByOrderId(userFeedBackFilter.OrderId));

            }
            else if (userFeedBackFilter.StoreId > 0)
            {
                result = _uow.UserFeedBack.GetByStoreId(userFeedBackFilter.StoreId);
            }
            else if (userFeedBackFilter.OrderId > 0)
            {
                result.Add(_uow.UserFeedBack.GetByOrderId(userFeedBackFilter.OrderId));
            }
            return result;
        }
    }
}
