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
    public partial class frmPurchaseReturn : Form
    {
        DALCustomer dalcustomer = new DALCustomer();
        DALSupplier dalsupplier = new DALSupplier();
        DALExchangeRate dalexchange = new DALExchangeRate();
        DALSaleReturn dalsalereturn = new DALSaleReturn();
        DALPurchaseReturn dalpurchasereturn = new DALPurchaseReturn();
        DALTransition daltran = new DALTransition();
        DALLocation dallocation = new DALLocation();
        BOLUser LstCheckPrintAndMsgBox = new BOLUser();
        DateTime SaleDate; DateTime LotteryDate;
        DALVoucherSetting dalVoucher = new DALVoucherSetting();
        DALStock dalstock = new DALStock();
        List<Int32> PRDetailID = new List<Int32>();

        public frmPurchaseReturn()
        {
            InitializeComponent();
            PurchaseReturnsetFormLoad();

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

        public frmPurchaseReturn(long salereturnid, string itemcode)
        {
            try
            {
                InitializeComponent();
                PurchaseReturnsetFormLoad();
                List<BOLPurchaseReturn> bolsalereturnlist = new List<BOLPurchaseReturn>();
                bolsalereturnlist = dalpurchasereturn.GetPurchaseReturnBySaleReturnID(salereturnid, 0);

                //string a = lblpurid.Text;

                if (bolsalereturnlist.Count > 0)
                {
                    if (salereturnid != 0)
                    {
                        lblsalereturnid.Text = salereturnid.ToString();
                        lbluserid.Text = bolsalereturnlist[0].Userid.ToString();
                        txtvoucherno.Text = bolsalereturnlist[0].Voucherno.ToString();
                        txtCustomer.Text = bolsalereturnlist[0].Customername.ToString();
                        lblCustomerID.Text = bolsalereturnlist[0].Cid.ToString();
                        cbopaymenttype.Text = bolsalereturnlist[0].Paymenttype.ToString();
                        cbocurrency.SelectedValue = Int32.Parse(bolsalereturnlist[0].Currencyid.ToString());
                        txttotalamount.Text = bolsalereturnlist[0].Totalamt.ToString();
                        txtsystemvoucherno.Text = bolsalereturnlist[0].Systemvoucherno.ToString();
                        txtdaylimit.Text = bolsalereturnlist[0].Daylimit.ToString();
                                                
                        dgvpurchasereturn.Rows.Clear();
                        btnsave.Text = "&Update";

                        List<BOLPurchaseReturn> lstsalereturndetail = new List<BOLPurchaseReturn>();
                        lstsalereturndetail = dalpurchasereturn.GetPurchaseReturnDetailList(salereturnid, 0);
                        dgvpurchasereturn.Rows.Clear();

                        if (lstsalereturndetail.Count > 0)
                       {
                           for (int i = 0; i < lstsalereturndetail.Count; i++)
                            {
                                dgvpurchasereturn.Rows.Add();
                                dgvpurchasereturn.Rows[i].Cells[0].Value = i + 1;
                                
                                dgvpurchasereturn.Rows[i].Cells[1].Value = lstsalereturndetail[i].Purchasereturnid.ToString();
                                dgvpurchasereturn.Rows[i].Cells[2].Value = lstsalereturndetail[i].Voucherno.ToString();
                                dgvpurchasereturn.Rows[i].Cells[3].Value = lstsalereturndetail[i].Itemcode.ToString();
                                dgvpurchasereturn.Rows[i].Cells[4].Value = lstsalereturndetail[i].Description.ToString();
                                dgvpurchasereturn.Rows[i].Cells[5].Value = lstsalereturndetail[i].Qty.ToString();
                                dgvpurchasereturn.Rows[i].Cells[6].Value = lstsalereturndetail[i].Purchaseprice.ToString();
                                dgvpurchasereturn.Rows[i].Cells[7].Value = lstsalereturndetail[i].Total.ToString();
                                dgvpurchasereturn.Rows[i].Cells[8].Value = lstsalereturndetail[i].Purchasereturndetailid.ToString();
                           }
                       }
                    }
                }
                btnnew.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void LoadTemp()
        {
            DateTime voucherno = DateTime.Now;
            string sysVoucherNo = daltran.GetVoucherNo("PurchaseReturn",voucherno);
            txtsystemvoucherno.Text = sysVoucherNo;
            lbltransactionid.Text = daltran.GetTransitionID("PurchaseReturn").ToString();

            BOLTransition boltrans = new BOLTransition();
            boltrans.TransName = "PurchaseReturn";
            boltrans.TransID = daltran.GetTransitionID("PurchaseReturn");
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
        private void PurchaseReturnsetFormLoad()
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
                DALSupplier dalcust = new DALSupplier();
                List<BOLSupplier> getlstcustomer = new List<BOLSupplier>();
                getlstcustomer = dalcust.GetSupplier(txtCustomer.Text);
                if (getlstcustomer.Count > 0)
                {
                    lblCustomerID.Text = getlstcustomer[0].Supplierid.ToString();
                    txtCustomer.Text = getlstcustomer[0].SupplierName;
                    dgvCustomer.Visible = false;
                }
                else
                {
                    BOLSupplier bolcustomer = new BOLSupplier();
                    bolcustomer.Supplierid = 0;
                    bolcustomer.SupplierName = "";
                    bolcustomer.Address = "";
                    bolcustomer.Phone = "";
                    bolcustomer.Email = "";
                    bolcustomer.TownshipID = 1;
                    bolcustomer.Creditlimit = 0;
                    bolcustomer.Iscash = true;
                    bolcustomer.Iscredit = false;
                    bolcustomer.Creditlimit = 0;
                    bolcustomer.Iscash = false;
                    bolcustomer.Iscredit = false;
                    dalcust.SaveSupplier(bolcustomer);

                    List<BOLSupplier> getcustomer = new List<BOLSupplier>();
                    getcustomer = dalcust.GetSupplier("Auto");
                    if (getcustomer.Count > 0 & chkwholesale.Checked != true)
                    {
                        lblCustomerID.Text = getcustomer[0].Supplierid.ToString();
                        txtCustomer.Text = getcustomer[0].SupplierName;
                        dgvCustomer.Visible = false;
                    }
                }
                
                lbluserid.Text = frmMain.UserID.ToString();
                txtCustomer.Focus();
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
                    txtCustomer.Text = dgvCustomer.CurrentRow.Cells[2].Value.ToString();
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

        private void frmPurchaseReturn_Load(object sender, EventArgs e)
        {
            try
            {
                LstCheckPrintAndMsgBox = dalsalereturn.CheckIsSavePrint(frmMain.UserID);
                if (lblsalereturnid.Text == "0")
                {
                    LoadTemp();
                }
                dgvitem.Focus();
                txtItemCode.Visible = true;
                txtCustomer.Text = "";
                txttotalamount.Enabled = false;
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
                List<BOLSale> lstsale = new List<BOLSale>();
                if (e.ColumnIndex == 8)
                {
                    if (dgvitem.Rows.Count > 0)
                    {
                        dgvpurchasereturn.Rows.Add(dgvpurchasereturn.Rows.Count + 1, dgvitem.CurrentRow.Cells[1].Value.ToString(), dgvitem.Rows[e.RowIndex].Cells[2].Value.ToString(), dgvitem.Rows[e.RowIndex].Cells[3].Value.ToString(), dgvitem.Rows[e.RowIndex].Cells[4].Value.ToString(), dgvitem.Rows[e.RowIndex].Cells[5].Value.ToString(), dgvitem.Rows[e.RowIndex].Cells[6].Value.ToString(), dgvitem.Rows[e.RowIndex].Cells[7].Value.ToString());
                        txtsalevoucher.Text = dgvitem.CurrentRow.Cells[2].Value.ToString();
                    }
                    dgvitem.CurrentRow.Visible = false;
                    int totalprice = 0, rowcount, amount = 0;
                    rowcount = dgvpurchasereturn.Rows.Count;
                    for (int i = 0; i < rowcount; i++)
                    {
                        amount = Int32.Parse(dgvpurchasereturn.Rows[i].Cells[5].Value.ToString()) * Int32.Parse(dgvpurchasereturn.Rows[i].Cells[6].Value.ToString());
                        dgvpurchasereturn.Rows[i].Cells[7].Value = amount.ToString();
                        totalprice += amount;
                        txttotalamount.Text = totalprice.ToString();

                    }
                    dgvitem.Focus();
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
                if (dgvpurchasereturn.Rows.Count > 0)
                {
                    if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow row in dgvpurchasereturn.SelectedRows)
                        {
                            if (row.Cells[8].Value != null)
                            {
                                PRDetailID.Add(Int32.Parse(row.Cells[8].Value.ToString()));
                            }
                            dgvpurchasereturn.Rows.Remove(row);

                            for (int i = 0; i < dgvpurchasereturn.Rows.Count; i++)
                            {
                                totalprice += Int32.Parse(dgvpurchasereturn.Rows[i].Cells[7].Value.ToString());
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show(" Please do sale reuturn.");
                }

                txttotalamount.Text = totalprice.ToString();
            }

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    //if (dgvsalereturn.CurrentCell.ColumnIndex ==2)
                    //  {
                    //      dgvsalereturn.CurrentCell = dgvsalereturn.Rows[dgvsalereturn.Rows.Count - 1].Cells[4];
                    //  }
                    if (dgvpurchasereturn.CurrentCell.ColumnIndex == 4)
                    {
                        int salecount = dgvpurchasereturn.Rows.Count;
                        int itemcount = dgvitem.Rows.Count;
                        if (dgvitem.Rows.Count > 0)
                        {
                            if (dgvpurchasereturn.Rows.Count > 0)
                            {
                                for (int i = 0; i < itemcount; i++)
                                {
                                    for (int j = 0; j < salecount; j++)
                                    {
                                        long saletotal = long.Parse(dgvpurchasereturn.Rows[j].Cells[5].Value.ToString());
                                        long item = long.Parse(dgvitem.Rows[i].Cells[2].Value.ToString());
                                        int saleqty = Int32.Parse(dgvpurchasereturn.Rows[j].Cells[5].Value.ToString());
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
                                            amount = Int32.Parse(dgvpurchasereturn.Rows[j].Cells[5].Value.ToString()) * Int32.Parse(dgvpurchasereturn.Rows[j].Cells[6].Value.ToString());
                                            dgvpurchasereturn.Rows[j].Cells[7].Value = amount.ToString();

                                        }

                                        totalprice += Int32.Parse(dgvpurchasereturn.Rows[j].Cells[7].Value.ToString());
                                        txttotalamount.Text = totalprice.ToString();
                                    }
                                }
                                dgvpurchasereturn.CurrentCell = dgvpurchasereturn.Rows[dgvpurchasereturn.Rows.Count - 1].Cells[dgvpurchasereturn.CurrentCell.ColumnIndex + 1];
                                txtItemCode.Enabled = true;
                                txtItemCode.Focus();
                                return;
                            }
                        }
                        else
                        {
                            if (dgvpurchasereturn.Rows.Count > 0)
                            {
                                for (int j = 0; j < salecount; j++)
                                {
                                    // long saletotal = long.Parse(dgvsalereturn.Rows[j].Cells[1].Value.ToString());

                                    int itemqty = Int32.Parse(dgvpurchasereturn.Rows[j].Cells[5].Value.ToString());
                                    // if (saletotal == item)
                                    //{
                                    //if (Convert.ToInt32(dgvitem.CurrentRow.Cells[4].Value) < Convert.ToInt32(dgvsalereturn.CurrentRow.Cells[5].Value))

                                    if (itemqty < 0)
                                    {
                                        MessageBox.Show("Qty Must be Greater than 0");
                                    } 
                                    amount = Int32.Parse(dgvpurchasereturn.Rows[j].Cells[5].Value.ToString()) * Int32.Parse(dgvpurchasereturn.Rows[j].Cells[6].Value.ToString());
                                    dgvpurchasereturn.Rows[j].Cells[7].Value = amount.ToString();
                                    totalprice = totalprice + amount;
                                }

                                txttotalamount.Text = totalprice.ToString();
                                //dgvsalereturn.Rows.Add();
                                //dgvsalereturn.CurrentCell = dgvsalereturn.Rows[dgvsalereturn.Rows.Count - 1].Cells[0];
                                //dgvsalereturn.Rows[dgvsalereturn.Rows.Count - 1].Cells[0].Value = dgvsalereturn.Rows.Count;
                            }

                        }
                        
                        dgvpurchasereturn.Rows.Add();
                        dgvpurchasereturn.CurrentCell = dgvpurchasereturn.Rows[dgvpurchasereturn.Rows.Count - 1].Cells[2];
                        dgvpurchasereturn.Rows[dgvpurchasereturn.Rows.Count - 1].Cells[0].Value = dgvpurchasereturn.Rows.Count;
                        txtItemCode.Focus();
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
                    List<BOLPurchase> lstpurchase = new List<BOLPurchase>();
                    lstpurchase = dalpurchasereturn.SelectPurchaseItem(txtsalevoucher.Text, lblCustomerID.Text);
                    dgvitem.Rows.Clear();

                    if (lstpurchase.Count > 0)
                    {
                        for (int i = 0; i < lstpurchase.Count; i++)
                        {

                            dgvitem.Rows.Add(lstpurchase[i].No = i + 1, lstpurchase[i].Purchasedetailid, lstpurchase[i].Voucherno, lstpurchase[i].Itemcode, lstpurchase[i].Description, lstpurchase[i].Qty, lstpurchase[i].Purchaseprice, lstpurchase[i].Total);
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
                if (lblCustomerID.Text=="0" )
                {
                    MessageBox.Show("Please Enter Require Data to Save.");
                    dgvpurchasereturn.Focus();
                    return;
                }
                int rowcount = 0;
                if (chkNotByVoucher.Checked == false | lblsalereturnid.Text != "0")
                {
                    rowcount = dgvpurchasereturn.Rows.Count;
                }
                else
                {
                    rowcount = dgvpurchasereturn.Rows.Count-1;
                }
                int issaved = 0; int isDetailSaved = 0;

                if (lblsalereturnid.Text == "0")
                {
                    BOLPurchaseReturn bolsalereturn = new BOLPurchaseReturn();
                    bolsalereturn.Tranpurchasereturnid = long.Parse(lbltransactionid.Text);
                    bolsalereturn.Voucherno = txtsystemvoucherno.Text;
                    bolsalereturn.Date = dtpSaleDate.Value;
                    bolsalereturn.Cid = Int32.Parse(lblCustomerID.Text);
                    bolsalereturn.Userid = Int32.Parse(lbluserid.Text);
                    bolsalereturn.Paymenttype = cbopaymenttype.Text;
                    bolsalereturn.Systemvoucherno = txtsalevoucher.Text;
                    if (cbocurrency.SelectedValue != "")
                    {
                        bolsalereturn.Currencyid = Int32.Parse(cbocurrency.SelectedValue.ToString());
                    }
                    bolsalereturn.Daylimit = Int32.Parse(txtdaylimit.Text);
                    bolsalereturn.Totalamt = Decimal.Parse(txttotalamount.Text);
                    bolsalereturn.Systemvoucherno = txtsystemvoucherno.Text;
                    bolsalereturn.Exchangerate = Decimal.Parse(txtexchangerate.Text);
                    bolsalereturn.Locationid = long.Parse(cboLocation.SelectedValue.ToString());
                    bolsalereturn.SaleSystemVoucherNo = txtsalevoucher.Text;

                    issaved = dalpurchasereturn.InsertPurchaseReturn(bolsalereturn);

                    if (issaved == 1)
                    {
                        for (int i = 0; i < rowcount; i++)
                        {
                            if (dgvpurchasereturn.Rows.Count > 0)
                            {
                                if (dgvpurchasereturn.Rows[i].Cells["PurReturnDetailID"].Value == null)
                                {
                                    BOLPurchaseReturn bolsalereturndetail = new BOLPurchaseReturn();
                                    bolsalereturndetail.Purchasereturnid = dalpurchasereturn.GetMaxPurchaseReturnID(txtsystemvoucherno.Text);
                                    lblsalereturnid.Text = bolsalereturndetail.Purchasereturnid.ToString();
                                    bolsalereturndetail.Itemcode = dgvpurchasereturn.Rows[i].Cells["colItem"].Value.ToString();
                                    bolsalereturndetail.Description = dgvpurchasereturn.Rows[i].Cells["colDescription"].Value.ToString();
                                    bolsalereturndetail.Type = "Pcs";
                                    bolsalereturndetail.Qty = Int32.Parse(dgvpurchasereturn.Rows[i].Cells["colQty"].Value.ToString());
                                    bolsalereturndetail.Purchaseprice = Decimal.Parse(dgvpurchasereturn.Rows[i].Cells["colpurprice"].Value.ToString());
                                    bolsalereturndetail.Total = Decimal.Parse(dgvpurchasereturn.Rows[i].Cells["colAmount"].Value.ToString());
                                    bolsalereturndetail.OriginalVoucherNo = dgvpurchasereturn.Rows[i].Cells["colVoucherNo"].Value.ToString();
                                    //bolsalereturndetail.Systemvoucherno = long.Parse(txtsystemvoucherno.Text);
                                    dalpurchasereturn.InsertPurchaseReturnDetail(bolsalereturndetail);
                                }
                                else if (dgvpurchasereturn.Rows[i].Cells["PurReturnDetailID"].Value.ToString() == "")
                                {
                                    BOLPurchaseReturn bolsalereturndetail = new BOLPurchaseReturn();
                                    bolsalereturndetail.Purchasereturnid = dalpurchasereturn.GetMaxPurchaseReturnID(txtsystemvoucherno.Text);
                                    lblsalereturnid.Text = bolsalereturndetail.Purchasereturnid.ToString();
                                    bolsalereturndetail.Itemcode = dgvpurchasereturn.Rows[i].Cells["colItem"].Value.ToString();
                                    bolsalereturndetail.Description = dgvpurchasereturn.Rows[i].Cells["colDescription"].Value.ToString();
                                    bolsalereturndetail.Type = "Pcs";
                                    bolsalereturndetail.Qty = Int32.Parse(dgvpurchasereturn.Rows[i].Cells["colQty"].Value.ToString());
                                    bolsalereturndetail.Purchaseprice = Decimal.Parse(dgvpurchasereturn.Rows[i].Cells["colpurprice"].Value.ToString());
                                    bolsalereturndetail.Total = Decimal.Parse(dgvpurchasereturn.Rows[i].Cells["colAmount"].Value.ToString());
                                    bolsalereturndetail.OriginalVoucherNo = dgvpurchasereturn.Rows[i].Cells["colVoucherNo"].Value.ToString();

                                    dalpurchasereturn.InsertPurchaseReturnDetail(bolsalereturndetail);
                                }
                                //}
                            }
                           
                        }
                        //MessageBox.Show("SaleReturn data is Successfully Saved.");
                        //btnclear_Click(sender, e);
                    }
                    
                                    //else
                                    //{
                                    //    BOLSaleReturn bolsalereturndetail = new BOLSaleReturn();
                                    //    bolsalereturndetail.Salereturnid = dalsalereturn.GetMaxSaleReturnID(long.Parse(txtsystemvoucherno.Text));
                                    //    lblsalereturnid.Text = bolsalereturndetail.Salereturnid.ToString();
                                    //    bolsalereturndetail.Itemcode = dgvsalereturn.Rows[i].Cells["colItem"].Value.ToString();
                                    //    bolsalereturndetail.Description = dgvsalereturn.Rows[i].Cells["colDescription"].Value.ToString();
                                    //    bolsalereturndetail.Type = "Psc";
                                    //    bolsalereturndetail.Qty = Int32.Parse(dgvsalereturn.Rows[i].Cells["colQty"].Value.ToString());
                                    //    bolsalereturndetail.Saleprice = Decimal.Parse(dgvsalereturn.Rows[i].Cells["colsaleprice"].Value.ToString());
                                    //    bolsalereturndetail.Total = Decimal.Parse(dgvsalereturn.Rows[i].Cells["colAmount"].Value.ToString());


                                    //    dalsalereturn.InsertSaleReturnDetail(bolsalereturndetail);
                                    //}        
                    MessageBox.Show("Data is Successfully saved.");
                    LoadTemp();
                                }
                else
                {
                    BOLPurchaseReturn bolsalereturnupdate = new BOLPurchaseReturn();
                    bolsalereturnupdate.Purchasereturnid = long.Parse(lblsalereturnid.Text);
                    bolsalereturnupdate.Voucherno = txtsystemvoucherno.Text;
                    bolsalereturnupdate.Editpurchaseretundate = dtpSaleDate.Value;
                    bolsalereturnupdate.Cid = long.Parse(lblCustomerID.Text);
                    //bolsalereturnupdate.Userid = Int32.Parse(lbluserid.Text);
                    bolsalereturnupdate.Edituserid = Int32.Parse(lbluserid.Text);
                    bolsalereturnupdate.Paymenttype = cbopaymenttype.Text;
                    bolsalereturnupdate.Currencyid = Int32.Parse(cbocurrency.SelectedValue.ToString());
                    bolsalereturnupdate.Daylimit = Int32.Parse(txtdaylimit.Text);
                    bolsalereturnupdate.Totalamt = Decimal.Parse(txttotalamount.Text);
                    bolsalereturnupdate.Exchangerate = Decimal.Parse(txtexchangerate.Text);
                    bolsalereturnupdate.Systemvoucherno = txtsystemvoucherno.Text;
                    bolsalereturnupdate.Locationid = long.Parse(cboLocation.SelectedValue.ToString());
                    bolsalereturnupdate.Date = dtpSaleDate.Value;
                    bolsalereturnupdate.Lotteryno = txtlotteryno.Text;

                    issaved = dalpurchasereturn.UpdatePurchaseReturnBySaleReturnID(bolsalereturnupdate);

                   // if (issaved == 1)
                    //{
                    for (int i = 0; i < rowcount; i++)
                        {
                            if (dgvpurchasereturn.Rows.Count > 0)
                            {
                                if (PRDetailID.Count > 0)
                                {
                                    for (int k = 0; k < PRDetailID.Count; k++)
                                    {
                                        dalpurchasereturn.DeletePurchaseReturnDetail(long.Parse(PRDetailID[k].ToString()));
                                    }
                                }
                                if (dgvpurchasereturn.Rows[i].Cells["PurReturnDetailID"].Value != "")
                                {
                                    BOLPurchaseReturn bolsalereturndetail = new BOLPurchaseReturn();
                                    if (dgvpurchasereturn.Rows[i].Cells["PurReturnDetailID"].Value != null)
                                    {
                                        bolsalereturndetail.Purchasereturndetailid = long.Parse(dgvpurchasereturn.Rows[i].Cells["PurReturnDetailID"].Value.ToString());
                                        //lblsalereturnid.Text = bolsalereturndetail.Salereturnid.ToString();
                                        bolsalereturndetail.Itemcode = dgvpurchasereturn.Rows[i].Cells["colItem"].Value.ToString();
                                        bolsalereturndetail.Description = dgvpurchasereturn.Rows[i].Cells["colDescription"].Value.ToString();
                                        bolsalereturndetail.Type = "Psc";
                                        bolsalereturndetail.Qty = Int32.Parse(dgvpurchasereturn.Rows[i].Cells["colQty"].Value.ToString());
                                        bolsalereturndetail.Purchaseprice = Decimal.Parse(dgvpurchasereturn.Rows[i].Cells["colpurprice"].Value.ToString());
                                        bolsalereturndetail.Total = Decimal.Parse(dgvpurchasereturn.Rows[i].Cells["colAmount"].Value.ToString());
                                        bolsalereturndetail.OriginalVoucherNo = dgvpurchasereturn.Rows[i].Cells["colVoucherNo"].Value.ToString();

                                        dalpurchasereturn.UpdatePurchaseReturnDetailData(bolsalereturndetail);
                                    }
                                    else
                                    {
                                        //BOLSaleReturn bolsalereturndetail = new BOLSaleReturn();
                                        //bolsalereturndetail.Salereturnid = dalsalereturn.GetMaxSaleReturnID(long.Parse(txtsystemvoucherno.Text));
                                        //lblsalereturnid.Text = bolsalereturndetail.Salereturnid.ToString();
                                        bolsalereturndetail.Purchasereturnid = long.Parse(lblsalereturnid.Text);
                                        bolsalereturndetail.Itemcode = dgvpurchasereturn.Rows[i].Cells["colItem"].Value.ToString();
                                        bolsalereturndetail.Description = dgvpurchasereturn.Rows[i].Cells["colDescription"].Value.ToString();
                                        bolsalereturndetail.Type = "Psc";
                                        bolsalereturndetail.Qty = Int32.Parse(dgvpurchasereturn.Rows[i].Cells["colQty"].Value.ToString());
                                        bolsalereturndetail.Purchaseprice = Decimal.Parse(dgvpurchasereturn.Rows[i].Cells["colpurprice"].Value.ToString());
                                        bolsalereturndetail.Total = Decimal.Parse(dgvpurchasereturn.Rows[i].Cells["colAmount"].Value.ToString());
                                        bolsalereturndetail.OriginalVoucherNo = dgvpurchasereturn.Rows[i].Cells["colVoucherNo"].Value.ToString();

                                        dalpurchasereturn.UpdatePurchaseReturnDetailData(bolsalereturndetail);
                                    }
                                }
                            }
                        }
                    MessageBox.Show("Data is Successfully updated.");
                    //}
                }
                clear();
               
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
                    List<BOLPurchase> lstsale = new List<BOLPurchase>();
                    lstsale = dalpurchasereturn.SelectPurchaseItem(txtsalevoucher.Text, lblCustomerID.Text);
                    /// dgvsalereturn.Rows.Clear();
                    int totalprice = 0;
                    if (lstsale.Count > 0)
                    {
                        for (int i = 0; i < lstsale.Count; i++)
                        {
                            dgvpurchasereturn.Rows.Add(dgvpurchasereturn.Rows.Count + 1, lstsale[i].Purchasedetailid, lstsale[i].Voucherno, lstsale[i].Itemcode, lstsale[i].Description, lstsale[i].Qty, lstsale[i].Purchaseprice, lstsale[i].Total);
                        }
                    }
                    if (dgvpurchasereturn.Rows.Count > 0)
                    {
                        for (int i = 0; i < dgvpurchasereturn.Rows.Count; i++)
                        {
                            totalprice += Int32.Parse(dgvpurchasereturn.Rows[i].Cells[6].Value.ToString());
                        }
                    }
                    txttotalamount.Text = totalprice.ToString();
                    dgvpurchasereturn.Focus();
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
                List<BOLPurchaseReturn> lst = new List<BOLPurchaseReturn>();
                for (int i = 0; i < dgvpurchasereturn.Rows.Count ; i++)
                {
                    BOLPurchaseReturn bolsalereturnpreview = new BOLPurchaseReturn();
                    bolsalereturnpreview.Purchasereturnid = long.Parse(lblsalereturnid.Text);
                    bolsalereturnpreview.Supplierid = txtCustomer.Text;
                    if (txtvoucherno.Text == "")
                    {
                        bolsalereturnpreview.Voucherno = txtsystemvoucherno.Text;
                    }
                    else
                    {
                        bolsalereturnpreview.Voucherno = txtvoucherno.Text;
                    }
                    bolsalereturnpreview.Total = Decimal.Parse(dgvpurchasereturn.Rows[i].Cells[7].Value.ToString());
                    bolsalereturnpreview.Paymenttype = cbopaymenttype.Text;
                    bolsalereturnpreview.Currency = cbocurrency.Text;
                    bolsalereturnpreview.Itemcode = dgvpurchasereturn.Rows[i].Cells[3].Value.ToString();
                    bolsalereturnpreview.Description = dgvpurchasereturn.Rows[i].Cells[4].Value.ToString();
                    bolsalereturnpreview.Qty = Int32.Parse(dgvpurchasereturn.Rows[i].Cells[5].Value.ToString());
                    bolsalereturnpreview.Purchaseprice = Decimal.Parse(dgvpurchasereturn.Rows[i].Cells[6].Value.ToString());
                    bolsalereturnpreview.Totalamt = Decimal.Parse(txttotalamount.Text);

                    bolsalereturnpreview.Location = cboLocation.Text;
                    bolsalereturnpreview.SaleSystemVoucherNo = txtsystemvoucherno.Text;

                    lst.Add(bolsalereturnpreview);

                }
                if (lst.Count > 0)
                {
                    this.UseWaitCursor = true;
                    ReportDocument cu_Report = new ReportDocument();
                    cu_Report.Load(Application.StartupPath + @"\Report\RptPurchaseRetun.rpt");
                    cu_Report.SetDataSource(lst);
                    cu_Report.SetDatabaseLogon("sa", "moeyan");

                    List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                    lstvoucherSetting = dalVoucher.SelectAllVoucher();

                    DataTable dtt = new DataTable();
                    DataColumn dc = new DataColumn();
                    dc.ColumnName = "Name";
                    dc.DataType = System.Type.GetType("System.String");
                    dtt.Columns.Add(dc);

                    if (lstvoucherSetting.Count > 0)
                    {
                        for (int i = 0; i < lstvoucherSetting.Count; i++)
                        {
                            DataRow dr = dtt.NewRow();
                            dr["Name"] = lstvoucherSetting[0].Name;
                            dtt.Rows.Add(dr);
                        }

                        cu_Report.Subreports[0].SetDataSource(dtt);
                    } 

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
            dgvpurchasereturn.Rows.Clear();
            dgvitem.Rows.Clear();
            txtsalevoucher.Focus();
            lbluserid.Text = frmMain.UserID.ToString();
            txttotalamount.Text = "0";
            if (cbopaymenttype.Text == "Cash" & chkNotByVoucher.Checked)
            {
                dgvpurchasereturn.Rows.Add();
                dgvpurchasereturn.Rows[0].Cells[0].Value = "1";
                dgvpurchasereturn.CurrentCell = dgvpurchasereturn.Rows[0].Cells[2];
                txtsalevoucher.Text = txtsystemvoucherno.Text;
                txtItemCode.Enabled = true;
            }
            else
            {
                dgvpurchasereturn.Rows.Clear();
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
                dgvpurchasereturn.Focus();
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
                        lstStk = dalstock.SelectStock("", 0, dgvpurchasereturn.CurrentRow.Cells[2].Value.ToString(), 0);
                        if (lstStk.Count > 0)
                        {
                            dgvpurchasereturn.CurrentRow.Cells[3].Value = lstStk[0].NameEng;
                            if (chkwholesale.Checked)
                            {
                                dgvpurchasereturn.CurrentRow.Cells[5].Value = lstStk[0].WholeSalePrice;
                            }
                            else
                            {
                                dgvpurchasereturn.CurrentRow.Cells[5].Value = lstStk[0].Price;
                            }
                            dgvpurchasereturn.CurrentCell = dgvpurchasereturn.CurrentRow.Cells[4];
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
                        int salecount = dgvpurchasereturn.Rows.Count;
                        int itemcount = dgvitem.Rows.Count;
                        if (dgvitem.Rows.Count > 0 & chkNotByVoucher.Checked == false)
                        {
                            if (dgvpurchasereturn.Rows.Count > 0)
                            {
                                for (int i = 0; i < itemcount; i++)
                                {
                                    for (int j = 0; j < salecount; j++)
                                    {
                                        long saletotal = long.Parse(dgvpurchasereturn.Rows[j].Cells[6].Value.ToString());
                                        long item = long.Parse(dgvitem.Rows[i].Cells[6].Value.ToString());
                                        int saleqty = Int32.Parse(dgvpurchasereturn.Rows[j].Cells[5].Value.ToString());
                                        int itemqty = Int32.Parse(dgvitem.Rows[i].Cells[5].Value.ToString());
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
                                            amount = Int32.Parse(dgvpurchasereturn.Rows[j].Cells[5].Value.ToString()) * Int32.Parse(dgvpurchasereturn.Rows[j].Cells[6].Value.ToString());
                                            dgvpurchasereturn.Rows[j].Cells[7].Value = amount.ToString();

                                        }
                                        totalprice = 0;
                                        for (int s = 0; s < dgvpurchasereturn.Rows.Count; s++)
                                        {
                                           // if (dgvsalereturn.Rows[s].Cells[7].Value.ToString() == "False")
                                           // {
                                                totalprice += Int32.Parse(dgvpurchasereturn.Rows[s].Cells[7].Value.ToString());
                                          //  }
                                        }

                                        //totalprice += Int32.Parse(dgvsalereturn.Rows[j].Cells[7].Value.ToString());
                                        amount = Int32.Parse(dgvpurchasereturn.Rows[j].Cells[5].Value.ToString()) * Int32.Parse(dgvpurchasereturn.Rows[j].Cells[6].Value.ToString());
                                        dgvpurchasereturn.Rows[j].Cells[7].Value = amount.ToString();
                                        txttotalamount.Text = totalprice.ToString();
                                    }
                                }
                                return;
                            }
                        }
                        else
                        {
                            if (dgvpurchasereturn.Rows.Count > 0 )
                            {
                                for (int j = 0; j < salecount; j++)
                                {
                                    //long saletotal = long.Parse(dgvsalereturn.Rows[j].Cells[1].Value.ToString());

                                    int itemqty = Int32.Parse(dgvpurchasereturn.Rows[j].Cells[5].Value.ToString());
                                    // if (saletotal == item)
                                    //{
                                    //if (Convert.ToInt32(dgvitem.CurrentRow.Cells[4].Value) < Convert.ToInt32(dgvsalereturn.CurrentRow.Cells[5].Value))

                                    if (itemqty < 0)
                                    {
                                        MessageBox.Show("Qty Must be Greater than 0");
                                   }
                                    amount = Int32.Parse(dgvpurchasereturn.Rows[j].Cells[5].Value.ToString()) * Int32.Parse(dgvpurchasereturn.Rows[j].Cells[6].Value.ToString());
                                    dgvpurchasereturn.Rows[j].Cells[7].Value = amount.ToString();
                                    totalprice = totalprice + amount;
                                }

                                txttotalamount.Text = totalprice.ToString();
                                if (chkNotByVoucher.Checked)
                                {
                                    dgvpurchasereturn.Rows.Add();
                                    dgvpurchasereturn.Rows[dgvpurchasereturn.Rows.Count - 1].Cells[0].Value = dgvpurchasereturn.Rows.Count;
                                    dgvpurchasereturn.CurrentCell = dgvpurchasereturn.Rows[dgvpurchasereturn.Rows.Count - 1].Cells[3];
                                    txtItemCode.Focus();
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

        private void txtCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //List<BOLCustomer> lstcustomer = new List<BOLCustomer>();
                    //lstcustomer = dalcustomer.GetCustomer(txtCustomer.Text);
                    List<BOLSupplier> lstsupplier = new List<BOLSupplier>();
                    lstsupplier = dalsupplier.GetSupplier(txtCustomer.Text);

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
                    foreach (BOLSupplier s in lstsupplier)
                    {
                        dgvCustomer.Rows.Add(s.Supplierid, s.Supplierid, s.SupplierName, s.Address, s.Township);
                        if (s.SupplierName == "Auto" & chkwholesale.Checked == false)
                        {
                            lblCustomerID.Text = s.Supplierid.ToString();
                            txtCustomer.Text = s.SupplierName;
                            dgvCustomer.Visible = false;
                        }
                        else
                        {
                            dgvCustomer.Visible = true;
                        }
                    }

                    dgvCustomer.Focus();
                    dgvCustomer.Visible = true;
                    //}

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
                    dgvpurchasereturn.Rows.Add();
                    dgvpurchasereturn.Rows[0].Cells[0].Value = "1";
                    dgvpurchasereturn.CurrentCell = dgvpurchasereturn.Rows[0].Cells[2];
                    txtsalevoucher.Text = txtsystemvoucherno.Text;
                    txtItemCode.Enabled = true;
                }
                else
                {
                    dgvpurchasereturn.Rows.Clear();
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
                    dgvpurchasereturn.Rows.Add();
                    dgvpurchasereturn.Rows[0].Cells[0].Value = "1";
                    dgvpurchasereturn.CurrentCell = dgvpurchasereturn.Rows[0].Cells[2];
                    txtsalevoucher.Text = txtsystemvoucherno.Text;
                    txtItemCode.Enabled = true;
                }
                else
                {
                    dgvpurchasereturn.Rows.Clear();
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

                if (e.ColumnIndex == 9)
                {
                    if (dgvpurchasereturn.Rows[e.RowIndex].Cells[8].Value == null) //dgvsalereturn.Rows[e.RowIndex].Cells[7].Value.ToString() == " "
                    {
                        dgvpurchasereturn.Rows.Remove(dgvpurchasereturn.Rows[e.RowIndex]);
                    }
                    else
                    {
                        if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            //dalpurchasereturn.DeletePurchaseReturnDetail(long.Parse(dgvpurchasereturn.Rows[e.RowIndex].Cells[8].Value.ToString()));
                                PRDetailID.Add(Int32.Parse(dgvpurchasereturn.Rows[e.RowIndex].Cells[8].Value.ToString()));
                                dgvpurchasereturn.Rows.Remove(dgvpurchasereturn.Rows[e.RowIndex]);
                        }
                    }

                    for (int i = 0; i < dgvpurchasereturn.Rows.Count; i++)
                    {
                        totalprice += Int32.Parse(dgvpurchasereturn.Rows[i].Cells[7].Value.ToString());
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
                    dgvpurchasereturn.Rows[dgvpurchasereturn.Rows.Count -1].Cells[0].Value = dgvpurchasereturn.Rows.Count.ToString();
                    dgvpurchasereturn.Rows[dgvpurchasereturn.Rows.Count - 1].Cells[2].Value = dgvItemCode.CurrentRow.Cells[1].Value.ToString();
                    dgvpurchasereturn.Rows[dgvpurchasereturn.Rows.Count - 1].Cells[3].Value = dgvItemCode.CurrentRow.Cells[2].Value.ToString();
                    dgvpurchasereturn.Rows[dgvpurchasereturn.Rows.Count - 1].Cells[5].Value = dgvItemCode.CurrentRow.Cells[4].Value.ToString();
                    dgvpurchasereturn.CurrentCell = dgvpurchasereturn.Rows[dgvpurchasereturn.Rows.Count - 1].Cells[4];
                    dgvpurchasereturn.Focus();
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
