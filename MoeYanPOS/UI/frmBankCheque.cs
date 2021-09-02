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
    public partial class frmBankCheque : Form
    {
        BOLBankCheque bolbankcheque = new BOLBankCheque();
        DALBankCheque dalbankcheque = new DALBankCheque();
        public frmBankCheque()
        {
            try
            {
                InitializeComponent();
                txtBankChequeID.Text = dalbankcheque.GetBankChequeID().ToString();
                DALBankCheque dal = new DALBankCheque();
                List<BolLocation> lstlocation = new List<BolLocation>();
                lstlocation = dal.FillLocation();
                
                cboLocationName.DisplayMember = "Location";
                cboLocationName.ValueMember = "ID";
                cboLocationName.DataSource = lstlocation ;
                if (lstlocation .Count > 0)
                {
                    cboLocationName.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
         }

        private void CleanBankCheque()
        {
            cboLocationName.SelectedIndex = 0;
            txtBankChequeID.Text = "";
            txtBankChequeName.Text = "";
            btnsave.Text = "&Save";
            lblerror.Visible = false;
            txtMyanmarName.Text = "";
        }

        private void txtBankChequeName_KeyDown(object sender, KeyEventArgs e)
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
           
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation.isNullOrEmptyField(" Bank Cheque Name ", txtBankChequeName.Text) != "")
                {
                    lblerror.Text = Validation.isNullOrEmptyField(" Bank Cheque Name ", txtBankChequeName.Text);
                    lblerror.Visible = true;
                }
                else
                {
                    lblerror.Visible = false;
                }

                if (btnsave.Text == "Update" & txtBankChequeID.Text != "" & txtBankChequeName.Text != " ")
                {
                    int update = 0;
                    BOLBankCheque bolbankcheque = new BOLBankCheque();
                    dgvBankCheque.Rows.Clear();
                    bolbankcheque.BankChequeID = Int32.Parse(txtBankChequeID.Text);
                    bolbankcheque.BankChequeName = txtBankChequeName.Text;
                    bolbankcheque.MyanmarName = txtMyanmarName.Text;
                    bolbankcheque.LocationID = Int32.Parse(cboLocationName.SelectedValue.ToString());

                    update=dalbankcheque.UpdateBankCheque(bolbankcheque);

                    if (update == 1)
                    {
                        MessageBox.Show("Bank Cheque Record is Successfully Updated");
                        CleanBankCheque();
                        //tabcategory.SelectedIndex = 1;
                        frmBankCheque_Load(sender, e);
                        txtBankChequeID.Text = dalbankcheque.GetBankChequeID().ToString();
                    }
                    else
                    {
                        MessageBox.Show("This Record is Already Exist!");
                        txtBankChequeID.Focus();
                        txtBankChequeID.SelectAll();
                    }
                }
                if (btnsave.Text == "&Save" & txtBankChequeID.Text != "" & txtBankChequeName.Text != " ")
                {
                    int issaved = 0;
                    bolbankcheque = new BOLBankCheque();
                    bolbankcheque.BankChequeID = Int32.Parse(txtBankChequeID.Text);
                    bolbankcheque.LocationID = Int32.Parse(cboLocationName.SelectedValue.ToString());
                    bolbankcheque.BankChequeName = txtBankChequeName.Text;
                    bolbankcheque.MyanmarName = txtMyanmarName.Text;

                    //added by KSAung
                    if (btnsave.Text == "&Save")
                    {
                        List<BOLBankCheque> getbankchequeid = new List<BOLBankCheque>();
                        getbankchequeid = dalbankcheque.DuplicateBankCheque(txtBankChequeID.Text);
                        if (getbankchequeid.Count > 0)
                        {
                            MessageBox.Show("Duplicate Bank Cheque ID");
                            return;
                        }
                    }

                    issaved = dalbankcheque.SaveBankCheque(bolbankcheque);

                    if (issaved == 1)
                    {
                        MessageBox.Show("Bank Cheque Record is Successfully Saved");
                        CleanBankCheque();
                        //txtBankChequeID.Text = Int32.Parse(txtBankChequeID.Text);
                        //tabcategory.SelectedIndex = 1;
                        frmBankCheque_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("This Record is Already Exist!");
                        txtBankChequeID.Focus();
                        txtBankChequeID.SelectAll();
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

        private void btnclear_Click_1(object sender, EventArgs e)
        {
            try
            {
                DALBankCheque dal = new DALBankCheque();
                List<BolLocation> lstlocation = new List<BolLocation>();
                lstlocation = dal.FillLocation();

                cboLocationName.DisplayMember = "LocationName";
                cboLocationName.ValueMember = "ID";
                cboLocationName.DataSource = lstlocation;
                cboLocationName.SelectedIndex = 0;

                dgvBankCheque.Rows.Clear();
                List<BOLBankCheque> lstbankcheque = new List<BOLBankCheque>();
                lstbankcheque = dalbankcheque.ShowAllBankCheque();
                foreach (BOLBankCheque c in lstbankcheque)
                {
                    dgvBankCheque.Rows.Add(c.BankChequeID, c.BankChequeName, c.MyanmarName, c.LocationName);
                }
                cboLocationName.SelectedIndex = 0;
                txtBankChequeName.Text = "";
                txtMyanmarName.Text = "";
                txtBankChequeID.Text = dalbankcheque.GetBankChequeID().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvBankCheque_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4)
                {
                    if (e.RowIndex >= 0)
                    {
                        int bankchequeid = 0;
                        bankchequeid = Int32.Parse(dgvBankCheque.Rows[e.RowIndex].Cells[0].Value.ToString());
                        tabbankcheque.SelectedIndex = 0;

                        txtBankChequeID.Text = dgvBankCheque.Rows[e.RowIndex].Cells[0].Value.ToString();
                        txtBankChequeName.Text = dgvBankCheque.Rows[e.RowIndex].Cells[1].Value.ToString();
                        txtMyanmarName.Text = dgvBankCheque.Rows[e.RowIndex].Cells[2].Value.ToString();
                        cboLocationName.Text = dgvBankCheque.Rows[e.RowIndex].Cells[3].Value.ToString();
                    }

                    btnsave.Text = "Update";
                }

                if (e.ColumnIndex == 5)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int bankchequeid = 0;
                            //dgvcategory.Rows.Clear();
                            bankchequeid = Int32.Parse(dgvBankCheque.Rows[e.RowIndex].Cells[0].Value.ToString());
                            int isdelete = 0;
                            isdelete = dalbankcheque.DeleteBankCheque(bankchequeid);
                            //dgvcategory.Rows.Clear();
                            if (isdelete == 1)
                            {
                                MessageBox.Show("Successfully Deleted!");
                                frmBankCheque_Load(sender, e);
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

        private void frmBankCheque_Load(object sender, EventArgs e)
        {
            try
            {
                List<BOLBankCheque> lstbankcheque = new List<BOLBankCheque>();
                dgvBankCheque.Rows.Clear();
                lstbankcheque = dalbankcheque.ShowAllBankCheque();
                foreach (BOLBankCheque c in lstbankcheque)
                {
                    dgvBankCheque.Rows.Add(c.BankChequeID, c.BankChequeName, c.MyanmarName, c.LocationName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void picClose1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
