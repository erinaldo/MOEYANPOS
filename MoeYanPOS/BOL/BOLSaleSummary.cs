using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLSaleSummary
    {
        private string categoryid;
        private string category;
        private string classname;
        private DateTime date;
        private decimal discount;
        private string paymenttype;
        private decimal grandtotal;

        public decimal Grandtotal
        {
            get { return grandtotal; }
            set { grandtotal = value; }
        }

        public string Paymenttype
        {
            get { return paymenttype; }
            set { paymenttype = value; }
        }

        public decimal Discount
        {
            get { return discount; }
            set { discount = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public string Classname
        {
            get { return classname; }
            set { classname = value; }
        }

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        public string Categoryid
        {
            get { return categoryid; }
            set { categoryid = value; }
        }
                
    }
}
