using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLPurchaseDetail
    {
        private long purchasedetailid;
        private long purchaseid;
        private int no;
        private string itemcode;
        private string description;
        private string type;
        private int qty;
        private decimal purchaseprice;
        private decimal total;
        private bool foc;
        private decimal itemdiscount;
        private int itemdiscountpercent;

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

        public long Purchaseid
        {
            get { return purchaseid; }
            set { purchaseid = value; }
        }

        public long Purchasedetailid
        {
            get { return purchasedetailid; }
            set { purchasedetailid = value; }
        }
    }
}
