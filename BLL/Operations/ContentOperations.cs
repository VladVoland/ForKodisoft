using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using DAL;
using BLL.Entities;
using AutoMapper;
using BLL.FeedRead;
using System.Runtime.Caching;
using System.Xml;

namespace BLL.Operations
{
    public class ContentOperations : IContentOperations
    {
        IKernel ninjectKernel;
        public IUnitOfWork uow { get; set; }
        public IFeedReader fReader { get; set; }
        public ContentOperations(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public ContentOperations()
        {
            ninjectKernel = new StandardKernel(new BLLNinjectConfig());
            this.uow = ninjectKernel.Get<IUnitOfWork>();
            this.fReader = ninjectKernel.Get<IFeedReader>();
        }

        public IEnumerable<ContentCollection> GetCollections()
        {
            IEnumerable<DBContentCollection> dbCollections = uow.ContentCollections.Get();
            var collections = Mapper.Map<IEnumerable<DBContentCollection>, IEnumerable<ContentCollection>>(dbCollections);
            return collections;
        }

        public ICollection<FeedCache> GetContent(int collectionId)
        {
            ICollection<FeedCache> contentCache = new List<FeedCache>();
            DBContentCollection contentCollection = uow.ContentCollections.GetWithInclude(c => c.ContentCollectionId == collectionId, c => c.Feeds).FirstOrDefault();

            if (contentCollection != null)
            {
                foreach (DBFeed feed in contentCollection.Feeds)
                {
                    ObjectCache cache = MemoryCache.Default;
                    var key = feed.FeedId.ToString();
                    FeedCache fc = cache[key] as FeedCache;

                    if (fc == null)
                    {
                        bool readed = fReader.getFeedsContent(feed.URL, out fc);
                        if (readed)
                        {
                            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                            cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddMinutes(10.00);
                            cache.Set(key, fc, cacheItemPolicy);
                            contentCache.Add(fc);
                        }
                    }
                    else
                    {
                        contentCache.Add(fc);
                    }
                }
            }
            
            return contentCache;
        }

        public int CreateContentCollection(string newTitle)
        {
            try
            {
                DBContentCollection dbcc = new DBContentCollection { Title = newTitle };
                uow.ContentCollections.Create(dbcc);
                uow.Save();
                DBContentCollection dbCollection = uow.ContentCollections.Get(c => c.Title == newTitle).First();
                return dbCollection.ContentCollectionId;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool AddFeedToCollection(int collectionId, Feed feed)
        {
            try
            {
                DBContentCollection contentCollection = uow.ContentCollections.FindById(collectionId);
                DBFeed newFeed = new DBFeed { URL = feed.URL, ContentCollection = contentCollection};
                uow.Feeds.Create(newFeed);
                uow.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool CheckFeed(int collectionId, string url)
        {
            IEnumerable<DBFeed> feeds = uow.Feeds.GetWithInclude(f => f.URL == url, f => f.ContentCollection);
            foreach (DBFeed feed in feeds)
            {
                if (feed != null && feed.ContentCollection.ContentCollectionId == collectionId)
                {
                    return false;
                }
            }

            try {
                XmlDocument doc = new XmlDocument();
                doc.Load(url);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool CheckContentCollection(string title)
        {
            DBContentCollection c = uow.ContentCollections.Get(coll => coll.Title == title).FirstOrDefault();
            if(c != null)
            {
                return false;
            }
            return true;
        }

        public bool RemoveContentCollection(int id)
        {
            try
            {
                DBContentCollection contentCollection = uow.ContentCollections.FindById(id);
                uow.ContentCollections.Remove(contentCollection);
                uow.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveFeed(int id)
        {
            try
            {
                DBFeed feed = uow.Feeds.FindById(id);
                uow.Feeds.Remove(feed);
                uow.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
