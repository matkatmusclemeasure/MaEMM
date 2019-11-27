using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaEMMDataAccessLogic
{
    class IncomingVoltageEvent : EventArgs
    {
        public double calculatedVoltage { get; set; }

        public IncomingVoltageEvent(double calculatedVoltage_)
        {
            calculatedVoltage = calculatedVoltage_; 
        }
    }
}
