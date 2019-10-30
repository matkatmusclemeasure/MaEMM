using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaeMMBusinessLogic
{
    public class DataPresenter : IDataPresenter
    {
        private double maxMuscle = 0;
        private double expMuscle = 0;
        public event EventHandler<SendCoordinateEvent> sendCoordinate;

        public DataPresenter()
        {

        }

        public void sendCoordinates(object sender, SendCoordinateEvent e)
        {
            SendCoordinateEvent SCevent = new SendCoordinateEvent(e.x, e.y);
            sendCoordinate?.Invoke(this, SCevent);
        }

        public MaxExpDTOP showResult()
        {
            MaxExpDTOP MaxDTO = new MaxExpDTOP(maxMuscle, expMuscle);
            return MaxDTO;
        }

        public void setParameter(DataPCParameterDTO PDTO)
        {

        }
    }
}
