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
        //public UsersController(IUserRepository userRepository)
        //{
        //    _userRepository = userRepository;
        //}
        private BllDbContext db = new BllDbContext();

        // GET: Users
        public ActionResult Index()
        {
            List<User> userList = new List<User>()
            {
                new User()
                {
                    EmployeeId = 1,
                    Password = "123",
                    Salt = "545545",
                    UserFullName = "SPP",
                    UserId = 45,
                    UserName = "Sfi",
                    UserSbUs = "2"

                },
                 new User()
                {
                    EmployeeId = 3,
                    Password = "wqpodj",
                    Salt = "wklahnclke",
                    UserFullName = "lqwkhadn",
                    UserId = 989,
                    UserName = "lxwkqhdnpe",
                    UserSbUs = "2cwqodj"
                }

            };
            return View();
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserFullName,UserName,Password,Salt,UserSbUs,EmployeeId")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
