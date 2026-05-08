using Myself.PMS.Client.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.DAL
{
    public class WebAccess : IWebAccess
    {
        public string HostName { get; set; } =
            "http://localhost:5273";

        public string Get(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(HostName);

                var response = client
                    .GetAsync(url)
                    .GetAwaiter().GetResult();

                string result = response.Content
                    .ReadAsStringAsync()
                    .GetAwaiter().GetResult();

                return result;
            }
        }
    }
}
