using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaeMMBusinessLogic
{
    class DataCalculator : IDataCalculator
    {
        private double armLength;
        private IDataProcessor dataProcessor_; 

        public DataCalculator(IDataProcessor dataProcessor)
        {
            dataProcessor_ = dataProcessor;
            dataProcessor_.calculateDataEvent += calculateForce; 
        }

        public void setArmLength(double length)
        {
            armLength = length; 
        }

        public void calculateForce(object sender, EventArgs e)
        {
            //Moment modtaget fra Processor, sendes via Event, regnes om til kraft og kan regnes tilbage til moment i den samlede arm (altså hele patientens armlængde) 
            double force = torque / (armLength - 5);

            double muscleTorque = force * armLength; 


        }
    }
}
