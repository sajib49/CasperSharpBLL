using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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
        private  byte[] _contentBytes;
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
        public ActionResult ExportCustomers()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "TestRepot.rpt"));

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "test.pdf");


        }

 
    }
}
