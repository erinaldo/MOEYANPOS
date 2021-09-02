using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeYanPOS.Function
{
    class MoeYanFunctions
    {
        public static void PrintReport(CrystalDecisions.CrystalReports.Engine.ReportDocument rpt, string printerName)
        {
            
            rpt.PrintOptions.PrinterName = printerName;
            //System.Drawing.Printing.PaperSource ps = new System.Drawing.Printing.PaperSource();
            //ps.SourceName = "Roll Paper 76 x 297 mm";
            //rpt.PrintOptions.CustomPaperSource = ps;
            rpt.PrintToPrinter(1, true, 0, 0);
        }
        public static class MoeYanPOS_Helper
        {
            public static string counterCode = "";
            public static string counterName = "";
            public static string locationCode = "";
        }
        public static long GenerateSysKey()
        { 
            Random random = new Random();
            long Syskey = 0;
            string date = DateTime.Now.ToString("yyyyMMddHHmmss");
            string loc = MoeYanFunctions.MoeYanPOS_Helper.locationCode;
            string ran = random.Next(0000, 9999).ToString();
            Syskey = Convert.ToInt64(date + loc + ran);
            return Syskey;
        }
    }
}
