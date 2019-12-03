using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaEMMDataAccessLogic;

namespace MaeMMBusinessLogic
{
    public class CalibrateLogic : ICalibrate
    {
        private double voltageAverage;
        private double count;

        private List<double> momentPointsY;
        private List<double> voltagePointsX;

        private double moment;
        private double voltage;

        private double aSlope;
        private double bIntercept;

        private ISaveData saver;

        public CalibrateLogic()
        {
            momentPointsY = new List<double>();
            voltagePointsX = new List<double>();
            saver = new SaveData();
        }

        public double getVoltage(int numberOfMeasurements)
        {
            for (int i = 0; i < numberOfMeasurements; i++)
            {
                //dataReciever.start();
            }

            return voltageAverage;
        }

        public void enterCalibrationValues(double weight_, double armlength_)
        {
            moment = weight_ * armlength_;
            momentPointsY.Add(moment);

            //voltage = IncomingVoltage.somehow;
            voltagePointsX.Add(voltage);
        }

        public void startCalibration(string test)
        {
            saver.startSaving(test);
            //int n = momentPointsY.Count;
            //double sumXY = 0;
            //double sumX = 0;
            //double sumXpower2 = 0;
            //double sumY = 0;
            //int count = 0;

            //foreach (var point in voltagePointsX)
            //{
            //    sumXY = +(point * momentPointsY[count]);
            //    count++;
            //}

            //foreach (var point in voltagePointsX)
            //{
            //    sumX = +point;
            //}

            //foreach (var point in momentPointsY)
            //{
            //    sumY = +point;
            //}

            //foreach (var point in voltagePointsX)
            //{
            //    sumXpower2 = +(point * point);
            //}

            //aSlope = (n * sumXY - (sumX * sumY)) / (sumXpower2 - (sumX * sumX));
            //bIntercept = (1 / n) * (sumY - (aSlope * sumX));

            // Til sidst gemmes a og b, kalibreringsværdierne på sd kortet så disse kan læses ind hver gang
            // programmet åbnes.
        }

        public string getlatestTest()
        {
            return saver.getLatestCalibration();
        }
    }
}
