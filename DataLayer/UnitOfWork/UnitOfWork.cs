using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Repositories;
using System.Data.Entity;

namespace DataLayer.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly DbContext db;
        bool disposed = false;

        Repository<T> entityRepository;

        public UnitOfWork(DbContext db)
        {
            this.db = db;
            db.Database.CommandTimeout = 100;
        }

        public Repository<T> EntityRepository
        {
            get
            {
                return entityRepository == null ? new Repository<T>() : entityRepository;
            }
        }

        public void BeginTransaction()
        {
            db.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            db.Database.CurrentTransaction.Commit();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (db != null)
                        db.Dispose();
                }
                disposed = true;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
