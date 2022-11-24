using Data;
using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3IA9Z_ADT_2022_23_1_Repository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(ChooseYourMovieDbContext DbContext) : base(DbContext) { }

        public override Movie GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(mov => mov.Id == id);
        }

        public void UpdatePrice(int id, int newprice)
        {
            var movie = this.GetOne(id);
            if (movie == null)
            {
                throw new Exception("The movie doesn't exist");
            }
            else
            {
                movie.Price = newprice;
                this.cntx.SaveChanges();
            }
        }
    }
}