using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MoeYanPOS.Function
{
    class Validation
    {
        public static string isNullOrEmptyField(string objName, string value)
        {
            string err = "";
            if (String.IsNullOrEmpty(value))
            {
               // throw new MoeYanException(" Fill Data for" + objName + ".");
                err= " Fill Data for" + objName + ".";
            }
            return err;
        }

        public static string isNumberField(string objName, string value)
        {
            string err = "";
            Regex reg = new Regex(@"^-[0-9]+$|^[0-9]+$", RegexOptions.Multiline);
            if (!reg.IsMatch(value))
            {
                //throw new MoeYanException(objName + " fills integer only.");
                err = objName + " fills integer only.";
            }
            return err;
        }

        public static string isEmail(string objName, string value)
        {
            string err = "";
            Regex reg = new Regex(@"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})", RegexOptions.Multiline);
            if (!reg.IsMatch(value))
            {
                //throw new MoeYanException(objName + " isn`t email.");
                err = objName + " isn`t email.";
            }
            return err;
        }
    }
        
}
