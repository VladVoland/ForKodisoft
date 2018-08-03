using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private KodiDB db = new KodiDB();
        private GenericRepository<DBFeed> FeedsRepository;
        private GenericRepository<DBContentCollection> ContentCollectionsRepository;
        private GenericRepository<DBUser> UsersRepository;

        public GenericRepository<DBFeed> Feeds
        {
            get
            {
                if (FeedsRepository == null)
                    FeedsRepository = new GenericRepository<DBFeed>(db);
                return FeedsRepository;
            }
        }

        public GenericRepository<DBContentCollection> ContentCollections
        {
            get
            {
                if (ContentCollectionsRepository == null)
                    ContentCollectionsRepository = new GenericRepository<DBContentCollection>(db);
                return ContentCollectionsRepository;
            }
        }

        public GenericRepository<DBUser> Users
        {
            get
            {
                if (UsersRepository == null)
                    UsersRepository = new GenericRepository<DBUser>(db);
                return UsersRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
