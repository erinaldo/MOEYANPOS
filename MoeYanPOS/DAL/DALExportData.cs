using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoeYanPOS.BOL;
using System.Data.SqlClient;
using System.Data;
using MoeYanPOS.Function;

namespace MoeYanPOS.DAL
{
    class DALExportData
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr = MoeYanConfiguration.GetConnection();
        #endregion

        #region "ExportData"
        public int ExportData(BOLExportData bolexportdata)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_ExportData", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd.Parameters.AddWithValue("@FromDate", bolexportdata.DtpFromDate);
                cmd.Parameters.AddWithValue("@ToDate", bolexportdata.DtpToDate);
                issaved = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return issaved;
        }
        #endregion
    }
}
