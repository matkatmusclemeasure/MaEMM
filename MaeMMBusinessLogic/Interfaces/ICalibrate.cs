using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaeMMBusinessLogic
{
    interface ICalibrate
    {
        double getVoltage(int numberOfMeasurements);
    }
}
