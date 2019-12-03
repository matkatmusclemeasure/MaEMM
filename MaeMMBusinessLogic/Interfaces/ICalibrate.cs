using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaeMMBusinessLogic
{
    public interface ICalibrate
    {
        double getVoltage(int numberOfMeasurements);

        void enterCalibrationValues(double weight_, double armlength_);

        void startCalibration(string test);

        string getlatestTest();
    }
}
