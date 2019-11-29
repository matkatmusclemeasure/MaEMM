using System;
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
        double timecount = 0;

        public event EventHandler<SendCoordinateEvent> sendCoordinate;

        
        public DataCalculator(IDataProcessor dataProcessor)
        {
            dataProcessor_ = dataProcessor;
            dataProcessor_.sendDouble += calculateForce; 
        }

        public void meassure()
        {
            dataProcessor_.meassure();
        }

        public void setParameter(DataPCParameterDTO PDTO)
        {
            armLength = PDTO.armLength;
            dataProcessor_.setParameter(PDTO);
        }

        public void calculateForce(object sender, SendDoubleEvent e)
        {
            //Moment modtaget fra Processor, sendes via Event, regnes om til kraft og kan regnes tilbage til moment i den samlede arm (altså hele patientens armlængde) 

            double torque = e.forceInput;

            //double force = torque / (armLength - 5);

            //double muscleTorque = force * armLength;

            double muscleTorque = torque; //TEST, SKAL BRUGE DEN OVENFOR

            timecount += 0.5;

            SendCoordinateEvent coordinateEvent = new SendCoordinateEvent(timecount, muscleTorque);
            sendCoordinate?.Invoke(this, coordinateEvent);


        }
    }
}
