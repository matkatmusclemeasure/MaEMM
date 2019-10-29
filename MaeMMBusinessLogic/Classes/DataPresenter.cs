using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaeMMBusinessLogic
{
    public class DataPresenter : IDataPresenter
    {
        public event EventHandler<SendCoordinateEvent> sendCoordinate;

        public void sendCoordinates()
        {
            
        }

        public MaxExpDTOP showResult()
        {
            
        }
    }
}
