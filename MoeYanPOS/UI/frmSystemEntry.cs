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
    public partial class frmSystemEntry : Form
    {
        BOLSystem bolsystem = new BOLSystem();
        DALSystem dalsystem = new DALSystem();
        public frmSystemEntry()
        {
            InitializeComponent();
            lblCompanyID.Text = dalsystem.GetSystemID().ToString();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation.isNullOrEmptyField(" Company Name ", txtcompanyname.Text) != "")
                {
                    lblcompanyname.Text = Validation.isNullOrEmptyField(" Company Name ", txtcompanyname.Text);
                    lblcompanyname.Visible = true;
                }
                else
                {
                    lblcompanyname.Visible = false;
                }
                if (Validation.isNullOrEmptyField(" Address ", txtAddress.Text) != "")
                {
                    lbladdress.Text = Validation.isNullOrEmptyField(" Address ", txtAddress.Text);
                    lbladdress.Visible = true;
                }
                else
                {
                    lbladdress.Visible = false;
                }
                if (Validation.isNullOrEmptyField(" PhoneNo ", txtphno.Text) != "")
                {
                    lblphno.Text = Validation.isNullOrEmptyField(" Phone No ", txtphno.Text);
                    lblphno.Visible = true;
                }
                else
                {
                    lblphno.Visible = false;
                }
                if (Validation.isNullOrEmptyField(" Web ", txtweb.Text) != "")
                {
                    lblweb.Text = Validation.isNullOrEmptyField(" Web ", txtweb.Text);
                    lblweb.Visible = true;
                }
                else
                {
                    lblweb.Visible = false;
                }
                if (Validation.isNullOrEmptyField(" Email ", txtemail.Text) != "")
                {
                    lblemail.Text = Validation.isNullOrEmptyField(" Email ", txtemail.Text);
                    lblemail.Visible = true;
                }
                else
                {
                    lblemail.Visible = false;
                }
                 if (Validation.isNullOrEmptyField(" PrinterSlip ", txtprinterslip.Text) != "")
                {
                    lblprinterslip.Text = Validation.isNullOrEmptyField(" PrinterSlip ", txtprinterslip.Text);
                    lblprinterslip.Visible = true;
                }
                 else
                 {
                     lblprinterslip.Visible = false;
                 }
                if (Validation.isNullOrEmptyField(" PrinterVoucher ", txtprintervoucher.Text) != "")
                {
                    lblprintervoucher.Text = Validation.isNullOrEmptyField(" PrinterVoucher ", txtprintervoucher.Text);
                    lblprintervoucher.Visible = true;
                }
                else
                {
                    lblprintervoucher.Visible = false;
                }
                if (btnsave.Text == "Update" & txtcompanyname.Text!="" & txtAddress.Text!="" & txtphno.Text!="" & txtweb.Text!="" & txtemail.Text!="" & txtprinterslip.Text!="" & txtprintervoucher.Text!="")
                {
                    int isupdate = 0;
                    BOLSystem bolsystemupdate = new BOLSystem();
                    bolsystemupdate.Companyid = Int32.Parse(lblCompanyID.Text);
                    bolsystemupdate.Companyname = txtcompanyname.Text;
                    bolsystemupdate.Address = txtAddress.Text;
                    bolsystemupdate.Phno = txtphno.Text;
                    bolsystemupdate.Web = txtweb.Text;
                    bolsystemupdate.Email = txtemail.Text;
                    bolsystemupdate.Printerslip = txtprinterslip.Text;
                    bolsystemupdate.Printervoucher = txtprintervoucher.Text;

                    isupdate=dalsystem.UpdateSystem(bolsystemupdate);

                    if (isupdate == 1)
                    {
                        MessageBox.Show("System is Successfully Updated");
                        CleanTextBox();
                        btnsave.Text = "&Save";
                        //tabsystem.SelectedIndex = 1;
                        frmSystemEntry_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("This Record is Already Exist");
                        txtcompanyname.Focus();
                        txtcompanyname.SelectAll();
                    }
                }
                if (btnsave.Text == "&Save" & txtcompanyname.Text != "" & txtAddress.Text != "" & txtphno.Text != "" & txtweb.Text != "" & txtemail.Text != "" & txtprinterslip.Text != "" & txtprintervoucher.Text != "")
                {
                    int issaved = 0;
                    bolsystem.Companyname = txtcompanyname.Text;
                    bolsystem.Address = txtAddress.Text;
                    bolsystem.Phno = txtphno.Text;
                    bolsystem.Web = txtweb.Text;
                    bolsystem.Email = txtemail.Text;
                    bolsystem.Printerslip = txtprinterslip.Text;
                    bolsystem.Printervoucher = txtprintervoucher.Text;

                    int chkSystemSetting = 1;

                    chkSystemSetting = dalsystem.CheckSystemSetting();
                    if (chkSystemSetting == 0)
                    {
                        issaved = dalsystem.SaveSystem(bolsystem);

                        if (issaved == 1)
                        {
                            MessageBox.Show("System is Successfully Saved");
                            CleanTextBox();
                            //tabsystem.SelectedIndex = 1;
                            frmSystemEntry_Load(sender, e);
                        }
                        else
                        {
                            MessageBox.Show(" This System duplicate Record ");
                            txtcompanyname.Focus();
                            txtcompanyname.SelectAll();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Only one Record can Save.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        
        }
        public void CleanTextBox()
        {
            txtcompanyname.Text = "";
            txtAddress.Text = "";
            txtphno.Text = "";
            txtweb.Text = "";
            txtemail.Text = "";
            txtprinterslip.Text = "";
            txtprintervoucher.Text = "";
        }

        private void frmSystemEntry_Load(object sender, EventArgs e)
        {
            try
            {
                dgvsystem.Rows.Clear();
                List<BOLSystem> lstsystem = new List<BOLSystem>();
                lstsystem = dalsystem.ShowAllSystem();

                foreach (BOLSystem s in lstsystem)
                {
                    dgvsystem.Rows.Add(s.Companyid, s.Companyname, s.Address, s.Phno, s.Web, s.Email, s.Printerslip, s.Printervoucher);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvsystem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 8)
                {
                    if (e.RowIndex >= 0)
                    {
                        int companyid = 0;
                        companyid = Int32.Parse(dgvsystem.Rows[e.RowIndex].Cells[0].Value.ToString());
                        tabsystem.SelectedIndex = 0;

                        lblCompanyID.Text=dgvsystem.Rows[e.RowIndex].Cells[0].Value.ToString();
                        txtcompanyname.Text=dgvsystem.Rows[e.RowIndex].Cells[1].Value.ToString();
                        txtAddress.Text=dgvsystem.Rows[e.RowIndex].Cells[2].Value.ToString();
                        txtphno.Text = dgvsystem.Rows[e.RowIndex].Cells[3].Value.ToString();
                        txtweb.Text = dgvsystem.Rows[e.RowIndex].Cells[4].Value.ToString();
                        txtemail.Text = dgvsystem.Rows[e.RowIndex].Cells[5].Value.ToString();
                        txtprinterslip.Text = dgvsystem.Rows[e.RowIndex].Cells[6].Value.ToString();
                        txtprintervoucher.Text = dgvsystem.Rows[e.RowIndex].Cells[7].Value.ToString();

                        btnsave.Text = "Update";
                    }
                }
                if (e.ColumnIndex == 9)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int companyid = 0;
                            companyid = Int32.Parse(dgvsystem.Rows[e.RowIndex].Cells[0].Value.ToString());
                            int isdelete = 0;
                            isdelete = dalsystem.DeleteSystem(companyid);
                            if (isdelete == 1)
                            {
                                MessageBox.Show("Successfully deleted!");
                                frmSystemEntry_Load(sender, e);
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
                CleanTextBox();
                btnsave.Text = "&Save";
                lblCompanyID.Text = dalsystem.GetSystemID().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtprintervoucher_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtprintervoucher_KeyDown(object sender, KeyEventArgs e)
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
