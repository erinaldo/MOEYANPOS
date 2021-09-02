using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MoeYanPOS.Function;
using MoeYanPOS.BOL;
using MoeYanPOS.DAL;
using CrystalDecisions.CrystalReports.Engine;

namespace MoeYanPOS.UI
{
    public partial class frmSupplier : Form
    {
        DALSupplier dalsupplier = new DALSupplier();
        BOLSupplier bolsupplier = new BOLSupplier();
        DALTownship daltownship = new DALTownship();

        public frmSupplier()
        {
            InitializeComponent();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation.isNullOrEmptyField(" Supplier Name ", txtsuppliername.Text) != "")
                {
                    lblsuppliername.Text = Validation.isNullOrEmptyField("Supplier Name", txtsuppliername.Text);
                    lblsuppliername.Visible = true;
                }
                else
                {
                    lblsuppliername.Visible = false;
                }
                if (Validation.isEmail(" EMail ", txtemail.Text) != "")
                {
                    lblEmail.Text = Validation.isNullOrEmptyField(" EMail ", txtemail.Text);
                    lblEmail.Visible = true;
                }
                else
                {
                    lblEmail.Visible = false;
                }
                if (btnsave.Text == "&Update" & txtsuppliername.Text != "" & txtemail.Text != "")
                {
                    int isupdate = 0;
                    BOLSupplier bolsupplier = new BOLSupplier();
                    bolsupplier.Supplierid = Int32.Parse(lblsupplierid.Text);
                    bolsupplier.SupplierName = txtsuppliername.Text;
                    bolsupplier.Address = txtaddress.Text;
                    bolsupplier.Phone = txtPhone.Text;
                    bolsupplier.Email = txtemail.Text;
                    bolsupplier.TownshipID = Int32.Parse(cboTownship.SelectedValue.ToString());
                    bolsupplier.Creditlimit = Decimal.Parse(txtcreditlimit.Text);

                    isupdate = dalsupplier.EditSupplier(bolsupplier);


                    MessageBox.Show("Supplier is Successfully Update ");
                    CleanTextBox();
                    frmSupplier_Load(sender, e);

                }
                if (btnsave.Text == "&Save" & txtsuppliername.Text != "" & txtemail.Text != "")
                {
                    int issaved = 0;
                    BOLSupplier bolsupplier = new BOLSupplier();
                    bolsupplier.SupplierName = txtsuppliername.Text;
                    bolsupplier.Address = txtaddress.Text;
                    bolsupplier.Phone = txtPhone.Text;
                    bolsupplier.Email = txtemail.Text;
                    bolsupplier.TownshipID = Int32.Parse(cboTownship.SelectedValue.ToString());
                    bolsupplier.Creditlimit = Decimal.Parse(txtcreditlimit.Text);


                    if (chkcash.Checked)
                    {
                        bolsupplier.Iscash = true;
                    }

                    if (chkcredit.Checked)
                    {
                        bolsupplier.Iscredit = true;
                    }
                    issaved = dalsupplier.SaveSupplier(bolsupplier);

                    if (issaved == 1)
                    {
                        MessageBox.Show(" This Record is Successfully Saved ");
                        CleanTextBox();
                        frmSupplier_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show(" This Record is Already Exist");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void CleanTextBox()
        {
            lblsupplierid.Text = "0";
            txtsuppliername.Text = "";
            txtaddress.Text = "";
            txtPhone.Text = "";
            txtemail.Text = "";
            txtcreditlimit.Text = "";
            chkcredit.Checked = false;
            chkcash.Checked = false;
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

        private void frmSupplier_Load(object sender, EventArgs e)
        {
            try
            {
                List<BOLTownship> lstTownship = new List<BOLTownship>();
                lstTownship = daltownship.GetAllTownship();
                cboTownship.DisplayMember = "Township";
                cboTownship.ValueMember = "ID";
                cboTownship.DataSource = lstTownship;

                List<BOLSupplier> lstsupplier = new List<BOLSupplier>();
                dgvsupplier.Rows.Clear();
                lstsupplier = dalsupplier.ShowAllSupplier();

                foreach (BOLSupplier s in lstsupplier)
                {
                    dgvsupplier.Rows.Add(false, s.Supplierid, s.SupplierName, s.Address, s.Phone, s.Email, s.Township, s.Creditlimit, s.Iscash, s.Iscredit,"","",s.TownshipID);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvsupplier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 10)
                {
                    if (e.RowIndex >= 0)
                    {
                        int supplierid = 0;
                        supplierid = Int32.Parse(dgvsupplier.Rows[e.RowIndex].Cells[1].Value.ToString());
                        tabsupplier.SelectedIndex = 0;

                        lblsupplierid.Text = dgvsupplier.Rows[e.RowIndex].Cells[1].Value.ToString();
                        txtsuppliername.Text = dgvsupplier.Rows[e.RowIndex].Cells[2].Value.ToString();
                        txtaddress.Text = dgvsupplier.Rows[e.RowIndex].Cells[3].Value.ToString();
                        txtPhone.Text = dgvsupplier.Rows[e.RowIndex].Cells[4].Value.ToString();
                        txtemail.Text = dgvsupplier.Rows[e.RowIndex].Cells[5].Value.ToString();
                        cboTownship.SelectedValue = Int32.Parse(dgvsupplier.Rows[e.RowIndex].Cells["TownshipID"].Value.ToString());
                        txtcreditlimit.Text = dgvsupplier.Rows[e.RowIndex].Cells[7].Value.ToString();

                        if (dgvsupplier.Rows[e.RowIndex].Cells[8].Value.ToString() == "True")
                        {
                            chkcash.Checked = true;
                        }
                        else
                        {
                            chkcash.Checked = false;
                        }

                        if (dgvsupplier.Rows[e.RowIndex].Cells[8].Value.ToString() == "True")
                        {
                            chkcredit.Checked = true;
                        }
                        else
                        {
                            chkcredit.Checked = false;
                        }


                        btnsave.Text = "&Update";
                    }
                }
                if (e.ColumnIndex == 11)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int id = 0;
                            id = Int32.Parse(dgvsupplier.Rows[e.RowIndex].Cells[1].Value.ToString());
                            int isdelete = 0;
                            isdelete = dalsupplier.DeleteSupplier(id);

                            if (isdelete == 1)
                            {
                                MessageBox.Show("Successfully Deleted!");
                                frmSupplier_Load(sender, e);
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

        private void chkcredit_CheckedChanged(object sender, EventArgs e)
        {
            if (chkcredit.Checked)
            {
                txtcreditlimit.Enabled = true;
                txtcreditlimit.Focus();
            }
        }

        private void chkcash_CheckedChanged(object sender, EventArgs e)
        {
            if (chkcash.Checked)
            {
                txtcreditlimit.Enabled = false;
            }
        }

        private void chkcash_Click(object sender, EventArgs e)
        {
            chkcredit.Checked = false;
        }

        private void chkcredit_Click(object sender, EventArgs e)
        {
            chkcash.Checked = false;
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            string division = "";
            division = txtdivision.Text;


            List<BOLSupplier> bolsupplierlist = new List<BOLSupplier>();
            bolsupplierlist = dalsupplier.SearchSupplier(division);
            dgvsupplier.Rows.Clear();

            if (bolsupplierlist.Count > 0)
            {
                for (int i = 0; i < bolsupplierlist.Count; i++)
                {
                    dgvsupplier.Rows.Add(false, bolsupplierlist[i].Supplierid, bolsupplierlist[i].SupplierName, bolsupplierlist[i].Address, bolsupplierlist[i].Phone, bolsupplierlist[i].Phone, bolsupplierlist[i].Email, bolsupplierlist[i].Township, bolsupplierlist[i].Creditlimit, bolsupplierlist[i].Iscash, bolsupplierlist[i].Iscredit,bolsupplierlist[i].TownshipID);
                }
            }
        }

        private void btnpreview_Click(object sender, EventArgs e)
        {
            try
            {
                List<BOLSupplier> lst = new List<BOLSupplier>();
                for (int i = 0; i < dgvsupplier.Rows.Count; i++)
                {
                    BOLSupplier bolsupplierpreview = new BOLSupplier();
                    if (dgvsupplier.Rows[i].Cells["chk"].Value.ToString() == "True")
                    {
                        bolsupplierpreview.Supplierid = Int32.Parse(dgvsupplier.Rows[i].Cells["SupplierID"].Value.ToString());
                        bolsupplierpreview.SupplierName = dgvsupplier.Rows[i].Cells[2].Value.ToString();
                        bolsupplierpreview.Address = dgvsupplier.Rows[i].Cells[3].Value.ToString();
                        bolsupplierpreview.Phone = dgvsupplier.Rows[i].Cells[4].Value.ToString();
                        bolsupplierpreview.Email = dgvsupplier.Rows[i].Cells[5].Value.ToString();
                        bolsupplierpreview.Township = dgvsupplier.Rows[i].Cells[6].Value.ToString();

                        //bolsupplierpreview.Creditlimit = Decimal.Parse(dgvsupplier.Rows[i].Cells[7].Value.ToString());
                       // bolsupplierpreview.Iscash = Boolean.Parse(dgvsupplier.Rows[i].Cells[8].Value.ToString());
                       // bolsupplierpreview.Iscredit = Boolean.Parse(dgvsupplier.Rows[i].Cells[9].Value.ToString());

                        //bolcustomerpreview.Credittype = dgvcustomer.Rows[i].Cells[11].Value.ToString();

                        lst.Add(bolsupplierpreview);
                    }

                }

                if (lst.Count > 0)
                {
                    this.UseWaitCursor = true;
                    ReportDocument cu_Report = new ReportDocument();
                    cu_Report.Load(Application.StartupPath + @"\Report\RptSupplier.rpt");
                    cu_Report.SetDataSource(lst);
                    cu_Report.SetDatabaseLogon("sa", "moeyan");

                    frmReport frmReport = new frmReport();
                    frmReport.rptViewer.ReportSource = cu_Report;
                    frmReport.rptViewer.Refresh();
                    frmReport.Show();
                    this.UseWaitCursor = false;
                }
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            CleanTextBox();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
