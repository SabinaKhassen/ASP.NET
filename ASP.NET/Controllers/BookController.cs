using AutoMapper;
using BussinessLayer.BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET.Controllers
{
    public class BookController : Controller
    {
        private readonly IMapper mapper;
        // GET: Book

        public BookController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var books = DependencyResolver.Current.GetService<BookBO>();
            var authors = DependencyResolver.Current.GetService<AuthorBO>();
            ViewBag.Books = books.GetListBooks();
            ViewBag.Authors = authors.GetListAuthors();

            return View();
        }

        public ActionResult Edit(int id)
        {
            Books books;
            if (id != 0)
            {
                ViewBag.Message = "Edit";
                using (Model1 db = new Model1())
                {
                    books = db.Books.Where(b => b.Id == id).FirstOrDefault();
                    ViewBag.Authors = new SelectList(db.Authors.ToList(), "Id", "LastName");
                }
            }
            else
            {
                ViewBag.Message = "Create";
                books = null;
                using (Model1 db = new Model1())
                {
                    ViewBag.Authors = new SelectList(db.Authors.ToList(), "Id", "LastName");
                }
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

        public ActionResult Delete(int id)
        {
            Books book;
            using (Model1 db = new Model1())
            {
                book = db.Books.Where(b => b.Id == id).FirstOrDefault();
                db.Books.Remove(book);
                db.SaveChanges();
            }
            return RedirectToActionPermanent("Index", "Book");
        }
    }
}