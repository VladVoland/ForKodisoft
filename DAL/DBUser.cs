using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
    public class DBUser
    {
        [Key]
        [Required]
        public int UserId { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [MaxLength(30)]
        public string Surname { get; set; }
        [Required]
        [MaxLength(30)]
        public string Patronymic { get; set; }
        [Required]
        [MaxLength(30)]
        public string Login { get; set; }
        [Required]
        [MaxLength(30)]
        public string Password { get; set; }
    }
}
