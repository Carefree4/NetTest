using System.Collections.Generic;

namespace NetTest.ViewModel.Helpers
{
    public class ViewGroup : IViewGroup
    {
        public ViewGroup(string label)
        {
            Label = label;
            Groups = new List<IViewGroup>();
        }

        public ViewGroup(string label, string path) 
            : this(label)
        {
            Path = path;
        }

        public string Path { get; }
        public string Label { get; }
        public List<IViewGroup> Groups { get; }

        public static List<IViewGroup> ViewGroupFactory()
        {
            var viewGroup = new List<IViewGroup>
            {
                new ViewGroup("ICMP")
            
            };
            // Views
            // ICMP
            viewGroup[0].Groups.Add(new ViewGroup("Continuous Ping", "ICMP/ContinuousPing.xaml"));

            return viewGroup;
        }
    }
}