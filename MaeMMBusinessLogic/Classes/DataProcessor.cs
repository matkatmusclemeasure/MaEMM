using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaEMMDataAccessLogic;
using System.Threading;
using System.Diagnostics;

namespace MaeMMBusinessLogic
{
    public class DataProcessor : IDataProcessor
    {
        //Finder vha. spænding momentet ved Strain Gauge ud fra de værdier der findes fra kalibrering 
        //Momentet sendes videre til calculator 
        int count = 1;
        private List<int> testList = new List<int>();
        private BlockingCollection<int> BC_;
        //private ADC adConverter;
        private Thread measureThread;

        public event EventHandler<SendCoordinateEvent> sendCoordinate;
        private string strengthLevel= "Full strength";

        public DataProcessor(BlockingCollection<int> BC)
        {
            BC_ = BC;
            //adConverter = new ADC();
        }

        public void startMeasure(BlockingCollection<int> BC)
        {
            BC_ = BC;
            measureThread = new Thread(this.procesVoltage);
            measureThread.IsBackground = true;

            measureThread.Start();
        }

        public void procesVoltage()
        {
            Stopwatch SW = new Stopwatch();
            SW.Start();

            while (!BC_.IsCompleted)
            {
                try
                {
                    int bit = BC_.Take();
                    double voltage_ = (0.002 * bit);

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

                    SendCoordinateEvent CoordinateEvent = new SendCoordinateEvent(SW.ElapsedMilliseconds, torque);
                    sendCoordinate?.Invoke(this, CoordinateEvent);
                }
                catch(InvalidOperationException e)
                {

                }
            }

            double timespan = SW.ElapsedMilliseconds;
            SW.Stop();
            
        }

        public void setParameter(DataPCParameterDTO PDTO)
        {
            strengthLevel = PDTO.strengthLevel;
        }

    }
}
