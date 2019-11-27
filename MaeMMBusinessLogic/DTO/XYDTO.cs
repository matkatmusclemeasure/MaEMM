using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaeMMBusinessLogic
{
    public class XYDTO
    {
        public XYDTO(double X_, double Y_)
        {
            X = X_;
            Y = Y_; 
        }

        public double X { get; set; }
        public double Y { get; set; }
    }
}
