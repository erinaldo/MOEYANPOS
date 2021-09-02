using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLOpeningStock
    {
        private long iD;

        public long ID
        {
            get { return iD; }
            set { iD = value; }
        }
        private DateTime openingDate;

        public DateTime OpeningDate
        {
            get { return openingDate; }
            set { openingDate = value; }
        }
        private string itemCode;

        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }
        private int qty;

        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private decimal salePrice;

        public decimal SalePrice
        {
            get { return salePrice; }
            set { salePrice = value; }
        }

        private decimal purchasePrice;

        public decimal PurchasePrice
        {
            get { return purchasePrice; }
            set { purchasePrice = value; }
        }

        private int currencyID;

        public int CurrencyID
        {
            get { return currencyID; }
            set { currencyID = value; }
        }

        private long locationID;

        public long LocationID
        {
            get { return locationID; }
            set { locationID = value; }
        }

        public BOLOpeningStock()
        {
            iD = locationID = 0;
            openingDate = DateTime.Now;
            itemCode = name = "";
            PurchasePrice = salePrice = 0;
            qty = currencyID = 0;
        }
    }
}
