using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class Buzzer: IBuzzer
    {
        private IOutput _output;
        public Buzzer(IOutput output)
        {
            _output = output;
        }
        public void CookingIsDone()
        {
            for (int i = 0; i < 3; ++i)
            {
                Thread.Sleep(200);
                _output.OutputLine("Beep Boop");
            }
        }
    }
}
