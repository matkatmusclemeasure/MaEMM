using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaeMMBusinessLogic
{
    public class DataProcessor : IDataProcessor
    {
        //Finder vha. spænding momentet ved Strain Gauge ud fra de værdier der findes fra kalibrering 
        //Momentet sendes videre til calculator 

        public event EventHandler<SendDoubleEvent> sendDouble;

        public DataProcessor()
        {

        }
        
        public void procesVoltage(object sender, SendDoubleEvent e)
        {
            double torque = 0;

            SendDoubleEvent doubleEvent = new SendDoubleEvent(torque);
            sendDouble?.Invoke(this, doubleEvent);
        }


    }
}
