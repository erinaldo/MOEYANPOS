using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using MoeYanPOS.BOL;
using MoeYanPOS.Function;

namespace MoeYanPOS.DAL
{
    class DALExchangeRate
    {
        #region "Declaration"
            public SqlConnection con;
            public SqlCommand cmd;
            //public SqlDataReader read;
            string Constr   = MoeYanConfiguration.GetConnection();
        #endregion

        #region "FillCurrency"
            public List<BOLCurrency> FillCurrency()
            {
                List<BOLCurrency> lstcurrency = new List<BOLCurrency>();
                try
                {
                    con = new SqlConnection(Constr  );
                    cmd = new SqlCommand("sp_selectCurrency", con);
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

                            bolcurrency.Id = Int32.Parse(reader["ID"].ToString());
                            bolcurrency.Currency = reader["Currency"].ToString();
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

        #region "SaveExchangerate"
            public int SaveExchangeRate(BOLExchange bolexchange)
            {
                int issaved = 0;
                try
                {
                    con = new SqlConnection(Constr  );
                    cmd = new SqlCommand("sp_InsertExchangeRate", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();

                    cmd.Parameters.AddWithValue("@currencyID", bolexchange.Currency);
                    cmd.Parameters.AddWithValue("@ExchangeRate", bolexchange.Exchangerate);
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

        #region "GetExchangeRate"
            public BOLExchange GetExchangeRate(int currencyid)
            {
                BOLExchange exchangerate = new BOLExchange();
                try
                {
                    con = new SqlConnection(Constr  );
                    cmd = new SqlCommand("SP_GetExchangeRate", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CurrencyID", currencyid);
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
                            //exchangerate.Id = Int32.Parse(reader["ID"].ToString());
                            //exchangerate.Currencyname = reader["Currency"].ToString();
                            exchangerate.Exchangerate = Int32.Parse(reader["ExchangeRate"].ToString());                           

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
                return exchangerate;
            }

            #endregion

        #region "ShowAllExchangeRate"
            public List<BOLExchange> ShowAllExchange()
            {
                List<BOLExchange> lstexchange = new List<BOLExchange>();
                //List<BOLCurrency> lstcurrency = new List<BOLCurrency>();
                try
                {
                    con = new SqlConnection(Constr  );
                    cmd = new SqlCommand("SP_ShowAllExchangeRate", con);
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
                            BOLExchange bolexchange = new BOLExchange();
                            //BOLCurrency bolcurrency = new BOLCurrency();
                            bolexchange.Id = Int32.Parse(reader["ID"].ToString());
                            bolexchange.Currencyname = reader["Currency"].ToString();
                            bolexchange.Exchangerate = Int32.Parse(reader["ExchangeRate"].ToString());
                            lstexchange.Add(bolexchange);
                            
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
                return lstexchange;
            }
        #endregion

        #region "GetExchangeRateforEdit"
            public BOLExchange GetExchangeRateforEdit(int exchangechid)
            {
                BOLExchange bolexchange = new BOLExchange();
                try
                {
                    con = new SqlConnection(Constr  );
                    cmd = new SqlCommand("SP_GetExchangeRateforEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", exchangechid);
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

                            bolexchange.Currency = Int32.Parse( reader["CurrencyID"].ToString());
                            bolexchange.Exchangerate = Int32.Parse(reader["ExchangeRate"].ToString());
                            

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
                return bolexchange;
            }

            #endregion

        #region "DeleteExchangeRate"
            public int DeleteExchangeRate(int exchangeid)
            {
                int isdelete = 0;
                try
                {
                    con = new SqlConnection(Constr  );
                    cmd = new SqlCommand("SP_DeletExchangeRate", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", exchangeid);
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

            #region "UpdateExchangeRate"
            public int UpdateExchangeRate(BOLExchange bolexchange)
            {
                int isupdate = 0;
                try
                {
                    con = new SqlConnection(Constr  );
                    cmd = new SqlCommand("SP_UpdateExcangeRate", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();

                    cmd.Parameters.AddWithValue("@ID", bolexchange.Id);
                    cmd.Parameters.AddWithValue("@currencyID", bolexchange.Currency);
                    cmd.Parameters.AddWithValue("@ExchangeRate", bolexchange.Exchangerate);
                    isupdate=cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return isupdate;
            }
            #endregion
    }
}
