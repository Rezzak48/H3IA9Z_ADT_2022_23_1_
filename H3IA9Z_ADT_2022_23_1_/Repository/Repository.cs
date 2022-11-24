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
        // Get back to it later
        public abstract T GetOne(int id);
        public void Insert(T entity)
        {
            cntx.Set<T>().Add(entity);

            cntx.SaveChanges();
        }

        public void Remove(T entity)
        {
            cntx.Set<T>().Remove(entity);
            cntx.SaveChanges();
        }
    }
}
