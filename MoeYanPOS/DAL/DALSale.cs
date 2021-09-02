using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoeYanPOS.BOL;
using System.Data;
using System.Data.SqlClient;
using MoeYanPOS.Function;

namespace MoeYanPOS.DAL
{
    class DALSale
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr   = MoeYanConfiguration.GetConnection();
        #endregion

        #region "SaveSaleData"

        public int SaveSaleData(BOLSale bolsale)
        {
            int isSaved = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_InsertSale", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@SaleID", bolsale.SaleID);
                cmd.Parameters.AddWithValue("@tranSaleID", bolsale.TranSaleID);
                cmd.Parameters.AddWithValue("@VoucherNo", bolsale.VoucherNo);
                cmd.Parameters.AddWithValue("@Date", bolsale.SaleDate);
                cmd.Parameters.AddWithValue("@CustomerID", bolsale.CustomerID);
                cmd.Parameters.AddWithValue("@UserID", bolsale.UserID);
                cmd.Parameters.AddWithValue("@PaymentType", bolsale.PaymentType);
                cmd.Parameters.AddWithValue("@CurrencyID", bolsale.CurrencyID);
                cmd.Parameters.AddWithValue("@DayLimit", bolsale.DayLimit);
                cmd.Parameters.AddWithValue("@TotalAmt", bolsale.TotalAmt);
                cmd.Parameters.AddWithValue("@Advance", bolsale.Advance);
                cmd.Parameters.AddWithValue("@Discount", bolsale.Discount);
                cmd.Parameters.AddWithValue("@GrandTotal", bolsale.GrandTotal);
                cmd.Parameters.AddWithValue("@PaidAmount", bolsale.PaidAmount);
                cmd.Parameters.AddWithValue("@RefundAmount", bolsale.RefundAmount);
                cmd.Parameters.AddWithValue("@CounterCode", bolsale.CounterID);
                cmd.Parameters.AddWithValue("@TotalFOC", bolsale.TotalFOC);
                cmd.Parameters.AddWithValue("@TotalItemDiscount", bolsale.TotalitemDiscount);
                cmd.Parameters.AddWithValue("@OriginalUserID", bolsale.OriginalUserID);
                cmd.Parameters.AddWithValue("@EditUserID", bolsale.EditUserID);
                cmd.Parameters.AddWithValue("@EditSaleDate", bolsale.EditSaleDate);
                cmd.Parameters.AddWithValue("@ExchangeRate", bolsale.ExchangeRate);
                cmd.Parameters.AddWithValue("@SystemVoucherNo", bolsale.SystemVoucherNo);
                cmd.Parameters.AddWithValue("@LotteryDate", bolsale.LotteryDate);
                cmd.Parameters.AddWithValue("@DrawingTimes", bolsale.DrawingTimes);
                cmd.Parameters.AddWithValue("@LotteryNo", bolsale.LotteryNo);
                cmd.Parameters.AddWithValue("@LocationID", bolsale.LocationID);
                cmd.Parameters.AddWithValue("@action", bolsale.Action);
                cmd.Parameters.AddWithValue("@TransportationAmt", bolsale.TransportationAmt);
                cmd.Parameters.AddWithValue("@Remark",bolsale.Remark);
                isSaved = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return isSaved;
        }
        #endregion

        //Added By KSAung 06Aug2015
        #region "DuplicateVoucher"
        public List<BOLSale> DuplicateVoucher(String voucherno)
        {
            List<BOLSale> txtSystemVoucherNo = new List<BOLSale>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_CheckDuplicateVoucher", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VoucherNo", voucherno);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BOLSale bolsale = new BOLSale();
                        bolsale.SystemVoucherNo = reader["VoucherNo"].ToString();
                        txtSystemVoucherNo.Add(bolsale);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return txtSystemVoucherNo;
        }
        #endregion

        #region "GetMaxSaleID"
        public long GetMaxSaleID(int action, string SystemVoucherNo)
        {
            long maxID = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_GetMaxSaleID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                string str = "";
                str = " Where SystemVoucherNo=N'" + SystemVoucherNo + "'";
                
                cmd.Parameters.AddWithValue("@Str", str);
               // cmd.Parameters.AddWithValue("@action", action);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                object o = new object();
                o = cmd.ExecuteScalar();
                if (o.GetType() == typeof(long))
                {
                    maxID = (long)o;                    
                }

                if (maxID == 0 | maxID == -1)
                {
                    maxID = 1;
                }
                //else
                //{
                //    maxID += 1;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return maxID;
        }
        #endregion

        #region "GetEditEnableDate"
        public long GetEditEnableDate()
        {
            int maxID = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetEditEnableDate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                object o = new object();
                o = cmd.ExecuteScalar();
                if (o.GetType() == typeof(int))
                {
                    maxID = (int)o;
                }

                if (maxID == 0 | maxID == -1)
                {
                    maxID = 1;
                }
                //else
                //{
                //    maxID += 1;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return maxID;
        }
        #endregion

        #region "SelectSaleVoucher"

        public DataSet SelectSaleVoucher(long SaleID, int action, string SysVoucherNo)
        {
            DataSet ds = new DataSet();
            BOLSale bolsale = new BOLSale();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_SaleVoucher", con);
                cmd.CommandType = CommandType.StoredProcedure;
                string str = "";

                if (action == 1)
                {
                    str = " Where s.SaleID=" + SaleID;
                }
                else
                {
                    str = " Where s.SaleID=" + SaleID;
                    //str = " Where s.SystemVoucherNo=N'" + SysVoucherNo + "'";
                }


                cmd.Parameters.AddWithValue("@Str", str);
                cmd.Parameters.AddWithValue("@action", action);
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

        #region "GetSaleDetailList"

        public List<BOLSale> GetSaleDetailList(long saleID,int action)
        {
            List<BOLSale> lstsaledetail = new List<BOLSale>();
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_GetSaleDetailList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SaleID", saleID);
                cmd.Parameters.AddWithValue("@action", action);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BOLSale bolsale = new BOLSale();
                        bolsale.SaleID = Convert.ToInt64(reader["SaleID"].ToString());                        
                        bolsale.SaleDetailID = long.Parse(reader["SaleDetailID"].ToString());
                        bolsale.ItemCode = reader["ItemCode"].ToString();
                        bolsale.Description = reader["Description"].ToString();
                        bolsale.Mtype = reader["Type"].ToString();
                        bolsale.Qty = Int32.Parse(reader["Qty"].ToString());
                        bolsale.SalePrice = Decimal.Parse(reader["SalePrice"].ToString());
                        bolsale.Total = Decimal.Parse(reader["Total"].ToString());
                        bolsale.Charge = Decimal.Parse(reader["Charges"].ToString()); 
                        if (reader["FOC"].ToString() == "True")
                        {
                            bolsale.FOC = true;
                        }
                        else
                        {
                            bolsale.FOC = false;
                        }
                        bolsale.ItemDiscount = Decimal.Parse(reader["ItemDiscount"].ToString());
                        bolsale.ItemDiscountPercent = Int32.Parse(reader["ItemDiscountPercent"].ToString());
                        lstsaledetail.Add(bolsale);
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return lstsaledetail;
        }

        #endregion

        #region "GetSale"

        public List<BOLSale> GetSale(DateTime startdate,DateTime enddate,string voucherno,long customerid,int classid,int categoryid,string itemcode,int userid,long locationid,string paymenttype)
        {
            List<BOLSale> lstsalelist = new List<BOLSale>();
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_GetSaleList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VoucherNo", voucherno);
                cmd.Parameters.AddWithValue("@CustomerID", customerid);
                cmd.Parameters.AddWithValue("@StartDate", startdate);
                cmd.Parameters.AddWithValue("@EndDate", enddate);
                cmd.Parameters.AddWithValue("@ClassID", classid);
                cmd.Parameters.AddWithValue("@CategoryID", categoryid);
                cmd.Parameters.AddWithValue("@ItemCode", itemcode);
                cmd.Parameters.AddWithValue("@UserID", userid);
                cmd.Parameters.AddWithValue("@LocationID", locationid);
                cmd.Parameters.AddWithValue("@PaymentType", paymenttype);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BOLSale bolsale = new BOLSale();
                        bolsale.SaleID = Convert.ToInt64(reader["SaleID"].ToString());
                        bolsale.SaleDate = DateTime.Parse(reader["Date"].ToString());
                        bolsale.VoucherNo = reader["VoucherNo"].ToString();
                        bolsale.CustomerID = reader["ID"].ToString();
                        bolsale.CustomerName = reader["CustomerName"].ToString();
                        bolsale.UserID = Int32.Parse(reader["UserID"].ToString());
                        bolsale.PaymentType = reader["PaymentType"].ToString();
                        bolsale.Currency = reader["Currency"].ToString();
                        bolsale.DayLimit = Int32.Parse(reader["DayLimit"].ToString());
                        bolsale.TotalAmt = Decimal.Parse(reader["TotalAmt"].ToString());
                        bolsale.Advance = Decimal.Parse(reader["Advance"].ToString());
                        bolsale.Discount = Decimal.Parse(reader["Discount"].ToString());
                        bolsale.GrandTotal = Decimal.Parse(reader["GrandTotal"].ToString());
                        bolsale.TotalFOC = Int32.Parse(reader["TotalFOC"].ToString());
                        bolsale.TotalitemDiscount = Decimal.Parse(reader["TotalItemDiscount"].ToString());
                        bolsale.LocationID = long.Parse(reader["SaleID"].ToString());
                        lstsalelist.Add(bolsale);
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return lstsalelist;
        }

        #endregion

        #region "GetSaleBySaleID"

        public List<BOLSale> GetSaleBySaleID(long SaleID, int action,string SysVoucherNo)
        {
            List<BOLSale> lstsalelist = new List<BOLSale>();
            try
            {
                //SysVoucherNo = "N'" + SysVoucherNo + "'";
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_GetSaleBySaleID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                string str = "";

                if (action == 0)
                {
                    str = " Where (s.SaleID=" + SaleID + " OR s.VoucherNo = '" + SaleID + "') ";
                }
                else
                {
                    str = " Where s.SystemVoucherNo=N'" + SysVoucherNo + "'";
                }


                cmd.Parameters.AddWithValue("@Str", str);
                cmd.Parameters.AddWithValue("@action", action);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BOLSale bolsale = new BOLSale();
                        bolsale.SaleID = Convert.ToInt64(reader["SaleID"].ToString());
                        bolsale.SaleDate = DateTime.Parse(reader["Date"].ToString());
                        bolsale.VoucherNo = reader["VoucherNo"].ToString();
                        bolsale.CustomerName = reader["Name"].ToString();
                        bolsale.CustomerID = reader["ID"].ToString();
                        if (action == 1)
                        {
                            bolsale.CustomerID = reader["CustomerID"].ToString();
                        }
                        bolsale.UserID = Int32.Parse(reader["UserID"].ToString());
                        bolsale.PaymentType = reader["PaymentType"].ToString();
                        bolsale.CurrencyID = Int32.Parse(reader["CurrencyID"].ToString());
                        bolsale.DayLimit = Int32.Parse(reader["DayLimit"].ToString());
                        bolsale.TotalAmt = Decimal.Parse(reader["TotalAmt"].ToString());
                        bolsale.Advance = Decimal.Parse(reader["Advance"].ToString());
                        bolsale.Discount = Decimal.Parse(reader["Discount"].ToString());
                        bolsale.GrandTotal = Decimal.Parse(reader["GrandTotal"].ToString());
                        bolsale.TotalFOC = Int32.Parse(reader["TotalFOC"].ToString());
                        bolsale.TotalitemDiscount = Decimal.Parse(reader["TotalItemDiscount"].ToString());
                        bolsale.ExchangeRate = Decimal.Parse(reader["ExchangeRate"].ToString());
                        bolsale.SystemVoucherNo = reader["SystemVoucherNo"].ToString();
                        bolsale.LotteryDate = DateTime.Parse(reader["LotteryDate"].ToString());
                        bolsale.DrawingTimes = reader["DrawingTimes"].ToString();
                        bolsale.LotteryNo = reader["LotteryNo"].ToString();
                        bolsale.LocationID = long.Parse(reader["LocationID"].ToString());
                        bolsale.TransportationAmt = Int32.Parse(reader["TransportationAmt"].ToString());
                        bolsale.Remark = reader["Remark"].ToString();
                        bolsale.PaidAmount = Decimal.Parse(reader["PaidAmount"].ToString());
                        bolsale.RefundAmount = Decimal.Parse(reader["RefundAmount"].ToString());
                        bolsale.CounterID = reader["CounterID"].ToString();

                        lstsalelist.Add(bolsale);
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return lstsalelist;
        }

        #endregion

        #region "UpdateSaleBySaleID"

        public int UpdateSaleBySaleID(BOLSale bolsale)
        {
            int isUpdated=0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_UpdateSale", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@SaleID", bolsale.SaleID);
                cmd.Parameters.AddWithValue("@VoucherNo", bolsale.VoucherNo);
                cmd.Parameters.AddWithValue("@CustomerID", bolsale.CustomerID);
                cmd.Parameters.AddWithValue("@PaymentType", bolsale.PaymentType);
                cmd.Parameters.AddWithValue("@CurrencyID", bolsale.CurrencyID);
                cmd.Parameters.AddWithValue("@DayLimit", bolsale.DayLimit);
                cmd.Parameters.AddWithValue("@TotalAmt", bolsale.TotalAmt);
                cmd.Parameters.AddWithValue("@Advance", bolsale.Advance);
                cmd.Parameters.AddWithValue("@Discount", bolsale.Discount);
                cmd.Parameters.AddWithValue("@GrandTotal", bolsale.GrandTotal);
                cmd.Parameters.AddWithValue("@PaidAmount", bolsale.PaidAmount);
                cmd.Parameters.AddWithValue("@RefundAmount", bolsale.RefundAmount);
                cmd.Parameters.AddWithValue("@CounterCode", bolsale.CounterID);
                cmd.Parameters.AddWithValue("@TotalFOC", bolsale.TotalFOC);
                cmd.Parameters.AddWithValue("@TotalItemDiscount", bolsale.TotalitemDiscount);
                cmd.Parameters.AddWithValue("@EditUserID", bolsale.EditUserID);
                cmd.Parameters.AddWithValue("@EditSaleDate", bolsale.EditSaleDate);
                cmd.Parameters.AddWithValue("@ExchangeRate", bolsale.ExchangeRate);
                cmd.Parameters.AddWithValue("@SystemVoucherNo", bolsale.SystemVoucherNo);
                cmd.Parameters.AddWithValue("@action", bolsale.Action);
                cmd.Parameters.AddWithValue("@LotteryDate", bolsale.LotteryDate);
                cmd.Parameters.AddWithValue("@DrawingTimes", bolsale.DrawingTimes);
                cmd.Parameters.AddWithValue("@LotteryNo", bolsale.LotteryNo);
                cmd.Parameters.AddWithValue("@LocationID", bolsale.LocationID);
                cmd.Parameters.AddWithValue("@Date", bolsale.SaleDate);
                cmd.Parameters.AddWithValue("@TransportationAmt", bolsale.TransportationAmt);
                cmd.Parameters.AddWithValue("@Remark", bolsale.Remark);
                isUpdated = cmd.ExecuteNonQuery();              


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return isUpdated;
        }

        #endregion

        #region "CheckIsSavePrint"
        public BOLUser CheckIsSavePrint(int Userid)
        {
            BOLUser boluser = new BOLUser();
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_ChkIsSavePrint", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("UserID", Userid);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {                       
                        boluser.IsSavePrint = bool.Parse(reader["IsSavePrint"].ToString());
                        boluser.IsmsgforVoucher = bool.Parse(reader["IsmsgforVoucher"].ToString());                       
                       
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return boluser;
        }
        #endregion

        #region "DeleteTempRows"
        public void DeleteTempRows(int action,string systemVoucher)
        {
           try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_DeleteTempRows", con);                
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", action);
                string str = "";
                str = "N'" + systemVoucher + "'";
                cmd.Parameters.AddWithValue("@Str", str);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
               cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
           
        }
        #endregion

        #region "GetTempData"

        public List<BOLSale> GetTempData()
        {
            List<BOLSale> lstsalelist = new List<BOLSale>();
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_GetTempRows", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BOLSale bolsale = new BOLSale();                        
                        bolsale.VoucherNo = reader["VoucherNo"].ToString();
                        bolsale.SystemVoucherNo = reader["SystemVoucherNo"].ToString();
                        lstsalelist.Add(bolsale);
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return lstsalelist;
        }

        #endregion

        #region "ChkSaveTemp"
        public int ChkSaveTemp(string SystemVoucherNo)
        {
            int maxID = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_ChkSaveTemp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SystemVoucherNo", SystemVoucherNo);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();

                maxID = (int)cmd.ExecuteScalar();
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return maxID;
        }
        #endregion

        #region "GetSaleReport"

        public List<BOLSale> GetSaleReport()
        {
            List<BOLSale> lstsalelist = new List<BOLSale>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_Test", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BOLSale bolsale = new BOLSale();
                        bolsale.SaleID = Convert.ToInt64(reader["SaleID"].ToString());
                        bolsale.VoucherNo = reader["VoucherNo"].ToString();
                        bolsale.CustomerID = reader["ID"].ToString();
                        bolsale.CustomerName = reader["Name"].ToString();
                        bolsale.UserID = Int32.Parse(reader["UserID"].ToString());
                        bolsale.UserName = reader["UserName"].ToString();
                        bolsale.PaymentType = reader["PaymentType"].ToString();
                        bolsale.CurrencyID = Int32.Parse(reader["CurrencyID"].ToString());
                        bolsale.DayLimit = Int32.Parse(reader["DayLimit"].ToString());
                        bolsale.TotalAmt = Decimal.Parse(reader["TotalAmt"].ToString());
                        bolsale.Advance = Decimal.Parse(reader["Advance"].ToString());
                        bolsale.Discount = Decimal.Parse(reader["Discount"].ToString());
                        bolsale.GrandTotal = Decimal.Parse(reader["GrandTotal"].ToString());
                        bolsale.TotalFOC = Int32.Parse(reader["TotalFOC"].ToString());
                        bolsale.TotalitemDiscount = Decimal.Parse(reader["TotalItemDiscount"].ToString());

                        lstsalelist.Add(bolsale);
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return lstsalelist;
        }

        #endregion

        #region "SelectSaleVoucherHistory"
        public DataSet SelectSaleVoucherHistory(DateTime startdate, DateTime enddate, string voucherno, long customerid, int classid, int categoryid, string itemcode, int userid, long locationid, string paymenttype)
        {
            DataSet ds = new DataSet();
           
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetSaleList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartDate", startdate);
                cmd.Parameters.AddWithValue("@EndDate", enddate);
                cmd.Parameters.AddWithValue("@VoucherNo", voucherno);
                cmd.Parameters.AddWithValue("@CustomerID", customerid);
                cmd.Parameters.AddWithValue("@ClassID", classid);
                cmd.Parameters.AddWithValue("@CategoryID", categoryid);
                cmd.Parameters.AddWithValue("@ItemCode", itemcode);
                cmd.Parameters.AddWithValue("@UserID", userid);
                cmd.Parameters.AddWithValue("@LocationID", locationid);
                cmd.Parameters.AddWithValue("@PaymentType", paymenttype);
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

        #region "GetSaleDetailReport"
        public DataSet GetSaleDetailReport(DateTime sdate, DateTime edate, string voucherno, long customerid, int classid, int categoryid, string itemcode, string paymenttype)
        {
            DataSet ds = new DataSet();
            BOLSale bolsale = new BOLSale();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetSaleDetailReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartDate", sdate);
                cmd.Parameters.AddWithValue("@EndDate", edate);
                cmd.Parameters.AddWithValue("@VoucherNo", voucherno);
                cmd.Parameters.AddWithValue("@CustomerID", customerid);
                cmd.Parameters.AddWithValue("@ClassID", classid);
                cmd.Parameters.AddWithValue("@CategoryID", categoryid);
                cmd.Parameters.AddWithValue("@ItemCode", itemcode);
                cmd.Parameters.AddWithValue("@PaymentType", paymenttype);
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

        #region "SelectGetAllSaleList"
        public DataSet SelectGetAllSaleList(DateTime startdate, DateTime enddate)
        {
            DataSet ds = new DataSet();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetAllSaleList", con);
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

        #region "SP_GetOnlySaleHistory"
        public DataSet SP_GetOnlySaleHistory(DateTime startdate, DateTime enddate)
        {
            DataSet ds = new DataSet();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetOnlySaleHistory", con);
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

        #region "DeleteSale"
        public void DeleteSale(long SaleID)
        {
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_DeleteSale", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SaleID", SaleID);
                
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

        }
        #endregion

        #region "ChkTransactionForSaleEditDelete"
        public int ChkTransactionForSaleEditDelete(string VoucherNo)
        {
            int chkTrans = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_ChkTransactionForSaleEditDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();

                chkTrans = (int)cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return chkTrans;
        }
        #endregion

        #region "SelectSaleVoucherCredit"

        public DataSet SelectSaleVoucherCredit(string SysVoucherNo)
        {
            DataSet ds = new DataSet();
            BOLSale bolsale = new BOLSale();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_SaleVoucherCredit", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //string str = "";

                //if (action == 1)
                //{
                //    str = " Where s.SaleID=" + SaleID;
                //}
                //else
                //{
                //    str = " Where s.SystemVoucherNo=N'" + SysVoucherNo + "'";
                //}


                cmd.Parameters.AddWithValue("@VoucherNo", SysVoucherNo);
                //cmd.Parameters.AddWithValue("@action", action);
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

        #region "GetMaxVoucherNo"
        public string GetMaxVoucherNo(string tranname,DateTime date)
        {
            long maxVNo = 0; long t = 0; string pre = ""; string voucher = "";
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetMaxVoucherNo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.Parameters.AddWithValue("@TransName", tranname);
                cmd.Parameters.AddWithValue("@Counter", MoeYanFunctions.MoeYanPOS_Helper.counterCode);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                object o = new object();
                o = cmd.ExecuteScalar();
                string tx = o.ToString();
                if (tx.Length > 12)
                {
                    pre = tx.Substring(0, 3);
                    t = long.Parse(tx.Substring(3, 10));
                }
                if (t.GetType() == typeof(long))
                {
                    maxVNo = (long)t;
                }

                if (maxVNo == 0 | maxVNo == -1)
                {
                    maxVNo = 1;
                }
                else
                {
                    maxVNo += 1;
                }
                voucher = pre + maxVNo.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return voucher;
        }
        #endregion

        #region "GetMaxVoucher"
        public string GetMaxVoucher(string tranname)
        {
            long maxVNo = 0; long t = 0; string pre = ""; string voucher = "";
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetMaxVoucher", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TransName", tranname);
                cmd.Parameters.AddWithValue("@Counter", MoeYanFunctions.MoeYanPOS_Helper.counterCode);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                object o = new object();
                o = cmd.ExecuteScalar();                
                voucher = o.ToString().PadLeft(3, '0'); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return voucher;
        }
        #endregion
    }
}
