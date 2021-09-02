using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLSale
    {
        private long tranSaleID;
        private long saleID;
        private string voucherNo;
        private DateTime saleDate;
        private string customerID;
        private int userID;
        private string paymentType;
        private int currencyID;        
        private int dayLimit;
        private decimal totalAmt;
        private decimal advance;
        private decimal discount;
        private decimal grandTotal;
        private decimal paidamount;
        private decimal refundamount;
        private int totalFOC;
        private decimal totalitemDiscount;
        private long saleDetailID;
        private string itemCode;
        private string description;
        private string mtype;
        private int qty;
        private decimal salePrice;
        private decimal charge;
        private decimal total;
        private bool fOC;
        private decimal itemDiscount;
        private decimal exchangeRate;
        private int originalUserID;
        private int editUserID;
        private DateTime editSaleDate;
        private string userName;
        private string customerName;
        private int itemDiscountPercent;
        private string systemVoucherNo;
        private string currency;
        private int action;
        private DateTime lotteryDate;
        private string drawingTimes;
        private string lotteryNo;
        private int no;
        private long locationid;
        private string counterid;
        private int transportationAmt;
        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        public int TransportationAmt
        {
            get { return transportationAmt; }
            set { transportationAmt = value; }
        }

        public long LocationID
        {
            get { return locationid; }
            set { locationid = value; }
        }

        public string CounterID
        {
            get { return counterid; }
            set { counterid = value; }
        }

        public int No
        {
            get { return no; }
            set { no = value; }
        }


        public string LotteryNo
        {
            get { return lotteryNo; }
            set { lotteryNo = value; }
        }

        public string DrawingTimes
        {
            get { return drawingTimes; }
            set { drawingTimes = value; }
        }

        public DateTime LotteryDate
        {
            get { return lotteryDate; }
            set { lotteryDate = value; }
        }


        public int Action
        {
            get { return action; }
            set { action = value; }
        }


        public string Currency
        {
            get { return currency; }
            set { currency = value; }
        }

        public string SystemVoucherNo
        {
            get { return systemVoucherNo; }
            set { systemVoucherNo = value; }
        }

        public int ItemDiscountPercent
        {
            get { return itemDiscountPercent; }
            set { itemDiscountPercent = value; }
        }

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }        

        public DateTime EditSaleDate
        {
            get { return editSaleDate; }
            set { editSaleDate = value; }
        }

        public int EditUserID
        {
            get { return editUserID; }
            set { editUserID = value; }
        }

        public int OriginalUserID
        {
            get { return originalUserID; }
            set { originalUserID = value; }
        }

        public decimal ExchangeRate
        {
            get { return exchangeRate; }
            set { exchangeRate = value; }
        }

        public long TranSaleID
        {
            get { return tranSaleID; }
            set { tranSaleID = value; }
        }

        public long SaleID
        {
            get { return saleID; }
            set { saleID = value; }
        }

        public decimal ItemDiscount
        {
            get { return itemDiscount; }
            set { itemDiscount = value; }
        }

        public int TotalFOC
        {
            get { return totalFOC; }
            set { totalFOC = value; }
        }

        public decimal GrandTotal
        {
            get { return grandTotal; }
            set { grandTotal = value; }
        }

        public decimal PaidAmount
        {
            get { return paidamount; }
            set { paidamount = value; }
        }
        
        public decimal RefundAmount
        {
            get { return refundamount; }
            set { refundamount = value; }
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

        public decimal TotalAmt
        {
            get { return totalAmt; }
            set { totalAmt = value; }
        }

        public int DayLimit
        {
            get { return dayLimit; }
            set { dayLimit = value; }
        }

        public int CurrencyID
        {
            get { return currencyID; }
            set { currencyID = value; }
        }

        public string PaymentType
        {
            get { return paymentType; }
            set { paymentType = value; }
        }

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public string CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        public DateTime SaleDate
        {
            get { return saleDate; }
            set { saleDate = value; }
        }

        public string VoucherNo
        {
            get { return voucherNo; }
            set { voucherNo = value; }
        }
       

        public decimal TotalitemDiscount
        {
            get { return totalitemDiscount; }
            set { totalitemDiscount = value; }
        }

        public bool FOC
        {
            get { return fOC; }
            set { fOC = value; }
        }

        public decimal Total
        {
            get { return total; }
            set { total = value; }
        }

        public decimal SalePrice
        {
            get { return salePrice; }
            set { salePrice = value; }
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

        public string Mtype
        {
            get { return mtype; }
            set { mtype = value; }
        }

        public long SaleDetailID
        {
            get { return saleDetailID; }
            set { saleDetailID = value; }
        }       

        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public BOLSale()
        {
            action = qty = dayLimit = totalFOC = currencyID = originalUserID = transportationAmt = 0;
            itemCode = description = mtype = voucherNo = paymentType = userName = customerName = currency = systemVoucherNo = customerID = "";
            salePrice = total = itemDiscount = totalitemDiscount = totalAmt = advance = discount = grandTotal = exchangeRate =  paidamount = refundamount = 0;        
            saleDate=editSaleDate=DateTime.Now;
            saleDetailID = locationid = 0;
            counterid = ""; saleID = 0;
            fOC = false;  
            
        }
    }
}
