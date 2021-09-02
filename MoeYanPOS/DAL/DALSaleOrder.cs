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
    class DALSaleOrder
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr   = MoeYanConfiguration.GetConnection();
        #endregion

       
        public int GetMaxSaleID()
        {
            int maxID = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_SelectSaleOrderID", con);
                cmd.CommandType = CommandType.StoredProcedure;
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

        #region "GetSaleOrder"

        public List<BOLSaleOrder> GetSaleOrder(DateTime startdate, DateTime enddate, string voucherno, long customerid,int userid,int classid,int categoryid,string itemcode,int locationid)
        {
            List<BOLSaleOrder> lstsaleorderlist = new List<BOLSaleOrder>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetSaleOrderList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VoucherNo", voucherno);
                cmd.Parameters.AddWithValue("@CustomerID", customerid);
                cmd.Parameters.AddWithValue("@UserID", userid);
                cmd.Parameters.AddWithValue("@CategoryID", categoryid);
                cmd.Parameters.AddWithValue("@ItemCode", itemcode);
                cmd.Parameters.AddWithValue("@ClassID", classid);
                cmd.Parameters.AddWithValue("@LocationID", locationid);
                cmd.Parameters.AddWithValue("@StartDate", startdate);
                cmd.Parameters.AddWithValue("@EndDate", enddate);
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
                        BOLSaleOrder bolsaleorder = new BOLSaleOrder();
                        bolsaleorder.Saleorderid = long.Parse(reader["SaleOrderID"].ToString());
                        bolsaleorder.Location = reader["Location"].ToString();
                        bolsaleorder.Voucherno = reader["VoucherNo"].ToString();
                        bolsaleorder.Systemvoucherno = long.Parse(reader["SystemVoucherNo"].ToString());
                        bolsaleorder.Cid = Int32.Parse(reader["ID"].ToString());
                        bolsaleorder.Customerid = reader["CustomerID"].ToString();
                        bolsaleorder.Customername = reader["CustomerName"].ToString();
                        bolsaleorder.Userid = Int32.Parse(reader["UserID"].ToString());
                        bolsaleorder.Username = reader["UserName"].ToString();
                        bolsaleorder.Totalamt = Decimal.Parse(reader["TotalAmt"].ToString());
                        bolsaleorder.Locationid = long.Parse(reader["ID"].ToString());

                        lstsaleorderlist.Add(bolsaleorder);
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
            return lstsaleorderlist;

        }
        #endregion

        #region "GetSaleOrderDetailReport"
        public DataSet GetSaleOrderDetailReport(DateTime sdate, DateTime edate, string voucherno, long customerid, int classid, int categoryid, string itemcode)
        {
            DataSet ds = new DataSet();
            BOLSale bolsale = new BOLSale();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetSaleOrderDetailReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartDate", sdate);
                cmd.Parameters.AddWithValue("@EndDate", edate);
                cmd.Parameters.AddWithValue("@VoucherNo", voucherno);
                cmd.Parameters.AddWithValue("@CustomerID", customerid);
                cmd.Parameters.AddWithValue("@ClassID", classid);
                cmd.Parameters.AddWithValue("@CategoryID", categoryid);
                cmd.Parameters.AddWithValue("@ItemCode", itemcode);

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

        //#region "GetSaleOrderDetailList"
        //public DataSet GetSaleOrderDetailList(long SaleOrderID)
        //{
        //    DataSet ds = new DataSet();

        //    try
        //    {
        //        SqlDataAdapter adapter = new SqlDataAdapter();
        //        con = new SqlConnection(Constr);
        //        cmd = new SqlCommand("SP_GetSaleOrderDetailList", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@SaleOrderID", SaleOrderID);
                
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

        #region "GetSaleOrderDetailList"

        public List<BOLSaleOrder> GetSaleOrderDetailList(long saleorderID, int action)
        {
            List<BOLSaleOrder> lstsaleorderdetail = new List<BOLSaleOrder>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("[SP_GetSaleOrderDetailList]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SaleOrderID", saleorderID);
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
                        BOLSaleOrder bolsaleorder = new BOLSaleOrder();
                        bolsaleorder.Saleorderid = long.Parse(reader["SaleOrderID"].ToString());
                        bolsaleorder.Saleorderdetailid = long.Parse(reader["SaleOrderDetailID"].ToString());
                        bolsaleorder.Itemcode = reader["ItemCode"].ToString();
                        bolsaleorder.Description = reader["Description"].ToString();
                        bolsaleorder.Type = reader["Type"].ToString();
                        bolsaleorder.Qty = Int32.Parse(reader["Qty"].ToString());
                        bolsaleorder.Saleprice = Decimal.Parse(reader["SalePrice"].ToString());
                        bolsaleorder.Total = Decimal.Parse(reader["Total"].ToString());
                        
                        lstsaleorderdetail.Add(bolsaleorder);
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
            return lstsaleorderdetail;
        }

        #endregion

        #region "GetSaleOrder"

        public List<BOLSaleOrder> GetSaleOrderList(DateTime startdate, DateTime enddate, string voucherno, int customerid, int classid, int categoryid, string itemcode)
        {
            List<BOLSaleOrder> lstsalelist = new List<BOLSaleOrder>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetSaleOrderList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VoucherNo", voucherno);
                cmd.Parameters.AddWithValue("@CustomerID", customerid);
                cmd.Parameters.AddWithValue("@StartDate", startdate);
                cmd.Parameters.AddWithValue("@EndDate", enddate);
                cmd.Parameters.AddWithValue("@ClassID", classid);
                cmd.Parameters.AddWithValue("@CategoryID", categoryid);
                cmd.Parameters.AddWithValue("@ItemCode", itemcode);
                
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
                        BOLSaleOrder bolsaleorder= new BOLSaleOrder();
                        bolsaleorder.Saleorderid = long.Parse(reader["SaleOrderID"].ToString());
                        bolsaleorder.Voucherno = reader["VoucherNo"].ToString();
                        bolsaleorder.Cid = long.Parse(reader["ID"].ToString());
                        bolsaleorder.Customerid =reader["CustomerID"].ToString();
                        bolsaleorder.Customername = reader["CustomerName"].ToString();
                        bolsaleorder.Userid = Int32.Parse(reader["UserID"].ToString());
                        bolsaleorder.Totalamt = Decimal.Parse(reader["TotalAmt"].ToString());                       

                        lstsalelist.Add(bolsaleorder);
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

        #region "SaveSaleOrderData"
        public int saveSaleOrderData(BOLSaleOrder bolsaleorder)
        {
            int isSaved = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_InsertSaleOrder", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                cmd.Parameters.AddWithValue("@TransSaleOrderID", bolsaleorder.Transsaleorderid);
                cmd.Parameters.AddWithValue("@OrderDate", bolsaleorder.Orderdate);
                cmd.Parameters.AddWithValue("@VoucherNo", bolsaleorder.Voucherno);
                cmd.Parameters.AddWithValue("@CustomerID", bolsaleorder.Customerid);
                cmd.Parameters.AddWithValue("@TotalAmt", bolsaleorder.Totalamt);
                cmd.Parameters.AddWithValue("@userid", bolsaleorder.Userid);
                cmd.Parameters.AddWithValue("@SystemVoucherNo", bolsaleorder.Systemvoucherno);
                cmd.Parameters.AddWithValue("@OriginalUserid", bolsaleorder.Originaluserid);
                cmd.Parameters.AddWithValue("@Edituserid", bolsaleorder.Edituserid);
                cmd.Parameters.AddWithValue("@EditSaleOrderDate", bolsaleorder.Editsaledate);
                cmd.Parameters.AddWithValue("@Remark", bolsaleorder.Remark);
                cmd.Parameters.AddWithValue("@DeliveryDate", bolsaleorder.Deliverydate);
                cmd.Parameters.AddWithValue("@Advance", bolsaleorder.Advance);
                cmd.Parameters.AddWithValue("@Balance", bolsaleorder.Balance);
                cmd.Parameters.AddWithValue("@LocationID", bolsaleorder.Locationid);
                cmd.Parameters.AddWithValue("@PaymentType", bolsaleorder.Paymenttype);

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

        #region "GetMaxSaleOrderID"
        public long GetMaxSaleOrderID(long SystemVoucherNo)
        {
            long maxID = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_SelectSaleOrderID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.AddWithValue("@SystemVoucherNo", SystemVoucherNo);

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

        #region "UpdateSaleOrderBySaleID"

        public int UpdateSaleOrderBySaleID(BOLSaleOrder bolsaleorder)
        {
            int isUpdated = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_UpdateSaleOrder", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@SaleOrderID", bolsaleorder.Saleorderid);
                cmd.Parameters.AddWithValue("@VoucherNo", bolsaleorder.Voucherno);
                cmd.Parameters.AddWithValue("@TransSaleOrderID", bolsaleorder.Transsaleorderid);
                cmd.Parameters.AddWithValue("@OrderDate", bolsaleorder.Orderdate);
                cmd.Parameters.AddWithValue("@DeliveryDate", bolsaleorder.Deliverydate);
                cmd.Parameters.AddWithValue("@CustomerID", bolsaleorder.Customerid);
                cmd.Parameters.AddWithValue("@TotalAmt", bolsaleorder.Totalamt);
                cmd.Parameters.AddWithValue("@EditUserID", bolsaleorder.Edituserid);
                cmd.Parameters.AddWithValue("@EditSaleOrderDate", bolsaleorder.Editsaledate);
                cmd.Parameters.AddWithValue("@SystemVoucherNo", bolsaleorder.Systemvoucherno);
                cmd.Parameters.AddWithValue("@OriginalUserid", bolsaleorder.Originaluserid);
                cmd.Parameters.AddWithValue("@Advance", bolsaleorder.Advance);
                cmd.Parameters.AddWithValue("@Balance", bolsaleorder.Balance);
                cmd.Parameters.AddWithValue("@LocationID", bolsaleorder.Locationid);
                cmd.Parameters.AddWithValue("@Remark", bolsaleorder.Remark);
                cmd.Parameters.AddWithValue("@UserID", bolsaleorder.Userid);

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

        #region "GetSaleOrderBySaleID"

        public List<BOLSaleOrder> GetSaleOrderBySaleID(long SaleOrderID, int action)
        {
            List<BOLSaleOrder> lstsaleorderlist = new List<BOLSaleOrder>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetSaleOrderBySaleOrderID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tranSaleID", SaleOrderID);
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
                        BOLSaleOrder bolsaleorder = new BOLSaleOrder();
                        bolsaleorder.Saleorderid = long.Parse(reader["SaleOrderID"].ToString());
                        bolsaleorder.Orderdate = DateTime.Parse(reader["OrderDate"].ToString());
                        bolsaleorder.Voucherno = reader["VoucherNo"].ToString();
                        bolsaleorder.Customername = reader["Name"].ToString();
                        bolsaleorder.Cid = long.Parse(reader["ID"].ToString());
                        bolsaleorder.Userid = Int32.Parse(reader["UserID"].ToString());
                        bolsaleorder.Totalamt = Decimal.Parse(reader["TotalAmt"].ToString());
                        bolsaleorder.Systemvoucherno = long.Parse(reader["SystemVoucherNo"].ToString());
                        bolsaleorder.Locationid = long.Parse(reader["LocationID"].ToString());

                        lstsaleorderlist.Add(bolsaleorder);
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
            return lstsaleorderlist;
        }

        #endregion

        #region "GetSaleDetailList"

        public List<BOLSaleOrder> GetSaleDetailList(long saleorderID, int action)
        {
            List<BOLSaleOrder> lstsaledetail = new List<BOLSaleOrder>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetSaleOrderDetailList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SaleOrderID", saleorderID);
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
                        BOLSaleOrder bolsaleorder = new BOLSaleOrder();
                        bolsaleorder.Saleorderid = long.Parse(reader["SaleOrderID"].ToString());
                        bolsaleorder.Saleorderdetailid = long.Parse(reader["SaleOrderDetailID"].ToString());
                        bolsaleorder.Itemcode = reader["ItemCode"].ToString();
                        bolsaleorder.Description = reader["Description"].ToString();
                        bolsaleorder.Type = reader["Type"].ToString();
                        bolsaleorder.Qty = Int32.Parse(reader["Qty"].ToString());
                        bolsaleorder.Saleprice = Decimal.Parse(reader["SalePrice"].ToString());
                        bolsaleorder.Total = Decimal.Parse(reader["Total"].ToString());

                        lstsaledetail.Add(bolsaleorder);
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

        #region "SelectSaleoOrderVoucher"
        public DataSet SelectSaleOrderVoucher(long saleorderid, int action)
        {
            DataSet ds = new DataSet();
            BOLSaleOrder bolsaleorder = new BOLSaleOrder();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_SelectSaleOrderVoucher", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SaleOrderID", saleorderid);
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

        #region "DeleteSaleOrder"
        public int DeleteSaleOrder(long id)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_DeleteSaleOrder", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SaleOrderID", id);               

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

        #region "DeleteSaleOrderDetail"
        public void DeleteSaleOrderDetail(long SaleDetailID, string SystemVoucherNo)
        {
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_DeleteSaleOrderDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SaleOrderDetailID", SaleDetailID);
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
    }
}
