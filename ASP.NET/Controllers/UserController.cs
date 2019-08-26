using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            List<Users> users;
            using (Model1 db = new Model1())
            {
                users = db.Users.ToList();
            }
            return View(users);
        }

        public ActionResult Edit(int id)
        {
            Users user;
            if (id != 0)
            {
                ViewBag.Message = "Edit";
                using (Model1 db = new Model1())
                {
                    user = db.Users.Where(u => u.Id == id).FirstOrDefault();
                }
            }
            else
            {
                ViewBag.Message = "Create";
                user = null;
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(Users user)
        {
            using (Model1 db = new Model1())
            {
                Users us = db.Users.Where(u => u.Id == user.Id).FirstOrDefault();
                if (us != null)
                {
                    us.FIO = user.FIO;
                }
                else
                    db.Users.Add(user);
                db.SaveChanges();
            }
            return RedirectToActionPermanent("Index", "User");
        }

        public ActionResult Delete(int id)
        {
            using (Model1 db = new Model1())
            {
                Users user = db.Users.Where(u => u.Id == id).FirstOrDefault();
                db.Users.Remove(user);
                db.SaveChanges();
            }
            return RedirectToActionPermanent("Index", "User");
        }
    }
}