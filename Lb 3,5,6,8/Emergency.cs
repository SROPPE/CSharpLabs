using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB3_C_SHARP
{
    class Emergency
    {
        private List<Engine> engineRegistered;
        public Emergency()
        {
        }
        public void RegisterNewEngine(Engine engine)
        {
            engine.FuelIsOver += SendHelp;
            engineRegistered.Add(engine);
        }
        private void SendHelp(Object sender, FuelOverEventArgs e)
        {
            if (e.NeedHelp == true && e.Location!="")
                Console.WriteLine($"Выслан отряд спасателей. {e.Location}");
            
        }
        public void Unregister(Engine engine)
        {
            engine.FuelIsOver -= SendHelp;
        }

    }
}
