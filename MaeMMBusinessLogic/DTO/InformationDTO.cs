using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaeMMBusinessLogic
{
    public class InformationDTO
    {
        public InformationDTO(string testTitle_, string patientName_, string personalID_, string testID_, string patientGender_, string dateOfMeasurement_, string strengthLevel_)
        {
            testTitle = testTitle_;
            patientName = patientName_;
            personalID = personalID_;
            testID = testID_;
            patientGender = patientGender_;
            dateOfMeasurement = dateOfMeasurement_;
            strengthLevel = strengthLevel_;
            
        }

        public string testTitle { get; set; }

        public string patientName { get; set; }

        public string personalID { get; set; }

        public string testID { get; set; }

        public string patientGender { get; set; }

        public string dateOfMeasurement { get; set; }

        public string armLength { get; set; }

        public string strengthLevel { get; set; }

        public string maxMuscle { get; set; }

        public string expMuscle { get; set; }

        public string upperArmAngle { get; set; }

        public string lowerArmAngle { get; set; }

        public string specificComments { get; set; }

        public string furtherComments { get; set; }

        public double aSlope { get; set; }

        public double bIntercept { get; set; }

    }
}

