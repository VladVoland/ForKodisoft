using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL.Entities
{
    public class User
    {
        public User() { }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
