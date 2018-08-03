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
    public class UserController : ApiController
    {
        IKernel ninjectKernel;
        IUserOperations UOperations;
        public UserController()
        {
            ninjectKernel = new StandardKernel(new NinjectConfig());
            UOperations = ninjectKernel.Get<IUserOperations>();
        }

        [HttpGet]
        [Route("api/user/{login}/{password}")]
        public IHttpActionResult SignIn(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                return BadRequest("Please, enter login and password");
            else
            {
                int userId = UOperations.CheckUser(login, password);
                if (userId > 0)
                    return Ok(userId);
                else return BadRequest("Please, check correctness of login and password");
            }
        }

        [HttpGet]
        [Route("api/user")]
        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = UOperations.GetUsers();
            return users;
        }

        [HttpPost]
        [Route("api/user/newUser")]
        public IHttpActionResult PostUser(User user)
        {
            string patt = @"^[\d|\D]{1,30}$";

            if (string.IsNullOrWhiteSpace(user.Login) || string.IsNullOrWhiteSpace(user.Password)
                || string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Surname)
                || string.IsNullOrWhiteSpace(user.Patronymic))
            {
                return BadRequest("Please, fill all fields");
            }
            else if (!Regex.IsMatch(user.Name, patt))
                return BadRequest("Name is too longs");
            else if (!Regex.IsMatch(user.Login, patt))
                return BadRequest("Login is too longs");
            else if (!Regex.IsMatch(user.Password, patt))
                return BadRequest("Password is too longs");
            else if (!Regex.IsMatch(user.Surname, patt))
                return BadRequest("Surname is too longs");
            else if (!Regex.IsMatch(user.Patronymic, patt))
                return BadRequest("Patronymic is too longs");
            else if (UOperations.CheckUser(user.Login))
                return BadRequest("This login already registered");
            else if (UOperations.UserAuthentication(user.Name, user.Surname, user.Patronymic))
                return BadRequest("Such person already registered");
            else
            {
                int userId = UOperations.SaveUser(user);
                if (userId > 0)
                    return Ok(userId);
                else return BadRequest("Please, check correctness of all fields");
            }
        }
        [HttpDelete]
        [Route("api/user/delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid user id");
            bool deleted = UOperations.deleteUser(id);
            if (deleted)
                return Ok();
            else
                return BadRequest("This user does not exist");
        }
    }
}
