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
        private List<double> muscleForce;
        public event EventHandler<SendCoordinateEvent> sendCoordinate;
        private IDataCalculator datacalculator;

        public DataPresenter(IDataCalculator datacalc)
        {
            datacalculator = datacalc;
            datacalculator.sendCoordinate += sendCoordinates;
            muscleForce = new List<double>();
        }

        public void meassure()
        {
           
            datacalculator.meassure();
        }

        public void sendCoordinates(object sender, SendCoordinateEvent e)
        {
            //if(e.y > maxMuscle) //Måske dette skal ændres til et gennemsnit af flere, så man tager højde for støj
            //{
            //    maxMuscle = e.y;
            //}

            //if(muscleForce.Count<11)
            //{
            //    muscleForce.Add(e.y);
            //}
            //else
            //{
            //    //Remove one from list and add a new one, then calculate slope
            //    //expMuscle = slope
            //}
            ////Ovenstående bør måske overvejes at skulle gøres i en anden selvstændig tråd, snak med Michael
            
            muscleForce.Add(e.y); //TESTER brug ovenstående rigtigt
            SendCoordinateEvent SCevent = new SendCoordinateEvent(e.x, e.y);
            sendCoordinate?.Invoke(this, SCevent);
        }

        public MaxExpDTOP showResult()
        {
            maxMuscle = muscleForce.Average();
            expMuscle = muscleForce.Count();

            MaxExpDTOP MaxDTO = new MaxExpDTOP(maxMuscle, expMuscle);
            return MaxDTO;
        }

        public void setParameter(DataPCParameterDTO PDTO)
        {
            datacalculator.setParameter(PDTO);
        }
    }
}
