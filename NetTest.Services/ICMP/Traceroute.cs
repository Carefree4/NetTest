using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;

namespace NetTest.Services.ICMP
{
    public class Traceroute
    {
        private const int Timeout = 2500;
        private string address;
        private int maxHops;

        public Traceroute(string address, int maxHops = 30)
        {
            maxHops = maxHops < 1 || maxHops > 100 ? maxHops : 30;
            this.maxHops = maxHops;

            this.address = Dns.GetHostAddresses(address)[0].ToString();
        }

        public List<PingResult> GetRoute(int nextHop = 1)
        {
            var result = GetPingResult(nextHop);

            List<PingResult> results;
            if (result.DestinationAddress.Equals(result.SourceAddress))
            {
                results = new List<PingResult>
                {
                    result
                };
            }
            else if (nextHop >= maxHops)
            {
                results = new List<PingResult>
                {
                    FailedPingResultFactory("Route exceded maximum hop count of " + maxHops)
                };
            }
            else
            {
                results = GetRoute(++nextHop);
                results.Add(result);
            }

            return results;
        }

        private PingResult GetPingResult(int nextHop)
        {
            PingResult result;
            using (var ping = new Ping())
            {
                var options = PingOptionsFactory(nextHop);
                try
                {
                    var p = ping.Send(address, Timeout, new byte[32], options);
                    result = p != null
                        ? PingResultFactory(nextHop, p)
                        : FailedPingResultFactory("Unable to reach " + address);
                }
                catch (PingException e)
                {
                    result = FailedPingResultFactory(e.Message);
                }
            }
            return result;
        }

        private PingResult FailedPingResultFactory(string e)
        {
            return new PingResult
            {
                SourceAddress = "0.0.0.0",
                RoundtripTime = 0,
                Status = e,
                DestinationAddress = address,
                TimeToLive = 0
            };
        }

        private PingResult PingResultFactory(int nextHop, PingReply p)
        {
            var result = new PingResult();

            if (p != null)
            {
                result.SourceAddress = p.Address?.ToString();
                result.DestinationAddress = address;
                result.Status = p.Status.ToString();
                result.RoundtripTime = p.RoundtripTime;
                result.TimeToLive = nextHop;
            }
            return result;
        }

        private static PingOptions PingOptionsFactory(int nextHop)
        {
            var options = new PingOptions
            {
                DontFragment = true,
                Ttl = nextHop
            };
            return options;
        }
    }
}