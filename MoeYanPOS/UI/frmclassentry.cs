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
    public partial class frmclassentry : Form
    {
        DALClass dalclass = new DALClass();
        BOLClass bolclass = new BOLClass();

        public frmclassentry()
        {
            InitializeComponent();
            lblid.Text = dalclass.GetClassID().ToString();
        }        

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation.isNullOrEmptyField(" Class Name ", txtclassname.Text) != "")
                {
                    lblclassname.Text = Validation.isNullOrEmptyField(" Class Name ", txtclassname.Text);
                    lblclassname.Visible = true;
                }
                else
                {
                    lblclassname.Visible = false;
                }
                if (Validation.isNullOrEmptyField(" MBC Class ID ", txtMBCClassID.Text) != "")
                {
                    lblMBCClassID.Text = Validation.isNullOrEmptyField(" MBC Class ID ", txtMBCClassID.Text);
                    lblMBCClassID.Visible = true;
                }
                else
                {
                    lblMBCClassID.Visible = false;
                }
                if (btnsave.Text == "Update" & txtclassname.Text != "" & txtMBCClassID.Text != " ")
                {
                    BOLClass bolclassupdate = new BOLClass();
                    bolclassupdate.Id = Int32.Parse(lblid.Text); 
                    bolclassupdate.ClassName = txtclassname.Text;
                    bolclassupdate.MBCClassID = txtMBCClassID.Text;

                    int isupdate = 0;

                    isupdate=dalclass.updateClassByClassID(bolclassupdate);
                    if (isupdate == 1)
                    {                        
                            MessageBox.Show(" Class is Successfully Update");
                            txtclassname.Text = "";
                            txtMBCClassID.Text = "";
                            btnsave.Text = "&Save";
                            //tabclass.SelectedIndex = 1;
                            frmclassentry_Load(sender, e);
                            lblid.Text = dalclass.GetClassID().ToString();
                    }
                    else
                    {
                        MessageBox.Show(" This Record is Already Exist ");
                        txtclassname.Focus();
                        txtclassname.SelectAll();
                    }
                }
                if (btnsave.Text == "&Save" & txtclassname.Text != "" & txtMBCClassID.Text != " ")
                {
                    int issaved = 0;
                    bolclass = new BOLClass();
                    bolclass.ClassName = txtclassname.Text;
                    bolclass.MBCClassID = txtMBCClassID.Text;
                    issaved = dalclass.SaveClass(bolclass);

                    if (issaved == 1)
                    {
                            MessageBox.Show("Class Name is Successfully Saved");
                            txtclassname.Text = "";
                            txtMBCClassID.Text = "";
                            lblid.Text = dalclass.GetClassID().ToString();
                            //tabclass.SelectedIndex = 1;
                            frmclassentry_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show(" This Record is Already Exist ");
                        txtclassname.Focus();
                        txtclassname.SelectAll();

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

        private void dgvclass_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    if (e.RowIndex >= 0)
                    {
                        int classid = 0; string mbcclassid = "";
                        classid = Int32.Parse(dgvclass.Rows[e.RowIndex].Cells[0].Value.ToString());
                        mbcclassid = dgvclass.Rows[e.RowIndex].Cells[2].Value.ToString();
                        tabclass.SelectedIndex = 0;

                        lblid.Text=dgvclass.Rows[e.RowIndex].Cells[0].Value.ToString();
                        txtclassname.Text=dgvclass.Rows[e.RowIndex].Cells[1].Value.ToString();
                        txtMBCClassID.Text = dgvclass.Rows[e.RowIndex].Cells[2].Value.ToString();

                        btnsave.Text="Update";
                    }
                }
                if (e.ColumnIndex == 4)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int classid = 0; string mbcclassid = "";
                            classid = Int32.Parse(dgvclass.Rows[e.RowIndex].Cells[0].Value.ToString());
                            mbcclassid = dgvclass.Rows[e.RowIndex].Cells[2].Value.ToString();
                            int isdelete = 0;
                            isdelete = dalclass.DeleteClass(classid);
                            //if (isdelete == 1)
                            //{
                                MessageBox.Show("Successfully Deleted!");
                                frmclassentry_Load(sender, e);
                            //}
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
                txtclassname.Text = "";
                txtMBCClassID.Text = "";
                btnsave.Text = "&Save";
                lblid.Text = dalclass.GetClassID().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmclassentry_Load(object sender, EventArgs e)
        {
            try
            {
                dgvclass.Rows.Clear();
                List<BOLClass> lstclass = new List<BOLClass>();
                lstclass = dalclass.SelectAllClass();

                foreach (BOLClass c in lstclass)
                {
                    dgvclass.Rows.Add(c.Id, c.ClassName, c.MBCClassID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtclassname_KeyDown(object sender, KeyEventArgs e)
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
