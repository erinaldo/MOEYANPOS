using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLTransition
    {
        private int id;
        private string transName;
        private long transID;

        public long TransID
        {
            get { return transID; }
            set { transID = value; }
        }
            
        public string TransName
        {
            get { return transName; }
            set { transName = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public BOLTransition()
        {
            id=0;
            transName="";
            transID=0;
        }
    }
}
