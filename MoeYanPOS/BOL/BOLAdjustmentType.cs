using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLAdjustmentType
    {
        private int iD;
        private string adjustmentType;
        private int action;
        private string header;

        public string Header
        {
            get { return header; }
            set { header = value; }
        }

        public int Action
        {
            get { return action; }
            set { action = value; }
        }

        public string AdjustmentType
        {
            get { return adjustmentType; }
            set { adjustmentType = value; }
        }

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        public BOLAdjustmentType()
        {
            iD = action = 0;
            adjustmentType = header = "";
        }
    }
}
