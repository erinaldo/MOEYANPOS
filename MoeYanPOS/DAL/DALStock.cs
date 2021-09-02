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
    class DALStock
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr   = MoeYanConfiguration.GetConnection();
        #endregion

        #region "SaveStock"

        public int SaveStock(BOLStock bolstock)
        {
            int isSaved = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_InsertStock", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();

                cmd.Parameters.AddWithValue("@ID", bolstock.Id);
                //cmd.Parameters.AddWithValue("@ClassID", bolstock.ClassID);
                cmd.Parameters.AddWithValue("@CategoryID", bolstock.CategoryID);
                cmd.Parameters.AddWithValue("@BrandID", bolstock.BrandID);
                cmd.Parameters.AddWithValue("@ItemCode", bolstock.ItemCode);
                cmd.Parameters.AddWithValue("@NameEng", bolstock.NameEng);
                cmd.Parameters.AddWithValue("@NameMM", bolstock.NameMM);
                cmd.Parameters.AddWithValue("@Price", bolstock.Price);
                cmd.Parameters.AddWithValue("@MinQty", bolstock.MinQty);
                cmd.Parameters.AddWithValue("@MaxQty", bolstock.MaxQty);
                cmd.Parameters.AddWithValue("@TypeID", bolstock.TypeID);
                cmd.Parameters.AddWithValue("@IsStock", bolstock.IsStock);
                cmd.Parameters.AddWithValue("@InActive", bolstock.InActive);
                cmd.Parameters.AddWithValue("@action", bolstock.Action);
                cmd.Parameters.AddWithValue("@PurchasePrice", bolstock.PurchasePrice);
                cmd.Parameters.AddWithValue("@WholeSalePrice", bolstock.WholeSalePrice);
                cmd.Parameters.AddWithValue("@Charges", bolstock.Charges);
                cmd.Parameters.AddWithValue("@UnitQty", bolstock.UnitQty);
                cmd.Parameters.AddWithValue("@ItemBarCode", bolstock.ItemBarCode);
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

        #region "SelectStock"
        public List<BOLStock> SelectStock(string str,int catid,string itemcode,int classid)
        {
            List<BOLStock> lststock = new List<BOLStock>();
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_GetAllStock", con);
                cmd.Parameters.AddWithValue("@str", str);
                cmd.Parameters.AddWithValue("@CategoryID", catid);
                cmd.Parameters.AddWithValue("@ItemCode", itemcode);
                cmd.Parameters.AddWithValue("@ClassID", classid);

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
                        
                        BOLStock bolstock = new BOLStock();
                        bolstock.Id = Int32.Parse(reader["ID"].ToString());
                        bolstock.ClassID = Int32.Parse(reader["ClassID"].ToString());
                        bolstock.CategoryID = Int32.Parse(reader["CategoryID"].ToString());
                        bolstock.TypeID = Int32.Parse(reader["TypeID"].ToString());
                        bolstock.MinQty = Int32.Parse(reader["MinQty"].ToString());
                        bolstock.MaxQty = Int32.Parse(reader["MaxQty"].ToString());                        
                        bolstock.BrandID = Int32.Parse(reader["BrandID"].ToString());
                        bolstock.ItemCode = reader["ItemCode"].ToString();
                        bolstock.NameEng = reader["NameEng"].ToString();
                        bolstock.NameMM = reader["NameMM"].ToString();
                        bolstock.Price = decimal.Parse(reader["Price"].ToString());
                        bolstock.Charges = decimal.Parse(reader["Charges"].ToString()); 
                        bolstock.IsStock = Boolean.Parse(reader["IsStock"].ToString());
                        bolstock.InActive = Boolean.Parse(reader["InActive"].ToString());
                        bolstock.WholeSalePrice = decimal.Parse(reader["WholeSalePrice"].ToString());
                        bolstock.PurchasePrice = decimal.Parse(reader["PurchasePrice"].ToString());
                        bolstock.UnitQty = Int32.Parse(reader["UnitQty"].ToString());
                        bolstock.ItemBarCode = reader["ItemBarCode"].ToString();
                        bolstock.MBCTypeID = reader["MBC_MeasurementID"].ToString();
                        lststock.Add(bolstock);
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
            return lststock;
        }
        #endregion

        #region "SearchStock"
        public List<BOLStock> SearchStock(string str)
        {
            List<BOLStock> lststock = new List<BOLStock>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_SearchStock", con);
                cmd.Parameters.AddWithValue("@str", str);

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

                        BOLStock bolstock = new BOLStock();
                        bolstock.Id = Int32.Parse(reader["ID"].ToString());
                        bolstock.ClassID = Int32.Parse(reader["ClassID"].ToString());
                        bolstock.CategoryID = Int32.Parse(reader["CategoryID"].ToString());
                        bolstock.TypeID = Int32.Parse(reader["TypeID"].ToString());
                        bolstock.MinQty = Int32.Parse(reader["MinQty"].ToString());
                        bolstock.MaxQty = Int32.Parse(reader["MaxQty"].ToString());
                        bolstock.BrandID = Int32.Parse(reader["BrandID"].ToString());
                        bolstock.ItemCode = reader["ItemCode"].ToString();
                        bolstock.NameEng = reader["NameEng"].ToString();
                        bolstock.NameMM = reader["NameMM"].ToString();
                        bolstock.Price = decimal.Parse(reader["Price"].ToString());
                        bolstock.Charges = decimal.Parse(reader["Charges"].ToString());
                        bolstock.IsStock = Boolean.Parse(reader["IsStock"].ToString());
                        bolstock.InActive = Boolean.Parse(reader["InActive"].ToString());
                        bolstock.WholeSalePrice = decimal.Parse(reader["WholeSalePrice"].ToString());
                        bolstock.PurchasePrice = decimal.Parse(reader["PurchasePrice"].ToString());
                        bolstock.UnitQty = Int32.Parse(reader["UnitQty"].ToString());
                        bolstock.ItemBarCode = reader["ItemBarCode"].ToString();
                        bolstock.MBCTypeID = reader["MBC_MeasurementID"].ToString();
                        lststock.Add(bolstock);
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
            return lststock;
        }
        #endregion

        #region "CheckStock"
        public int CheckStock(string str)
        {
            int stkcount = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_ChkStockItemCode", con);
                cmd.Parameters.AddWithValue("@ItemCode", str);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                stkcount = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return stkcount;
        }
        #endregion

        #region "CheckStockName"
        public int CheckStockName(string str)
        {
            int stkcount = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_ChkStockItemName", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Itemname", str);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                stkcount = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return stkcount;
        }
        #endregion

        #region "GetStockBalance"

        public DataSet GetStockBalance(DateTime StartDate, DateTime EndDate,int CatID,int ClassID,string ItemCode,string BalanceType,int Value,long LocationID)
        {
            DataSet ds = new DataSet();
            BOLSale bolsale = new BOLSale();
            try
            {
                string Str = ""; string ConditionStr = "";

                if (CatID != 0)
                {
                    Str += " And cat.ID= " + CatID;
                }

                if (ClassID != 0)
                {                   
                  Str += " And c.ID= " + ClassID;                  
                }

                if (ItemCode != "")
                {
                    Str += " And stk.ItemCode='" + ItemCode + "'";                 
                }

                if (LocationID != 0)
                {
                    Str += " And l.ID='" + LocationID + "'";
                }

                if (BalanceType != "")
                {
                    ConditionStr = BalanceType; //" Where Qty" +
                }

                if (Value != 0)
                {
                    ConditionStr = " Where Qty >=" + Value;
                }

                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetStockBalance", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartDate", StartDate);
                cmd.Parameters.AddWithValue("@EndDate", EndDate);
                cmd.Parameters.AddWithValue("@Str", Str);
                cmd.Parameters.AddWithValue("@ConditionStr", ConditionStr);

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

        #region "GetAllStockByCategory"
        public List<BOLStock> GetAllStockByCategory(int catid)
        {
            List<BOLStock> lststock = new List<BOLStock>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetAllStockByCategory", con);
                cmd.Parameters.AddWithValue("@CategoryID", catid);
                
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

                        BOLStock bolstock = new BOLStock();
                        bolstock.Id = Int32.Parse(reader["ID"].ToString());
                        bolstock.ClassID = Int32.Parse(reader["ClassID"].ToString());
                        bolstock.CategoryID = Int32.Parse(reader["CategoryID"].ToString());
                        bolstock.TypeID = Int32.Parse(reader["TypeID"].ToString());
                        bolstock.MinQty = Int32.Parse(reader["MinQty"].ToString());
                        bolstock.MaxQty = Int32.Parse(reader["MaxQty"].ToString());
                        bolstock.BrandID = Int32.Parse(reader["BrandID"].ToString());
                        bolstock.ItemCode = reader["ItemCode"].ToString();
                        bolstock.NameEng = reader["NameEng"].ToString();
                        bolstock.NameMM = reader["NameMM"].ToString();
                        bolstock.Price = decimal.Parse(reader["Price"].ToString());
                        bolstock.IsStock = Boolean.Parse(reader["IsStock"].ToString());
                        bolstock.InActive = Boolean.Parse(reader["InActive"].ToString());
                        bolstock.WholeSalePrice = decimal.Parse(reader["WholeSalePrice"].ToString());
                        bolstock.PurchasePrice = decimal.Parse(reader["PurchasePrice"].ToString());
                        lststock.Add(bolstock);
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
            return lststock;
        }
        #endregion

        #region "GetAllStockByItemCode"
        public List<BOLStock> GetAllStockByItemCode(string ItemCode)
        {
            List<BOLStock> lststock = new List<BOLStock>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetAllStockByItemCode", con);
                cmd.Parameters.AddWithValue("@ItemCode", ItemCode);

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

                        BOLStock bolstock = new BOLStock();
                        bolstock.Id = Int32.Parse(reader["ID"].ToString());
                        bolstock.ClassID = Int32.Parse(reader["ClassID"].ToString());
                        bolstock.CategoryID = Int32.Parse(reader["CategoryID"].ToString());
                        bolstock.TypeID = Int32.Parse(reader["TypeID"].ToString());
                        bolstock.MinQty = Int32.Parse(reader["MinQty"].ToString());
                        bolstock.MaxQty = Int32.Parse(reader["MaxQty"].ToString());
                        bolstock.BrandID = Int32.Parse(reader["BrandID"].ToString());
                        bolstock.ItemCode = reader["ItemCode"].ToString();
                        bolstock.NameEng = reader["NameEng"].ToString();
                        bolstock.NameMM = reader["NameMM"].ToString();
                        bolstock.Price = decimal.Parse(reader["Price"].ToString());
                        bolstock.IsStock = Boolean.Parse(reader["IsStock"].ToString());
                        bolstock.InActive = Boolean.Parse(reader["InActive"].ToString());
                        bolstock.WholeSalePrice = decimal.Parse(reader["WholeSalePrice"].ToString());
                        bolstock.PurchasePrice = decimal.Parse(reader["PurchasePrice"].ToString());
                        lststock.Add(bolstock);
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
            return lststock;
        }
        #endregion

        #region "GetStockHistory"

        public DataSet GetStockHistory(DateTime StartDate, DateTime EndDate, int ClassID, int CatID, long LocationID, string ItemCode)
        {
            DataSet ds = new DataSet();
            BOLSale bolsale = new BOLSale();
            try
            {
                //string Str = ""; string ConditionStr = "";

                //ConditionStr += " Where Date >= CONVERT(VARCHAR(max),''" + StartDate + "'', 25) And Date <= CONVERT(VARCHAR(max),''" + EndDate + "'', 25)";

                //if (CatID != 0)
                //{
                //    Str += " And cat.ID= " + CatID;
                //}

                //if (ClassID != 0)
                //{
                //    Str += " And c.ID= " + ClassID;
                //}

                //if (ItemCode != "")
                //{
                //    Str += " And stk.ItemCode='" + ItemCode + "'";
                //}

                //if (LocationID != 0)
                //{
                //    Str += " And l.ID='" + LocationID + "'";
                //}

                //if (CatID != 0)
                //{
                //    ConditionStr += " And CategoryID= " + CatID;
                //}

                //if (ClassID != 0)
                //{
                //    ConditionStr += " And ClassID= " + ClassID;
                //}

                //if (ItemCode != "")
                //{
                //    ConditionStr += " And ItemCode='" + ItemCode + "'";
                //}

                //if (LocationID != 0)
                //{
                //    ConditionStr += " And LocationID='" + LocationID + "'";
                //}

                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_StockHistoryByItemCode", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartDate", StartDate);
                cmd.Parameters.AddWithValue("@EndDate", EndDate);
                cmd.Parameters.AddWithValue("@ClassID", ClassID);
                cmd.Parameters.AddWithValue("@CategoryID", CatID);
                cmd.Parameters.AddWithValue("@LocationID", LocationID);
                cmd.Parameters.AddWithValue("@Itemcode", ItemCode);

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

        #region "UpdateStockPrice"

        public int UpdateStockPrice(BOLStock bolstock,bool IsWholeSale)
        {
            int isSaved = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_UpdateStockPrice", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();                
                cmd.Parameters.AddWithValue("@ItemCode", bolstock.ItemCode);                
                cmd.Parameters.AddWithValue("@Price", bolstock.Price);
                cmd.Parameters.AddWithValue("@WholeSalePrice", bolstock.WholeSalePrice);
                cmd.Parameters.AddWithValue("@IsWholeSale",IsWholeSale);

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

        #region "GetStockReport"

        public DataSet GetStockReport(string Str)
        {
            DataSet ds = new DataSet();
            BOLSale bolsale = new BOLSale();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetStockReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Str", Str);
               
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
