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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Drawing.Printing;
using System.IO;
using MoeYanPOS.UI;

namespace MoeYanPOS.UI
{
    public partial class frmPOS : Form
    {
        BOLUser LstCheckPrintAndMsgBox = new BOLUser();
        int Total = 0; int TotalFOC = 0; decimal TotalItemDiscount = 0; DateTime SaleDate; DateTime LotteryDate; string LocationNo; string SysVoucherNo; string Remark; string status = "";
        DALSale dalsale = new DALSale();
        DALSaleDetail dalsaledetail = new DALSaleDetail();
        DALExchangeRate dalexchangerate = new DALExchangeRate();
        DALCustomer dalcustomer = new DALCustomer();
        DALTransition daltrans = new DALTransition();
        DALStock dalstock = new DALStock();
        DALMeasurement dalmeasurement = new DALMeasurement();
        DALSystem dalSystem = new DALSystem();
        DALVoucherSetting dalVoucher = new DALVoucherSetting();
        DALLocation dalLocation = new DALLocation();
        List<Int32> SDetailID = new List<Int32>();
        DALCounter dalcounter = new DALCounter();
        int isSaved = 0; int isDetailSaved = 0,editindex=-1;
        long voucher = 0;string balance = "0", refund = "0";

        public frmPOS()
        {
            this.InitializeComponent();
        }

        public frmPOS(long voucherno)
        {
            this.InitializeComponent();
            this.voucher = voucherno;
        }

        private void picClose1_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void LoadForm()
        {
            this.lblCusID.Text = "";
            this.lblSaleID.Text = "";
            string currentloc = this.dalLocation.GetCurrentLocationCode();
            this.txtCustomer.Text = "";
            this.txtCustomerName.Text = "";
            string voucher = currentloc + MoeYanFunctions.MoeYanPOS_Helper.counterCode + DateTime.Now.ToString("yyMMdd") + this.dalsale.GetMaxVoucher("sale").ToString();
            this.txtVoucherNo.Text = voucher;
            this.cboPaymentType.SelectedIndex = 0;
            this.txtDrawingTimes.Text = "";
            this.txtItemCode.Text = "";
            this.dtpSaleDate.Value = DateTime.Now;
            List<BOLCurrency> lstCurrency = new List<BOLCurrency>();
            lstCurrency = this.dalexchangerate.FillCurrency();
            if (lstCurrency.Count > 0)
            {
                this.cbocurrency.DisplayMember = "Currency";
                this.cbocurrency.ValueMember = "Id";
                this.cbocurrency.DataSource = lstCurrency;
                this.cbocurrency.SelectedIndex = 0;
            }
            List<BolLocation> LstLocation = new List<BolLocation>();
            LstLocation = this.dalLocation.SelectAllLocation();
            this.cboLocation.DisplayMember = "Location";
            this.cboLocation.ValueMember = "ID";
            this.cboLocation.DataSource = LstLocation;
            for (int i = 0; i < LstLocation.Count; i++)
            {
                if (LstLocation[i].IsThisLocation)
                {
                    this.cboLocation.SelectedValue = LstLocation[i].ID;
                    this.LocationNo = this.dalLocation.GetLocationByID(LstLocation[i].ID).LocationNo;
                }
            }
            this.txtCounter.Text = MoeYanFunctions.MoeYanPOS_Helper.counterName;
            this.txtCreditAmount.Text = "0";
            this.txtCreditLimit.Text = "0";
            this.txtTqty.Text = "0";
            this.txtTddis.Text = "0";
            this.txtTdis.Text = "0";
            this.txtTotalAmount.Text = "0";
            this.txtBalance.Text = "0";
            this.txtPaidAmount.Text = "0";
            this.txtRefundAmount.Text = "0";
            this.dgvSale.Rows.Clear();
            this.dgvCustomer.Rows.Clear();
            this.dgvCustomer.Visible = false;
            this.dgvItemCode.Rows.Clear();
            this.dgvItemCode.Visible = false;
            this.txtRemark.Text = "";
            this.txtCustomer.Focus();
        }
        private void txtCustomer_TextChanged(object sender, EventArgs e)
        {
        }
        private void dgvItemCode_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void frmPOS_Load(object sender, EventArgs e)
        {
            this.LoadForm();
            if (this.voucher != 0L)
            {
                this.BindPOS(this.voucher);
            }
        }
        private void txtCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    List<BOLCustomer> lstcustomer = new List<BOLCustomer>();
                    lstcustomer = this.dalcustomer.GetCustomer2(this.txtCustomer.Text);
                    this.dgvCustomer.Rows.Clear();
                    foreach (BOLCustomer c in lstcustomer)
                    {
                        this.dgvCustomer.Rows.Add(new object[]
						{
							c.ID,
							c.CustomerID,
							c.MyanmarName,
							c.Creditlimit
						});
                    }
                    this.dgvCustomer.Visible = true;
                    this.dgvCustomer.Focus();
                }
                else if (e.KeyCode == Keys.F8)
                {
                    this.btnNew_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void dgvCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    List<BOLCustomer> bolcustomer = new List<BOLCustomer>();
                    this.txtCustomer.Text = this.dgvCustomer.CurrentRow.Cells[1].Value.ToString();
                    bolcustomer = this.dalcustomer.GetCustomer2(this.txtCustomer.Text);
                    this.txtCreditAmount.Text = "0";
                    this.txtCreditLimit.Text = "0";
                    foreach (BOLCustomer a in bolcustomer)
                    {
                        this.txtCreditAmount.Text = a.CreditAmount.ToString();
                        this.txtCreditLimit.Text = a.Creditlimit.ToString();
                    }
                    int iCreditAmount = int.Parse(this.txtCreditAmount.Text);
                    int iCreditLimit = int.Parse(this.txtCreditLimit.Text);
                    if (iCreditLimit >= iCreditAmount)
                    {
                        this.txtCreditAmount.ForeColor = Color.Green;
                    }
                    else
                    {
                        this.txtCreditAmount.ForeColor = Color.Red;
                    }
                    this.lblCusID.Text = this.dgvCustomer.CurrentRow.Cells[0].Value.ToString();
                    this.txtCustomer.Text = this.dgvCustomer.CurrentRow.Cells[1].Value.ToString();
                    this.txtCustomerName.Text = this.dgvCustomer.CurrentRow.Cells[2].Value.ToString();
                    this.dgvCustomer.Visible = false;
                    this.cboPaymentType.Focus();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    this.dgvCustomer.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void cboPaymentType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.cboPaymentType.Text == "Credit")
                {
                    if (!this.dalcustomer.ChkPaymentType((long)int.Parse(this.lblCusID.Text)))
                    {
                        MessageBox.Show("Customer Is not Allowed your Payment Type!", "Not Allowed Payment Type", MessageBoxButtons.OK);
                        this.cboPaymentType.Focus();
                    }
                    else
                    {
                        this.txtDrawingTimes.Focus();
                    }
                }
                else if (e.KeyCode == Keys.F8)
                {
                    this.btnNew_Click(sender, e);
                }
                else
                {
                    this.txtDrawingTimes.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void txtDrawingTimes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                this.txtItemCode.Focus();
            }
            else if (e.KeyCode == Keys.F8)
            {
                this.btnNew_Click(sender, e);
            }
        }
        private void txtItemCode_TextChanged(object sender, EventArgs e)
        {
        }
        private void dgvItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (this.editindex > -1)
                {
                    this.dgvSale.CurrentRow.Cells[1].Value = this.dgvItemCode.CurrentRow.Cells[1].Value.ToString();
                    this.dgvSale.CurrentRow.Cells[2].Value = this.dgvItemCode.CurrentRow.Cells[3].Value.ToString();
                    this.dgvSale.CurrentRow.Cells[4].Value = this.dgvItemCode.CurrentRow.Cells[4].Value.ToString();
                    this.dgvSale.CurrentRow.Cells[5].Value = this.dgvItemCode.CurrentRow.Cells[5].Value.ToString();
                    this.dgvSale.CurrentRow.Cells[6].Value = "0";
                    this.dgvSale.CurrentRow.Cells[7].Value = "0";
                    this.dgvSale.CurrentRow.Cells[8].Value = "0";
                    this.dgvSale.CurrentRow.Cells[3].Value = "1";
                    this.dgvSale.CurrentRow.Cells[3].Selected = true;
                    this.editindex = -1;
                }
                else
                {
                    this.dgvSale.Rows.Add();
                    int count = this.dgvSale.Rows.Count;
                    count--;
                    this.dgvSale.Rows[count].Cells[0].Value = count + 1;
                    this.dgvSale.Rows[count].Cells[1].Value = this.dgvItemCode.CurrentRow.Cells[1].Value.ToString();
                    this.dgvSale.Rows[count].Cells[2].Value = this.dgvItemCode.CurrentRow.Cells[3].Value.ToString();
                    this.dgvSale.Rows[count].Cells[4].Value = this.dgvItemCode.CurrentRow.Cells[4].Value.ToString();
                    this.dgvSale.Rows[count].Cells[5].Value = this.dgvItemCode.CurrentRow.Cells[5].Value.ToString();
                    this.dgvSale.Rows[count].Cells[6].Value = "0";
                    this.dgvSale.Rows[count].Cells[7].Value = "0";
                    this.dgvSale.Rows[count].Cells[8].Value = "0";
                    this.dgvSale.Rows[count].Cells[3].Value = "1";
                    this.dgvSale.Rows[count].Cells[3].Selected = true;
                }
                this.txtItemCode.Text = "";
                this.dgvItemCode.Visible = false;
                this.dgvSale.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.dgvItemCode.Visible = false;
            }
        }
        private void dgvSale_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvSale.CurrentCell.ColumnIndex == 3)
            {
                string qtyCheck = Validation.isNumberField("Qty", this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[3].Value.ToString());
                if (qtyCheck != "")
                {
                    MessageBox.Show(qtyCheck);
                    this.dgvSale.CurrentRow.Cells[3].Value = 1;
                }
                else
                {
                    int totalqty = Convert.ToInt32(this.txtTqty.Text);
                    int qty = Convert.ToInt32(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[3].Value);
                    decimal price = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[4].Value);
                    decimal charges = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[5].Value);
                    decimal dis = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[7].Value);
                    decimal amount = (price + charges) * qty - dis;
                    this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[8].Value = amount.ToString();
                    this.dgvSale.CurrentCell = this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[4];
                    this.dgvSale.CurrentRow.Cells[6].Value = "0";
                    this.dgvSale.CurrentRow.Cells[7].Value = "0";
                    this.CalculateHeaderAmount();
                }
            }
            else if (this.dgvSale.CurrentCell.ColumnIndex == 4)
            {
                string qtyCheck = Validation.isNumberField("Price", this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[4].Value.ToString());
                if (qtyCheck != "")
                {
                    MessageBox.Show(qtyCheck);
                    this.dgvSale.CurrentRow.Cells[4].Value = 0;
                }
                else
                {
                    int qty = Convert.ToInt32(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[3].Value);
                    decimal price = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[4].Value);
                    decimal charges = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[5].Value);
                    decimal dis = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[7].Value);
                    decimal amount = (price + charges) * qty - dis;
                    amount = (price + charges) * qty;
                    this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[8].Value = amount.ToString();
                    this.dgvSale.CurrentCell = this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[5];
                    this.CalculateHeaderAmount();
                }
            }
            else if (this.dgvSale.CurrentCell.ColumnIndex == 5)
            {
                string qtyCheck = Validation.isNumberField("Charges", this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[5].Value.ToString());
                if (qtyCheck != "")
                {
                    MessageBox.Show(qtyCheck);
                    this.dgvSale.CurrentRow.Cells[5].Value = 0;
                }
                else
                {
                    int qty = Convert.ToInt32(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[3].Value);
                    decimal price = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[4].Value);
                    decimal charges = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[5].Value);
                    decimal dis = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[7].Value);
                    decimal amount = (price + charges) * qty - dis;
                    amount = (price + charges) * qty;
                    this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[8].Value = amount.ToString();
                    this.txtItemCode.Focus();
                    this.CalculateHeaderAmount();
                }
            }
            else if (this.dgvSale.CurrentCell.ColumnIndex == 6)
            {
                string qtyCheck = Validation.isNumberField("Item Discount %", this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[6].Value.ToString());
                if (qtyCheck != "")
                {
                    MessageBox.Show(qtyCheck);
                    this.dgvSale.CurrentRow.Cells[6].Value = 0;
                }
                else
                {
                    decimal dis = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[6].Value);
                    int qty = Convert.ToInt32(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[3].Value);
                    decimal price = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[4].Value);
                    decimal charges = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[5].Value);
                    decimal tamount = (price + charges) * qty;
                    this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[7].Value = Convert.ToInt32(tamount * (dis / 100m));
                    this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[8].Value = Convert.ToInt32(tamount - tamount * (dis / 100m));
                    this.txtItemCode.Focus();
                    this.CalculateHeaderAmount();
                }
            }
            else if (this.dgvSale.CurrentCell.ColumnIndex == 7)
            {
                string qtyCheck = Validation.isNumberField("Item Discount Amount", this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[7].Value.ToString());
                if (qtyCheck != "")
                {
                    MessageBox.Show(qtyCheck);
                    this.dgvSale.CurrentRow.Cells[7].Value = 0;
                }
                else
                {
                    decimal dis = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[7].Value);
                    int qty = Convert.ToInt32(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[3].Value);
                    decimal price = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[4].Value);
                    decimal charges = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[5].Value);
                    decimal tamount = (price + charges) * qty;
                    this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[6].Value = Convert.ToInt32(dis * 100m / tamount);
                    this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[8].Value = Convert.ToInt32(tamount - dis);
                    this.txtItemCode.Focus();
                    this.CalculateHeaderAmount();
                }
            }
            else if (this.dgvSale.CurrentCell.ColumnIndex == 8)
            {
                string qtyCheck = Validation.isNumberField("Total Amount", this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[8].Value.ToString());
                if (qtyCheck != "")
                {
                    MessageBox.Show(qtyCheck);
                    this.dgvSale.CurrentRow.Cells[8].Value = 0;
                }
                else
                {
                    this.CalculateHeaderAmount();
                    this.txtItemCode.Focus();
                    this.txtItemCode.Select();
                }
            }
        }
        private void dgvSale_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                this.editindex = this.dgvSale.CurrentRow.Index;
            }
        }
        private void dgvSale_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (this.dgvSale.CurrentCell.ColumnIndex == 1)
                {
                    List<BOLStock> lstStk = new List<BOLStock>();
                    lstStk = this.dalstock.SelectStock("", 0, this.dgvSale.CurrentRow.Cells[1].Value.ToString(), 0);
                    if (lstStk.Count > 0)
                    {
                        this.dgvSale.CurrentRow.Cells[2].Value = lstStk[0].NameMM;
                        this.dgvSale.CurrentRow.Cells[4].Value = lstStk[0].Price;
                        this.dgvSale.CurrentRow.Cells[5].Value = lstStk[0].Charges;
                        this.dgvSale.CurrentRow.Cells[6].Value = "0";
                        this.dgvSale.CurrentRow.Cells[7].Value = "0";
                        this.dgvSale.CurrentRow.Cells[8].Value = "0";
                        this.dgvSale.CurrentRow.Cells[3].Value = "1";
                        this.dgvSale.CurrentCell = this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[3];
                    }
                }
                if (this.dgvSale.CurrentCell.ColumnIndex == 3)
                {
                    int totalqty = Convert.ToInt32(this.txtTqty.Text);
                    int qty = Convert.ToInt32(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[3].Value);
                    decimal price = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[4].Value);
                    decimal charges = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[5].Value);
                    decimal dis = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[7].Value);
                    decimal amount = (price + charges) * qty - dis;
                    this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[8].Value = amount.ToString();
                    this.dgvSale.CurrentCell = this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[4];
                    this.dgvSale.CurrentRow.Cells[6].Value = "0";
                    this.dgvSale.CurrentRow.Cells[7].Value = "0";
                    this.CalculateHeaderAmount();
                }
                else if (this.dgvSale.CurrentCell.ColumnIndex == 4)
                {
                    int qty = Convert.ToInt32(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[3].Value);
                    decimal price = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[4].Value);
                    decimal charges = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[5].Value);
                    decimal dis = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[7].Value);
                    decimal amount = (price + charges) * qty - dis;
                    amount = (price + charges) * qty;
                    this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[8].Value = amount.ToString();
                    this.dgvSale.CurrentCell = this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[5];
                    this.CalculateHeaderAmount();
                }
                else if (this.dgvSale.CurrentCell.ColumnIndex == 5)
                {
                    int qty = Convert.ToInt32(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[3].Value);
                    decimal price = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[4].Value);
                    decimal charges = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[5].Value);
                    decimal dis = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[7].Value);
                    decimal amount = (price + charges) * qty - dis;
                    amount = (price + charges) * qty;
                    this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[8].Value = amount.ToString();
                    this.txtItemCode.Focus();
                    this.CalculateHeaderAmount();
                }
                else if (this.dgvSale.CurrentCell.ColumnIndex == 6)
                {
                    decimal dis = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[6].Value);
                    int qty = Convert.ToInt32(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[3].Value);
                    decimal price = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[4].Value);
                    decimal charges = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[5].Value);
                    decimal tamount = (price + charges) * qty;
                    this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[7].Value = Convert.ToInt32(tamount * (dis / 100m));
                    this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[8].Value = Convert.ToInt32(tamount - tamount * (dis / 100m));
                    this.txtItemCode.Focus();
                    this.CalculateHeaderAmount();
                }
                else if (this.dgvSale.CurrentCell.ColumnIndex == 7)
                {
                    decimal dis = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[7].Value);
                    int qty = Convert.ToInt32(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[3].Value);
                    decimal price = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[4].Value);
                    decimal charges = Convert.ToDecimal(this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[5].Value);
                    decimal tamount = (price + charges) * qty;
                    this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[6].Value = Convert.ToInt32(dis * 100m / tamount);
                    this.dgvSale.Rows[this.dgvSale.CurrentCell.RowIndex].Cells[8].Value = Convert.ToInt32(tamount - dis);
                    this.txtItemCode.Focus();
                    this.CalculateHeaderAmount();
                }
                else if (this.dgvSale.CurrentCell.ColumnIndex == 8)
                {
                    this.CalculateHeaderAmount();
                    this.txtItemCode.Focus();
                    this.txtItemCode.Select();
                }
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (this.dgvSale.Rows.Count > 0)
                    {
                        foreach (object obj in this.dgvSale.SelectedRows)
                        {
                            DataGridViewRow row = (DataGridViewRow)obj;
                            if (row.Cells[9].Value != "" && row.Cells[9].Value != "0" && row.Cells[9].Value != null)
                            {
                                this.SDetailID.Add(int.Parse(row.Cells[9].Value.ToString()));
                            }
                            this.dgvSale.Rows.Remove(row);
                        }
                        this.CalculateHeaderAmount();
                    }
                }
            }
            else if (e.KeyCode == Keys.F2)
            {
                this.txtTdis.Focus();
            }
            else if (e.KeyCode == Keys.F5)
            {
                this.btnSave_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F6)
            {
                this.btnPreviewVoucher_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F8)
            {
                this.btnNew_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F9)
            {
                this.btnClose_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F10)
            {
                if (this.lblCusID.Text != "" || this.dgvSale.Rows.Count > 0)
                {
                    this.btnSave_Click(sender, e);
                    this.PrintVoucher(this.voucher);
                    this.btnNew_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Please Check Your Input!");
                }
            }
        }
        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (this.txtItemCode.Text == "")
                {
                    List<BOLStock> lstStk = new List<BOLStock>();
                    lstStk = this.dalstock.SearchStock(this.txtItemCode.Text);
                    this.dgvItemCode.Rows.Clear();
                    if (lstStk.Count > 1)
                    {
                        for (int i = 0; i < lstStk.Count; i++)
                        {
                            this.dgvItemCode.Rows.Add(new object[]
							{
								lstStk[i].Id,
								lstStk[i].ItemCode,
								lstStk[i].NameMM,
								lstStk[i].NameMM,
								lstStk[i].Price,
								lstStk[i].Charges
							});
                        }
                        this.dgvItemCode.Visible = true;
                        this.txtItemCode.Focus();
                    }
                    else if (lstStk.Count == 1)
                    {
                        this.dgvSale.Rows.Add();
                        int count = this.dgvSale.Rows.Count;
                        count--;
                        this.txtItemCode.Text = "";
                        this.dgvItemCode.Visible = false;
                        this.dgvSale.Focus();
                        this.dgvSale.Rows[count].Cells[0].Value = count + 1;
                        this.dgvSale.Rows[count].Cells[3].Value = "1";
                        this.dgvSale.Rows[count].Cells[3].Selected = true;
                        this.dgvSale.Rows[count].Cells[1].Value = lstStk[0].ItemCode;
                        this.dgvSale.Rows[count].Cells[2].Value = lstStk[0].NameMM;
                        this.dgvSale.Rows[count].Cells[5].Value = lstStk[0].Charges;
                        this.dgvSale.Rows[count].Cells[4].Value = lstStk[0].Price;
                        this.dgvItemCode.Visible = false;
                        this.txtItemCode.BackColor = Color.Azure;
                        this.dgvSale.Focus();
                        this.dgvSale.CurrentRow.Cells[3].Selected = true;
                    }
                    this.dgvItemCode.Focus();
                    this.txtItemCode.BackColor = Color.Azure;
                    this.dgvItemCode.RowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue;
                }
                else
                {
                    List<BOLStock> lstStk = new List<BOLStock>();
                    lstStk = this.dalstock.SearchStock(this.txtItemCode.Text);
                    this.dgvItemCode.Rows.Clear();
                    if (lstStk.Count > 1)
                    {
                        for (int i = 0; i < lstStk.Count; i++)
                        {
                            this.dgvItemCode.Rows.Add(new object[]
							{
								lstStk[i].Id,
								lstStk[i].ItemCode,
								lstStk[i].NameMM,
								lstStk[i].NameMM,
								lstStk[i].Price,
								lstStk[i].Charges
							});
                        }
                        this.dgvItemCode.Visible = true;
                        this.txtItemCode.Focus();
                    }
                    else if (lstStk.Count == 1)
                    {
                        this.dgvSale.Rows.Add();
                        int count = this.dgvSale.Rows.Count;
                        count--;
                        this.txtItemCode.Text = "";
                        this.dgvItemCode.Visible = false;
                        this.dgvSale.Focus();
                        this.dgvSale.Rows[count].Cells[0].Value = count + 1;
                        this.dgvSale.Rows[count].Cells[3].Value = "1";
                        this.dgvSale.Rows[count].Cells[3].Selected = true;
                        this.dgvSale.Rows[count].Cells[1].Value = lstStk[0].ItemCode;
                        this.dgvSale.Rows[count].Cells[2].Value = lstStk[0].NameMM;
                        this.dgvSale.Rows[count].Cells[5].Value = lstStk[0].Charges;
                        this.dgvSale.Rows[count].Cells[4].Value = lstStk[0].Price;
                        this.dgvItemCode.Visible = false;
                        this.txtItemCode.BackColor = Color.Azure;
                        this.dgvSale.Focus();
                        this.dgvSale.CurrentRow.Cells[3].Selected = true;
                    }
                    this.dgvItemCode.Focus();
                    this.txtItemCode.BackColor = Color.Azure;
                    this.dgvItemCode.RowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue;
                }
            }
            else if (e.KeyCode == Keys.F2)
            {
                this.txtTdis.Focus();
            }
            else if (e.KeyCode == Keys.F5)
            {
                this.btnSave_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F6)
            {
                this.btnPreviewVoucher_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F8)
            {
                this.btnNew_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F9)
            {
                this.btnClose_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F10)
            {
                if (this.lblCusID.Text != "" || this.dgvSale.Rows.Count > 0)
                {
                    this.btnSave_Click(sender, e);
                    this.PrintVoucher(this.voucher);
                    this.btnNew_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Please Check Your Input!");
                }
            }
        }
        private void CalculateHeaderAmount()
        {
            decimal detaildis = 0m;
            decimal totalamount = 0m;
            int qty = 0;
            for (int i = 0; i < this.dgvSale.Rows.Count; i++)
            {
                detaildis += decimal.Parse(this.dgvSale.Rows[i].Cells[7].Value.ToString());
                totalamount += decimal.Parse(this.dgvSale.Rows[i].Cells[8].Value.ToString());
                qty += int.Parse(this.dgvSale.Rows[i].Cells[3].Value.ToString());
            }
            this.txtTqty.Text = qty.ToString();
            this.txtTddis.Text = detaildis.ToString();
            this.txtTotalAmount.Text = totalamount.ToString();
        }
        private void txtTdis_TextChanged(object sender, EventArgs e)
        {
            string var = this.txtTdis.Text;
            if (this.checkEnterKey(var))
            {
                decimal discount = Convert.ToDecimal((this.txtTdis.Text == "") ? "0" : this.txtTdis.Text);
                decimal amount = Convert.ToDecimal((this.txtTotalAmount.Text == "") ? "0" : this.txtTotalAmount.Text);
                this.txtBalance.Text = (amount - discount).ToString();
            }
            else
            {
                MessageBox.Show("Please Enter Number Only!");
                this.txtTdis.Text = "0";
                this.txtTdis.Select();
            }
        }
        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            string var = this.txtPaidAmount.Text;
            if (this.checkEnterKey(var))
            {
                decimal balance = Convert.ToDecimal((this.txtBalance.Text == "") ? "0" : this.txtBalance.Text);
                decimal paidamount = Convert.ToDecimal((this.txtPaidAmount.Text == "") ? "0" : this.txtPaidAmount.Text);
                this.txtRefundAmount.Text = (paidamount - balance).ToString();
            }
            else
            {
                MessageBox.Show("Please Enter Number Only!");
                this.txtPaidAmount.Text = "0";
            }
        }
        private void txtTdis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                this.txtPaidAmount.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                this.txtTdis.Focus();
            }
            else if (e.KeyCode == Keys.F5)
            {
                this.btnSave_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F6)
            {
                this.btnPreviewVoucher_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F8)
            {
                this.btnNew_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F9)
            {
                this.btnClose_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F10)
            {
                if (this.lblCusID.Text != "" || this.dgvSale.Rows.Count > 0)
                {
                    this.btnSave_Click(sender, e);
                    this.PrintVoucher(this.voucher);
                    this.btnNew_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Please Check Your Input!");
                }
            }
        }
        private void txtPaidAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (this.lblCusID.Text != "" || this.dgvSale.Rows.Count > 0)
                {
                    this.btnSave_Click(sender, e);
                    this.PrintVoucher(this.voucher);
                    this.btnNew_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Please Check Your Input!");
                }
            }
            else if (e.KeyCode == Keys.F2)
            {
                this.txtTdis.Focus();
            }
            else if (e.KeyCode == Keys.F5)
            {
                this.btnSave_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F6)
            {
                this.btnPreviewVoucher_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F8)
            {
                this.btnNew_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F9)
            {
                this.btnClose_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F10)
            {
                if (this.lblCusID.Text != "" || this.dgvSale.Rows.Count > 0)
                {
                    this.btnSave_Click(sender, e);
                    this.PrintVoucher(this.voucher);
                    this.btnNew_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Please Check Your Input!");
                }
            }
        }
        private void btnPaid_Click(object sender, EventArgs e)
        {
            if (this.lblCusID.Text != "" || this.dgvSale.Rows.Count > 0)
            {
                this.btnSave_Click(sender, e);
                this.PrintVoucher(this.voucher);
                this.btnNew_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Please Check Your Input!");
            }
        }
        private void txtItemCode_Enter(object sender, EventArgs e)
        {
            if (this.txtItemCode.Text != "")
            {
            }
            this.txtItemCode.BackColor = Color.SkyBlue;
            this.dgvItemCode.RowsDefaultCellStyle.SelectionBackColor = Color.Azure;
        }
        private void txtItemCode_Leave(object sender, EventArgs e)
        {
            this.txtItemCode.BackColor = Color.Azure;
        }
        private void lblCusID_Click(object sender, EventArgs e)
        {
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void txtTotalAmount_TextChanged(object sender, EventArgs e)
        {
            this.txtBalance.Text = this.txtTotalAmount.Text;
        }
        private void BindPOS(long voucherno)
        {
            List<BOLSale> bolsale = new List<BOLSale>();
            bolsale = this.dalsale.GetSaleBySaleID(voucherno, 0, "");
            this.dgvSale.Rows.Clear();
            this.lblSaleID.Text = bolsale[0].SaleID.ToString();
            this.lblCusID.Text = bolsale[0].CustomerID;
            this.txtCustomerName.Text = bolsale[0].CustomerName;
            this.txtVoucherNo.Text = bolsale[0].VoucherNo;
            this.txtVoucherNo.Text = bolsale[0].SystemVoucherNo;
            this.dtpSaleDate.Value = bolsale[0].SaleDate;
            this.lblCusID.Text = bolsale[0].CustomerID;
            this.cboPaymentType.Text = bolsale[0].PaymentType;
            this.cbocurrency.SelectedValue = bolsale[0].CurrencyID;
            this.txtTotalAmount.Text = bolsale[0].TotalAmt.ToString();
            this.txtTddis.Text = bolsale[0].TotalitemDiscount.ToString();
            this.txtTdis.Text = bolsale[0].Discount.ToString();
            this.txtBalance.Text = bolsale[0].GrandTotal.ToString();
            this.txtPaidAmount.Text = bolsale[0].PaidAmount.ToString();
            this.txtRefundAmount.Text = bolsale[0].RefundAmount.ToString();
            this.txtExchangeRate.Text = bolsale[0].ExchangeRate.ToString();
            this.txtDrawingTimes.Text = bolsale[0].DrawingTimes.ToString();
            List<BolLocation> LstLocation = new List<BolLocation>();
            LstLocation = this.dalLocation.SelectAllLocation();
            this.cboLocation.DisplayMember = "Location";
            this.cboLocation.ValueMember = "ID";
            this.cboLocation.DataSource = LstLocation;
            for (int i = 0; i < LstLocation.Count; i++)
            {
                if (LstLocation[i].ID == bolsale[0].LocationID)
                {
                    this.cboLocation.SelectedValue = LstLocation[i].ID;
                }
            }
            BOLCounter lstcounter = new BOLCounter();
            lstcounter = this.dalcounter.GetCounter(bolsale[0].CounterID);
            this.txtCounter.Text = lstcounter.Name.ToString();
            this.txtRemark.Text = bolsale[0].Remark;
            MoeYanFunctions.MoeYanPOS_Helper.counterCode = lstcounter.Code.ToString();
            MoeYanFunctions.MoeYanPOS_Helper.counterName = lstcounter.Name.ToString();
            List<BOLSale> lstsaledetail = new List<BOLSale>();
            lstsaledetail = this.dalsale.GetSaleDetailList(voucherno, 0);
            this.dgvSale.Rows.Clear();
            if (lstsaledetail.Count > 0)
            {
                for (int i = 0; i < lstsaledetail.Count; i++)
                {
                    this.dgvSale.Rows.Add();
                    this.dgvSale.Rows[i].Cells[0].Value = i + 1;
                    this.dgvSale.Rows[i].Cells[1].Value = lstsaledetail[i].ItemCode.ToString();
                    this.dgvSale.Rows[i].Cells[2].Value = lstsaledetail[i].Description.ToString();
                    this.dgvSale.Rows[i].Cells[3].Value = lstsaledetail[i].Qty.ToString();
                    this.dgvSale.Rows[i].Cells[4].Value = lstsaledetail[i].SalePrice.ToString();
                    this.dgvSale.Rows[i].Cells[5].Value = lstsaledetail[i].Charge.ToString();
                    this.dgvSale.Rows[i].Cells[8].Value = lstsaledetail[i].Total.ToString();
                    this.dgvSale.Rows[i].Cells[7].Value = lstsaledetail[i].ItemDiscountPercent.ToString();
                    this.dgvSale.Rows[i].Cells[6].Value = lstsaledetail[i].ItemDiscount.ToString();
                    this.dgvSale.Rows[i].Cells[9].Value = lstsaledetail[i].SaleDetailID.ToString();
                }
            }
        }
        private void frmPOS_FormClosed(object sender, FormClosedEventArgs e)
        {
            BOLCounter blocounter = new BOLCounter();
            blocounter.Code = MoeYanFunctions.MoeYanPOS_Helper.counterCode;
            blocounter.Name = MoeYanFunctions.MoeYanPOS_Helper.counterName;
            blocounter.IsthisLocation = false;
            blocounter.IsDelete = false;
            this.dalcounter.updateCounter(blocounter);
        }
        private void btnPreviewVoucher_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lblCusID.Text != "" || this.dgvSale.Rows.Count > 0)
                {
                    this.btnSave_Click(sender, e);
                    ReportDocument l_Report = new ReportDocument();
                    ReportDocument l_Report2 = new ReportDocument();
                    DataSet ds = new DataSet();
                    ds = this.dalsale.SelectSaleVoucher(this.voucher, 0, this.txtVoucherNo.Text);
                    List<BOLSystem> lstsystem = new List<BOLSystem>();
                    lstsystem = this.dalSystem.ShowAllSystem();
                    ds.WriteXmlSchema(Application.StartupPath + "\\DataSets\\DS_SaleCredit.xsd");
                    ds.Tables[0].TableName = "DS_SaleCredit";
                    if (ds.Tables[0].Rows[0]["ShopName"].ToString() == "Star Moeyan Lottery Enterprise")
                    {
                        l_Report.Load(Application.StartupPath + "\\Report\\RptSaleCredit.rpt");
                        l_Report2.Load(Application.StartupPath + "\\Report\\SaleChargesReport.rpt");
                    }
                    else
                    { 
                        l_Report.Load(Application.StartupPath + "\\Report\\RptSaleCredit(Customer).rpt");
                    }
                    //l_Report2.Load(Application.StartupPath + "\\Report\\SaleChargesReport.rpt");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        l_Report.SetDataSource(ds);
                        l_Report.SetDatabaseLogon("sa", "sa123");
                        if (ds.Tables[0].Rows[0]["ShopName"].ToString() == "Star Moeyan Lottery Enterprise")
                        {
                            l_Report2.SetDataSource(ds);
                            l_Report2.SetDatabaseLogon("sa", "sa123");
                        }
                        if (lstsystem.Count > 0)
                        {
                            if (lstsystem[0].Printerslip != "")
                            {
                                if (ds.Tables[0].Rows[0]["ShopName"].ToString() == "Star Moeyan Lottery Enterprise")
                                {
                                    PageMargins margin = l_Report.PrintOptions.PageMargins;
                                    //l_Report.PrintOptions.PaperSize = 5;
                                    frmReport frmReport = new frmReport();
                                    frmReport.rptViewer.ReportSource = l_Report;
                                    frmReport.rptViewer.RefreshReport();
                                    frmReport.Show();
                                    base.UseWaitCursor = false;
                                    frmReport frmReport2 = new frmReport();
                                    frmReport2.rptViewer.ReportSource = l_Report2;
                                    frmReport2.rptViewer.RefreshReport();
                                    frmReport2.Show();
                                    base.UseWaitCursor = false;
                                }
                                else 
                                {
                                    PageMargins margin = l_Report.PrintOptions.PageMargins;
                                    //l_Report.PrintOptions.PaperSize = 5;
                                    frmReport frmReport = new frmReport();
                                    frmReport.rptViewer.ReportSource = l_Report;
                                    frmReport.rptViewer.RefreshReport();
                                    frmReport.Show();
                                    base.UseWaitCursor = false;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please put printer name at System Setting.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please Check Your Input!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lblCusID.Text != "" && this.dgvSale.Rows.Count > 0)
                {
                    if (this.lblSaleID.Text == "")
                    {
                        BOLSale bolsale = new BOLSale();
                        long syskey = MoeYanFunctions.GenerateSysKey();
                        this.voucher = syskey;
                        bolsale.SaleID = syskey;
                        bolsale.TranSaleID = long.Parse(this.daltrans.GetTransitionID("Sale").ToString());
                        bolsale.VoucherNo = this.txtVoucherNo.Text;
                        bolsale.SystemVoucherNo = this.txtVoucherNo.Text;
                        bolsale.SaleDate = DateTime.Now;
                        bolsale.CustomerID = this.lblCusID.Text;
                        bolsale.UserID = frmMain.UserID;
                        bolsale.PaymentType = this.cboPaymentType.Text;
                        bolsale.CurrencyID = int.Parse(this.cbocurrency.SelectedValue.ToString());
                        bolsale.DayLimit = 0;
                        bolsale.Advance = 0m;
                        bolsale.TotalAmt = decimal.Parse(this.txtTotalAmount.Text);
                        bolsale.TotalitemDiscount = decimal.Parse(this.txtTddis.Text);
                        bolsale.Discount = decimal.Parse(this.txtTdis.Text);
                        bolsale.GrandTotal = decimal.Parse(this.txtBalance.Text);
                        bolsale.PaidAmount = decimal.Parse(this.txtPaidAmount.Text);
                        bolsale.RefundAmount = decimal.Parse(this.txtRefundAmount.Text);
                        bolsale.OriginalUserID = frmMain.UserID;
                        bolsale.EditUserID = frmMain.UserID;
                        bolsale.EditSaleDate = DateTime.Now;
                        bolsale.LotteryDate = DateTime.Now;
                        bolsale.ExchangeRate = int.Parse(this.txtExchangeRate.Text);
                        bolsale.LotteryNo = "";
                        bolsale.DrawingTimes = this.txtDrawingTimes.Text;
                        bolsale.LocationID = (long)int.Parse(this.cboLocation.SelectedValue.ToString());
                        bolsale.CounterID = MoeYanFunctions.MoeYanPOS_Helper.counterCode;
                        bolsale.Remark = this.txtRemark.Text;
                        this.isSaved = this.dalsale.SaveSaleData(bolsale);
                        if (this.isSaved == 1)
                        {
                            for (int i = 0; i < this.dgvSale.Rows.Count; i++)
                            {
                                if (this.dgvSale.Rows.Count > 0)
                                {
                                    if (this.dgvSale.Rows[i].Cells["colSaleDetailID"].Value == null)
                                    {
                                        BOLSale bolsaledetail = new BOLSale();
                                        bolsaledetail.SaleID = syskey;
                                        this.lblSaleID.Text = bolsaledetail.SaleID.ToString();
                                        bolsaledetail.No = int.Parse(this.dgvSale.Rows[i].Cells["colNo"].Value.ToString());
                                        bolsaledetail.ItemCode = this.dgvSale.Rows[i].Cells["colItemCode"].Value.ToString();
                                        int isExist = this.dalstock.CheckStock(bolsaledetail.ItemCode);
                                        if (isExist == 0 & bolsaledetail.ItemCode != "")
                                        {
                                            MessageBox.Show(" This stock code doesn't exist.");
                                            return;
                                        }
                                        bolsaledetail.Description = this.dgvSale.Rows[i].Cells["colDescription"].Value.ToString();
                                        bolsaledetail.Qty = int.Parse(this.dgvSale.Rows[i].Cells["colQty"].Value.ToString());
                                        bolsaledetail.SalePrice = decimal.Parse(this.dgvSale.Rows[i].Cells["colSalePrice"].Value.ToString());
                                        bolsaledetail.Total = decimal.Parse(this.dgvSale.Rows[i].Cells["colTotal"].Value.ToString());
                                        bolsaledetail.Charge = decimal.Parse(this.dgvSale.Rows[i].Cells["colCharges"].Value.ToString());
                                        bolsaledetail.FOC = false;
                                        bolsaledetail.ItemDiscount = decimal.Parse(this.dgvSale.Rows[i].Cells["colItemDiscount"].Value.ToString());
                                        bolsaledetail.ItemDiscountPercent = int.Parse(this.dgvSale.Rows[i].Cells["colItemDiscountPercent"].Value.ToString());
                                        this.dalsaledetail.SaveSaleDetailData(bolsaledetail);
                                    }
                                }
                            }
                        }
                        MessageBox.Show("Sale data is successfully saved.");
                        this.GenerateTextFile();
                        this.btnNew_Click(sender, e);
                    }
                    else
                    {
                        BOLSale bolsale = new BOLSale();
                        bolsale.TranSaleID = long.Parse(this.daltrans.GetTransitionID("Sale").ToString());
                        bolsale.SaleID = Convert.ToInt64(this.lblSaleID.Text);
                        this.voucher = Convert.ToInt64(this.lblSaleID.Text);
                        bolsale.CustomerID = this.lblCusID.Text;
                        bolsale.VoucherNo = this.txtVoucherNo.Text;
                        bolsale.SystemVoucherNo = this.txtVoucherNo.Text;
                        bolsale.SaleDate = this.dtpSaleDate.Value;
                        bolsale.CustomerID = this.lblCusID.Text;
                        bolsale.PaymentType = this.cboPaymentType.Text;
                        bolsale.CurrencyID = int.Parse(this.cbocurrency.SelectedValue.ToString());
                        bolsale.DayLimit = 0;
                        bolsale.Advance = 0m;
                        bolsale.TotalAmt = decimal.Parse(this.txtTotalAmount.Text);
                        bolsale.TotalitemDiscount = decimal.Parse(this.txtTddis.Text);
                        bolsale.Discount = decimal.Parse(this.txtTdis.Text);
                        bolsale.GrandTotal = decimal.Parse(this.txtBalance.Text);
                        bolsale.PaidAmount = decimal.Parse(this.txtPaidAmount.Text);
                        bolsale.RefundAmount = decimal.Parse(this.txtRefundAmount.Text);
                        bolsale.OriginalUserID = frmMain.UserID;
                        bolsale.EditUserID = frmMain.UserID;
                        bolsale.EditSaleDate = DateTime.Now;
                        bolsale.LotteryDate = this.dtpSaleDate.Value;
                        bolsale.ExchangeRate = int.Parse(this.txtExchangeRate.Text);
                        bolsale.LotteryNo = "";
                        bolsale.DrawingTimes = this.txtDrawingTimes.Text;
                        bolsale.LocationID = (long)int.Parse(this.cboLocation.SelectedValue.ToString());
                        bolsale.CounterID = MoeYanFunctions.MoeYanPOS_Helper.counterCode;
                        bolsale.Remark = this.txtRemark.Text;
                        this.isSaved = this.dalsale.UpdateSaleBySaleID(bolsale);
                        if (this.isSaved == 1)
                        {
                            if (this.SDetailID.Count > 0)
                            {
                                for (int j = 0; j < this.SDetailID.Count; j++)
                                {
                                    this.dalsaledetail.DeleteSaleDetail(long.Parse(this.SDetailID[j].ToString()), this.lblSaleID.Text);
                                }
                            }
                            for (int i = 0; i < this.dgvSale.Rows.Count; i++)
                            {
                                if (this.dgvSale.Rows.Count > 0)
                                {
                                    if (this.dgvSale.Rows[i].Cells["colSaleDetailID"].Value == null)
                                    {
                                        BOLSale bolsaledetail = new BOLSale();
                                        bolsaledetail.SaleID = Convert.ToInt64(this.lblSaleID.Text);
                                        this.lblSaleID.Text = bolsaledetail.SaleID.ToString();
                                        bolsaledetail.No = int.Parse(this.dgvSale.Rows[i].Cells["colNo"].Value.ToString());
                                        bolsaledetail.ItemCode = this.dgvSale.Rows[i].Cells["colItemCode"].Value.ToString();
                                        int isExist = this.dalstock.CheckStock(bolsaledetail.ItemCode);
                                        if (isExist == 0 & bolsaledetail.ItemCode != "")
                                        {
                                            MessageBox.Show(" This stock code doesn't exist.");
                                            return;
                                        }
                                        bolsaledetail.Description = this.dgvSale.Rows[i].Cells["colDescription"].Value.ToString();
                                        bolsaledetail.Qty = int.Parse(this.dgvSale.Rows[i].Cells["colQty"].Value.ToString());
                                        bolsaledetail.SalePrice = decimal.Parse(this.dgvSale.Rows[i].Cells["colSalePrice"].Value.ToString());
                                        bolsaledetail.Total = decimal.Parse(this.dgvSale.Rows[i].Cells["colTotal"].Value.ToString());
                                        bolsaledetail.Charge = decimal.Parse(this.dgvSale.Rows[i].Cells["colCharges"].Value.ToString());
                                        bolsaledetail.FOC = false;
                                        bolsaledetail.ItemDiscount = decimal.Parse(this.dgvSale.Rows[i].Cells["colItemDiscount"].Value.ToString());
                                        bolsaledetail.ItemDiscountPercent = int.Parse(this.dgvSale.Rows[i].Cells["colItemDiscountPercent"].Value.ToString());
                                        this.dalsaledetail.SaveSaleDetailData(bolsaledetail);
                                    }
                                    else
                                    {
                                        BOLSale bolsaledetail = new BOLSale();
                                        bolsaledetail.SaleID = Convert.ToInt64(this.lblSaleID.Text);
                                        this.lblSaleID.Text = bolsaledetail.SaleID.ToString();
                                        bolsaledetail.No = int.Parse(this.dgvSale.Rows[i].Cells["colNo"].Value.ToString());
                                        bolsaledetail.SaleDetailID = (long)int.Parse(this.dgvSale.Rows[i].Cells["colSaleDetailID"].Value.ToString());
                                        bolsaledetail.ItemCode = this.dgvSale.Rows[i].Cells["colItemCode"].Value.ToString();
                                        int isExist = this.dalstock.CheckStock(bolsaledetail.ItemCode);
                                        if (isExist == 0 & bolsaledetail.ItemCode != "")
                                        {
                                            MessageBox.Show(" This stock code doesn't exist.");
                                            return;
                                        }
                                        bolsaledetail.Description = this.dgvSale.Rows[i].Cells["colDescription"].Value.ToString();
                                        bolsaledetail.Qty = int.Parse(this.dgvSale.Rows[i].Cells["colQty"].Value.ToString());
                                        bolsaledetail.SalePrice = decimal.Parse(this.dgvSale.Rows[i].Cells["colSalePrice"].Value.ToString());
                                        bolsaledetail.Total = decimal.Parse(this.dgvSale.Rows[i].Cells["colTotal"].Value.ToString());
                                        bolsaledetail.Charge = decimal.Parse(this.dgvSale.Rows[i].Cells["colCharges"].Value.ToString());
                                        bolsaledetail.FOC = false;
                                        bolsaledetail.ItemDiscount = decimal.Parse(this.dgvSale.Rows[i].Cells["colItemDiscount"].Value.ToString());
                                        bolsaledetail.ItemDiscountPercent = int.Parse(this.dgvSale.Rows[i].Cells["colItemDiscountPercent"].Value.ToString());
                                        this.dalsaledetail.UpdateSaleDetailData(bolsaledetail);
                                    }
                                }
                            }
                        }
                        MessageBox.Show("Sale data is successfully updated.");
                        this.GenerateTextFile();
                        this.btnNew_Click(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("Please Check Your Input!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void GenerateTextFile()
        {
            string folder = string.Concat(new string[]
			{
				Application.StartupPath,
				"\\BackupFile\\",
				this.cboLocation.Text,
				" - ",
				this.txtCounter.Text,
				" - ",
				DateTime.Now.ToString("MM-dd-yyyy")
			});
            string filepath = folder + "\\" + this.txtVoucherNo.Text + ".txt";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            using (StreamWriter file = new StreamWriter(filepath))
            {
                file.WriteLine(string.Concat(new object[]
				{
					this.txtVoucherNo.Text,
					" ,",
					this.dtpSaleDate.Value.ToShortDateString(),
					" ,",
					this.txtCustomer.Text,
					" ,",
					frmMain.UserID,
					" ,",
					this.cboPaymentType.Text,
					" ,",
					this.cbocurrency.Text,
					" ,",
					this.txtTqty.Text,
					" ,",
					this.txtTotalAmount.Text,
					" ,",
					this.txtTddis.Text,
					" ,",
					this.txtTdis.Text,
					" ,",
					this.txtPaidAmount.Text,
					" ,",
					this.txtRefundAmount.Text,
					" ,",
					this.txtBalance.Text,
					" ,",
					this.cboLocation.Text,
					" ,",
					MoeYanFunctions.MoeYanPOS_Helper.counterCode,
					" ,",
					this.txtDrawingTimes.Text,
					" ,",
					this.txtRemark.Text
				}));
                for (int i = 0; i < this.dgvSale.Rows.Count; i++)
                {
                    file.WriteLine(string.Concat(new string[]
					{
						this.dgvSale.Rows[i].Cells[0].Value.ToString(),
						" ,",
						this.dgvSale.Rows[i].Cells[1].Value.ToString(),
						" ,",
						this.dgvSale.Rows[i].Cells[2].Value.ToString(),
						" ,",
						this.dgvSale.Rows[i].Cells[3].Value.ToString(),
						" ,",
						this.dgvSale.Rows[i].Cells[4].Value.ToString(),
						" ,",
						this.dgvSale.Rows[i].Cells[5].Value.ToString(),
						" ,",
						this.dgvSale.Rows[i].Cells[6].Value.ToString(),
						" ,",
						this.dgvSale.Rows[i].Cells[7].Value.ToString(),
						" ,",
						this.dgvSale.Rows[i].Cells[8].Value.ToString()
					}));
                }
            }
        }
        private void PrintVoucher(long voucher)
        {
            try
            {
                ReportDocument l_Report = new ReportDocument();
                ReportDocument l_Report2 = new ReportDocument();
                DataSet ds = new DataSet();
                ds = this.dalsale.SelectSaleVoucher(voucher, 0, this.txtVoucherNo.Text);
                List<BOLSystem> lstsystem = new List<BOLSystem>();
                lstsystem = this.dalSystem.ShowAllSystem();
                ds.WriteXmlSchema(Application.StartupPath + "\\DataSets\\DS_SaleCredit.xsd");
                ds.Tables[0].TableName = "DS_SaleCredit";
                if (ds.Tables[0].Rows[0]["ShopName"].ToString() == "Star Moeyan Lottery Enterprise")
                {
                    l_Report.Load(Application.StartupPath + "\\Report\\RptSaleCredit.rpt");
                    l_Report2.Load(Application.StartupPath + "\\Report\\SaleChargesReport.rpt");
                }
                else
                {
                    l_Report.Load(Application.StartupPath + "\\Report\\RptSaleCredit(Customer).rpt");
                }
                //l_Report2.Load(Application.StartupPath + "\\Report\\SaleChargesReport.rpt");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    l_Report.SetDataSource(ds);
                    l_Report.SetDatabaseLogon("sa", "sa123");
                    if (ds.Tables[0].Rows[0]["ShopName"].ToString() == "Star Moeyan Lottery Enterprise")
                    {
                        l_Report2.SetDataSource(ds);
                        l_Report2.SetDatabaseLogon("sa", "sa123");
                    }
                    if (lstsystem.Count > 0)
                    {
                        if (lstsystem[0].Printerslip != "")
                        {
                            if (ds.Tables[0].Rows[0]["ShopName"].ToString() == "Star Moeyan Lottery Enterprise")
                            {
                                PageMargins margin = l_Report.PrintOptions.PageMargins;
                                //l_Report.PrintOptions.PaperSize = 5;
                                l_Report.PrintOptions.PrinterName = lstsystem[0].Printervoucher;
                                l_Report.PrintToPrinter(3, false, 0, 0);
                                l_Report2.PrintOptions.PrinterName = lstsystem[0].Printerslip;
                                l_Report2.PrintToPrinter(1, false, 0, 0);
                            }
                            else 
                            {
                                PageMargins margin = l_Report.PrintOptions.PageMargins;
                                //l_Report.PrintOptions.PaperSize = 5;
                                l_Report.PrintOptions.PrinterName = lstsystem[0].Printervoucher;
                                l_Report.PrintToPrinter(2, false, 0, 0);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please put printer name at System Setting.");
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
            this.LoadForm();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }
        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void txtBalance_MouseClick(object sender, MouseEventArgs e)
        {
            this.txtBalance.Enabled = false;
        }
        private void txtBalance_MouseLeave(object sender, EventArgs e)
        {
            this.txtBalance.Enabled = true;
        }
        private void txtRefundAmount_MouseClick(object sender, MouseEventArgs e)
        {
            this.txtRefundAmount.Enabled = false;
        }
        private void txtRefundAmount_MouseLeave(object sender, EventArgs e)
        {
            this.txtRefundAmount.Enabled = true;
        }
        private bool checkEnterKey(string key)
        {
            int i;
            bool result;
            if (int.TryParse(key, out i) || key == Keys.Return.ToString() || key == Keys.Back.ToString() || key == "")
            {
                result = true;
            }
            else
            {
                MessageBox.Show("Pleas Enter Number Only!");
                result = false;
            }
            return result;
        }
        private void frmPOS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.txtTdis.Focus();
            }
            else if (e.KeyCode == Keys.F5)
            {
                this.btnSave_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F6)
            {
                this.btnPreviewVoucher_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F8)
            {
                this.btnNew_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F9)
            {
                this.btnClose_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F10)
            {
                if (this.lblCusID.Text != "" || this.dgvSale.Rows.Count > 0)
                {
                    this.btnSave_Click(sender, e);
                    this.PrintVoucher(this.voucher);
                    this.btnNew_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Please Check Your Input!");
                }
            }
        }
        private void txtTdis_Enter(object sender, EventArgs e)
        {
        }
        private void btnCustomer_Click(object sender, EventArgs e)
        {
            frmCustomer newform = new frmCustomer();
            newform.ShowDialog();
        }
    }
}
