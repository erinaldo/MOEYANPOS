using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.BOL
{
    class BOLTownship
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private int divisionID;

        public int DivisionID
        {
            get { return divisionID; }
            set { divisionID = value; }
        }
        private string township;

        public string Township
        {
            get { return township; }
            set { township = value; }
        }

        private string mbctownship;

        public string MBCTownshipID
        {
            get { return mbctownship; }
            set { mbctownship = value; }
        }

        public BOLTownship()
        {
            id = 0;
            divisionID = 0;
            township = "";
            mbctownship = "";
        }

    }
}
