using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using nideriji_export.helpers;
using nideriji_export.JsonModel;
using nideriji_export.Models;
using static System.Net.WebRequestMethods;

namespace nideriji_export
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("请输入邮箱：");
            var email = Console.ReadLine();

            Console.Write("请输入密码：");
            var password = PasswordInputHiddenHapper.PasswordInput();
            Console.WriteLine("");

            // 数据库连接
            var sqlDbContext = new SqlDbContext();

            // 配置 mapper 映射
            var mapper = new AutoMapperHelper();

            var http = new HttpHelper();


            var loginReq = new LoginReq()
            {
                Email = email,
                Password = password,
            };
            LoginRes loginRes = await http.Login(loginReq);
            if (loginRes != null && !string.IsNullOrEmpty(loginRes.Token))
            {

                // 保存数据
                var myUser = loginRes.User;
                var pairedUser = loginRes.User.PairedUser;
                var userDb = mapper.Mapper.Map<UserDb>(myUser);
                sqlDbContext.Insert(userDb);
                Console.WriteLine($"登录成功，已保存用户信息，Token:{loginRes.Token}");
            }
            else
            {
                Console.WriteLine("登录失败，无法获取 Token");
                return;
            }


            Console.Write("是否同步日记ID：输入 1 开始，输入其他结束 ");
            string flag = Console.ReadLine();
            if (flag != null && flag == "1")
            {
                Console.WriteLine("开始同步");
                await SyncData(http, sqlDbContext);
                Console.WriteLine("同步ID结束");
            }
            else
            {
                Console.WriteLine("退出程序");
                return;
            }
            Console.Write("是否开始同步日记：输入 1 开始，输入其他结束 ");
            flag = Console.ReadLine();
            if (flag != null && flag == "1")
            {
                Console.WriteLine("开始同步日记");
                await GetAllDailyData(http, sqlDbContext, mapper);
                Console.WriteLine("同步日记结束");
            }
            else
            {
                Console.WriteLine("退出程序");
                return;
            }
            // 注销掉
            //Console.Write("是否开始删除日记（会删除软件中的日记，慎选）：输入 1 开始，输入其他结束 ");
            //flag = Console.ReadLine();
            //if (flag != null && flag == "1")
            //{
            //    Console.WriteLine("开始删除日记");
            //    await DeleteAddDailyData(http, sqlDbContext);
            //    Console.WriteLine("删除日记完成");
            //}
            //else
            //{
            //    Console.WriteLine("退出程序");
            //    return;
            //}
            Console.WriteLine("输入任意键，退出程序");
            flag = Console.ReadLine();

        }

        static async Task DeleteAddDailyData(HttpHelper http, SqlDbContext sqlDbContext)
        {
            var userIdWidthDailyAll = sqlDbContext.GetUserIdWidthDailyIdAll();

            foreach (var ud in userIdWidthDailyAll)
            {

                if (ud.Deleted == 0)
                {

                    var result =  await http.DeleteDailyByDaily(ud.DailyId);
                    if (result)
                    {
                        ud.Deleted = 1;
                        sqlDbContext.Update(ud);
                        await Task.Delay(1000);
                        Console.WriteLine($"已删除 {ud.Id} {ud.DailyId}");

                    } else
                    {

                        Console.WriteLine($"删除请求失败 {ud.Id} {ud.DailyId}");
                    }
                }
                else {
                    Console.WriteLine($"已删除过，跳过删除 {ud.Id} {ud.DailyId}");
                }
            }
        }

        static async Task GetAllDailyData(HttpHelper http, SqlDbContext sqlDbContext, AutoMapperHelper mapper)
        {
            var userIdWidthDailyAll = sqlDbContext.GetUserIdWidthDailyIdAll();

            foreach (var ud in userIdWidthDailyAll)
            {

                if (ud.Saved == 1)
                {
                    Console.WriteLine($"已保存过，不再保存 {ud.DailyId}");
                    continue;
                }
                var daily = await http.GetDailyByUserIdWidthDaily(ud.UserId, ud.DailyId);
                if (daily != null)
                {
                    var dailyDb = mapper.Mapper.Map<DailyDb>(daily);

                    sqlDbContext.Insert(dailyDb);

                    ud.Saved = 1;
                    sqlDbContext.Update(ud);
                    Console.WriteLine($"当前日记获取成功, {dailyDb.Id}, {dailyDb.DailyId}");

                    await Task.Delay(2000);
                }
                else
                {
                    Console.WriteLine($"获取的日记为空, 0， {ud.UserId}, {ud.DailyId}");
                }
            }

        }

        static async Task SyncData(HttpHelper http, SqlDbContext sqlDbContext)
        {
            var syscData = await http.Sync();
            if (syscData != null)
            {
                foreach (var item in syscData.Dailys)
                {
                    var udId = new UserIdWidthDailyId
                    {
                        UserId = item.UserId,
                        DailyId = item.DailyId,
                        Saved = 0
                    };
                    var x = sqlDbContext.QueryByUserIdWithDailyId(udId.UserId, udId.DailyId);
                    if (x != null)
                    {
                        Console.WriteLine("之前同步过数据，跳过");
                        continue;
                    }
                    else
                    {
                        sqlDbContext.Insert(udId);

                        Console.WriteLine($"Id 同步成功 ：{udId.Id}, {udId.DailyId}");
                    }
                }
            }
        }
    }
}
