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
using System.Drawing.Printing;

namespace MoeYanPOS
{
    public partial class frmSaleReturn : Form
    {
        DALCustomer dalcustomer = new DALCustomer();
        DALExchangeRate dalexchange = new DALExchangeRate();
        DALSaleReturn dalsalereturn = new DALSaleReturn();
        DALTransition daltran = new DALTransition();
        DALLocation dallocation = new DALLocation();
        BOLUser LstCheckPrintAndMsgBox = new BOLUser();
        DateTime SaleDate; DateTime LotteryDate;
        DALVoucherSetting dalVoucher = new DALVoucherSetting();
        DALStock dalstock = new DALStock();
        List<Int32> SRDetailID = new List<Int32>();
        DALSystem dalSystem = new DALSystem();
        string InsertVoucher = "";

        public frmSaleReturn()
        {
            InitializeComponent();
            SaleReturnsetFormLoad();

            //txtCustomer.Text = "Auto";
            dgvitem.Rows.Add();
            dgvitem.Rows[0].Cells[0].Value = 1;

            List<BOLCurrency> lstCurrency = new List<BOLCurrency>();
            lstCurrency = dalexchange.FillCurrency();
            if (lstCurrency.Count > 0)
            {
                cbocurrency.DisplayMember = "Currency";
                cbocurrency.ValueMember = "Id";
                cbocurrency.DataSource = lstCurrency;
                cbocurrency.SelectedIndex = 0;
            }
            cbopaymenttype.SelectedIndex = 0;
            txtItemCode.Visible = true;
            txtCustomer.Text = "";
        }

        public frmSaleReturn(long salereturnid, string itemcode)
        {
            try
            {
                InitializeComponent();
                SaleReturnsetFormLoad();
                List<BOLSaleReturn> bolsalereturnlist = new List<BOLSaleReturn>();
                bolsalereturnlist = dalsalereturn.GetSaleReturnBySaleReturnID(salereturnid, 0);

                //string a = lblpurid.Text;

                if (bolsalereturnlist.Count > 0)
                {
                    if (salereturnid != 0)
                    {
                        lblsalereturnid.Text = salereturnid.ToString();


                        lbluserid.Text = bolsalereturnlist[0].Userid.ToString();
                        txtvoucherno.Text = bolsalereturnlist[0].Voucherno.ToString();
                        txtCustomer.Text = bolsalereturnlist[0].Customerid.ToString();
                        lblCustomerName.Text = bolsalereturnlist[0].Customername.ToString(); 
                        lblCustomerID.Text = bolsalereturnlist[0].Cid.ToString();
                        cbopaymenttype.Text = bolsalereturnlist[0].Paymenttype.ToString();
                        cbocurrency.SelectedValue = Int32.Parse(bolsalereturnlist[0].Currencyid.ToString());
                        txttotalamount.Text = bolsalereturnlist[0].Totalamt.ToString();
                        txtsystemvoucherno.Text = bolsalereturnlist[0].Systemvoucherno.ToString();
                        txtdaylimit.Text = bolsalereturnlist[0].Daylimit.ToString();
                                                
                        dgvsalereturn.Rows.Clear();
                        btnsave.Text = "&Update";

                        List<BOLSaleReturn> lstsalereturndetail = new List<BOLSaleReturn>();
                        lstsalereturndetail = dalsalereturn.GetSaleReturnDetailList(salereturnid,  0);
                        dgvsalereturn.Rows.Clear();

                        if (lstsalereturndetail.Count > 0)
                       {
                           for (int i = 0; i < lstsalereturndetail.Count; i++)
                            {
                                dgvsalereturn.Rows.Add();
                                dgvsalereturn.Rows[i].Cells[0].Value = i + 1;
                                
                                dgvsalereturn.Rows[i].Cells[1].Value = lstsalereturndetail[i].Salereturnid.ToString();
                                dgvsalereturn.Rows[i].Cells[2].Value = lstsalereturndetail[i].Voucherno.ToString();
                                dgvsalereturn.Rows[i].Cells[3].Value = lstsalereturndetail[i].Itemcode.ToString();
                                dgvsalereturn.Rows[i].Cells[4].Value = lstsalereturndetail[i].Description.ToString();
                                dgvsalereturn.Rows[i].Cells[5].Value = lstsalereturndetail[i].Qty.ToString();
                                dgvsalereturn.Rows[i].Cells[6].Value = lstsalereturndetail[i].Saleprice.ToString();
                                dgvsalereturn.Rows[i].Cells[7].Value = lstsalereturndetail[i].Total.ToString();
                                dgvsalereturn.Rows[i].Cells[8].Value = lstsalereturndetail[i].Charge.ToString();
                                dgvsalereturn.Rows[i].Cells[9].Value = lstsalereturndetail[i].Salereturndetailid.ToString();
                           }
                       }
                    }
                }
                btnnew.Visible = true;
                //txtCustomer.Text = ""; commented by htzn
                //dgvsalereturn.Rows.Add();
                //dgvsalereturn.CurrentCell = dgvsalereturn.Rows[dgvsalereturn.Rows.Count - 1].Cells[2];
                //dgvsalereturn.Rows[dgvsalereturn.Rows.Count - 1].Cells[0].Value = (dgvsalereturn.Rows.Count + 1)-1;
                //dgvsalereturn.Rows[dgvsalereturn.Rows.Count - 1].Cells[1].Value = "0000";
                //dgvsalereturn.Rows[dgvsalereturn.Rows.Count - 1].Cells[4].Value = "1";
                //dgvsalereturn.Rows[dgvsalereturn.Rows.Count - 1].Cells[5].Value = "0";
                //dgvsalereturn.Rows[dgvsalereturn.Rows.Count - 1].Cells[6].Value = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void LoadTemp()
        {
            DateTime voucherno = DateTime.Now;
            Random getrandom = new Random();
            string currentloc = dallocation.GetCurrentLocationCode();
            string sysVoucherNo = daltran.GetMaxVoucher("salereturn");
            txtsystemvoucherno.Text = currentloc + getrandom.Next(001,999) + DateTime.Now.ToString("yyMMdd") + sysVoucherNo;
            lbltransactionid.Text = daltran.GetTransitionID("SaleReturn").ToString();

            BOLTransition boltrans = new BOLTransition();
            boltrans.TransName = "SaleReturn";
            boltrans.TransID = daltran.GetTransitionID("SaleReturn");
            daltran.SaveTransition(boltrans);
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtvoucherno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void SaleReturnsetFormLoad()
        {
            try
            {
                string sm = dtplotterydate.Value.Month.ToString().Length > 1 ? dtplotterydate.Value.Month.ToString() : "0" + dtplotterydate.Value.Month.ToString();
                string sd = dtplotterydate.Value.Day.ToString().Length > 1 ? dtplotterydate.Value.Day.ToString() : "0" + dtplotterydate.Value.Day.ToString();
                string sdate = dtplotterydate.Value.Year.ToString() + "-" + sm + "-" + sd + " 00:00:00.000";
                LotteryDate = DateTime.Parse(sdate);

                List<BOLCurrency> lstCurrency = new List<BOLCurrency>();
                lstCurrency = dalexchange.FillCurrency();
                if (lstCurrency.Count > 0)
                {
                    cbocurrency.DisplayMember = "Currency";
                    cbocurrency.ValueMember = "Id";
                    cbocurrency.DataSource = lstCurrency;
                    cbocurrency.SelectedIndex = 0;
                }
                cbopaymenttype.SelectedIndex = 0;

                List<BolLocation> lstlocation = new List<BolLocation>();
                lstlocation = dallocation.SelectAllLocation();

                if (lstlocation.Count > 0)
                {
                    cboLocation.DisplayMember = "Location";
                    cboLocation.ValueMember = "ID";
                    cboLocation.DataSource = lstlocation;
                    for (int i = 0; i < lstlocation.Count; i++)
                    {
                        if (lstlocation[i].IsThisLocation)
                        {
                            cboLocation.SelectedValue = lstlocation[i].ID;
                        }
                    }
                    cboLocation.SelectedIndex = 0;
                }
                DALCustomer dalcust = new DALCustomer();
                List<BOLCustomer> getlstcustomer = new List<BOLCustomer>();
                getlstcustomer = dalcustomer.GetCustomer(txtCustomer.Text, "Cash", 0);
                if (getlstcustomer.Count > 0)
                {
                    lblCustomerID.Text = getlstcustomer[0].ID.ToString();
                    txtCustomer.Text = getlstcustomer[0].CustomerID;
                    dgvCustomer.Visible = false;
                }
                else
                {
                    BOLCustomer bolcustomer = new BOLCustomer();
                    bolcustomer.ID = 0;
                    bolcustomer.Name = "";
                    bolcustomer.CustomerID = "";
                    bolcustomer.Memberid = "0";
                    bolcustomer.Address = "";
                    bolcustomer.Phone = "";
                    bolcustomer.Dateofbirth = DateTime.Now;
                    bolcustomer.Joindate = DateTime.Now;
                    bolcustomer.Currentdate = DateTime.Now;
                    bolcustomer.Nrc = "";
                    bolcustomer.Email = "";
                    bolcustomer.Customerlevel = "";
                    bolcustomer.Township = 1;
                    bolcustomer.Divisionid = 1;
                    bolcustomer.Creditlimit = 0;
                    bolcustomer.Iscash = true;
                    bolcustomer.Iscredit = false;
                    bolcustomer.Shop = "";
                    bolcustomer.SortingID = 1;
                    bolcustomer.Saletarget = 0;
                    bolcustomer.DepositAmount = 0;
                    bolcustomer.Contactperson = "";
                    bolcustomer.Departmentid = 1;
                    dalcust.SaveCustomer(bolcustomer);

                    List<BOLCustomer> getcustomer = new List<BOLCustomer>();
                    getcustomer = dalcustomer.GetCustomer("Auto", "", 0);
                    if (getcustomer.Count > 0 & chkwholesale.Checked != true)
                    {
                        lblCustomerID.Text = getcustomer[0].ID.ToString();
                        txtCustomer.Text = getcustomer[0].CustomerID;
                        dgvCustomer.Visible = false;
                    }
                }
                
                lbluserid.Text = frmMain.UserID.ToString();
                //txtCustomer.Focus();
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
                if (e.KeyCode == Keys.Enter)
                {
                    lblCustomerID.Text = dgvCustomer.CurrentRow.Cells[0].Value.ToString();
                    txtCustomer.Text = dgvCustomer.CurrentRow.Cells[1].Value.ToString();
                    lblCustomerName.Text = dgvCustomer.CurrentRow.Cells[2].Value.ToString();
                    bool chkPaymentType = false;
                    chkPaymentType = dalcustomer.ChkPaymentType(Int32.Parse(lblCustomerID.Text));
                    if (chkPaymentType)
                    {
                        cbopaymenttype.Enabled = true;
                    }
                    else
                    {
                        cbopaymenttype.Enabled = false;
                    }

                    if (dgvCustomer.CurrentRow.Cells[4].Value.ToString() == "True")
                    {
                        chkwholesale.Checked = true;
                        chkwholesale.Enabled = true;
                    }
                    else
                    {
                        chkwholesale.Checked = false;
                        chkwholesale.Enabled = false;
                    }
                    dgvCustomer.Visible = false;
                    txtsalevoucher.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmSaleReturn_Load(object sender, EventArgs e)
        {
            try
            {
                LstCheckPrintAndMsgBox = dalsalereturn.CheckIsSavePrint(frmMain.UserID);
                if (lblsalereturnid.Text == "0")
                {
                    LoadTemp();
                    txtCustomer.Text = "";
                    txtCustomer.Focus();
                }
                //dgvitem.Focus();
                txtItemCode.Visible = true;
                txttotalamount.Enabled = false;
                txtCustomer.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvitem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                List<BOLSale> list = new List<BOLSale>();
                if (e.ColumnIndex == 9)
                {
                    if (this.InsertVoucher == "")
                    {
                        this.InsertVoucher = this.dgvitem.CurrentRow.Cells["VoucherNo"].Value.ToString();
                    }
                    if (this.InsertVoucher == this.dgvitem.CurrentRow.Cells["VoucherNo"].Value.ToString())
                    {
                        if (this.dgvitem.Rows.Count > 0)
                        {
                            this.dgvsalereturn.Rows.Add(new object[]
							{
								this.dgvsalereturn.Rows.Count + 1,
								this.dgvitem.CurrentRow.Cells[1].Value.ToString(),
								this.dgvitem.Rows[e.RowIndex].Cells[2].Value.ToString(),
								this.dgvitem.Rows[e.RowIndex].Cells[3].Value.ToString(),
								this.dgvitem.Rows[e.RowIndex].Cells[4].Value.ToString(),
								this.dgvitem.Rows[e.RowIndex].Cells[5].Value.ToString(),
								this.dgvitem.Rows[e.RowIndex].Cells[6].Value.ToString(),
								this.dgvitem.Rows[e.RowIndex].Cells[7].Value.ToString(),
								this.dgvitem.Rows[e.RowIndex].Cells[8].Value.ToString()
							});
                            this.txtsalevoucher.Text = this.dgvitem.CurrentRow.Cells[2].Value.ToString();
                        }
                        this.dgvitem.CurrentRow.Visible = false;
                        int num = 0;
                        int count = this.dgvsalereturn.Rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            int num2 = int.Parse(this.dgvsalereturn.Rows[i].Cells[5].Value.ToString()) * int.Parse(this.dgvsalereturn.Rows[i].Cells[6].Value.ToString());
                            this.dgvsalereturn.Rows[i].Cells[7].Value = num2.ToString();
                            num += num2;
                            this.txttotalamount.Text = num.ToString();
                        }
                        this.dgvitem.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Please Choose Same Voucher's Stock", "Unvalid Voucher No!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvitem_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dgvsalereturn_KeyDown(object sender, KeyEventArgs e)
        {
            int amount, totalprice = 0;

            if (e.KeyCode == Keys.Delete)
            {
                if (dgvsalereturn.Rows.Count > 0)
                {
                    if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow row in dgvsalereturn.SelectedRows)
                        {
                            if (row.Cells[8].Value == null) //dgvsalereturn.Rows[e.RowIndex].Cells[7].Value.ToString() == " "
                            {
                                dgvsalereturn.Rows.Remove(row);
                            }

                            else 
                            {
                                SRDetailID.Add(Int32.Parse(row.Cells[8].Value.ToString()));
                                dgvsalereturn.Rows.Remove(row);
                            }

                            for (int i = 0; i < dgvsalereturn.Rows.Count; i++)
                            { 
                                totalprice += Int32.Parse(dgvsalereturn.Rows[i].Cells[7].Value.ToString());
                            }
                        }
                    }

                    txttotalamount.Text = totalprice.ToString();
                }
                else
                {
                    MessageBox.Show(" Please do sale reuturn.");
                }
            }

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    //if (dgvsalereturn.CurrentCell.ColumnIndex ==2)
                    //  {
                    //      dgvsalereturn.CurrentCell = dgvsalereturn.Rows[dgvsalereturn.Rows.Count - 1].Cells[4];
                    //  }
                    if (dgvsalereturn.CurrentCell.ColumnIndex == 4)
                    {
                        int salecount = dgvsalereturn.Rows.Count;
                        int itemcount = dgvitem.Rows.Count;
                        if (dgvitem.Rows.Count > 0)
                        {
                            if (dgvsalereturn.Rows.Count > 0)
                            {
                                for (int i = 0; i < itemcount; i++)
                                {
                                    for (int j = 0; j < salecount; j++)
                                    {
                                        long saletotal = long.Parse(dgvsalereturn.Rows[j].Cells[5].Value.ToString());
                                        long item = long.Parse(dgvitem.Rows[i].Cells[2].Value.ToString());
                                        int saleqty = Int32.Parse(dgvsalereturn.Rows[j].Cells[5].Value.ToString());
                                        int itemqty = Int32.Parse(dgvitem.Rows[j].Cells[4].Value.ToString());
                                        if (saletotal == item)
                                        {
                                            //if (Convert.ToInt32(dgvitem.CurrentRow.Cells[4].Value) < Convert.ToInt32(dgvsalereturn.CurrentRow.Cells[5].Value))
                                            if (saleqty == 0)
                                            {
                                                MessageBox.Show("Qty Must be Greater than 0");
                                            }
                                            if (saleqty < 0)
                                            {
                                                MessageBox.Show("Qty Must be Greater than 0");
                                            }
                                            amount = Int32.Parse(dgvsalereturn.Rows[j].Cells[5].Value.ToString()) * Int32.Parse(dgvsalereturn.Rows[j].Cells[6].Value.ToString());
                                            dgvsalereturn.Rows[j].Cells[7].Value = amount.ToString();

                                        }

                                        totalprice += Int32.Parse(dgvsalereturn.Rows[j].Cells[7].Value.ToString());
                                        txttotalamount.Text = totalprice.ToString();
                                    }
                                }
                                dgvsalereturn.CurrentCell = dgvsalereturn.Rows[dgvsalereturn.Rows.Count - 1].Cells[dgvsalereturn.CurrentCell.ColumnIndex + 1];
                                txtItemCode.Enabled = true;
                                txtItemCode.Focus();
                                return;
                            }
                        }
                        else
                        {
                            if (dgvsalereturn.Rows.Count > 0)
                            {
                                for (int j = 0; j < salecount; j++)
                                {
                                    // long saletotal = long.Parse(dgvsalereturn.Rows[j].Cells[1].Value.ToString());

                                    int itemqty = Int32.Parse(dgvsalereturn.Rows[j].Cells[5].Value.ToString());
                                    // if (saletotal == item)
                                    //{
                                    //if (Convert.ToInt32(dgvitem.CurrentRow.Cells[4].Value) < Convert.ToInt32(dgvsalereturn.CurrentRow.Cells[5].Value))

                                    if (itemqty < 0)
                                    {
                                        MessageBox.Show("Qty Must be Greater than 0");
                                    } 
                                    amount = Int32.Parse(dgvsalereturn.Rows[j].Cells[5].Value.ToString()) * Int32.Parse(dgvsalereturn.Rows[j].Cells[6].Value.ToString());
                                    dgvsalereturn.Rows[j].Cells[7].Value = amount.ToString();
                                    totalprice = totalprice + amount;
                                }

                                txttotalamount.Text = totalprice.ToString();
                                //dgvsalereturn.Rows.Add();
                                //dgvsalereturn.CurrentCell = dgvsalereturn.Rows[dgvsalereturn.Rows.Count - 1].Cells[0];
                                //dgvsalereturn.Rows[dgvsalereturn.Rows.Count - 1].Cells[0].Value = dgvsalereturn.Rows.Count;
                            }

                        }
                        
                        dgvsalereturn.Rows.Add();
                        dgvsalereturn.CurrentCell = dgvsalereturn.Rows[dgvsalereturn.Rows.Count - 1].Cells[2];
                        dgvsalereturn.Rows[dgvsalereturn.Rows.Count - 1].Cells[0].Value = dgvsalereturn.Rows.Count;
                        txtItemCode.Focus();
                    }

                    if (dgvsalereturn.CurrentCell.ColumnIndex == 5)
                    {
                        dgvsalereturn.CurrentCell = dgvsalereturn.Rows[dgvsalereturn.CurrentRow.Index+1].Cells[5];
                    }

                    if (dgvsalereturn.CurrentCell.ColumnIndex == 8)
                    {
                        long saletotal = long.Parse(dgvsalereturn.CurrentRow.Cells[7].Value.ToString());
                        long charges = long.Parse(dgvsalereturn.CurrentRow.Cells[8].Value.ToString());
                        dgvsalereturn.CurrentRow.Cells[7].Value = saletotal + charges;
                        decimal totalamt = Convert.ToDecimal(txttotalamount.Text);
                        txttotalamount.Text = (totalamt + charges).ToString();
                    }
                    
                    //remove by ksaung
                    //if(dgvsalereturn.CurrentCell.ColumnIndex==7)
                    //{
                    //    dgvsalereturn.Rows.Add();
                    //    dgvsalereturn.CurrentCell = dgvsalereturn.Rows[dgvsalereturn.Rows.Count - 1].Cells[2];
                    //    dgvsalereturn.Rows[dgvsalereturn.Rows.Count - 1].Cells[0].Value = dgvsalereturn.Rows.Count;
                    //    txtItemCode.Focus();
                    //}
                    //else
                    //{
                    //    dgvsalereturn.CurrentCell = dgvsalereturn.Rows[dgvsalereturn.Rows.Count - 1].Cells[dgvsalereturn.CurrentCell.ColumnIndex+1];
                    //}
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void txtsalevoucher_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               if(txtCustomer.Text != "")
               {
                try
                {
                    List<BOLSale> lstsale = new List<BOLSale>();
                    lstsale = dalsalereturn.SelectSaleItem(txtsalevoucher.Text,lblCustomerID.Text);
                    dgvitem.Rows.Clear();

                    if (lstsale.Count > 0)
                    {
                        for (int i = 0; i < lstsale.Count; i++)
                        {
                            dgvitem.Rows.Add(lstsale[i].No = i + 1, lstsale[i].SaleDetailID, lstsale[i].VoucherNo, lstsale[i].ItemCode, lstsale[i].Description, lstsale[i].Qty, lstsale[i].SalePrice,lstsale[i].Total ,lstsale[i].Charge);
                        }
                        dgvitem.Focus();
                        dgvitem.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
               }
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            
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

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
			{
				if (this.txttotalamount.Text == "0" || this.lblCustomerID.Text == "0")
				{
					MessageBox.Show("Please Enter Require Data to Save.");
					this.dgvsalereturn.Focus();
				}
				else
				{
					int num;
					if (!this.chkNotByVoucher.Checked | this.lblsalereturnid.Text != "0")
					{
						num = this.dgvsalereturn.Rows.Count;
					}
					else
					{
						num = this.dgvsalereturn.Rows.Count - 1;
					}
					if (this.lblsalereturnid.Text == "0")
					{
						BOLSaleReturn bolsaleReturn = new BOLSaleReturn();
						bolsaleReturn.Transalereturnid = long.Parse(this.lbltransactionid.Text);
						bolsaleReturn.Voucherno = this.txtsystemvoucherno.Text;
						bolsaleReturn.Date = this.dtpSaleDate.Value;
						bolsaleReturn.Cid = (long)int.Parse(this.lblCustomerID.Text);
						bolsaleReturn.Userid = int.Parse(this.lbluserid.Text);
						bolsaleReturn.Paymenttype = this.cbopaymenttype.Text;
						bolsaleReturn.Systemvoucherno = this.txtsalevoucher.Text;
						if (this.cbocurrency.SelectedValue != "")
						{
							bolsaleReturn.Currencyid = int.Parse(this.cbocurrency.SelectedValue.ToString());
						}
						bolsaleReturn.Daylimit = int.Parse(this.txtdaylimit.Text);
						bolsaleReturn.Totalamt = decimal.Parse(this.txttotalamount.Text);
						bolsaleReturn.Systemvoucherno = this.txtsystemvoucherno.Text;
						bolsaleReturn.Exchangerate = decimal.Parse(this.txtexchangerate.Text);
						bolsaleReturn.Locationid = long.Parse(this.cboLocation.SelectedValue.ToString());
						bolsaleReturn.SaleSystemVoucherNo = this.InsertVoucher;
						int num2 = this.dalsalereturn.InsertSaleReturn(bolsaleReturn);
						if (num2 == 1)
						{
							for (int i = 0; i < num; i++)
							{
								if (this.dgvsalereturn.Rows.Count > 0)
								{
									if (this.dgvsalereturn.Rows[i].Cells["SaleReturnDetailID"].Value == null)
									{
										BOLSaleReturn bolsaleReturn2 = new BOLSaleReturn();
										bolsaleReturn2.Salereturnid = this.dalsalereturn.GetMaxSaleReturnID(this.txtsystemvoucherno.Text);
										this.lblsalereturnid.Text = bolsaleReturn2.Salereturnid.ToString();
										bolsaleReturn2.Itemcode = this.dgvsalereturn.Rows[i].Cells["colItem"].Value.ToString();
										bolsaleReturn2.Description = this.dgvsalereturn.Rows[i].Cells["colDescription"].Value.ToString();
										bolsaleReturn2.Type = "Pcs";
										bolsaleReturn2.Qty = int.Parse(this.dgvsalereturn.Rows[i].Cells["colQty"].Value.ToString());
										bolsaleReturn2.Saleprice = decimal.Parse(this.dgvsalereturn.Rows[i].Cells["colsaleprice"].Value.ToString());
										bolsaleReturn2.Total = decimal.Parse(this.dgvsalereturn.Rows[i].Cells["colAmount"].Value.ToString());
										bolsaleReturn2.OriginalVoucherNo = this.dgvsalereturn.Rows[i].Cells["colVoucherNo"].Value.ToString();
										bolsaleReturn2.Charge = decimal.Parse(this.dgvsalereturn.Rows[i].Cells["colCharges"].Value.ToString());
										this.dalsalereturn.InsertSaleReturnDetail(bolsaleReturn2);
									}
									else if (this.dgvsalereturn.Rows[i].Cells["SaleReturnDetailID"].Value.ToString() == "")
									{
										BOLSaleReturn bolsaleReturn2 = new BOLSaleReturn();
										bolsaleReturn2.Salereturnid = this.dalsalereturn.GetMaxSaleReturnID(this.txtsystemvoucherno.Text);
										this.lblsalereturnid.Text = bolsaleReturn2.Salereturnid.ToString();
										bolsaleReturn2.Itemcode = this.dgvsalereturn.Rows[i].Cells["colItem"].Value.ToString();
										bolsaleReturn2.Description = this.dgvsalereturn.Rows[i].Cells["colDescription"].Value.ToString();
										bolsaleReturn2.Type = "Pcs";
										bolsaleReturn2.Qty = int.Parse(this.dgvsalereturn.Rows[i].Cells["colQty"].Value.ToString());
										bolsaleReturn2.Saleprice = decimal.Parse(this.dgvsalereturn.Rows[i].Cells["colsaleprice"].Value.ToString());
										bolsaleReturn2.Total = decimal.Parse(this.dgvsalereturn.Rows[i].Cells["colAmount"].Value.ToString());
										bolsaleReturn2.OriginalVoucherNo = this.dgvsalereturn.Rows[i].Cells["colVoucherNo"].Value.ToString();
										bolsaleReturn2.Charge = decimal.Parse(this.dgvsalereturn.Rows[i].Cells["colCharges"].Value.ToString());
										this.dalsalereturn.InsertSaleReturnDetail(bolsaleReturn2);
									}
								}
							}
						}
						MessageBox.Show("Data is Successfully saved.");
						this.LoadTemp();
					}
					else
					{
						BOLSaleReturn bolsaleReturn3 = new BOLSaleReturn();
						bolsaleReturn3.Salereturnid = long.Parse(this.lblsalereturnid.Text);
						bolsaleReturn3.Voucherno = this.txtsystemvoucherno.Text;
						bolsaleReturn3.Editsaleretundate = this.dtpSaleDate.Value;
						bolsaleReturn3.Cid = long.Parse(this.lblCustomerID.Text);
						bolsaleReturn3.Edituserid = int.Parse(this.lbluserid.Text);
						bolsaleReturn3.Paymenttype = this.cbopaymenttype.Text;
						bolsaleReturn3.Currencyid = int.Parse(this.cbocurrency.SelectedValue.ToString());
						bolsaleReturn3.Daylimit = int.Parse(this.txtdaylimit.Text);
						bolsaleReturn3.Totalamt = decimal.Parse(this.txttotalamount.Text);
						bolsaleReturn3.Exchangerate = decimal.Parse(this.txtexchangerate.Text);
						bolsaleReturn3.Systemvoucherno = this.txtsystemvoucherno.Text;
						bolsaleReturn3.Locationid = long.Parse(this.cboLocation.SelectedValue.ToString());
						bolsaleReturn3.Date = this.dtpSaleDate.Value;
						bolsaleReturn3.Lotteryno = this.txtlotteryno.Text;
						int num2 = this.dalsalereturn.UpdateSaleReturnBySaleReturnID(bolsaleReturn3);
						for (int i = 0; i < num; i++)
						{
							if (this.dgvsalereturn.Rows.Count > 0)
							{
								if (this.SRDetailID.Count > 0)
								{
									for (int j = 0; j < this.SRDetailID.Count; j++)
									{
										this.dalsalereturn.DeleteSaleReturnDetail(long.Parse(this.SRDetailID[j].ToString()));
									}
								}
								if (this.dgvsalereturn.Rows[i].Cells["SaleReturnDetailID"].Value != "")
								{
									BOLSaleReturn bolsaleReturn2 = new BOLSaleReturn();
									if (this.dgvsalereturn.Rows[i].Cells["SaleReturnDetailID"].Value != null)
									{
										bolsaleReturn2.Salereturndetailid = long.Parse(this.dgvsalereturn.Rows[i].Cells["SaleReturnDetailID"].Value.ToString());
										bolsaleReturn2.Itemcode = this.dgvsalereturn.Rows[i].Cells["colItem"].Value.ToString();
										bolsaleReturn2.Description = this.dgvsalereturn.Rows[i].Cells["colDescription"].Value.ToString();
										bolsaleReturn2.Type = "Psc";
										bolsaleReturn2.Qty = int.Parse(this.dgvsalereturn.Rows[i].Cells["colQty"].Value.ToString());
										bolsaleReturn2.Saleprice = decimal.Parse(this.dgvsalereturn.Rows[i].Cells["colsaleprice"].Value.ToString());
										bolsaleReturn2.Total = decimal.Parse(this.dgvsalereturn.Rows[i].Cells["colAmount"].Value.ToString());
										bolsaleReturn2.OriginalVoucherNo = this.dgvsalereturn.Rows[i].Cells["colVoucherNo"].Value.ToString();
										bolsaleReturn2.Charge = decimal.Parse(this.dgvsalereturn.Rows[i].Cells["colCharges"].Value.ToString());
										this.dalsalereturn.UpdateSaleReturnDetailData(bolsaleReturn2);
									}
									else
									{
										bolsaleReturn2.Salereturnid = long.Parse(this.lblsalereturnid.Text);
										bolsaleReturn2.Itemcode = this.dgvsalereturn.Rows[i].Cells["colItem"].Value.ToString();
										bolsaleReturn2.Description = this.dgvsalereturn.Rows[i].Cells["colDescription"].Value.ToString();
										bolsaleReturn2.Type = "Psc";
										bolsaleReturn2.Qty = int.Parse(this.dgvsalereturn.Rows[i].Cells["colQty"].Value.ToString());
										bolsaleReturn2.Saleprice = decimal.Parse(this.dgvsalereturn.Rows[i].Cells["colsaleprice"].Value.ToString());
										bolsaleReturn2.Total = decimal.Parse(this.dgvsalereturn.Rows[i].Cells["colAmount"].Value.ToString());
										bolsaleReturn2.OriginalVoucherNo = this.dgvsalereturn.Rows[i].Cells["colVoucherNo"].Value.ToString();
										bolsaleReturn2.Charge = decimal.Parse(this.dgvsalereturn.Rows[i].Cells["colCharges"].Value.ToString());
										this.dalsalereturn.InsertSaleReturnDetail(bolsaleReturn2);
									}
								}
							}
						}
						MessageBox.Show("Data is Successfully updated.");
					}
					btnpreview_Click(sender, e);
					btnnew_Click(sender, e);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
        }
        

        private void btnAll_Click(object sender, EventArgs e)
        {
            try
            {
                string sale = txtsalevoucher.Text;
                if (txtsalevoucher.Text != "")
                {
                    List<BOLSale> lstsale = new List<BOLSale>();
                    lstsale = dalsalereturn.SelectSaleItem(txtsalevoucher.Text,lblCustomerID.Text);
                    /// dgvsalereturn.Rows.Clear();
                    int totalprice = 0;
                    if (lstsale.Count > 0)
                    {
                        for (int i = 0; i < lstsale.Count; i++)
                        {
                            dgvsalereturn.Rows.Add(dgvsalereturn.Rows.Count + 1, lstsale[i].SaleDetailID, lstsale[i].VoucherNo, lstsale[i].ItemCode, lstsale[i].Description, lstsale[i].Qty, lstsale[i].SalePrice, lstsale[i].Total);
                        }
                    }
                    if (dgvsalereturn.Rows.Count > 0)
                    {
                        for (int i = 0; i < dgvsalereturn.Rows.Count; i++)
                        {
                            totalprice += Int32.Parse(dgvsalereturn.Rows[i].Cells[6].Value.ToString());
                        }
                    }
                    txttotalamount.Text = totalprice.ToString();
                    dgvsalereturn.Focus();
                    btnAll.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Enter Voucher No !!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void cbocurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int currencyid = Int32.Parse(cbocurrency.SelectedValue.ToString());
                BOLExchange exchange = new BOLExchange();
                exchange = dalexchange.GetExchangeRate(currencyid);
                txtexchangerate.Text = exchange.Exchangerate.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            try
            {
                BOLExchange bolexchange = new BOLExchange();
                bolexchange.Id = Int32.Parse(lblexchangerate.Text);
                bolexchange.Exchangerate = Int32.Parse(txtexchangerate.Text);
                bolexchange.Currency = Int32.Parse(cbocurrency.SelectedValue.ToString());

                if (Int32.Parse(lblexchangerate.Text) == 0)
                {
                    dalexchange.SaveExchangeRate(bolexchange);
                }
                else
                {
                    dalexchange.UpdateExchangeRate(bolexchange);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtsalevoucher_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnpreview_Click(object sender, EventArgs e)
        {
            try
            {
                List<BOLSaleReturn> lst = new List<BOLSaleReturn>();
                List<BOLSystem> lstsystem = new List<BOLSystem>();
                lstsystem = dalSystem.ShowAllSystem();

                for (int i = 0; i < dgvsalereturn.Rows.Count ; i++)
                {
                    BOLSaleReturn bolsalereturnpreview = new BOLSaleReturn();
                    bolsalereturnpreview.Salereturnid = long.Parse(lblsalereturnid.Text);
                    bolsalereturnpreview.Customerid = lblCustomerName.Text;
                    if (txtvoucherno.Text == "")
                    {
                        bolsalereturnpreview.Voucherno = txtsystemvoucherno.Text;
                    }
                    else
                    {
                        bolsalereturnpreview.Voucherno = txtvoucherno.Text;
                    }
                    bolsalereturnpreview.Total = Decimal.Parse(dgvsalereturn.Rows[i].Cells[7].Value.ToString());
                    bolsalereturnpreview.Paymenttype = cbopaymenttype.Text;
                    bolsalereturnpreview.Currency = cbocurrency.Text;
                    bolsalereturnpreview.Itemcode = dgvsalereturn.Rows[i].Cells[3].Value.ToString();
                    bolsalereturnpreview.Description = dgvsalereturn.Rows[i].Cells[4].Value.ToString();
                    bolsalereturnpreview.Qty = Int32.Parse(dgvsalereturn.Rows[i].Cells[5].Value.ToString());
                    bolsalereturnpreview.Charge = Decimal.Parse(dgvsalereturn.Rows[i].Cells[8].Value.ToString());
                    bolsalereturnpreview.Saleprice = Decimal.Parse(dgvsalereturn.Rows[i].Cells[6].Value.ToString());
                    bolsalereturnpreview.Totalamt = Decimal.Parse(txttotalamount.Text);

                    bolsalereturnpreview.Location = cboLocation.Text;
                    bolsalereturnpreview.SaleSystemVoucherNo = txtsystemvoucherno.Text;

                    lst.Add(bolsalereturnpreview);

                }
                if (lst.Count > 0)
                {
                    this.UseWaitCursor = true;
                    ReportDocument cu_Report = new ReportDocument();
                    cu_Report.Load(Application.StartupPath + @"\Report\RptSaleRetun.rpt");
                    cu_Report.SetDataSource(lst);
                    cu_Report.SetDatabaseLogon("sa", "moeyan");

                    List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                    lstvoucherSetting = dalVoucher.SelectAllVoucher();

                    DataTable dtt = new DataTable();
                    DataColumn dc = new DataColumn();
                    dc.ColumnName = "Name";
                    dc.DataType = System.Type.GetType("System.String");
                    dtt.Columns.Add(dc);
                    DataColumn dc1 = new DataColumn();
                    dc1.ColumnName = "Message";
                    dc1.DataType = System.Type.GetType("System.String");
                    dtt.Columns.Add(dc1);
                    DataColumn dc2 = new DataColumn();
                    dc2.ColumnName = "Logo";
                    dc2.DataType = System.Type.GetType("System.Byte[]");
                    dtt.Columns.Add(dc2);
                    DataColumn dc3 = new DataColumn();
                    dc3.ColumnName = "Address";
                    dc3.DataType = System.Type.GetType("System.String");
                    dtt.Columns.Add(dc3);
                    DataColumn dc4 = new DataColumn();
                    dc4.ColumnName = "Phone";
                    dc4.DataType = System.Type.GetType("System.String");
                    dtt.Columns.Add(dc4);

                    if (lstvoucherSetting.Count > 0)
                    {
                        for (int i = 0; i < lstvoucherSetting.Count; i++)
                        {
                            DataRow dr = dtt.NewRow();
                            dr["Name"] = lstvoucherSetting[0].Name;
                            dr["Message"] = lstvoucherSetting[0].Message;
                            dr["Logo"] = lstvoucherSetting[0].Logo;
                            dr["Address"] = lstvoucherSetting[0].Address;
                            dr["Phone"] = lstvoucherSetting[0].Phone;
                            dtt.Rows.Add(dr);

                        }

                        cu_Report.Subreports[0].SetDataSource(dtt);
                    }
                    if (lstsystem.Count > 0)
                    {
                        if (lstsystem[0].Printerslip != "")
                        {

                            CrystalDecisions.Shared.PageMargins margin = cu_Report.PrintOptions.PageMargins;
                            cu_Report.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLetter;
                            cu_Report.PrintOptions.ApplyPageMargins(margin);
                        }
                    }
                    cu_Report.PrintOptions.PrinterName = lstsystem[0].Printerslip;
                    cu_Report.PrintToPrinter(2, false, 0, 0);

                    //frmReport frmReport = new frmReport();
                    //frmReport.rptViewer.ReportSource = cu_Report;
                    //frmReport.rptViewer.Refresh();
                    //frmReport.Show();
                    //this.UseWaitCursor = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            //try
            //{
            //    this.UseWaitCursor = true;
            //    ReportDocument l_Report = new ReportDocument();
            //    DataSet dts = new DataSet();
            //    dts = dalsalereturn.SelectSaleReturnVoucher(long.Parse(txtsystemvoucherno.Text), 1);

            //    if (dts.Tables[0].Rows.Count > 0)
            //    {
            //        //ReportDocument l_Report = new ReportDocument();

            //        dts.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_SaleReturn.xsd");
            //        l_Report.Load(Application.StartupPath + @"\Report\RptSaleRetun.rpt");

            //        DataSet dt = new DataSet();
                    
            //        l_Report.SetDataSource(dts);
            //        l_Report.SetDatabaseLogon("sa", "moeyan");

            //        frmReport frmReport = new frmReport();
            //        frmReport.rptViewer.ReportSource = l_Report;
            //        frmReport.rptViewer.Refresh();
            //        frmReport.Show();
            //        this.UseWaitCursor = false;
            //    }
            //    else
            //    {
            //        MessageBox.Show("No data for Preview");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
        }

        private void clear()
        {
            txtsalevoucher.Text = "";
            txtCustomer.Text = "";
            lblCustomerID.Text = "0";
            lblsalereturnid.Text = "0";
            txtvoucherno.Text = "";
            dgvsalereturn.Rows.Clear();
            dgvitem.Rows.Clear();
            txtsalevoucher.Focus();
            lbluserid.Text = frmMain.UserID.ToString();
            txttotalamount.Text = "0";
            if (cbopaymenttype.Text == "Cash" & chkNotByVoucher.Checked)
            {
                dgvsalereturn.Rows.Add();
                dgvsalereturn.Rows[0].Cells[0].Value = "1";
                dgvsalereturn.CurrentCell = dgvsalereturn.Rows[0].Cells[2];
                txtsalevoucher.Text = txtsystemvoucherno.Text;
                txtItemCode.Enabled = true;
            }
            else
            {
                dgvsalereturn.Rows.Clear();
                txtItemCode.Enabled = false;
                txttotalamount.Text = "0";
            }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                LoadTemp();
                dgvsalereturn.Focus();
                btnAll.Enabled = true;
                dgvitem.Visible = false;
                dgvCustomer.Visible = false;
                dgvItemCode.Visible = false;
                //dgvsalereturn.Rows[0].Cells[0].ReadOnly = true;
                //dgvsalereturn.Rows[0].Cells[1].Selected = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtdrawingtimes_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Int32.Parse(txtdaylimit.Text);
            }
            catch
            {
                MessageBox.Show("Enter Number");
            }

        }

        private void txtdaylimit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Int32.Parse(txtdaylimit.Text);
            }
            catch
            {
                MessageBox.Show("Enter Number");
            }

        }

        private void panel1_Click(object sender, EventArgs e)
        {
            try
            {
                dgvitem.Visible = false;
                dgvCustomer.Visible = false;
                dgvItemCode.Visible = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        //private void panel1_Paint(object sender, PaintEventArgs e)
        //{

        //}

        private void dgvsalereturn_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int amount, totalprice = 0;
                 try
                {
                    if (e.ColumnIndex == 3)
                    {
                        List<BOLStock> lstStk = new List<BOLStock>();
                        lstStk = dalstock.SelectStock("", 0, dgvsalereturn.CurrentRow.Cells[2].Value.ToString(), 0);
                        if (lstStk.Count > 0)
                        {
                            dgvsalereturn.CurrentRow.Cells[3].Value = lstStk[0].NameEng;
                            if (chkwholesale.Checked)
                            {
                                dgvsalereturn.CurrentRow.Cells[5].Value = lstStk[0].WholeSalePrice;
                            }
                            else
                            {
                                dgvsalereturn.CurrentRow.Cells[5].Value = lstStk[0].Price;
                            }
                            dgvsalereturn.CurrentCell = dgvsalereturn.CurrentRow.Cells[4];
                        }
                        else
                        {
                            MessageBox.Show("Enter correct Item Code!");
                            //txtItemCode.Text = dgvSale.CurrentRow.Cells[1].Value.ToString();
                            //txtItemCode.Focus();
                        }
                    }
                    if (e.ColumnIndex == 5)
                    {
                        //dgvsalereturn.CurrentCell = dgvsalereturn.CurrentRow.Cells[8];
                        //dgvsalereturn.CurrentCell = dgvsalereturn.Rows[dgvsalereturn.CurrentRow.Index + 1].Cells[5];
                        
                        int salecount = dgvsalereturn.Rows.Count;
                        int itemcount = dgvitem.Rows.Count;
                        if (dgvitem.Rows.Count > 0 & chkNotByVoucher.Checked == false)
                        {
                            if (dgvsalereturn.Rows.Count > 0)
                            {
                                //for (int i = 0; i < itemcount; i++)
                                //{
                                    totalprice = 0;
                                    for (int j = 0; j < salecount; j++)
                                    {
                                        long saletotal = long.Parse(dgvsalereturn.Rows[j].Cells[6].Value.ToString());
                                        //long item = long.Parse(dgvitem.Rows[i].Cells[6].Value.ToString());
                                        long item = 0;
                                        int saleqty = Int32.Parse(dgvsalereturn.Rows[j].Cells[5].Value.ToString());
                                        int itemqty = Int32.Parse(dgvitem.Rows[j].Cells[5].Value.ToString());
                                        if (dgvsalereturn.CurrentRow.Index == j)
                                        {
                                            dgvsalereturn.CurrentRow.Cells[8].Value = 0;
                                        }
                                        int charges = Int32.Parse(String.IsNullOrEmpty(dgvsalereturn.Rows[j].Cells[8].Value.ToString())?"0":dgvsalereturn.Rows[j].Cells[8].Value.ToString());
                                        //if (dgvsalereturn.CurrentRow.Index == j)
                                        //{

                                            if (saletotal == item)
                                            {
                                                //if (Convert.ToInt32(dgvitem.CurrentRow.Cells[4].Value) < Convert.ToInt32(dgvsalereturn.CurrentRow.Cells[5].Value))
                                                if (saleqty == 0)
                                                {
                                                    MessageBox.Show("Qty Must be Greater than 0");
                                                }
                                                if (saleqty < 0)
                                                {
                                                    MessageBox.Show("Qty Must be Greater than 0");
                                                }
                                                amount = Int32.Parse(dgvsalereturn.Rows[j].Cells[5].Value.ToString()) * Int32.Parse(dgvsalereturn.Rows[j].Cells[6].Value.ToString()) + charges;
                                                dgvsalereturn.Rows[j].Cells[7].Value = amount.ToString();

                                            }

                                            //for (int s = 0; s < dgvsalereturn.Rows.Count; s++)
                                            //{
                                                // if (dgvsalereturn.Rows[s].Cells[7].Value.ToString() == "False")
                                                // {
                                                totalprice += Int32.Parse(dgvsalereturn.Rows[j].Cells[5].Value.ToString()) * Int32.Parse(dgvsalereturn.Rows[j].Cells[6].Value.ToString()) + Int32.Parse(dgvsalereturn.Rows[j].Cells[8].Value.ToString());
                                                //  }
                                            //}

                                            //totalprice += Int32.Parse(dgvsalereturn.Rows[j].Cells[7].Value.ToString());
                                            amount = Int32.Parse(dgvsalereturn.Rows[j].Cells[5].Value.ToString()) * Int32.Parse(dgvsalereturn.Rows[j].Cells[6].Value.ToString()) + charges;
                                            dgvsalereturn.Rows[j].Cells[7].Value = amount.ToString();
                                        //}
                                        txttotalamount.Text = totalprice.ToString();
                                    }
                                //}
                                return;
                            }
                        }
                        else
                        {
                            if (dgvsalereturn.Rows.Count > 0 )
                            {
                                for (int j = 0; j < salecount; j++)
                                {
                                    //long saletotal = long.Parse(dgvsalereturn.Rows[j].Cells[1].Value.ToString());

                                    int itemqty = Int32.Parse(dgvsalereturn.Rows[j].Cells[5].Value.ToString());
                                    // if (saletotal == item)
                                    //{
                                    //if (Convert.ToInt32(dgvitem.CurrentRow.Cells[4].Value) < Convert.ToInt32(dgvsalereturn.CurrentRow.Cells[5].Value))

                                    if (itemqty < 0)
                                    {
                                        MessageBox.Show("Qty Must be Greater than 0");
                                   }
                                    amount = Int32.Parse(dgvsalereturn.Rows[j].Cells[5].Value.ToString()) * Int32.Parse(dgvsalereturn.Rows[j].Cells[6].Value.ToString());
                                    dgvsalereturn.Rows[j].Cells[7].Value = amount.ToString();
                                    totalprice = totalprice + amount;
                                }

                                txttotalamount.Text = totalprice.ToString();
                                if (chkNotByVoucher.Checked)
                                {
                                    dgvsalereturn.Rows.Add();
                                    dgvsalereturn.Rows[dgvsalereturn.Rows.Count - 1].Cells[0].Value = dgvsalereturn.Rows.Count;
                                    dgvsalereturn.CurrentCell = dgvsalereturn.Rows[dgvsalereturn.Rows.Count - 1].Cells[3];
                                    txtItemCode.Focus();
                                }
                            }
                        }

                    }
                    if (e.ColumnIndex == 6)
                    {
                        dgvsalereturn.CurrentRow.Cells[7].Value = Int32.Parse(dgvsalereturn.CurrentRow.Cells[6].Value.ToString()) * Int32.Parse(dgvsalereturn.CurrentRow.Cells[5].Value.ToString());
                    }
                    if (e.ColumnIndex == 8)
                    {
                        long saletotal = (Int32.Parse(dgvsalereturn.CurrentRow.Cells[5].Value.ToString()))*(long.Parse(dgvsalereturn.CurrentRow.Cells[6].Value.ToString()));
                        long charges = long.Parse(dgvsalereturn.CurrentRow.Cells[8].Value.ToString());
                        int qty = Int32.Parse(dgvsalereturn.CurrentRow.Cells[5].Value.ToString());
                        dgvsalereturn.CurrentRow.Cells[7].Value = saletotal + (charges*qty);
                        dgvsalereturn.CurrentRow.Cells[8].Value = charges * qty;
                        for (int i = 0; i < dgvsalereturn.Rows.Count; i++)
                        {
                            totalprice += Int32.Parse(dgvsalereturn.Rows[i].Cells[7].Value.ToString());
                        }
                        txttotalamount.Text = totalprice.ToString();
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

        private void txtCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    List<BOLCustomer> lstcustomer = new List<BOLCustomer>();
                    string Type = cbopaymenttype.SelectedItem.ToString();
                    lstcustomer = dalcustomer.GetCustomer(txtCustomer.Text, Type, 0);

                    //if (lstcustomer.Count == 0 & txtCustomer.Text == "Auto")
                    //{
                    //    DALCustomer dalcust = new DALCustomer();
                    //    BOLCustomer bolcustomer = new BOLCustomer();
                    //    bolcustomer.Name = "Auto";
                    //    bolcustomer.CustomerID = "";
                    //    bolcustomer.Memberid = "0";
                    //    bolcustomer.Address = "";
                    //    bolcustomer.Phone = "";
                    //    bolcustomer.Dateofbirth = DateTime.Now;
                    //    bolcustomer.Joindate = DateTime.Now;
                    //    bolcustomer.Currentdate = DateTime.Now;
                    //    bolcustomer.Nrc = "";
                    //    bolcustomer.Email = "";
                    //    bolcustomer.Customerlevel = "";
                    //    bolcustomer.Township = 1;
                    //    bolcustomer.Divisionid = 1;
                    //    bolcustomer.Creditlimit = 0;
                    //    bolcustomer.Iscash = true;
                    //    bolcustomer.Iscredit = false;
                    //    bolcustomer.Shop = "";
                    //    bolcustomer.SortingID = 1;
                    //    bolcustomer.Saletarget = 0;
                    //    bolcustomer.DepositAmount = 0;
                    //    bolcustomer.Contactperson = "";
                    //    bolcustomer.Departmentid = 1;
                    //    dalcust.SaveCustomer(bolcustomer);

                    //    List<BOLCustomer> getlstcustomer = new List<BOLCustomer>();
                    //    getlstcustomer = dalcustomer.GetCustomer(txtCustomer.Text);
                    //    if (getlstcustomer.Count > 0)
                    //    {
                    //        lblCustomerID.Text = getlstcustomer[0].ID.ToString();
                    //        txtCustomer.Text = getlstcustomer[0].CustomerID;
                    //    }
                    //}
                    //else if (lstcustomer.Count > 0)
                    //{
                    dgvCustomer.Rows.Clear();
                    foreach (BOLCustomer c in lstcustomer)
                    {
                        dgvCustomer.Rows.Add(c.ID, c.CustomerID, c.MyanmarName, c.Address, c.Wholesaleprice);
                        //if (c.Name == "Auto" & chkwholesale.Checked == false)
                        //{
                        //    lblCustomerID.Text = c.ID.ToString();
                        //    txtCustomer.Text = c.Name;
                        //    dgvCustomer.Visible = false;
                        //}
                        //else
                        //{
                        //    dgvCustomer.Visible = true;
                        //}
                    }

                    bool isCrdite = false;
                    isCrdite = dalcustomer.ChkPaymentType(Int32.Parse(lblCustomerID.Text));
                    if (isCrdite)
                    {
                        cbopaymenttype.Enabled = true;
                    }
                    else
                    {
                        cbopaymenttype.Enabled = false;
                    }

                    dgvCustomer.Visible = true;
                    dgvCustomer.Focus();
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

        private void cbopaymenttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbopaymenttype.Text == "Cash" & chkNotByVoucher.Checked)
                {
                    dgvsalereturn.Rows.Add();
                    dgvsalereturn.Rows[0].Cells[0].Value = "1";
                    dgvsalereturn.CurrentCell = dgvsalereturn.Rows[0].Cells[2];
                    txtsalevoucher.Text = txtsystemvoucherno.Text;
                    txtItemCode.Enabled = true;
                }
                else
                {
                    dgvsalereturn.Rows.Clear();
                    txtItemCode.Enabled = false;
                    txttotalamount.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void chkNotByVoucher_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbopaymenttype.Text == "Cash" & chkNotByVoucher.Checked)
                {
                    dgvsalereturn.Rows.Add();
                    dgvsalereturn.Rows[0].Cells[0].Value = "1";
                    dgvsalereturn.CurrentCell = dgvsalereturn.Rows[0].Cells[2];
                    txtsalevoucher.Text = txtsystemvoucherno.Text;
                    txtItemCode.Enabled = true;
                }
                else
                {
                    dgvsalereturn.Rows.Clear();
                    txtItemCode.Enabled = false;
                    txttotalamount.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvsalereturn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dgvitem.Visible = false;
                dgvCustomer.Visible = false;
                dgvItemCode.Visible = false;
                int totalprice = 0;

                if (e.ColumnIndex == 10)
                {
                    if (dgvsalereturn.Rows[e.RowIndex].Cells[8].Value == null) //dgvsalereturn.Rows[e.RowIndex].Cells[7].Value.ToString() == " "
                    {
                        dgvsalereturn.Rows.Remove(dgvsalereturn.Rows[e.RowIndex]);
                    }
                    else
                    {
                        //int isdelete = 0;
                        //isdelete=dalsalereturn.DeleteSaleReturnDetail(long.Parse(dgvsalereturn.Rows[e.RowIndex].Cells[9].Value.ToString()));
                        //if (isdelete > 0)
                        //{
                        if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {

                            SRDetailID.Add(Int32.Parse(dgvsalereturn.Rows[e.RowIndex].Cells[8].Value.ToString()));
                            dgvsalereturn.Rows.Remove(dgvsalereturn.Rows[e.RowIndex]);
                        }
                    }
                    for (int i = 0; i < dgvsalereturn.Rows.Count; i++)
                    {
                        totalprice += Int32.Parse(dgvsalereturn.Rows[i].Cells[7].Value.ToString());
                    }
                    txttotalamount.Text = totalprice.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void dgvItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    //dgvsalereturn.Rows.Add();
                    dgvsalereturn.Rows[dgvsalereturn.Rows.Count -1].Cells[0].Value = dgvsalereturn.Rows.Count.ToString();
                    dgvsalereturn.Rows[dgvsalereturn.Rows.Count - 1].Cells[2].Value = dgvItemCode.CurrentRow.Cells[1].Value.ToString();
                    dgvsalereturn.Rows[dgvsalereturn.Rows.Count - 1].Cells[3].Value = dgvItemCode.CurrentRow.Cells[2].Value.ToString();
                    dgvsalereturn.Rows[dgvsalereturn.Rows.Count - 1].Cells[5].Value = dgvItemCode.CurrentRow.Cells[4].Value.ToString();
                    dgvsalereturn.CurrentCell = dgvsalereturn.Rows[dgvsalereturn.Rows.Count - 1].Cells[4];
                    dgvsalereturn.Focus();
                    dgvItemCode.Visible = false;
                    txtItemCode.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtItemCode_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    List<BOLStock> lstStk = new List<BOLStock>();
                    lstStk = dalstock.SelectStock(txtItemCode.Text, 0, "", 0);
                    dgvItemCode.Rows.Clear();
                    if (lstStk.Count > 0)
                    {
                        for (int i = 0; i < lstStk.Count; i++)
                        {
                            dgvItemCode.Rows.Add(lstStk[i].Id, lstStk[i].ItemCode, lstStk[i].NameEng, lstStk[i].NameMM, lstStk[i].Price);
                        }
                        dgvItemCode.Visible = true;
                        dgvItemCode.Focus();

                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmSaleReturn_Click(object sender, EventArgs e)
        {
            try
            {
                dgvitem.Visible = false;
                dgvCustomer.Visible = false;
                dgvItemCode.Visible = false;
                txtCustomer.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvitem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvsalereturn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                dgvitem.Visible = false;
                dgvCustomer.Visible = false;
                dgvItemCode.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cboLocation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboLocation_Click(object sender, EventArgs e)
        {
            try
            {
                dgvitem.Visible = false;
                dgvCustomer.Visible = false;
                dgvItemCode.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cbocurrency_Click(object sender, EventArgs e)
        {
            try
            {
                dgvitem.Visible = false;
                dgvCustomer.Visible = false;
                dgvItemCode.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtsystemvoucherno_Click(object sender, EventArgs e)
        {
            try
            {
                dgvitem.Visible = false;
                dgvCustomer.Visible = false;
                dgvItemCode.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cbopaymenttype_Click(object sender, EventArgs e)
        {
            try
            {
                dgvitem.Visible = false;
                dgvCustomer.Visible = false;
                dgvItemCode.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtlotteryno_Click(object sender, EventArgs e)
        {
            try
            {
                dgvitem.Visible = false;
                dgvCustomer.Visible = false;
                dgvItemCode.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtdaylimit_Click(object sender, EventArgs e)
        {
            try
            {
                dgvitem.Visible = false;
                dgvCustomer.Visible = false;
                dgvItemCode.Visible = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtexchangerate_Click(object sender, EventArgs e)
        {
            try
            {
                dgvitem.Visible = false;
                dgvCustomer.Visible = false;
                dgvItemCode.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtdrawingtimes_Click(object sender, EventArgs e)
        {
            try
            {
                dgvitem.Visible = false;
                dgvCustomer.Visible = false;
                dgvItemCode.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dtpSaleDate_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                dgvitem.Visible = false;
                dgvCustomer.Visible = false;
                dgvItemCode.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvsalereturn_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                dgvitem.Visible = false;
                dgvCustomer.Visible = false;
                dgvItemCode.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }       
 }

        
    
    
}
