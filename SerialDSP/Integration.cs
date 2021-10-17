using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialDSP
{
    public class Integration
    {
        private float _integrationIn = 0, _integrationOut = 0;
        private int _integrateCursor = 0;
        private int _integrateWindow = 16;

        public void Reset()
        {
            _integrationIn = _integrationOut = 0;
            _integrateCursor = 0;
        }
    }
}
