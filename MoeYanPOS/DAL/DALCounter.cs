using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoeYanPOS.BOL;
using System.Data.SqlClient;
using System.Data;
using MoeYanPOS.Function;

namespace MoeYanPOS.DAL
{
    class DALCounter
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr = MoeYanConfiguration.GetConnection();
        #endregion

        #region "SaveCounter"
        public int SaveCounter(BOLCounter bolcounter)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_InsertCounter", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd.Parameters.AddWithValue("@code", bolcounter.Code);
                cmd.Parameters.AddWithValue("@name", bolcounter.Name);
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

        #region "GetCounter"
        public BOLCounter GetCounter(string code)
        {
            BOLCounter bolcounter = new BOLCounter();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetCounterByCode", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd.Parameters.AddWithValue("@code", code);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        bolcounter.Name = reader["Description"].ToString();                                               
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
            return bolcounter;
        }
        #endregion

        #region "GetCounterID"
        public int GetCounterID()
        {
            int counterid = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetCounterID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                counterid = (int)cmd.ExecuteScalar();
                if (counterid == -1 | counterid == 0)
                {
                    counterid = 1;
                }
                else
                {
                    counterid += 1;
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
            return counterid;
        }
        #endregion

        #region "SelectAllcounter"
        public List<BOLCounter> SelectAllCounter()
        {
            List<BOLCounter> lstcounter = new List<BOLCounter>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_ShowAllCounter", con);
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
                        BOLCounter bolcounter = new BOLCounter();
                        bolcounter.Id = Int32.Parse(reader["ID"].ToString());
                        bolcounter.Code = reader["Code"].ToString();
                        bolcounter.Name = reader["Description"].ToString();
                        bolcounter.IsthisLocation = Convert.ToBoolean(reader["IsThisCounter"].ToString());
                        bolcounter.IsDelete = Convert.ToBoolean(reader["IsDelete"].ToString());
                        lstcounter.Add(bolcounter);
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
            return lstcounter;
        }
        #endregion

        #region "UpdateCounter"
        public int updateCounter(BOLCounter bolcounter)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_UpdateCounter", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd.Parameters.AddWithValue("@code", bolcounter.Code);
                cmd.Parameters.AddWithValue("@name", bolcounter.Name);
                cmd.Parameters.AddWithValue("@isthiscounter", bolcounter.IsthisLocation);
                cmd.Parameters.AddWithValue("@isdelete", bolcounter.IsDelete);

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

        #region "DeleteCounter"
        public int DeleteCounter(string code)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_DeleteCounter", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@code", code);
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

        #region "CheckCounterStatus"
        public bool CheckCounterStatus(string Code)
        {
            BOLCounter bolcounter = new BOLCounter();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_CheckCounterStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd.Parameters.AddWithValue("@code", Code);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        bolcounter.IsthisLocation = Convert.ToBoolean(reader["IsThisCounter"].ToString());
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
            return bolcounter.IsthisLocation;
        }
        #endregion
    }
}
