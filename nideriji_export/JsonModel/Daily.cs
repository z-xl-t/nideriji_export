using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nideriji_export.JsonModel
{
    public class Daily
    {

        [JsonProperty("id")]
        public int DailyId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("createddate")]
        public string CreatedDate { get; set; }
        [JsonProperty("createdtime")]
        public long CreatedTime { get; set; }

        [JsonProperty("user")]
        public int UserId { get; set; }

    }
}
