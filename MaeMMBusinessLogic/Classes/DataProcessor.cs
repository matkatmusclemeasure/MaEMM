using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaEMMDataAccessLogic;


namespace MaeMMBusinessLogic
{
    public class DataProcessor : IDataProcessor
    {
        //Finder vha. spænding momentet ved Strain Gauge ud fra de værdier der findes fra kalibrering 
        //Momentet sendes videre til calculator 

        private ADC adConverter;

        public event EventHandler<SendDoubleEvent> sendDouble;
        private string strengthLevel= "Full strength";

        public DataProcessor()
        {
            adConverter = new ADC();
        }

        public void meassure()
        {
            procesVoltage(1);
        }
        
        public void procesVoltage(double voltage)
        {
            double voltage_ = voltage;
            double torque =0;
            switch(strengthLevel)
            {
                case "Reduced strength":
                    torque = 13784 * voltage_ + (2 * Math.Pow(10, -8));
                    break;
                case "Medium strength":
                    torque = 25332 * voltage_ + (2 * Math.Pow(10, -8));
                    break;
                case "Full strength":
                    torque = 37491 * voltage_ + (2 * Math.Pow(10, -8));
                    break;

            }

            SendDoubleEvent doubleEvent = new SendDoubleEvent(torque);
            sendDouble?.Invoke(this, doubleEvent);
        }

        public void setParameter(DataPCParameterDTO PDTO)
        {
            strengthLevel = PDTO.strengthLevel;
        }

    }
}
