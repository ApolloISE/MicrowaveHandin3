using System;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class PowerTube : IPowerTube
    {
        private IOutput myOutput;
        private bool IsOn = false;
        private int _maxPower;
        public int MaxPower {
            get => _maxPower;
            set {
                if (value >= 50 && value <= 1200)
                {
                    _maxPower = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("MaxPower", MaxPower, "Must be between 50 and 1200 (incl.)");
                }
            }
        }

        public PowerTube(IOutput output, int maxPower)
        {
            myOutput = output;
            MaxPower = maxPower;
        }

        public void TurnOn(int power)
        {
            if (power < 1 || MaxPower < power)
            {
                throw new ArgumentOutOfRangeException("power", power, "Must be between 1 and 1200 (incl.)");
            }

            if (IsOn)
            {
                throw new ApplicationException("PowerTube.TurnOn: is already on");
            }

            myOutput.OutputLine($"PowerTube works with {power}");
            IsOn = true;
        }

        public void TurnOff()
        {
            if (IsOn)
            {
                myOutput.OutputLine($"PowerTube turned off");
            }

            IsOn = false;
        }
    }
}