using System;
using System.Collections.Concurrent;
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
        private List<XYDTO> muscleForce;
        public event EventHandler<SendCoordinateEvent> sendCoordinate;
        private IDataCalculator datacalculator;
        private double zeroPointValue =0;
        private List<double> zeroPointValues;
        private bool zeroPointAdjusting = false;

        public DataPresenter(IDataCalculator datacalc)
        {
            datacalculator = datacalc;
            
            datacalculator.sendCoordinate += sendCoordinates;
            muscleForce = new List<XYDTO>();
        }

        public void resetList()
        {
            muscleForce.Clear();
        }

        public void meassure(BlockingCollection<int> BC)
        {
           
            datacalculator.meassure(BC);
        }

        public void zeroPointAdjust(BlockingCollection<int> BC)
        {
            zeroPointAdjusting = true;
            zeroPointValues = new List<double>();

            datacalculator.meassure(BC);

        }

        public void getZeroPointAdjustment()
        {
            zeroPointAdjusting = false;

            double zeroPointSum = 0;

            foreach (var zeropoint in zeroPointValues)
            {
                zeroPointSum += zeropoint;
            }

            zeroPointValue = zeroPointSum / zeroPointValues.Count;
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
            if(zeroPointAdjusting == false)
            {
                muscleForce.Add(new XYDTO(e.x, e.y-zeroPointValue)); 
                SendCoordinateEvent SCevent = new SendCoordinateEvent(e.x, e.y-zeroPointValue);
                sendCoordinate?.Invoke(this, SCevent);
            }
            else if(zeroPointAdjusting == true)
            {
                if(zeroPointValues.Count == 300)
                {
                    getZeroPointAdjustment();
                }
                else
                {
                    zeroPointValues.Add(e.y);
                }
                
            }
            
        }

        public MaxExpDTOP showResult()
        {
            List<double> MaxList = new List<double>();
            List<XYDTO> expList = new List<XYDTO>();

            for(int i =0; i<muscleForce.Count;i++)
            {
                //if(MaxList.Count>=10)
                //{
                //    MaxList.RemoveAt(0);
                //    MaxList.Add(muscleForce[i].Y);

                //    if(MaxList.Average()>maxMuscle)
                //    {
                //        maxMuscle = MaxList.Average();
                //    }
                //}
                //else
                //{
                //    MaxList.Add(muscleForce[i].Y);
                //}

                if (maxMuscle < muscleForce[i].Y)
                {
                    maxMuscle = muscleForce[i].Y;
                }

                if(expList.Count>=50)
                {
                    expList.RemoveAt(0);
                    expList.Add(new XYDTO(muscleForce[i].X, muscleForce[i].Y));

                    double sumXY = 0;
                    double sumX = 0;
                    double sumXpower2 = 0;
                    double sumY = 0;
                    int count = 0;

                    foreach (var point in expList)
                    {
                        sumXY += point.X * point.Y;
                    
                    }

                    foreach (var point in expList)
                    {
                        sumX += point.X;
                    }

                    foreach (var point in expList)
                    {
                        sumY += point.Y;
                    }

                    foreach (var point in expList)
                    {
                        sumXpower2 += point.X*point.X;
                    }

                    double Slope = (sumXY-(sumX*sumY))/(sumXpower2-(sumX*sumX));

                    if(Slope>expMuscle)
                    {
                        expMuscle = Slope;
                    }
                }
                else
                {
                    expList.Add(new XYDTO(muscleForce[i].X, muscleForce[i].Y));
                }

                
            }


            //maxMuscle = muscleForce.Average();
            //expMuscle = muscleForce.Count();

            MaxExpDTOP MaxDTO = new MaxExpDTOP(maxMuscle, expMuscle);
            return MaxDTO;
        }

        public void setParameter(DataPCParameterDTO PDTO)
        {
            datacalculator.setParameter(PDTO);
        }
    }
}
