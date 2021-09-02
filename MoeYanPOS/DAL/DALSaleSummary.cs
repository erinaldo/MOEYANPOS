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
    class DALSaleSummary
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string constr = MoeYanConfiguration.GetConnection();
        #endregion

        #region "SelectSaleSummaryCash"
        public DataSet SelectSaleSummaryCash(DateTime startdate, DateTime enddate,int ClassID,int CategoryID,string ItemCode,string PaymentType,long CustomerID,int CurrencyID,int BrandID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetSaleSummaryCash", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StartDate", startdate);
                cmd.Parameters.AddWithValue("@EndDate", enddate);
                cmd.Parameters.AddWithValue("@ClassID", ClassID);
                cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                cmd.Parameters.AddWithValue("@ItemCode", ItemCode);
                cmd.Parameters.AddWithValue("@PaymentType", PaymentType);
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@CurrencyID", CurrencyID);
                cmd.Parameters.AddWithValue("@BrandID", BrandID);

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

        #region "SelectSaleSummaryByStock"
        public DataSet SelectSaleSummaryByStock(DateTime startdate, DateTime enddate, int ClassID, int CategoryID, string ItemCode, string PaymentType, long CustomerID, int CurrencyID, int BrandID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetSaleSummaryByStock", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StartDate", startdate);
                cmd.Parameters.AddWithValue("@EndDate", enddate);
                cmd.Parameters.AddWithValue("@ClassID", ClassID);
                cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                cmd.Parameters.AddWithValue("@ItemCode", ItemCode);
                cmd.Parameters.AddWithValue("@PaymentType", PaymentType);
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                //cmd.Parameters.AddWithValue("@CurrencyID", CurrencyID);
                //cmd.Parameters.AddWithValue("@BrandID", BrandID);

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

        #region "MonthlySaleSummaryByCustomer"
        public DataSet MonthlySaleSummaryByCustomer(DateTime startdate, DateTime enddate, int ClassID, int CategoryID, string ItemCode, string PaymentType, long CustomerID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetCustomerSaleAmountbyMonthly", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StartDate", startdate);
                cmd.Parameters.AddWithValue("@EndDate", enddate);
                cmd.Parameters.AddWithValue("@Class", ClassID);
                cmd.Parameters.AddWithValue("@Category", CategoryID);
                cmd.Parameters.AddWithValue("@Item", ItemCode);
                cmd.Parameters.AddWithValue("@Payment", PaymentType);
                cmd.Parameters.AddWithValue("@Customer", CustomerID);

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

        #region "StockMovementReport"
        public DataSet GetStockMovementData(DateTime startdate, DateTime enddate, int ClassID, int CategoryID, string ItemCode, int LocationID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                this.con = new SqlConnection(this.constr);
                this.cmd = new SqlCommand("SP_GetStockMovementReport", this.con);
                this.cmd.CommandType = CommandType.StoredProcedure;
                this.cmd.Parameters.AddWithValue("@StartDate", startdate);
                this.cmd.Parameters.AddWithValue("@EndDate", enddate);
                this.cmd.Parameters.AddWithValue("@ClassID", ClassID);
                this.cmd.Parameters.AddWithValue("@LocationID", LocationID);
                this.cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                this.cmd.Parameters.AddWithValue("@ItemCode", ItemCode);
                if (this.con.State == ConnectionState.Open)
                {
                    this.con.Close();
                }
                this.con.Open();
                adapter.SelectCommand = this.cmd;
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.con.Close();
            }
            return ds;
        }
        #endregion

        #region "SelectPurchaseSummaryCash"
        public DataSet SelectPurchaseSummaryCash(DateTime startdate, DateTime enddate, int ClassID, int CategoryID, string ItemCode, string PaymentType, long CustomerID, int CurrencyID, int BrandID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetPurchaseSummaryByStock", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StartDate", startdate);
                cmd.Parameters.AddWithValue("@EndDate", enddate);
                cmd.Parameters.AddWithValue("@ClassID", ClassID);
                cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                cmd.Parameters.AddWithValue("@ItemCode", ItemCode);
                cmd.Parameters.AddWithValue("@PaymentType", PaymentType);
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@CurrencyID", CurrencyID);
                cmd.Parameters.AddWithValue("@BrandID", BrandID);

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

        #region "SelectSaleSummaryCredit"
        public DataSet SelectSaleSummaryCredit(DateTime startdate, DateTime enddate)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetSaleSummaryCredit", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StartDate", startdate);
                cmd.Parameters.AddWithValue("@EndDate", enddate);

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

        //#region "SelectSaleSummaryCash"
        //public DataSet SelectSaleSummaryCredit(DateTime startdate, DateTime enddate)
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        SqlDataAdapter adapter = new SqlDataAdapter();
        //        con = new SqlConnection(constr);
        //        cmd = new SqlCommand("SP_GetSaleSummaryCash", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@StartDate", startdate);
        //        cmd.Parameters.AddWithValue("@EndDate", enddate);

        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //        con.Open();
        //        adapter.SelectCommand = cmd;
        //        adapter.Fill(ds);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return ds;
        //}
        //#endregion

        //#region "SelectSaleSummaryCredit"
        //public List<BOLSaleSummary> SelectSaleSummaryCredit(DateTime startdate, DateTime enddate)
        //{
        //    List<BOLSaleSummary> lstsummary = new List<BOLSaleSummary>();
        //    try
        //    {
        //        con = new SqlConnection(constr);
        //        cmd = new SqlCommand("SP_GetSaleSummaryCredit", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@StartDate", startdate);
        //        cmd.Parameters.AddWithValue("@EndDate", enddate);

        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }

        //        con.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {
        //                BOLSaleSummary bolsummary = new BOLSaleSummary();

        //                bolsummary.Category = reader["Category"].ToString();
        //                bolsummary.Categoryid = reader["CategoryID"].ToString();
        //                bolsummary.Classname = reader["ClassName"].ToString();
        //                bolsummary.Discount = Decimal.Parse(reader["Discount"].ToString());
        //                bolsummary.Grandtotal = Decimal.Parse(reader["Grandtotal"].ToString());
        //                bolsummary.Date = DateTime.Parse(reader["Date"].ToString());

        //                lstsummary.Add(bolsummary);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return lstsummary;
        //}
        //#endregion

        #region "GetCustomerLedger"
        public DataSet GetCustomerLedger(DateTime startdate, DateTime enddate,long CustomerID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetCustomerLedger", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StartDate", startdate);
                cmd.Parameters.AddWithValue("@EndDate", enddate);
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
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

        #region "GetAllCustomerLedger"
        public DataSet GetAllCustomerLedger(DateTime startdate, DateTime enddate)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetAllCustomerLedger", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StartDate", startdate);
                cmd.Parameters.AddWithValue("@EndDate", enddate);
                
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

        #region "GetNetSale"  
        //Add by Hnin Thazin Nwe (1.8.2017)
        public DataTable SelectNetSale(DateTime startdate, DateTime enddate, int ClassID, int CategoryID, string ItemCode, string PaymentType, long CustomerID, int CurrencyID, int BrandID)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetNetSaleReport", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StartDate", startdate);
                cmd.Parameters.AddWithValue("@EndDate", enddate);
                cmd.Parameters.AddWithValue("@ClassID", ClassID);
                cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                cmd.Parameters.AddWithValue("@ItemCode", ItemCode);
                cmd.Parameters.AddWithValue("@PaymentType", PaymentType);
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@CurrencyID", CurrencyID);
                cmd.Parameters.AddWithValue("@BrandID", BrandID);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public DataTable SelectNetSaleHeader(DateTime startdate, DateTime enddate, string PaymentType, long CustomerID, int CurrencyID, int BrandID)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetNetSaleReportHeader", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StartDate", startdate);
                cmd.Parameters.AddWithValue("@EndDate", enddate);
                cmd.Parameters.AddWithValue("@PaymentType", PaymentType);
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@CurrencyID", CurrencyID);
                cmd.Parameters.AddWithValue("@BrandID", BrandID);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public decimal GetAllDiscountForNetSale(DateTime startdate, DateTime enddate,string PaymentType, long CustomerID, int CurrencyID, int BrandID)
        {
            decimal discount = 0;
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetAllDiscount_for_NetSale", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StartDate", startdate);
                cmd.Parameters.AddWithValue("@EndDate", enddate);
                cmd.Parameters.AddWithValue("@PaymentType", PaymentType);
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@CurrencyID", CurrencyID);
                cmd.Parameters.AddWithValue("@BrandID", BrandID);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                string var = cmd.ExecuteScalar().ToString();
                if (var != "")
                {
                    discount = Convert.ToDecimal(cmd.ExecuteScalar().ToString());
                }
                else{discount = 0;}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return discount;
        }
        #endregion

        #region "GetNetPurchase"
        //Add by Hnin Thazin Nwe (21.9.2017) for Money Exchange
        public DataTable SelectNetPurchaseHeader(DateTime startdate, DateTime enddate, string PaymentType, long CustomerID, int CurrencyID, int BrandID)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetNetPurchaseReportHeader", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StartDate", startdate);
                cmd.Parameters.AddWithValue("@EndDate", enddate);
                cmd.Parameters.AddWithValue("@PaymentType", PaymentType);
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@CurrencyID", CurrencyID);
                cmd.Parameters.AddWithValue("@BrandID", BrandID);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public DataTable SelectNetPurchase(DateTime startdate, DateTime enddate, int ClassID, int CategoryID, string ItemCode, string PaymentType, long CustomerID, int CurrencyID, int BrandID)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetNetPurchaseReport", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StartDate", startdate);
                cmd.Parameters.AddWithValue("@EndDate", enddate);
                cmd.Parameters.AddWithValue("@ClassID", ClassID);
                cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                cmd.Parameters.AddWithValue("@ItemCode", ItemCode);
                cmd.Parameters.AddWithValue("@PaymentType", PaymentType);
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@CurrencyID", CurrencyID);
                cmd.Parameters.AddWithValue("@BrandID", BrandID);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public decimal GetAllDiscountForNetPurchase(DateTime startdate, DateTime enddate, string PaymentType, long CustomerID, int CurrencyID, int BrandID)
        {
            decimal discount = 0;
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetAllDiscount_for_NetPurchase", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StartDate", startdate);
                cmd.Parameters.AddWithValue("@EndDate", enddate);
                cmd.Parameters.AddWithValue("@PaymentType", PaymentType);
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@CurrencyID", CurrencyID);
                cmd.Parameters.AddWithValue("@BrandID", BrandID);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                string var = cmd.ExecuteScalar().ToString();
                if (var != "")
                {
                    discount = Convert.ToDecimal(cmd.ExecuteScalar().ToString());
                }
                else { discount = 0; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return discount;
        }
        #endregion
    }
}
