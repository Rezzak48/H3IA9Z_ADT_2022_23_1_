using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using H3IA9Z_ADT_2022_23_1_WpfClient.Clients;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace H3IA9Z_ADT_2022_23_1_WpfClient.ViewModels
{
    internal class MoviesWindowViewModel : ObservableRecipient
    {
        private ApiClient _apiClient = new ApiClient();

        public ObservableCollection<Movie> Movies { get; set; }
        public IList<KeyValuePair<string, int>> TotalMoviesEarnings { get; set; }
        public IList<KeyValuePair<string, int>> MostPaidMovie { get; set; }
        public IList<KeyValuePair<string, int>> LessPaidMovie { get; set; }

        private Movie _selectedMovie;

        public Movie SelectedMovie
        {
            get => _selectedMovie;
            set
            {
                SetProperty(ref _selectedMovie, value);
            }
        }

        private int _selectedMovieIndex;

        public int SelectedMovieIndex
        {
            get => _selectedMovieIndex;
            set
            {
                SetProperty(ref _selectedMovieIndex, value);
            }
        }

        public RelayCommand AddMovieCommand { get; set; }
        public RelayCommand EditMovieCommand { get; set; }
        public RelayCommand DeleteMovieCommand { get; set; }
        public RelayCommand MoviesEarningCommand { get; set; }
        public RelayCommand MostPaidMovieCommand { get; set; }
        public RelayCommand LessPaidMovieCommand { get; set; }

        public MoviesWindowViewModel()
        {
            Movies = new ObservableCollection<Movie>();

            TotalMoviesEarnings = new List<KeyValuePair<string, int>>();
            MostPaidMovie = new List<KeyValuePair<string, int>>();
            LessPaidMovie = new List<KeyValuePair<string, int>>();

            _apiClient
                .GetAsync<List<Movie>>("http://localhost:18972/movie")
                .ContinueWith((movies) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        movies.Result.ForEach((movie) =>
                        {
                            Movies.Add(movie);
                        });
                    });
                });

            _apiClient
              .GetAsync<List<KeyValuePair<string, int>>>("http://localhost:18972/Noncrudmovie/MovieEarnings")
              .ContinueWith((moviesEar) =>
              {
                  Application.Current.Dispatcher.Invoke(() =>
                  {
                      moviesEar.Result.ForEach((movieea) =>
                      {
                          TotalMoviesEarnings.Add(movieea);
                      });
                  });
              });
            _apiClient
               .GetAsync<List<KeyValuePair<string, int>>>("http://localhost:18972/Noncrudmovie/Mostpaidmov")
               .ContinueWith((MostMov) =>
               {
                   Application.Current.Dispatcher.Invoke(() =>
                   {
                       MostMov.Result.ForEach((mostmovie) =>
                       {
                           MostPaidMovie.Add(mostmovie);
                       });
                   });
               });
            _apiClient
              .GetAsync<List<KeyValuePair<string, int>>>("http://localhost:18972/Noncrudmovie/Lesspaidmo")
              .ContinueWith((MostMov) =>
              {
                  Application.Current.Dispatcher.Invoke(() =>
                  {
                      MostMov.Result.ForEach((mostmovie) =>
                      {
                          LessPaidMovie.Add(mostmovie);
                      });
                  });
              });

            AddMovieCommand = new RelayCommand(AddMovie);
            EditMovieCommand = new RelayCommand(EditMovie);
            DeleteMovieCommand = new RelayCommand(DeleteMovie);
            MoviesEarningCommand = new RelayCommand(MoviesEarningCalculation);
            MostPaidMovieCommand = new RelayCommand(MostPaidMovieCalculation);
            LessPaidMovieCommand = new RelayCommand(LessPaidMovieCalculation);
        }

        #region CRUD

        private void AddMovie()
        {
            Movie n = new Movie
            {
                Name = SelectedMovie.Name,
                Price = _selectedMovie.Price,
                Category = SelectedMovie.Category,
                Duration = SelectedMovie.Duration
            };

            _apiClient
                .PostAsync(n, "http://localhost:18972/movie")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Movies.Add(n);
                    });
                });
        }

        private void EditMovie()
        {
            _apiClient
                .PutAsync(SelectedMovie, "http://localhost:18972/movie")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        int i = SelectedMovieIndex;
                        Movie selec = SelectedMovie;
                        Movies.Remove(SelectedMovie);
                        Movies.Insert(i, selec);
                    });
                });
        }

        private void DeleteMovie()
        {
            _apiClient
                .DeleteAsync(SelectedMovie.Id, "http://localhost:18972/movie")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Movies.Remove(SelectedMovie);
                    });
                });
        }

        #endregion CRUD

        #region NON-CRUD

        private void MoviesEarningCalculation()
        {
            new MoviesEarningWindow().Show();
        }

        private void MostPaidMovieCalculation()
        {
            new MostPaidMovieWindow().Show();
        }

        private void LessPaidMovieCalculation()
        {
            new LessPaidMovieWindow().Show();
        }

        #endregion NON-CRUD
    }
}