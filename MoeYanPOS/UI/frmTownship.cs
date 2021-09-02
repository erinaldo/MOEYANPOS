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
    public partial class frmTownship : Form
    {
        DALTownship daltownship = new DALTownship();

        public frmTownship()
        {
            InitializeComponent();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation.isNullOrEmptyField(" Division ", txtTownship.Text) != "")
                {
                    lblerror.Text = Validation.isNullOrEmptyField(" Division ", txtTownship.Text);
                    lblerror.Visible = true;
                }
                else
                {
                    lblerror.Visible = false;
                }
                if (btnsave.Text == "&Update" & txtTownship.Text != "" & txtTownship.Text != " " & txtMBCTownshipID.Text != " ")
                {
                    BOLTownship bolTownship = new BOLTownship();
                    bolTownship.Id = Int32.Parse(lblid.Text);
                    bolTownship.Township = txtTownship.Text;
                    bolTownship.DivisionID = Int32.Parse(cboDivision.SelectedValue.ToString());
                    bolTownship.MBCTownshipID = txtMBCTownshipID.Text;

                    int isupdate = 0;

                    isupdate = daltownship.UpdateTownship(bolTownship);
                    if (isupdate == 1)
                    {
                        MessageBox.Show("Township is Successfully Update");
                        txtTownship.Text = "";
                        btnsave.Text = "&Save";
                        lblid.Text ="0";
                        txtMBCTownshipID.Text = "";
                        tabtownship.TabIndex = 1;
                        frmTownship_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("This Record is Already Exist");
                        txtTownship.Focus();
                        txtTownship.SelectAll();
                    }
                }
                if (btnsave.Text == "&Save" & txtTownship.Text != "" & txtTownship.Text != " " & txtMBCTownshipID.Text != " " )
                {
                    BOLTownship bolTownship = new BOLTownship();
                    //bolTownship.Id = Int32.Parse(lblid.Text);
                    bolTownship.Township = txtTownship.Text;
                    bolTownship.DivisionID = Int32.Parse(cboDivision.SelectedValue.ToString());
                    bolTownship.MBCTownshipID = txtMBCTownshipID.Text;

                    int issaved = daltownship.SaveTownship(bolTownship);

                    if (issaved == 1)
                    {
                        MessageBox.Show(" Township is Successfully Saved");
                        txtTownship.Text = "";
                        lblid.Text = "0";
                        txtMBCTownshipID.Text = "";
                        tabtownship.TabIndex = 1;
                        frmTownship_Load(sender, e);

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

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                txtTownship.Text = "";
                btnsave.Text = "&Save";
                lblid.Text = "0" ;
                txtMBCTownshipID.Text = "";
                lblerror.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvTownship_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 5)
                {
                    if (e.RowIndex >= 0)
                    {                       
                        tabtownship.SelectedIndex = 0;
                        lblid.Text = dgvTownship.Rows[e.RowIndex].Cells[0].Value.ToString();
                        txtTownship.Text = dgvTownship.Rows[e.RowIndex].Cells[3].Value.ToString();
                        cboDivision.SelectedValue = Int32.Parse(dgvTownship.Rows[e.RowIndex].Cells[1].Value.ToString());
                        txtMBCTownshipID.Text = dgvTownship.Rows[e.RowIndex].Cells[4].Value.ToString();
                        lblid.Visible = false;
                        btnsave.Text = "&Update";
                    }
                }
                if (e.ColumnIndex == 6)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int id = 0;
                            id = Int32.Parse(dgvTownship.Rows[e.RowIndex].Cells[0].Value.ToString());
                            int isdelete = 0;
                            isdelete = daltownship.DeleteTownship(id);
                            if (isdelete == 1)
                            {
                                MessageBox.Show("Successfully Deleted!");
                                frmTownship_Load(sender, e);
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

        private void frmTownship_Load(object sender, EventArgs e)
        {
            try
            {
                dgvTownship.Rows.Clear();
               DataSet ds = new DataSet();
                ds = daltownship.ShowAllTownship();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvTownship.Rows.Add(ds.Tables[0].Rows[i].ItemArray[0].ToString(), ds.Tables[0].Rows[i].ItemArray[1].ToString(), ds.Tables[0].Rows[i].ItemArray[2].ToString(), ds.Tables[0].Rows[i].ItemArray[3].ToString(), ds.Tables[0].Rows[i].ItemArray[4].ToString());
                    }
                }

                DALDivision daldivision = new DALDivision();
                List<BOLDivision> lstdivision = new List<BOLDivision>();
                lstdivision = daldivision.SelectAllDivision();

                cboDivision.DisplayMember = "Division";
                cboDivision.ValueMember = "Id";
                cboDivision.DataSource = lstdivision;
                if (lstdivision.Count > 0)
                    cboDivision.SelectedIndex = 0;
                
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

        private void dgvTownship_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
