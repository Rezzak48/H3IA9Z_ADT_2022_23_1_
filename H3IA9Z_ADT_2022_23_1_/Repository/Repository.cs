using Data;
using H3IA9Z_ADT_2022_23_1_Repository;
using System;
using System.Linq;

namespace Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected ChooseYourMovieDbContext cntx;
        public Repository(ChooseYourMovieDbContext cntx)
        {
            this.cntx = cntx;
        }

        public IQueryable<T> GetAll()
        {
            return this.cntx.Set<T>();
        }

        public T GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
