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
            using (Model1 db = new NET.Model1())
            {
                authors = db.Authors.ToList();
            }
            return View(authors);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Authors author)
        {
            using (Model1 db = new Model1())
            {
                db.Authors.Add(author);
                db.SaveChanges();
            }
            return Redirect("Index");
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
    }
}