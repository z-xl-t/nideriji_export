using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nideriji_export.JsonModel
{
    public class LoginRes
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("userid")]
        public int UserId { get; set; }
        [JsonProperty("user_config")]
        public User User { get; set; }
    }
}
