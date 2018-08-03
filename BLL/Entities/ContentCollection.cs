using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class ContentCollection
    {
        public int ContentCollectionId { get; set; }
        public string Title { get; set; }

        public ICollection<Feed> Feeds { get; set; }
    }
}
