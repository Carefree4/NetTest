using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NetTest.Annotations;
using NetTest.Services.ICMP;

namespace NetTest.ViewModel
{
    public class ContinuousPingViewModel : ViewModelBase
    {
        private PingSender pingSender;

        public ContinuousPingViewModel()
        {
            pingSender = new PingSender();
            PingResponces = new ObservableCollection<PingResponceViewModel>();

            pingSender.AllPingsReceived += (sender, args) => { UpdatePingResponces(args.PingResults); };
            PingResponces.CollectionChanged += (sender, args) => UpdatePingSenderList();

            TogglePings = new RelayCommand(() => { pingSender.Toggle(); });
        }

        public RelayCommand TogglePings { get; set; }

        public ObservableCollection<PingResponceViewModel> PingResponces { get; set; }

        private void UpdatePingSenderList()
        {
            // Refresh on each input just incase any data is deleted or edited
            // And prevent duplacates
            var s = PingResponces.Select(responce => responce.Address).ToList();
            pingSender.SetAddressList(s);
        }

        private void UpdatePingResponces(List<PingResult> pingResults)
        {
            foreach (var responce in PingResponces)
            {
                foreach (var result in pingResults)
                {
                    if (result.DestinationAddress.Equals(responce.Address))
                    {
                        responce.Status = result.Status;
                        responce.RoundTripTime = result.RoundtripTime.ToString();
                        responce.TimeToLive = result.TimeToLive.ToString();
                    }
                }
            }
        }

        public class PingResponceViewModel : INotifyPropertyChanged
        {
            private string address;
            private string interfaceName;
            private string roundTripTime;
            private string status;
            private string timeToLive;

            public string InterfaceName
            {
                get { return interfaceName; }
                set
                {
                    interfaceName = value;
                    OnPropertyChanged(nameof(InterfaceName));
                }
            }

            public string Address
            {
                get { return address; }
                set
                {
                    address = value;
                    OnPropertyChanged(nameof(Address));
                }
            }

            public string Status
            {
                get { return status; }
                set
                {
                    status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }

            public string RoundTripTime
            {
                get { return roundTripTime; }
                set
                {
                    roundTripTime = value;
                    OnPropertyChanged(nameof(RoundTripTime));
                }
            }

            public string TimeToLive
            {
                get { return timeToLive; }
                set
                {
                    timeToLive = value;
                    OnPropertyChanged(nameof(TimeToLive));
                }
            }


            public new string ToString()
                => $"\"{Status}\" from {InterfaceName}({Address}) in >={RoundTripTime} with {TimeToLive} hops left.";

            #region INotifyPropertyChanged Members

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            #endregion // INotifyPropertyChanged Members
        }
    }
}