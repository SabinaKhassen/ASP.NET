using ASP.NET.Models;
using AutoMapper;
using BussinessLayer.BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET.Controllers
{
    public class AuthorController : Controller
    {
        protected IMapper mapper;
        // GET: Author

        public AuthorController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            AuthorBO authors = DependencyResolver.Current.GetService<AuthorBO>();
            List<AuthorViewModel> authorsTop = new List<AuthorViewModel>();
            BookBO books = DependencyResolver.Current.GetService<BookBO>();
            var expensiveBooks = books.GetListBooks().Select(item => mapper.Map<BookViewModel>(item))
                                .OrderByDescending(b => b.Price).ToList();
            //expensiveBooks.ForEach(x => authorsTop.Add(db.Authors.Where(a => a.Id == x).FirstOrDefault()));
            foreach (var item in expensiveBooks)
            {
                authorsTop.Add(authors.GetListAuthors().Select(a => mapper.Map<AuthorViewModel>(a))
                    .Where(a => a.Id == item.AuthorId).FirstOrDefault());
            }
            ViewBag.Authors = authors.GetListAuthors().Select(item => mapper.Map<AuthorViewModel>(item)).ToList();
            ViewBag.AuthorsTop = authorsTop.Distinct().Take(5);
            
            return View();
        }

        public ActionResult Edit(int id)
        {
            AuthorBO authors = DependencyResolver.Current.GetService<AuthorBO>();
            AuthorViewModel model = null;
            if (id != 0)
            {
                ViewBag.Message = "Edit";
                model = mapper.Map<AuthorViewModel>(authors.GetListAuthorsById(id));
            }
            else
                ViewBag.Message = "Create";
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AuthorViewModel author)
        {
            var auths = DependencyResolver.Current.GetService<AuthorBO>();
            var auth = mapper.Map<AuthorBO>(author);
            auth.Save();

            return RedirectToActionPermanent("Index", "Author");
        }

        public ActionResult Delete(int id)
        {
            var author = DependencyResolver.Current.GetService<AuthorBO>().GetListAuthorsById(id);
            author.Delete(id);

            return RedirectToActionPermanent("Index", "Author");
        }

        public ActionResult _MyPartialView()
        {
            var books = DependencyResolver.Current.GetService<BookBO>();
            var authors = DependencyResolver.Current.GetService<AuthorBO>();
            var expensiveBooks = books.GetListBooks().Select(item => mapper.Map<BookViewModel>(item))
                                .OrderByDescending(b => b.Price).ToList();
            ViewBag.ExpBooks = expensiveBooks;
            ViewBag.Authors = authors.GetListAuthors();
            
            return PartialView();
        }
    }
}