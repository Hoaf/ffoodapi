
namespace DataService.Models.RequestModels.PostRequests
{
    public class AccountNotificationPostRequestModel
    {
        public int AccountId { get; set; }
        public int NotificationId { get; set; }
        public int Seen { get; set; }
        public int isDelete { get; set; }
    }
}