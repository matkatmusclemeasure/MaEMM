﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaeMMBusinessLogic
{
    public class SendDoubleEvent : EventArgs
    {
        public double forceInput { get; set; }

        public SendDoubleEvent(double forceInput_)
        {
            forceInput = forceInput_; 
        }
    }
}