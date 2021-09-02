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
    class DALAdjustmentType
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr = MoeYanConfiguration.GetConnection();
        #endregion

        #region "GetAdjustmentType"
        public int GetAdjustmentType()
        {
            int brandid = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetAdjustmentTypeID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                brandid = (int)cmd.ExecuteScalar();
                if (brandid == -1 | brandid == null)
                {
                    brandid = 1;
                }
                else
                {
                    brandid += 1;
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
            return brandid;
        }
        #endregion

        #region "SaveAdjustmentType"
        public int SaveAdjustmentType(BOLAdjustmentType bolAdjustmentType)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_InsertAdjustmentType", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@id", bolAdjustmentType.ID);
                cmd.Parameters.AddWithValue("@AdjustmentType", bolAdjustmentType.AdjustmentType);
                cmd.Parameters.AddWithValue("@Header", bolAdjustmentType.Header);
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

        #region "ShowAllAdjustmentType"
        public List<BOLAdjustmentType> ShowAllAdjustmentType()
        {
            List<BOLAdjustmentType> lstAdjustmentType = new List<BOLAdjustmentType>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetAllAdjustmentType", con);
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
                        BOLAdjustmentType bolAdjustmentType = new BOLAdjustmentType();
                        bolAdjustmentType.ID = Int32.Parse(reader["ID"].ToString());
                        bolAdjustmentType.AdjustmentType = reader["AdjustmentType"].ToString();
                        bolAdjustmentType.Header = reader["Header"].ToString();
                        lstAdjustmentType.Add(bolAdjustmentType);
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
            return lstAdjustmentType;
        }
        #endregion

        #region "GetAdjustmentType"
        public BOLAdjustmentType GetAdjustmentType(int ID)
        {
            BOLAdjustmentType bolAdjustmentType = new BOLAdjustmentType();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetAdjustmentType", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", bolAdjustmentType.ID);

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
                        bolAdjustmentType.ID = Int32.Parse(reader["ID"].ToString());
                        bolAdjustmentType.AdjustmentType = reader["AdjustmentType"].ToString();
                        bolAdjustmentType.Header = reader["Header"].ToString();                        
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
            return bolAdjustmentType;
        }
        #endregion

        #region "DeleteAdjustmentType"
        public int DeleteAdjustmentType(int id)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_DeleteAdjustmentType", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
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

        #region "UpdateAdjustmentType"
        public int UpdateAdjustmentType(BOLAdjustmentType bolAdjustmentType)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_UpdateAdjustmentType", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@ID", bolAdjustmentType.ID);
                cmd.Parameters.AddWithValue("@AdjustmentType", bolAdjustmentType.AdjustmentType);
                cmd.Parameters.AddWithValue("@Header", bolAdjustmentType.Header);

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

        #region "DuplicateAdjustmentType"
        public BOLAdjustmentType DuplicateAdjustmentType(String name)
        {
            BOLAdjustmentType bolBOLAdjustmentType = new BOLAdjustmentType();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_DuplicateAdjustmentType", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Header", name);

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
                        bolBOLAdjustmentType.Header = reader["Header"].ToString();
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
            return bolBOLAdjustmentType;
        }
        #endregion

        #region "CheckAdjustmentType"
        public string CheckAdjustmentType(int ID)
        {
            string adjustmentType = "";
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_CheckAdjustmentType", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);

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
                        adjustmentType = reader["AdjustmentType"].ToString();
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
            return adjustmentType;
        }
        #endregion

        #region "DuplicateAdjustmentTypeforUpdate"
        public BOLAdjustmentType DuplicateAdjustmentTypeforUpdate(String name, int id)
        {
            BOLAdjustmentType bolAdjustmentType = new BOLAdjustmentType();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_DuplicateAdjustmentTypeforUpdate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Header", name);
                cmd.Parameters.AddWithValue("@ID", id);

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
                        bolAdjustmentType.Header = reader["Header"].ToString();
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
            return bolAdjustmentType;
        }
        #endregion
    }
}
