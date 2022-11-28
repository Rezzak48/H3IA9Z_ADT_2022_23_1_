using H3IA9Z_ADT_2022_23_1_Repository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3IA9Z_ADT_2022_23_1_Logic
{
    public class MovieLogic : IMovieLogic
    {
        protected IMovieRepository imovieRepository;
        protected IReservationsRepository ireservationsRepository;
        public MovieLogic(IMovieRepository imovieRepository, IReservationsRepository ireservationsRepository)
        {
            this.imovieRepository = imovieRepository;
            this.ireservationsRepository = ireservationsRepository;
        }
        public Movie AddNewArtist(Movie newMovie)
        {
            this.imovieRepository.Insert(newMovie);
            return newMovie;
        }

        public void DeleteMovie(int id)
        {
            Movie movieToDelete = this.imovieRepository.GetOne(id);
            if (movieToDelete != null)
            {
                this.imovieRepository.Remove(movieToDelete);
            }
            else
            {
                throw new ArgumentException("This ID can't be founded.");
            }
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return this.imovieRepository.GetAll();

        }

        public Movie GetMovie(int id)
        {
            Movie returnedMovie = this.imovieRepository.GetOne(id);
            if (returnedMovie != null)
            {
                return returnedMovie;
            }
            else
            {
                throw new Exception("No movie is founded with this ID.");
            }
        }

        public List<KeyValuePair<string, int>> LessSellMovie()
        {
            throw new NotImplementedException();
        }

        public List<KeyValuePair<string, int>> MostSellMovie()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<KeyValuePair<string, int>> MovieEarnings()
        {
            throw new NotImplementedException();
        }

        public void UpdateMovieCost(Movie value)
        {
            this.imovieRepository.UpdatePrice(value.Id, value.Price);

        }
    }
}
