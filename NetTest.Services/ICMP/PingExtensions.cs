using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace NetTest.Services.ICMP
{
    public static class PingExtensions
    {
        /**
         * Extension method Curtosy of SO user L.B.
         * http://stackoverflow.com/a/25534776
         **/
        public static Task<PingResult> SendTaskAsync(this Ping ping, string address)
        {
            var tcs = new TaskCompletionSource<PingResult>();
            PingCompletedEventHandler response = null;
            response = (s, e) =>
            {
                ping.PingCompleted -= response;
                if (e.Reply != null)
                {
                    tcs.SetResult(new PingResult()
                    {
                        DestinationAddress = address,
                        SourceAddress = e.Reply?.Address.ToString(),
                        Status = e.Reply?.Status.ToString(),
                        RoundtripTime = (long)e.Reply?.RoundtripTime,
                        TimeToLive = e.Reply.Options?.Ttl ?? -1
                    });
                }
                else
                {
                    tcs.SetResult(new PingResult()
                    {
                        DestinationAddress = address,
                        SourceAddress = "0.0.0.0",
                        Status = "INVALID",
                        RoundtripTime = -1,
                        TimeToLive = -1
                    });
                }
            };
            ping.PingCompleted += response;

            try
            {
                ping.SendAsync(address, address);
            }
            catch (Exception)
            {
                // ignored
            }

            return tcs.Task;
        }
    }
}