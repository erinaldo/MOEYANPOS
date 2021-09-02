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
    class DALUser
    {

        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr= MoeYanConfiguration.GetConnection();
        #endregion

        #region "SelectAllUser"

        public List<BOLUser> SelectAllUser()
        {
            List<BOLUser> lstuser = new List<BOLUser>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("sp_ShowAllUser", con);
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
                        boluser.UserName = reader["Name"].ToString();
                        boluser.CardID = reader["CardID"].ToString();
                        boluser.Password = reader["Password"].ToString();
                        boluser.Township = reader["Township"].ToString();
                        boluser.NRC = reader["NRC"].ToString();
                        boluser.Level = reader["Level"].ToString();
                        boluser.IsSavePrint = bool.Parse(reader["IsSavePrint"].ToString());
                        boluser.IsmsgforVoucher = bool.Parse(reader["IsmsgforVoucher"].ToString());
                        boluser.StaffName = reader["StaffName"].ToString();
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

        #region "SaveUser"

        public int SaveUser(BOLUser boluser)
        {
            int isSaved = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_InsertUser",con);
                cmd.CommandType=CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                
                cmd.Parameters.AddWithValue("@Name", boluser.UserName);
                cmd.Parameters.AddWithValue("@CardID", boluser.CardID);
                cmd.Parameters.AddWithValue("@Password", boluser.Password);
                cmd.Parameters.AddWithValue("@Township", boluser.TownshipID);
                cmd.Parameters.AddWithValue("@NRC", boluser.NRC);
                cmd.Parameters.AddWithValue("@Level",boluser.Level);
                cmd.Parameters.AddWithValue("@IsSavePrint", boluser.IsSavePrint);
                cmd.Parameters.AddWithValue("@IsmsgforVoucher", boluser.IsmsgforVoucher);
                cmd.Parameters.AddWithValue("@StaffID", boluser.StaffID);
                isSaved= cmd.ExecuteNonQuery();
                
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

        #region "GetUserID"
        public int GetUserID()
        {
            int UserID = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_GetUserID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                UserID = Convert.ToInt32( cmd.ExecuteScalar());
                if (UserID == -1 | UserID == null)
                {
                    UserID = 1;
                }
                else
                {
                    UserID += 1;
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
            return UserID;
        }
        #endregion

        #region "SelectUser"
        public int SelectUser(BOLUser boluser)
        {
            int userid = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_SelectUserNameandPassword", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd.Parameters.AddWithValue("@UserID", boluser.UserID);
                cmd.Parameters.AddWithValue("@Password", boluser.Password);
                userid =(int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return userid;
        }
        #endregion

        #region "UserList"
        public List<BOLUser> GetUser(string UserName, int township,string Staffid)
        {
            List<BOLUser> lstuser = new List<BOLUser>();
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("sp_GetUserList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@township", township);
                cmd.Parameters.AddWithValue("@staffid", Staffid);

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
                        boluser.UserName = reader["Name"].ToString();
                        boluser.CardID = reader["CardID"].ToString();
                        boluser.Township = reader["Township"].ToString();
                        boluser.TownshipID = Int32.Parse(reader["TownshipID"].ToString());
                        boluser.StaffID = reader["StaffID"].ToString();
                        boluser.StaffName = reader["StaffName"].ToString();
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

        #region "GetUserByUserID"
        public BOLUser GetUserByUserID(int Userid)
        {
            BOLUser boluser = new BOLUser();
            try
            {

                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_GetUserByUserID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", Userid);

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

                        boluser.UserID = Int32.Parse(reader["UserID"].ToString());
                        boluser.UserName = reader["Name"].ToString();
                        boluser.CardID = reader["CardID"].ToString();
                        boluser.Password = reader["Password"].ToString();
                        boluser.NRC = reader["NRC"].ToString();
                        boluser.Township = reader["Township"].ToString();
                        boluser.IsSavePrint = bool.Parse(reader["IsSavePrint"].ToString());
                        boluser.IsmsgforVoucher = bool.Parse(reader["IsmsgforVoucher"].ToString());
                        boluser.TownshipID = Int32.Parse(reader["TownshipID"].ToString());
                        boluser.StaffID = reader["StaffID"].ToString();
                        boluser.StaffName = reader["StaffName"].ToString();
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

            return boluser;
        }
        #endregion

        #region "updateUserByUserID"
        public int updateUserByUserID(BOLUser boluser)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_UpdateUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd.Parameters.AddWithValue("@UserID", boluser.UserID);
                cmd.Parameters.AddWithValue("@UserName", boluser.UserName);
                cmd.Parameters.AddWithValue("@CardID", boluser.CardID);
                cmd.Parameters.AddWithValue("@NRC", boluser.NRC);
                cmd.Parameters.AddWithValue("@Password", boluser.Password);
                cmd.Parameters.AddWithValue("@Township", boluser.TownshipID);
                cmd.Parameters.AddWithValue("@Level", boluser.Level);
                cmd.Parameters.AddWithValue("@IsSavePrint", boluser.IsSavePrint);
                cmd.Parameters.AddWithValue("@IsmsgforVoucher", boluser.IsmsgforVoucher);
                cmd.Parameters.AddWithValue("@StaffID", boluser.StaffID);
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

        #region "DeleteUser"
        public int DeleteUser(int userid)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_DeleteUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userid", userid);
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

        #region "FillUserID"
        public int FillUserID(string cardid)
        {
            int userid = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_SelectUserID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd.Parameters.AddWithValue("@Cardid", cardid);
                userid = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return userid;

        }
        #endregion

        #region "FillCardID"
        public string FillCardID(int userid)
        {
            string cardid = "";
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_FillCardID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd.Parameters.AddWithValue("@userid", userid);
                cardid = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return cardid;
        }
        #endregion

        #region "DuplicateUser"
        public List<BOLUser> SelectUser(String username)
        {
            List<BOLUser> lstuser = new List<BOLUser>();
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_DuplicateUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", username);

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
                        boluser.UserName = reader["Name"].ToString();
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

        #region "Backup"
        public void Backup()
        {

            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_Backup", con);
                cmd.CommandType = CommandType.StoredProcedure;

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
   
#region "BackupExport"
        public void BackupExport()
        {

            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_Backup_Export", con);
                cmd.CommandType = CommandType.StoredProcedure;

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
