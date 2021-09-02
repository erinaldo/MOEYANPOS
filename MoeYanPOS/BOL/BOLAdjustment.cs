using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLAdjustment
    {
        private long iD;
        private DateTime adjDate;
        private int userID;
        private string itemCode;
        private int adjustmentTypeID;
        private int qty;
        private string remark;
        private long adjustmentID;
        private decimal price;
        private decimal amount;
        private string header;
        private long lID;

        public long LID
        {
            get { return lID; }
            set { lID = value; }
        }

        

        public string Header
        {
            get { return header; }
            set { header = value; }
        }

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }        

        public long AdjustmentID
        {
            get { return adjustmentID; }
            set { adjustmentID = value; }
        }

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }

        public int AdjustmentTypeID
        {
            get { return adjustmentTypeID; }
            set { adjustmentTypeID = value; }
        }

        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public DateTime AdjDate
        {
            get { return adjDate; }
            set { adjDate = value; }
        }

        public long ID
        {
            get { return iD; }
            set { iD = value; }
        }

        public BOLAdjustment()
        {
            UserID = AdjustmentTypeID = Qty =0;
            iD = adjustmentID =LID=  0;
            Remark = ItemCode = Header = "";
            adjDate = DateTime.Now;
            price = amount = 0;
        }
    }
}
