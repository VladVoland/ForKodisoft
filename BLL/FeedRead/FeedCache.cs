using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.FeedRead
{
    public class FeedCache
    {
        public Items[] articles { get; set; }
        public ImageOfChanel imageChanel { get; set; }
        public ChannelClass channel { get; set; }
        public FeedCache()
        {
            imageChanel = new ImageOfChanel();
            channel = new ChannelClass();
        }
    }
}
