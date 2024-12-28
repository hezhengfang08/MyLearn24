using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Infrastructure.Elasticsearch
{
    public class ElasticsearchOptions
    {
        public Uri[] NodeUris { get; set; } 
        public string UserName {  get; set; }
        public string Password { get; set; }    
    }
}
