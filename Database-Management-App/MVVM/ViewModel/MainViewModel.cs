using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database_Management_App.Core;

namespace Database_Management_App.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand SearchViewCommand { get; set; }
        public RelayCommand AddViewCommand { get; set; }
        public RelayCommand UpdateViewCommand { get; set; }


        public HomeViewModel HomeVM { get; set; }

        public SearchViewModel SearchVM { get; set; }

        public AddViewModel AddVM { get; set; }
        public UpdateViewModel UpdateVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value;
                OnPropertyChanged();
            }
        }



        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            SearchVM = new SearchViewModel();
            AddVM = new AddViewModel();
            UpdateVM = new UpdateViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            SearchViewCommand = new RelayCommand(o =>
            {
                CurrentView = SearchVM;
            });

            AddViewCommand = new RelayCommand(o =>
            {
                CurrentView = AddVM;
            });

            UpdateViewCommand = new RelayCommand(o =>
            {
                CurrentView = UpdateVM;
            });

        }
    }
}
