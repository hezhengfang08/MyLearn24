using MySelf.PMS.Client.Entities;
using MySelf.PMS.Client.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Client.DAL
{
    public class WebAccess : IWebAccess
    {
        public string HostName { get; set; } = "http://localhost:5273";
        GlobalValues _globalValues;
        public WebAccess(GlobalValues globalValues)
        {
            _globalValues = globalValues; 
        }

        public string Get(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(HostName);

                // 请求的Token数据带上
                if (!string.IsNullOrEmpty(_globalValues.Token))
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalValues.Token);

                var response = client
                    .GetAsync(url)
                    .GetAwaiter().GetResult();

                string result = response.Content
                    .ReadAsStringAsync()
                    .GetAwaiter().GetResult();
                return result;
            }
        }

        public string Post(string url, HttpContent content)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(HostName);

                if (!string.IsNullOrEmpty(_globalValues.Token))
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalValues.Token);

                var response = client
                    .PostAsync(url, content)
                    .GetAwaiter().GetResult();

                string result = response.Content
                    .ReadAsStringAsync()
                    .GetAwaiter().GetResult();
                return result;
            }
        }
        public MultipartFormDataContent GetFormData(Dictionary<string, HttpContent> contents)
        {
            var postContent = new MultipartFormDataContent();
            string boundary = string.Format("-------{0}", DateTime.Now.Ticks.ToString("x"));
            postContent.Headers.Add("ContentType", $"multipart/form-data, boundary={boundary}");
            //数据格式 // "{\"username\":\"admin\",\"password\":\"123456\"}"
            foreach (var item in contents)
            {
                // 需要进行传递的键值对：比如 ：username=admin
                postContent.Add(item.Value, item.Key);
            }
            return postContent;
        }
    }
}
