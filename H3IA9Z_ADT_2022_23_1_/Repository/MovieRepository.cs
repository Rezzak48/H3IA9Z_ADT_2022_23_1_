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
        public MovieRepository(ChooseYourMovieDbContext cntx) : base(cntx)
        {
        }

        public void UpdatePrice(int id, int newprice)
        {
            throw new NotImplementedException();
        }
    }
}