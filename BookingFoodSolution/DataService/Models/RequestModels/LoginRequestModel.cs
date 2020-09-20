using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Models.RequestModels
{
    public class LoginRequestModel
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
