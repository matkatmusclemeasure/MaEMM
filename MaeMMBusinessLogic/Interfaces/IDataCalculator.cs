using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaeMMBusinessLogic
{
    public interface IDataCalculator
    {
        event EventHandler<SendCoordinateEvent> sendCoordinate; 
    }
}
