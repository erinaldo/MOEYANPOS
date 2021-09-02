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
    public partial class frmOutletCashHeader : Form
    {
        DALOutletCashHeader dalOutletcashheader = new DALOutletCashHeader();

        public frmOutletCashHeader()
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
                    BOLOutLetCashHeader bolOutLetCashHeaderCheck = new BOLOutLetCashHeader();
                    bolOutLetCashHeaderCheck = dalOutletcashheader.DuplicateOutLetCashHeader(txtHeader.Text);
                    if (bolOutLetCashHeaderCheck.Header == null)
                    {
                        MessageBox.Show("This Header is already exist !!");
                        txtHeader.Focus();
                        txtHeader.SelectAll();
                        return;
                    }
                    else
                    {
                        BOLOutLetCashHeader bolOutLetCashHeader = new BOLOutLetCashHeader();
                        //dgvAdjustmentType.Rows.Clear();

                        if (rdoCashIn.Checked)
                        {
                            bolOutLetCashHeader.Type = "ရေငြ";
                        }
                        if (rdoCashOut.Checked)
                        {
                            bolOutLetCashHeader.Type = "ေပးေငြ";
                        }
                        bolOutLetCashHeader.ID = Int32.Parse(lblID.Text);
                        bolOutLetCashHeader.Header = txtHeader.Text;

                        update = dalOutletcashheader.UpdateOutLetCashHeader(bolOutLetCashHeader);


                        MessageBox.Show("Data is Successfully Updated.");
                        CleanOutletCash();
                        frmOutletCashHeader_Load(sender, e);
                        lblID.Text = dalOutletcashheader.GetOutLetCashHeader().ToString();
                    }

                }
                if (btnsave.Text == "&Save" & txtHeader.Text != "")
                {
                    int issaved = 0;
                    BOLOutLetCashHeader bolcheck = new BOLOutLetCashHeader();
                    bolcheck = dalOutletcashheader.DuplicateOutLetCashHeader(txtHeader.Text);
                    if (bolcheck.Header == null | bolcheck.Header == "")
                    {
                        BOLOutLetCashHeader bolOutLetCashHeader = new BOLOutLetCashHeader();

                        if (rdoCashIn.Checked)
                        {
                            bolOutLetCashHeader.Type = "ရေငြ";
                        }
                        if (rdoCashOut.Checked)
                        {
                            bolOutLetCashHeader.Type = "ေပးေငြ";
                        }

                        bolOutLetCashHeader.Header = txtHeader.Text;
                        issaved = dalOutletcashheader.SaveOutLetCashHeader(bolOutLetCashHeader);
                        MessageBox.Show("Record is Successfully Saved");

                        CleanOutletCash();
                        lblID.Text = dalOutletcashheader.GetOutLetCashHeader().ToString();
                        frmOutletCashHeader_Load (sender, e);
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

        private void btnclear_Click(object sender, EventArgs e)
        {
            try
            {
                CleanOutletCash();
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

        private void picClose1_Click(object sender, EventArgs e)
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

        private void frmOutletCashHeader_Load(object sender, EventArgs e)
        {
            try
            {
                lblID.Text = dalOutletcashheader.GetOutLetCashHeader().ToString();

                List<BOLOutLetCashHeader> lstOutletCashHeader= new List<BOLOutLetCashHeader>();
                dgvOutletCashHeader.Rows.Clear();
                lstOutletCashHeader = dalOutletcashheader.ShowAllOutLetCashHeader();
                foreach (BOLOutLetCashHeader o in lstOutletCashHeader)
                {
                    dgvOutletCashHeader.Rows.Add(o.ID, o.Header, o.Type);
                }

                rdoCashIn.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void CleanOutletCash()
        {
            txtHeader.Text = "";
            btnsave.Text = "&Save";
        }

        private void rdoStockIn_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dgvOutletCashHeader_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    if (e.RowIndex >= 0)
                    {
                        int id = 0;
                        id = Int32.Parse(dgvOutletCashHeader.Rows[e.RowIndex].Cells[0].Value.ToString());
                        tabcategory.SelectedIndex = 0;

                        lblID.Text = dgvOutletCashHeader.Rows[e.RowIndex].Cells[0].Value.ToString();
                        txtHeader.Text = dgvOutletCashHeader.Rows[e.RowIndex].Cells[1].Value.ToString();

                        if (dgvOutletCashHeader.Rows[e.RowIndex].Cells[2].Value.ToString() == "ရေငြ")
                        {
                            rdoCashIn.Checked = true;
                        }
                        else
                        {
                            rdoCashOut.Checked = true;
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
                            int id = 0;
                            id = Int32.Parse(dgvOutletCashHeader.Rows[e.RowIndex].Cells[0].Value.ToString());
                            int isdelete = 0;
                            isdelete = dalOutletcashheader.DeleteOutLetCashHeader(id);
                            if (isdelete == 1)
                            {
                                MessageBox.Show("Successfully Deleted!");
                                frmOutletCashHeader_Load(sender, e);
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
    }
}
