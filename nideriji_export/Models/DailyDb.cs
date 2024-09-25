using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nideriji_export.Models
{
    public class DailyDb
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed, JsonProperty("id")]
        public int DailyId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CreatedDate { get; set; }
        public long CreatedTime { get; set; }
        public int UserId { get; set; }
    }
}
