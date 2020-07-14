using System;
using System.Collections.Generic;
using System.Text;

namespace SpiderMusic.model
{
    class commit
    {
        public string Csrf_token { get; set; }
        public string Cursor { get; set; }
        public string Offset { get; set; }
        public string OrderType { get; set; }
        public string PageNo { get; set; }
        public string PageSize { get; set; }
        public string Rid { get; set; }
        public string ThreadId { get; set; }
    }
}
