﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaEMMDataAccessLogic
{
    public interface ISaveData
    {
        //void saveDatastring(string dataArranged);

        void startSaving(string saveInformation_);

        void save(string saveInformation);

        string getLatestCalibration();
    }
}
