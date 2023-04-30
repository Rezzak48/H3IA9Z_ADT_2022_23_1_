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
    public class ReservationsWindowViewModel : ObservableRecipient
    {
        private ApiClient _apiClient = new ApiClient();

        public ObservableCollection<Reservation> Reservations { get; set; }
        private Reservation _selectedReservation;

        public Reservation SelectedReservation
        {
            get => _selectedReservation;
            set
            {
                SetProperty(ref _selectedReservation, value);
            }
        }

        private int _selectedReservationIndex;

        public int SelectedReservationIndex
        {
            get => _selectedReservationIndex;
            set
            {
                SetProperty(ref _selectedReservationIndex, value);
            }
        }

        public RelayCommand AddReservationCommand { get; set; }
        public RelayCommand EditReservationCommand { get; set; }
        public RelayCommand DeleteReservationCommand { get; set; }

        public ReservationsWindowViewModel()
        {
            Reservations = new ObservableCollection<Reservation>();

            _apiClient
                .GetAsync<List<Reservation>>("http://localhost:18972/reservations")
                .ContinueWith((reservations) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        reservations.Result.ForEach((reservation) =>
                        {
                            Reservations.Add(reservation);
                        });
                    });
                });

            AddReservationCommand = new RelayCommand(AddReservation);
            EditReservationCommand = new RelayCommand(EditReservation);
            DeleteReservationCommand = new RelayCommand(DeleteReservation);
        }

        private void AddReservation()
        {
            Reservation n = new Reservation
            {
                MovieId = SelectedReservation.MovieId,
                VisitorId = SelectedReservation.VisitorId,
                DateTime = SelectedReservation.DateTime
            };

            _apiClient
                .PostAsync(n, "http://localhost:18972/reservations")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Reservations.Add(n);
                    });
                });
        }

        private void EditReservation()
        {
            _apiClient
                .PutAsync(SelectedReservation, "http://localhost:18972/reservations")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        int i = SelectedReservationIndex;
                        Reservation a = SelectedReservation;
                        Reservations.Remove(SelectedReservation);
                        Reservations.Insert(i, a);
                    });
                });
        }

        private void DeleteReservation()
        {
            _apiClient
                .DeleteAsync(SelectedReservation.Id, "http://localhost:18972/reservations")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Reservations.Remove(SelectedReservation);
                    });
                });
        }
    }
}