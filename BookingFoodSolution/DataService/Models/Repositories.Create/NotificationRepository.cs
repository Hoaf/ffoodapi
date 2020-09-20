using DataService.Models.Repositories.Basic;
using System.Linq;
using System.Collections.Generic;

namespace DataService.Models.Repositories.Create
{
    public partial class NotificationRepository : BaseRepository<Notification>
    {
        public NotificationRepository(BookingFoodContext Context) : base(Context)
        { }

        public IEnumerable<Notification> GetNotification()
        {
            return Get();
        }

        public Notification GetNotificationById(int id)
        {
            return Get().Where(p => p.Id == id && p.IsDelete == 0).FirstOrDefault();
        }
    }
}