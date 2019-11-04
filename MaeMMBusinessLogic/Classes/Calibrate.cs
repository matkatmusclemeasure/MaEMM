using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaeMMBusinessLogic
{
    public class Calibrate : ICalibrate
    {
        private double voltageAverage;
        private double count;
        private double voltage;
        
        public double getVoltage(int numberOfMeasurements)
        {
            for(int i = 0; i<numberOfMeasurements; i++)
            {
                //dataReciever.start();
            }

            return voltageAverage;
        }
    }
}
