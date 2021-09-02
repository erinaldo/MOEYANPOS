using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MoeYanPOS.BOL;
using System.IO;
using MoeYanPOS.Function;

namespace MoeYanPOS.DAL
{
    class DALVoucherSetting
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string constr = MoeYanConfiguration.GetConnection();
        #endregion

        #region "GetVoucherSettingID"
            public int GetVoucherSettingID()
            {
                int voucherid = 0;
                try
                {
                    con = new SqlConnection(constr);
                    cmd = new SqlCommand("SP_GetVoucherID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    voucherid = (int)cmd.ExecuteScalar();
                    if (voucherid == -1 | voucherid == null)
                    {
                        voucherid = 1;
                    }
                    else
                    {
                        voucherid += 1;
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
                return voucherid;
            }
        #endregion

        #region "SaveVoucherSetting"
            public int SaveVoucherSetting(BOLVoucherSetting bolvoucher)
            {
                //bolvoucher.Logo = GetPhoto(photoFilePath);
                int issaved = 0;
                try
                {
                    con = new SqlConnection(constr);
                    cmd = new SqlCommand("SP_InsertVoucherSetting", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();

                    cmd.Parameters.AddWithValue("@id", bolvoucher.Id);
                    cmd.Parameters.AddWithValue("@Name", bolvoucher.Name);
                    cmd.Parameters.AddWithValue("@Address", bolvoucher.Address);
                    cmd.Parameters.AddWithValue("@Phone", bolvoucher.Phone);
                    cmd.Parameters.AddWithValue("@Message", bolvoucher.Message);
                    cmd.Parameters.AddWithValue("@logo", bolvoucher.Logo);
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

        #region "SelectAll"
            public List<BOLVoucherSetting> SelectAllVoucher()
            {
                List<BOLVoucherSetting> lstvoucher = new List<BOLVoucherSetting>();
                try
                {
                    con = new SqlConnection(constr);
                    cmd = new SqlCommand("SP_SelectAllVoucher", con);

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
                            BOLVoucherSetting bolvoucher = new BOLVoucherSetting();

                            bolvoucher.Id = Int32.Parse(reader["ID"].ToString());
                            bolvoucher.Name = reader["Name"].ToString();
                            bolvoucher.Address = reader["Address"].ToString();
                            bolvoucher.Phone = reader["Phone"].ToString();
                            bolvoucher.Message = reader["Message"].ToString();
                            bolvoucher.Logo =(byte[])reader["Logo"];
                            lstvoucher.Add(bolvoucher);
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
                return lstvoucher;
            }
        #endregion

        #region "UpdateVoucherSetting"
            public int UpdateVoucherSetting(BOLVoucherSetting bolvoucher)
            {
                int isupdated = 0;
                try
                {
                    con = new SqlConnection(constr);
                    cmd = new SqlCommand("SP_UpdateVoucherSetting", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    cmd.Parameters.AddWithValue("@ID", bolvoucher.Id);
                    cmd.Parameters.AddWithValue("@Name", bolvoucher.Name);
                    cmd.Parameters.AddWithValue("@Address", bolvoucher.Address);
                    cmd.Parameters.AddWithValue("@Phone", bolvoucher.Phone);
                    cmd.Parameters.AddWithValue("@Message", bolvoucher.Message);
                    cmd.Parameters.AddWithValue("@Logo", bolvoucher.Logo);

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

        #region "DeleteVoucher"
            public int DeleteVoucher(int id)
            {
                int isdelete = 0;
                try
                {
                    con = new SqlConnection(constr);
                    cmd = new SqlCommand("SP_DeleteVoucherSetting", con);
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

        #region "CheckVoucherSetting"
            public int CheckVoucherSetting()
            {
                int voucherid = 0;
                try
                {
                    con = new SqlConnection(constr);
                    cmd = new SqlCommand("SP_CheckVoucherSetting", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    voucherid = (int)cmd.ExecuteScalar();
                    if (voucherid == -1 | voucherid == null)
                    {
                        voucherid = 1;
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
                return voucherid;
            }
            #endregion
    }
}
