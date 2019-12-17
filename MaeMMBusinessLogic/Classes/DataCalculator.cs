using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaeMMBusinessLogic
{
    public class DataCalculator : IDataCalculator
    {
        private double armLength;
        private IDataProcessor dataProcessor_;
        

        public event EventHandler<SendCoordinateEvent> sendCoordinate;

        
        public DataCalculator(IDataProcessor dataProcessor)
        {
            dataProcessor_ = dataProcessor;
            dataProcessor_.sendCoordinate += calculateForce; 
        }

        public void meassure(BlockingCollection<int> BC)
        {
            dataProcessor_.startMeasure(BC);
        }

        public void setParameter(DataPCParameterDTO PDTO)
        {
            armLength = PDTO.armLength;
            dataProcessor_.setParameter(PDTO);
        }

        public void calculateForce(object sender, SendCoordinateEvent e)
        {
            //Moment modtaget fra Processor, sendes via Event, regnes om til kraft og kan regnes tilbage til moment i den samlede arm (altså hele patientens armlængde) 

            double torque = e.y;

            double force = torque / (armLength - 0.05);

            double muscleTorque = force * armLength;

            //double muscleTorque = torque; //TEST, SKAL BRUGE DEN OVENFOR

            double timecount = e.x/1000;

            SendCoordinateEvent coordinateEvent = new SendCoordinateEvent(timecount, muscleTorque);
            sendCoordinate?.Invoke(this, coordinateEvent);


        }
    }
}
