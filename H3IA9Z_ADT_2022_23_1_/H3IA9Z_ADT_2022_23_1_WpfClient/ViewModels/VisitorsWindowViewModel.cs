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
    public class VisitorsWindowViewModel : ObservableRecipient
    {
        private ApiClient _apiClient = new ApiClient();

        public ObservableCollection<Visitor> Visitors { get; set; }
        private Visitor _selectedVisitor;
        public IList<KeyValuePair<int, int>> BestVisitors { get; set; }
        public IList<KeyValuePair<int, int>> WorstVisitors { get; set; }

        public Visitor SelectedVisitor
        {
            get => _selectedVisitor;
            set
            {
                SetProperty(ref _selectedVisitor, value);
            }
        }

        private int _selectedVisitorIndex;

        public int SelectedVisitorIndex
        {
            get => _selectedVisitorIndex;
            set
            {
                SetProperty(ref _selectedVisitorIndex, value);
            }
        }

        public RelayCommand AddVisitorCommand { get; set; }
        public RelayCommand EditVisitorCommand { get; set; }
        public RelayCommand DeleteVisitorCommand { get; set; }
        public RelayCommand BestVisitorCommand { get; set; }
        public RelayCommand WorstVisitorCommand { get; set; }

        public VisitorsWindowViewModel()
        {
            Visitors = new ObservableCollection<Visitor>();
            BestVisitors = new List<KeyValuePair<int, int>>();
            WorstVisitors = new List<KeyValuePair<int, int>>();

            _apiClient
                .GetAsync<List<Visitor>>("http://localhost:18972/visitor")
                .ContinueWith((visitors) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        visitors.Result.ForEach((visitor) =>
                        {
                            Visitors.Add(visitor);
                        });
                    });
                });

            //_apiClient
            // .GetAsync<List<KeyValuePair<int, int>>>("http://localhost:18972/Noncrudvis/BestVisitors")
            // .ContinueWith((BestVisit) =>
            // {
            //     Application.Current.Dispatcher.Invoke(() =>
            //     {
            //         BestVisit.Result.ForEach((Bestf) =>
            //         {
            //             BestVisitors.Add(Bestf);
            //         });
            //     });
            // });
            //_apiClient
            //  .GetAsync<List<KeyValuePair<int, int>>>("http://localhost:18972/NoncrudVisitor/WorstFans")
            //  .ContinueWith((WorstVisit) =>
            //  {
            //      Application.Current.Dispatcher.Invoke(() =>
            //      {
            //          WorstVisit.Result.ForEach((Worstf) =>
            //          {
            //              WorstVisitors.Add(Worstf);
            //          });
            //      });
            //  });

            AddVisitorCommand = new RelayCommand(AddVisitor);
            EditVisitorCommand = new RelayCommand(EditVisitor);
            DeleteVisitorCommand = new RelayCommand(DeleteVisitor);
        }

        #region CRUD

        private void AddVisitor()
        {
            Visitor n = new Visitor
            {
                Name = SelectedVisitor.Name,
                Address = SelectedVisitor.Address,
                Email = SelectedVisitor.Email,
                PhoneNumber = SelectedVisitor.PhoneNumber
            };

            _apiClient
                .PostAsync(n, "http://localhost:18972/visitor")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Visitors.Add(n);
                    });
                });
        }

        private void EditVisitor()
        {
            _apiClient
                .PutAsync(SelectedVisitor, "http://localhost:18972/visitor")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        int i = SelectedVisitorIndex;
                        Visitor a = SelectedVisitor;
                        Visitors.Remove(SelectedVisitor);
                        Visitors.Insert(i, a);
                    });
                });
        }

        private void DeleteVisitor()
        {
            _apiClient
                .DeleteAsync(SelectedVisitor.Id, "http://localhost:18972/visitor")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Visitors.Remove(SelectedVisitor);
                    });
                });
        }

        #endregion CRUD

        #region NON-CRUD

        private void BestMovieMethod()
        {
            new BestVisitorWindow().Show();
        }

        private void WorstMovieMethod()
        {
            new WorstVisitorWindow().Show();
        }

        #endregion NON-CRUD
    }
}