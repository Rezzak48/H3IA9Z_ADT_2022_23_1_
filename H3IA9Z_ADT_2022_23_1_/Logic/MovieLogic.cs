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
            int min = MovieEarnings().Min(t => t.Value);
            string[] minNums = MovieEarnings().Where(x => x.Value == min).Select(x => x.Key).ToArray();
            List<KeyValuePair<string, int>> revenu = new List<KeyValuePair<string, int>>();
            for (int i = 0; i < minNums.Length; i++)
            {
                revenu.Add(new KeyValuePair<string, int>(minNums[i], min));
            }
            return revenu;
        }

        public List<KeyValuePair<string, int>> MostSellMovie()
        {
            int max = MovieEarnings().Max(t => t.Value);
            string[] maxNums = MovieEarnings().Where(x => x.Value == max).Select(x => x.Key).ToArray();
            List<KeyValuePair<string, int>> revenu = new List<KeyValuePair<string, int>>();
            for (int i = 0; i < maxNums.Length; i++)
            {
                revenu.Add(new KeyValuePair<string, int>(maxNums[i], max));
            }
            return revenu;
        }

        public IEnumerable<KeyValuePair<string, int>> MovieEarnings()
        {
            var TotalEarning = from movies in this.imovieRepository.GetAll().ToList()
                               join reservations in this.ireservationsRepository.GetAll().ToList()
                               on movies.Id equals reservations.MovieId
                               group reservations by reservations.MovieId.Value into gr
                               select new KeyValuePair<string, int>
                               (imovieRepository.GetOne(gr.Key).Name, (gr.Count()) * imovieRepository.GetOne(gr.Key).Price);
            return TotalEarning;
        }

        public void UpdateMovieCost(Movie value)
        {
            this.imovieRepository.UpdatePrice(value.Id, value.Price);

        }
    }
}
