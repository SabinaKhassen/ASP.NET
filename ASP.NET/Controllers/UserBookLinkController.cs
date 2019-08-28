using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;

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
                {
                    link.CreationDate = DateTime.Now;
                    link.ReturnDate = DateTime.Now;
                    db.UserBookLinks.Add(link);
                }
                else
                {
                    //lnk.UserId = link.UserId;
                    lnk.BookId = link.BookId;
                    lnk.Deadline = link.Deadline;
                    lnk.ReturnDate = link.ReturnDate;
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

        public ActionResult SendEmail(int id)
        {
            using (Model1 db = new Model1())
            {
                var link = db.UserBookLinks.Where(u => u.Id == id).FirstOrDefault();
                var titleBook = db.Books.Where(b => b.Id == link.BookId).FirstOrDefault();

                MailAddress from = new MailAddress("sabina.khasen@gmail.com", "Sabina");
                MailAddress to = new MailAddress(db.Users.Where(u => u.Id == link.UserId).Select(u => u.Email).FirstOrDefault());
                MailMessage message = new MailMessage(from, to);
                message.Subject = "Return '" + titleBook.Title + "'";
                message.Body = string.Format("Your order was due to " + link.Deadline + ". Return the book!");
                message.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("sabina.khasen@gmail.com", "asgbdn19737577");
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
            return RedirectToActionPermanent("Index", "UserBookLink");
        }
    }
}