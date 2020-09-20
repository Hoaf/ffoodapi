using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Models.ServiceModels
{
    class WalletServiceModel
    {
        public int Id { get; set; }
        public double? Balance { get; set; }
        public int AccountId { get; set; }
        public int? IsDelete { get; set; }
    }
}
