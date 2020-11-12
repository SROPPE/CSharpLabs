using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB3_C_SHARP
{
    public class FuelOverEventArgs : EventArgs
    {
        public FuelOverEventArgs(Func<string> GetLocationFrom, bool needHelp)
        {
            Location = GetLocationFrom?.Invoke();
            this.NeedHelp = needHelp;
        }
        public string Location { get; }
        public bool NeedHelp { get; }
    }
}
