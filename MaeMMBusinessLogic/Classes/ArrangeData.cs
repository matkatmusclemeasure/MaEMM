using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaeMMBusinessLogic
{
    class ArrangeData : IArrangeData
    {
        //private SaveData saveData;

        public void arrangeDataForSave(InformationDTO informationDTO)
        {
            string dataArranged = informationDTO.testTitle + ";" + informationDTO.patientName + ";" + informationDTO.personalID + ";" + informationDTO.testID + ";" + informationDTO.patientGender + ";" + informationDTO.dateOfMeasurement + ";" + informationDTO.armLength + ";" + informationDTO.strengthLevel + ";" + informationDTO.upperArmAngle + ";" + informationDTO.lowerArmAngle + ";" + informationDTO.specificComments + ";" + informationDTO.furtherComments;

            //saveData.saveDataString(dataArranged); 
        }

    }
}
