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
    class DALSaleReturn
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr   = MoeYanConfiguration.GetConnection();
        #endregion

        #region "SaveSaleReturn"

        /*public int SaveSaleReturn(BOLSaleReturn bolsaleReturn)
        {
            int isSaved = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_InsertSaleReturn", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();

                cmd.Parameters.AddWithValue("@Date", bolsaleReturn.Date);
                cmd.Parameters.AddWithValue("@CustomerID", bolsaleReturn.CustomerID);
                cmd.Parameters.AddWithValue("@VoucherNo", bolsaleReturn.VoucherNo);
                cmd.Parameters.AddWithValue("@PaymentType", bolsaleReturn.PaymentType);
                cmd.Parameters.AddWithValue("@Currency", bolsaleReturn.CurrencyID);
                cmd.Parameters.AddWithValue("@SaleVoucher", bolsaleReturn.SaleVoucher);
                cmd.Parameters.AddWithValue("@TotalAmt", bolsaleReturn.TotalAmt);
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
        }*/

        #endregion

        #region "GetSaleReturnID"
        public int GetSaleReturnID()
        {
            int SaleReturnID = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_GetSaleReturnID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SaleReturnID = (int)cmd.ExecuteScalar();
                if (SaleReturnID == -1 | SaleReturnID == null)
                {
                    SaleReturnID = 1;
                }
                else
                {
                    SaleReturnID += 1;
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
            return SaleReturnID;
        }
        #endregion

        #region "DeleteSaleReturn"
        public int DeleteSaleReturn(int SaleReturnid)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_DeleteSaleReturn", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SaleReturnID", SaleReturnid);
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

        #region "DeleteSaleReturnDetail"
        public int DeleteSaleReturnDetail(long SaleReturnDetailID)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_DeleteSaleReturnDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SaleReturnDetailID", SaleReturnDetailID);
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

        #region "UpdateSaleReturn"
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

        #region "GetSaleReturnBySaleReturnID"
        public List<BOLSaleReturn> GetSaleReturnBySaleReturnID(long salereturnid, int action)
        {
            List<BOLSaleReturn> lstsalelist = new List<BOLSaleReturn>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetSaleReturnBySaleReturnID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@transsalereturnID", salereturnid);
                cmd.Parameters.AddWithValue("@action", action);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    BOLSaleReturn bolsalereturn = new BOLSaleReturn();
                    bolsalereturn.Salereturnid = long.Parse(reader["SaleReturnID"].ToString());
                    bolsalereturn.Date = DateTime.Parse(reader["Date"].ToString());
                    bolsalereturn.Voucherno = reader["VoucherNo"].ToString();
                    bolsalereturn.Customerid = reader["CustomerID"].ToString();
                    bolsalereturn.Cid = long.Parse(reader["ID"].ToString());
                    bolsalereturn.Customername = reader["Name"].ToString();
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
        
        #region "SaleItem"
        public List<BOLSale> SelectSaleItem(string systemVoucher,string CustomerID)
        {
            List<BOLSale> lstsale = new List<BOLSale>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_FillReturnItem", con);
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
                        BOLSale bolsale = new BOLSale();
                        bolsale.SaleDetailID = long.Parse(reader["SaleDetailID"].ToString());
                        bolsale.VoucherNo = reader["VoucherNo"].ToString();
                        bolsale.ItemCode = reader["ItemCode"].ToString();
                        bolsale.Description = reader["Description"].ToString();
                        bolsale.Qty = Int32.Parse(reader["Qty"].ToString());
                        bolsale.SalePrice = Decimal.Parse(reader["SalePrice"].ToString());
                        //bolsale.Charge = Decimal.Parse(reader["Charges"].ToString());
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

        #region "InsertSaleReturn"
        public int InsertSaleReturn(BOLSaleReturn bolsalereturn)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_InsertSaleReturnt", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@tranSaleID",bolsalereturn.Transalereturnid);
                cmd.Parameters.AddWithValue("@Date", bolsalereturn.Date);
                cmd.Parameters.AddWithValue("@CustomerID", bolsalereturn.Cid);
                cmd.Parameters.AddWithValue("@VoucherNo", bolsalereturn.Voucherno);
                cmd.Parameters.AddWithValue("@PaymentType", bolsalereturn.Paymenttype);
                cmd.Parameters.AddWithValue("@CurrencyID", bolsalereturn.Currencyid);
                cmd.Parameters.AddWithValue("@DayLimit", bolsalereturn.Daylimit);
                cmd.Parameters.AddWithValue("@ExchangeRate", bolsalereturn.Exchangerate);
                cmd.Parameters.AddWithValue("@TotalAmt", bolsalereturn.Totalamt);
                cmd.Parameters.AddWithValue("@UserID", bolsalereturn.Userid);
                cmd.Parameters.AddWithValue("@SystemVoucherNo", bolsalereturn.Systemvoucherno);
                cmd.Parameters.AddWithValue("@LotteryDate", bolsalereturn.Lotterydate);
                cmd.Parameters.AddWithValue("@LotteryNo", bolsalereturn.Lotteryno);
                cmd.Parameters.AddWithValue("@DrawingTimes", bolsalereturn.Drawingtimes);
                cmd.Parameters.AddWithValue("@OriginalUserID", bolsalereturn.Originaluserid);
                cmd.Parameters.AddWithValue("@EditUserID", bolsalereturn.Edituserid);
                cmd.Parameters.AddWithValue("@EditSaleReturnDate", bolsalereturn.Editsaleretundate);
                cmd.Parameters.AddWithValue("@LocationID", bolsalereturn.Locationid);
                cmd.Parameters.AddWithValue("@SaleSystemVoucherNO", bolsalereturn.SaleSystemVoucherNo);

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

        #region "SP_InsertSaleReturnDetail"
        public int InsertSaleReturnDetail(BOLSaleReturn bolsalereturn)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_InsertSaleReturnDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@SaleReturnID", bolsalereturn.Salereturnid);
                cmd.Parameters.AddWithValue("@ItemCode", bolsalereturn.Itemcode);
                cmd.Parameters.AddWithValue("@Description", bolsalereturn.Description);
                cmd.Parameters.AddWithValue("@Type", bolsalereturn.Type);
                cmd.Parameters.AddWithValue("@Qty", bolsalereturn.Qty);
                cmd.Parameters.AddWithValue("@SalePrice", bolsalereturn.Saleprice);
                cmd.Parameters.AddWithValue("@Charges", bolsalereturn.Charge);
                cmd.Parameters.AddWithValue("@Total", bolsalereturn.Total);
                cmd.Parameters.AddWithValue("@OriginalVoucherNo", bolsalereturn.OriginalVoucherNo);

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

        #region "GetMaxSaleReturnID"
        public long GetMaxSaleReturnID(string SystemVoucherNo)
        {
            long maxid = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetMaxSaleReturnID", con);
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

        #region "GetMaxPurchaseReturnID"
        public long GetMaxPurchaseReturnID(long SystemVoucherNo)
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

        #region "GetSaleReturn"

        public List<BOLSaleReturn> GetSaleReturn(DateTime startdate, DateTime enddate, string voucherno, int customerid,int userid, int classid, int categoryid, string itemcode,int locationid)
        {
            List<BOLSaleReturn> lstsalereturnlist = new List<BOLSaleReturn>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetSaleReturnHistory", con);
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
                        BOLSaleReturn bolsalereturn = new BOLSaleReturn();
                        bolsalereturn.Salereturnid = long.Parse(reader["SaleReturnID"].ToString());
                        bolsalereturn.Voucherno = reader["VoucherNo"].ToString();
                        bolsalereturn.Date = DateTime.Parse(reader["Date"].ToString());
                        bolsalereturn.Systemvoucherno = reader["SystemVoucherNo"].ToString();
                        bolsalereturn.Cid = long.Parse(reader["CID"].ToString());
                        bolsalereturn.Customerid = reader["CustomerID"].ToString();
                        bolsalereturn.Customername = reader["Name"].ToString();
                        bolsalereturn.Userid = Int32.Parse(reader["UserID"].ToString());
                        bolsalereturn.Username = reader["Name"].ToString();
                        bolsalereturn.Paymenttype = reader["PaymentType"].ToString();
                        bolsalereturn.Currencyid =Int32.Parse(reader["CurrencyID"].ToString());
                        bolsalereturn.Totalamt = Decimal.Parse(reader["TotalAmt"].ToString());
                        //bolsalereturn.Itemcode = reader["ItemCode"].ToString();
                        bolsalereturn.Location = reader["Location"].ToString();

                        lstsalereturnlist.Add(bolsalereturn);
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
            return lstsalereturnlist;

        }
        #endregion#region "GetSaleReturnDetailList"

        #region "GetSaleReturnDetailList"
        public List<BOLSaleReturn> GetSaleReturnDetailList(long salereturnid, int action)
        {
            List<BOLSaleReturn> lstsalereturndetail = new List<BOLSaleReturn>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetSaleReturnDetailList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SaleReturnID", salereturnid);                
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
                        BOLSaleReturn bolsalereturn = new BOLSaleReturn();
                        bolsalereturn.Salereturnid = long.Parse(reader["SaleReturnID"].ToString());
                        bolsalereturn.Salereturndetailid = long.Parse(reader["SaleReturnDetailID"].ToString());
                        bolsalereturn.Itemcode = reader["ItemCode"].ToString();
                        bolsalereturn.Description = reader["Description"].ToString();
                        bolsalereturn.Type = reader["Type"].ToString();
                        bolsalereturn.Qty = Int32.Parse(reader["Qty"].ToString());
                        bolsalereturn.Charge = Decimal.Parse(reader["Charges"].ToString());
                        bolsalereturn.Saleprice = Decimal.Parse(reader["SalePrice"].ToString());
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

        #region "UpdateSaleReturnBySaleReturnID"
        public int UpdateSaleReturnBySaleReturnID(BOLSaleReturn bolsalereturn)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_UpdateSaleReturn", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@SaleReturnID", bolsalereturn.Salereturnid);
                cmd.Parameters.AddWithValue("@SaleReturnDate", bolsalereturn.Date);
                cmd.Parameters.AddWithValue("@VoucherNo", bolsalereturn.Voucherno);
                cmd.Parameters.AddWithValue("@CustomerID", bolsalereturn.Cid);                
                //cmd.Parameters.AddWithValue("@UserID", bolsalereturn.Userid);
                cmd.Parameters.AddWithValue("@PaymentType", bolsalereturn.Paymenttype);
                cmd.Parameters.AddWithValue("@CurrencyID", bolsalereturn.Currencyid);
                cmd.Parameters.AddWithValue("@DayLimit", bolsalereturn.Daylimit);
                cmd.Parameters.AddWithValue("@TotalAmt", bolsalereturn.Totalamt);
                cmd.Parameters.AddWithValue("@OriginalUserID", bolsalereturn.Originaluserid);
                cmd.Parameters.AddWithValue("@EditUserID", bolsalereturn.Edituserid);
                cmd.Parameters.AddWithValue("@EditSaleReturnDate", bolsalereturn.Editsaleretundate);
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

        
        #region "UpdateSaleReturnDetailData"
        public int UpdateSaleReturnDetailData(BOLSaleReturn bolsalereturndetail)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_UpdateSaleReturnDetailData", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();

                cmd.Parameters.AddWithValue("@SaleReturnDetailID", bolsalereturndetail.Salereturndetailid);
                cmd.Parameters.AddWithValue("@ItemCode", bolsalereturndetail.Itemcode);
                cmd.Parameters.AddWithValue("@Description", bolsalereturndetail.Description);
                cmd.Parameters.AddWithValue("@Type", bolsalereturndetail.Type);
                cmd.Parameters.AddWithValue("@Qty", bolsalereturndetail.Qty);
                cmd.Parameters.AddWithValue("@SalePrice", bolsalereturndetail.Saleprice);
                cmd.Parameters.AddWithValue("@Charges", bolsalereturndetail.Charge);
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

        #region "SelectSaleReturnVoucher"
        public DataSet SelectSaleReturnVoucher(long salereturnid, int action)
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

        #region "SaleReturnItemTotal"
        public DataSet SaleReturnItemTotal()
        {
            DataSet ds = new DataSet();
            BOLSaleReturn bolsalereturnitem = new BOLSaleReturn();
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

        #region "SelectAllCategory"
        public List<BOLCategory> SelectAllCategory()
        {
            List<BOLCategory> lstcategory = new List<BOLCategory>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_SelectAllCategory", con);
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
                        BOLCategory bolcategory = new BOLCategory();
                        bolcategory.Id = Int32.Parse(reader["ID"].ToString());
                        bolcategory.CategoryName = reader["CategoryName"].ToString();
                        lstcategory.Add(bolcategory);
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
            return lstcategory;
        }
        #endregion

        #region "SelectAllUser"
        public List<BOLUser> SelectAllUser()
        {
            List<BOLUser> lstuser = new List<BOLUser>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_SelectUser", con);
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
                        BOLUser boluser = new BOLUser();
                        boluser.UserID = Int32.Parse(reader["UserID"].ToString());
                        boluser.UserName = reader["UserName"].ToString();

                        lstuser.Add(boluser);
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
            return lstuser;
        }
        #endregion

        #region "SelectAllClass"
        public List<BOLClass> SelectAllClass()
        {
            List<BOLClass> lstclass = new List<BOLClass>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_SelectAllClass", con);
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
                        BOLClass bolclass = new BOLClass();
                        bolclass.Id = Int32.Parse(reader["ID"].ToString());
                        bolclass.ClassName = reader["ClassName"].ToString();

                        lstclass.Add(bolclass);
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
            return lstclass;
        }
        #endregion

        #region "GetSaleReturnDetailReport"
        public DataSet GetSaleReturnDetailReport(DateTime startdate, DateTime enddate, string voucherno, int supplierid, int classid, int categoryid, int locaitonid, string ItemCode, int userid)
        {
            DataSet ds = new DataSet();
            BOLSaleReturn bolsale = new BOLSaleReturn();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetSaleReturnDetailReport", con);
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
        