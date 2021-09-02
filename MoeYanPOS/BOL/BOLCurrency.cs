using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLCurrency
    {
        private int id;
        private string currency;
        private decimal exchangerate;
        private string mbccurrencyid;

        public string MBCCurrencyID
        {
            get { return mbccurrencyid; }
            set { mbccurrencyid = value; }
        }

        public decimal Exchangerate
        {
            get { return exchangerate; }
            set { exchangerate = value; }
        }

        public string Currency
        {
            get { return currency; }
            set { currency = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public BOLCurrency()
        {
           id=0;
           currency = mbccurrencyid ="";
           exchangerate = 0;
        }
    }
}
