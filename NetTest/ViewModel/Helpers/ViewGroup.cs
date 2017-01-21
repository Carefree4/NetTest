using System.Collections.Generic;

namespace NetTest.ViewModel.Helpers
{
    public class ViewGroup : IViewGroup
    {
        private ViewGroup(string label)
        {
            Label = label;
            Groups = new List<IViewGroup>();
        }

        private ViewGroup(string label, string path) 
            : this(label)
        {
            Path = path;
        }

        public string Path { get; }
        public string Label { get; }
        public List<IViewGroup> Groups { get; }

        public enum GroupIndex
        {
            Icmp = 0
        }

        public static List<IViewGroup> ViewGroupFactory()
        {
            var viewGroup = new List<IViewGroup>
            {
                new ViewGroup("ICMP")
            };
            // Views
            // ICMP
            viewGroup[(int)GroupIndex.Icmp].Groups.Add(new ViewGroup("Continuous Ping", "ICMP/ContinuousPing.xaml"));
            viewGroup[(int)GroupIndex.Icmp].Groups.Add(new ViewGroup("Traceroot", "ICMP/Traceroot.xaml"));

            return viewGroup;
        }
    }
}