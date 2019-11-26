using System;
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

        void meassure();

        void setParameter(DataPCParameterDTO PDTO);
        

    }
}
