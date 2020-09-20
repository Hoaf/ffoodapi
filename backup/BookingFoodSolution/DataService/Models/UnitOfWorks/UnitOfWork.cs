using DataService.Models.Repositories.Create;
using System;
using System.Collections.Generic;
using System.Text;
using static DataService.Models.Repositories.Create.IStoreRepository;

namespace DataService.Models.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookingFoodContext _dbContext;
        //
        //delacre Repository
        public AccountRepository Account { get; private set; }
        public NotificationRepository Notification { get; private set; }
        public AccountNotificationRepository AccountNotification { get; private set; }
        public ProductRepository Product { get; private set; }
        public WalletRepository Wallet { get; private set; }
        public OrderRepository Order { get; private set; }
        public OrderDetailRepository OrderDetail { get; private set; }
        public TransactionRepository Transaction { get; private set; }
        public StoreRepository Store { get; private set; }
        public PaymentRepository Payment { get; private set; }
        public UserFeedBackRepository UserFeedBack { get; private set; }

        public UnitOfWork(BookingFoodContext dbContext)
        {
            this._dbContext = dbContext;
            this.Account = new AccountRepository(this._dbContext);
            this.Notification = new NotificationRepository(this._dbContext);
            this.AccountNotification = new AccountNotificationRepository(this._dbContext);
            this.Product = new ProductRepository(this._dbContext);
            this.Wallet = new WalletRepository(this._dbContext);
            this.OrderDetail = new OrderDetailRepository(this._dbContext);
            this.Order = new OrderRepository(this._dbContext);
            this.Product = new ProductRepository(this._dbContext);
            this.Transaction = new TransactionRepository(this._dbContext);
            this.Store = new StoreRepository(this._dbContext);
            this.Payment = new PaymentRepository(this._dbContext);
            this.UserFeedBack = new UserFeedBackRepository(this._dbContext);
        }
        public void Commit()
        {
            this._dbContext.SaveChanges();
        }

        public void Dispose()
        {
            this._dbContext.Dispose();
        }
    }
}
