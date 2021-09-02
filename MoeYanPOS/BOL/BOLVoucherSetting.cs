using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLVoucherSetting
    {
        private int id;
        private string name;
        private string address;
        private string phone;
        private string message;
        private byte[] logo;

        public byte[] Logo
        {
            get { return logo; }
            set { logo = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public BOLVoucherSetting()
        {
            id=0;
            name=address=phone=message="";
            logo=null;
        }
    }
}
