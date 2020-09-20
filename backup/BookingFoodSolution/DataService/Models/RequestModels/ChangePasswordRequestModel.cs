using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Models.RequestModels
{
   public class ChangePasswordRequestModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("old_password")]
        public string OldPassword { get; set; }
        [JsonProperty("new_password")]
        public string NewPassword { get; set; }
    }
}
