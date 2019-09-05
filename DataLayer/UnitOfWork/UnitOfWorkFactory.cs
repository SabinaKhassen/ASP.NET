using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.UnitOfWork
{
    public class UnitOfWorkFactory<T> where T : class
    {
        public virtual IUnitOfWork<T> Create()
        {
            return new UnitOfWork<T>(new Model1());
        }
    }
}
