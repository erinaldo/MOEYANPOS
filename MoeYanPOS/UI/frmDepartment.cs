using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MoeYanPOS.BOL;
using MoeYanPOS.DAL;
using MoeYanPOS.Function;

namespace MoeYanPOS.UI
{
    public partial class frmDepartment : Form
    {
        DALDepartment daldepartment = new DALDepartment();
        BOLDepartment boldepartment = new BOLDepartment();
        public frmDepartment()
        {
            InitializeComponent();
            lblid.Text = daldepartment.GetDepartmentID().ToString();
            
        }

        private void frmDepartment_Load(object sender, EventArgs e)
        {
            try
            {
                List<BOLDepartment> lstdepartment = new List<BOLDepartment>();
                dgvdepartment.Rows.Clear();
                lstdepartment = daldepartment.ShowAllDepartment();
                foreach (BOLDepartment d in lstdepartment)
                {
                    dgvdepartment.Rows.Add(d.Id, d.Departmentname, d.MBCDepartmentID);
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

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation.isNullOrEmptyField(" DepartmentName ", txtdepartmentname.Text) != "")
                {
                    lbldepartmentname.Text = Validation.isNullOrEmptyField(" DepartmentName ", txtdepartmentname.Text);
                    lbldepartmentname.Visible = true;
                }
                else
                {
                    lbldepartmentname.Visible = false;
                }

                if (Validation.isNullOrEmptyField(" MBC Department ID ", txtMBCDepartmentID.Text) != "")
                {
                    lblMBCDepartmentID.Text = Validation.isNullOrEmptyField(" MBC Department ID ", txtMBCDepartmentID.Text);
                    lblMBCDepartmentID.Visible = true;
                }
                else
                {
                    lblMBCDepartmentID.Visible = false;
                }

                if (btnsave.Text == "Update" & txtdepartmentname.Text != "" & txtdepartmentname.Text != " ")
                {
                    int update = 0;
                    BOLDepartment boldepartment = new BOLDepartment();
                    dgvdepartment.Rows.Clear();

                    boldepartment.Id = Int32.Parse(lblid.Text);
                    boldepartment.Departmentname = txtdepartmentname.Text;
                    boldepartment.MBCDepartmentID = txtMBCDepartmentID.Text;

                    update = daldepartment.UpdateDepartment(boldepartment);

                    if (update == 1)
                    {
                        MessageBox.Show("Department Record is Successfully Updated");
                        tabdepartment.SelectedIndex = 1;
                        txtdepartmentname.Text = "";
                        txtMBCDepartmentID.Text = "";
                        frmDepartment_Load(sender, e);
                        lblid.Text = daldepartment.GetDepartmentID().ToString();
                        btnsave.Text = "&Save";
                    }
                    else
                    {
                        MessageBox.Show("This Record is Already Exist!");
                        txtdepartmentname.SelectAll();
                    }
                }
                if (btnsave.Text == "&Save" & txtdepartmentname.Text != "" & txtdepartmentname.Text != " ")
                {
                    int issaved = 0;
                    boldepartment = new BOLDepartment();
                    boldepartment.Id = Int32.Parse(lblid.Text);
                    boldepartment.Departmentname = txtdepartmentname.Text;
                    boldepartment.MBCDepartmentID = txtMBCDepartmentID.Text;

                    issaved = daldepartment.SaveDepartment(boldepartment);

                    if (issaved == 1)
                    {
                        MessageBox.Show(" Department Record is Successfully Saved");
                        lblid.Text = daldepartment.GetDepartmentID().ToString();
                        txtdepartmentname.Text = "";
                        txtMBCDepartmentID.Text = "";
                        frmDepartment_Load(sender,e);
                        tabdepartment.SelectedIndex = 1;
                    }
                    else
                    {
                        MessageBox.Show("This Record is Already Exist!");
                        txtdepartmentname.SelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvdepartment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    if (e.RowIndex >= 0)
                    {
                        int departmentid = 0;
                        string mbcdepartmentid = "";
                        departmentid = Int32.Parse(dgvdepartment.Rows[e.RowIndex].Cells[0].Value.ToString());
                        mbcdepartmentid = dgvdepartment.Rows[e.RowIndex].Cells[2].Value.ToString();
                        tabdepartment.SelectedIndex = 0;

                        lblid.Text = dgvdepartment.Rows[e.RowIndex].Cells[0].Value.ToString();
                        txtdepartmentname.Text = dgvdepartment.Rows[e.RowIndex].Cells[1].Value.ToString();
                        txtMBCDepartmentID.Text = dgvdepartment.Rows[e.RowIndex].Cells[2].Value.ToString();
                        lbldepartmentname.Visible = false;
                        lblMBCDepartmentID.Visible = false;
                    }
                    btnsave.Text = "Update";
                }
                if (e.ColumnIndex == 4)
                {
                    if (e.RowIndex >= 0)
                    {
                        if(MessageBox.Show("Are you sure to delete?","Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                        {
                            int departmentid = 0;
                            string mbcdepartmentid = "";
                            departmentid = Int32.Parse(dgvdepartment.Rows[e.RowIndex].Cells[0].Value.ToString());
                            mbcdepartmentid = dgvdepartment.Rows[e.RowIndex].Cells[2].Value.ToString();
                            int isdelete = 0;
                            isdelete = 0;
                            isdelete = daldepartment.DeleteDepartment(departmentid);

                            if (isdelete == 1)
                            {
                                MessageBox.Show("Successfully Deleted!");
                                frmDepartment_Load(sender, e);
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

        private void btnnew_Click(object sender, EventArgs e)
        {
            try
            {
                txtdepartmentname.Text = "";
                btnsave.Text = "&Save";
                txtMBCDepartmentID.Text = "";
                lblid.Text = daldepartment.GetDepartmentID().ToString();
                lbldepartmentname.Visible = false;
                lblMBCDepartmentID.Visible = false;
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
