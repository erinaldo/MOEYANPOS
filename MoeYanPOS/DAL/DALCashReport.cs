using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MoeYanPOS.BOL;
using MoeYanPOS.Function;

namespace MoeYanPOS.DAL
{
    class DALCashReport
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string constr = MoeYanConfiguration.GetConnection();
        #endregion     

        #region "GetDailyCashStatementSummary"
        public DataSet GetDailyCashStatementSummary(DateTime CashReceiveDate, long LocationID)
        {
            DataSet ds = new DataSet();         
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetDailyCashStatementSummary", con);
                cmd.Parameters.AddWithValue("@Date", CashReceiveDate);
                cmd.Parameters.AddWithValue("@LocationID", LocationID);
                cmd.CommandType = CommandType.StoredProcedure;
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

        #region"GetProfit&Loss"
        public DataSet GetProfitAndLoss(DateTime CashReceiveDate,DateTime ToDate, long LocationID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_ProfitAndLoss", con);
                cmd.Parameters.AddWithValue("@Date", CashReceiveDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
                cmd.Parameters.AddWithValue("@LocationID", LocationID);
                cmd.CommandType = CommandType.StoredProcedure;
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

        #region "GetDailyCashStatementSummaryforOpening"
        public DataSet GetDailyCashStatementSummaryforOpening(DateTime CashReceiveDate, long LocationID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetDailyCashStatementSummaryforOpening", con);
                cmd.Parameters.AddWithValue("@Date", CashReceiveDate);
                cmd.Parameters.AddWithValue("@LocationID", LocationID);
                cmd.CommandType = CommandType.StoredProcedure;
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

        #region "GetDailyCashStatementSummaryReport"
        public DataSet GetDailyCashStatementSummaryReport(DateTime CashReceiveDate, long LocationID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                //cmd = new SqlCommand("SP_GetDailyCashStatementSummaryReport", con);
                cmd = new SqlCommand("SP_GetDailyCashStatementSummary", con);
                cmd.Parameters.AddWithValue("@Date", CashReceiveDate);
                cmd.Parameters.AddWithValue("@LocationID", LocationID);
                cmd.CommandType = CommandType.StoredProcedure;
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
