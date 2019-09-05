using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using AutoMapper;
using DataLayer.UnitOfWork;

namespace BussinessLayer.BussinessObjects
{
    public class UserBO : BussinessObjectBase<Users>
    {
        private readonly IUnityContainer container;

        public UserBO(IMapper mapper, UnitOfWorkFactory<Users> unitOfWorkFactory, IUnityContainer container) : base(mapper, unitOfWorkFactory)
        {
            this.container = container;
        }

        public List<UserBO> GetListUsers()
        {
            List<UserBO> users = null;

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                users = unitOfWork.EntityRepository.GetAll().Select(item => mapper.Map<UserBO>(item)).ToList();
            }
            return users;
        }

        public UserBO GetListUsersById(int id)
        {
            UserBO user = null;

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                user = unitOfWork.EntityRepository.GetAll().Where(u => u.Id == id).Select(item => mapper.Map<UserBO>(item)).FirstOrDefault();
            }
            return user;
        }
    }
}
