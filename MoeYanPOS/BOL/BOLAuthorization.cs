using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLAuthorization
    {
        private int userid;
        private Boolean saleentry;
        private Boolean purchaseentry;
        private Boolean salereturn;
        private Boolean saleorder;
        private Boolean stockajustment;
        private Boolean stocktransfer;
        private Boolean openingstock;
        private Boolean stockbalance;
        private Boolean payforcredit;
        private Boolean pettycash;
        private Boolean salehistory;
        private Boolean purchasehistory;
        private Boolean salereturnhistory;
        private Boolean saleorderhistory;
        private Boolean stockajustmenthistory;
        private Boolean stocktransferhistory;
        private Boolean openingstockhistory;
        private Boolean stockhistory;
        private Boolean payforcredithistory;
        private Boolean pettycashhistory;
        private Boolean user;
        private Boolean division;
        private Boolean department;
        private Boolean township;
        private Boolean location;
        private Boolean customer;
        private Boolean supplier;
        private Boolean measurement;
        private Boolean brand;
        private Boolean class_;
        private Boolean category;
        private Boolean stock;
        private Boolean currency;
        private Boolean systemsetting;
        private Boolean cashreport;
        private Boolean salereport;
        private Boolean purchasereport;
        private Boolean stockreport;
        private Boolean customerreport;
        private Boolean supplierreport;
        private Boolean setlocation;
        private Boolean exportnimport;
        private Boolean authorization;
        private Boolean vouchersetting;
        private Boolean stockadjustmentType;
        private Boolean outletcashheader;

        public int Userid
        {
            get { return userid; }
            set { userid = value; }
        }
        
        public Boolean Saleentry
        {
            get { return saleentry; }
            set { saleentry = value; }
        }
        
        public Boolean Purchaseentry
        {
            get { return purchaseentry; }
            set { purchaseentry = value; }
        }

        public Boolean Salereturn
        {
            get { return salereturn; }
            set { salereturn = value; }
        }
        
        public Boolean Saleorder
        {
            get { return saleorder; }
            set { saleorder = value; }
        }
        
        public Boolean Stockajustment
        {
            get { return stockajustment; }
            set { stockajustment = value; }
        }
        
        public Boolean Stocktransfer
        {
            get { return stocktransfer; }
            set { stocktransfer = value; }
        }
        
        public Boolean Openingstock
        {
            get { return openingstock; }
            set { openingstock = value; }
        }
        
        public Boolean Stockbalance
        {
            get { return stockbalance; }
            set { stockbalance = value; }
        }

        public Boolean Payforcredit
        {
            get { return payforcredit; }
            set { payforcredit = value; }
        }
        
        public Boolean Pettycash
        {
            get { return pettycash; }
            set { pettycash = value; }
        }
        
        public Boolean Salehistory
        {
            get { return salehistory; }
            set { salehistory = value; }
        }
        
        public Boolean Purchasehistory
        {
            get { return purchasehistory; }
            set { purchasehistory = value; }
        }
        
        public Boolean Salereturnhistory
        {
            get { return salereturnhistory; }
            set { salereturnhistory = value; }
        }
        
        public Boolean Saleorderhistory
        {
            get { return saleorderhistory; }
            set { saleorderhistory = value; }
        }
        
        public Boolean Stockajustmenthistory
        {
            get { return stockajustmenthistory; }
            set { stockajustmenthistory = value; }
        }
        
        public Boolean Stocktransferhistory
        {
            get { return stocktransferhistory; }
            set { stocktransferhistory = value; }
        }
        
        public Boolean Openingstockhistory
        {
            get { return openingstockhistory; }
            set { openingstockhistory = value; }
        }
        
        public Boolean Stockhistory
        {
            get { return stockhistory; }
            set { stockhistory = value; }
        }
        
        public Boolean Payforcredithistory
        {
            get { return payforcredithistory; }
            set { payforcredithistory = value; }
        }
        
        public Boolean Pettycashhistory
        {
            get { return pettycashhistory; }
            set { pettycashhistory = value; }
        }
        
        public Boolean User
        {
            get { return user; }
            set { user = value; }
        }
        
        public Boolean Division
        {
            get { return division; }
            set { division = value; }
        }
        
        public Boolean Department
        {
            get { return department; }
            set { department = value; }
        }
        
        public Boolean Township
        {
            get { return township; }
            set { township = value; }
        }
        
        public Boolean Location
        {
            get { return location; }
            set { location = value; }
        }
        
        public Boolean Customer
        {
            get { return customer; }
            set { customer = value; }
        }
        
        public Boolean Supplier
        {
            get { return supplier; }
            set { supplier = value; }
        }
        
        public Boolean Measurement
        {
            get { return measurement; }
            set { measurement = value; }
        }

        public Boolean Brand
        {
            get { return brand; }
            set { brand = value; }
        }
       
        public Boolean Class_
        {
            get { return class_; }
            set { class_ = value; }
        }
        
        public Boolean Category
        {
            get { return category; }
            set { category = value; }
        }
        
        public Boolean Stock
        {
            get { return stock; }
            set { stock = value; }
        }
        
        public Boolean Currency
        {
            get { return currency; }
            set { currency = value; }
        }
        
        public Boolean Systemsetting
        {
            get { return systemsetting; }
            set { systemsetting = value; }
        }
        
        public Boolean Cashreport
        {
            get { return cashreport; }
            set { cashreport = value; }
        }
        
        public Boolean Salereport
        {
            get { return salereport; }
            set { salereport = value; }
        }
        
        public Boolean Purchasereport
        {
            get { return purchasereport; }
            set { purchasereport = value; }
        }
        
        public Boolean Stockreport
        {
            get { return stockreport; }
            set { stockreport = value; }
        }
        
        public Boolean Customerreport
        {
            get { return customerreport; }
            set { customerreport = value; }
        }
        
        public Boolean Supplierreport
        {
            get { return supplierreport; }
            set { supplierreport = value; }
        }

        public Boolean Setlocation
        {
            get { return setlocation; }
            set { setlocation = value; }
        } 

        public Boolean Exportnimport
        {
            get { return exportnimport; }
            set { exportnimport = value; }
        }

        public Boolean Authorization
        {
            get { return authorization; }
            set { authorization = value; }
        }
        public Boolean VoucherSetting
        {
            get { return vouchersetting; }
            set { vouchersetting = value; }
        }
        public Boolean StockAdjustmentType
        {
            get { return stockadjustmentType; }
            set { stockadjustmentType = value; }
        }
        public Boolean OutletCashHeader
        {
            get { return outletcashheader; }
            set { outletcashheader = value; }
        }
    }
}
