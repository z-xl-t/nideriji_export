using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nideriji_export.Models
{
    public class UserIdWidthDailyId
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DailyId { get; set; }
        public int Saved{ get; set; }
        public int Deleted { get; set; }

    }
}
