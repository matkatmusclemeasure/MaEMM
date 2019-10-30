using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaeMMBusinessLogic
{
    public class DataPCParameterDTO
    {
        public DataPCParameterDTO(double armLength_, string strengthLevel_)
        {
            armLength = armLength_;
            strengthLevel = strengthLevel_; 
        }

        public double armLength { get; set; }

        public string strengthLevel { get; set; }
    }
}
