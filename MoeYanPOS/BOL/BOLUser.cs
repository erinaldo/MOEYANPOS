using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLUser
    {
        private int userID;
        private string userName;
        private int memberID;
        private string cardID;
        private string password;
        private string township;
        private int townshipID;
        private string nRC;
        private string level;
        private bool isSavePrint;
        private bool ismsgforVoucher;
        private string staffid;
        private string staffname;

        public string StaffName
        {
            get { return staffname; }
            set { staffname = value; }
        }

        public int TownshipID
        {
            get { return townshipID; }
            set { townshipID = value; }
        }

        public bool IsmsgforVoucher
        {
            get { return ismsgforVoucher; }
            set { ismsgforVoucher = value; }
        }

        public string Level
        {
            get { return level; }
            set { level = value; }
        }       

        public bool IsSavePrint
        {
            get { return isSavePrint; }
            set { isSavePrint = value; }
        }

        public string NRC
        {
            get { return nRC; }
            set { nRC = value; }
        }

        public string Township
        {
            get { return township; }
            set { township = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string CardID
        {
            get { return cardID; }
            set { cardID = value; }
        }

        public int MemberID
        {
            get { return memberID; }
            set { memberID = value; }
        }
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public string StaffID
        {
            get { return staffid; }
            set { staffid = value; }
        }

        public BOLUser()
        {
            memberID = userID = townshipID = 0; 
            userName=cardID=password=nRC=level= township= staffid = "";
            ismsgforVoucher = isSavePrint = false;
        }
    }
}
