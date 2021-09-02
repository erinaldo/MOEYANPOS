using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLStaff
    {

        private int staffid;
        private string staffname;
        private int departmentid;
        private string departmentname;
        private string mbcstaffid;

        public string MCBStaffID
        {
            get { return mbcstaffid; }
            set { mbcstaffid = value; }
        }

        public int StaffID
        {
            get { return staffid; }
            set { staffid = value; }
        }

        public string StaffName
        {
            get { return staffname; }
            set { staffname = value; }
        }

        public int DepartmentID
        {
            get { return departmentid; }
            set { departmentid = value; }
        }
       

        public string DepartmentName
        {
            get { return departmentname; }
            set { departmentname = value; }
        }
        
        public BOLStaff()
        {
            StaffID = DepartmentID = 0;
            StaffName = DepartmentName = MCBStaffID = "";
        }

    }
}
