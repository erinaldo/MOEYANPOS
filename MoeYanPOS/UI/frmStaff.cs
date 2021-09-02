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
    public partial class frmStaff : Form
    {
        BOLStaff bolstaff = new BOLStaff();
        DALStaff dalstaff = new DALStaff();

        public frmStaff()
        {
            try
            {
                InitializeComponent();
                txtStaffID.Text = dalstaff.GetStaffID().ToString();
                DALStaff dal = new DALStaff();
                List<BOLDepartment> lstdepartment = new List<BOLDepartment>();
                lstdepartment = dal.FillDepartment();
                
                cboDepartmentName.DisplayMember = "DepartmentName";
                cboDepartmentName.ValueMember = "DepartmentID";
                cboDepartmentName.DataSource = lstdepartment;
                if (lstdepartment.Count > 0)
                {
                    cboDepartmentName.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
         }

        private void CleanStaff()
        {
            cboDepartmentName.SelectedIndex = 0;
            txtStaffID.Text = "";
            txtStaffName.Text = "";
            txtMBCStaffID.Text = "";
            btnsave.Text = "&Save";
            lblerror.Visible = false;
            lblMBCStaffID.Visible = false;
        }

        private void txtStaffName_KeyDown(object sender, KeyEventArgs e)
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

        private void btnclear_Click(object sender, EventArgs e)
        {
            try
            {
                DALStaff dal = new DALStaff();
                List<BOLDepartment> lstdepartment = new List<BOLDepartment>();
                lstdepartment = dal.FillDepartment();

                cboDepartmentName.DisplayMember = "DepartmentName";
                cboDepartmentName.ValueMember = "DepartmentID";
                cboDepartmentName.DataSource = lstdepartment;
                cboDepartmentName.SelectedIndex = 0;

                dgvStaff.Rows.Clear();
                List<BOLStaff> lststaff = new List<BOLStaff>();
                lststaff = dalstaff.ShowAllStaff();
                foreach (BOLStaff c in lststaff)
                {
                    dgvStaff.Rows.Add(c.StaffID, c.StaffName, c.DepartmentName, c.MCBStaffID);
                }
                cboDepartmentName.SelectedIndex = 0;
                CleanStaff();
                btnsave.Text = "&Save";
                txtStaffID.Text = dalstaff.GetStaffID().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation.isNullOrEmptyField(" Staff Name ", txtStaffName.Text) != "")
                {
                    lblerror.Text = Validation.isNullOrEmptyField(" Staff Name ", txtStaffName.Text);
                    lblerror.Visible = true;
                }
                else
                {
                    lblerror.Visible = false;
                }

                if (Validation.isNullOrEmptyField(" MBC Staff ID ", txtMBCStaffID.Text) != "")
                {
                    lblMBCStaffID.Text = Validation.isNullOrEmptyField(" MBC Staff ID ", txtMBCStaffID.Text);
                    lblMBCStaffID.Visible = true;
                }
                else
                {
                    lblMBCStaffID.Visible = false;
                }

                if (btnsave.Text == "Update" & txtStaffID.Text != "" & txtStaffName.Text != " ")
                {
                    int update = 0;
                    BOLStaff bolstaff = new BOLStaff();
                    dgvStaff.Rows.Clear();
                    bolstaff.StaffID = Int32.Parse(txtStaffID.Text);
                    bolstaff.StaffName = txtStaffName.Text;
                    bolstaff.DepartmentID = Int32.Parse(cboDepartmentName.SelectedValue.ToString());
                    bolstaff.MCBStaffID = txtMBCStaffID.Text;

                    update = dalstaff.UpdateStaff(bolstaff);

                    if (update == 1)
                    {
                        MessageBox.Show("Staff Record is Successfully Updated");
                        CleanStaff();
                        //tabcategory.SelectedIndex = 1;
                        frmStaff_Load(sender, e);
                        txtStaffID.Text = dalstaff.GetStaffID().ToString();
                        txtStaffID.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("This Record is Already Exist!");
                        txtStaffID.Focus();
                        txtStaffID.SelectAll();
                    }
                }
                if (btnsave.Text == "&Save" & txtStaffID.Text != "" & txtStaffName.Text != " ")
                {
                    int issaved = 0;
                    bolstaff = new BOLStaff();
                    bolstaff.StaffID = Int32.Parse(txtStaffID.Text);
                    bolstaff.DepartmentID = Int32.Parse(cboDepartmentName.SelectedValue.ToString());
                    bolstaff.StaffName = txtStaffName.Text;
                    bolstaff.MCBStaffID = txtMBCStaffID.Text;

                    //added by KSAung
                    if (btnsave.Text == "&Save")
                    {
                        List<BOLStaff> getstaffid = new List<BOLStaff>();
                        getstaffid = dalstaff.DuplicateStaff(Int32.Parse(txtStaffID.Text));
                        if (getstaffid.Count > 0)
                        {
                            MessageBox.Show("Duplicate Staff ID");
                            return;
                        }
                    }

                    issaved = dalstaff.SaveStaff(bolstaff);

                    if (issaved == 1)
                    {
                        MessageBox.Show("Staff Record is Successfully Saved");
                        CleanStaff();
                        //txtBankChequeID.Text = Int32.Parse(txtBankChequeID.Text);
                        //tabcategory.SelectedIndex = 1;
                        frmStaff_Load(sender, e);
                        txtStaffID.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("This Record is Already Exist!");
                        txtStaffID.Focus();
                        txtStaffID.SelectAll();
                    }
                }
                // btnNew_Click(sender, e);
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

        private void frmStaff_Load(object sender, EventArgs e)
        {
            try
            {
                List<BOLStaff> lststaff = new List<BOLStaff>();
                dgvStaff.Rows.Clear();
                lststaff = dalstaff.ShowAllStaff();
                foreach (BOLStaff c in lststaff)
                {
                    dgvStaff.Rows.Add(c.StaffID, c.StaffName, c.DepartmentName, c.MCBStaffID);
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

        private void dgvStaff_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4)
                {
                    if (e.RowIndex >= 0)
                    {
                        int staffid = 0;
                        staffid = Int32.Parse(dgvStaff.Rows[e.RowIndex].Cells[0].Value.ToString());
                        tabstaff.SelectedIndex = 0;

                        txtStaffID.Text = dgvStaff.Rows[e.RowIndex].Cells[0].Value.ToString();
                        txtStaffName.Text = dgvStaff.Rows[e.RowIndex].Cells[1].Value.ToString();
                        cboDepartmentName.Text = dgvStaff.Rows[e.RowIndex].Cells[2].Value.ToString();
                        txtMBCStaffID.Text = dgvStaff.Rows[e.RowIndex].Cells[3].Value.ToString();

                        txtStaffID.Enabled = false;
                    }

                    btnsave.Text = "Update";
                }

                if (e.ColumnIndex == 5)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int staffid = 0;
                            //dgvcategory.Rows.Clear();
                            staffid = Int32.Parse(dgvStaff.Rows[e.RowIndex].Cells[0].Value.ToString());
                            int isdelete = 0;
                            isdelete = dalstaff.DeleteStaff(staffid);
                            //dgvcategory.Rows.Clear();
                            if (isdelete == 1)
                            {
                                MessageBox.Show("Successfully Deleted!");
                                frmStaff_Load(sender, e);
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
