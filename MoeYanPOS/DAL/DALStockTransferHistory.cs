using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoeYanPOS.BOL;
using MoeYanPOS.DAL;
using System.Data;
using System.Data.SqlClient;
using MoeYanPOS.Function;

namespace MoeYanPOS.DAL
{
    class DALStockTransferHistory
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr = MoeYanConfiguration.GetConnection();
        #endregion

        #region "GetStockTransferHistory"
        public DataSet GetStockTransferHistory(DateTime startdate, DateTime enddate, string ItemCode, long LocationID, long ToLocationID)
        {
            DataSet ds = new DataSet();
            try
            {

                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetStockTransferHistory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartDate", startdate);
                cmd.Parameters.AddWithValue("@EndDate", enddate);
                cmd.Parameters.AddWithValue("@ItemCode", ItemCode);               
                cmd.Parameters.AddWithValue("@LocationID", LocationID);
                cmd.Parameters.AddWithValue("@LocationToID", ToLocationID);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return ds;
        }
        #endregion

        #region "GetStockTransferDetail"
        public DataSet GetStockTransferDetail(long ID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetStockTransferDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return ds;
        }
        #endregion

        #region "DeleteStockTranserHistory"
        public int DeleteStockTranserHistory(long id)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_DeleteStockTransfer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                isdelete = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return isdelete;
        }
        #endregion

        #region "GetStockTransferReport"
        public DataSet GetStockTransferReport(DateTime startdate, DateTime enddate, string ItemCode, long LocationID, long TolocationID)
        {
            DataSet ds = new DataSet();
            try
            {

                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetTransferReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartDate", startdate);
                cmd.Parameters.AddWithValue("@EndDate", enddate);
                cmd.Parameters.AddWithValue("@ItemCode", ItemCode);                
                cmd.Parameters.AddWithValue("@LocationID", LocationID);
                cmd.Parameters.AddWithValue("@LocationToID", TolocationID);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return ds;
        }
        #endregion
    }
}
