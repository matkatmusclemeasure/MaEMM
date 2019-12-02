using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaEMMDataAccessLogic;

namespace MaeMMBusinessLogic
{
    public class ZeroPointAdjustment : IZeroPointAdjustment
    {
        IDataProcessor dataprocessor;
        ADC adc;
        List<double> zeroPointValues;
        public event EventHandler<SendDoubleEvent> sendDouble;

        public ZeroPointAdjustment()
        {
            dataprocessor = new DataProcessor();
            adc = new ADC();
            zeroPointValues = new List<double>();
            dataprocessor.sendDouble += zeroPointAdjust; 
        }

        public void zeroPointAdjust(object sender, SendDoubleEvent e)
        {
            int count = 100;
            double zeroPointValue = 0;
            double zeroPointSum = 0;

            for (int i = 0; i < count; i++)
            {
                zeroPointValue = e.forceInput;
                System.Threading.Thread.Sleep(50);
                zeroPointValues.Add(zeroPointValue);
            }

            foreach (var zeropoint in zeroPointValues)
            {
                zeroPointSum =+ zeropoint; 
            }

            double zeroPointAdjustmentValue = zeroPointSum / zeroPointValues.Count;

            SendDoubleEvent doubleEvent = new SendDoubleEvent(zeroPointAdjustmentValue);
            sendDouble?.Invoke(this, doubleEvent);
        }

    }
}
