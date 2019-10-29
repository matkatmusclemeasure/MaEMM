using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaeMMBusinessLogic
{
    public class SendCoordinateEvent : EventArgs
    {
        public double x { get; set; }
        public double y { get; set; }

        public SendCoordinateEvent(double x_, double y_)
        {
            x = x_;
            y = y_; 
        }
    }
}
