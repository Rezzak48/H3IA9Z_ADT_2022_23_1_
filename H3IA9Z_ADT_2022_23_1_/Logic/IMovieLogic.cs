using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3IA9Z_ADT_2022_23_1_Logic
{
    public interface IMovieLogic
    {
        public Movie AddNewMovie(Movie newMovie);
        public void DeleteMovie(int id);
        Movie GetMovie(int id);
        IEnumerable<Movie> GetAllMovies();
        void UpdateMovieCost(Movie value);

        IEnumerable<KeyValuePair<string, int>> MovieEarnings();
        List<KeyValuePair<string, int>> MostSellMovie();
        List<KeyValuePair<string, int>> LessSellMovie();
    }
}
