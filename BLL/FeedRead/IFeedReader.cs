using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.FeedRead
{
    public interface IFeedReader
    {
        bool getFeedsContent(string source, out FeedCache feed);
    }
}
