using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.Entities;
using BLL.Operations;
using Ninject;
using System.Text.RegularExpressions;
using ForKodisoft.Models;

namespace ForKodisoft.Controllers
{
    public class ContentCollectionsController : ApiController
    {
        IKernel ninjectKernel;
        IContentOperations COperations;
        public ContentCollectionsController()
        {
            ninjectKernel = new StandardKernel(new NinjectConfig());
            COperations = ninjectKernel.Get<IContentOperations>();
        }

        [HttpGet]
        [Route("api/contentcollections")]
        public IEnumerable<ContentCollection> GetCollections()
        {
            IEnumerable<ContentCollection> collections = COperations.GetCollections();
            return collections;
        }

        [HttpPost]
        [Route("api/contentcollections/{title}")]
        public IHttpActionResult PostCollection(string title)
        {
            string patt = @"^[\d|\D]{1,30}$";

            if (string.IsNullOrWhiteSpace(title))
            {
                return BadRequest("Please, write title of your content collection");
            }
            if (!Regex.IsMatch(title, patt))
            {
                return BadRequest("Title is too long");
            }
            if (!COperations.CheckContentCollection(title))
            {
                return BadRequest("Not a valid title for collection");
            }
            else
            {
                int id = COperations.CreateContentCollection(title);
                if (id > 0)
                    return Ok(id);
                else return BadRequest("Please, check correctness of the title");
            }
        }
        [HttpDelete]
        [Route("api/contentcollections/delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid collection id");
            bool deleted = COperations.RemoveContentCollection(id);
            if (deleted)
                return Ok(id);
            else
                return BadRequest("This content collection does not exist");
        }
    }
}
