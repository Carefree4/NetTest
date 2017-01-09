using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Timers;

namespace NetTest.Services.ICMP
{
    public class PingEventArgs : EventArgs
    {
        public List<PingResult> PingResults { get; set; }
    }

    public class PingSender
    {
        private List<string> addresses;
        private Timer timer;

        public PingSender(int interval = 2000)
        {
            addresses = new List<string>();

            timer = new Timer();
            timer.Elapsed += SendPingsAsync;
            timer.Interval = interval;
        }

        private async void SendPingsAsync(object source, ElapsedEventArgs e)
        {
            var pingTasks = addresses.Select(ip =>
            {
                using (var ping = new Ping())
                {
                    return ping.SendTaskAsync(ip);
                }
            }).ToList();

            await Task.WhenAll(pingTasks);

            var pingReplies = pingTasks.Select(reply => reply.Result).ToList();

            OnAllPingsReceived(pingReplies);
        }

        #region Timer control

        public void Start() => timer.Enabled = true;
        public void Stop() => timer.Enabled = false;
        public void Toggle() => timer.Enabled = !timer.Enabled;

        #endregion // Timer control

        #region CRUD

        public List<string> GetAddressList()
        {
            return addresses;
        }

        public void SetAddressList(List<string> newAddresses)
        {
            addresses = new List<string>(newAddresses.Where(s => s != null));
        }

     // public void AddAddress(string ipString) =>
     //     addresses?.Add(ipString);
     //
     // public void RemoveAddress(List<string> ipStrings) =>
     //     addresses?.RemoveAll(ip => ipStrings.Any(s => s.Equals(ip)));

        #endregion // CRUD

        #region Pings Recieved Event

        public event EventHandler<PingEventArgs> AllPingsReceived;

        protected virtual void OnAllPingsReceived(List<PingResult> pingResults)
        {
            AllPingsReceived?.Invoke(this, new PingEventArgs {PingResults = pingResults});
        }

        #endregion // Pings Recieved Event
    }
}