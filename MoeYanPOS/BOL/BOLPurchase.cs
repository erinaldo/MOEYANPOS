using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLPurchase
    {
        private long purchaseid;
        private string voucherno;
        private DateTime purchasedate;
        private int supplierid;
        private int userid;
        private string paymenttype;
        private int currencyid;
        private int daylimit;
        private decimal totalamt;
        private decimal advance;
        private decimal discount;
        private decimal grandtotal;
        private int totalfoc;
        private decimal totalitemdiscount;
        private int originaluserid;
        private int edituserid;
        private DateTime editpurchsedate;
        private decimal exchangerate;
        private string systemvoucherno;
        private DateTime lotterydate;
        private long drawingTimes;
        private string lotteryno;
        private long purchasedetailid;
        private int no;
        private string supplierName;
        private string mtype;
        private string itemcode;
        private string description;
        private string type;
        private int qty;
        private decimal purchaseprice;
        private decimal total;
        private bool foc;
        private decimal itemdiscount;
        private int itemdiscountpercent;
        private long transPurchaseID;
        private string currency;
        private long locationid;
        private string location;
        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; }
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

        public string Currency
        {
            get { return currency; }
            set { currency = value; }
        }

       public string Mtype
        {
            get { return mtype; }
            set { mtype = value; }
        }

        public string SupplierName
        {
            get { return supplierName; }
            set { supplierName = value; }
        }       

        public BOLPurchase()
        {
            purchaseid = drawingTimes = Purchasedetailid = transPurchaseID=locationid=0;
            voucherno = systemvoucherno = paymenttype = lotteryno = supplierName = mtype = description = type = itemcode = username = "";
            purchasedate = editpurchsedate = lotterydate = DateTime.Now;
            supplierid = userid = currencyid = daylimit = totalfoc = originaluserid = edituserid = no =qty =0;
            totalamt = advance = discount = grandtotal = totalitemdiscount = exchangerate =purchaseprice=total=itemdiscount= 0;
        }
        
        public long TransPurchaseID
        {
            get { return transPurchaseID; }
            set { transPurchaseID = value; }
        }

        public int Itemdiscountpercent
        {
            get { return itemdiscountpercent; }
            set { itemdiscountpercent = value; }
        }

        public decimal Itemdiscount
        {
            get { return itemdiscount; }
            set { itemdiscount = value; }
        }

        public bool Foc
        {
            get { return foc; }
            set { foc = value; }
        }

        public decimal Total
        {
            get { return total; }
            set { total = value; }
        }

        public decimal Purchaseprice
        {
            get { return purchaseprice; }
            set { purchaseprice = value; }
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

        public int No
        {
            get { return no; }
            set { no = value; }
        }

        public long Purchasedetailid
        {
            get { return purchasedetailid; }
            set { purchasedetailid = value; }
        }

        public string Lotteryno
        {
            get { return lotteryno; }
            set { lotteryno = value; }
        }

        public long DrawingTimes
        {
            get { return drawingTimes; }
            set { drawingTimes = value; }
        }

        public DateTime Lotterydate
        {
            get { return lotterydate; }
            set { lotterydate = value; }
        }

        public string Systemvoucherno
        {
            get { return systemvoucherno; }
            set { systemvoucherno = value; }
        }

        public decimal Exchangerate
        {
            get { return exchangerate; }
            set { exchangerate = value; }
        }

        public DateTime Editpurchsedate
        {
            get { return editpurchsedate; }
            set { editpurchsedate = value; }
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

        public decimal Totalitemdiscount
        {
            get { return totalitemdiscount; }
            set { totalitemdiscount = value; }
        }
            
        public int Totalfoc
        {
            get { return totalfoc; }
            set { totalfoc = value; }
        }

        public decimal Grandtotal
        {
            get { return grandtotal; }
            set { grandtotal = value; }
        }

        public decimal Discount
        {
            get { return discount; }
            set { discount = value; }
        }

        public decimal Advance
        {
            get { return advance; }
            set { advance = value; }
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

        public int Userid
        {
            get { return userid; }
            set { userid = value; }
        }

        public int SupplierID
        {
            get { return supplierid; }
            set { supplierid = value; }
        }

        public DateTime Purchasedate
        {
            get { return purchasedate; }
            set { purchasedate = value; }
        }

        public string Voucherno
        {
            get { return voucherno; }
            set { voucherno = value; }
        }

        public long Purchaseid
        {
            get { return purchaseid; }
            set { purchaseid = value; }
        }
    }
}
