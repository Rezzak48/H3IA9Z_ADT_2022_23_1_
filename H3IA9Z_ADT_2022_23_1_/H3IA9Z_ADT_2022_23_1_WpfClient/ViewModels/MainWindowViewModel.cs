using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3IA9Z_ADT_2022_23_1_WpfClient.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public RelayCommand ManageMoviesCommand { get; set; }
        public RelayCommand ManageVisitorsCommand { get; set; }
        public RelayCommand ManageReservationsCommand { get; set; }

        public MainWindowViewModel()
        {
            ManageMoviesCommand = new RelayCommand(OpenMoviesWindow);
            ManageVisitorsCommand = new RelayCommand(OpenVisitorsWindow);
            ManageReservationsCommand = new RelayCommand(OpenReservationsWindow);
        }

        //! Windows to create

        private void OpenMoviesWindow()
        {
            new MoviesWindow().Show();
        }

        private void OpenVisitorsWindow()
        {
            new VisitorWindow().Show();
        }

        private void OpenReservationsWindow()
        {
            new ReservationsWindow().Show();
        }
    }
}