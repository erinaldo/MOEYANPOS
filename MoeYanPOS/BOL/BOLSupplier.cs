using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLSupplier
    {
        private int supplierid;
        private string supplierName;
        private string address;
        private string phone;
        private string email;
        private string township;
        private int townshipID;
        private decimal creditlimit;
        private bool iscredit;
        private bool iscash;


        public int TownshipID
        {
            get { return townshipID; }
            set { townshipID = value; }
        }
        public bool Iscash
        {
            get { return iscash; }
            set { iscash = value; }
        }

        public bool Iscredit
        {
            get { return iscredit; }
            set { iscredit = value; }
        }

        public decimal Creditlimit
        {
            get { return creditlimit; }
            set { creditlimit = value; }
        }

        public string Township
        {
            get { return township; }
            set { township = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        
        public string Address
        {
            get { return address; }
            set { address = value; }
        }     

        public string SupplierName
        {
            get { return supplierName; }
            set { supplierName = value; }
        }
        public int Supplierid
        {
            get { return supplierid; }
            set { supplierid = value; }
        }

        public BOLSupplier()
        {
           supplierid=0;
        supplierName="";
        address="";
        phone="";
        email="";
        township="";
        townshipID=0;
        creditlimit=0;
         iscredit=false;
        iscash=false;
        }

    }
}
