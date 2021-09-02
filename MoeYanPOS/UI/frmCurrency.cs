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
    public partial class frmCurrency : Form
    {
        BOLCurrency bolcurrency = new BOLCurrency();
        DALCurrency dalcurrency = new DALCurrency();
        public frmCurrency()
        {
            try
            {
                InitializeComponent();
                lblID.Text = dalcurrency.GetCurrencyID().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        public frmCurrency(int CurrencyID)
        {            
            try
            {
                InitializeComponent();
                lblID.Text = CurrencyID.ToString();
                BOLCurrency bolcurrency = new BOLCurrency();
                bolcurrency = dalcurrency.GetCurrency(CurrencyID);
                txtcurrency.Text = bolcurrency.Currency;
                txtMBCCurrencyID.Text = bolcurrency.MBCCurrencyID;
                lblID.Text = bolcurrency.Id.ToString();
                btnsave.Text = "Update";
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
                int issaved = 0;
                if (Validation.isNullOrEmptyField(" Currency ", txtcurrency.Text) != "")
                {
                    lblCurrency.Text = Validation.isNullOrEmptyField("Currency", txtcurrency.Text);
                    lblCurrency.Visible = true;
                }
                else
                {
                    lblCurrency.Visible = false;
                }
                if (Validation.isNullOrEmptyField(" Exchange Rate ", txtexchangerate.Text) != "")
                {
                    lblexchagerate.Text = Validation.isNullOrEmptyField("ExchangeRate", txtexchangerate.Text);
                    lblexchagerate.Visible = true;
                }
                else
                {
                    lblexchagerate.Visible = false;
                }
                if (Validation.isNullOrEmptyField(" MBC Currency ID ", txtMBCCurrencyID.Text) != "")
                {
                    lblMBCCurrencyID.Text = Validation.isNullOrEmptyField(" MBC Currency ID ", txtMBCCurrencyID.Text);
                    lblMBCCurrencyID.Visible = true;
                }
                else
                {
                    lblMBCCurrencyID.Visible = false;
                }
                if (btnsave.Text == "Update" & txtcurrency.Text != "" & txtexchangerate.Text != "" & txtMBCCurrencyID.Text != "")
                {
                    int isupdate = 0;
                    BOLCurrency bolcurrency = new BOLCurrency();
                    bolcurrency.Id = Int32.Parse(lblID.Text);
                    bolcurrency.Currency = txtcurrency.Text;
                    bolcurrency.Exchangerate = Decimal.Parse(txtexchangerate.Text);
                    bolcurrency.MBCCurrencyID = txtMBCCurrencyID.Text;

                    isupdate = dalcurrency.EditCurrency(bolcurrency);

                    if (isupdate == 1)
                    {
                        MessageBox.Show(" Currency is Successfully Update ");
                        txtcurrency.Text = "";
                        txtexchangerate.Text = "1";
                        txtMBCCurrencyID.Text = "";
                        btnsave.Text = "&Save";
                        //tabcurrency.SelectedIndex = 1;
                        frmCurrency_Load(sender, e);
                        lblID.Text = dalcurrency.GetCurrencyID().ToString();
                    }
                    else
                    {
                        MessageBox.Show(" This Record is Already Exist");
                    }
                }
                if (btnsave.Text == "&Save" & txtcurrency.Text != "" & txtexchangerate.Text != "")
                {
                  
                    if (txtcurrency.Text != "")
                    {
                        if (txtexchangerate.Text != "")
                        {

                            bolcurrency = new BOLCurrency();
                            bolcurrency.Currency = txtcurrency.Text;
                            bolcurrency.Exchangerate = Decimal.Parse(txtexchangerate.Text);
                            bolcurrency.MBCCurrencyID = txtMBCCurrencyID.Text;

                            issaved = dalcurrency.SaveCurrency(bolcurrency);

                            if (issaved == 1)
                            {
                                MessageBox.Show(" Currency is Successfully Saved ");
                                txtexchangerate.Text = "1";
                                txtcurrency.Text = "";
                                txtMBCCurrencyID.Text = "";
                                //tabcurrency.SelectedIndex=1;
                                frmCurrency_Load(sender, e);
                                lblID.Text = dalcurrency.GetCurrencyID().ToString();
                            }
                            else
                            {
                                MessageBox.Show(" Duplicate Currency ");
                                txtcurrency.Focus();
                                txtcurrency.SelectAll();
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

        private void frmCurrency_Load(object sender, EventArgs e)
        {
            try
            {
                dgvcurrency.Rows.Clear();
                List<BOLCurrency> lstcurrency = new List<BOLCurrency>();
                lstcurrency = dalcurrency.ShowAllCurrency();

                foreach (BOLCurrency cu in lstcurrency)
                {
                    dgvcurrency.Rows.Add(cu.Id, cu.Currency, cu.Exchangerate,cu.MBCCurrencyID );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvcurrency_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4)
                {
                    if (e.RowIndex >= 0)
                    {
                        int currencyid = 0;
                        currencyid = Int32.Parse(dgvcurrency.Rows[e.RowIndex].Cells[0].Value.ToString());
                        tabcurrency.SelectedIndex = 0;

                        lblID.Text = dgvcurrency.Rows[e.RowIndex].Cells[0].Value.ToString();
                        txtcurrency.Text = dgvcurrency.Rows[e.RowIndex].Cells[1].Value.ToString();
                        txtexchangerate.Text = dgvcurrency.Rows[e.RowIndex].Cells[2].Value.ToString();
                        txtMBCCurrencyID.Text = dgvcurrency.Rows[e.RowIndex].Cells[3].Value.ToString();

                        btnsave.Text = "Update";
                    }
                }
                if (e.ColumnIndex == 5)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int id = 0;
                            id = Int32.Parse(dgvcurrency.Rows[e.RowIndex].Cells[0].Value.ToString());
                            int isdelete = 0;
                            isdelete = dalcurrency.DeleteCurrency(id);
                            if (isdelete == 1)
                            {
                                MessageBox.Show("Successfully Deleted!");
                                frmCurrency_Load(sender, e);
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
                txtcurrency.Text = "";
                txtexchangerate.Text = "1";
                txtMBCCurrencyID.Text = "";
                btnsave.Text = "&Save";
                lblID.Text = dalcurrency.GetCurrencyID().ToString();
                lblCurrency.Visible = false;
                lblexchagerate.Visible = false;
                lblMBCCurrencyID.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void txtexchangerate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string strCheck = "";
                    strCheck = MoeYanPOS.Function.Validation.isNumberField("Exchange Rate", txtexchangerate.Text);
                    if (strCheck != "")
                    {
                        MessageBox.Show(strCheck);
                        txtexchangerate.Text = "1";
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        //private void txtexchangerate_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string strCheck = "";
        //        strCheck = MoeYanPOS.Function.Validation.isNumberField("Exchange Rate", txtexchangerate.Text);
        //        if (strCheck != "")
        //        {
        //            MessageBox.Show(strCheck);
        //            txtexchangerate.Text = "1";
        //        }
                
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message.ToString());
        //    }
        //}

        private void picClose1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
