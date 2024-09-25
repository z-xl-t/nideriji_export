using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nideriji_export.Models
{
    public class UserDb
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int UserId { get; set; }
        public string Description { get; set; }
        public int DiaryCount { get; set; }
        public int WordCount { get; set; }
        public string Name { get; set; }
    }
}
