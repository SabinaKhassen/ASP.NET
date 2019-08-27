using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        public ActionResult Index()
        {
            List<Authors> authors;
            List<Authors> authorsTop = new List<Authors>();
            using (Model1 db = new NET.Model1())
            {
                authors = db.Authors.ToList();
                var expensiveBooks = db.Books
                    .OrderByDescending(b => b.Price).ToList();
                //expensiveBooks.ForEach(x => authorsTop.Add(db.Authors.Where(a => a.Id == x).FirstOrDefault()));
                foreach (var item in expensiveBooks)
                {
                    authorsTop.Add(db.Authors.Where(a => a.Id == item.AuthorId).FirstOrDefault());
                }
                ViewBag.AuthorsTop = authorsTop.Distinct().Take(5);
            }
            return View(authors);
        }

        public ActionResult Edit(int id)
        {
            Authors author;
            using(Model1 db = new Model1())
            {
                if (id != 0)
                {
                    ViewBag.Message = "Edit";
                    author = db.Authors.Where(a => a.Id == id).FirstOrDefault();
                }
                else
                {
                    ViewBag.Message = "Create";
                    author = null;
                }
            }
            return View(author);
        }

        [HttpPost]
        public ActionResult Edit(Authors author)
        {
            using (Model1 db = new Model1())
            {
                Authors auth = db.Authors.Where(a => a.Id == author.Id).FirstOrDefault();
                if (auth != null)
                {
                    auth.FirstName = author.FirstName;
                    auth.LastName = author.LastName;
                }
                else
                    db.Authors.Add(author);
                db.SaveChanges();
            }
            return RedirectToActionPermanent("Index", "Author");
        }

        public ActionResult Delete(int id)
        {
            Authors author;
            using (Model1 db = new Model1())
            {
                author = db.Authors.Where(a => a.Id == id).FirstOrDefault();
                db.Authors.Remove(author);
                db.SaveChanges();
            }
            return RedirectToActionPermanent("Index", "Author");
        }

        public ActionResult _MyPartialView()
        {
            using (Model1 db = new NET.Model1())
            {
                var expensiveBooks = db.Books
                    .OrderByDescending(b => b.Price).ToList();
                ViewBag.ExpBooks = expensiveBooks;
                ViewBag.Authors = db.Authors.ToList();
            }
            return PartialView();
        }
    }
}