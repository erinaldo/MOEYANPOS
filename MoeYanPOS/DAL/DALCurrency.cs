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
    class DALCurrency
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr   = MoeYanConfiguration.GetConnection();
        #endregion

        #region "SaveCustomer"
        public int SaveCurrency(BOLCurrency bolcurrency)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_InsertCurrency", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd.Parameters.AddWithValue("@currency", bolcurrency.Currency);
                cmd.Parameters.AddWithValue("@exchangerate", bolcurrency.Exchangerate);
                cmd.Parameters.AddWithValue("@mbccurrencyid", bolcurrency.MBCCurrencyID);
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

        #region "EditCurrency"
        public int EditCurrency(BOLCurrency bolcurrency)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_UpdateCurrency", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd.Parameters.AddWithValue("@ID", bolcurrency.Id);
                cmd.Parameters.AddWithValue("@Currency", bolcurrency.Currency);
                cmd.Parameters.AddWithValue("@ExchangeRate", bolcurrency.Exchangerate);
                cmd.Parameters.AddWithValue("@mbccurrencyid", bolcurrency.MBCCurrencyID);
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

        #region "ShowAllCurrency"
        public List<BOLCurrency> ShowAllCurrency()
        {
            List<BOLCurrency> lstcurrency = new List<BOLCurrency>();
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("sp_showallcurrency", con);
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
                        BOLCurrency bolcurrency = new BOLCurrency();
                        bolcurrency.Id = Int32.Parse( reader["ID"].ToString());
                        bolcurrency.Currency = reader["Currency"].ToString();
                        bolcurrency.Exchangerate = decimal.Parse(reader["exchangerate"].ToString());
                        bolcurrency.MBCCurrencyID = reader["MBC_CurrencyID"].ToString();
                        lstcurrency.Add(bolcurrency);
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
            return lstcurrency;
        }
        #endregion

        #region "GetCurrency"
        public BOLCurrency GetCurrency(int currencyID)
        {
            BOLCurrency bolcurrency = new BOLCurrency();
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_GetCurrency", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", currencyID);

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
                        bolcurrency.Id = Int32.Parse( reader["ID"].ToString());
                        bolcurrency.Currency = reader["Currency"].ToString();
                        bolcurrency.MBCCurrencyID = reader["MBC_Currency"].ToString();
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
            return bolcurrency;
        }
        #endregion

        #region "DeleteCurrency"
        public int DeleteCurrency(int currencyid)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_DeletCurrency", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", currencyid);
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

        #region "GetCurrencyID"
        public int GetCurrencyID()
        {
            int currencyid = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_GetCurrrencyID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                currencyid = Convert.ToInt32( cmd.ExecuteScalar());
                if (currencyid == -1 | currencyid == null)
                {
                    currencyid = 1;
                }
                else
                {
                    currencyid += 1;
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
            return currencyid;
        }
        #endregion
    }
}
