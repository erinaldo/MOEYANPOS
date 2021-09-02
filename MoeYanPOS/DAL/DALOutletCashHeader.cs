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
    class DALOutletCashHeader
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr = MoeYanConfiguration.GetConnection();
        #endregion

        #region "GetOutLetCashHeader"
        public int GetOutLetCashHeader()
        {
            int id = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetOutletCashHeaderID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                id = (int)cmd.ExecuteScalar();
                if (id == -1 | id == null)
                {
                    id = 1;
                }
                else
                {
                    id += 1;
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
            return id;
        }
        #endregion

        #region "SaveOutLetCashHeader"
        public int SaveOutLetCashHeader(BOLOutLetCashHeader bolOutletCashHeader)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_InsertOutLetCashHeader", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@Type", bolOutletCashHeader.Type);
                cmd.Parameters.AddWithValue("@Header", bolOutletCashHeader.Header);
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

        #region "ShowAllOutLetCashHeader"
        public List<BOLOutLetCashHeader> ShowAllOutLetCashHeader()
        {
            List<BOLOutLetCashHeader> lstOutLetCashHeader = new List<BOLOutLetCashHeader>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetAllOutletCashHeader", con);
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
                        BOLOutLetCashHeader bolOutLetCashHeader = new BOLOutLetCashHeader();
                        bolOutLetCashHeader.ID = Int32.Parse(reader["ID"].ToString());
                        bolOutLetCashHeader.Type = reader["Type"].ToString();
                        bolOutLetCashHeader.Header = reader["Header"].ToString();
                        lstOutLetCashHeader.Add(bolOutLetCashHeader);
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
            return lstOutLetCashHeader;
        }
        #endregion

        #region "DeleteOutLetCashHeader"
        public int DeleteOutLetCashHeader(int id)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_DeleteOutletCashHeader", con);
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

        #region "UpdateOutLetCashHeader"
        public int UpdateOutLetCashHeader(BOLOutLetCashHeader bolOutLetCashHeader)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_UpdateOutletCashHeader", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@ID", bolOutLetCashHeader.ID);
                cmd.Parameters.AddWithValue("@Type", bolOutLetCashHeader.Type);
                cmd.Parameters.AddWithValue("@Header", bolOutLetCashHeader.Header);

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

        #region "DuplicateOutLetCashHeader"
        public BOLOutLetCashHeader DuplicateOutLetCashHeader(String name)
        {
            BOLOutLetCashHeader bolOutLetCashHeader = new BOLOutLetCashHeader();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_DuplicateOutletCashHeader", con);
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
                        bolOutLetCashHeader.Header = reader["Header"].ToString();
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
            return bolOutLetCashHeader;
        }
        #endregion

        #region "CheckOutLetCashHeader"
        public string CheckOutLetCashHeader(int ID)
        {
            string Type = "";
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_CheckOutletCashHeader", con);
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
                        Type = reader["Type"].ToString();
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
            return Type;
        }
        #endregion

        #region "DuplicateOutLetCashHeaderforUpdate"
        public BOLOutLetCashHeader DuplicateOutLetCashHeaderforUpdate(String name, int id)
        {
            BOLOutLetCashHeader bolOutLetCashHeader = new BOLOutLetCashHeader();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_DuplicateOutletCashHeaderforUpdate", con);
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
                        bolOutLetCashHeader.Header = reader["Header"].ToString();
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
            return bolOutLetCashHeader;
        }
        #endregion

        #region "GetOutletCashHeaderByType"
        public List<BOLOutLetCashHeader> GetOutletCashHeaderByType(string Type)
        {
            List<BOLOutLetCashHeader> lstbolOutletCashHeader = new List<BOLOutLetCashHeader>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetOutletCashHeaderByType", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Type", Type);

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
                        BOLOutLetCashHeader bol = new BOLOutLetCashHeader();
                        bol.ID = Int32.Parse(reader["ID"].ToString());
                        bol.Type = reader["Type"].ToString();
                        bol.Header = reader["Header"].ToString();
                        lstbolOutletCashHeader.Add(bol);
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
            return lstbolOutletCashHeader;
        }
        #endregion
    }
}
