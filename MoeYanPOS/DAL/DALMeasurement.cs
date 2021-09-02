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
    class DALMeasurement
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr   = MoeYanConfiguration.GetConnection();
        #endregion

        #region "GetMeasurementiD"
        public int GetMeasurementiD()
        {
            int measurementid = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_GetMeasurementID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                measurementid = (int)cmd.ExecuteScalar();

                if (measurementid == -1 | measurementid == null)
                {
                    measurementid = 1;
                }
                else
                {
                    measurementid += 1;
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
            return measurementid;
        }
        #endregion

        #region "SaveMeasurement"
        public int SaveMeasurement(BOLMeasurement bolmeasurement)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_InsertMeasurement", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@measurement", bolmeasurement.Measurement);
                cmd.Parameters.AddWithValue("@mbcmeasurementid", bolmeasurement.MBCMeasurementID);
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

        #region "SelectAllMeasurement"
        public List<BOLMeasurement> SelectAllMeasurement()
        {
            List<BOLMeasurement> lstmeasurement = new List<BOLMeasurement>();
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_ShowAllMeasurement", con);
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
                        BOLMeasurement bolmeasurement = new BOLMeasurement();
                        bolmeasurement.Id = Int32.Parse(reader["ID"].ToString());
                        bolmeasurement.Measurement = reader["MeasurementName"].ToString();
                        bolmeasurement.MBCMeasurementID = reader["MBC_MeasurementID"].ToString();
                        lstmeasurement.Add(bolmeasurement);
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
            return lstmeasurement;
        }
        #endregion

        #region "DeleteMeasurement"
        public int DeleteMeasurement(int measurementid)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_DeleteMeasurement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", measurementid);
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

        #region "updateMeasurement"
        public int UpdateMeasurement(BOLMeasurement bolmeasurement)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_UpdateMeasurement", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                cmd.Parameters.AddWithValue("@id", bolmeasurement.Id);
                cmd.Parameters.AddWithValue("@measurement", bolmeasurement.Measurement);
                cmd.Parameters.AddWithValue("@mbcmeasurementid", bolmeasurement.MBCMeasurementID);

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
    }
}
