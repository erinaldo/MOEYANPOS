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
    public partial class frmLocation : Form
    {
        DALLocation dallocation = new DALLocation();
        public frmLocation()
        {
            InitializeComponent();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation.isNullOrEmptyField(" Location Name ", txtLocationName.Text) != "")
                {
                    lblLocationname.Text = Validation.isNullOrEmptyField(" Location Name ", txtLocationName.Text);
                    lblLocationname.Visible = true;
                }
                else
                {
                    lblLocationname.Visible = false;
                }
                if (btnsave.Text == "&Update" & txtLocationName.Text != "" & txtLocationName.Text != " ")
                {
                    int update = 0;
                    BolLocation bolLocation = new BolLocation();
                    bolLocation.ID = long.Parse(lblLocationID.Text);
                    bolLocation.Location = txtLocationName.Text;

                    update = dallocation.UpdateLocation(bolLocation);

                    if (update == 1)
                    {
                        MessageBox.Show("Location is Successfully Updated");
                        txtLocationName.Text = "";
                        //tabBrand.SelectedIndex = 1;
                        btnsave.Text = "&Save";
                        frmLocation_Load(sender, e);
                        lblLocationID.Text = "0";
                    }
                    else
                    {
                        MessageBox.Show("This Record is Already Exist!");
                        txtLocationName.Focus();
                        txtLocationName.SelectAll();
                    }
                }
                if (btnsave.Text == "&Save" & txtLocationName.Text != "" & txtLocationName.Text != " ")
                {
                    int issaved = 0;
                    BolLocation bolLocation = new BolLocation();
                    bolLocation.ID = long.Parse(lblLocationID.Text);
                    bolLocation.Location = txtLocationName.Text;

                    issaved = dallocation.SaveLocation(bolLocation);
                    if (issaved == 1)
                    {
                        MessageBox.Show("Location Name is Successfully Saved");
                        txtLocationName.Text = "";
                        //tabBrand.SelectedIndex = 1;
                        btnsave.Text = "&Save";
                        frmLocation_Load(sender, e);
                        lblLocationID.Text = "0";
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
                txtLocationName.Text = "";
                btnsave.Text = "&Save";
                lblLocationID.Text = "0";

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

        private void LoadLocation()
        {
            try
            {
                List<BolLocation> lstLocation = new List<BolLocation>();
                lstLocation = dallocation.GetAllLocation();
                dgvLocation.Rows.Clear();
                if (lstLocation.Count > 0)
                {
                    for (int i = 0; i < lstLocation.Count; i++)
                    {
                        dgvLocation.Rows.Add(lstLocation[i].ID, lstLocation[i].Location);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmLocation_Load(object sender, EventArgs e)
        {
            try
            {
                LoadLocation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtLocationName_KeyDown(object sender, KeyEventArgs e)
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

        private void dgvLocation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2)
                {
                    List<BOLBrand> lstbrand = new List<BOLBrand>();
                    lblLocationID.Text = dgvLocation.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtLocationName.Text = dgvLocation.Rows[e.RowIndex].Cells[1].Value.ToString();
                    btnsave.Text = "&Update";
                    tabBrand.SelectedIndex = 0;
                    
                }
                if (e.ColumnIndex == 3)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int id = 0;
                            id = Int32.Parse(dgvLocation.Rows[e.RowIndex].Cells[0].Value.ToString());
                            int isdelete = 0;
                            isdelete = dallocation.DeleteLocation(id);
                            if (isdelete == 1)
                            {
                                MessageBox.Show("Successfully Deleted!");
                                frmLocation_Load(sender, e);
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
