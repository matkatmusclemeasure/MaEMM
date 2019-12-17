using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaeMMBusinessLogic
{
    public interface IDataCalculator
    {
        event EventHandler<SendCoordinateEvent> sendCoordinate;
        void meassure(BlockingCollection<int> BC);

        void setParameter(DataPCParameterDTO PDTO);

        void calculateForce(object sender, SendCoordinateEvent e);
        
    }
}
