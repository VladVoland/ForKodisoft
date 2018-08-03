using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
    public class DBContentCollection
    {
        [Key]
        [Required]
        public int ContentCollectionId { get; set; }
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        public ICollection<DBFeed> Feeds { get; set; }
    }
}
