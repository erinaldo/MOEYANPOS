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
    class DALPurchase
    {
        #region "Delclaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string constr = MoeYanConfiguration.GetConnection();
        #endregion

        #region "CheckIsSavePrint"
        public BOLUser CheckIsSavePrint(int Userid)
        {
            BOLUser boluser = new BOLUser();
            try
            {
                con = new SqlConnection(constr);
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

        #region "GetPurchaseByPurchaseID"
        public List<BOLPurchase> GetPurchaseByPurchaseID(long PurchaseID, int action)
        {
            List<BOLPurchase> lstpurchaselist = new List<BOLPurchase>();
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetPurchaseByPurchaseID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@transPurchaseID", PurchaseID);
                cmd.Parameters.AddWithValue("@action", action);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    BOLPurchase bolpurchase = new BOLPurchase();
                    bolpurchase.Purchaseid = long.Parse(reader["PurchaseID"].ToString());
                    bolpurchase.Purchasedate = DateTime.Parse(reader["PurchaseDate"].ToString());
                    bolpurchase.Voucherno = reader["VoucherNo"].ToString();
                    bolpurchase.SupplierID = Int32.Parse(reader["SupplierID"].ToString());
                    bolpurchase.SupplierName = reader["SupplierName"].ToString();
                    bolpurchase.Userid = Int32.Parse(reader["UserID"].ToString());
                    bolpurchase.Paymenttype = reader["PaymentType"].ToString();
                    bolpurchase.Currencyid = Int32.Parse(reader["CurrencyID"].ToString());
                    bolpurchase.Currency = reader["Currency"].ToString();
                    bolpurchase.Daylimit = Int32.Parse(reader["DayLimit"].ToString());
                    bolpurchase.Totalamt = Decimal.Parse(reader["TotalAmt"].ToString());
                    bolpurchase.Advance = Decimal.Parse(reader["Advance"].ToString());
                    bolpurchase.Discount = Decimal.Parse(reader["Discount"].ToString());
                    bolpurchase.Grandtotal = Decimal.Parse(reader["GrandTotal"].ToString());
                    bolpurchase.Totalfoc = Int32.Parse(reader["TotalFOC"].ToString());
                    bolpurchase.Totalitemdiscount = Decimal.Parse(reader["TotalItemDiscount"].ToString());
                    bolpurchase.Exchangerate = Decimal.Parse(reader["ExchangeRate"].ToString());
                    bolpurchase.Systemvoucherno = reader["SystemVoucherNo"].ToString();
                    bolpurchase.Lotterydate = DateTime.Parse(reader["LotteryDate"].ToString());
                    bolpurchase.DrawingTimes = long.Parse(reader["DrawingTimes"].ToString());
                    bolpurchase.Lotteryno = reader["LotteryNo"].ToString();

                    lstpurchaselist.Add(bolpurchase);
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
            return lstpurchaselist;
        }
        #endregion

        #region "GetSupplier"
        public List<BOLSupplier> GetSupplier(string suppliername)
        {
            List<BOLSupplier> lstsupplier = new List<BOLSupplier>();
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetAllSupplier", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", suppliername);

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
                        BOLSupplier bolsupplier = new BOLSupplier();
                        bolsupplier.Supplierid = Int32.Parse(reader["SupplierID"].ToString());
                        bolsupplier.SupplierName = reader["SupplierName"].ToString();
                        bolsupplier.Address = reader["Address"].ToString();
                        bolsupplier.Phone = reader["Phone"].ToString();
                        lstsupplier.Add(bolsupplier);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstsupplier;
        }
        #endregion

        #region "GetPurchaseDetailList"
        public List<BOLPurchase> GetPurchaseDetailList(long purchaseid, string itemcode)
        {
            List<BOLPurchase> lstpurchasedetail = new List<BOLPurchase>();
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetPurchaseDetailList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PurchaseID", purchaseid);
                cmd.Parameters.AddWithValue("@ItemCode", itemcode);


                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    BOLPurchase bolpurchase = new BOLPurchase();
                    bolpurchase.Purchaseid = long.Parse(reader["PurchaseID"].ToString());
                    bolpurchase.Purchasedetailid = long.Parse(reader["PurchaseDetailID"].ToString());
                    bolpurchase.Itemcode = reader["ItemCode"].ToString();
                    bolpurchase.Description = reader["Description"].ToString();
                    bolpurchase.Type = reader["Type"].ToString();
                    bolpurchase.Qty = Int32.Parse(reader["Qty"].ToString());
                    bolpurchase.Purchaseprice = Decimal.Parse(reader["SalePrice"].ToString());

                    if (reader["FOC"].ToString() == "True")
                    {
                        bolpurchase.Foc = true;
                    }
                    else
                    {
                        bolpurchase.Foc = false;
                    }

                    bolpurchase.Itemdiscount = Decimal.Parse(reader["ItemDiscount"].ToString());
                    bolpurchase.Itemdiscountpercent = Int32.Parse(reader["ItemDiscountPercent"].ToString());
                    lstpurchasedetail.Add(bolpurchase);
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
            return lstpurchasedetail;
        }
        #endregion

        #region "SavePurchaseData"
        public int SavePurchaseData(BOLPurchase bolpurchase)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_InsertPurchase", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@PurchaseID", bolpurchase.Purchaseid);
                cmd.Parameters.AddWithValue("@TransPurchaseID", bolpurchase.TransPurchaseID);
                cmd.Parameters.AddWithValue("@VoucherNo", bolpurchase.Voucherno);
                cmd.Parameters.AddWithValue("@SupplierID", bolpurchase.SupplierID);
                cmd.Parameters.AddWithValue("@PurchaseDate", bolpurchase.Purchasedate);
                cmd.Parameters.AddWithValue("@UserID", bolpurchase.Userid);
                cmd.Parameters.AddWithValue("@PaymentType", bolpurchase.Paymenttype);
                cmd.Parameters.AddWithValue("@CurrencyID", bolpurchase.Currencyid);
                cmd.Parameters.AddWithValue("@DayLimit", bolpurchase.Daylimit);
                cmd.Parameters.AddWithValue("@TotalAmt", bolpurchase.Totalamt);
                cmd.Parameters.AddWithValue("@Advance", bolpurchase.Advance);
                cmd.Parameters.AddWithValue("@Discount", bolpurchase.Discount);
                cmd.Parameters.AddWithValue("@GrandTotal", bolpurchase.Grandtotal);
                cmd.Parameters.AddWithValue("@TotalFOC", bolpurchase.Totalfoc);
                cmd.Parameters.AddWithValue("@TotalItemDiscount", bolpurchase.Totalitemdiscount);
                cmd.Parameters.AddWithValue("@OriginalUserID", bolpurchase.Originaluserid);
                cmd.Parameters.AddWithValue("@EditUserID", bolpurchase.Edituserid);
                cmd.Parameters.AddWithValue("@EditPurchaseDate", bolpurchase.Editpurchsedate);
                cmd.Parameters.AddWithValue("@ExchangeRate", bolpurchase.Exchangerate);
                cmd.Parameters.AddWithValue("@SystemVoucherNo", bolpurchase.Systemvoucherno);
                cmd.Parameters.AddWithValue("@LotteryDate", bolpurchase.Lotterydate);
                cmd.Parameters.AddWithValue("@DrawingTimes", bolpurchase.DrawingTimes);
                cmd.Parameters.AddWithValue("@LotteryNo", bolpurchase.Lotteryno);
                cmd.Parameters.AddWithValue("@LocationID", bolpurchase.Locationid);


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

        #region "SavePurchaseDetailData"
        public int SavePurchaseDetailData(BOLPurchase bolpurchasedetail)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_InsertPurchaseDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();

                cmd.Parameters.AddWithValue("@PurchaseID", bolpurchasedetail.Purchaseid);
                cmd.Parameters.AddWithValue("@No", bolpurchasedetail.No);
                cmd.Parameters.AddWithValue("@ItemCode", bolpurchasedetail.Itemcode);
                cmd.Parameters.AddWithValue("@Description", bolpurchasedetail.Description);
                cmd.Parameters.AddWithValue("@Type", bolpurchasedetail.Type);
                cmd.Parameters.AddWithValue("@Qty", bolpurchasedetail.Qty);
                cmd.Parameters.AddWithValue("@PurchasePrice", bolpurchasedetail.Purchaseprice);
                cmd.Parameters.AddWithValue("@Total", bolpurchasedetail.Total);
                cmd.Parameters.AddWithValue("@FOC", bolpurchasedetail.Foc);
                cmd.Parameters.AddWithValue("@ItemDiscount", bolpurchasedetail.Itemdiscount);
                cmd.Parameters.AddWithValue("@ItemDiscountPercent", bolpurchasedetail.Itemdiscountpercent);
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

        #region "SelectItemDiscount"

        public DataSet SelectItemDiscount(long PurchaseID)
        {
            DataSet ds = new DataSet();
            BOLSale bolsale = new BOLSale();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_SelectItemDiscount", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PurchaseID", PurchaseID);

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

        #region "UpdatePurchaseDetailData"
        public int UpdatePurchaseDetailData(BOLPurchase bolpurchasedetail)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_UpdatePurchaseDetailData", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();

                cmd.Parameters.AddWithValue("@PurchaseDetailID", bolpurchasedetail.Purchasedetailid);
                cmd.Parameters.AddWithValue("@ItemCode", bolpurchasedetail.Itemcode);
                cmd.Parameters.AddWithValue("@Description", bolpurchasedetail.Description);
                cmd.Parameters.AddWithValue("@Type", bolpurchasedetail.Type);
                cmd.Parameters.AddWithValue("@Qty", bolpurchasedetail.Qty);
                cmd.Parameters.AddWithValue("@SalePrice", bolpurchasedetail.Purchaseprice);
                cmd.Parameters.AddWithValue("@Total", bolpurchasedetail.Total);
                cmd.Parameters.AddWithValue("@FOC", bolpurchasedetail.Foc);
                cmd.Parameters.AddWithValue("@ItemDiscount", bolpurchasedetail.Discount);
                cmd.Parameters.AddWithValue("@ItemDiscountPercent", bolpurchasedetail.Itemdiscountpercent);
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

        #region "UpdatePurchaseByPurchaseID"
        public int UpdatePurchaseByPurchaseID(BOLPurchase bolpurchase)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_UpdatePurchase", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@PurchaseID", bolpurchase.Purchaseid);
                cmd.Parameters.AddWithValue("@VoucherNo", bolpurchase.Voucherno);
                cmd.Parameters.AddWithValue("@SupplierID", bolpurchase.SupplierID);
                cmd.Parameters.AddWithValue("@PurchaseDate", bolpurchase.Purchasedate);
                cmd.Parameters.AddWithValue("@PaymentType", bolpurchase.Paymenttype);
                cmd.Parameters.AddWithValue("@CurrencyID", bolpurchase.Currencyid);
                cmd.Parameters.AddWithValue("@DayLimit", bolpurchase.Daylimit);
                cmd.Parameters.AddWithValue("@TotalAmt", bolpurchase.Totalamt);
                cmd.Parameters.AddWithValue("@Advance", bolpurchase.Advance);
                cmd.Parameters.AddWithValue("@Discount", bolpurchase.Discount);
                cmd.Parameters.AddWithValue("@GrandTotal", bolpurchase.Grandtotal);
                cmd.Parameters.AddWithValue("@TotalFOC", bolpurchase.Totalfoc);
                cmd.Parameters.AddWithValue("@TotalItemDiscount", bolpurchase.Totalitemdiscount);
                cmd.Parameters.AddWithValue("@OriginalUserID", bolpurchase.Originaluserid);
                cmd.Parameters.AddWithValue("@EditUserID", bolpurchase.Edituserid);
                cmd.Parameters.AddWithValue("@EditPurchaseDate", bolpurchase.Editpurchsedate);
                cmd.Parameters.AddWithValue("@ExchangeRate", bolpurchase.Exchangerate);
                cmd.Parameters.AddWithValue("@SystemVoucherNo", bolpurchase.Systemvoucherno);
                cmd.Parameters.AddWithValue("@LotteryDate", bolpurchase.Lotterydate);
                cmd.Parameters.AddWithValue("@DrawingTimes", bolpurchase.DrawingTimes);
                cmd.Parameters.AddWithValue("@LotteryNo", bolpurchase.Lotteryno);
                cmd.Parameters.AddWithValue("@LocationID", bolpurchase.Locationid);

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

        #region "GetMaxPurchseID"
        public long GetMaxPurchaseID(string SystemVoucherNo)
        {
            long maxid = 0;
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetMaxPurchaseID", con);
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

        #region "SelectPurchaseVoucher"
        public DataSet SelectPurchaseVoucher(long purchaseID, int action)
        {
            DataSet ds = new DataSet();
            BOLPurchase bolpurchase = new BOLPurchase();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_SelectPurchaseVoucher", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PurchaseID", purchaseID);
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

        #region "PurchaseItemTotal"
        public DataSet PurchaseItemTotal(DateTime sdate,DateTime edate)
        {
            DataSet ds = new DataSet();
            BOLPurchase bolitempurchase = new BOLPurchase();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_CalculateItem", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartDate", sdate);
                cmd.Parameters.AddWithValue("@EndDate", edate);

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

        #region "PurchaseVoucher"
        public DataSet PurchaseVoucher(long purchaseid, int action)
        {
            DataSet ds = new DataSet();
            BOLPurchase bolpurchase = new BOLPurchase();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_SelectPurchaseVoucher", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PurchaseID", purchaseid);
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

        #region "PreviewPurchaseData"
        public DataSet PreviewPurchaseData(long purchaseid, int action)
        {
            DataSet ds = new DataSet();
            BOLPurchase bolpurchase = new BOLPurchase();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_SelectPurchaseVoucher", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PurchaseID", purchaseid);
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

        #region "GetPurchase"

        public List<BOLPurchase> GetPurchase(DateTime startdate, DateTime enddate, string voucherno,  int supplierid,int classid,int categoryid,int locaitonid,string ItemCode,int userid)
        {
            List<BOLPurchase> lstpurchaselist = new List<BOLPurchase>();
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetPurchaseList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VoucherNo", voucherno);                
                cmd.Parameters.AddWithValue("@SupplierID", supplierid);
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
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BOLPurchase bolpurchase = new BOLPurchase();
                        bolpurchase.Purchaseid = long.Parse(reader["PurchaseID"].ToString());
                        bolpurchase.Voucherno = reader["VoucherNo"].ToString();
                        bolpurchase.SupplierID = Int32.Parse(reader["SupplierID"].ToString());
                        bolpurchase.SupplierName = reader["SupplierName"].ToString();
                        bolpurchase.Userid = Int32.Parse(reader["UserID"].ToString());
                        bolpurchase.Username = reader["UserName"].ToString();
                        bolpurchase.Paymenttype = reader["PaymentType"].ToString();
                        bolpurchase.Currencyid = Int32.Parse(reader["CurrencyID"].ToString());
                        //bolpurchase.Currency = reader["Currency"].ToString();
                        bolpurchase.Daylimit = Int32.Parse(reader["DayLimit"].ToString());
                        bolpurchase.Totalamt = Decimal.Parse(reader["TotalAmt"].ToString());
                        bolpurchase.Advance = Decimal.Parse(reader["Advance"].ToString());
                        bolpurchase.Discount = Decimal.Parse(reader["Discount"].ToString());
                        bolpurchase.Grandtotal = Decimal.Parse(reader["GrandTotal"].ToString());
                        bolpurchase.Totalfoc = Int32.Parse(reader["TotalFOC"].ToString());
                        bolpurchase.Totalitemdiscount = Decimal.Parse(reader["TotalItemDiscount"].ToString());                       

                        lstpurchaselist.Add(bolpurchase);
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
            return lstpurchaselist;

        }
        #endregion

        #region "GetPurchaseDetailList"

        public List<BOLPurchase> GetPurchaseDetailList(long purchaseid, string itemcode, int action)
        {
            List<BOLPurchase> lstpurchasedetail = new List<BOLPurchase>();
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetPurchaseDetailList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PurchaseID", purchaseid);
                cmd.Parameters.AddWithValue("@ItemCode", itemcode);
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
                        BOLPurchase bolpurchase = new BOLPurchase();
                        bolpurchase.Purchaseid = long.Parse(reader["PurchaseID"].ToString());
                        bolpurchase.Purchasedetailid = long.Parse(reader["PurchaseDetailID"].ToString());
                        bolpurchase.Itemcode = reader["ItemCode"].ToString();
                        bolpurchase.Description = reader["Description"].ToString();
                        bolpurchase.Mtype = reader["Type"].ToString();
                        bolpurchase.Qty = Int32.Parse(reader["Qty"].ToString());
                        bolpurchase.Purchaseprice = Decimal.Parse(reader["PurchasePrice"].ToString());
                        bolpurchase.Total = Decimal.Parse(reader["Total"].ToString());
                        if (reader["FOC"].ToString() == "True")
                        {
                            bolpurchase.Foc = true;
                        }
                        else
                        {
                            bolpurchase.Foc = false;
                        }
                        bolpurchase.Itemdiscount = Decimal.Parse(reader["ItemDiscount"].ToString());
                        bolpurchase.Itemdiscountpercent = Int32.Parse(reader["ItemDiscountPercent"].ToString());
                        lstpurchasedetail.Add(bolpurchase);
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
            return lstpurchasedetail;
        }

        #endregion

        #region "GetPurchaseDetailReport"
        public DataSet GetPurchaseDetailReport(DateTime startdate, DateTime enddate, string voucherno, int supplierid, int classid, int categoryid, int locaitonid, string ItemCode, int userid)
        {
            DataSet ds = new DataSet();
            BOLSale bolsale = new BOLSale();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetPurchaseDetailReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VoucherNo", voucherno);
                cmd.Parameters.AddWithValue("@SupplierID", supplierid);
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

        #region "DeletePurchase"
        public int DeletePurchase(int purchaseid)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_DeletePurchase", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@purchaseid", purchaseid);
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

        #region "DeletePurchaseDetail"
        public void DeletePurchaseDetail(long SaleDetailID, string SystemVoucherNo)
        {
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_DeletePurchaseDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PurchaseDetailID", SaleDetailID);
                cmd.Parameters.AddWithValue("@SystemVoucherNo", SystemVoucherNo);

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

        #region "SelectAllCategory"
        public List<BOLCategory> SelectAllCategory()
        {
            List<BOLCategory> lstcategory = new List<BOLCategory>();
            try
            {
                con = new SqlConnection(constr);
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
                con = new SqlConnection(constr);
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
                con = new SqlConnection(constr);
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

        //#region "GetPurchaseMaxID"
        //public long GetPurchaseMaxID()
        //{
        //    long TransID = 0;
        //    try
        //    {
        //        con = new SqlConnection(constr);
        //        cmd = new SqlCommand("SP_GetPurchaseID", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //        con.Open();
        //        object o = new object();
        //        o = cmd.ExecuteScalar();
        //        if (o.GetType() == typeof(long))
        //        {
        //            TransID = (long)o;
        //        }

        //        if (TransID == 0 | TransID == -1)
        //        {
        //            TransID = 1;
        //        }
        //        else
        //        {
        //            TransID += 1;
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
        //    return TransID;
        //}
        //#endregion

        #region "SelectPurchaseVoucher2"
        public DataSet SelectPurchaseVoucher2(string VoucherNo)
        {
            DataSet ds = new DataSet();
            BOLPurchase bolpurchase = new BOLPurchase();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_SelectPurchaseVoucher_002", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
               // cmd.Parameters.AddWithValue("@action", action);

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
