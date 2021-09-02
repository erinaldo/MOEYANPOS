using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLSaleOrder
    {
        private long saleorderid;
        private DateTime orderdate;
        private string voucherno;
        private string customerid;
        private string currencytype;
        private string remark;
        private decimal totalamt;
        private decimal advance;
        private decimal grandtotal;
        private decimal total;
        private long saleorderdetailid;
        //private int saleorderid;
        private string itemcode;
        private string description;
        private string type;
        private int qty;
        private decimal saleprice;
        private long transsaleorderid;
        private long systemvoucherno;
        private int originaluserid;
        private int edituserid;
        private DateTime editsaledate;
        private int userid;
        private string customername;
        private string username;
        private decimal balance;
        private DateTime deliverydate;
        private long locationid;
        private string location;
        private string paymenttype;
        private long cid;

        public long Cid
        {
            get { return cid; }
            set { cid = value; }
        }

        public string Paymenttype
        {
            get { return paymenttype; }
            set { paymenttype = value; }
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

        public DateTime Deliverydate
        {
            get { return deliverydate; }
            set { deliverydate = value; }
        }

        public decimal Balance
        {
            get { return balance; }
            set { balance = value; }
        }
       
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Customername
        {
            get { return customername; }
            set { customername = value; }
        }

        public int Userid
        {
            get { return userid; }
            set { userid = value; }
        }

        public DateTime Editsaledate
        {
            get { return editsaledate; }
            set { editsaledate = value; }
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

        public long Systemvoucherno
        {
            get { return systemvoucherno; }
            set { systemvoucherno = value; }
        }

        public long Transsaleorderid
        {
            get { return transsaleorderid; }
            set { transsaleorderid = value; }
        }

        public decimal Grandtotal
        {
            get { return grandtotal; }
            set { grandtotal = value; }
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

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        public string Currencytype
        {
            get { return currencytype; }
            set { currencytype = value; }
        }

        public string Customerid
        {
            get { return customerid; }
            set { customerid = value; }
        }

        public string Voucherno
        {
            get { return voucherno; }
            set { voucherno = value; }
        }

        public DateTime Orderdate
        {
            get { return orderdate; }
            set { orderdate = value; }
        }

        public long Saleorderid
        {
            get { return saleorderid; }
            set { saleorderid = value; }
        }
        //SaleOrderDetail
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

        public long Saleorderdetailid
        {
            get { return saleorderdetailid; }
            set { saleorderdetailid = value; }
        }


        public BOLSaleOrder()
        {
            cid = 0;
            qty = originaluserid = edituserid = userid = 0;
            voucherno = currencytype = remark = itemcode = description = type = username = customername =customerid= "";
            orderdate = editsaledate = deliverydate = DateTime.Now;
            totalamt = advance = grandtotal = saleprice = balance = 0;
            saleorderdetailid = saleorderid = transsaleorderid = systemvoucherno = 0;
        }
    }
}
