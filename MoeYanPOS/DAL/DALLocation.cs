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
    class DALLocation
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr = MoeYanConfiguration.GetConnection();
        #endregion        

        #region "SaveLocation"
        public int SaveLocation(BolLocation bolLocation)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_InsertLocation", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@location", bolLocation.Location);
                cmd.Parameters.AddWithValue("@IsThisPlace", bolLocation.IsThisLocation);
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

        #region "ShowAllLocation"
        public List<BolLocation> SelectAllLocation()
        {
            List<BolLocation> lstLocation = new List<BolLocation>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_ShowAllLocation", con);
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
                        BolLocation bolLocation = new BolLocation();
                        bolLocation.ID = Int32.Parse(reader["ID"].ToString());
                        bolLocation.Location = reader["Location"].ToString();
                        bolLocation.IsThisLocation = bool.Parse(reader["IsThisLocation"].ToString());
                        lstLocation.Add(bolLocation);
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
            return lstLocation;
        }
        #endregion

        #region "DeleteLocation"
        public int DeleteLocation(int id)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_DeleteLocation", con);
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

        #region "updateLocation"
        public int UpdateLocation(BolLocation bolLocation)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_UpdateLocation", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                cmd.Parameters.AddWithValue("@id", bolLocation.ID);
                cmd.Parameters.AddWithValue("@location", bolLocation.Location);
               // cmd.Parameters.AddWithValue("@IsThisLocation", bolLocation.IsThisLocation);
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

        #region "GetLocationByID"
        public BolLocation GetLocationByID(long ID)
        {
            BolLocation bolLocation = new BolLocation();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetLocationByID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID",ID);
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
                        bolLocation.ID = Int32.Parse(reader["ID"].ToString());
                        bolLocation.Location = reader["Location"].ToString();
                        bolLocation.IsThisLocation = bool.Parse(reader["IsThisLocation"].ToString());
                        bolLocation.LocationNo = reader["LocationNo"].ToString();
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
            return bolLocation;
        }
        #endregion

        #region "GetCurrentLocationCode"
        public string GetCurrentLocationCode()
        {
            string reader="";
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_LocationCode", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                reader = cmd.ExecuteScalar().ToString();
                if (reader.Count() > 3)
                {
                    reader = reader.Substring(2, 3);
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
            return reader;
        }
        #endregion

        #region "updateIsThisLocation"
        public int updateIsThisLocation(BolLocation bolLocation)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_UpdateIsThisLocation", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                cmd.Parameters.AddWithValue("@id", bolLocation.ID);
                cmd.Parameters.AddWithValue("@IsThisLocation", bolLocation.IsThisLocation);
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

        #region "GetAllLocation"
        public List<BolLocation> GetAllLocation()
        {
            List<BolLocation> lstLocation = new List<BolLocation>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetAllLocation", con);
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
                        BolLocation bolLocation = new BolLocation();
                        bolLocation.ID = Int32.Parse(reader["ID"].ToString());
                        bolLocation.Location = reader["Location"].ToString();
                        bolLocation.IsThisLocation = bool.Parse(reader["IsThisLocation"].ToString());
                        lstLocation.Add(bolLocation);
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
            return lstLocation;
        }
        #endregion

        #region "SelectAllLocations"
        public List<BolLocation> SelectAllLocations()
        {
            List<BolLocation> lstLocation = new List<BolLocation>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_SelectAllLocation", con);
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
                        BolLocation bolLocation = new BolLocation();
                        bolLocation.ID = Int32.Parse(reader["ID"].ToString());
                        bolLocation.Location = reader["Location"].ToString();
                        bolLocation.IsThisLocation = bool.Parse(reader["IsThisLocation"].ToString());
                        lstLocation.Add(bolLocation);
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
            return lstLocation;
        }
        #endregion
    }
}
