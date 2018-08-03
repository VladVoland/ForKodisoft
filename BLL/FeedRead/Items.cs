using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.FeedRead
{
    public class Items
    {
        public string title;
        public string link;
        public string description;
        public string pubDate;

        public Items()
        {
            title = "";
            link = "";
            description = "";
            pubDate = "";
        }
    }
}