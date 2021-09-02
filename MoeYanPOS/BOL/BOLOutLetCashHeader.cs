using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLOutLetCashHeader
    {
        private int iD;
        private string header;
        private string type;
        private bool isDelete;

        public bool IsDelete
        {
            get { return isDelete; }
            set { isDelete = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Header
        {
            get { return header; }
            set { header = value; }
        }

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        public BOLOutLetCashHeader()
        {
            iD = 0;
            header = type = "";
            isDelete = false;
        }
    }
}
