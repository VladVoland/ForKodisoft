using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BLL.Entities;

namespace BLL.Operations
{
    public interface IUserOperations
    {
        IUnitOfWork uow { get; set; }
        bool CheckUser(string login);
        int CheckUser(string login, string password);
        bool UserAuthentication(string name, string surname, string patronymic);
        List<User> GetUsers();
        int SaveUser(User user);
        bool deleteUser(int UserId);
    }
}
