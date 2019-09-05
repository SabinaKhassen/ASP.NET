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
    public class AuthorBO : BussinessObjectBase<Authors>
    {
        private readonly IUnityContainer container;

        public int? Id { get; set; }

        public AuthorBO(IMapper mapper, UnitOfWorkFactory<Authors> unitOfWorkFactory, IUnityContainer container) : base(mapper, unitOfWorkFactory)
        {
            this.container = container;
        }

        public List<AuthorBO> GetListAuthors()
        {
            List<AuthorBO> authors = null;

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                authors = unitOfWork.EntityRepository.GetAll().Select(item => mapper.Map<AuthorBO>(item)).ToList();
            }
            return authors;
        }

        public AuthorBO GetListAuthorsById(int id)
        {
            AuthorBO author = null;

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                author = unitOfWork.EntityRepository.GetAll().Where(a => a.Id == id).Select(item => mapper.Map<AuthorBO>(item)).FirstOrDefault();
            }
            return author;
        }

        public void Save()
        {
            using (var unitofWork = unitOfWorkFactory.Create())
            {
                if (Id != null)
                    Update(unitofWork);
                else
                    Add(unitofWork);
            }
        }

        public void Delete(int id)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                unitOfWork.EntityRepository.Delete(id);
                unitOfWork.Save();
            }
        }

        void Add(IUnitOfWork<Authors> unitOfWork)
        {
            var author = mapper.Map<Authors>(this);
            unitOfWork.EntityRepository.Add(author);
            unitOfWork.Save();
        }

        void Update(IUnitOfWork<Authors> unitOfWork)
        {
            var author = mapper.Map<Authors>(this);
            unitOfWork.EntityRepository.Update(author);
            unitOfWork.Save();
        }
    }
}
