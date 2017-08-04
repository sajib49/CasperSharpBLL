using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using CS.Model;
using CS.Data;
using CS.Data.Interfaces;
using CS.Data.Repositories;
using WebGrease.Css.Extensions;

namespace CS.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository = new UserRepository();

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn([Bind(Include = "UserName,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    var loggedUser = _userRepository.Find(x => x.UserName.ToLower() == user.UserName.Trim().ToLower());
                    if (loggedUser != null)
                    {
                        PdsaHash mph2 = new PdsaHash(PdsaHash.PdsaHashType.MD5);
                        string pass = mph2.CreateHash(user.Password, loggedUser.Salt);
                        if (pass != loggedUser.Password)
                        {
                            ModelState.AddModelError("LoginFaild", "The User Name or Password is Incorrect");
                            return View();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("LoginFaild", "The User Name or Password is Incorrect");
                        return View();
                    }
                    Session["LoggedUser"] = loggedUser;
                    return RedirectToAction("Index", "Cbg");
                }
                ModelState.AddModelError("LoginFaild", "Please Provide User Name & Password");
                return View();
            }

            return View();
        }

    }
}
