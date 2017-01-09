namespace NetTest.Services.IpInformation
{
    public class IpLocation
    {
        public string IpAddress { get; set; }
        public string CountryCode { get; set; }
        public string StateProv { get; set; }
        public string City { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}