using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTest.ViewModel.Helpers
{
    public interface IViewGroup
    {
        string Path { get; }
        string Label { get; }
        List<IViewGroup> Groups { get; }
    }
}
