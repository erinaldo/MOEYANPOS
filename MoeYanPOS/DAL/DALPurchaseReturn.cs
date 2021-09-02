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
    class DALPurchaseReturn
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr = MoeYanConfiguration.GetConnection();
        #endregion

        #region "GetPurchaseReturnID"
        public int GetPurchaseReturnID()
        {
            int PurchaseReturnID = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetSaleReturnID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                PurchaseReturnID = (int)cmd.ExecuteScalar();
                if (PurchaseReturnID == -1 | PurchaseReturnID == null)
                {
                    PurchaseReturnID = 1;
                }
                else
                {
                    PurchaseReturnID += 1;
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
            return PurchaseReturnID;
        }
        #endregion

        #region "DeletePurchaseReturn"
        public int DeletePurchaseReturn(int SaleReturnid)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_DeletePurchaseReturn", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PurchaseReturnID", SaleReturnid);
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

        #region "DeletePurchaseReturnDetail"
        public int DeletePurchaseReturnDetail(long PurchaseReturnDetailID)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_DeletePurchaseReturnDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PurchaseReturnDetailID", PurchaseReturnDetailID);
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

        #region "UpdatePurchaseReturn"
        /*public int UpdateSaleReturn(BOLSaleReturn bolsalereturn)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_UpdateSaleReturn", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd.Parameters.AddWithValue("@Date", boluser.Date);
                cmd.Parameters.AddWithValue("@CustomerID", boluser.CustomerID);
                cmd.Parameters.AddWithValue("@VoucherNo", boluser.VoucherNo);
                cmd.Parameters.AddWithValue("@PaymentType", boluser.PaymentType);
                cmd.Parameters.AddWithValue("@CurrencyID", boluser.CurrencyID);
                cmd.Parameters.AddWithValue("@TotalAmt", boluser.TotalAmt);
                cmd.Parameters.AddWithValue("@UserID", boluser.UserID);
                cmd.Parameters.AddWithValue("@SystemVoucherNo", boluser.SystemVoucherNo);

                isupdated = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return isupdated;
        }*/
        #endregion

        #region "GetPurchaseReturnBySaleReturnID"
        public List<BOLPurchaseReturn> GetPurchaseReturnBySaleReturnID(long purchasereturnid, int action)
        {
            List<BOLPurchaseReturn> lstsalelist = new List<BOLPurchaseReturn>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetPurchaseReturnByPurchaseReturnID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@transPurchasereturnID", purchasereturnid);
                cmd.Parameters.AddWithValue("@action", action);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    BOLPurchaseReturn bolsalereturn = new BOLPurchaseReturn();
                    bolsalereturn.Purchasereturnid = long.Parse(reader["PurchaseReturnID"].ToString());
                    bolsalereturn.Date = DateTime.Parse(reader["Date"].ToString());
                    bolsalereturn.Voucherno = reader["VoucherNo"].ToString();
                    bolsalereturn.Supplierid = reader["supplierID"].ToString();
                    bolsalereturn.Cid = long.Parse(reader["supplierID"].ToString());
                    bolsalereturn.Customername = reader["suppliername"].ToString();
                    bolsalereturn.Userid = Int32.Parse(reader["UserID"].ToString());
                    bolsalereturn.Paymenttype = reader["PaymentType"].ToString();
                    bolsalereturn.Currencyid = Int32.Parse(reader["CurrencyID"].ToString());
                    bolsalereturn.Daylimit = Int32.Parse(reader["DayLimit"].ToString());
                    bolsalereturn.Totalamt = Decimal.Parse(reader["Totalamt"].ToString());
                    bolsalereturn.Exchangerate = Decimal.Parse(reader["ExchangeRate"].ToString());
                    bolsalereturn.Systemvoucherno = reader["SystemVoucherNo"].ToString();
                    bolsalereturn.Lotterydate = DateTime.Parse(reader["LotteryDate"].ToString());
                    bolsalereturn.Drawingtimes = long.Parse(reader["DrawingTimes"].ToString());
                    bolsalereturn.Lotteryno = reader["LotteryNo"].ToString();

                    lstsalelist.Add(bolsalereturn);
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

        #region "CheckIsSavePrint"
        public BOLUser CheckIsSavePrint(int Userid)
        {
            BOLUser boluser = new BOLUser();
            try
            {
                con = new SqlConnection(Constr);
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

        #region "PurchaseItem"
        public List<BOLPurchase> SelectPurchaseItem(string systemVoucher, string CustomerID)
        {
            List<BOLPurchase> lstsale = new List<BOLPurchase>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_FillPurchaseReturnItem", con);
                //string str = "";
                //str = "N'" + systemVoucher + "'";
                cmd.Parameters.AddWithValue("@Str", systemVoucher);
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
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
                        BOLPurchase bolsale = new BOLPurchase();
                        bolsale.Purchasedetailid = long.Parse(reader["PurchaseDetailID"].ToString());
                        bolsale.Voucherno = reader["VoucherNo"].ToString();
                        bolsale.Itemcode = reader["ItemCode"].ToString();
                        bolsale.Description = reader["Description"].ToString();
                        bolsale.Qty = Int32.Parse(reader["Qty"].ToString());
                        bolsale.Purchaseprice = Decimal.Parse(reader["PurchasePrice"].ToString());
                        bolsale.Total = Decimal.Parse(reader["Total"].ToString());

                        lstsale.Add(bolsale);
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
            return lstsale;
        }
        #endregion

        #region "InsertPurchaseReturn"
        public int InsertPurchaseReturn(BOLPurchaseReturn bolpurchasereturn)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_InsertPurchaseReturnt", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@tranPurchaseID", bolpurchasereturn.Tranpurchasereturnid);
                cmd.Parameters.AddWithValue("@Date", bolpurchasereturn.Date);
                cmd.Parameters.AddWithValue("@CustomerID", bolpurchasereturn.Cid);
                cmd.Parameters.AddWithValue("@VoucherNo", bolpurchasereturn.Voucherno);
                cmd.Parameters.AddWithValue("@PaymentType", bolpurchasereturn.Paymenttype);
                cmd.Parameters.AddWithValue("@CurrencyID", bolpurchasereturn.Currencyid);
                cmd.Parameters.AddWithValue("@DayLimit", bolpurchasereturn.Daylimit);
                cmd.Parameters.AddWithValue("@ExchangeRate", bolpurchasereturn.Exchangerate);
                cmd.Parameters.AddWithValue("@TotalAmt", bolpurchasereturn.Totalamt);
                cmd.Parameters.AddWithValue("@UserID", bolpurchasereturn.Userid);
                cmd.Parameters.AddWithValue("@SystemVoucherNo", bolpurchasereturn.Systemvoucherno);
                cmd.Parameters.AddWithValue("@LotteryDate", bolpurchasereturn.Lotterydate);
                cmd.Parameters.AddWithValue("@LotteryNo", bolpurchasereturn.Lotteryno);
                cmd.Parameters.AddWithValue("@DrawingTimes", bolpurchasereturn.Drawingtimes);
                cmd.Parameters.AddWithValue("@OriginalUserID", bolpurchasereturn.Originaluserid);
                cmd.Parameters.AddWithValue("@EditUserID", bolpurchasereturn.Edituserid);
                cmd.Parameters.AddWithValue("@EditPurchaseReturnDate", bolpurchasereturn.Editpurchaseretundate);
                cmd.Parameters.AddWithValue("@LocationID", bolpurchasereturn.Locationid);
                cmd.Parameters.AddWithValue("@PurchaseSystemVoucherNO", bolpurchasereturn.SaleSystemVoucherNo);

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

        #region "SP_InsertPurchaseReturnDetail"
        public int InsertPurchaseReturnDetail(BOLPurchaseReturn bolpurchasereturn)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_InsertPurchaseReturnDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@PurchaseReturnID", bolpurchasereturn.Purchasereturnid);
                cmd.Parameters.AddWithValue("@ItemCode", bolpurchasereturn.Itemcode);
                cmd.Parameters.AddWithValue("@Description", bolpurchasereturn.Description);
                cmd.Parameters.AddWithValue("@Type", bolpurchasereturn.Type);
                cmd.Parameters.AddWithValue("@Qty", bolpurchasereturn.Qty);
                cmd.Parameters.AddWithValue("@PurchasePrice", bolpurchasereturn.Purchaseprice);
                cmd.Parameters.AddWithValue("@Total", bolpurchasereturn.Total);
                cmd.Parameters.AddWithValue("@OriginalVoucherNo", bolpurchasereturn.OriginalVoucherNo);

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

        #region "GetMaxPurchaseReturnID"
        public long GetMaxPurchaseReturnID(string SystemVoucherNo)
        {
            long maxid = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetMaxPurchaseReturnID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SystemVoucherNo", SystemVoucherNo);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                object o = new object();
                o = cmd.ExecuteScalar();

                if (o.GetType() == typeof(long))
                {
                    maxid = (long)o;
                }
                if (maxid == 0 | maxid == -1)
                {
                    maxid = 1;
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
            return maxid;
        }
        #endregion

        #region "GetPurchaseReturn"

        public List<BOLPurchaseReturn> GetPurchaseReturn(DateTime startdate, DateTime enddate, string voucherno, int customerid, int userid, int classid, int categoryid, string itemcode, int locationid)
        {
            List<BOLPurchaseReturn> lstpurchasereturnlist = new List<BOLPurchaseReturn>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetPurchaseReturnHistory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VoucherNo", voucherno);
                cmd.Parameters.AddWithValue("@ItemCode", itemcode);
                cmd.Parameters.AddWithValue("@CustomerID", customerid);
                cmd.Parameters.AddWithValue("@ClassID", classid);
                cmd.Parameters.AddWithValue("@CategoryID", categoryid);
                cmd.Parameters.AddWithValue("@StartDate", startdate);
                cmd.Parameters.AddWithValue("@EndDate", enddate);
                cmd.Parameters.AddWithValue("@UserID", userid);

                cmd.Parameters.AddWithValue("@Location", locationid);
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
                        BOLPurchaseReturn bolsalereturn = new BOLPurchaseReturn();
                        bolsalereturn.Purchasereturnid = long.Parse(reader["PurchaseReturnID"].ToString());
                        bolsalereturn.Voucherno = reader["VoucherNo"].ToString();
                        bolsalereturn.Date = DateTime.Parse(reader["Date"].ToString());
                        bolsalereturn.Systemvoucherno = reader["SystemVoucherNo"].ToString();
                        bolsalereturn.Cid = long.Parse(reader["CID"].ToString());
                        bolsalereturn.Supplierid = reader["SupplierID"].ToString();
                        bolsalereturn.Customername = reader["SupplierName"].ToString();
                        bolsalereturn.Userid = Int32.Parse(reader["UserID"].ToString());
                        bolsalereturn.Username = reader["UserName"].ToString();
                        bolsalereturn.Paymenttype = reader["PaymentType"].ToString();
                        bolsalereturn.Currencyid = Int32.Parse(reader["CurrencyID"].ToString());
                        bolsalereturn.Totalamt = Decimal.Parse(reader["TotalAmt"].ToString());
                        //bolsalereturn.Itemcode = reader["ItemCode"].ToString();
                        bolsalereturn.Location = reader["Location"].ToString();

                        lstpurchasereturnlist.Add(bolsalereturn);
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
            return lstpurchasereturnlist;

        }
        #endregion#region "GetSaleReturnDetailList"

        #region "GetPurchaseReturnDetailList"
        public List<BOLPurchaseReturn> GetPurchaseReturnDetailList(long salereturnid, int action)
        {
            List<BOLPurchaseReturn> lstsalereturndetail = new List<BOLPurchaseReturn>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetPurchaseReturnDetailList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PurchaseReturnID", salereturnid);
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
                        BOLPurchaseReturn bolsalereturn = new BOLPurchaseReturn();
                        bolsalereturn.Purchasereturnid = long.Parse(reader["PurchaseReturnID"].ToString());
                        bolsalereturn.Purchasereturndetailid = long.Parse(reader["PurchaseReturnDetailID"].ToString());
                        bolsalereturn.Itemcode = reader["ItemCode"].ToString();
                        bolsalereturn.Description = reader["Description"].ToString();
                        bolsalereturn.Type = reader["Type"].ToString();
                        bolsalereturn.Qty = Int32.Parse(reader["Qty"].ToString());
                        bolsalereturn.Purchaseprice = Decimal.Parse(reader["PurchasePrice"].ToString());
                        bolsalereturn.Total = Decimal.Parse(reader["Total"].ToString());

                        lstsalereturndetail.Add(bolsalereturn);
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
            return lstsalereturndetail;
        }
        #endregion

        #region "UpdatePurchaseReturnBySaleReturnID"
        public int UpdatePurchaseReturnBySaleReturnID(BOLPurchaseReturn bolsalereturn)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_UpdatePurchaseReturn", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@PurchaseReturnID", bolsalereturn.Purchasereturnid);
                cmd.Parameters.AddWithValue("@PurchaseReturnDate", bolsalereturn.Date);
                cmd.Parameters.AddWithValue("@VoucherNo", bolsalereturn.Voucherno);
                cmd.Parameters.AddWithValue("@CustomerID", bolsalereturn.Cid);
                //cmd.Parameters.AddWithValue("@UserID", bolsalereturn.Userid);
                cmd.Parameters.AddWithValue("@PaymentType", bolsalereturn.Paymenttype);
                cmd.Parameters.AddWithValue("@CurrencyID", bolsalereturn.Currencyid);
                cmd.Parameters.AddWithValue("@DayLimit", bolsalereturn.Daylimit);
                cmd.Parameters.AddWithValue("@TotalAmt", bolsalereturn.Totalamt);
                cmd.Parameters.AddWithValue("@OriginalUserID", bolsalereturn.Originaluserid);
                cmd.Parameters.AddWithValue("@EditUserID", bolsalereturn.Edituserid);
                cmd.Parameters.AddWithValue("@EditPurchaseReturnDate", bolsalereturn.Editpurchaseretundate);
                cmd.Parameters.AddWithValue("@ExchangeRate", bolsalereturn.Exchangerate);
                cmd.Parameters.AddWithValue("@SystemVoucherNo", bolsalereturn.Systemvoucherno);
                cmd.Parameters.AddWithValue("@LotteryDate", bolsalereturn.Lotterydate);
                cmd.Parameters.AddWithValue("@DrawingTimes", bolsalereturn.Drawingtimes);
                cmd.Parameters.AddWithValue("@LotteryNo", bolsalereturn.Lotteryno);
                cmd.Parameters.AddWithValue("@LocationID", bolsalereturn.Locationid);
                // cmd.Parameters.AddWithValue("@tranSaleID", bolsalereturn.Transalereturnid);

                isupdated = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return isupdated;
        }
        #endregion

        #region "UpdatePurchaseReturnDetailData"
        public int UpdatePurchaseReturnDetailData(BOLPurchaseReturn bolsalereturndetail)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_UpdatePurchaseReturnDetailData", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();

                cmd.Parameters.AddWithValue("@PurchaseReturnDetailID", bolsalereturndetail.Purchasereturndetailid);
                cmd.Parameters.AddWithValue("@ItemCode", bolsalereturndetail.Itemcode);
                cmd.Parameters.AddWithValue("@Description", bolsalereturndetail.Description);
                cmd.Parameters.AddWithValue("@Type", bolsalereturndetail.Type);
                cmd.Parameters.AddWithValue("@Qty", bolsalereturndetail.Qty);
                cmd.Parameters.AddWithValue("@PurchasePrice", bolsalereturndetail.Purchaseprice);
                cmd.Parameters.AddWithValue("@Total", bolsalereturndetail.Total);
                cmd.Parameters.AddWithValue("@OriginalVoucherNo", bolsalereturndetail.OriginalVoucherNo);

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

        #region "SelectPurchaseReturnVoucher"
        public DataSet SelectPurchaseReturnVoucher(long salereturnid, int action)
        {
            DataSet ds = new DataSet();
            BOLPurchase bolpurchase = new BOLPurchase();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_SelectSaleReturnVoucher ", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SaleReturnID", salereturnid);
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

        #region "PurchaseReturnItemTotal"
        public DataSet PurchaseReturnItemTotal()
        {
            DataSet ds = new DataSet();
            BOLPurchaseReturn bolsalereturnitem = new BOLPurchaseReturn();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_CalculateItem", con);
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

        #region "GetPurchaseReturnDetailReport"
        public DataSet GetPurchaseReturnDetailReport(DateTime startdate, DateTime enddate, string voucherno, int supplierid, int classid, int categoryid, int locaitonid, string ItemCode, int userid)
        {
            DataSet ds = new DataSet();
            BOLPurchaseReturn bolsale = new BOLPurchaseReturn();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetPurchaseReturnDetailReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VoucherNo", voucherno);
                cmd.Parameters.AddWithValue("@CustomerID", supplierid);
                cmd.Parameters.AddWithValue("@StartDate", startdate);
                cmd.Parameters.AddWithValue("@EndDate", enddate);
                cmd.Parameters.AddWithValue("@Location", locaitonid);
                cmd.Parameters.AddWithValue("@CategoryID", categoryid);
                cmd.Parameters.AddWithValue("@ClassID", classid);
                cmd.Parameters.AddWithValue("@UseriD", userid);

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
