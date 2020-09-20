using AutoMapper;
using DataService.Models.RequestModels.PostRequests;
using DataService.Models.UnitOfWorks;
using System.Collections.Generic;
using System;
using System.Linq;
using DataService.Models.RequestModels.PutRequests;

namespace DataService.Models.Services
{
    public interface INotificationService
    {
        Notification GetNotificationByAccountId(int accountId);
        Notification Create(NotificationPostRequestModel model, int currentAccountId);
    }

    public class NotificationService : BaseUnitOfWork<UnitOfWork>, INotificationService
    {
        private readonly IMapper _mapper;
        private AccountNotificationService accountNotificationService;

        public NotificationService(UnitOfWork uow, IMapper mapper) : base(uow)
        {
            _mapper = mapper;
            accountNotificationService = new AccountNotificationService(uow, mapper);
        }

        public Notification Create(NotificationPostRequestModel model, int currentAccountId)
        {
            // TODO: Authorization

            var noti = new Notification
            {
                Title = model.Title,
                Description = model.Description,
                Content = model.Content,
                Created = DateTime.Today,
                IsDelete = 0
            };

            _uow.Notification.Create(noti);
            _uow.Commit();

            // Get Notificaiton
            var notificationDTO = _uow.Notification.GetById(noti.Id);

            if (notificationDTO == null)
            {
                return null;
            }

            // Get Account
            var accounts = _uow.Account.Get().Where(p => p.Id != currentAccountId).ToList();

            if (accounts == null)
            {
                return null;
            }

            // Create Account Notification
            for (int i = 0; i < accounts.Count; i++)
            {
                accountNotificationService.Create(notificationDTO.Id, accounts.ElementAt(i).Id);
            }

            return notificationDTO;
        }

        public Notification GetNotificationByAccountId(int accountId)
        {
            // TODO: Authorization
            var account = _uow.Account.GetAccountById(accountId);
            if (account == null)
            {
                return null;
            }

            var accoutNotification = _uow.AccountNotification.GetAccountNotificationByAccountId(accountId).ToList();
            if (accoutNotification == null || accoutNotification.Count == 0)
            {
                return null;
            }

            int notificationId = accoutNotification.ElementAt(0).NotificationId;
            var notification = _uow.Notification.GetNotificationById(notificationId);

            return notification;
        }

        public int GetCountNotification(int accountId)
        {
            int count = 0;
            // TODO: Authorization
            var account = _uow.Account.GetAccountById(accountId);
            if (account == null)
            {
                return count;
            }

            var accoutNotification = _uow.AccountNotification.GetAccountNotificationByAccountId(accountId).ToList();
            if (accoutNotification == null)
            {
                return count;
            }

            count = accoutNotification.Count;

            return count;
        }

        public IEnumerable<Notification> GetAllNotfication()
        {
            //TODO: Authorization
            var result = _uow.Notification.GetNotification();
            if (result == null)
            {
                return null;
            }
            else
            {
                return result;
            }
        }

        public bool UpdateStatusHasSeen(NotificationPutRequestModel model)
        {
            //TODO: Authorization
            var account = _uow.Account.GetAccountById(model.AccountId);
            if (account == null)
            {
                return false;
            }


            var accountNotification = _uow.AccountNotification.GetAccountNotificationByAccountIdAndNotificationId(model.AccountId, model.NotificationId);

            if (accountNotification == null)
            {
                return false;
            }

            accountNotification.Seen = model.Seen;

            _uow.AccountNotification.Update(accountNotification);
            _uow.Commit();
            return true;
        }
    }
}