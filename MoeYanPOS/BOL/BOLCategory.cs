using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLCategory
    {
        private int id;
        private string categoryName;
        private int classID;
        private string classname;
        private string reportGroupID;
        private string mbc_categoryID;

        public string MBC_CategoryID
        {
            get { return mbc_categoryID; }
            set { mbc_categoryID = value; }
        }

        public string ReportGroupID
        {
            get { return reportGroupID; }
            set { reportGroupID = value; }
        }

        public string Classname
        {
            get { return classname; }
            set { classname = value; }
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
        

        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }
        public BOLCategory()
        {
            id = classID = 0;
            categoryName= reportGroupID = "";
        }
    }
}
