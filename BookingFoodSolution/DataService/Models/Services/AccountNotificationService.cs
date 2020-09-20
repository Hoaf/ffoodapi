using System;
using System.Linq;
using AutoMapper;
using DataService.Models.RequestModels.PostRequests;
using DataService.Models.UnitOfWorks;
using System.Collections.Generic;

namespace DataService.Models.Services
{
    public class AccountNotificationService : BaseUnitOfWork<UnitOfWork>
    {
        private readonly IMapper _mapper;

        public AccountNotificationService(UnitOfWork uow, IMapper mapper) : base(uow)
        {
            _mapper = mapper;
        }

        public IEnumerable<AccountNotification> GetAccountNotification()
        {
            var result = _uow.AccountNotification.Get();
            if (result == null)
            {
                return null;
            }
            return result;
        }


        public IEnumerable<AccountNotification> GetAccountNotificationById(int accountId)
        {
            // TODO: Authorization
            var result = _uow.AccountNotification.GetAccountNotificationByAccountId(accountId);

            if (result == null)
            {
                return null;
            }

            return result;
        }

        public AccountNotification Create(int accountId, int notificationId)
        {
            // TODO: Authorization
            AccountNotification accountNotification = new AccountNotification();
            accountNotification.AccountId = accountId;
            accountNotification.NotificationId = notificationId;
            accountNotification.Seen = 0;
            accountNotification.Created = DateTime.Today;
            accountNotification.IsDelete = 0;

            _uow.AccountNotification.Create(accountNotification);
            _uow.Commit();

            var accountNoti = GetAccountNotificationById(accountId, notificationId);

            return accountNoti;
        }

        public AccountNotification GetAccountNotificationById(int accountId, int notificationId)
        {
            var accountNotification = _uow.AccountNotification.Get().Where(p => p.AccountId == accountId && p.NotificationId == notificationId && p.IsDelete == 0).FirstOrDefault();
            if (accountNotification == null)
            {
                return null;
            }
            return accountNotification;
        }

        public bool UpdateStatusHasSeen(int notificationId, int accountId, int currentStatus)
        {
            var accountNotification = GetAccountNotificationById(accountId, notificationId);
            if (accountNotification == null)
            {
                return false;
            }

            accountNotification.Seen = currentStatus;
            _uow.AccountNotification.Update(accountNotification);
            _uow.Commit();
            return true;
        }

        public bool Delete(int accountId, int notificationId, int isDelete)
        {
            var accountNotification = GetAccountNotificationById(accountId, notificationId);
            if (accountNotification == null)
            {
                return false;
            }

            if (isDelete == 1)
            {
                accountNotification.IsDelete = 1;
            }
            else
            {
                accountNotification.IsDelete = 0;
            }

            _uow.AccountNotification.Update(accountNotification);
            _uow.Commit();
            return true;
        }
    }
}