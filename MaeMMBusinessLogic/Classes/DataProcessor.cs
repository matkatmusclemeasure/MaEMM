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
        int count = 1;
        private List<int> testList = new List<int>();

        private ADC adConverter;

        public event EventHandler<SendDoubleEvent> sendDouble;
        private string strengthLevel= "Full strength";

        public DataProcessor()
        {

            adConverter = new ADC();
        }

        public void meassure()
        {
            procesVoltage(adConverter.readADC_Differential_0_1());

            //if (testList.Count < 6000)
            //{
            //    testList.Add(1);
            //}
            //else
            //{
            //    testList.Add(1);
            //}
            
            //procesVoltage(1000);
            //procesVoltage(count);
            //count++;
        }

        public void procesVoltage(double bit)
        {

            double voltage_ = (0.002*bit);

            //double voltage_ = voltage;

            double torque = 0;
            switch (strengthLevel)
            {
                case "Reduced strength":
                    torque = 15.32 * voltage_ + (Math.Pow(10, -12));
                    break;
                case "Medium strength":
                    torque = 28.16 * voltage_ + (2 * Math.Pow(10, -12));
                    break;
                case "Full strength":
                    torque = 41.68 * voltage_ + (Math.Pow(10, -12));
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
