namespace NetTest.Services.ICMP
{
    public class PingResult
    {
        /// <summary>
        /// The address attempted to reach
        /// </summary>
        public string DestinationAddress { get; set; }

        /// <summary>
        /// The address that responded to the ICMP packet
        /// </summary>
        public string SourceAddress { get; set; }

        /// <summary>
        /// See Ping.Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// See Ping.RoundtripTime
        /// </summary>
        public long RoundtripTime { get; set; }

        public int TimeToLive { get; set; }
    }
}