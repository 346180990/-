using SpiderMusic.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SpiderMusic
{
    class Program
    {
        static void Main(string[] args)
        {
            string statusCode;
            string postData="";
            ///weapi/login/cellphone?csrf_token=
            GetDataAsync(out statusCode, postData, "https://music.163.com/weapi/login/cellphone?csrf_token=");
            string s =ChooseUserAgent("pc");
            Console.WriteLine(s);
        }
        private static string GetDataAsync(out string statusCode, string postData, string url) 
        {
            string result = string.Empty;

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("user-agent", ChooseUserAgent("pc"));
                //httpClient.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
                if (url.Contains("music.163.com")) {
                    httpClient.DefaultRequestHeaders.Add("Referer", "https://music.163.com");
                }
                List<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>();
                Crypto crypto = new Crypto();
                returnValue resultmath =crypto.Weapi("");
                param.Add(new KeyValuePair<string, string>("params", resultmath.Params));
                param.Add(new KeyValuePair<string, string>("encSecKey", "840a2d4758355c861800f3949c41ef28a98c1e9bafb7bbb4e61d29afccdc8673fcf5d8c893630bf5c3a27ab2502f4cbebe1a609fd92f06f0f8c8357eb47a54e5e7aa567f438aafb90283f453693ac77c8fbe8071a159803de50a9064179167cee1fc49f0aee3817e4c26b68866fc17299c5e90b124bd4fc9ce2dda91a19eacfd"));
                var content = new FormUrlEncodedContent(param);
                //异步Post
                HttpResponseMessage response = httpClient.PostAsync(url, content).Result;
                //输出Http响应状态码
                statusCode = response.StatusCode.ToString();
                //确保Http响应成功
                if (response.IsSuccessStatusCode)
                {
                    //异步读取json
                    result = response.Content.ReadAsStringAsync().Result;
                }
            }
            return result;
        }
        public static string ChooseUserAgent(string options) 
        {
            List<string> userAgentList = new List<string>() {
            "Mozilla/5.0 (iPhone; CPU iPhone OS 9_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13B143 Safari/601.1",
            "Mozilla/5.0 (iPhone; CPU iPhone OS 9_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13B143 Safari/601.1",
            "Mozilla/5.0 (Linux; Android 5.0; SM-G900P Build/LRX21T) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Mobile Safari/537.36",
            "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Mobile Safari/537.36",
            "Mozilla/5.0 (Linux; Android 5.1.1; Nexus 6 Build/LYZ28E) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Mobile Safari/537.36",
            "Mozilla/5.0 (iPhone; CPU iPhone OS 10_3_2 like Mac OS X) AppleWebKit/603.2.4 (KHTML, like Gecko) Mobile/14F89;GameHelper",
            "Mozilla/5.0 (iPhone; CPU iPhone OS 10_0 like Mac OS X) AppleWebKit/602.1.38 (KHTML, like Gecko) Version/10.0 Mobile/14A300 Safari/602.1",
            "Mozilla/5.0 (iPad; CPU OS 10_0 like Mac OS X) AppleWebKit/602.1.38 (KHTML, like Gecko) Version/10.0 Mobile/14A300 Safari/602.1",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.12; rv:46.0) Gecko/20100101 Firefox/46.0",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_5) AppleWebKit/603.2.4 (KHTML, like Gecko) Version/10.1.1 Safari/603.2.4",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:46.0) Gecko/20100101 Firefox/46.0",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/13.10586"
            };
            var index = 0;
            if (options == "moblie")
            {
                index = new Random().Next(0,7);
            }
            else if (options == "pc")
            {
                index = new Random().Next(7, 14);
            }
            return userAgentList[index];
        }
    }
}
