using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLStockTransfer
    {
        private long iD;
        private DateTime date;
        private int userID;
        private string itemCode;
        private int qty;
        private string remark;
        private long transferID;
        private decimal price;
        private decimal amount;
        private long lID;
        private long toLID;
        private string voucherNo;
        private int times;

        public int Times
        {
            get { return times; }
            set { times = value; }
        }

        public string VoucherNo
        {
            get { return voucherNo; }
            set { voucherNo = value; }
        }

        public long ToLID
        {
            get { return toLID; }
            set { toLID = value; }
        }

        public long LID
        {
            get { return lID; }
            set { lID = value; }
        }

        public long TransferID
        {
            get { return transferID; }
            set { transferID = value; }
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

        public BOLStockTransfer()
        {
            UserID = Qty =0;
            iD = transferID =LID= toLID = TransferID= 0;
            Remark = ItemCode = VoucherNo = "";
            date = DateTime.Now;
            price = amount = Times = 0;
        }
    }
}
