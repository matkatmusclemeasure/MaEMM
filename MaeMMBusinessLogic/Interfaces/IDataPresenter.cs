using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaeMMBusinessLogic
{
    public interface IDataPresenter
    {
        event EventHandler<SendCoordinateEvent> sendCoordinate; 

        MaxExpDTOP showResult();

        void sendCoordinates(object sender, SendCoordinateEvent e);

        void meassure(BlockingCollection<int> BC);

        void setParameter(DataPCParameterDTO PDTO);

        void resetList();

        void zeroPointAdjust(BlockingCollection<int> BC);

        void getZeroPointAdjustment();

    }
}
