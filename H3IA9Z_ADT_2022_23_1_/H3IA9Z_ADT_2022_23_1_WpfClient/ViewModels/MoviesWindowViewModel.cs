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

        public MoviesWindowViewModel()
        {
            Movies = new ObservableCollection<Movie>();

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

            AddMovieCommand = new RelayCommand(AddMovie);
            EditMovieCommand = new RelayCommand(EditMovie);
            DeleteMovieCommand = new RelayCommand(DeleteMovie);
        }

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
    }
}