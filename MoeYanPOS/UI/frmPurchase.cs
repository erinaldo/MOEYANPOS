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

namespace MoeYanPOS.UI
{
    public partial class frmPurchase : Form
    {
        BOLUser LstCheckPrintAndMsgBox = new BOLUser();
        int Total = 0; int TotalFOC = 0; decimal TotalItemDiscount = 0; DateTime SaleDate; DateTime LotteryDate; string SysVoucherNo;
        DALSupplier dalsupplier = new DALSupplier();
        DALStock dalstock = new DALStock();
        DALExchangeRate dalexchange = new DALExchangeRate();
        DALPurchase dalpurchase = new DALPurchase();
        DALTransition daltrans = new DALTransition();
        DALMeasurement dalmeasurement = new DALMeasurement();
        DALLocation dallocation = new DALLocation();
        DALSystem dalSystem = new DALSystem();
        DALVoucherSetting dalVoucher = new DALVoucherSetting();
        TextBox tb = new TextBox();
        List<Int32> PDetailID = new List<Int32>();

        public frmPurchase()
        {
            try
            {
                InitializeComponent();
                purchasesetFormLoad();
                //btnnew.Visible = false;

                //txtsupplier.Text = "Auto";

                List<BOLSupplier> lstsupplier = new List<BOLSupplier>();
                lstsupplier = dalpurchase.GetSupplier(txtsupplier.Text);
                txtsupplier.Text = "";


                //string voucherno = DateTime.Now.ToString("yyyyMMdd");
                //txtVoucherNo.Text = "DTT" + voucherno + dalpurchase.GetPurchaseMaxID().ToString();
                //txtsupplier.Text = "Auto";

//commented by htzn for Purchase 
                //if (lstsupplier.Count == 0 & txtsupplier.Text == "Auto")
                //{
                //    DALPurchase dalpur = new DALPurchase();
                //    BOLSupplier bolsupplier = new BOLSupplier();
                //    bolsupplier.SupplierName = "Auto";

                //    dalsupplier.SaveSupplier(bolsupplier);

                //    List<BOLSupplier> getlstsupplier = new List<BOLSupplier>();
                //    getlstsupplier = dalsupplier.GetSupplier(txtsupplier.Text);

                //    if (getlstsupplier.Count > 0)
                //    {
                //        lblsupplierid.Text = getlstsupplier[0].Supplierid.ToString();
                //        txtsupplier.Text = getlstsupplier[0].SupplierName;
                //    }

                //}
                //else
                //{
                //        lblsupplierid.Text = lstsupplier[0].Supplierid.ToString();
                //        txtsupplier.Text = lstsupplier[0].SupplierName;
                //}
//commented by htzn for Purchase 
                dgvpurchase.Rows.Add();
                dgvpurchase.Rows[0].Cells[0].Value = 1;
                dgvpurchase.Rows[0].Cells[1].Value = "0000";
                dgvpurchase.Rows[0].Cells[2].Value = "";
                dgvpurchase.Rows[0].Cells[3].Value = colType.Items[0].ToString();
                dgvpurchase.Rows[0].Cells[4].Value = "1";
                dgvpurchase.Rows[0].Cells[5].Value = "0";
                dgvpurchase.Rows[0].Cells[6].Value = "0";
                dgvpurchase.Rows[0].Cells[7].Value = colFOC.Items[1].ToString();
                dgvpurchase.Rows[0].Cells[8].Value = "0";
                dgvpurchase.Rows[0].Cells[9].Value = "0";
                dgvpurchase.Rows[0].Cells[10].Value = "";
                dgvpurchase.Rows[0].Cells[1].Selected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        public frmPurchase(long purchaseid,string itemcode)
        {
            try
            {
                InitializeComponent();
                purchasesetFormLoad();
                List<BOLPurchase> bolpurchaselist = new List<BOLPurchase>();
                bolpurchaselist = dalpurchase.GetPurchaseByPurchaseID(purchaseid, 0);

                //string a = lblpurid.Text;
                
                if (bolpurchaselist.Count > 0)
                {
                    if (purchaseid != 0)
                    {
                        lblpurid.Text = purchaseid.ToString();
                                                    
                        
                        lblUserID.Text = bolpurchaselist[0].Userid.ToString();
                        txtVoucherNo.Text = bolpurchaselist[0].Voucherno.ToString();
                        txtsupplier.Text = bolpurchaselist[0].SupplierName.ToString();
                        lblsupplierid.Text = bolpurchaselist[0].SupplierID.ToString();
                        cbopaymentby.Text = bolpurchaselist[0].Paymenttype.ToString();
                        cbocurrency.SelectedValue = Int32.Parse(bolpurchaselist[0].Currencyid.ToString());
                        txtTotal.Text = bolpurchaselist[0].Totalamt.ToString();
                        txtAdvance.Text = bolpurchaselist[0].Advance.ToString();
                        txtItemDiscount.Text = bolpurchaselist[0].Totalitemdiscount.ToString();
                        txtgrandtotal.Text = bolpurchaselist[0].Grandtotal.ToString();
                        txtsystemvoucherno.Text = bolpurchaselist[0].Systemvoucherno.ToString();
                        txtTotalFOC.Text = bolpurchaselist[0].Totalfoc.ToString();
                        txtDrawingTimes.Text = bolpurchaselist[0].DrawingTimes.ToString();
                        
                        dgvpurchase.Rows.Clear();
                        btnSave.Text = "&Update";
                        btnnew.Visible = true;

                        List<BOLPurchase> lstpurchasedetail = new List<BOLPurchase>();
                        lstpurchasedetail = dalpurchase.GetPurchaseDetailList(purchaseid,itemcode,0);
                        dgvpurchase.Rows.Clear();

                        if (lstpurchasedetail.Count > 0)
                       {
                           for (int i = 0; i < lstpurchasedetail.Count; i++)
                            {
                                dgvpurchase.Rows.Add();
                                dgvpurchase.Rows[i].Cells[0].Value = i + 1;
                                dgvpurchase.Rows[i].Cells[1].Value = lstpurchasedetail[i].Itemcode.ToString();
                                dgvpurchase.Rows[i].Cells[2].Value = lstpurchasedetail[i].Description.ToString();
                                dgvpurchase.Rows[i].Cells[3].Value = lstpurchasedetail[i].Type.ToString();
                                dgvpurchase.Rows[i].Cells[4].Value = lstpurchasedetail[i].Qty.ToString();
                                dgvpurchase.Rows[i].Cells[5].Value = lstpurchasedetail[i].Purchaseprice.ToString();
                                dgvpurchase.Rows[i].Cells[6].Value = lstpurchasedetail[i].Total.ToString();
                                dgvpurchase.Rows[i].Cells[7].Value = lstpurchasedetail[i].Foc.ToString();
                                dgvpurchase.Rows[i].Cells[8].Value = lstpurchasedetail[i].Itemdiscountpercent.ToString();
                                dgvpurchase.Rows[i].Cells[9].Value = lstpurchasedetail[i].Itemdiscount.ToString();
                                dgvpurchase.Rows[i].Cells[10].Value =lstpurchasedetail[i].Purchasedetailid.ToString();
                           }
                       }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            btnnew.Visible = true;
        }
        private void LoadTemp()
        {
            DateTime voucherno = DateTime.Now;
            string currentloc = dallocation.GetCurrentLocationCode();
            string sysVoucherNo = daltrans.GetMaxVoucher("purchase");
            //txtsystemvoucherno.Text = "P-"+sysVoucherNo.ToString();
            Random getrandom = new Random();
            txtsystemvoucherno.Text = currentloc + getrandom.Next(001, 999) + DateTime.Now.ToString("yyMMdd") + sysVoucherNo;
            lblTransactionID.Text = daltrans.GetTransitionID("Purchase").ToString();

            BOLTransition boltrans = new BOLTransition();
            boltrans.TransName = "Purchase";
            boltrans.TransID = daltrans.GetTransitionID("Purchase");
            daltrans.SaveTransition(boltrans);
        }
        private void purchasesetFormLoad()
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
                cbopaymentby.SelectedIndex = 0;

                List<BOLMeasurement> lstmeasurement = new List<BOLMeasurement>();
                lstmeasurement = dalmeasurement.SelectAllMeasurement();
                if (lstmeasurement.Count > 0)
                {
                    colType.DisplayMember = "Id";
                    colType.ValueMember = "Measurement";
                    colType.DataSource = lstmeasurement;
                }
                else
                {
                    colType.Items.Add("Pcs"); ;
                }

                lblUserID.Text = frmMain.UserID.ToString(); 

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
                }

                cboLocation.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void txtitem_TextChanged(object sender, EventArgs e)
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
        private void setFormLoad()
        {
            try
            {
                string sm = dtplotterydate.Value.Month.ToString().Length > 1 ? dtplotterydate.Value.Month.ToString() : "0" + dtplotterydate.Value.Month.ToString();
                string sd = dtplotterydate.Value.Day.ToString().Length > 1 ? dtplotterydate.Value.Day.ToString() : "0" + dtplotterydate.Value.Day.ToString();
                string sdate = dtplotterydate.Value.Year.ToString() + "-" + sm + "-" + sd + " 00:00:00.000";
                LotteryDate = DateTime.Parse(sdate);    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void dgvItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[1].Value = dgvItemCode.CurrentRow.Cells[1].Value.ToString();
                    dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[2].Value = dgvItemCode.CurrentRow.Cells[3].Value.ToString();
                    dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[5].Value = dgvItemCode.CurrentRow.Cells[4].Value.ToString();
                    txtitem.Text = "";
                    dgvItemCode.Visible = false;
                    dgvpurchase.Focus();
                    dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[0].Selected = false;
                    dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[0].ReadOnly = false;
                    dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[4].Selected = true;
                    dgvpurchase.CurrentCell = dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[4];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmPurchase_Load(object sender, EventArgs e)
        {
            try
            {
                LstCheckPrintAndMsgBox = dalpurchase.CheckIsSavePrint(frmMain.UserID);

                if (lblpurid.Text == "0")
                {
                    LoadTemp();
                }

                dtppurchasedate.Value = DateTime.Now;

                //BOLPurchase bolpurchase = new BOLPurchase();
                //if (txtsystemvoucherno.Text == "0")
                //{
                //    lblpurid.Text=bolpurchase.Purchaseid.ToString();
                //}
                //else
                //{
                //    LoadTemp();
                //}
                dgvpurchase.Focus();
                if(dgvpurchase.Rows.Count > 0)
                {
                    //dgvpurchase.Rows[0].Cells[1].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cbopaymentby_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbopaymentby.Text == "Credit")
                {
                    txtdaylimit.Enabled = true;
                    txtAdvance.Enabled = true;
                }
                else
                {
                    txtdaylimit.Text = "0";
                    txtAdvance.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void CleanTextBox()
        {
            try
            {
                btnSave.Text = "&Save";
                
                //lblpurid.Text = "0";
                txtVoucherNo.Text = "";
                //DateTime purchasedate = DateTime.Now;
                dtppurchasedate.Value = DateTime.Now;
                
                txtlotteryno.Text = "0";
                dtplotterydate.Value = DateTime.Now;
                txtDrawingTimes.Text = "0";

                lblpurid.Text = "0";
                cbopaymentby.SelectedIndex = 0;
                cbocurrency.SelectedIndex = 0;
                lblUserID.Text = frmMain.UserID.ToString();
                txtdaylimit.Text = "0";
                txtTotal.Text = "0";
                txtAdvance.Text = "0";
                txtDiscount.Text = "0";
                txtgrandtotal.Text = "0";
                txtDiscount.Text = "0";
                txtgrandtotal.Text = "0";
                txtTotalFOC.Text = "0";
                txtItemDiscount.Text = "0";
                txtitem.Text = "";
                dgvItemCode.Rows.Clear();
                dgvsupplier.Visible = false;
                dgvItemCode.Visible = false;
                dgvpurchase.Rows.Clear();
                dgvpurchase.Rows.Add();
                dgvpurchase.Rows[0].Cells[0].Value = 1;
                dgvpurchase.Rows[0].Cells[1].Value = "0000";
                dgvpurchase.Rows[0].Cells[2].Value = "";
                dgvpurchase.Rows[0].Cells[3].Value = colType.Items[0].ToString();
                dgvpurchase.Rows[0].Cells[4].Value = "1";
                dgvpurchase.Rows[0].Cells[5].Value = "0";
                dgvpurchase.Rows[0].Cells[6].Value = "0";
                dgvpurchase.Rows[0].Cells[7].Value = colFOC.Items[1].ToString();
                dgvpurchase.Rows[0].Cells[8].Value = "0";
                dgvpurchase.Rows[0].Cells[9].Value = "0";
                dgvpurchase.Rows[0].Cells[10].Value = "";
                dgvpurchase.Rows[0].Cells[1].Selected = true;
                //btnnew.Visible = false;
                //LoadTemp();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                BOLExchange bolexchange = new BOLExchange();
                bolexchange.Id = Int32.Parse(lblexchangerateid.Text);
                bolexchange.Exchangerate = Int32.Parse(txtex.Text);
                bolexchange.Id = Int32.Parse(cbocurrency.SelectedValue.ToString());

                if (Int32.Parse(lblexchangerateid.Text) == 0)
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

        private void pnlHeader_Click(object sender, EventArgs e)
        {
            try
            {
                dgvItemCode.Visible = false;
                //dgvsupplier.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtdaylimit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strCheck = "";
                strCheck = MoeYanPOS.Function.Validation.isNumberField("Day", txtex.Text);
                if (strCheck != "")
                {
                    MessageBox.Show(strCheck);
                    txtex.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtex_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strCheck = "";
                strCheck = MoeYanPOS.Function.Validation.isNumberField("Exchange Rate", txtex.Text);
                if (strCheck != "")
                {
                    MessageBox.Show(strCheck);
                    txtex.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvpurchase_KeyDown(object sender, KeyEventArgs e)
        {
            int totalamount = 0;
            try
            {
                //int icolumn=dgvpurchase.CurrentCell.ColumnIndex;
                //int irow=dgvpurchase.CurrentCell.RowIndex;
                if (e.KeyCode == Keys.F6)
                {
                }

                if (e.KeyCode == Keys.F7)
                {
                }

                if (e.KeyCode == Keys.F8)
                {
                }

                if (e.KeyCode == Keys.F9)
                {
                }

                if (e.KeyCode == Keys.Delete)
                {
                    foreach (DataGridViewRow row in dgvpurchase.SelectedRows)
                    {
                        if (row.Cells[10].Value == null)
                        {
                            dgvpurchase.Rows.Remove(row);
                        }
                        else if (row.Cells[10].Value.ToString() == "")
                        {
                            dgvpurchase.Rows.Remove(row);
                        }
                        else
                        {
                            if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                PDetailID.Add(Int32.Parse(row.Cells[10].Value.ToString()));
                                dgvpurchase.Rows.Remove(row);
                            }
                        }
                    }

                    for (int i = 0; i < dgvpurchase.Rows.Count; i++)
                    {
                        totalamount += Int32.Parse(dgvpurchase.Rows[i].Cells[6].Value.ToString());
                    }
                    txtTotal.Text = (totalamount - (Convert.ToInt32(txtItemDiscount.Text) + Convert.ToInt32(txtTotalFOC.Text))).ToString();
                    txtgrandtotal.Text = (Convert.ToInt32(txtTotal.Text) - (Convert.ToInt32(txtAdvance.Text) + Convert.ToInt32(txtDiscount.Text))).ToString();
                }
                if (e.KeyCode == Keys.Enter)
                {
                    if (dgvpurchase.CurrentCell.ColumnIndex == 1)
                    {
                        if (dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[1].Value.ToString() == "0000")
                        {
                            if (cbopaymentby.Text == "Cash")
                            {
                                txtAdvance.Enabled = false;
                                txtDiscount.Focus();
                            }
                            else if (cbopaymentby.Text == "Credit")
                            {
                                txtAdvance.Enabled = true;
                                txtAdvance.Focus();
                            }
                        }
                        else
                        {
                            List<BOLStock> lststk = new List<BOLStock>();
                            lststk = dalstock.SelectStock("", 0, dgvpurchase.CurrentRow.Cells[1].Value.ToString(), 0);
                            if (lststk.Count > 0)
                            {
                                dgvpurchase.CurrentRow.Cells[2].Value = lststk[0].NameMM;
                                dgvpurchase.CurrentRow.Cells[5].Value = lststk[0].PurchasePrice;
                                //dgvpurchase.CurrentCell = dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[4];
                                dgvpurchase.CurrentCell = dgvpurchase.CurrentRow.Cells[4];
                            }
                            else
                            {
                                MessageBox.Show("Enter correct Item Code!");
                                txtitem.Text = dgvpurchase.CurrentRow.Cells[1].Value.ToString();
                                txtitem.Focus();
                            }
                        }
                    }
                    else if (dgvpurchase.CurrentCell.ColumnIndex == 2)
                    {
                        dgvpurchase.CurrentCell = dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[4];
                    }

                    else if (dgvpurchase.CurrentCell.ColumnIndex == 4)
                    {
                        string qtyCheck = "";
                        qtyCheck = MoeYanPOS.Function.Validation.isNumberField("Qty", dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[4].Value.ToString());

                        if (qtyCheck != "")
                        {
                            MessageBox.Show(qtyCheck);
                            dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[4].Value = 1;
                        }

                        dgvpurchase.CurrentCell = dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[5];
                    }
                    else if (dgvpurchase.CurrentCell.ColumnIndex == 5)
                    {
                        dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[6].Value = Convert.ToString(Int32.Parse(dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[4].Value.ToString()) * Int32.Parse(dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[5].Value.ToString()));

                        int totalprice = 0;
                        for (int i = 0; i < dgvpurchase.Rows.Count; i++)
                        {
                            if (dgvpurchase.Rows[i].Cells[7].Value.ToString() == "False")
                            {
                                totalprice += Int32.Parse(dgvpurchase.Rows[i].Cells[6].Value.ToString());
                            }
                        }
                        txtTotal.Text = totalprice.ToString();
                        dgvpurchase.CurrentCell = dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[6];
                    }
                    else if (dgvpurchase.CurrentCell.ColumnIndex == 6)
                    {
                        dgvpurchase.CurrentCell = dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[7];
                    }
                    else if (dgvpurchase.CurrentCell.ColumnIndex == 7)
                    {
                        if (dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells["colFOC"].Value.ToString() == "True")
                        {
                            dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[8].ReadOnly = true;
                            dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[9].ReadOnly = true;
                        }
                        else
                        {
                            dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[8].ReadOnly = false;
                            dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[9].ReadOnly = false;
                        }
                        dgvpurchase.CurrentCell = dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[8];
                    }
                    else if (dgvpurchase.CurrentCell.ColumnIndex == 8)
                    {
                        string DisCheck = "";
                        DisCheck = MoeYanPOS.Function.Validation.isNumberField("Item Discount%", dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[8].Value.ToString());
                        if (DisCheck != "")
                        {
                            MessageBox.Show(DisCheck);
                            dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[9].Value = "0";
                        }
                        else
                        {
                            int dispercent = 0;
                            dispercent = (Int32.Parse(dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[8].Value.ToString()) * Int32.Parse(dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[6].Value.ToString())) / 100;
                            dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[9].Value = dispercent.ToString();
                        }
                        dgvpurchase.CurrentCell = dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[9];
                    }
                    else if (dgvpurchase.CurrentCell.ColumnIndex != 9)
                    {
                        dgvpurchase.CurrentCell = dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[dgvpurchase.CurrentCell.ColumnIndex + 1];
                    }
                    else
                    {
                        string DisCheck = "";
                        DisCheck = MoeYanPOS.Function.Validation.isNumberField("Item Discount", dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[9].Value.ToString());

                        if (DisCheck != "")
                        {
                            MessageBox.Show(DisCheck);
                            dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[9].Value = 0;
                        }
                        else
                        {
                            dgvpurchase.Rows.Add();
                            dgvpurchase.CurrentCell = dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[0];
                            dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[0].Value = dgvpurchase.Rows.Count;
                            dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[1].Value = "0000";
                            dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[4].Value = "1";
                            dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[5].Value = "0";
                            dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[6].Value = "0";
                            dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[8].Value = "0";
                            dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[9].Value = "0";
                            dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[7].Value = colFOC.Items[1].ToString();
                            txtitem.Focus();
                            decimal TotalDis = 0; decimal TotalFOC = 0; decimal TotalAmt = 0;

                            for (int i = 0; i < dgvpurchase.Rows.Count; i++)
                            {
                                TotalDis += Decimal.Parse(dgvpurchase.Rows[i].Cells[9].Value.ToString());
                            }

                            for (int i = 0; i < dgvpurchase.Rows.Count; i++)
                            {
                                //if (dgvpurchase.Rows[i].Cells[7].Value.ToString() == "True")
                                //{
                                TotalAmt += Decimal.Parse(dgvpurchase.Rows[i].Cells[6].Value.ToString());
                                //}
                            }
                            for (int i = 0; i < dgvpurchase.Rows.Count; i++)
                            {
                                if (dgvpurchase.Rows[i].Cells[7].Value.ToString() == "True")
                                {
                                    TotalFOC += Decimal.Parse(dgvpurchase.Rows[i].Cells[6].Value.ToString());
                                }
                            }

                            txtTotal.Text = TotalAmt.ToString();
                            txtTotalFOC.Text = TotalFOC.ToString();
                            txtItemDiscount.Text = TotalDis.ToString();
                            txtgrandtotal.Text = Convert.ToString(Int32.Parse(txtTotal.Text) - (Int32.Parse(txtAdvance.Text) + Int32.Parse(txtDiscount.Text) + Int32.Parse(txtTotalFOC.Text) + Int32.Parse(txtItemDiscount.Text)));
                            dgvpurchase.CurrentCell = dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[1];

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            } 
           
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strCheck = "";
                strCheck = MoeYanPOS.Function.Validation.isNumberField("Discount", txtDiscount.Text);
                if (Int32.Parse(txtgrandtotal.Text) > Int32.Parse(txtgrandtotal.Text))
                {
                    txtDiscount.Text = "0";
                    txtDiscount.Focus();
                }
                else if (strCheck != "")
                {
                    MessageBox.Show(strCheck);
                    txtDiscount.Text = "0";
                }
                else
                {
                    txtgrandtotal.Text = Convert.ToString(Int32.Parse(txtTotal.Text) - (Int32.Parse(txtAdvance.Text) + Int32.Parse(txtDiscount.Text) + Int32.Parse(txtTotalFOC.Text) + Int32.Parse(txtItemDiscount.Text)));

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtgrandtotal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strCheck = "";
                strCheck = MoeYanPOS.Function.Validation.isNumberField("Grand Total", txtgrandtotal.Text);
                if (strCheck != "")
                {
                    MessageBox.Show(strCheck);
                    // txtGrandTotal.Text = Convert.ToString(Int32.Parse(txtTotalAmt.Text) - (Int32.Parse(txtAdvance.Text) + Int32.Parse(txtDiscount.Text)));
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
                int issaved = 0; int isDetailSaved = 0;

                if (lblpurid.Text == "0")
                {
                    BOLPurchase bolpurchase = new BOLPurchase();
                    bolpurchase.TransPurchaseID = long.Parse(lblTransactionID.Text);
                    bolpurchase.Voucherno = txtVoucherNo.Text;// txtVoucherNo.Text;
                    bolpurchase.Purchasedate = dtppurchasedate.Value;
                    //bolpurchase.SupplierID = 1;//Int32.Parse(lblsupplierid.Text);  Edited by KTMM for DTT Transfer , there is only one supplier//commented by thzn for changing Purchase transaction
                    bolpurchase.SupplierID = Int32.Parse(lblsupplierid.Text);
                    bolpurchase.Userid = Int32.Parse(lblUserID.Text);
                    bolpurchase.Paymenttype = cbopaymentby.Text;
                    if (cbocurrency.SelectedValue != "")
                    {
                        bolpurchase.Currencyid = Int32.Parse(cbocurrency.SelectedValue.ToString());
                    }
                    bolpurchase.Daylimit = Int32.Parse(txtdaylimit.Text);
                    bolpurchase.Totalamt = Decimal.Parse(txtTotal.Text);
                    bolpurchase.Advance = Decimal.Parse(txtAdvance.Text);
                    bolpurchase.Discount = Decimal.Parse(txtDiscount.Text);
                    bolpurchase.Grandtotal = Decimal.Parse(txtgrandtotal.Text);
                    bolpurchase.Totalfoc = Int32.Parse(txtTotalFOC.Text);
                    bolpurchase.Totalitemdiscount = Decimal.Parse(txtItemDiscount.Text);
                    bolpurchase.Exchangerate = Decimal.Parse(txtex.Text);
                    bolpurchase.Systemvoucherno = txtsystemvoucherno.Text;
                    bolpurchase.Lotterydate = LotteryDate;
                    bolpurchase.Lotteryno = txtlotteryno.Text;
                    bolpurchase.DrawingTimes = long.Parse(txtDrawingTimes.Text);
                    bolpurchase.Locationid = long.Parse(cboLocation.SelectedValue.ToString());

                    issaved = dalpurchase.SavePurchaseData(bolpurchase);

                    if (issaved == 1)
                    {
                        for (int i = 0; i < dgvpurchase.Rows.Count; i++)
                        {
                            if (dgvpurchase.Rows.Count > 0)
                            {
                                if (dgvpurchase.Rows[i].Cells["colPurchaseDetailID"].Value == null)
                                {
                                    BOLPurchase bolpurchasedetail = new BOLPurchase();
                                    bolpurchasedetail.Purchaseid = dalpurchase.GetMaxPurchaseID((txtsystemvoucherno.Text));
                                    lblpurid.Text = bolpurchasedetail.Purchaseid.ToString();
                                    bolpurchasedetail.Itemcode = dgvpurchase.Rows[i].Cells["Item"].Value.ToString();
                                    bolpurchasedetail.Description = dgvpurchase.Rows[i].Cells["Description"].Value.ToString();
                                    bolpurchasedetail.Type = "Psc";
                                    bolpurchasedetail.Qty = Int32.Parse(dgvpurchase.Rows[i].Cells["Qty"].Value.ToString());
                                    bolpurchasedetail.Purchaseprice = Decimal.Parse(dgvpurchase.Rows[i].Cells["PurchasePrice"].Value.ToString());
                                    bolpurchasedetail.Total = Decimal.Parse(dgvpurchase.Rows[i].Cells["Amount"].Value.ToString());

                                    if (dgvpurchase.Rows[i].Cells["colFOC"].Value.ToString() == "True")
                                    {
                                        bolpurchasedetail.Foc = true;
                                    }
                                    else
                                    {
                                        bolpurchasedetail.Foc = false;
                                    }
                                    bolpurchasedetail.Itemdiscount = Decimal.Parse(dgvpurchase.Rows[i].Cells["ItemDis"].Value.ToString());
                                    bolpurchasedetail.Itemdiscountpercent = Int32.Parse(dgvpurchase.Rows[i].Cells["ItemDiscount"].Value.ToString());
                                    dalpurchase.SavePurchaseDetailData(bolpurchasedetail);
                                }
                                else if (dgvpurchase.Rows[i].Cells["colPurchaseDetailID"].Value.ToString() == "")
                                {
                                    BOLPurchase bolpurchasedetail = new BOLPurchase();
                                    bolpurchasedetail.Purchaseid = dalpurchase.GetMaxPurchaseID(txtsystemvoucherno.Text);
                                    lblpurid.Text = bolpurchasedetail.Purchaseid.ToString();
                                    bolpurchasedetail.Itemcode = dgvpurchase.Rows[i].Cells["Item"].Value.ToString();
                                    bolpurchasedetail.Description = dgvpurchase.Rows[i].Cells["Description"].Value.ToString();
                                    bolpurchasedetail.Type = "Psc";
                                    bolpurchasedetail.Qty = Int32.Parse(dgvpurchase.Rows[i].Cells["Qty"].Value.ToString());
                                    bolpurchasedetail.Purchaseprice = Decimal.Parse(dgvpurchase.Rows[i].Cells["PurchasePrice"].Value.ToString());
                                    bolpurchasedetail.Total = Decimal.Parse(dgvpurchase.Rows[i].Cells["Amount"].Value.ToString());

                                    if (dgvpurchase.Rows[i].Cells["colFOC"].Value.ToString() == "True")
                                    {
                                        bolpurchasedetail.Foc = true;
                                    }
                                    else
                                    {
                                        bolpurchasedetail.Foc = false;
                                    }
                                    bolpurchasedetail.Itemdiscount = Decimal.Parse(dgvpurchase.Rows[i].Cells["ItemDis"].Value.ToString());
                                    bolpurchasedetail.Itemdiscountpercent = Int32.Parse(dgvpurchase.Rows[i].Cells["ItemDiscount"].Value.ToString());
                                    dalpurchase.SavePurchaseDetailData(bolpurchasedetail);
                                }
                            }
                        }
                        MessageBox.Show("DTT data is Successfully Saved.");

                        SysVoucherNo = "";
                        SysVoucherNo = txtsystemvoucherno.Text;

                        btnnew_Click(sender, e);
                        //CleanTextBox();
                    }
                }
                else
                {
                    BOLPurchase bolpurchaseupdate = new BOLPurchase();
                    bolpurchaseupdate.Purchaseid = long.Parse(lblpurid.Text);
                    bolpurchaseupdate.Voucherno = txtVoucherNo.Text;
                    bolpurchaseupdate.Purchasedate = dtppurchasedate.Value;
                    bolpurchaseupdate.SupplierID = Int32.Parse(lblsupplierid.Text);
                    bolpurchaseupdate.Edituserid = Int32.Parse(lblUserID.Text);
                    bolpurchaseupdate.Paymenttype = cbopaymentby.Text;
                    bolpurchaseupdate.Currencyid = Int32.Parse(cbocurrency.SelectedValue.ToString());
                    bolpurchaseupdate.Daylimit = Int32.Parse(txtdaylimit.Text);
                    bolpurchaseupdate.Totalamt = Decimal.Parse(txtTotal.Text);
                    bolpurchaseupdate.Advance = Decimal.Parse(txtAdvance.Text);
                    bolpurchaseupdate.Discount = Decimal.Parse(txtDiscount.Text);
                    bolpurchaseupdate.Grandtotal = Decimal.Parse(txtgrandtotal.Text);
                    bolpurchaseupdate.Totalfoc = Int32.Parse(txtTotalFOC.Text);
                    bolpurchaseupdate.Totalitemdiscount = Decimal.Parse(txtItemDiscount.Text);
                    bolpurchaseupdate.Exchangerate = Decimal.Parse(txtex.Text);
                    bolpurchaseupdate.Lotterydate = LotteryDate;
                    bolpurchaseupdate.Lotteryno = txtlotteryno.Text;
                    bolpurchaseupdate.DrawingTimes = long.Parse(txtDrawingTimes.Text);
                    bolpurchaseupdate.Systemvoucherno = txtsystemvoucherno.Text;
                    bolpurchaseupdate.Locationid = long.Parse(cboLocation.SelectedValue.ToString());

                    issaved = dalpurchase.UpdatePurchaseByPurchaseID(bolpurchaseupdate);

                    if (issaved == 1)
                    {
                        for (int i = 0; i < dgvpurchase.Rows.Count; i++)
                        {
                            if (dgvpurchase.Rows.Count > 0)
                            {
                                if (PDetailID.Count > 0)
                                {
                                    for (int k = 0; k < PDetailID.Count; k++)
                                    {
                                        dalpurchase.DeletePurchaseDetail(long.Parse(PDetailID[k].ToString()), txtsystemvoucherno.Text);
                                    }
                                }
                                if (dgvpurchase.Rows[i].Cells["colPurchaseDetailID"].Value != "")
                                {
                                    BOLPurchase bolpurchasedetail = new BOLPurchase();
                                    if (dgvpurchase.Rows[i].Cells["colPurchaseDetailID"].Value != null)
                                    {
                                        bolpurchasedetail.Purchasedetailid = long.Parse(dgvpurchase.Rows[i].Cells["colPurchaseDetailID"].Value.ToString());
                                        bolpurchasedetail.Itemcode = dgvpurchase.Rows[i].Cells["Item"].Value.ToString();
                                        bolpurchasedetail.Description = dgvpurchase.Rows[i].Cells["Description"].Value.ToString();
                                        bolpurchasedetail.Type = "Psc";
                                        bolpurchasedetail.Qty = Int32.Parse(dgvpurchase.Rows[i].Cells["Qty"].Value.ToString());
                                        bolpurchasedetail.Purchaseprice = Decimal.Parse(dgvpurchase.Rows[i].Cells["PurchasePrice"].Value.ToString());
                                        bolpurchasedetail.Total = Decimal.Parse(dgvpurchase.Rows[i].Cells["Amount"].Value.ToString());

                                        if (dgvpurchase.Rows[i].Cells["colFOC"].Value.ToString() == "True")
                                        {
                                            bolpurchasedetail.Foc = true;
                                        }
                                        else
                                        {
                                            bolpurchasedetail.Foc = false;
                                        }

                                        bolpurchasedetail.Itemdiscount = Decimal.Parse(dgvpurchase.Rows[i].Cells["ItemDis"].Value.ToString());
                                        bolpurchasedetail.Itemdiscountpercent = Int32.Parse(dgvpurchase.Rows[i].Cells["ItemDiscount"].Value.ToString());
                                        dalpurchase.UpdatePurchaseDetailData(bolpurchasedetail);

                                    }
                                    else
                                    {
                                        //BOLPurchase bolpurchasedetail = new BOLPurchase();
                                        bolpurchasedetail.Purchaseid = dalpurchase.GetMaxPurchaseID(txtsystemvoucherno.Text);
                                        lblpurid.Text = bolpurchasedetail.Purchaseid.ToString();
                                        bolpurchasedetail.Itemcode = dgvpurchase.Rows[i].Cells["Item"].Value.ToString();
                                        bolpurchasedetail.Description = dgvpurchase.Rows[i].Cells["Description"].Value.ToString();
                                        bolpurchasedetail.Type = "Psc";
                                        bolpurchasedetail.Qty = Int32.Parse(dgvpurchase.Rows[i].Cells["Qty"].Value.ToString());
                                        bolpurchasedetail.Purchaseprice = Decimal.Parse(dgvpurchase.Rows[i].Cells["PurchasePrice"].Value.ToString());
                                        bolpurchasedetail.Total = Decimal.Parse(dgvpurchase.Rows[i].Cells["Amount"].Value.ToString());

                                        if (dgvpurchase.Rows[i].Cells["colFOC"].Value.ToString() == "True")
                                        {
                                            bolpurchasedetail.Foc = true;
                                        }
                                        else
                                        {
                                            bolpurchasedetail.Foc = false;
                                        }

                                        bolpurchasedetail.Itemdiscount = Decimal.Parse(dgvpurchase.Rows[i].Cells["ItemDis"].Value.ToString());
                                        bolpurchasedetail.Itemdiscountpercent = Int32.Parse(dgvpurchase.Rows[i].Cells["ItemDiscount"].Value.ToString());
                                        dalpurchase.SavePurchaseDetailData(bolpurchasedetail);
                                    }

                                }
                                else if (dgvpurchase.Rows[i].Cells["colPurchaseDetailID"].Value != null)
                                {
                                    BOLPurchase bolpurchasedetail = new BOLPurchase();
                                    bolpurchasedetail.Purchasedetailid = long.Parse(dgvpurchase.Rows[i].Cells["colPurchaseDetailID"].Value.ToString());
                                    bolpurchasedetail.Itemcode = dgvpurchase.Rows[i].Cells["Item"].Value.ToString();
                                    bolpurchasedetail.Description = dgvpurchase.Rows[i].Cells["Description"].Value.ToString();
                                    bolpurchasedetail.Type = "Psc";
                                    bolpurchasedetail.Qty = Int32.Parse(dgvpurchase.Rows[i].Cells["Qty"].Value.ToString());
                                    bolpurchasedetail.Purchaseprice = Decimal.Parse(dgvpurchase.Rows[i].Cells["PurchasePrice"].Value.ToString());
                                    bolpurchasedetail.Total = Decimal.Parse(dgvpurchase.Rows[i].Cells["Amount"].Value.ToString());

                                    if (dgvpurchase.Rows[i].Cells["colFOC"].Value.ToString() == "True")
                                    {
                                        bolpurchasedetail.Foc = true;
                                    }
                                    else
                                    {
                                        bolpurchasedetail.Foc = false;
                                    }

                                    bolpurchasedetail.Itemdiscount = Decimal.Parse(dgvpurchase.Rows[i].Cells["ItemDis"].Value.ToString());
                                    bolpurchasedetail.Itemdiscountpercent = Int32.Parse(dgvpurchase.Rows[i].Cells["ItemDiscount"].Value.ToString());
                                    dalpurchase.UpdatePurchaseDetailData(bolpurchasedetail);
                                }
                            }
                        }
                    }
                    MessageBox.Show("Save Data is Successfully Updated");

                    SysVoucherNo = "";
                    SysVoucherNo = txtsystemvoucherno.Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtsupplierid_TextChanged(object sender, EventArgs e)
        {
            List<BOLSupplier> lstsupplier = new List<BOLSupplier>();
            lstsupplier = dalsupplier.GetSupplier(txtsupplier.Text);

            if (lstsupplier.Count == 0 & txtsupplier.Text == "Auto")
            {
                DALSupplier dalsup = new DALSupplier();
                BOLSupplier bolsupplier = new BOLSupplier();
                bolsupplier.SupplierName = "Auto";
                dalsupplier.SaveSupplier(bolsupplier);

                List<BOLSupplier> getlstsupplier = new List<BOLSupplier>();
                getlstsupplier = dalsupplier.GetSupplier(txtsupplier.Text);

                if (getlstsupplier.Count > 0)
                {
                    lblsupplierid.Text = getlstsupplier[0].Supplierid.ToString();
                    txtsupplier.Text = getlstsupplier[0].SupplierName;
                }
            }
            else if (lstsupplier.Count > 0)
            {
                dgvsupplier.Rows.Clear();
                foreach (BOLSupplier s in lstsupplier)
                {
                    dgvsupplier.Rows.Add(s.Supplierid, s.SupplierName, s.Address, s.Phone);

                    if (s.SupplierName == "Auto")
                    {
                        lblsupplierid.Text = s.Supplierid.ToString();
                        txtsupplier.Text = s.SupplierName;
                        dgvsupplier.Visible = false;
                    }
                    else
                    {
                        dgvsupplier.Visible = true;
                    }
                    dgvsupplier.Focus();
                }
            }
            //txtitem.Focus();
        }

       
        private void dgvsupplier_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    lblsupplierid.Text = dgvsupplier.CurrentRow.Cells[0].Value.ToString();
                    txtsupplier.Text = dgvsupplier.CurrentRow.Cells[1].Value.ToString();
                    dgvsupplier.Visible = false;
                    //txtsupplier.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtsupplier_TextChanged(object sender, EventArgs e)
        {
            txtitem.Focus();
        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtgrandtotal.Text = Convert.ToString(Int32.Parse(txtTotal.Text) - (Int32.Parse(txtAdvance.Text) + Int32.Parse(txtAdvance.Text) + Int32.Parse(txtDiscount.Text) + Int32.Parse(txtTotalFOC.Text) + Int32.Parse(txtItemDiscount.Text)));
                    if (txtTotal.Text == "0")
                    {
                        MessageBox.Show("Enter Data to Purchase");
                    }
                    else
                    {
                        btnSave_Click(sender, e);
                    }
                }
                else if (e.KeyCode == Keys.F5)
                {
                    txtgrandtotal.Text = Convert.ToString(Int32.Parse(txtTotal.Text) - (Int32.Parse(txtAdvance.Text) + Int32.Parse(txtDiscount.Text) + Int32.Parse(txtTotalFOC.Text) + Int32.Parse(txtItemDiscount.Text)));
                    if (txtTotal.Text == "0" | lblsupplierid.Text == "0")
                    {
                        MessageBox.Show("Enter Data for Sale");
                    }
                    else
                    {
                        btnSave_Click(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        
        private void btnnew_Click(object sender, EventArgs e)
        {
            try
            {
                CleanTextBox();
                
                string voucherno = DateTime.Now.ToString("yyyyMMdd");
                string sysVoucherNo = voucherno + daltrans.GetTransitionID("Purchase");
                txtsystemvoucherno.Text = sysVoucherNo;
                lblTransactionID.Text=daltrans.GetTransitionID("Purchase").ToString();

                BOLTransition boltrans = new BOLTransition();
                boltrans.TransID = daltrans.GetTransitionID("Purchase");
                daltrans.SaveTransition(boltrans);

                LoadTemp();
                dgvpurchase.Focus();
                dgvpurchase.Rows[0].Cells[0].ReadOnly = true;
                dgvpurchase.Rows[0].Cells[1].Selected = true;
                txtitem.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnpreviewvoucher_Click(object sender, EventArgs e)
        {
            try
            {
                List<BOLPurchase> lst = new List<BOLPurchase>();
                for (int i = 0; i < dgvpurchase.Rows.Count-1; i++)
                {
                    BOLPurchase bolpurchasepreview = new BOLPurchase();
                    bolpurchasepreview.Purchaseid = long.Parse(lblpurid.Text);
                    bolpurchasepreview.SupplierName = txtsupplier.Text;
                    if (txtVoucherNo.Text == "")
                    {
                        bolpurchasepreview.Voucherno = txtsystemvoucherno.Text;
                    }
                    else
                    {
                        bolpurchasepreview.Voucherno = txtVoucherNo.Text;
                    }
                    bolpurchasepreview.Total = Decimal.Parse(dgvpurchase.Rows[i].Cells[6].Value.ToString());
                    bolpurchasepreview.Paymenttype = cbopaymentby.Text ;
                    bolpurchasepreview.Currency = cbocurrency.Text;
                    bolpurchasepreview.Itemcode = dgvpurchase.Rows[i].Cells[1].Value.ToString();
                    bolpurchasepreview.Description = dgvpurchase.Rows[i].Cells[2].Value.ToString();
                    bolpurchasepreview.Qty = Int32.Parse(dgvpurchase.Rows[i].Cells[4].Value.ToString());
                    bolpurchasepreview.Purchaseprice = Decimal.Parse(dgvpurchase.Rows[i].Cells[5].Value.ToString());
                    bolpurchasepreview.Itemdiscountpercent = Int32.Parse(dgvpurchase.Rows[i].Cells[9].Value.ToString()) * Int32.Parse(txtDiscount.Text);
                    bolpurchasepreview.Totalamt = Decimal.Parse(txtTotal.Text);
                    bolpurchasepreview.Itemdiscount = Decimal.Parse(dgvpurchase.Rows[i].Cells[9].Value.ToString());
                    bolpurchasepreview.Advance = Decimal.Parse(txtAdvance.Text);
                    bolpurchasepreview.Discount = Decimal.Parse(txtDiscount.Text);
                    bolpurchasepreview.Grandtotal = Decimal.Parse(txtgrandtotal.Text);
                    bolpurchasepreview.Totalfoc = Int32.Parse(txtTotalFOC.Text);
                    bolpurchasepreview.Totalitemdiscount = Decimal.Parse(txtItemDiscount.Text);
                    bolpurchasepreview.Location = cboLocation.Text;

                    lst.Add(bolpurchasepreview);

                }
                if (lst.Count > 0)
                {
                    this.UseWaitCursor = true;
                    ReportDocument cu_Report = new ReportDocument();

                    //ds.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_SaleVoucher.xsd"); 
                    cu_Report.Load(Application.StartupPath + @"\Report\RptPurchaseReport.rpt");
                    cu_Report.SetDataSource(lst);
                    cu_Report.SetDatabaseLogon("sa", "moeyan");

                    List<BOLSystem> lstsystem = new List<BOLSystem>();
                    lstsystem = dalSystem.ShowAllSystem();

                    if (lstsystem.Count > 0)
                    {
                        List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                        lstvoucherSetting = dalVoucher.SelectAllVoucher();

                        DataTable dtt = new DataTable();
                        DataColumn dc = new DataColumn();
                        DataRow dr;
                        dc.ColumnName = "Name";
                        dc.DataType = System.Type.GetType("System.String");
                        dtt.Columns.Add(dc);

                        DataColumn dc1 = new DataColumn();
                        dc1.ColumnName = "Logo";
                        dc1.DataType = System.Type.GetType("System.Byte[]");
                        dtt.Columns.Add(dc1);

                        if (lstvoucherSetting.Count > 0)
                        {
                            for (int i = 0; i < lstvoucherSetting.Count; i++)
                            {
                                dr = dtt.NewRow();
                                dr["Name"] = lstvoucherSetting[0].Name;
                                dr["Logo"] = lstvoucherSetting[0].Logo;
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
            //    dts = dalpurchase.SelectPurchaseVoucher(long.Parse(txtsystemvoucherno.Text), 1);
                
            //    if (dts.Tables[0].Rows.Count > 0)
            //    {
            //        //ReportDocument l_Report = new ReportDocument();

            //        dts.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_Purchase.xsd");
            //        l_Report.Load(Application.StartupPath + @"\Report\RptPurchaseReport.rpt");

            //        DataSet dt = new DataSet();

                    
            //            int purchaseid = Int32.Parse(lblpurid.Text);
            //            dt = dalpurchase.SelectItemDiscount(purchaseid);
            //            if (dt.Tables[0].Rows.Count > 0)
            //           {
            //               l_Report.DataDefinition.FormulaFields[1].Text = "Replace(cstr(" + dt.Tables[0].Rows[0].ItemArray[0].ToString() + "),'.00','')";
            //           }
            //            else
            //            {
            //                l_Report.DataDefinition.FormulaFields[1].Text = "Replace(cstr(00),'.00','')";
            //           }
     


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
            //        this.UseWaitCursor = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
        }
        //private void btnPreview_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.UseWaitCursor = true;
        //        ReportDocument l_Report = new ReportDocument();
        //        DataSet dts = new DataSet();
        //        dts = dalpurchase.PurchaseItemTotal();

        //        if (dts.Tables[0].Rows.Count > 0)
        //        {                   
                    
        //            dts.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_Item.xsd");
        //            l_Report.Load(Application.StartupPath + @"\Report\RptPurchaseItem.rpt");

        //            l_Report.SetDataSource(dts);
        //            l_Report.SetDatabaseLogon("sa", "moeyan");

        //            frmReport frmReport = new frmReport();
        //            frmReport.rptViewer.ReportSource = l_Report;
        //            frmReport.rptViewer.Refresh();
        //            frmReport.Show();
        //            this.UseWaitCursor = false;
        //        }
        //        else
        //        {
        //            MessageBox.Show("No data for Preview");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message.ToString());
        //    }
        //}
        private void cbocurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int currencyid = Int32.Parse(cbocurrency.SelectedValue.ToString());
                BOLExchange exchange = new BOLExchange();
                exchange = dalexchange.GetExchangeRate(currencyid);
                lblexchangerateid.Text = currencyid.ToString();
                txtex.Text = exchange.Exchangerate.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtDrawingTimes_TextChanged(object sender, EventArgs e)
        {            
                try
                {
                    Int32.Parse(txtDrawingTimes.Text);
                }
                catch
                {
                    MessageBox.Show("Enter Valid Number");
                }           

        }

        private void txtitem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    List<BOLStock> lststk = new List<BOLStock>();
                    lststk = dalstock.SelectStock(txtitem.Text, 0, "", 0);
                    dgvItemCode.Rows.Clear();
                    if (lststk.Count > 0)
                    {
                        for (int i = 0; i < lststk.Count; i++)
                        {
                            dgvItemCode.Rows.Add(lststk[i].Id, lststk[i].ItemCode, lststk[i].NameMM, lststk[i].NameMM, lststk[i].PurchasePrice);
                        }
                        dgvItemCode.Visible = true;
                        //dgvItemCode.Focus();
                    }
                    dgvItemCode.Focus();
                }
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

        //private void dgvpurchase_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        //{

        //}
        //private void tb_KeyPress(Object sender, EventArgs e)
        //{      
        //    if (Keys.Enter == Keys.Enter)
        //    {
        //        int row = dgvpurchase.CurrentCell.RowIndex;
        //        int col = dgvpurchase.CurrentCell.ColumnIndex;
        //        int currentcell = Int32.Parse(dgvpurchase.CurrentCell.Value.ToString());
        //        if (tb.Text != "0")
        //        {
        //            dgvpurchase.CurrentCell = dgvpurchase[col + 1, row];
        //        }
        //    }
        //}

        private void dgvpurchase_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //int row = dgvpurchase.CurrentCell.RowIndex;
            //int col = dgvpurchase.CurrentCell.ColumnIndex;


            //int qty = Int32.Parse(dgvpurchase.CurrentCell.Value.ToString());
            //for (int i = 0; i < dgvpurchase.Rows.Count - 1; i++)
            //{
            //    if (dgvpurchase.CurrentCell.ColumnIndex == 4)
            //    {
            //        dgvpurchase.CurrentCell = dgvpurchase[col + 1, row];
            //    }
            //}

           
        }


        //private void dgvpurchase_CellEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.ColumnIndex == 4)
        //    {
        //        dgvpurchase.BeginEdit(true);
        //        if (dgvpurchase.Rows[e.RowIndex-1].Cells[4].Value != null)
        //        {
        //            dgvpurchase.CurrentCell = dgvpurchase.Rows[e.RowIndex-1].Cells[4];
        //            dgvpurchase.Rows[e.RowIndex-1].Cells[4].Selected = true;
        //        }
        //    }
        //}

        private void dgvpurchase_SizeChanged(object sender, EventArgs e)
        {

        }

        private void dgvpurchase_SelectionChanged(object sender, EventArgs e)
        {
            //DataGridViewCell currentcell = dgvpurchase.CurrentCell;
            //if (currentcell != null)
            //{
            //    int nextrow = currentcell.RowIndex-1;
            //    int nextcol = currentcell.ColumnIndex + 1;

            //    if (nextcol > dgvpurchase.ColumnCount)
            //    {
            //        nextcol = 0;
            //        nextrow++;
            //    }
            //    if (nextrow > dgvpurchase.RowCount)
            //    {
            //        nextrow = 0;
            //    }
            //    DataGridViewCell nextcell = dgvpurchase.Rows[nextrow].Cells[nextcol-1];

            //    if (nextcell != null && nextcell.Visible)
            //    {
            //        dgvpurchase.CurrentCell = nextcell;
            //        nextcell.Selected = true;
            //    }
            //}
        }

        private void dgvpurchase_TabIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void txtsupplier_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    List<BOLSupplier> lstsupplier = new List<BOLSupplier>();
                    lstsupplier = dalpurchase.GetSupplier(txtsupplier.Text);

                    //txtsupplier.Text = "Auto";

                    if (lstsupplier.Count == 0 & txtsupplier.Text == "Auto")
                    {
                        DALPurchase dalpur = new DALPurchase();
                        BOLSupplier bolsupplier = new BOLSupplier();
                        bolsupplier.SupplierName = "Auto";

                        dalsupplier.SaveSupplier(bolsupplier);

                        List<BOLSupplier> getlstsupplier = new List<BOLSupplier>();
                        getlstsupplier = dalsupplier.GetSupplier(txtsupplier.Text);

                        if (getlstsupplier.Count > 0)
                        {
                            lblsupplierid.Text = getlstsupplier[0].Supplierid.ToString();
                            txtsupplier.Text = getlstsupplier[0].SupplierName;
                        }
                        dgvsupplier.Focus();
                    }
                    else if (lstsupplier.Count > 0)
                    {
                        dgvsupplier.Rows.Clear();
                        foreach (BOLSupplier s in lstsupplier)
                        {
                            dgvsupplier.Rows.Add(s.Supplierid, s.SupplierName, s.Address, s.Phone);

                            if (s.SupplierName == "Auto")
                            {
                                lblsupplierid.Text = s.Supplierid.ToString();
                                txtsupplier.Text = s.SupplierName;
                                dgvsupplier.Visible = false;
                            }
                            else
                            {
                                dgvsupplier.Visible = true;
                            }
                        }
                        bool isCrdite = false;
                        isCrdite = dalsupplier.ChkPaymentType(Int32.Parse(lblsupplierid.Text));
                        if (isCrdite)
                        {
                            cbopaymentby.Enabled = true;
                        }
                        else
                        {
                            cbopaymentby.Enabled = false;
                        }
                        dgvsupplier.Focus();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvpurchase_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //int icolumn=dgvpurchase.CurrentCell.ColumnIndex;
                //int irow=dgvpurchase.CurrentCell.RowIndex;

                if (dgvpurchase.CurrentCell.ColumnIndex == 1)
                {
                    //if (dgvpurchase.Rows[dgvpurchase.Rows.Count - 2].Cells[1].Value.ToString() == "0000")
                    if (dgvpurchase.Rows.Count == 0) // added by KSAung
                    {
                        if (cbopaymentby.Text == "Cash")
                        {
                            txtAdvance.Enabled = false;
                            txtDiscount.Focus();
                        }
                        else if (cbopaymentby.Text == "Credit")
                        {
                            txtAdvance.Enabled = true;
                            txtAdvance.Focus();
                        }
                    }
                    else
                    {
                        List<BOLStock> lststk = new List<BOLStock>();
                        lststk = dalstock.SelectStock("", 0, dgvpurchase.CurrentRow.Cells[1].Value.ToString(), 0);
                        if (lststk.Count > 0)
                        {
                            dgvpurchase.CurrentRow.Cells[2].Value = lststk[0].NameMM;
                            dgvpurchase.CurrentRow.Cells[5].Value = lststk[0].PurchasePrice;
                           // dgvpurchase.CurrentCell = dgvpurchase.CurrentRow.Cells[4];
                            dgvpurchase.CurrentCell = dgvpurchase[dgvpurchase.CurrentCell.ColumnIndex + 3, dgvpurchase.CurrentCell.RowIndex];
                            //dgvpurchase.CurrentCell = dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[4];
                        }
                        else
                        {
                            MessageBox.Show("Enter correct Item Code!");
                            txtitem.Text = dgvpurchase.CurrentRow.Cells[1].Value.ToString();
                            txtitem.Focus();
                        }
                    }
                }
                else if (dgvpurchase.CurrentCell.ColumnIndex == 2)
                {
                    dgvpurchase.CurrentCell = dgvpurchase.CurrentRow.Cells[4]; // dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[4];
                }

                else if (dgvpurchase.CurrentCell.ColumnIndex == 4)
                {
                    string qtyCheck = "";
                    qtyCheck = MoeYanPOS.Function.Validation.isNumberField("Qty", dgvpurchase.CurrentRow.Cells[4].Value.ToString());

                    if (qtyCheck != "")
                    {
                        MessageBox.Show(qtyCheck);
                        dgvpurchase.CurrentRow.Cells[4].Value = 1;
                    }
                    //dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[6].Value = Convert.ToString(Int32.Parse(dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[4].Value.ToString()) * Int32.Parse(dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[5].Value.ToString()));
                    dgvpurchase.CurrentRow.Cells[6].Value = Convert.ToString(Int32.Parse(dgvpurchase.CurrentRow.Cells[4].Value.ToString()) * Int32.Parse(dgvpurchase.CurrentRow.Cells[5].Value.ToString()));
                    dgvpurchase.CurrentCell = dgvpurchase.CurrentRow.Cells[5];
                }
                else if (dgvpurchase.CurrentCell.ColumnIndex == 5)
                {
                    //add by kyawsithu
                    dgvpurchase.CurrentRow.Cells[6].Value = Convert.ToString(Int32.Parse(dgvpurchase.CurrentRow.Cells[4].Value.ToString()) * Int32.Parse(dgvpurchase.CurrentRow.Cells[5].Value.ToString()));
                    // dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[6].Value = Convert.ToString(Int32.Parse(dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[4].Value.ToString()) * Int32.Parse(dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[5].Value.ToString()));

                    int totalprice = 0;
                    for (int i = 0; i < dgvpurchase.Rows.Count; i++)
                    {
                        if (dgvpurchase.Rows[i].Cells[7].Value.ToString() == "False")
                        {
                            totalprice += Int32.Parse(dgvpurchase.Rows[i].Cells[6].Value.ToString());
                        }
                    }
                    txtTotal.Text = totalprice.ToString();
                    dgvpurchase.CurrentCell = dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[6];
                }
                else if (dgvpurchase.CurrentCell.ColumnIndex == 6)
                {
                    dgvpurchase.CurrentCell = dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[7];
                }
                else if (dgvpurchase.CurrentCell.ColumnIndex == 7)
                {
                    if (dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells["colFOC"].Value.ToString() == "True")
                    {
                        dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[8].ReadOnly = true;
                        dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[9].ReadOnly = true;
                    }
                    else
                    {
                        dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[8].ReadOnly = false;
                        dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[9].ReadOnly = false;
                    }
                    dgvpurchase.CurrentCell = dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[8];
                }
                else if (dgvpurchase.CurrentCell.ColumnIndex == 8)
                {
                    string DisCheck = "";
                    DisCheck = MoeYanPOS.Function.Validation.isNumberField("Item Discount%", dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[8].Value.ToString());
                    if (DisCheck != "")
                    {
                        MessageBox.Show(DisCheck);
                        dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[9].Value = "0";
                    }
                    else
                    {
                        int dispercent = 0;
                        dispercent = (Int32.Parse(dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[8].Value.ToString()) * Int32.Parse(dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[6].Value.ToString())) / 100;
                        dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[9].Value = dispercent.ToString();
                    }
                    dgvpurchase.CurrentCell = dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[9];
                }
                else if (dgvpurchase.CurrentCell.ColumnIndex != 9)
                {
                    dgvpurchase.CurrentCell = dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[dgvpurchase.CurrentCell.ColumnIndex + 1];
                }
                else
                {
                    string DisCheck = "";
                    DisCheck = MoeYanPOS.Function.Validation.isNumberField("Item Discount", dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[9].Value.ToString());

                    if (DisCheck != "")
                    {
                        MessageBox.Show(DisCheck);
                        dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[9].Value = 0;
                    }
                    else
                    {
                        dgvpurchase.Rows.Add();
                        dgvpurchase.CurrentCell = dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[0];
                        dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[0].Value = dgvpurchase.Rows.Count;
                        dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[1].Value = "0000";
                        dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[4].Value = "1";
                        dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[5].Value = "0";
                        dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[6].Value = "0";
                        dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[8].Value = "0";
                        dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[9].Value = "0";
                        dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[7].Value = colFOC.Items[1].ToString();
                        txtitem.Focus();

                        decimal TotalDis = 0; decimal TotalFOC = 0; decimal TotalAmt = 0;

                        for (int i = 0; i < dgvpurchase.Rows.Count; i++)
                        {
                            TotalDis += Decimal.Parse(dgvpurchase.Rows[i].Cells[9].Value.ToString());
                        }

                        for (int i = 0; i < dgvpurchase.Rows.Count; i++)
                        {
                            if (dgvpurchase.Rows[i].Cells[7].Value.ToString() == "True")
                            {
                                TotalAmt += Decimal.Parse(dgvpurchase.Rows[i].Cells[6].Value.ToString());
                            }
                        }
                        for (int i = 0; i < dgvpurchase.Rows.Count; i++)
                        {
                            if (dgvpurchase.Rows[i].Cells[7].Value.ToString() == "True")
                            {
                                TotalFOC += Decimal.Parse(dgvpurchase.Rows[i].Cells[6].Value.ToString());
                            }
                        }

                        txtTotal.Text = TotalAmt.ToString();
                        txtTotalFOC.Text = TotalFOC.ToString();
                        txtItemDiscount.Text = TotalDis.ToString();
                        txtgrandtotal.Text = Convert.ToString(Int32.Parse(txtTotal.Text) - (Int32.Parse(txtAdvance.Text) + Int32.Parse(txtDiscount.Text) + Int32.Parse(txtTotalFOC.Text) + Int32.Parse(txtItemDiscount.Text)));
                        // dgvpurchase.CurrentCell = dgvpurchase.Rows[dgvpurchase.Rows.Count - 1].Cells[1];

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }            
        }

        private void dgvpurchase_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvItemCode_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtppurchasedate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtDrawingTimes_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtitem.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dtppurchasedate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtDrawingTimes.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtVoucherNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnA4Print_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave_Click(sender, e);
                MoeYanPOS.Report.RptPurchaseReport rpt = new MoeYanPOS.Report.RptPurchaseReport();
                //this.UseWaitCursor = true;
                ReportDocument l_Report = new ReportDocument();
                DataSet ds = new DataSet();
                //ds = dalsale.SelectSaleVoucher(long.Parse(lblSaleID.Text), 0, ""); remove by ksaung
                ds = dalpurchase.SelectPurchaseVoucher2(SysVoucherNo); //add by ksaung

                if (ds.Tables[0].Rows.Count > 0)
                {

                    ds.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_Purchase.xsd");
                    l_Report.Load(Application.StartupPath + @"\Report\RptPurchaseReport.rpt");

                    l_Report.SetDataSource(ds.Tables[0]);
                    l_Report.SetDatabaseLogon("sa", "moeyan");

                    List<BOLSystem> lstsystem = new List<BOLSystem>();
                    lstsystem = dalSystem.ShowAllSystem();

                    if (lstsystem.Count > 0)
                    {
                        List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                        lstvoucherSetting = dalVoucher.SelectAllVoucher();

                        DataTable dtt = new DataTable();
                        DataColumn dc = new DataColumn();
                        DataRow dr;
                        dc.ColumnName = "Name";
                        dc.DataType = System.Type.GetType("System.String");
                        dtt.Columns.Add(dc);

                        DataColumn dc1 = new DataColumn();
                        dc1.ColumnName = "Logo";
                        dc1.DataType = System.Type.GetType("System.Byte[]");
                        dtt.Columns.Add(dc1);

                        if (lstvoucherSetting.Count > 0)
                        {
                            for (int i = 0; i < lstvoucherSetting.Count; i++)
                            {
                                dr = dtt.NewRow();
                                dr["Name"] = lstvoucherSetting[0].Name;
                                dr["Logo"] = lstvoucherSetting[0].Logo;
                                dtt.Rows.Add(dr);
                            }

                            l_Report.Subreports[0].SetDataSource(dtt);
                        }

                        l_Report.PrintOptions.PrinterName = lstsystem[0].Printervoucher;
                        l_Report.PrintToPrinter(1, false, 0, 0);
                    }
                    else
                    {
                        MessageBox.Show("Please put printer name at System Setting.");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("No data to print");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtVoucherNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //txtitem.Focus();
                    dgvpurchase.Focus();
                    //dgvpurchase.Rows[0].Cells[1].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtsystemvoucherno_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtsystemvoucherno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtitem.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void pnlSaleEntry_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
