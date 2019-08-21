using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            List<Books> books;
            using (Model1 db = new Model1())
            {
                books = db.Books.ToList();
            }
            return View(books);
        }

        public ActionResult Edit(int id)
        {
            Books books;
            if (id != 0)
            {
                ViewBag.Message = "Edit";
                using (Model1 db = new Model1())
                    books = db.Books.Where(b => b.Id == id).FirstOrDefault();
            }
            else
            {
                ViewBag.Message = "Create";
                books = null;
            }

            return View(books);
        }

        [HttpPost]
        public ActionResult Edit(Books book)
        {
            using (Model1 db = new Model1())
            {
                var bk = db.Books.Where(b => b.Id == book.Id).FirstOrDefault();
                if (bk == null)
                    db.Books.Add(book);
                else
                {
                    bk.AuthorId = book.AuthorId;
                    bk.Authors = book.Authors;
                    bk.Pages = book.Pages;
                    bk.Price = book.Price;
                    bk.Title = book.Title;
                }
                db.SaveChanges();
            }
            return RedirectToActionPermanent("Index", "Book");
        }
    }
}