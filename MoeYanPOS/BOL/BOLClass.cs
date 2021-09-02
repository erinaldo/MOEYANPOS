using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLClass
    {
        private int id;
        private string className;
        private string mbcclassid;

        public string MBCClassID
        {
            get { return mbcclassid; }
            set { mbcclassid = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
      

        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }
        public BOLClass()
        {
            id=0;
            className = mbcclassid = "";
        }
    }
}
