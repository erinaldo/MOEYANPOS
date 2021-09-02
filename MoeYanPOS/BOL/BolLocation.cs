using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BolLocation
    {
        private long id;
        private bool isThisLocation;
        private string locationNo;

        public string LocationNo
        {
            get { return locationNo; }
            set { locationNo = value; }
        }

        public bool IsThisLocation
        {
            get { return isThisLocation; }
            set { isThisLocation = value; }
        }
        public long ID
        {
            get { return id; }
            set { id = value; }
        }
        private string location;

        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        public BolLocation()
        {
            id = 0;
            location = "";
            locationNo = "";
            isThisLocation = false;
        }

    }
}
