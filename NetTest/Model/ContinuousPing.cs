using System.ComponentModel;
using System.Runtime.CompilerServices;
using NetTest.Annotations;

namespace NetTest.Model
{
    public class ContinuousPing : ModelBase
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
    }
}