using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using MoeYanPOS.Function;


namespace MoeYanPOS.UI
{
    public partial class frmExportImportPriceList : Form
    {
        public frmExportImportPriceList()
        {
            InitializeComponent();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
           //// string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
           // string constr   = MoeYanConfiguration.GetConnection();
           // using (SqlConnection con = new SqlConnection(constr))
           // {
           //     using (SqlCommand cmd = new SqlCommand("SELECT * FROM Customers"))
           //     {
           //         using (SqlDataAdapter sda = new SqlDataAdapter())
           //         {
           //             cmd.Connection = con; 
           //             sda.SelectCommand = cmd;
           //             using (DataTable dt = new DataTable())
           //             {
           //                 sda.Fill(dt);

           //                 //Build the Text file data.
           //                 string txt = string.Empty;

           //                 foreach (DataColumn column in dt.Columns)
           //                 {
           //                     //Add the Header row for Text file.
           //                     txt += column.ColumnName + "\t\t";
           //                 }

           //                 //Add new line.
    
           //                 txt += "\r\n";

           //                 foreach (DataRow row in dt.Rows)
           //                 {
           //                     foreach (DataColumn column in dt.Columns)
           //                     {
           //                         //Add the Data rows.
           //                         txt += row[column.ColumnName].ToString() + "\t\t";
           //                     }

           //                     //Add new line.
           //                     txt += "\r\n";
           //                 }

           //                 //Download the Text file.
           //                 Response.Clear();
           //                 Response.Buffer = true;
           //                 Response.AddHeader("content-disposition", "attachment;filename=SqlExport.txt");
           //                 Response.Charset = "";
           //                 Response.ContentType = "application/text";
           //                 Response.Output.Write(txt);
           //                 Response.Flush();
           //                 Response.End();
                //        }
                //    }
                //}
            //}
        }
    }
}
