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
    class DALOpeningStock
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr = MoeYanConfiguration.GetConnection();
        #endregion

        #region "SaveOpeningStock"
        public int SaveOpeningStock(BOLOpeningStock bolOpeningStock,int action)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_InsertOpeningStock", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();    
                cmd.Parameters.AddWithValue("@OpeningDate", bolOpeningStock.OpeningDate);
                cmd.Parameters.AddWithValue("@ItemCode", bolOpeningStock.ItemCode);
                cmd.Parameters.AddWithValue("@Qty", bolOpeningStock.Qty);                
                cmd.Parameters.AddWithValue("@Name", bolOpeningStock.Name);
                cmd.Parameters.AddWithValue("@SalePrice", bolOpeningStock.SalePrice);
                cmd.Parameters.AddWithValue("@PurchasePrice", bolOpeningStock.PurchasePrice);
                cmd.Parameters.AddWithValue("@CurrencyID", bolOpeningStock.CurrencyID);
                cmd.Parameters.AddWithValue("@action", action);
                cmd.Parameters.AddWithValue("@LocationID", bolOpeningStock.LocationID);
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

        #region "ShowAllOpeningStock"
        public DataSet ShowAllOpeningStock(string ItemCode, DateTime StartDate, DateTime EndDate,int ClassID, int CategoryID,int CurrencyID,long LocationID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetOpeningStockHistory", con);
                cmd.Parameters.AddWithValue("@ItemCode", ItemCode);
                cmd.Parameters.AddWithValue("@StartDate", StartDate);
                cmd.Parameters.AddWithValue("@EndDate", EndDate);
                cmd.Parameters.AddWithValue("@ClassID", ClassID);
                cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                cmd.Parameters.AddWithValue("@CurrencyID", CurrencyID);
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

        #region "GetOpeningStockTemp"
        public DataSet GetOpeningStockTemp()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetOpeningStockTemp", con);
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

        #region "GetOpeningStock"
        public BOLOpeningStock GetOpeningStock(long id,int action)
        {
            BOLOpeningStock bolOpeningStock = new BOLOpeningStock();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetOpeningStock", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID",id);
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
                        bolOpeningStock.ID = long.Parse(reader["ID"].ToString());
                        bolOpeningStock.OpeningDate = DateTime.Parse(reader["OpeningDate"].ToString());
                        bolOpeningStock.ItemCode = reader["ItemCode"].ToString();
                        bolOpeningStock.Qty = Int32.Parse(reader["Qty"].ToString());
                        bolOpeningStock.SalePrice = decimal.Parse(reader["SalePrice"].ToString());
                        bolOpeningStock.PurchasePrice = decimal.Parse(reader["PurchasePrice"].ToString());
                        bolOpeningStock.CurrencyID = Int32.Parse(reader["CurrencyID"].ToString());
                        bolOpeningStock.Name = reader["Name"].ToString();
                        bolOpeningStock.LocationID = long.Parse(reader["LocationID"].ToString());
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
            return bolOpeningStock;
        }
        #endregion

        #region "DeleteOpeningStock"
        public int DeleteOpeningStock(long id,int action)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_DeleteOpeningStock", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@action", action);

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

        #region "DeleteOpeningTemp"
        public int DeleteOpeningTemp()
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_DeleteOpeningTemp", con);
                cmd.CommandType = CommandType.StoredProcedure;

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

        #region "UpdateOpeningStock"
        public int UpdateOpeningStock(BOLOpeningStock bolOpeningStock,int action)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_UpdateOpeningStock", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OpeningDate", bolOpeningStock.OpeningDate);
                cmd.Parameters.AddWithValue("@ItemCode", bolOpeningStock.ItemCode);
                cmd.Parameters.AddWithValue("@Qty", bolOpeningStock.Qty);
                cmd.Parameters.AddWithValue("@ID", bolOpeningStock.ID);
                cmd.Parameters.AddWithValue("@Name", bolOpeningStock.Name);
                cmd.Parameters.AddWithValue("@SalePrice", bolOpeningStock.SalePrice);
                cmd.Parameters.AddWithValue("@PurchasePrice", bolOpeningStock.PurchasePrice);
                cmd.Parameters.AddWithValue("@CurrencyID", bolOpeningStock.CurrencyID);
                cmd.Parameters.AddWithValue("@LocationID", bolOpeningStock.LocationID);
                cmd.Parameters.AddWithValue("@action", action);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                
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

        #region "UpdateOpeningStockDate"
        public int UpdateOpeningStockDate(BOLOpeningStock bolOpeningStock)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_UpdateOpeningStockDate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OpeningDate", bolOpeningStock.OpeningDate);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();

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

        #region "ChkSaveOpeningTemp"
        public int ChkSaveOpeningTemp(long OpeningID)
        {
            int maxID = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_ChkSaveOpeningTemp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", OpeningID);
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

        #region "GetTempOpeningStock"
        public DataSet GetTempOpeningStock(long Tempid, int action)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetOpeningStock", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", Tempid);
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

        #region "ChkSaveOpening"
        public int ChkSaveOpening(string Itemcode)
        {
            int count = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_ChkSaveOpening", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ItemCode", Itemcode);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                object o = new object();
                o = cmd.ExecuteScalar();
                if (o.GetType() == typeof(int))
                {
                    count = (int)o;
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
            return count;
        }
        #endregion
    }
}
