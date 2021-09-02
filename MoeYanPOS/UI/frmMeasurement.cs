using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MoeYanPOS.DAL;
using MoeYanPOS.BOL;
using MoeYanPOS.Function;
namespace MoeYanPOS.UI
{
    public partial class frmMeasurement : Form
    {
        BOLMeasurement bolmeasurement = new BOLMeasurement();
        DALMeasurement dalmeasurement = new DALMeasurement();
        public frmMeasurement()
        {
            InitializeComponent();
            lblid.Text = dalmeasurement.GetMeasurementiD().ToString();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation.isNullOrEmptyField(" Measurement Name ", txtmeasurement.Text) != "")
                {
                    lblMeasurement.Text = Validation.isNullOrEmptyField(" Measurement Name ", txtmeasurement.Text);
                }

                if (Validation.isNullOrEmptyField(" MBC Measurement ID ", txtMBCMeasurementID.Text) != "")
                {
                    lblMBCMeasurementID.Text = Validation.isNullOrEmptyField(" MBC Measurement ID ", txtMBCMeasurementID.Text);
                }

                if (btnsave.Text == "Update" & txtmeasurement.Text!="" & txtMBCMeasurementID.Text!="")
                {
                    int isupdate = 0;
                    BOLMeasurement bolmeasurement = new BOLMeasurement();
                    bolmeasurement.Id = Int32.Parse(lblid.Text);
                    bolmeasurement.Measurement = txtmeasurement.Text;
                    bolmeasurement.MBCMeasurementID = txtMBCMeasurementID.Text;

                    isupdate=dalmeasurement.UpdateMeasurement(bolmeasurement);

                    if (isupdate == 1)
                    {
                        MessageBox.Show(" Measurement is Successfully Update");
                        txtmeasurement.Text = "";
                        txtMBCMeasurementID.Text = "";
                        btnsave.Text = "&Save";
                        //tabmeasurement.SelectedIndex = 1;
                        frmMeasurement_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show(" Measurement is Already exist");
                    }
                }
                if (btnsave.Text == "&Save" & txtmeasurement.Text!="")
                {
                    int issaved = 0;
                    bolmeasurement = new BOLMeasurement();
                    bolmeasurement.Measurement = txtmeasurement.Text;
                    bolmeasurement.MBCMeasurementID = txtMBCMeasurementID.Text;
                    issaved = dalmeasurement.SaveMeasurement(bolmeasurement);

                    if (issaved == 1)
                    {
                        MessageBox.Show("Measurement Name is Successfully Saved");
                        txtmeasurement.Text = "";
                        txtMBCMeasurementID.Text = "";
                        frmMeasurement_Load(sender,e);
                        //tabmeasurement.SelectedIndex = 1;
                        lblid.Text = dalmeasurement.GetMeasurementiD().ToString();
                    }
                    else
                    {
                        MessageBox.Show("Measurement Name is already exist");
                        txtmeasurement.Focus();
                        txtmeasurement.SelectAll();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmMeasurement_Load(object sender, EventArgs e)
        {
            try
            {
                dgvmeasurement.Rows.Clear();
                List<BOLMeasurement> lstmeasurement = new List<BOLMeasurement>();
                lstmeasurement = dalmeasurement.SelectAllMeasurement();

                foreach (BOLMeasurement m in lstmeasurement)
                {
                    dgvmeasurement.Rows.Add(m.Id, m.Measurement, m.MBCMeasurementID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvmeasurement_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    if (e.RowIndex >= 0)
                    {
                        int measurementid = 0; string mbcmeasurementid = "";
                        measurementid = Int32.Parse(dgvmeasurement.Rows[e.RowIndex].Cells[0].Value.ToString());
                        mbcmeasurementid = dgvmeasurement.Rows[e.RowIndex].Cells[2].Value.ToString();
                        tabmeasurement.SelectedIndex = 0;

                        lblid.Text = dgvmeasurement.Rows[e.RowIndex].Cells[0].Value.ToString();
                        txtmeasurement.Text = dgvmeasurement.Rows[e.RowIndex].Cells[1].Value.ToString();
                        txtMBCMeasurementID.Text = dgvmeasurement.Rows[e.RowIndex].Cells[2].Value.ToString();

                        btnsave.Text = "Update";
                    }
                }
                if (e.ColumnIndex == 4)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (MessageBox.Show("Are you sure want to delete", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int measurementid = 0; string mbcmeasurementid = "";
                            measurementid = Int32.Parse(dgvmeasurement.Rows[e.RowIndex].Cells[0].Value.ToString());
                            mbcmeasurementid = dgvmeasurement.Rows[e.RowIndex].Cells[2].Value.ToString();
                            int isdelete = 0;
                            isdelete = dalmeasurement.DeleteMeasurement(measurementid);
                            if (isdelete == 1)
                            {
                                MessageBox.Show("Successfully Deleted");
                                frmMeasurement_Load(sender, e);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            try
            {
                txtmeasurement.Text = "";
                txtMBCMeasurementID.Text = "";
                btnsave.Text = "&Save";
                lblid.Text = dalmeasurement.GetMeasurementiD().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtmeasurement_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnsave.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void picClose1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
