using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.UnitOfWork;
using Unity;

namespace BussinessLayer.BussinessObjects
{
    public class BookBO : BussinessObjectBase<Books>
    {
        private readonly IUnityContainer container;

        public BookBO(IMapper mapper, UnitOfWorkFactory<Books> unitOfWorkFactory, IUnityContainer container) : base(mapper, unitOfWorkFactory)
        {
            this.container = container;
        }

        public List<BookBO> GetListBooks()
        {
            List<BookBO> books = null;

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                books = unitOfWork.EntityRepository.GetAll().Select(item => mapper.Map<BookBO>(item)).ToList();
            }
            return books;
        }

        public BookBO GetListBooksById(int id)
        {
            BookBO book = null;

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                book = unitOfWork.EntityRepository.GetAll().Where(b => b.Id == id).Select(item => mapper.Map<BookBO>(item)).FirstOrDefault();
            }
            return book;
        }
    }
}
