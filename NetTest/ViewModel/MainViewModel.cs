using System.Collections.Generic;
using GalaSoft.MvvmLight;
using NetTest.ViewModel.Helpers;

namespace NetTest.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public List<IViewGroup> Views { get; private set; }

        public MainViewModel()
        {
            Views = ViewGroup.ViewGroupFactory();
        }
    }
}