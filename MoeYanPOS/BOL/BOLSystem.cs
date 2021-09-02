using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLSystem
    {
        private int companyid;
        private string companyname;
        private string address;
        private string phno;
        private string web;
        private string email;
        private string printerslip;
        private string printervoucher;
        private string fax;

        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }

        public string Printervoucher
        {
            get { return printervoucher; }
            set { printervoucher = value; }
        }

        public string Printerslip
        {
            get { return printerslip; }
            set { printerslip = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Web
        {
            get { return web; }
            set { web = value; }
        }

        public string Phno
        {
            get { return phno; }
            set { phno = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Companyname
        {
            get { return companyname; }
            set { companyname = value; }
        }

        public int Companyid
        {
            get { return companyid; }
            set { companyid = value; }
        }
        public BOLSystem()
        {
            companyid = 0;
            companyname = address = phno = web = email = printerslip = printervoucher = "";
        }
    }
}
