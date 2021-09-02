using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLPettyCash
    {
        private long iD;
        private DateTime date;
        private long locationID;
        private decimal amount;
        private string remark;
        private int userID;
        private string location;
        private bool isGetAmt;
        private bool isPaidAmt;
        private string type;
        private string voucherNo;

        public string VoucherNo
        {
            get { return voucherNo; }
            set { voucherNo = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        

        public bool IsPaidAmt
        {
            get { return isPaidAmt; }
            set { isPaidAmt = value; }
        }

        public bool IsGetAmt
        {
            get { return isGetAmt; }
            set { isGetAmt = value; }
        }

        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        public long LocationID
        {
            get { return locationID; }
            set { locationID = value; }
        }
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
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

        public BOLPettyCash()
        {
            iD=0;
            date=DateTime.Now;
            locationID=0;
            amount=0;
            remark=location="";
            userID=0;
            isGetAmt = isPaidAmt = false;
            type = "";
        }
    }
}
