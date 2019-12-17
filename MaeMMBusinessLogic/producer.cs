using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaEMMDataAccessLogic;
using System.Threading;

namespace MaeMMBusinessLogic
{
    public class producer : IProducer
    {
        private BlockingCollection<int> BC_;
        private ADC adConverter;
        private bool measuring = false;
        private Thread measureThread;
        private int count = 0;

        public producer(BlockingCollection<int> BC)
        {
            BC_ = BC;
            adConverter = new ADC();
        }

        public void startMeasure(BlockingCollection<int> BC)
        {
            BC_ = BC;
            measureThread = new Thread(this.measure);
            measureThread.IsBackground = true;
            
            measureThread.Start();
        }

        public void measure()
        {
            measuring = true;

            while (measuring == true)
            {
                //BC_.Add(adConverter.readADC_Differential_0_1());
                BC_.Add(1000);
                Thread.Sleep(1);
                count++;
               
            }

            BC_.CompleteAdding();
            
            //procesVoltage(adConverter.readADC_Differential_0_1());

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

        public void stopMeasure()
        {
            measuring = false;
        }

        public void zeroPointAdjust(BlockingCollection<int> BC)
        {
            BC_ = BC;


            for(int i =0;i<=300;i++)
            {
                //BC_.Add(adConverter.readADC_Differential_0_1());
                BC_.Add(1000);
            }
            BC_.CompleteAdding();
        }
    }
}
