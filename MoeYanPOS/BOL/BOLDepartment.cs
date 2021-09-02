using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLDepartment
    {
        private int id;
        private string departmentname;
        private string mbcdepartmentid;

        public string MBCDepartmentID
        {
            get { return mbcdepartmentid; }
            set { mbcdepartmentid = value; }
        }

        public string Departmentname
        {
            get { return departmentname; }
            set { departmentname = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public BOLDepartment()
        {
            id = 0;
            departmentname = MBCDepartmentID = "";
        }
    }
}
