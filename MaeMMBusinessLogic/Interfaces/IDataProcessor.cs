using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaeMMBusinessLogic
{
    public interface IDataProcessor
    {
        event EventHandler<SendDoubleEvent> sendDouble;

        void procesVoltage(double voltage);

        void meassure();

        void setParameter(DataPCParameterDTO PDTO);
      

    }
}
