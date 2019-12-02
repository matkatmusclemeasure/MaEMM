using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaeMMBusinessLogic
{
    public interface IZeroPointAdjustment
    {
        event EventHandler<SendDoubleEvent> sendDouble;
        void zeroPointAdjust(object sender, SendDoubleEvent e);
    }
}
