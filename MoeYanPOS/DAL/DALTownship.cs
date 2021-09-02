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
    class DALTownship
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string constr = MoeYanConfiguration.GetConnection();
        #endregion       

        #region "SaveTownship"
        public int SaveTownship(BOLTownship bolTownship)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_InsertTownship", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd.Parameters.AddWithValue("@DivisionID", bolTownship.DivisionID);
                cmd.Parameters.AddWithValue("@Township", bolTownship.Township);
                cmd.Parameters.AddWithValue("@MBCTownshipID", bolTownship.MBCTownshipID);
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

        #region "ShowAllTownship"
        public DataSet ShowAllTownship()
        {
            DataSet ds = new DataSet();
            List<BOLTownship> lstTownship = new List<BOLTownship>();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_ShowAllTownship", con);
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

        #region "DeleteTownship"
        public int DeleteTownship(int id)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_DeleteTownship", con);
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

        #region "UpdateTownship"
        public int UpdateTownship(BOLTownship bolTownship)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_UpdateTownship", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@id", bolTownship.Id);
                cmd.Parameters.AddWithValue("@DivisionID", bolTownship.DivisionID);
                cmd.Parameters.AddWithValue("@Township", bolTownship.Township);
                cmd.Parameters.AddWithValue("@MBCTownshipID", bolTownship.MBCTownshipID);
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

        #region "GetAllTownship"
        public List<BOLTownship> GetAllTownship()
        {
            List<BOLTownship> lstTownship = new List<BOLTownship>();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_ShowAllTownship", con);
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
                        BOLTownship bolTownship = new BOLTownship();
                        bolTownship.Id = Int32.Parse(reader["ID"].ToString());
                        bolTownship.DivisionID = Int32.Parse(reader["DivisionID"].ToString());
                        bolTownship.Township = reader["Township"].ToString();
                        bolTownship.MBCTownshipID = reader["MBCTownshipID"].ToString();
                        lstTownship.Add(bolTownship);
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
            return lstTownship;
        }
        #endregion

        #region "GetTownshipByDivisionID"
        public List<BOLTownship> GetTownshipByDivisionID(int DivisionID)
        {
            List<BOLTownship> lstTownship = new List<BOLTownship>();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetTownshipByDivisionID", con);
                cmd.Parameters.AddWithValue("@DivisionID",DivisionID);
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
                        BOLTownship bolTownship = new BOLTownship();
                        bolTownship.Id = Int32.Parse(reader["ID"].ToString());
                        bolTownship.DivisionID = Int32.Parse(reader["DivisionID"].ToString());
                        bolTownship.Township = reader["Township"].ToString();
                        lstTownship.Add(bolTownship);
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
            return lstTownship;
        }
        #endregion

    }
}
