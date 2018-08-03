using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUnitOfWork
    {
        GenericRepository<DBFeed> Feeds { get; }
        GenericRepository<DBContentCollection> ContentCollections { get; }
        GenericRepository<DBUser> Users { get; }

        void Save();
        void Dispose();
    }
}
