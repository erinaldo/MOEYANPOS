using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLBankCheque
    {
        private int bankchequeid;
        private string bankchequename;
        private string myanmarname;
        private int locationid;
        private string locationname;

        public string MyanmarName
        {
            get { return myanmarname; }
            set { myanmarname = value; }
        }

        public int BankChequeID
        {
            get { return bankchequeid; }
            set { bankchequeid = value; }
        }

        public string BankChequeName
        {
            get { return bankchequename; }
            set { bankchequename = value; }
        }

        public int LocationID
        {
            get { return locationid; }
            set { locationid = value; }
        }
       

        public string LocationName
        {
            get { return locationname; }
            set { locationname = value; }
        }
        
        public BOLBankCheque()
        {
            BankChequeID = LocationID = 0;
            BankChequeName= LocationName = myanmarname = "";
        }
    }
}
