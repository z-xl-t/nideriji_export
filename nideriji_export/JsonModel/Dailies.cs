using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nideriji_export.JsonModel
{
    public class Dailies
    {
        [JsonProperty("diaries")]
        public List<Daily> Dailys { get; set; }
    }
}
