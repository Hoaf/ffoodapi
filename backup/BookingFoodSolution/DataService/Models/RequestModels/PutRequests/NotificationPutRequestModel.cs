namespace DataService.Models.RequestModels.PutRequests
{
    public class NotificationPutRequestModel
    {
        public int AccountId { get; set; }
        public int NotificationId { get; set; }
        public int Seen { get; set; }
    }
}