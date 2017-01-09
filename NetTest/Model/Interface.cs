using System;

namespace NetTest.Model
{
    public class Interface : ModelBase
    {
        private string address;
        private string name;
        
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
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

    }
}