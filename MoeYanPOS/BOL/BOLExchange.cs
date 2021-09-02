using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLExchange
    {
        private int id;
        private int currency;
        private int exchangerate;
        private string currencyname;

        public string Currencyname
        {
            get { return currencyname; }
            set { currencyname = value; }
        }

        public int Exchangerate
        {
            get { return exchangerate; }
            set { exchangerate = value; }
        }

        public int Currency
        {
            get { return currency; }
            set { currency = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        
        public BOLExchange()
        {
          id=currency=exchangerate=0;        
          currencyname="";
        }
    }
}
