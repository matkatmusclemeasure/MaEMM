using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaeMMBusinessLogic
{
    interface IDataPresenter
    {
        event EventHandler<SendCoordinateEvent> sendCoordinate; 

        MaxExpDTOP showResult();

        void sendCoordinates();

    }
}
