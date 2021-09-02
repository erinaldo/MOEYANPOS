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
    public partial class frmBrand : Form
    {
        DALBrand dalbrand = new DALBrand();
        BOLBrand bolbrand = new BOLBrand();

        public frmBrand()
        {
            InitializeComponent();
            lblid.Text = dalbrand.GetBrandID().ToString();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation.isNullOrEmptyField(" Brand Name ", txtBrandName.Text) != "")
                {
                    lblbrandname.Text = Validation.isNullOrEmptyField(" Brand Name ", txtBrandName.Text);
                    lblbrandname.Visible = true;
                }
                else
                {
                    lblbrandname.Visible = false;
                }
                if (btnsave.Text == "Update" & txtBrandName.Text != "" & txtBrandName.Text != " ")
                {
                    int update = 0;
                    BOLBrand bolbrand = new BOLBrand();
                    bolbrand.Id = Int32.Parse(lblid.Text);
                    bolbrand.Brandname = txtBrandName.Text;

                    update=dalbrand.UpdateBrand(bolbrand);

                    if (update == 1)
                    {
                        MessageBox.Show("Category Record is Successfully Updated");
                        txtBrandName.Text = "";
                        //tabBrand.SelectedIndex = 1;
                        btnsave.Text = "&Save";
                        frmBrand_Load(sender, e);
                        lblid.Text = dalbrand.GetBrandID().ToString();
                    }
                    else
                    {
                        MessageBox.Show("This Record is Already Exist!");
                        txtBrandName.Focus();
                        txtBrandName.SelectAll();
                    }
                }
                if (btnsave.Text == "&Save" & txtBrandName.Text != "" & txtBrandName.Text != " ")
                {
                    int issaved = 0;
                    bolbrand = new BOLBrand();
                    bolbrand.Id = Int32.Parse(lblBrandID.Text);
                    bolbrand.Brandname = txtBrandName.Text;
                    if (lblBrandID.Text == "0")
                    {
                        bolbrand.Action = 0;
                    }
                    else
                    {
                        bolbrand.Action = 1;
                    }

                    issaved = dalbrand.SaveBrand(bolbrand);
                    if(issaved==1 & txtBrandName.Text!="")
                    {
                        MessageBox.Show("Brand Name is Successfully Saved");
                        txtBrandName.Text="";
                        lblid.Text=dalbrand.GetBrandID().ToString();
                        btnsave.Text = "&Save";
                        //.SelectedIndex = 1;
                        frmBrand_Load(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void LoadBrand()
        {
            try
            {
                List<BOLBrand> lstbrand = new List<BOLBrand>();
                lstbrand = dalbrand.ShowAllBrand(0);
                dgvbrand.Rows.Clear();
                if (lstbrand.Count > 0)
                {
                    for (int i = 0; i < lstbrand.Count; i++)
                    {
                        dgvbrand.Rows.Add(lstbrand[i].Id, lstbrand[i].Brandname);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmBrand_Load(object sender, EventArgs e)
        {
            try
            {
                LoadBrand();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvbrand_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(e.ColumnIndex==2)
                {
                    List<BOLBrand> lstbrand = new List<BOLBrand>();
                    lstbrand = dalbrand.ShowAllBrand(Int32.Parse(dgvbrand.Rows[e.RowIndex].Cells[0].Value.ToString()));
                    if (lstbrand.Count > 0)
                    {
                        lblid.Text = dgvbrand.Rows[e.RowIndex].Cells[0].Value.ToString();
                        txtBrandName.Text = dgvbrand.Rows[e.RowIndex].Cells[1].Value.ToString();
                        btnsave.Text = "Update";
                        tabBrand.SelectedIndex = 0;
                    }
                }
                if (e.ColumnIndex == 3)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int id = 0;
                            id = Int32.Parse(dgvbrand.Rows[e.RowIndex].Cells[0].Value.ToString());
                            int isdelete = 0;
                            isdelete = dalbrand.DeleteBrand(id);
                            if (isdelete == 1)
                            {
                                MessageBox.Show("Successfully Deleted!");
                                frmBrand_Load(sender, e);
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

        private void dgvbrand_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

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
                txtBrandName.Text = "";
                btnsave.Text = "&Save";
                lblid.Text = dalbrand.GetBrandID().ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtBrandName_KeyDown(object sender, KeyEventArgs e)
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
