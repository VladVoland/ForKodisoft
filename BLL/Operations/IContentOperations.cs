using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BLL.Entities;
using BLL.FeedRead;

namespace BLL.Operations
{
    public interface IContentOperations
    {
        IUnitOfWork uow { get; set; }
        IFeedReader fReader { get; set; }
        IEnumerable<ContentCollection> GetCollections();
        ICollection<FeedCache> GetContent(int collectionId);
        int CreateContentCollection(string title);
        bool AddFeedToCollection(int collectionId, Feed feed);
        bool RemoveContentCollection(int id);
        bool RemoveFeed(int id);
        bool CheckFeed(int collectionId, string url);
        bool CheckContentCollection(string title);
    }
}