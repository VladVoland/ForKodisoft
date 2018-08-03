using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
    public class DBFeed
    {
        [Key]
        [Required]
        public int FeedId { get; set; }
        [Required]
        public string URL { get; set; }
        [Required]
        public DBContentCollection ContentCollection { get; set; }
    }
}
