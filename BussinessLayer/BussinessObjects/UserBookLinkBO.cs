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
    public class UserBookLinkBO : BussinessObjectBase<UserBookLinks>
    {
        private readonly IUnityContainer container;

        public UserBookLinkBO(IMapper mapper, UnitOfWorkFactory<UserBookLinks> unitOfWorkFactory, IUnityContainer container) : base(mapper, unitOfWorkFactory)
        {
            this.container = container;
        }

        public List<UserBookLinkBO> GetListUserLinks()
        {
            List<UserBookLinkBO> links = null;

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                links = unitOfWork.EntityRepository.GetAll().Select(item => mapper.Map<UserBookLinkBO>(item)).ToList();
            }
            return links;
        }

        public UserBookLinkBO GetListUserLinksById(int id)
        {
            UserBookLinkBO link = null;

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                link = unitOfWork.EntityRepository.GetAll().Where(l => l.Id == id).Select(item => mapper.Map<UserBookLinkBO>(item)).FirstOrDefault();
            }
            return link;
        }
    }
}
