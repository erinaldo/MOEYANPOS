using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLPayForCredit    
    {
        private long iD;
        private DateTime date;
        private string customerID;
        private long cid;
        private string salevoucherno;
        private string voucherNo;
        private decimal amt;
        private decimal amount;
        private long locationID;
        private int userID;
        private string name;
        private string location;
        private string cashReceiveVoucherNo;
        private string crpaymenttype;

        public string CRPaymenttype
        {
            get { return crpaymenttype; }
            set { crpaymenttype = value; }
        }

        public string CashReceiveVoucherNo
        {
            get { return cashReceiveVoucherNo; }
            set { cashReceiveVoucherNo = value; }
        }

        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public long Cid
        {
            get { return cid; }
            set { cid = value; }
        }
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public long LocationID
        {
            get { return locationID; }
            set { locationID = value; }
        }

        public decimal Amt
        {
            get { return amt; }
            set { amt = value; }
        }

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public string SaleVoucher
        {
            get { return salevoucherno; }
            set { salevoucherno = value; }
        }

        public string VoucherNo
        {
            get { return voucherNo; }
            set { voucherNo = value; }
        }

        public string CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public long ID
        {
            get { return iD; }
            set { iD = value; }
        }

        public BOLPayForCredit()
        {
            iD=0;
            date=DateTime.Now;
            cid=0;
            voucherNo = customerID = Name = cashReceiveVoucherNo= "";
            userID = 0;
            amt=0;
            locationID=0;
            location = "";
            crpaymenttype = "";
        }
    }
}
