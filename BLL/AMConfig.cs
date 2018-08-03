using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DAL;
using BLL.Entities;

namespace BLL
{
    public static class BLLAutoMapper
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DBUser, User>();
                cfg.CreateMap<DBFeed, Feed>()
                    .ForMember("ContentCollectionId", x => x.MapFrom(c => c.ContentCollection.ContentCollectionId));
                cfg.CreateMap<DBContentCollection, ContentCollection> ();
            });
        }
    }
}
