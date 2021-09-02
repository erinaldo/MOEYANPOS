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
    public partial class frmCategory : Form
    {
        BOLCategory bolcategory = new BOLCategory();
        DALCategory dalcategory = new DALCategory();
        public frmCategory()
        {
           
            try
            {
                InitializeComponent();
                lblID.Text = dalcategory.GetCategoryID().ToString();
                DALCategory dal = new DALCategory();
                List<BOLClass> lstclass = new List<BOLClass>();
                lstclass = dal.FillClass();
                
                cboclassname.DisplayMember = "ClassName";
                cboclassname.ValueMember = "ID";
                cboclassname.DataSource = lstclass;
                if (lstclass.Count > 0)
                {
                    cboclassname.SelectedIndex = 0;
                }
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
                if (Validation.isNullOrEmptyField(" Category Name ", txtcategory.Text) != "")
                {
                    lblerror.Text = Validation.isNullOrEmptyField(" CatgoryName ", txtcategory.Text);
                    lblerror.Visible = true;
                }
                else
                {
                    lblerror.Visible = false;
                }

                if (btnsave.Text == "Update" & txtcategory.Text != "" & txtcategory.Text != " ")
                {
                    int update = 0;
                    BOLCategory bolcategory = new BOLCategory();
                    dgvcategory.Rows.Clear();
                    bolcategory.Id = Int32.Parse(lblID.Text);
                    bolcategory.ClassID = Int32.Parse(cboclassname.SelectedValue.ToString());
                    bolcategory.CategoryName = txtcategory.Text;
                    bolcategory.ReportGroupID = txtReportGroupID.Text;
                    bolcategory.MBC_CategoryID = txtMBCCategoryID.Text;

                    update=dalcategory.UpdateCategory(bolcategory);

                    if (update == 1)
                    {
                        MessageBox.Show("Category Record is Successfully Updated");
                        CleanCategory();
                        //tabcategory.SelectedIndex = 1;
                        frmCategory_Load(sender, e);
                        lblID.Text = dalcategory.GetCategoryID().ToString();
                    }
                    else
                    {
                        MessageBox.Show("This Record is Already Exist!");
                        txtcategory.Focus();
                        txtcategory.SelectAll();
                    }
                }
                if (btnsave.Text == "&Save" & txtcategory.Text != "" & txtcategory.Text != " ")
                {
                    int issaved = 0;
                    bolcategory = new BOLCategory();
                    bolcategory.ClassID = Int32.Parse(cboclassname.SelectedValue.ToString());
                    bolcategory.CategoryName=txtcategory.Text;
                    bolcategory.ReportGroupID = txtReportGroupID.Text;
                    bolcategory.MBC_CategoryID = txtMBCCategoryID.Text;

                    issaved = dalcategory.SaveCategory(bolcategory);

                    if (issaved == 1)
                    {
                        MessageBox.Show("Category Record is Successfully Saved");
                        CleanCategory();
                        lblID.Text = dalcategory.GetCategoryID().ToString();
                        //tabcategory.SelectedIndex = 1;
                        frmCategory_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("This Record is Already Exist!");
                        txtcategory.Focus();
                        txtcategory.SelectAll();
                    }
                }

               // btnNew_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void CleanCategory()
        {
            cboclassname.SelectedIndex = 0;
            txtcategory.Text = "";
            btnsave.Text = "&Save";
            lblerror.Visible = false;
            txtReportGroupID.Text = "";
            txtMBCCategoryID.Text = "";
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

        private void frmCategory_Load(object sender, EventArgs e)
        {
            try
            {
                List<BOLCategory> lstcategory = new List<BOLCategory>();
                dgvcategory.Rows.Clear();
                lstcategory = dalcategory.ShowAllCategory();
                foreach (BOLCategory c in lstcategory)
                {
                    dgvcategory.Rows.Add(c.Id, c.Classname, c.CategoryName,c.ReportGroupID,c.MBC_CategoryID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvcategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 5)
                {
                    if (e.RowIndex >= 0)
                    {
                        int categoryid = 0;
                        categoryid = Int32.Parse(dgvcategory.Rows[e.RowIndex].Cells[0].Value.ToString());
                        tabcategory.SelectedIndex = 0;

                        lblID.Text = dgvcategory.Rows[e.RowIndex].Cells[0].Value.ToString();
                        cboclassname.Text = dgvcategory.Rows[e.RowIndex].Cells[1].Value.ToString();
                        txtcategory.Text = dgvcategory.Rows[e.RowIndex].Cells[2].Value.ToString();
                        txtReportGroupID.Text = dgvcategory.Rows[e.RowIndex].Cells[3].Value.ToString();
                        txtMBCCategoryID.Text = dgvcategory.Rows[e.RowIndex].Cells[4].Value.ToString();
                    }

                    btnsave.Text = "Update";
                }

                if (e.ColumnIndex == 6)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int categoryid = 0;
                            //dgvcategory.Rows.Clear();
                            categoryid = Int32.Parse(dgvcategory.Rows[e.RowIndex].Cells[0].Value.ToString());
                            int isdelete = 0;
                            isdelete = dalcategory.DeleteCategory(categoryid);
                            //dgvcategory.Rows.Clear();
                            if (isdelete == 1)
                            {
                                MessageBox.Show("Successfully Deleted!");
                                frmCategory_Load(sender, e);
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                DALCategory dal = new DALCategory();
                List<BOLClass> lstclass = new List<BOLClass>();
                lstclass = dal.FillClass();

                cboclassname.DisplayMember = "ClassName";
                cboclassname.ValueMember = "ID";
                cboclassname.DataSource = lstclass;
                cboclassname.SelectedIndex = 0;

                dgvcategory.Rows.Clear();
                List<BOLCategory> lstcategory = new List<BOLCategory>();
                lstcategory = dalcategory.ShowAllCategory();
                foreach (BOLCategory c in lstcategory)
                {
                    dgvcategory.Rows.Add(c.Id, c.Classname, c.CategoryName,c.ReportGroupID);
                }
                cboclassname.SelectedIndex = 0;
                txtcategory.Text = "";
                lblID.Text = dalcategory.GetCategoryID().ToString();
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
                cboclassname.SelectedIndex = 0;
                txtcategory.Text = "";
                btnsave.Text = "&Save";
                lblID.Text = dalcategory.GetCategoryID().ToString();
                txtReportGroupID.Text = "";
                txtMBCCategoryID.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtcategory_KeyDown(object sender, KeyEventArgs e)
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

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
