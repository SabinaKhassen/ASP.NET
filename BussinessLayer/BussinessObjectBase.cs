using AutoMapper;
using DataLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public abstract class BussinessObjectBase<T> where T : class
    {
        protected IMapper mapper;
        protected UnitOfWorkFactory<T> unitOfWorkFactory;

        public BussinessObjectBase(IMapper mapper, UnitOfWorkFactory<T> unitOfWorkFactory)
        {
            this.mapper = mapper;
            this.unitOfWorkFactory = unitOfWorkFactory;
        }
    }
}
