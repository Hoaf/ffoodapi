using DataService.Models.Repositories.Basic;
using System.Collections.Generic;
using System.Linq;

namespace DataService.Models.Repositories.Create
{

    public partial class AccountNotificationRepository : BaseRepository<AccountNotification>
    {

        public AccountNotificationRepository(BookingFoodContext Context) : base(Context)
        {
        }

        public IEnumerable<AccountNotification> GetAccountNotificationByAccountId(int accountId)
        {
            return Get().Where(p => p.AccountId == accountId && p.IsDelete == 0);
        }


        public AccountNotification GetAccountNotificationByAccountIdAndNotificationId(int accountId, int notificationId)
        {
            return Get().Where(p => p.AccountId == accountId && p.NotificationId == notificationId).FirstOrDefault();
        }
    }
}