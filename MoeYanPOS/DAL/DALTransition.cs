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
    class DALTransition
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr   = MoeYanConfiguration.GetConnection();
        #endregion

        #region "SaveTransition"
        public int SaveTransition(BOLTransition boltransition)
        {
            int isSaved = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_InsertTransition", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                cmd.Parameters.AddWithValue("@TransName", boltransition.TransName);
                cmd.Parameters.AddWithValue("@TransID", boltransition.TransID);
                
                isSaved = cmd.ExecuteNonQuery();
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

        #region "GetTransitionID"
        public string GetVoucherNo(string transName,DateTime date)
        {
            string TransID = "";
            long maxVNo = 0; long t = 0; string pre = "";
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_GetMaxVoucherNo", con);
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TransName", transName);
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.Parameters.AddWithValue("@Counter", MoeYanFunctions.MoeYanPOS_Helper.counterCode == "" ? "0" : MoeYanFunctions.MoeYanPOS_Helper.counterCode);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                TransID = cmd.ExecuteScalar().ToString();;
                if (TransID.Length > 12)
                {
                    pre = TransID.Substring(0, 3);
                    t = long.Parse(TransID.Substring(3, 10));
                }
                if (t.GetType() == typeof(long))
                {
                    maxVNo = (long)t;
                }

                if (maxVNo == 0 | maxVNo == -1)
                {
                    maxVNo = 1;
                }
                else
                {
                    maxVNo += 1;
                }
                TransID = pre + maxVNo.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return TransID;
        }

        public long GetTransitionID(string transName)
        {
            long TransID = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetTransID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TransName", transName);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                object o = new object();
                o = cmd.ExecuteScalar();
                if (o.GetType() == typeof(long))
                {
                    TransID = (long)o;
                }

                if (TransID == 0 | TransID == -1)
                {
                    TransID = 1;
                }
                else
                {
                    TransID += 1;
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
            return TransID;
        }
        #endregion

        #region "GetMaxVoucher"
        public string GetMaxVoucher(string tranname)
        {
            long maxVNo = 0; long t = 0; string pre = ""; string voucher = "";
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetMaxVoucher", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TransName", tranname);
                cmd.Parameters.AddWithValue("@Counter", MoeYanFunctions.MoeYanPOS_Helper.counterCode);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                object o = new object();
                o = cmd.ExecuteScalar();
                voucher = o.ToString().PadLeft(3, '0');
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return voucher;
        }
        #endregion
    }
}
