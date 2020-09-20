using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Models.RequestModels.Filter
{
    public class StoreFilter
    {
        public int ConfirmAdmin { get; set; }
        public int AccountId { get; set; }
        public bool? IsHot { get; set; }
        public bool? Created { get; set; }


    }
}
