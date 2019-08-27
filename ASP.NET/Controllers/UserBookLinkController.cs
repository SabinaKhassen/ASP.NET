using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET.Controllers
{
    public class UserBookLinkController : Controller
    {
        // GET: UserBookLink
        public ActionResult Index()
        {
            List<UserBookLinks> link;
            using (Model1 db = new Model1())
            {
                link = db.UserBookLinks.ToList();
                ViewBag.Users = db.Users.ToList();
                ViewBag.Books = db.Books.ToList();
            }
            return View(link);
        }

        public ActionResult Edit(int id)
        {
            UserBookLinks link;
            List<Books> books = new List<Books>();
            if (id != 0)
            {
                ViewBag.Message = "Edit";
                using (Model1 db = new Model1())
                {
                    link = db.UserBookLinks.Where(u => u.Id == id).FirstOrDefault();
                    ViewBag.Users = new SelectList(db.Users.ToList(), "Id", "FIO");
                    ViewBag.Books = new SelectList(db.Books.ToList(), "Id", "Title");
                    List<UserBookLinks> links = db.UserBookLinks.Where(u => u.UserId == link.UserId).ToList();
                    links.ForEach(l => books.Add(db.Books.Where(b => b.Id == l.BookId).FirstOrDefault()));
                    ViewBag.BookOrders = books.Distinct().Take(5);
                }
            }
            else
            {
                ViewBag.Message = "Create";
                link = null;
                using (Model1 db = new Model1())
                {
                    ViewBag.Users = new SelectList(db.Users.ToList(), "Id", "FIO");
                    ViewBag.Books = new SelectList(db.Books.ToList(), "Id", "Title");
                }
            }
            return View(link);
        }

        [HttpPost]
        public ActionResult Edit(UserBookLinks link)
        {
            using (Model1 db = new Model1())
            {
                UserBookLinks lnk = db.UserBookLinks.Where(u => u.Id == link.Id).FirstOrDefault();
                if (lnk == null)
                    db.UserBookLinks.Add(link);
                else
                {
                    //lnk.UserId = link.UserId;
                    lnk.BookId = link.BookId;
                }
                db.SaveChanges();
            }
            return RedirectToActionPermanent("Index", "UserBookLink");
        }

        public ActionResult Delete(int id)
        {
            using (Model1 db = new Model1())
            {
                UserBookLinks link = db.UserBookLinks.Where(u => u.Id == id).FirstOrDefault();
                db.UserBookLinks.Remove(link);
                db.SaveChanges();
            }
            return RedirectToActionPermanent("Index", "UserBookLink");
        }

        public ActionResult UserOrders()
        {
            return PartialView();
        }
    }
}