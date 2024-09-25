using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nideriji_export.JsonModel
{
    public class User
    {
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("diary_count")]
        public int DiaryCount { get; set; }
        [JsonProperty("word_count")]
        public int WordCount { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("userid")]
        public int UserId { get; set; }
        [JsonProperty("image_count")]
        public int ImageCount { get; set; }

        [JsonProperty("paired_user_config"), Ignore]
        public User PairedUser { get; set; }

    }
}
