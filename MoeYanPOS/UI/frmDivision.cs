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
    public partial class frmDivision : Form
    {
        BOLDivision boldivision = new BOLDivision();
        DALDivision daldivision = new DALDivision();
        public frmDivision()
        {
            InitializeComponent();
            lblid.Text = daldivision.GetDivisionID().ToString();
        }

        private void frmDivision_Load(object sender, EventArgs e)
        {
            try
            {
                dgvdivision.Rows.Clear();
                List<BOLDivision> lstdivision = new List<BOLDivision>();
                lstdivision = daldivision.SelectAllDivision();

                foreach (BOLDivision d in lstdivision)
                {
                    dgvdivision.Rows.Add(d.Id, d.Division);
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
                if (Validation.isNullOrEmptyField(" Division ", txtdivision.Text) != "")
                {
                    lbldivision.Text = Validation.isNullOrEmptyField(" Division ", txtdivision.Text);
                    lbldivision.Visible = true;
                }
                else
                {
                    lbldivision.Visible = false;
                }
                if (btnsave.Text == "Update" & txtdivision.Text != "" & txtdivision.Text != " ")
                {
                    BOLDivision boldivision = new BOLDivision();
                    boldivision.Id = Int32.Parse(lblid.Text);
                    boldivision.Division = txtdivision.Text;

                    int isupdate = 0;

                    isupdate = daldivision.UpdateDivision(boldivision);
                    if (isupdate == 1)
                    {
                        MessageBox.Show("Division is Successfully Update");
                        txtdivision.Text = "";
                        btnsave.Text ="&Save";
                        lblid.Text = daldivision.GetDivisionID().ToString();
                        tabdivision.TabIndex = 1;
                        frmDivision_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("This Record is Already Exist");
                        txtdivision.Focus();
                        txtdivision.SelectAll();
                    }
                }
                if (btnsave.Text == "&Save" & txtdivision.Text != "" & txtdivision.Text != " ")
                {
                    int issaved = 0;
                    boldivision = new BOLDivision();
                    boldivision.Id = Int32.Parse(lblid.Text);
                    boldivision.Division = txtdivision.Text;

                    issaved = daldivision.SaveDivision(boldivision);

                    if (issaved == 1)
                    {
                        MessageBox.Show(" Division is Successfully Saved");
                        txtdivision.Text = "";
                        lblid.Text = daldivision.GetDivisionID().ToString();
                        tabdivision.SelectedIndex = 1;
                        frmDivision_Load(sender, e);
                        
                    }
                    else
                    {
                        MessageBox.Show("This Record is Already Exist");
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

        private void dgvdivision_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2)
                {
                    if (e.RowIndex >= 0)
                    {
                        int divisionid = 0;
                        divisionid = Int32.Parse(dgvdivision.Rows[e.RowIndex].Cells[0].Value.ToString());
                        tabdivision.SelectedIndex = 0;

                        lblid.Text = dgvdivision.Rows[e.RowIndex].Cells[0].Value.ToString();
                        txtdivision.Text = dgvdivision.Rows[e.RowIndex].Cells[1].Value.ToString();
                        lbldivision.Visible = false;
                        btnsave.Text = "Update";
                    }
                }
                if (e.ColumnIndex == 3)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int divisionid = 0;
                            divisionid = Int32.Parse(dgvdivision.Rows[e.RowIndex].Cells[0].Value.ToString());
                            int isdelete = 0;
                            isdelete = daldivision.DeleteDivision(divisionid);
                            if (isdelete == 1)
                            {
                                MessageBox.Show("Successfully Deleted!");
                                frmDivision_Load(sender, e);
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
                txtdivision.Text = "";
                btnsave.Text = "&Save";
                lblid.Text = daldivision.GetDivisionID().ToString();
                lbldivision.Visible = false;
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
