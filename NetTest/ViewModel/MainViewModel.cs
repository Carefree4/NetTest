using System.Collections.Generic;
using System.ComponentModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NetTest.ViewModel.Helpers;

namespace NetTest.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Views = ViewGroup.ViewGroupFactory();
            CurrentPageSource = Views[(int) ViewGroup.GroupIndex.Icmp].Groups[0].Path;
        }

        public List<IViewGroup> Views { get; }

        private string currentPageSource;

        public string CurrentPageSource
    {
            get { return currentPageSource; }
            set
            {
                currentPageSource = value;
                RaisePropertyChanged(nameof(CurrentPageSource));
            }
        }


        public RelayCommand<IViewGroup> NavigateToPage { get; set; }
    }
}