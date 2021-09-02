using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLBrand
    {
        private int id;
        private string brandname;
        private int action;

        public int Action
        {
            get { return action; }
            set { action = value; }
        }

        public string Brandname
        {
            get { return brandname; }
            set { brandname = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public BOLBrand()
        {
            id = action = 0;
            brandname = "";
        }
    }
}
