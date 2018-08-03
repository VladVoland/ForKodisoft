using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.FeedRead
{
    public class ChannelClass
    {
        public string title;
        public string description;
        public string link;
        public string copyright;

        public ChannelClass()
        {
            title = "";
            description = "";
            link = "";
            copyright = "";
        }
    }
}