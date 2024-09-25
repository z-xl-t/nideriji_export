using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using nideriji_export.JsonModel;
using nideriji_export.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace nideriji_export.helpers
{
    public class HttpHelper
    {
        public string Host { get; set; } = "nideriji.cn";
        public string BaseUrl { get; set; } = "https://nideriji.cn/";
        public string Token { get; set; } = string.Empty;
        public HttpHelper()
        {
        }
        public HttpHelper(string token)
        {
            Token = token;
        }
        
        public async Task<bool> DeleteDailyByDaily(int dailyId)
        {
            if (string.IsNullOrEmpty(Token))
            {
                Console.WriteLine("DeleteDaily Token 为空，请登录");
                return false;
            }
            // 设置HTTP客户端和请求头
            var request = BaseUrl.AppendPathSegment($"/api/diary/delete/{dailyId}/")
                .WithHeader("User-Agent", "OhApp/3.0 Platform/Android")
                .WithHeader("Host", Host)
                .WithHeader("Connection", "Keep-Alive")
                .WithHeader("auth", $"token {Token}")
                .WithHeader("Accept-Encoding", "gzip");

            try
            {
                await request.GetAsync();
                return await Task.Run(() => true);
            }
            catch (Exception)
            {
                return await Task.Run(() => false);
            }

        }

        public async Task<Daily> GetDailyByUserIdWidthDaily(int userId, int dailyId)
        {
            if (string.IsNullOrEmpty(Token))
            {
                Console.WriteLine("GetDaily Token 为空，请登录");
                return null;
            }

            // 创建MultipartFormDataContent
            var content = new MultipartFormDataContent();

            // 添加表单数据
            content.Add(new StringContent(dailyId.ToString()), "diary_ids");

            // 设置HTTP客户端和请求头
            var request = BaseUrl.AppendPathSegment($"/api/diary/all_by_ids/{userId}/")
                .WithHeader("User-Agent", "OhApp/3.0 Platform/Android")
                .WithHeader("Host", Host)
                .WithHeader("Connection", "Keep-Alive")
                .WithHeader("auth", $"token {Token}")
                .WithHeader("Accept-Encoding", "gzip");


            try
            {
                // 发送POST请求
                var response = await request.PostAsync(content);

                // 读取响应内容
                string responseContent = await response.GetStringAsync();
                Daily daily = JsonConvert.DeserializeObject<Dailies>(responseContent).Dailys[0];

                return daily;
            }
            catch (Exception)
            {

                return null;
            }


        }

        public async Task<Sync> Sync()
        {
            if (string.IsNullOrEmpty(Token))
            {
                Console.WriteLine("Sync Token 为空,请登录");
                return null;
            }

            // 创建MultipartFormDataContent
            var content = new MultipartFormDataContent();

            // 添加表单数据
            content.Add(new StringContent("0"), "user_config_ts");
            content.Add(new StringContent("0"), "diaries_ts");
            content.Add(new StringContent("0"), "readmark_ts");
            content.Add(new StringContent("0"), "images_ts");

            // 设置HTTP客户端和请求头
            var request = BaseUrl.AppendPathSegment("api/v2/sync/")
                .WithHeader("User-Agent", "OhApp/3.0 Platform/Android")
                .WithHeader("Host", Host)
                .WithHeader("Connection", "Keep-Alive")
                .WithHeader("auth", $"token {Token}")
                .WithHeader("Accept-Encoding", "gzip");

            try
            {

                // 发送POST请求
                var response = await request.PostAsync(content);

                // 读取响应内容
                string responseContent = await response.GetStringAsync();
                Sync syncData = JsonConvert.DeserializeObject<Sync>(responseContent);

                return syncData;
            }
            catch (Exception)
            {

                return null;
            }

        }

        public async Task<LoginRes> Login(LoginReq req)
        {
            // 创建MultipartFormDataContent
            var content = new MultipartFormDataContent();

            // 添加表单数据
            content.Add(new StringContent(req.Email), "email");
            content.Add(new StringContent(req.Password), "password");

            // 设置HTTP客户端和请求头
            var request = BaseUrl.AppendPathSegment("api/login/")
                .WithHeader("User-Agent", "OhApp/3.0 Platform/Android")
                .WithHeader("Host", Host)
                .WithHeader("Connection", "Keep-Alive")
                .WithHeader("Accept-Encoding", "gzip");

            try
            {
                // 发送POST请求
                var response = await request.PostAsync(content);

                // 读取响应内容
                string responseContent = await response.GetStringAsync();

                LoginRes res = JsonConvert.DeserializeObject<LoginRes>(responseContent);

                Token = res.Token;
                return res;

            }
            catch (Exception)
            {

                return null;
            }
            
        }
    }
}





