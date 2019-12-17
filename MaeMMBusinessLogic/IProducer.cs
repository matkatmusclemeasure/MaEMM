using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaeMMBusinessLogic
{
    public interface IProducer
    {
        void measure();

        void stopMeasure();

        void startMeasure(BlockingCollection<int> BC);

        void zeroPointAdjust(BlockingCollection<int> BC);
    }
}
