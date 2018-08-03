using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using DAL;
using BLL.Entities;
using AutoMapper;

namespace BLL.Operations
{
    public class UserOperations : IUserOperations
    {
        IKernel ninjectKernel;
        public IUnitOfWork uow { get; set; }
        public UserOperations(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public UserOperations()
        {
            ninjectKernel = new StandardKernel(new BLLNinjectConfig());
            this.uow = ninjectKernel.Get<IUnitOfWork>();
        }
        public bool CheckUser(string login)
        {
            IEnumerable<DBUser> users = uow.Users.Get();
            foreach (DBUser user in users)
            {
                if (user.Login == login) return true;
            }
            return false;
        }

        public int CheckUser(string login, string password)
        {
            IEnumerable<DBUser> users = uow.Users.Get();
            foreach (DBUser user in users)
            {
                if (user.Login == login && user.Password == password)
                    return user.UserId;
            }
            return 0;
        }
        public bool UserAuthentication(string name, string surname, string patronymic)
        {
            IEnumerable<DBUser> users = uow.Users.Get();
            foreach (DBUser user in users)
            {
                if (user.Name == name && user.Surname == surname && user.Patronymic == patronymic) return true;
            }
            return false;
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            IEnumerable<DBUser> dbusers = uow.Users.Get();
            users = Mapper.Map<IEnumerable<DBUser>, List<User>>(dbusers);
            return users;
        }

        public int SaveUser(User user)
        {
            try
            {
                DBUser newUser = Mapper.Map<User, DBUser>(user);
                uow.Users.Create(newUser);
                uow.Save();
                int id = CheckUser(user.Login, user.Password);
                return id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool deleteUser(int UserId)
        {
            try
            {
                DBUser user = uow.Users.FindById(UserId);
                if (user != null)
                    uow.Users.Remove(user);
                uow.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
