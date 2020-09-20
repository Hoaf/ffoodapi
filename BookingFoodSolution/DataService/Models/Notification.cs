using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class Notification
    {
        public Notification()
        {
            AccountNotification = new HashSet<AccountNotification>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime? Created { get; set; }
        public string UrlImage { get; set; }
        public int? IsDelete { get; set; }

        public virtual ICollection<AccountNotification> AccountNotification { get; set; }
    }
}
