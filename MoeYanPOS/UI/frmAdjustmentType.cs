using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MoeYanPOS.BOL;
using MoeYanPOS.DAL;
using MoeYanPOS.Function;

namespace MoeYanPOS.UI
{
    public partial class frmAdjustmentType : Form
    {
        DALAdjustmentType daladjustment = new DALAdjustmentType();

        public frmAdjustmentType()
        {            
            InitializeComponent();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation.isNullOrEmptyField(" Header ", txtHeader.Text) != "")
                {
                    lblerror.Text = Validation.isNullOrEmptyField(" Header ", txtHeader.Text);
                    lblerror.Visible = true;
                }
                else
                {
                    lblerror.Visible = false;
                }

                if (btnsave.Text == "Update" & txtHeader.Text != "")
                {
                    int update = 0;
                     BOLAdjustmentType bolcheck= new BOLAdjustmentType();
                    bolcheck = daladjustment.DuplicateAdjustmentType(txtHeader.Text);
                    if (bolcheck.AdjustmentType == null)
                    {
                        MessageBox.Show("This Type is already exist !!");
                        txtHeader.Focus();
                        txtHeader.SelectAll();
                        return;
                    }
                    else
                    {
                        BOLAdjustmentType bolAdjustmentType = new BOLAdjustmentType();
                        //dgvAdjustmentType.Rows.Clear();

                        if (rdoStockIn.Checked)
                        {
                            bolAdjustmentType.AdjustmentType = "In";
                        }
                        if (rdoStockOut.Checked)
                        {
                            bolAdjustmentType.AdjustmentType = "Out";
                        }
                        bolAdjustmentType.ID = Int32.Parse(lblID.Text);
                        bolAdjustmentType.Header = txtHeader.Text;

                        update = daladjustment.UpdateAdjustmentType(bolAdjustmentType);

                       
                        MessageBox.Show("Data is Successfully Updated");
                        Cleandaladjustment();                        
                        frmAdjustmentType_Load(sender, e);
                        lblID.Text = daladjustment.GetAdjustmentType().ToString();
                    }
                  
                }
                if (btnsave.Text == "&Save" & txtHeader.Text != "")
                {
                    int issaved = 0;
                    BOLAdjustmentType bolcheck= new BOLAdjustmentType();
                    bolcheck = daladjustment.DuplicateAdjustmentType(txtHeader.Text);
                    if (bolcheck.AdjustmentType == null | bolcheck.AdjustmentType == "" )
                    {
                        BOLAdjustmentType bolAdjustmentType = new BOLAdjustmentType();

                        if (rdoStockIn.Checked)
                        {
                            bolAdjustmentType.AdjustmentType = "In";
                        }
                        if (rdoStockOut.Checked)
                        {
                            bolAdjustmentType.AdjustmentType = "Out";
                        }

                        bolAdjustmentType.Header = txtHeader.Text;
                        issaved = daladjustment.SaveAdjustmentType(bolAdjustmentType);
                        MessageBox.Show("Record is Successfully Saved");

                        Cleandaladjustment();
                        lblID.Text = daladjustment.GetAdjustmentType().ToString();
                        frmAdjustmentType_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("This Record is Already Exist!");
                        txtHeader.Focus();
                        txtHeader.SelectAll();
                    }
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmAdjustmentType_Load(object sender, EventArgs e)
        {
            try
            {
                lblID.Text = daladjustment.GetAdjustmentType().ToString();

                List<BOLAdjustmentType> lstAdjustmentType = new List<BOLAdjustmentType>();
                dgvAdjustmentType.Rows.Clear();
                lstAdjustmentType = daladjustment.ShowAllAdjustmentType();
                foreach (BOLAdjustmentType c in lstAdjustmentType)
                {
                    dgvAdjustmentType.Rows.Add(c.ID,c.Header, c.AdjustmentType);
                }

                rdoStockIn.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Cleandaladjustment()
        {            
            txtHeader.Text = "";
            btnsave.Text = "&Save";
        }

        private void dgvAdjustmentType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    if (e.RowIndex >= 0)
                    {
                        int categoryid = 0;
                        categoryid = Int32.Parse(dgvAdjustmentType.Rows[e.RowIndex].Cells[0].Value.ToString());
                        tabcategory.SelectedIndex = 0;

                        lblID.Text = dgvAdjustmentType.Rows[e.RowIndex].Cells[0].Value.ToString();
                        txtHeader.Text = dgvAdjustmentType.Rows[e.RowIndex].Cells[1].Value.ToString();

                        if(dgvAdjustmentType.Rows[e.RowIndex].Cells[2].Value.ToString()=="In")
                        {
                            rdoStockIn.Checked=true;
                        }
                        else
                        {
                            rdoStockOut.Checked=true;
                        }
                    }

                    btnsave.Text = "Update";
                }

                if (e.ColumnIndex == 4)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int AdjustmentTypeid = 0;                           
                            AdjustmentTypeid = Int32.Parse(dgvAdjustmentType.Rows[e.RowIndex].Cells[0].Value.ToString());
                            int isdelete = 0;
                            isdelete = daladjustment.DeleteAdjustmentType(AdjustmentTypeid);                           
                            if (isdelete == 1)
                            {
                                MessageBox.Show("Successfully Deleted!");
                                frmAdjustmentType_Load(sender, e);
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

        private void btnclear_Click(object sender, EventArgs e)
        {
            try
            {
                Cleandaladjustment();
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
