using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLStock
    {
        private int id;
        private int classID;
        private string itemCode;
        private string nameEng;
        private string nameMM;
        private int typeID;
        private decimal price;
        private decimal charges;
        private int minQty;
        private int maxQty;
        private bool isStock;
        private bool inActive;
        private int action;
        private int brandID;
        private decimal wholeSalePrice;
        private decimal purchasePrice;
        private int unitQty;
        private string itembarCode;
        private string mbctypeid;

        public string MBCTypeID
        {
            get { return mbctypeid; }
            set { mbctypeid = value; }
        }

        public string ItemBarCode
        {
            get { return itembarCode; }
            set { itembarCode = value; }
        }

        public int UnitQty
        {
            get { return unitQty; }
            set { unitQty = value; }
        }

        public decimal PurchasePrice
        {
            get { return purchasePrice; }
            set { purchasePrice = value; }
        }

        public decimal WholeSalePrice
        {
            get { return wholeSalePrice; }
            set { wholeSalePrice = value; }
        }

        public decimal Charges
        {
            get { return charges; }
            set { charges = value; }
        }

        public int BrandID
        {
            get { return brandID; }
            set { brandID = value; }
        }

        public int Action
        {
            get { return action; }
            set { action = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int ClassID
        {
            get { return classID; }
            set { classID = value; }
        }
        private int categoryID;

        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }

        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }
        
        public string NameEng
        {
            get { return nameEng; }
            set { nameEng = value; }
        }

        public string NameMM
        {
            get { return nameMM; }
            set { nameMM = value; }
        }

        public int TypeID
        {
            get { return typeID; }
            set { typeID = value; }
        }
      
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }
       
        public int MinQty
        {
            get { return minQty; }
            set { minQty = value; }
        }
        
        public int MaxQty
        {
            get { return maxQty; }
            set { maxQty = value; }
        }
        
        public bool IsStock
        {
            get { return isStock; }
            set { isStock = value; }
        }
        
        public bool InActive
        {
            get { return inActive; }
            set { inActive = value; }
        }

        public BOLStock()
        {
             id=classID=categoryID=typeID=minQty=maxQty=action=brandID= unitQty= 0;
             itemCode=nameEng=nameMM="";
             price = wholeSalePrice = purchasePrice = 0;
             isStock = inActive=false;       
        }
    }
}
