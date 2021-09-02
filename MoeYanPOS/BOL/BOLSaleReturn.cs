using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class 
        BOLSaleReturn
    {
        private long salereturnid;
        private long transalereturnid;
        private DateTime date;
        private string customerid;
        private string voucherno;
        private string paymenttype;
        private int currencyid;
        private int daylimit;
        private decimal exchangerate;        
        private decimal totalamt;
        private int userid;
        private string systemvoucherno;
        private long salereturndetailid;
        private string itemcode;
        private string description;
        private string type;
        private int qty;
        private decimal saleprice;
        private decimal charge;
        private decimal total;
        private DateTime lotterydate;
        private string lotteryno;
        private long drawingtimes;
        private string customername;
        private int originaluserid;
        private int edituserid;
        private DateTime editsaleretundate;
        private long locationid;
        private string location;
        private string currency;
        private string saleSystemVoucherNo;
        private string username;
        private long cid;
        private string originalVoucherNo;

        public string OriginalVoucherNo
        {
            get { return originalVoucherNo; }
            set { originalVoucherNo = value; }
        }

        public long Cid
        {
            get { return cid; }
            set { cid = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string SaleSystemVoucherNo
        {
            get { return saleSystemVoucherNo; }
            set { saleSystemVoucherNo = value; }
        }

        public string Currency
        {
            get { return currency; }
            set { currency = value; }
        }

        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        public long Locationid
        {
            get { return locationid; }
            set { locationid = value; }
        }

        public DateTime Editsaleretundate
        {
            get { return editsaleretundate; }
            set { editsaleretundate = value; }
        }

        public int Edituserid
        {
            get { return edituserid; }
            set { edituserid = value; }
        }

        public int Originaluserid
        {
            get { return originaluserid; }
            set { originaluserid = value; }
        }

        public string Customername
        {
            get { return customername; }
            set { customername = value; }
        }

        public long Drawingtimes
        {
            get { return drawingtimes; }
            set { drawingtimes = value; }
        }

        public string Lotteryno
        {
            get { return lotteryno; }
            set { lotteryno = value; }
        }

        public DateTime Lotterydate
        {
            get { return lotterydate; }
            set { lotterydate = value; }
        }

        public decimal Exchangerate
        {
            get { return exchangerate; }
            set { exchangerate = value; }
        }
        
        public decimal Total
        {
            get { return total; }
            set { total = value; }
        }

        public decimal Saleprice
        {
            get { return saleprice; }
            set { saleprice = value; }
        }

        public decimal Charge
        {
            get { return charge; }
            set { charge = value; }
        }

        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Itemcode
        {
            get { return itemcode; }
            set { itemcode = value; }
        }

        public long Salereturndetailid
        {
            get { return salereturndetailid; }
            set { salereturndetailid = value; }
        }

        public string Systemvoucherno
        {
            get { return systemvoucherno; }
            set { systemvoucherno = value; }
        }
        
        public int Userid
        {
            get { return userid; }
            set { userid = value; }
        }

        public decimal Totalamt
        {
            get { return totalamt; }
            set { totalamt = value; }
        }

        public int Daylimit
        {
            get { return daylimit; }
            set { daylimit = value; }
        }

        public int Currencyid
        {
            get { return currencyid; }
            set { currencyid = value; }
        }

        public string Paymenttype
        {
            get { return paymenttype; }
            set { paymenttype = value; }
        }

        public string Voucherno
        {
            get { return voucherno; }
            set { voucherno = value; }
        }
        
        public string Customerid
        {
            get { return customerid; }
            set { customerid = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public long Transalereturnid
        {
            get { return transalereturnid; }
            set { transalereturnid = value; }
        }

        public long Salereturnid
        {
            get { return salereturnid; }
            set { salereturnid = value; }
        }


        public BOLSaleReturn()
        {
             currencyid = daylimit = userid = qty = originaluserid = edituserid = 0;
            editsaleretundate = date = lotterydate = DateTime.Now;
            salereturnid = transalereturnid = salereturndetailid = drawingtimes = locationid = 0;
            voucherno = paymenttype = itemcode = description = type = lotteryno = customername = systemvoucherno = customerid="";
        }
    }
}
