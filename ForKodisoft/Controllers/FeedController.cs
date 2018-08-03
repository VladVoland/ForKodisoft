using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.Entities;
using BLL.FeedRead;
using BLL.Operations;
using Ninject;
using System.Text.RegularExpressions;
using ForKodisoft.Models;

namespace ForKodisoft.Controllers
{
    public class FeedController : ApiController
    {
        IKernel ninjectKernel;
        IContentOperations COperations;
        public FeedController()
        {
            ninjectKernel = new StandardKernel(new NinjectConfig());
            COperations = ninjectKernel.Get<IContentOperations>();
        }

        [HttpGet]
        [Route("api/feeds/{collectionId}")]
        public IEnumerable<Items> GetItems(int collectionId)
        {
            IEnumerable<FeedCache> feedsCache = COperations.GetContent(collectionId);
            var items = new List<Items>();
            foreach (FeedCache cache in feedsCache)
            {
                items.AddRange(cache.articles);
            }
            return items;
        }

        [HttpPost]
        [Route("api/feeds/{collectionId}")]
        public IHttpActionResult PostFeed(int collectionId, Feed feed)
        {
            string patt = @"^[\d|\D]{1,4000}$";
            if (!Regex.IsMatch(feed.URL, patt))
            {
                return BadRequest("URL is too long");
            }
            if (collectionId < 0)
            {
                return BadRequest("Not a valid collection id");
            }
            if (string.IsNullOrWhiteSpace(feed.URL))
            {
                return BadRequest("Please, write URL on feed");
            }
            if (!COperations.CheckFeed(collectionId, feed.URL))
            {
                return BadRequest("Not a valid URL on feed");
            }
            bool added = COperations.AddFeedToCollection(collectionId, feed);
            if (added)
                return Ok();
            else return BadRequest("Please, check correctness of the title");
        }
        [HttpDelete]
        [Route("api/feeds/delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid collection id");
            bool deleted = COperations.RemoveFeed(id);
            if (deleted)
                return Ok(id);
            else
                return BadRequest("This content collection does not exist");
        }
    }
}
