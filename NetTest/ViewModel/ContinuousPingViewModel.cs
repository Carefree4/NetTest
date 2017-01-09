using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NetTest.Model;
using NetTest.Services.ICMP;

namespace NetTest.ViewModel
{
    public class ContinuousPingViewModel : ViewModelBase
    {
        private readonly PingSender pingSender;

        public ContinuousPingViewModel()
        {
            pingSender = new PingSender();
            PingResponces = new ObservableCollection<ContinuousPing>();

            pingSender.AllPingsReceived += (sender, args) => { UpdatePingResponces(args.PingResults); };
            PingResponces.CollectionChanged += (sender, args) => UpdatePingSenderList();

            TogglePings = new RelayCommand(() => { pingSender.Toggle(); });
        }

        public RelayCommand TogglePings { get; set; }

        public ObservableCollection<ContinuousPing> PingResponces { get; set; }

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
    }
}