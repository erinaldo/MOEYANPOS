using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLCustomer
    {
        private long iD;
        private string customerID;
        private string name;
        private string myanmarname;
        private string memberid;
        private string address;
        private DateTime dateofbirth;
        private DateTime joindate;
        private DateTime currentdate;        
        private string nrc;       
        private string phone;
        private string email;
        private string customerlevel;
        private int township;
        private int divisionid;        
        private decimal creditlimit;
        private bool iscredit;
        private bool iscash;
        private string shop;
        private int sortingID;
        private decimal depositAmount;
        private int departmentid;      
        private string contactperson;
        private decimal saletarget;
        private string division;
        private string departmentName;
        private bool wholesaleprice;
        private string townshipName;
        private decimal creditOpeningAmt;
        private decimal grandtotal;
        private decimal paidcreditamount;
        private decimal creditamount;

        public decimal CreditAmount
        {
            get { return creditamount; }
            set { creditamount = value; }
        }

        public decimal PaidCreditAmount
        {
            get { return paidcreditamount; }
            set { paidcreditamount = value; }
        }

        public decimal GrandTotal
        {
            get { return grandtotal; }
            set { grandtotal = value; }
        }

        public decimal CreditOpeningAmt
        {
            get { return creditOpeningAmt; }
            set { creditOpeningAmt = value; }
        }

        public string TownshipName
        {
            get { return townshipName; }
            set { townshipName = value; }
        }

        public long ID
        {
            get { return iD; }
            set { iD = value; }
        }

        public bool Wholesaleprice
        {
            get { return wholesaleprice; }
            set { wholesaleprice = value; }
        }
        private bool retailsaleprice;

        public bool Retailsaleprice
        {
            get { return retailsaleprice; }
            set { retailsaleprice = value; }
        }

        public DateTime Currentdate
        {
            get { return currentdate; }
            set { currentdate = value; }
        }
        
        public string DepartmentName
        {
            get { return departmentName; }
            set { departmentName = value; }
        }

        public string Division
        {
            get { return division; }
            set { division = value; }
        }

        public decimal Saletarget
        {
            get { return saletarget; }
            set { saletarget = value; }
        }

        public string Contactperson
        {
            get { return contactperson; }
            set { contactperson = value; }
        }

       
        public decimal DepositAmount
        {
            get { return depositAmount; }
            set { depositAmount = value; }
        }
        public int Departmentid
        {
            get { return departmentid; }
            set { departmentid = value; }
        }
        public string Nrc
        {
            get { return nrc; }
            set { nrc = value; }
        }

        public DateTime Joindate
        {
            get { return joindate; }
            set { joindate = value; }
        }

        public DateTime Dateofbirth
        {
            get { return dateofbirth; }
            set { dateofbirth = value; }
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

        public int SortingID
        {
            get { return sortingID; }
            set { sortingID = value; }
        }

        public string CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        public string Shop
        {
            get { return shop; }
            set { shop = value; }
        }

        public decimal Creditlimit
        {
            get { return creditlimit; }
            set { creditlimit = value; }
        }

        public int Divisionid
        {
            get { return divisionid; }
            set { divisionid = value; }
        }

        public int Township
        {
            get { return township; }
            set { township = value; }
        }

        public string Customerlevel
        {
            get { return customerlevel; }
            set { customerlevel = value; }
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

        public string Memberid
        {
            get { return memberid; }
            set { memberid = value; }
        }

        public string MyanmarName
        {
            get { return myanmarname; }
            set { myanmarname = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public BOLCustomer()
        {
            ID = 0;
            name = memberid = address = phone = email = customerlevel = customerID= townshipName= myanmarname= "";
            creditlimit = creditOpeningAmt= saletarget= depositAmount= 0;
            sortingID = divisionid = township = 0;
            iscash = iscredit =wholesaleprice=retailsaleprice= false;
            joindate=dateofbirth=currentdate=DateTime.Today.Date;
        }
    }
}
