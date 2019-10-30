using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaEMM
{
    public class InformationDTO
    {
        public InformationDTO(string testTitle_, string patientName_, string personalID_, string testID_, string patientGender_, string dateOfMeasurement_)
        {
            testTitle = testTitle_;
            patientName = patientName_;
            personalID = personalID_;
            testID = testID_;
            patientGender = patientGender_;
            dateOfMeasurement = dateOfMeasurement_; 
        }

        public string testTitle { get; set; }

        public string patientName { get; set; }

        public string personalID { get; set; }

        public string testID { get; set; }

        public string patientGender { get; set; }

        public string dateOfMeasurement { get; set; }
    }
}
