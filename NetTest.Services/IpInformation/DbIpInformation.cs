using System.Net;
using GoogleMaps.LocationServices;
using Newtonsoft.Json;

namespace NetTest.Services.IpInformation
{
    public class DbIpInformation : IIpInformation
    {
        private string apiKey;

        public DbIpInformation(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public IpLocation GetIpLocation(string address)
        {
            // TODO: add dns lookup
            var apiUrl = $"http://api.db-ip.com/v2/{apiKey}/{address}";

            string json;
            using (var web = new WebClient())
            {
                json = web.DownloadString(apiUrl);
            }

            var jsonObj = JsonConvert.DeserializeObject<IpLocation>(json);

            // TODO: Safety!!
            jsonObj = PopulateLatitudeLongitude(jsonObj);
            return jsonObj;
        }

        private IpLocation PopulateLatitudeLongitude(IpLocation location)
        {
            var address = $"{location.City}, {location.StateProv}, {location.CountryCode}";

            var locationService = new GoogleLocationService();
            var point = locationService.GetLatLongFromAddress(address);

            point = point ?? new MapPoint
            {
                Longitude = 0,
                Latitude = 0
            };

            return new IpLocation
            {
                IpAddress = location.IpAddress ?? "0.0.0.0",
                CountryCode = location.CountryCode ?? "ZZ",
                StateProv = location.StateProv ?? "ZZ",
                City = location.City ?? "ZZ",
                Latitude = point.Latitude,
                Longitude = point.Longitude
            };
        }
    }
}