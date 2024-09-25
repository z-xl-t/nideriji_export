using nideriji_export.JsonModel;
using nideriji_export.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nideriji_export.helpers
{
    public class SqlDbContext
    {
        private SQLiteConnection _db;
        public SqlDbContext()
        {
            // Get an absolute path to the database file
            var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "nideriji.sqlite3");
            _db = new SQLiteConnection(dbPath);

            // 创建表
            Create();

        }
        public void Create()
        {
            _db.CreateTable<UserDb>();

            _db.CreateTable<DailyDb>();
            _db.CreateTable<UserIdWidthDailyId>();
        }

        public void Update<T>(T entity) where T : class
        {
            _db.Update(entity);
        }
        public void Insert<T>(T entity) where T : class
        {
            _db.Insert(entity);
        }

        public  UserIdWidthDailyId QueryByUserIdWithDailyId(int userId, int dailyId)
        {
            // 执行查询，并返回结果列表
            var results =  _db.Table<UserIdWidthDailyId>()
                                  .Where(u => u.UserId == userId && u.DailyId == dailyId)
                                  .FirstOrDefault();
            // 如果查询结果不为空，返回第一个结果，否则返回null
            return results;
        }

        public UserIdWidthDailyId QueryById(int id)
        {
            // 执行查询，并返回结果列表
            var results = _db.Table<UserIdWidthDailyId>().Where(u => u.Id == id).FirstOrDefault();
            
            return results;
        }
        public ICollection<UserIdWidthDailyId>  GetUserIdWidthDailyIdAll()
        {
            // 获取所有记录
            var results = _db.Table<UserIdWidthDailyId>().ToList();
            return results;
        }

    }
}
