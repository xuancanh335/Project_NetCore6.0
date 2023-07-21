using System;
using System.Collections.Generic;

namespace Common.Params.Base
{
    public class BaseParam
    {
        public Guid tenant_id { get; set; }
        public bool flag { get; set; } = true;
    }
    public class TopicParam
    {
        public Object data { get; set; }
        public string topic { get; set; }
        public string token { get; set; }
        public string tenant_id { get; set; }
        public string email { get; set; }
    }
    public class TopicSubscribe
    {
        public List<string> list_topic { get; set; }
    }
}
