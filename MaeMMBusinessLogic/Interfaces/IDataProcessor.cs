﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaeMMBusinessLogic
{
    public interface IDataProcessor
    {
        event EventHandler<SendCoordinateEvent> sendCoordinate;

        void procesVoltage();

        void startMeasure(BlockingCollection<int> BC);

        void setParameter(DataPCParameterDTO PDTO);
      

    }
}
