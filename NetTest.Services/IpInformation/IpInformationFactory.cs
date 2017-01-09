using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTest.Services.IpInformation
{
    public enum IpInformationServices
    {
        DbIp
    }

    public class IpInformationFactory
    {
        

        public static IIpInformation GetIpInformationService(string apiKey, IpInformationServices service)
        {
            IIpInformation s;
            switch (service)
            {
                case IpInformationServices.DbIp:
                    s = new DbIpInformation(apiKey);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(service), service, null);
            }
            return s;
        }
    }
}
