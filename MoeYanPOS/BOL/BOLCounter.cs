using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLCounter
    {
        private Int32 id;
        private string name;
        private string code;
        private bool isthisLocation;
        private bool isdelete;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public bool IsthisLocation
        {
            get { return isthisLocation; }
            set { isthisLocation = value; }
        }
        public bool IsDelete
        {
            get { return isdelete; }
            set { isdelete = value; }
        }
        public BOLCounter()
        {
            Id=0;
            Name = Code = "";
            IsDelete = IsthisLocation = false;
        }
    }
}
