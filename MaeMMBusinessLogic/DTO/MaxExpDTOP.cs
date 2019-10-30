using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaeMMBusinessLogic
{
    public class MaxExpDTOP
    {
        public MaxExpDTOP(double maxMuscle_, double expMuscle_)
        {
            maxMuscle = maxMuscle_;

            expMuscle = expMuscle_; 
        }

        public double maxMuscle { get; set; }

        public double expMuscle { get; set; }
    }
}
