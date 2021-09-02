using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLMeasurement
    {
        private int id;
        private string measurement;
        private string mbcmeasurementid;

        public string MBCMeasurementID
        {
            get { return mbcmeasurementid; }
            set { mbcmeasurementid = value; }
        }

        public string Measurement
        {
            get { return measurement; }
            set { measurement = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

    }
}
