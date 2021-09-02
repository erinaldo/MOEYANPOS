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
using MoeYanPOS.UI;


namespace MoeYanPOS
{
    public partial class frmSale : Form
    {
        BOLUser LstCheckPrintAndMsgBox = new BOLUser();
        int Total = 0; int TotalFOC = 0; decimal TotalItemDiscount = 0; DateTime SaleDate; DateTime LotteryDate; string LocationNo; string SysVoucherNo; string Remark;        
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
        public frmSale()
        {
            try
            {
                InitializeComponent();                
                SetFormLoad();

                string voucherno = DateTime.Now.ToString("yyMMdd");
                string currentloc = dalLocation.GetCurrentLocationCode();
                string voucher = "INV-" + currentloc + MoeYanFunctions.MoeYanPOS_Helper.counterCode + DateTime.Now.ToString("yyMMdd") + daltrans.GetMaxVoucher("sale").ToString();
                txtSystemVoucherNo.Text = voucher;
                txtVoucherNo.Text = voucher;
                ////Save Transition
                BOLTransition boltrans = new BOLTransition();
                boltrans.TransName = "Sale";
                boltrans.TransID = daltrans.GetTransitionID("Sale");
                daltrans.SaveTransition(boltrans);

               // txtCustomer.Text = "Auto";
                DALCustomer dalcust = new DALCustomer();
                List<BOLCustomer> getlstcustomer = new List<BOLCustomer>();
                getlstcustomer = dalcustomer.GetCustomer("Auto", "", 0);
                if (getlstcustomer.Count > 0)
                {
                    //lblCustomerID.Text = getlstcustomer[0].ID.ToString();
                    //txtCustomer.Text = getlstcustomer[0].CustomerID; 
                    //dgvCustomer.Visible = false;
               }
                    else
                    {
                        BOLCustomer bolcustomer = new BOLCustomer();
                        bolcustomer.ID = 0;
                        bolcustomer.Name = "Auto";
                        bolcustomer.CustomerID = "0000";
                        bolcustomer.Memberid = "0";
                        bolcustomer.Address ="";
                        bolcustomer.Phone = "";
                        bolcustomer.Dateofbirth = DateTime.Now;
                        bolcustomer.Joindate = DateTime.Now;
                        bolcustomer.Currentdate = DateTime.Now;
                        bolcustomer.Nrc = "";
                        bolcustomer.Email = "";
                        bolcustomer.Customerlevel ="";
                        bolcustomer.Township = 1;
                        bolcustomer.Divisionid = 1;
                        bolcustomer.Creditlimit =0;
                        bolcustomer.Iscash = true;
                        bolcustomer.Iscredit = false;
                        bolcustomer.Shop = "";
                        bolcustomer.SortingID = 1;
                        bolcustomer.Saletarget = 0;
                        bolcustomer.DepositAmount =0;
                        bolcustomer.Contactperson = "";
                        bolcustomer.Departmentid = 1;   
                        dalcust.SaveCustomer(bolcustomer);

                        List<BOLCustomer> getcustomer = new List<BOLCustomer>();
                        getcustomer = dalcustomer.GetCustomer("Auto", "", 0);
                        if (getcustomer.Count > 0 & chkwholesale.Checked != true)
                        {
                            //lblCustomerID.Text = getcustomer[0].ID.ToString();
                            //txtCustomer.Text = getcustomer[0].CustomerID;
                            //dgvCustomer.Visible = false;
                        }
                    }

                dgvSale.Rows.Add();
                dgvSale.Rows[0].Cells[0].Value = 1;
                dgvSale.Rows[0].Cells[1].Value = "00001";
                dgvSale.Rows[0].Cells[2].Value = "";
                dgvSale.Rows[0].Cells[3].Value = "";
                dgvSale.Rows[0].Cells[4].Value = "1";
                dgvSale.Rows[0].Cells[5].Value = "0";
                dgvSale.Rows[0].Cells[6].Value = "0";
                dgvSale.Rows[0].Cells[7].Value = colFOC.Items[1].ToString();
                dgvSale.Rows[0].Cells[8].Value = "0";
                dgvSale.Rows[0].Cells[9].Value = "0";
                dgvSale.Rows[0].Cells[10].Value = "";
                dgvSale.Rows[0].Cells[1].Selected = true;
                txtCustomer.Focus();
                //dalsale.DeleteTempRows(0);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public frmSale(long SaleID)
        {            
            try
            {
                //btnNew.Enabled = false;
                //dgvTemp.Enabled = false;
                InitializeComponent();
                btnPreview.Enabled = false;
                btnPreviewVoucher.Enabled = false;
                SetFormLoad();
                List<BOLSale> bolSaleList = new List<BOLSale>();
                bolSaleList = dalsale.GetSaleBySaleID(SaleID, 0, "");
                dgvSale.Rows.Clear();
                if (bolSaleList.Count > 0)
                {
                    lblSaleID.Text = SaleID.ToString();
                    lblUserID.Text = bolSaleList[0].UserID.ToString();
                    BOLCustomer bolcustomer = new BOLCustomer();
                    bolcustomer = dalcustomer.GetCustomerByCustomerID(long.Parse(bolSaleList[0].CustomerID));
                    dtpSaleDate.Text = bolSaleList[0].SaleDate.ToString();
                    txtCustomer.Text = bolcustomer.CustomerID;
                    lblCustomerID.Text = bolSaleList[0].CustomerID;
                    txtVoucherNo.Text = bolSaleList[0].VoucherNo.ToString();
                    cboPaymentType.Text = bolSaleList[0].PaymentType.ToString();
                    txtDrawingTimes.Text = bolSaleList[0].DrawingTimes.ToString();
                    cbocurrency.SelectedValue = Int32.Parse(bolSaleList[0].CurrencyID.ToString());
                    txtTotalAmt.Text = bolSaleList[0].TotalAmt.ToString();
                    txtAdvance.Text = bolSaleList[0].Advance.ToString();
                    txtItemDiscount.Text = bolSaleList[0].TotalitemDiscount.ToString();
                    txtDiscount.Text = bolSaleList[0].Discount.ToString();
                    txtGrandTotal.Text = bolSaleList[0].GrandTotal.ToString();
                    txtSystemVoucherNo.Text = bolSaleList[0].SystemVoucherNo.ToString();
                    txtTotalFOC.Text = bolSaleList[0].TotalFOC.ToString();
                    txtTransportationAmt.Text = bolSaleList[0].TransportationAmt.ToString();
                    txtRemark.Text = bolSaleList[0].Remark.ToString();
                    txtLotteryNo.Text = bolSaleList[0].LotteryNo.ToString();
                    dtpLotteryDate.Text = bolSaleList[0].LotteryDate.ToString();
                    txtDayLimit.Text = bolSaleList[0].DayLimit.ToString();
                    txtExchangeRate.Text = bolSaleList[0].ExchangeRate.ToString();
                    dgvCustomer.Visible = false;
                    dgvSale.Rows.Clear();
                    btnSave.Text = "&Update";
                    //if (dgvSale.CurrentRow != null)
                    //{

                    List<BOLSale> lstsaledetail = new List<BOLSale>();
                    lstsaledetail = dalsale.GetSaleDetailList(SaleID, 0);
                    dgvSale.Rows.Clear();

                    if (lstsaledetail.Count > 0)
                    {
                        for (int i = 0; i < lstsaledetail.Count; i++)
                        {
                            dgvSale.Rows.Add();
                            dgvSale.Rows[i].Cells[0].Value = i + 1;
                            dgvSale.Rows[i].Cells[1].Value = lstsaledetail[i].ItemCode.ToString();
                            dgvSale.Rows[i].Cells[2].Value = lstsaledetail[i].Description.ToString();
                            dgvSale.Rows[i].Cells[3].Value = lstsaledetail[i].Mtype.ToString();
                            dgvSale.Rows[i].Cells[4].Value = lstsaledetail[i].Qty.ToString();
                            dgvSale.Rows[i].Cells[5].Value = lstsaledetail[i].SalePrice.ToString();
                            dgvSale.Rows[i].Cells[6].Value = lstsaledetail[i].Total.ToString();
                            dgvSale.Rows[i].Cells[7].Value = lstsaledetail[i].FOC.ToString();
                            dgvSale.Rows[i].Cells[8].Value = lstsaledetail[i].ItemDiscountPercent.ToString();
                            dgvSale.Rows[i].Cells[9].Value = lstsaledetail[i].ItemDiscount.ToString();
                            dgvSale.Rows[i].Cells[10].Value = lstsaledetail[i].Charge.ToString();
                            dgvSale.Rows[i].Cells[11].Value = lstsaledetail[i].SaleDetailID.ToString();

                        }                       
                        
                       // dgvSale.Rows.Add(dgvSale.RowCount + 1, "0000", "", "", "1", "0", 1, colFOC.Items[1].ToString(), "0", "0", "");
                    }
                }
                
                 //dalsale.DeleteTempRows(0);
                } 
                    
                
            //}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void SetFormLoad()
        {
            try
            {
                
                //Set form components
                this.Width = Screen.PrimaryScreen.Bounds.Width;
                this.Height = Screen.PrimaryScreen.Bounds.Height;
                this.dgvSale.Width = this.Width;// - (dgvTemp.Width + 200)
                var point = new Point(((this.Width / 2) - (dgvSale.Width) / 2) + 10, dgvTemp.Location.Y);
                this.dgvSale.Location = point;
                var pnlheader = new Point((this.Width / 2) - (pnlHeader.Width) / 2, 39);
                this.pnlHeader.Location = pnlheader;
                this.pnlHeader.Width = this.Width;
                this.pnlSaleEntry.Width = this.Width;
                var pnlfooter = new Point(0, pnlHeader.Height+dgvSale.Height+50);//(this.Width / 2) - (pnlFooter.Width) / 2
                this.pnlFooter.Location = pnlfooter;
                this.pnlFooter.Width = this.Width;
                pnlFooter.Height = this.Height - (pnlHeader.Height + dgvSale.Height + 50);

                var pnlCloseBtn = new Point(this.Width-34, picClose1.Location.Y);
                picClose1.Location = pnlCloseBtn;


                //Set Lottery Date 
                string sm = dtpLotteryDate.Value.Month.ToString().Length > 1 ? dtpLotteryDate.Value.Month.ToString() : "0" + dtpLotteryDate.Value.Month.ToString();
                string sd = dtpLotteryDate.Value.Day.ToString().Length > 1 ? dtpLotteryDate.Value.Day.ToString() : "0" + dtpLotteryDate.Value.Day.ToString();
                string sdate = dtpLotteryDate.Value.Year.ToString() + "-" + sm + "-" + sd + " 00:00:00.000";
                LotteryDate = DateTime.Parse(sdate);                

                //Get Currency
                List<BOLCurrency> lstCurrency = new List<BOLCurrency>();
                lstCurrency = dalexchangerate.FillCurrency();
               if (lstCurrency.Count > 0)
                {
                    cbocurrency.DisplayMember = "Currency";
                    cbocurrency.ValueMember = "Id";
                    cbocurrency.DataSource = lstCurrency;
                    cbocurrency.SelectedIndex = 0;   
                }
                cboPaymentType.SelectedIndex = 0;

                //Get Measurement
                //List<BOLMeasurement> lstmeasurement = new List<BOLMeasurement>();
                //lstmeasurement = dalmeasurement.SelectAllMeasurement();

                //Remove By KSAung
                //if (lstmeasurement.Count > 0)
                //{
                //    colType.DisplayMember = "Id";
                //    colType.ValueMember = "MBCMeasurementID";//"Measurement";
                //    colType.DataSource = lstmeasurement;
                //}
                //else
                //{
                //    colType.Items.Add("Pcs");
                //}

                List<BolLocation> LstLocation = new List<BolLocation>();
                LstLocation = dalLocation.SelectAllLocation();
                cboLocation.DisplayMember = "Location";
                cboLocation.ValueMember = "ID";
                cboLocation.DataSource=LstLocation;
                for (int i = 0; i < LstLocation.Count; i++)
                {
                    if(LstLocation[i].IsThisLocation)
                    {
                        cboLocation.SelectedValue = LstLocation[i].ID;
                        LocationNo = dalLocation.GetLocationByID(LstLocation[i].ID).LocationNo;
                    }
                }
                //userID
                lblUserID.Text = frmMain.UserID.ToString();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmSale_Load(object sender, EventArgs e)
        {
            try
            {
                string voucherno = DateTime.Now.ToString("yyMMdd");
                txtCounter.Text = MoeYanFunctions.MoeYanPOS_Helper.counterName;
                if (btnSave.Text == "&Update")
                {
                    txtVoucherNo.Enabled = false;
                }

                //txtSystemVoucherNo.Text = LocationNo+ voucherno + daltrans.GetTransitionID("Sale").ToString();
                //txtSystemVoucherNo.Text = cboLocation.Text + "_" +voucherno + daltrans.GetTransitionID("Sale").ToString();
                //lblTransID.Text = daltrans.GetTransitionID("Sale").ToString();

            //    LstCheckPrintAndMsgBox = dalsale.CheckIsSavePrint(frmMain.UserID);
            //    dgvTemp.Rows.Clear();
            //    if (lblSaleID.Text == "0")
            //    {
            //        LoadTemp(); 
            //        if(dgvTemp.Rows.Count>0)
            //        {
            //            dgvTemp.Rows[0].Selected = true;
            //            long Tempsaleid = 0;
            //            List<BOLSale> bolSaleList = new List<BOLSale>();
            //            bolSaleList = dalsale.GetSaleBySaleID(0, 1, dgvTemp.Rows[0].Cells[2].Value.ToString());
            //            dgvSale.Rows.Clear();

            //            if (bolSaleList.Count > 0)
            //            {
            //                lblSaleID.Text = "0";
            //                lblTransID.Text = bolSaleList[0].TranSaleID.ToString();
            //                Tempsaleid = bolSaleList[0].SaleID;
            //                txtSystemVoucherNo.Text = bolSaleList[0].SystemVoucherNo.ToString();
            //                lblUserID.Text = bolSaleList[0].UserID.ToString();
            //                txtCustomer.Text = bolSaleList[0].CustomerID.ToString();
            //                txtCustomerName.Text = bolSaleList[0].CustomerName.ToString();
            //                lblCustomerID.Text = bolSaleList[0].CustomerID;
            //                txtVoucherNo.Text = bolSaleList[0].VoucherNo.ToString();
            //                cboPaymentType.Text = bolSaleList[0].PaymentType.ToString();
            //                cbocurrency.SelectedValue = Int32.Parse(bolSaleList[0].CurrencyID.ToString());
            //                txtTotalAmt.Text = bolSaleList[0].TotalAmt.ToString();
            //                txtAdvance.Text = bolSaleList[0].Advance.ToString();
            //                txtDiscount.Text = bolSaleList[0].Discount.ToString();
            //                txtGrandTotal.Text = bolSaleList[0].GrandTotal.ToString();
            //                txtTotalFOC.Text = bolSaleList[0].TotalFOC.ToString();
            //                txtItemDiscount.Text = bolSaleList[0].TotalitemDiscount.ToString();
            //                dgvCustomer.Visible = false;
            //                //dgvSale.Rows.Clear();
            //                //btnSave.Text = "Save";
            //                //if (dgvSale.CurrentRow != null)
            //                //{

            //                List<BOLSale> lstsaledetail = new List<BOLSale>();
            //                lstsaledetail = dalsale.GetSaleDetailList(Tempsaleid, 1);
            //                //dgvSale.Rows.Clear();

            //                if (lstsaledetail.Count > 0)
            //                {
            //                    //dgvSale.Rows.Clear();
            //                    for (int i = 0; i < lstsaledetail.Count; i++)
            //                    {
            //                        dgvSale.Rows.Add(dgvSale.Rows.Count + 1, "0000", "", colType.Items[0].ToString(), "1", "0", "0", colFOC.Items[1].ToString(), "0", "0", "");
            //                        dgvSale.Rows[i].Cells[0].Value = i + 1;
            //                        dgvSale.Rows[i].Cells[1].Value = lstsaledetail[i].ItemCode.ToString();
            //                        dgvSale.Rows[i].Cells[2].Value = lstsaledetail[i].Description.ToString();
            //                        dgvSale.Rows[i].Cells[3].Value = lstsaledetail[i].Mtype.ToString();
            //                        dgvSale.Rows[i].Cells[4].Value = lstsaledetail[i].Qty.ToString();
            //                        dgvSale.Rows[i].Cells[5].Value = lstsaledetail[i].SalePrice.ToString();
            //                        dgvSale.Rows[i].Cells[6].Value = lstsaledetail[i].Total.ToString();
            //                        dgvSale.Rows[i].Cells[7].Value = lstsaledetail[i].FOC.ToString();
            //                        dgvSale.Rows[i].Cells[8].Value = lstsaledetail[i].ItemDiscountPercent.ToString();
            //                        dgvSale.Rows[i].Cells[9].Value = lstsaledetail[i].ItemDiscount.ToString();
            //                        //dgvSale.Rows[i].Cells[10].Value = lstsaledetail[i].SaleDetailID.ToString();
            //                    }

            //                }
            //                dgvSale.Rows.Add(dgvSale.Rows.Count + 1, "0000", "", colType.Items[0].ToString(), "1", "0", "0", colFOC.Items[1].ToString(), "0", "0", "");
            //                dgvSale.CurrentRow.Cells[1].Selected = true;
            //            }
            //        }
            //    }
            //    //dgvTemp.Rows[0].Cells[1].Selected = true;
            //    dgvSale.Focus();    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void LoadTemp()
        {
            try
            {
               dgvTemp.Rows.Clear();
               List<BOLSale> lstsalelist = new List<BOLSale>();
                lstsalelist = dalsale.GetTempData();
                if (lstsalelist.Count > 0)
                {
                    for (int i = 0; i < lstsalelist.Count; i++)
                    {
                        txtSystemVoucherNo.Text = lstsalelist[i].SystemVoucherNo.ToString();
                        if (lstsalelist[i].VoucherNo == "")
                        {
                            dgvTemp.Rows.Add(i + 1, lstsalelist[i].SystemVoucherNo, lstsalelist[i].SystemVoucherNo,"");
                            
                        }
                        else
                        {
                            dgvTemp.Rows.Add(i + 1, lstsalelist[i].VoucherNo, lstsalelist[i].SystemVoucherNo, "");
                        }

                    }
                }
                else
                {
                    string voucherno = DateTime.Now.ToString("yyMMdd");
                    txtSystemVoucherNo.Text = "";
                    //txtSystemVoucherNo.Text = "INV-" + dalsale.GetMaxVoucherNo("sale", dtpSaleDate.Value).ToString();
                    //txtVoucherNo.Text = "INV-" + dalsale.GetMaxVoucherNo("sale", dtpSaleDate.Value).ToString();

                    string currentloc = dalLocation.GetCurrentLocationCode();
                    string voucher = "INV-" + currentloc + MoeYanFunctions.MoeYanPOS_Helper.counterCode + DateTime.Now.ToString("yyMMdd") + daltrans.GetMaxVoucher("sale").ToString();
                    txtSystemVoucherNo.Text = voucher;
                    txtVoucherNo.Text = voucher;
                    //txtSystemVoucherNo.Text = LocationNo + voucherno + dalsale.GetMaxVoucherNo(dtpSaleDate.Value).ToString();
                   // txtSystemVoucherNo.Text = cboLocation.Text + "_" + voucherno + daltrans.GetTransitionID("Sale").ToString();
                    //txtVoucherNo.Text = cboLocation.Text+ voucherno + daltrans.GetTransitionID("Sale").ToString(); 
                    lblTransID.Text = daltrans.GetTransitionID("Sale").ToString();
                    //Save Transition
                    BOLTransition boltrans = new BOLTransition();
                    boltrans.TransName = "Sale";
                    boltrans.TransID = daltrans.GetTransitionID("Sale");
                    daltrans.SaveTransition(boltrans);

                    saveTampSale();
                    if (txtVoucherNo.Text == "")
                    {
                        dgvTemp.Rows.Add(dgvTemp.Rows.Count + 1, txtVoucherNo.Text, txtSystemVoucherNo.Text);
                    }
                    else
                    {
                        dgvTemp.Rows.Add(dgvTemp.Rows.Count + 1, txtVoucherNo.Text, txtSystemVoucherNo.Text);
                    }
                }
                if (dgvTemp.Rows.Count > 0)
                {
                    dgvTemp.Rows[dgvTemp.Rows.Count - 1].Selected = true;
                    long Tempsaleid = 0;
                    List<BOLSale> bolSaleList = new List<BOLSale>();
                    bolSaleList = dalsale.GetSaleBySaleID(0, 1, dgvTemp.Rows[dgvTemp.Rows.Count - 1].Cells[2].Value.ToString());
                    dgvSale.Rows.Clear();

                    if (bolSaleList.Count > 0)
                    {
                        lblSaleID.Text = "0";
                        lblTransID.Text = bolSaleList[0].TranSaleID.ToString();
                        Tempsaleid = bolSaleList[0].SaleID;
                        txtSystemVoucherNo.Text = bolSaleList[0].SystemVoucherNo.ToString();
                        lblUserID.Text = bolSaleList[0].UserID.ToString();
                        txtCustomer.Text = bolSaleList[0].CustomerID.ToString();
                        txtCustomerName.Text = bolSaleList[0].CustomerName.ToString();
                        lblCustomerID.Text = bolSaleList[0].CustomerID.ToString();
                        txtVoucherNo.Text = bolSaleList[0].VoucherNo.ToString();
                        cboPaymentType.Text = bolSaleList[0].PaymentType.ToString();
                        cbocurrency.SelectedValue = Int32.Parse(bolSaleList[0].CurrencyID.ToString());
                        txtTotalAmt.Text = bolSaleList[0].TotalAmt.ToString();
                        txtAdvance.Text = bolSaleList[0].Advance.ToString();
                        txtDiscount.Text = bolSaleList[0].Discount.ToString();
                        txtGrandTotal.Text = bolSaleList[0].GrandTotal.ToString();
                        txtTotalFOC.Text = bolSaleList[0].TotalFOC.ToString();
                        txtItemDiscount.Text = bolSaleList[0].TotalitemDiscount.ToString();
                        txtRemark.Text = bolSaleList[0].Remark.ToString();
                        dgvCustomer.Visible = false;
                        //dgvSale.Rows.Clear();
                        //btnSave.Text = "Save";
                        //if (dgvSale.CurrentRow != null)
                        //{

                        List<BOLSale> lstsaledetail = new List<BOLSale>();
                        lstsaledetail = dalsale.GetSaleDetailList(Tempsaleid, 1);
                        //dgvSale.Rows.Clear();

                        if (lstsaledetail.Count > 0)
                        {
                            //dgvSale.Rows.Clear();
                            for (int i = 0; i < lstsaledetail.Count; i++)
                            {
                                dgvSale.Rows.Add(dgvSale.Rows.Count + 1, "0000", "", "", "1", "0", "0", colFOC.Items[1].ToString(), "0", "0", "");
                                dgvSale.Rows[i].Cells[0].Value = i + 1;
                                dgvSale.Rows[i].Cells[1].Value = lstsaledetail[i].ItemCode.ToString();
                                dgvSale.Rows[i].Cells[2].Value = lstsaledetail[i].Description.ToString();
                                dgvSale.Rows[i].Cells[3].Value = lstsaledetail[i].Mtype.ToString();
                                dgvSale.Rows[i].Cells[4].Value = lstsaledetail[i].Qty.ToString();
                                dgvSale.Rows[i].Cells[5].Value = lstsaledetail[i].SalePrice.ToString();
                                dgvSale.Rows[i].Cells[6].Value = lstsaledetail[i].Total.ToString();
                                dgvSale.Rows[i].Cells[7].Value = lstsaledetail[i].FOC.ToString();
                                dgvSale.Rows[i].Cells[8].Value = lstsaledetail[i].ItemDiscountPercent.ToString();
                                dgvSale.Rows[i].Cells[9].Value = lstsaledetail[i].ItemDiscount.ToString();
                                //dgvSale.Rows[i].Cells[10].Value = lstsaledetail[i].SaleDetailID.ToString();
                            }

                        }
                        dgvSale.Rows.Add(dgvSale.Rows.Count + 1, "0000", "", "", "1", "0", "0", colFOC.Items[1].ToString(), "0", "0", "");
                        dgvSale.CurrentRow.Cells[1].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void Save_TempTables()        
        {
            try
            {
                //if (dgvSale.Rows.Count > 1)
                //{

                    if (dgvSale.Rows[0].Cells[11].Value == null)
                    {
                        //saveTampSale();
                    }
                    else if (dgvSale.Rows[0].Cells[11].Value.ToString() == "")
                    {
                        //saveTampSale();
                    }

                    BOLSale bolsaledetail = new BOLSale();

                    if (lblSaleID.Text == "0")
                    {
                        //bolsaledetail.SaleID = dalsale.GetMaxSaleID(1,txtSystemVoucherNo.Text);
                    }
                    else
                    {
                        bolsaledetail.SaleID = long.Parse(lblSaleID.Text);
                    }

                    bolsaledetail.No = Int32.Parse(dgvSale.CurrentRow.Cells["colNo"].Value.ToString());
                    bolsaledetail.ItemCode = dgvSale.CurrentRow.Cells["colItemCode"].Value.ToString();
                    int isExist = 0;
                    isExist = dalstock.CheckStock(bolsaledetail.ItemCode);
                    if (isExist == 0 & bolsaledetail.ItemCode != "0000")
                    {
                        MessageBox.Show(" This stock code doesn't exist.");
                        return;
                    }
                    bolsaledetail.Description = dgvSale.CurrentRow.Cells["colDescription"].Value.ToString();
                    bolsaledetail.Mtype = dgvSale.CurrentRow.Cells["colType"].Value.ToString(); //"Pcs";
                    bolsaledetail.Qty = Int32.Parse(dgvSale.CurrentRow.Cells["colQty"].Value.ToString());
                    bolsaledetail.SalePrice = Decimal.Parse(dgvSale.CurrentRow.Cells["colSalePrice"].Value.ToString());
                    bolsaledetail.Total = Decimal.Parse(dgvSale.CurrentRow.Cells["colTotal"].Value.ToString());
                    if (dgvSale.CurrentRow.Cells["colFOC"].Value.ToString() == "True")
                    {
                        bolsaledetail.FOC = true;
                    }
                    else
                    {
                        bolsaledetail.FOC = false;
                    }
                    bolsaledetail.ItemDiscount = Decimal.Parse(dgvSale.CurrentRow.Cells["colItemDiscount"].Value.ToString());
                    bolsaledetail.ItemDiscountPercent = Int32.Parse(dgvSale.CurrentRow.Cells["colItemDiscountPercent"].Value.ToString());
                    bolsaledetail.Action = 1;

                    int chktemp = 0;
                    chktemp = dalsaledetail.ChkIsTempSaleDetailSaved(bolsaledetail.No,frmMain.UserID, txtSystemVoucherNo.Text);
                    if (bolsaledetail.ItemCode != "0000")
                    {
                        if (chktemp == 0)
                        {
                            dalsaledetail.SaveSaleDetailData(bolsaledetail);
                        }
                        else if (chktemp > 0)
                        {
                            bolsaledetail.UserID = frmMain.UserID;
                            bolsaledetail.SystemVoucherNo = txtSystemVoucherNo.Text;
                            dalsaledetail.UpdateSaleDetailData(bolsaledetail);
                        } 
                    }
                                     

                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        
        private void dgvSale_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvSale.SelectedCells[0].ColumnIndex == 1)
                {
                    if (dgvSale.CurrentRow.Cells[1].Value != null)
                    {
                        if (dgvSale.CurrentRow.Cells[1].Value.ToString() == "0000")
                        {
                            if (cboPaymentType.Text == "Cash")
                            {
                                txtAdvance.Enabled = false;
                                txtDiscount.Focus();
                            }
                            else if (cboPaymentType.Text == "Credit")
                            {
                                txtAdvance.Enabled = true;
                                txtAdvance.Focus();
                            }
                        }

                        else
                        {
                            List<BOLStock> lstStk = new List<BOLStock>();
                            lstStk = dalstock.SelectStock("", 0, dgvSale.CurrentRow.Cells[1].Value.ToString(), 0);
                            if (lstStk.Count > 0)
                            {
                                dgvSale.CurrentRow.Cells[2].Value = lstStk[0].NameMM;
                                if (chkwholesale.Checked)
                                {
                                    dgvSale.CurrentRow.Cells[5].Value = lstStk[0].WholeSalePrice;
                                }
                                else
                                {
                                    dgvSale.CurrentRow.Cells[5].Value = lstStk[0].Price;
                                }
                                dgvSale.CurrentCell = dgvSale.CurrentRow.Cells[4];
                            }
                            else
                            {
                                MessageBox.Show("Enter correct Item Code!");
                                //txtItemCode.Text = dgvSale.CurrentRow.Cells[1].Value.ToString();
                                txtItemCode.Focus();
                            }

                        }
                    }
                }

                else if (dgvSale.CurrentCell.ColumnIndex == 2)
                {
                    dgvSale.CurrentCell = dgvSale.Rows[dgvSale.CurrentCell.RowIndex].Cells[4];
                }
                else if (dgvSale.CurrentCell.ColumnIndex == 3)
                {
                    dgvSale.CurrentCell = dgvSale.Rows[dgvSale.CurrentCell.RowIndex].Cells[4];
                }

                else if (dgvSale.CurrentCell.ColumnIndex == 4)
                {
                    string qtyCheck = "";
                    qtyCheck = MoeYanPOS.Function.Validation.isNumberField("Qty", dgvSale.Rows[dgvSale.CurrentCell.RowIndex].Cells[4].Value.ToString());
                    if (qtyCheck != "")
                    {
                        MessageBox.Show(qtyCheck);
                        dgvSale.CurrentRow.Cells[4].Value = 1;
                        return;
                    }
                    int charge = Int32.Parse(dgvSale.CurrentRow.Cells[10].Value.ToString() == "" ? "0" : dgvSale.CurrentRow.Cells[10].Value.ToString());
                    dgvSale.CurrentRow.Cells[6].Value = Convert.ToString((Int32.Parse(dgvSale.CurrentRow.Cells[4].Value.ToString()) * Int32.Parse(dgvSale.CurrentRow.Cells[5].Value.ToString()))+(Int32.Parse(dgvSale.CurrentRow.Cells[4].Value.ToString()) * charge));
                    dgvSale.CurrentCell = dgvSale.Rows[dgvSale.CurrentCell.RowIndex].Cells[5];
                    

                }

                else if (dgvSale.CurrentCell.ColumnIndex == 5)
                {
                    BOLStock bolstock= new BOLStock ();
                    bolstock.ItemCode = dgvSale.CurrentRow.Cells[1].Value.ToString();
                    bolstock.Price = decimal.Parse( dgvSale.CurrentRow.Cells[5].Value.ToString());
                    bolstock.WholeSalePrice = decimal.Parse(dgvSale.CurrentRow.Cells[5].Value.ToString());
                    bool IsWholeSale=false;
                    if (chkwholesale.Checked)
                    {
                        IsWholeSale = true;
                    }
                    dalstock.UpdateStockPrice(bolstock,IsWholeSale);
                    int charge = Int32.Parse(dgvSale.CurrentRow.Cells[10].Value.ToString() == "" ? "0" : dgvSale.CurrentRow.Cells[10].Value.ToString());
                    dgvSale.CurrentRow.Cells[6].Value = Convert.ToString((Int32.Parse(dgvSale.CurrentRow.Cells[4].Value.ToString()) * Int32.Parse(dgvSale.CurrentRow.Cells[5].Value.ToString())) + (Int32.Parse(dgvSale.CurrentRow.Cells[4].Value.ToString()) * charge));

                    int totalprice = 0;
                    for (int i = 0; i < dgvSale.Rows.Count; i++)
                    {
                        if (dgvSale.Rows[i].Cells[7].Value.ToString() == "False")
                        {
                            totalprice += Int32.Parse(dgvSale.Rows[i].Cells[6].Value.ToString());
                        }
                    }

                    txtTotalAmt.Text = totalprice.ToString();
                    dgvSale.CurrentCell = dgvSale.Rows[dgvSale.CurrentCell.RowIndex].Cells[10];

                }
                else if (dgvSale.CurrentCell.ColumnIndex == 6)
                {
                    dgvSale.CurrentCell = dgvSale.Rows[dgvSale.CurrentCell.RowIndex].Cells[7];
                }
                else if (dgvSale.CurrentCell.ColumnIndex == 7)
                {
                    if (dgvSale.CurrentRow.Cells[7].Value.ToString() == "True")
                    {
                        dgvSale.CurrentRow.Cells[8].Value = "0";
                        dgvSale.CurrentRow.Cells[9].Value = "0";
                        dgvSale.CurrentRow.Cells[8].ReadOnly = true;
                        dgvSale.CurrentRow.Cells[9].ReadOnly = true;
                    }
                    else
                    {
                        dgvSale.CurrentRow.Cells[8].ReadOnly = false;
                        dgvSale.CurrentRow.Cells[9].ReadOnly = false;
                    }
                    dgvSale.CurrentCell = dgvSale.Rows[dgvSale.CurrentCell.RowIndex].Cells[8];
                }
                else if (dgvSale.CurrentCell.ColumnIndex == 8)
                {
                    string DisCheck = "";
                    DisCheck = MoeYanPOS.Function.Validation.isNumberField("Item Discount %", dgvSale.CurrentRow.Cells[8].Value.ToString());
                    if (DisCheck != "")
                    {
                        MessageBox.Show(DisCheck);
                        dgvSale.CurrentRow.Cells[9].Value = "0";
                    }
                    else
                    {
                        int dispercent = 0;
                        dispercent = (Int32.Parse(dgvSale.CurrentRow.Cells[8].Value.ToString()) * Int32.Parse(dgvSale.CurrentRow.Cells[6].Value.ToString())) / 100;
                        dgvSale.CurrentRow.Cells[9].Value = dispercent.ToString();
                    }

                    // Remove By Kyaw SiThu - for first day sale
                    //if (Int32.Parse(dgvSale.CurrentRow.Cells[8].Value.ToString()) == 100)
                    //{
                    //    MessageBox.Show("Please check your discount percent !!");
                    //    return;
                    //}

                    dgvSale.CurrentCell = dgvSale.Rows[dgvSale.CurrentCell.RowIndex].Cells[9];
                }
                else if (dgvSale.CurrentCell.ColumnIndex == 10)
                {
                    string ChargeCheck = "";
                    ChargeCheck = MoeYanPOS.Function.Validation.isNumberField("Charges", dgvSale.CurrentRow.Cells[10].Value.ToString());

                    if (ChargeCheck != "")
                    {
                        MessageBox.Show(ChargeCheck);
                        dgvSale.CurrentRow.Cells[9].Value = 0;
                    }
                    else
                    {
                        decimal charge = Convert.ToDecimal(dgvSale.Rows[dgvSale.CurrentCell.RowIndex].Cells[10].Value.ToString());
                        int qty = Convert.ToInt32(dgvSale.Rows[dgvSale.CurrentCell.RowIndex].Cells[4].Value.ToString());
                        decimal amt = Convert.ToDecimal(Int32.Parse(dgvSale.Rows[dgvSale.CurrentCell.RowIndex].Cells[5].Value.ToString()) * Convert.ToInt32(dgvSale.Rows[dgvSale.CurrentCell.RowIndex].Cells[4].Value.ToString()));
                        dgvSale.CurrentRow.Cells[6].Value = amt + (charge*qty);
                        dgvSale.CurrentRow.Cells[10].Value = charge;

                        if (dgvSale.CurrentRow.Index == dgvSale.Rows.Count - 1)
                        {
                            dgvSale.Rows.Add(dgvSale.Rows.Count + 1, "0000", "", "", "1", "0", "0", colFOC.Items[1].ToString(), "0", "0","0","");
                            dgvSale.Rows[dgvSale.Rows.Count - 1].Cells[1].Selected = true;
                            txtItemCode.Focus();
                        }
                    }
                    txtItemCode.BackColor = Color.LightGreen;
                }
                else
                {
                    string DisCheck = "";
                    DisCheck = MoeYanPOS.Function.Validation.isNumberField("Item Discount", dgvSale.CurrentRow.Cells[9].Value.ToString());
                    if (DisCheck != "")
                    {
                        MessageBox.Show(DisCheck);
                        dgvSale.CurrentRow.Cells[9].Value = 0;
                    }
                    else
                    {
                        //if (Int32.Parse(dgvSale.CurrentRow.Cells[9].Value.ToString()) == Int32.Parse(dgvSale.CurrentRow.Cells[6].Value.ToString()))
                        //{
                        //    MessageBox.Show("Please check your discount amount !!");
                        //    return;
                        //}

                        //if (dgvSale.CurrentRow.Index == dgvSale.Rows.Count - 1)
                        //{
                        //    dgvSale.Rows.Add(dgvSale.Rows.Count + 1, "0000", "", "", "1", "0", "0", colFOC.Items[1].ToString(), "0", "0", "");
                        //    dgvSale.Rows[dgvSale.Rows.Count-1].Cells[1].Selected = true;
                        //    txtItemCode.Focus();
                        //}
                    }

                }

                decimal TotalDis = 0; decimal TotalFOC = 0; decimal TotalAmt = 0;

                for (int i = 0; i < dgvSale.Rows.Count; i++)
                {
                    if (dgvSale.Rows[i].Cells[9].Value != null)
                    {
                        TotalDis += Decimal.Parse(dgvSale.Rows[i].Cells[9].Value.ToString());
                    }

                }

                for (int i = 0; i < dgvSale.Rows.Count; i++)
                {
                    if (dgvSale.Rows[i].Cells[6].Value != null)
                    {
                        TotalAmt += Decimal.Parse(dgvSale.Rows[i].Cells[6].Value.ToString());
                    }
                }

                for (int i = 0; i < dgvSale.Rows.Count; i++)
                {
                    if (dgvSale.Rows[i].Cells[7].Value != null)
                    {
                        if (dgvSale.Rows[i].Cells[7].Value.ToString() == "True")
                        {
                            if (dgvSale.Rows[i].Cells[6].Value != null)
                            {
                                TotalFOC += Decimal.Parse(dgvSale.Rows[i].Cells[6].Value.ToString());
                            }
                        }
                    }
                }

                txtTotalAmt.Text = TotalAmt.ToString();
                txtTotalFOC.Text = TotalFOC.ToString();
                txtItemDiscount.Text = TotalDis.ToString();
                txtGrandTotal.Text = Convert.ToString(Int32.Parse(txtTotalAmt.Text) - (Int32.Parse(txtAdvance.Text) + Int32.Parse(txtDiscount.Text) + Int32.Parse(txtTotalFOC.Text) + Int32.Parse(txtItemDiscount.Text)));
                Save_TempTables();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtAdvance_KeyDown(object sender, KeyEventArgs e)
        {            
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtDiscount.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (Int32.Parse(txtDiscount.Text) == Int32.Parse(txtTotalAmt.Text))
                    {
                        MessageBox.Show(" Enter Data for Sale.");
                    }

                    txtGrandTotal.Text = Convert.ToString(Int32.Parse(txtTotalAmt.Text) - (Int32.Parse(txtAdvance.Text) + Int32.Parse(txtDiscount.Text) + Int32.Parse(txtTotalFOC.Text) +Int32.Parse(txtItemDiscount.Text)));
                    if (txtTotalAmt.Text == "0")
                    {
                        MessageBox.Show(" Enter Data for Sale.");
                    }
                    else
                    {
                        //btnSave_Click(sender, e);
                    }

                }
                else if (e.KeyCode == Keys.F5)
                {
                    txtGrandTotal.Text = Convert.ToString(Int32.Parse(txtTotalAmt.Text) - (Int32.Parse(txtAdvance.Text) + Int32.Parse(txtDiscount.Text) + Int32.Parse(txtTotalFOC.Text) + Int32.Parse(txtItemDiscount.Text)));
                    if (txtTotalAmt.Text == "0" | lblCustomerID.Text=="0")
                    {
                        MessageBox.Show(" Enter Data for Sale.");
                    }
                    else
                    {
                        //btnSave_Click(sender, e);
                    }

                }
                if (e.KeyCode == Keys.F8)
                {
                    btnNew_Click(sender, e);
                }
                else if (e.KeyCode == Keys.F10)
                {
                    btnPrint_Click(sender, e);
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
                BOLSale bolsale = new BOLSale();
               // if (txtTotalAmt.Text == "0" | lblCustomerID.Text == "0" )
                if (lblCustomerID.Text == "0")
                {
                    MessageBox.Show(" Please Enter Require Data to Save.");
                    dgvSale.Focus();
                    return;
                }

                if (btnSave.Text == "&Save (F5)")
                {
                    if (dgvSale.CurrentRow.Cells["colItemCode"].Value != "0000")
                    {
                        MessageBox.Show(" Please press Enter key until next line.");
                        dgvSale.Focus();
                        return;
                    }
                }

                //remove by KSAung
                //if (long.Parse( txtTotalAmt.Text) == long.Parse(txtDiscount.Text))
                //{
                //    MessageBox.Show("Total and Discount Amount are the same.Data Cannot Save.");
                //    dgvSale.Focus();
                //    return;
                //}

                //if (txtVoucherNo.Text == "")
                //{
                //    MessageBox.Show("Enter voucher no.");
                //    return;
                //}

                if (cboPaymentType.Text == "Credit")
                {
                    string myStr = txtCustomer.Text;
                    string subStr = myStr.Substring(myStr.Length - 6);

                    if (subStr == "DF0001")
                    {
                        MessageBox.Show("Default Customer not allow to credit.", "POS Alert");
                        txtCustomer.Focus();
                        return;
                    }
                }

                //else if(long.Parse(lblSaleID.Text)!= 0)
                //{
                //    MessageBox.Show(" This data is already saved."); 
                //    return;
                //}
                
                int isSaved = 0; int isDetailSaved = 0;
                List<BOLCustomer> getlstcustomer = new List<BOLCustomer>();
                getlstcustomer = dalcustomer.GetCustomer(txtCustomer.Text, "", 0);
                if (getlstcustomer.Count > 0)
                {
                    bolsale.CustomerID =getlstcustomer[0].ID.ToString();                   
                }

                //Remove By KSAung
                //int isVName = 0; 
                //For duplicate added By KSAung
                //if (btnSave.Text == "&Save (F5)" | btnSave.Text == "&Print Slip (F9)") 
                //{
                //    List<BOLSale> getvoucher = new List<BOLSale>();
                //    getvoucher = dalsale.DuplicateVoucher(txtVoucherNo.Text);
                //    if (getvoucher.Count > 0)
                //    {
                //        MessageBox.Show("Duplicate Voucher No");
                //        txtVoucherNo.Focus();
                //        return;
                //    }
                //}

                if (lblSaleID.Text == "0")
                {
                    //Save Sale
                   
                    bolsale.TranSaleID = long.Parse(lblTransID.Text); //   
                
                    if (txtVoucherNo.Text =="")
                    {
                        bolsale.VoucherNo = txtSystemVoucherNo.Text;
                    }
                    else
                    {
                        bolsale.VoucherNo = txtVoucherNo.Text;
                    }

                    bolsale.SaleDate = dtpSaleDate.Value;   //  SaleDate;                    
                    bolsale.UserID = Int32.Parse(lblUserID.Text);
                    bolsale.PaymentType = cboPaymentType.Text;
                    bolsale.CurrencyID = Int32.Parse(cbocurrency.SelectedValue.ToString());
                    bolsale.DayLimit = Int32.Parse(txtDayLimit.Text);
                    bolsale.TotalAmt = Decimal.Parse(txtTotalAmt.Text);
                    bolsale.Advance = Decimal.Parse(txtAdvance.Text);
                    bolsale.Discount = Decimal.Parse(txtDiscount.Text);
                    bolsale.GrandTotal = Decimal.Parse(txtGrandTotal.Text);
                    bolsale.TotalFOC = Int32.Parse(txtTotalFOC.Text);
                    bolsale.TotalitemDiscount = Decimal.Parse(txtItemDiscount.Text);
                    bolsale.ExchangeRate = Decimal.Parse(txtExchangeRate.Text);
                    bolsale.SystemVoucherNo = txtSystemVoucherNo.Text;
                    bolsale.LotteryDate = LotteryDate;
                    bolsale.LotteryNo = txtLotteryNo.Text;
                    bolsale.DrawingTimes = txtDrawingTimes.Text;
                    bolsale.LocationID = long.Parse(cboLocation.SelectedValue.ToString());
                    bolsale.TransportationAmt = Int32.Parse(txtTransportationAmt.Text);
                    bolsale.Remark = txtRemark.Text;
                    bolsale.CounterID = txtCounter.Text;

                    isSaved = dalsale.SaveSaleData(bolsale);

                    if (isSaved == 1)
                    {                      

                        //Save Sale Detail
                        for (int i = 0; i < dgvSale.Rows.Count; i++)
                        {

                            if (dgvSale.Rows.Count >0)
                            {
                                if (dgvSale.Rows[i].Cells["colSaleDetailID"].Value == null & dgvSale.Rows[i].Cells["colItemCode"].Value != "0000")
                                {
                                    BOLSale bolsaledetail = new BOLSale();
                                    bolsaledetail.SaleID = dalsale.GetMaxSaleID(0, txtSystemVoucherNo.Text);
                                    lblSaleID.Text = bolsaledetail.SaleID.ToString();
                                    bolsaledetail.No = Int32.Parse(dgvSale.Rows[i].Cells["colNo"].Value.ToString());
                                    bolsaledetail.ItemCode = dgvSale.Rows[i].Cells["colItemCode"].Value.ToString();
                                    int isExist = 0;
                                    isExist = dalstock.CheckStock(bolsaledetail.ItemCode);
                                    if (isExist == 0 & bolsaledetail.ItemCode != "0000")
                                    {
                                        MessageBox.Show(" This stock code doesn't exist.");
                                        return;
                                    }
                                    bolsaledetail.Description = dgvSale.Rows[i].Cells["colDescription"].Value.ToString();
                                    bolsaledetail.Mtype = dgvSale.Rows[i].Cells["colType"].Value.ToString();
                                    bolsaledetail.Qty = Int32.Parse(dgvSale.Rows[i].Cells["colQty"].Value.ToString());
                                    bolsaledetail.SalePrice = Decimal.Parse(dgvSale.Rows[i].Cells["colSalePrice"].Value.ToString());
                                    bolsaledetail.Total = Decimal.Parse(dgvSale.Rows[i].Cells["colTotal"].Value.ToString());
                                    bolsaledetail.Charge = Decimal.Parse(dgvSale.Rows[i].Cells["colCharges"].Value.ToString());
                                    if (dgvSale.Rows[i].Cells["colFOC"].Value.ToString() == "True")
                                    {
                                        bolsaledetail.FOC = true;
                                    }
                                    else
                                    {
                                        bolsaledetail.FOC = false;
                                    }
                                    bolsaledetail.ItemDiscount = Decimal.Parse(dgvSale.Rows[i].Cells["colItemDiscount"].Value.ToString());
                                    bolsaledetail.ItemDiscountPercent = Int32.Parse(dgvSale.Rows[i].Cells["colItemDiscountPercent"].Value.ToString());
                                    dalsaledetail.SaveSaleDetailData(bolsaledetail);
                                }
                                else if ((dgvSale.Rows[i].Cells["colSaleDetailID"].Value == null || dgvSale.Rows[i].Cells["colSaleDetailID"].Value.ToString() == "") & dgvSale.Rows[i].Cells["colItemCode"].Value != "0000")
                                {
                                    BOLSale bolsaledetail = new BOLSale();
                                    bolsaledetail.SaleID = dalsale.GetMaxSaleID(0, txtSystemVoucherNo.Text);
                                    lblSaleID.Text = bolsaledetail.SaleID.ToString();
                                    bolsaledetail.No = Int32.Parse(dgvSale.Rows[i].Cells["colNo"].Value.ToString());
                                    bolsaledetail.ItemCode = dgvSale.Rows[i].Cells["colItemCode"].Value.ToString();
                                    int isExist = 0;
                                    isExist = dalstock.CheckStock(bolsaledetail.ItemCode);
                                    if (isExist == 0 & bolsaledetail.ItemCode != "0000")
                                    {
                                        MessageBox.Show(" This stock code doesn't exist.");
                                        return;
                                    }
                                    bolsaledetail.Description = dgvSale.Rows[i].Cells["colDescription"].Value.ToString();
                                    //bolsaledetail.Mtype = "Pcs";
                                    bolsaledetail.Mtype = dgvSale.Rows[i].Cells["colType"].Value.ToString();
                                    bolsaledetail.Qty = Int32.Parse(dgvSale.Rows[i].Cells["colQty"].Value.ToString());
                                    bolsaledetail.SalePrice = Decimal.Parse(dgvSale.Rows[i].Cells["colSalePrice"].Value.ToString());
                                    bolsaledetail.Total = Decimal.Parse(dgvSale.Rows[i].Cells["colTotal"].Value.ToString());
                                    bolsaledetail.Charge = Decimal.Parse(dgvSale.Rows[i].Cells["colCharges"].Value.ToString());
                                    if (dgvSale.Rows[i].Cells["colFOC"].Value.ToString() == "True")
                                    {
                                        bolsaledetail.FOC = true;
                                    }
                                    else
                                    {
                                        bolsaledetail.FOC = false;
                                    }
                                    bolsaledetail.ItemDiscount = Decimal.Parse(dgvSale.Rows[i].Cells["colItemDiscount"].Value.ToString());
                                    bolsaledetail.ItemDiscountPercent = Int32.Parse(dgvSale.Rows[i].Cells["colItemDiscountPercent"].Value.ToString());
                                    dalsaledetail.SaveSaleDetailData(bolsaledetail);
                                }
                            }
                        }

                        MessageBox.Show("Sale data is successfully saved.");

                        //btnPreviewVoucher_Click(sender, e); //add by KSAung 

                        //if (LstCheckPrintAndMsgBox.IsSavePrint)
                        //{
                        //    btnA4Print_Click(sender, e);
                        //}
                        //dalsale.DeleteTempRows(0, bolsale.SystemVoucherNo);

                        SysVoucherNo = "";
                        SysVoucherNo = txtSystemVoucherNo.Text;

                        btnNew.BackColor = Color.Azure;
                        btnNew_Click(sender,e);

                        //CleanTextBox();
                        //saveTampSale();

                        //if (txtVoucherNo.Text == "")
                        //{
                        //    dgvTemp.Rows.Add(dgvTemp.Rows.Count + 1, txtSystemVoucherNo.Text, txtSystemVoucherNo.Text);
                        //}
                        //else
                        //{
                        //    dgvTemp.Rows.Add(dgvTemp.Rows.Count + 1, txtVoucherNo.Text, txtSystemVoucherNo.Text);
                        //}
                    }
                }
                else
                {                                      
                    //Save Sale                   
                    bolsale.SaleID = long.Parse(lblSaleID.Text);
                    bolsale.VoucherNo = txtVoucherNo.Text;
                    bolsale.EditSaleDate = dtpSaleDate.Value;
                    bolsale.SaleDate = dtpSaleDate.Value;
                    bolsale.EditUserID =frmMain.UserID;
                    bolsale.PaymentType = cboPaymentType.Text;
                    bolsale.CurrencyID = Int32.Parse(cbocurrency.SelectedValue.ToString());
                    bolsale.DayLimit = Int32.Parse(txtDayLimit.Text);
                    bolsale.TotalAmt = Decimal.Parse(txtTotalAmt.Text);
                    bolsale.Advance = Decimal.Parse(txtAdvance.Text);
                    bolsale.Discount = Decimal.Parse(txtDiscount.Text);
                    bolsale.GrandTotal = Decimal.Parse(txtGrandTotal.Text);
                    bolsale.TotalFOC = Int32.Parse(txtTotalFOC.Text);
                    bolsale.TotalitemDiscount = Decimal.Parse(txtItemDiscount.Text);
                    bolsale.ExchangeRate = Decimal.Parse(txtExchangeRate.Text);
                    bolsale.LotteryDate = LotteryDate;
                    bolsale.LotteryNo = txtLotteryNo.Text;
                    bolsale.DrawingTimes = txtDrawingTimes.Text;
                    bolsale.LocationID = long.Parse(cboLocation.SelectedValue.ToString());
                    bolsale.Remark = txtRemark.Text;
                    bolsale.CounterID = txtCounter.Text;

                    isSaved = dalsale.UpdateSaleBySaleID(bolsale);

                    txtVoucherNo.Enabled = true;

                    if (isSaved == 1)
                    {
                        //Save Sale Detail
                        for (int i = 0; i < dgvSale.Rows.Count - 1; i++)
                        {

                            if (dgvSale.Rows.Count > 1)
                            {
                                if(SDetailID.Count > 0)
                                {
                                    for (int k = 0; k < SDetailID.Count; k++)
                                    {
                                        dalsaledetail.DeleteSaleDetail(long.Parse(SDetailID[k].ToString()), txtSystemVoucherNo.Text);
                                    }
                                }
                                if (dgvSale.Rows[i].Cells["colSaleDetailID"].Value != "" && dgvSale.Rows[i].Cells["colSaleDetailID"].Value != null)
                                {
                                    BOLSale bolsaledetail = new BOLSale();
                                    bolsaledetail.SaleDetailID = long.Parse(dgvSale.Rows[i].Cells["colSaleDetailID"].Value.ToString());
                                    bolsaledetail.ItemCode = dgvSale.Rows[i].Cells["colItemCode"].Value.ToString();
                                    int isExist = 0;
                                    isExist = dalstock.CheckStock(bolsaledetail.ItemCode);
                                    if (isExist == 0 & bolsaledetail.ItemCode != "0000")
                                    {
                                        MessageBox.Show(" This stock code doesn't exist.");
                                        return;
                                    }
                                    bolsaledetail.Description = dgvSale.Rows[i].Cells["colDescription"].Value.ToString();
                                    //bolsaledetail.Mtype = "Pcs";//dgvSale.Rows[i].Cells["colType"].Value.ToString(); \\Remove By KSAung
                                    bolsaledetail.Mtype =  dgvSale.Rows[i].Cells["colType"].Value.ToString();
                                    bolsaledetail.Qty = Int32.Parse(dgvSale.Rows[i].Cells["colQty"].Value.ToString());
                                    bolsaledetail.SalePrice = Decimal.Parse(dgvSale.Rows[i].Cells["colSalePrice"].Value.ToString());
                                    bolsaledetail.Total = Decimal.Parse(dgvSale.Rows[i].Cells["colTotal"].Value.ToString());
                                    if (dgvSale.Rows[i].Cells["colFOC"].Value.ToString() == "True")
                                    {
                                        bolsaledetail.FOC = true;
                                    }
                                    else
                                    {
                                        bolsaledetail.FOC = false;
                                    }
                                    bolsaledetail.ItemDiscount = Decimal.Parse(dgvSale.Rows[i].Cells["colItemDiscount"].Value.ToString());
                                    bolsaledetail.ItemDiscountPercent = Int32.Parse(dgvSale.Rows[i].Cells["colItemDiscountPercent"].Value.ToString());
                                    bolsaledetail.Charge = Decimal.Parse(dgvSale.Rows[i].Cells["colCharges"].Value.ToString());
                                    dalsaledetail.UpdateSaleDetailData(bolsaledetail);
                                }
                                else
                                {
                                    BOLSale bolsaledetail = new BOLSale();
                                    bolsaledetail.SaleID = dalsale.GetMaxSaleID(0,txtSystemVoucherNo.Text);
                                    lblSaleID.Text = bolsaledetail.SaleID.ToString();
                                    bolsaledetail.ItemCode = dgvSale.Rows[i].Cells["colItemCode"].Value.ToString();
                                    int isExist = 0;
                                    isExist = dalstock.CheckStock(bolsaledetail.ItemCode);
                                    if (isExist == 0 & bolsaledetail.ItemCode != "0000")
                                    {
                                        MessageBox.Show(" This stock code doesn't exist.");
                                        return;
                                    }
                                    bolsaledetail.Description = dgvSale.Rows[i].Cells["colDescription"].Value.ToString();
                                    bolsaledetail.Mtype = dgvSale.Rows[i].Cells["colType"].Value.ToString();
                                    bolsaledetail.Qty = Int32.Parse(dgvSale.Rows[i].Cells["colQty"].Value.ToString());
                                    bolsaledetail.SalePrice = Decimal.Parse(dgvSale.Rows[i].Cells["colSalePrice"].Value.ToString());
                                    bolsaledetail.Total = Decimal.Parse(dgvSale.Rows[i].Cells["colTotal"].Value.ToString());
                                    if (dgvSale.Rows[i].Cells["colFOC"].Value.ToString() == "True")
                                    {
                                        bolsaledetail.FOC = true;
                                    }
                                    else
                                    {
                                        bolsaledetail.FOC = false;
                                    }
                                    bolsaledetail.ItemDiscount = Decimal.Parse(dgvSale.Rows[i].Cells["colItemDiscount"].Value.ToString());
                                    bolsaledetail.ItemDiscountPercent = Int32.Parse(dgvSale.Rows[i].Cells["colItemDiscountPercent"].Value.ToString());
                                    bolsaledetail.Charge = Decimal.Parse(dgvSale.Rows[i].Cells["colCharges"].Value.ToString());
                                    dalsaledetail.SaveSaleDetailData(bolsaledetail);
                                }
                            }
                        }
                        MessageBox.Show("Sale data is successfully updated.");
                        //btnSave.Text = "&Save";
                        //CleanTextBox();
                        //btnNew_Click(sender, e);
                        SysVoucherNo = txtSystemVoucherNo.Text;
                        this.Close();
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
            try
            {
                chkwholesale.Checked = false;
                //chkwholesale.Enabled = false;
                btnSave.Text = "&Save";
                lblSaleID.Text = "0";              
                txtVoucherNo.Text="";
                //dtpSaleDate.Value = DateTime.Parse( DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss ttt"));

                txtLotteryNo.Text = "0";
                dtpLotteryDate.Value = DateTime.Now;
                txtDrawingTimes.Text = "0";
                txtCustomer.Text = "";
                lblCurrencyID.Text = "";
                txtCustomerName.Text = "";
                             
                cboPaymentType.SelectedIndex = 0;
                cbocurrency.SelectedIndex =0;
                lblUserID.Text = frmMain.UserID.ToString();
                txtDayLimit.Text="0";
                txtTotalAmt.Text="0";
                txtAdvance.Text="0";
                txtDiscount.Text="0";
                txtGrandTotal.Text="0";
                txtTotalFOC.Text="0";
                txtItemDiscount.Text="0";
                txtItemCode.Text = "";
                txtTransportationAmt.Text = "0";
                txtRemark.Text = "";
                dgvItemCode.Rows.Clear();
                dgvCustomer.Visible = false;
                dgvItemCode.Visible = false;
                dgvSale.Rows.Clear();
                dgvSale.Rows.Add();
                dgvSale.Rows[0].Cells[0].Value = 1;
                dgvSale.Rows[0].Cells[1].Value = "0000";
                dgvSale.Rows[0].Cells[2].Value = "";
                dgvSale.Rows[0].Cells[3].Value = "";
                dgvSale.Rows[0].Cells[4].Value = "1";
                dgvSale.Rows[0].Cells[5].Value = "0";
                dgvSale.Rows[0].Cells[6].Value = "0";
                dgvSale.Rows[0].Cells[7].Value = colFOC.Items[1].ToString();
                dgvSale.Rows[0].Cells[8].Value = "0";
                dgvSale.Rows[0].Cells[9].Value = "0";
                dgvSale.Rows[0].Cells[10].Value = "";
                dgvSale.Rows[0].Cells[1].Selected = true;
                txtCustomer.Focus();
                txtItemCode.BackColor = Color.White;
                //LoadTemp();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvSale_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
               
                int index = dgvSale.CurrentRow.Index;
                if (e.KeyCode == Keys.F5)
                {
                    btnSave_Click(sender, e);
                }

                if (e.KeyCode == Keys.F6)
                {
                    btnPreviewVoucher_Click(sender, e);
                }

                if (e.KeyCode == Keys.F7)
                {
                    btnA4Print_Click(sender, e);
                }

                if (e.KeyCode == Keys.F8)
                {
                    btnNew_Click(sender, e);
                }

                if (e.KeyCode == Keys.F9)
                {
                    btnClose_Click(sender, e);
                }

                if (e.KeyCode == Keys.F2)
                {
                    txtPending2_Click(sender, e);
                }

                if (e.KeyCode == Keys.F3)
                {
                    txtPending3_Click(sender, e);
                }
                else if (e.KeyCode == Keys.F10)
                {
                    btnPrint_Click(sender, e);
                }

                //if (e.KeyCode == Keys.F10)
                //{
                //    btnNew_Click(sender, e);
                //}

                //if (e.KeyCode == Keys.F11)
                //{
                //    btnClose_Click(sender, e);
                //}

                if (e.KeyCode == Keys.Delete)
                {
                    if (dgvSale.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow row in dgvSale.SelectedRows)
                        {
                            if (row.Cells[11].Value == null)
                            {
                                dalsaledetail.DeleteTempSaleDetail(Int32.Parse(dgvSale.CurrentRow.Cells[0].Value.ToString()), txtSystemVoucherNo.Text);
                                dgvSale.Rows.Remove(row);
                            }
                            else if (row.Cells[11].Value.ToString() == "")
                            {
                                dalsaledetail.DeleteTempSaleDetail(Int32.Parse(dgvSale.CurrentRow.Cells[0].Value.ToString()),txtSystemVoucherNo.Text);
                                dgvSale.Rows.Remove(row);
                            }
                            else
                            {
                                if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    //dalsaledetail.DeleteSaleDetail(long.Parse(row.Cells[10].Value.ToString()), txtSystemVoucherNo.Text);
                                    SDetailID.Add(Int32.Parse(row.Cells[11].Value.ToString()));
                                    dgvSale.Rows.Remove(row);
                                }
                                else
                                {
                                    MessageBox.Show(" Please do sale reuturn.");
                                }
                            }

                                    decimal TotalDis = 0; decimal TotalFOC = 0; decimal TotalAmt = 0;

                                    for (int i = 0; i < dgvSale.Rows.Count; i++)
                                    {
                                        TotalDis += Decimal.Parse(dgvSale.Rows[i].Cells[9].Value.ToString());
                                    }

                                    for (int i = 0; i < dgvSale.Rows.Count; i++)
                                    {
                                        //if (dgvSale.Rows[i].Cells[7].Value.ToString() == "False")
                                        //{
                                        TotalAmt += Decimal.Parse(dgvSale.Rows[i].Cells[6].Value.ToString());
                                        //}
                                    }

                                    for (int i = 0; i < dgvSale.Rows.Count; i++)
                                    {
                                        if (dgvSale.Rows[i].Cells[7].Value.ToString() == "True")
                                        {
                                            TotalFOC += Decimal.Parse(dgvSale.Rows[i].Cells[6].Value.ToString());
                                        }
                                    }

                                    //txtDiscount.Text = TotalDis.ToString();
                                    txtTotalAmt.Text = TotalAmt.ToString();
                                    txtTotalFOC.Text = TotalFOC.ToString();
                                    txtItemDiscount.Text = TotalDis.ToString();
                                    txtGrandTotal.Text = Convert.ToString(Int32.Parse(txtTotalAmt.Text) - (Int32.Parse(txtAdvance.Text) + Int32.Parse(txtDiscount.Text) + Int32.Parse(txtTotalFOC.Text) + Int32.Parse(txtItemDiscount.Text)));
                                    
                              }
                    }

                }
                if (e.KeyCode == Keys.Enter)
                {
                    if (dgvSale.SelectedCells[0].ColumnIndex == 1)
                    {
                        if (dgvSale.CurrentRow.Cells[1].Value != null)
                        {
                            if (dgvSale.CurrentRow.Cells[1].Value.ToString() == "0000")
                            {
                                if (cboPaymentType.Text == "Cash")
                                {
                                    txtAdvance.Enabled = false;
                                    txtDiscount.Focus();
                                }
                                else if (cboPaymentType.Text == "Credit")
                                {
                                    txtAdvance.Enabled = true;
                                    txtAdvance.Focus();
                                }
                            }

                            else
                            {
                                List<BOLStock> lstStk = new List<BOLStock>();
                                lstStk = dalstock.SelectStock("", 0, dgvSale.CurrentRow.Cells[1].Value.ToString(), 0);
                                if (lstStk.Count > 0)
                                {
                                    dgvSale.CurrentRow.Cells[2].Value = lstStk[0].NameMM;
                                    if (chkwholesale.Checked)
                                    {
                                        dgvSale.CurrentRow.Cells[5].Value = lstStk[0].WholeSalePrice;
                                    }
                                    else
                                    {
                                        dgvSale.CurrentRow.Cells[5].Value = lstStk[0].Price;
                                    }
                                    dgvSale.CurrentCell = dgvSale.CurrentRow.Cells[4];
                                }
                                else
                                {
                                    MessageBox.Show("Enter correct Item Code!");
                                    //txtItemCode.Text = dgvSale.CurrentRow.Cells[1].Value.ToString();
                                    txtItemCode.Focus();
                                    txtItemCode.BackColor = Color.LightGreen;
                                }

                            }
                        }
                    }

                    else if (dgvSale.CurrentCell.ColumnIndex == 2)
                    {
                        dgvSale.CurrentCell = dgvSale.Rows[dgvSale.CurrentCell.RowIndex].Cells[4];
                    }
                    else if (dgvSale.CurrentCell.ColumnIndex == 3)
                    {
                        dgvSale.CurrentCell = dgvSale.Rows[dgvSale.CurrentCell.RowIndex].Cells[4];
                    }

                    else if (dgvSale.CurrentCell.ColumnIndex == 4)
                    {
                        string qtyCheck = "";
                        qtyCheck = MoeYanPOS.Function.Validation.isNumberField("Qty", dgvSale.Rows[dgvSale.CurrentCell.RowIndex].Cells[4].Value.ToString());
                        if (qtyCheck != "")
                        {
                            MessageBox.Show(qtyCheck);
                            dgvSale.CurrentRow.Cells[4].Value = 1;
                            return;
                        }
                        int charge = Int32.Parse(dgvSale.CurrentRow.Cells[10].Value.ToString() == "" ? "0" : dgvSale.CurrentRow.Cells[10].Value.ToString());
                        dgvSale.CurrentRow.Cells[6].Value = Convert.ToString((Int32.Parse(dgvSale.CurrentRow.Cells[4].Value.ToString()) * Int32.Parse(dgvSale.CurrentRow.Cells[5].Value.ToString())) + (Int32.Parse(dgvSale.CurrentRow.Cells[4].Value.ToString()) * charge));
                        dgvSale.CurrentCell = dgvSale.Rows[dgvSale.CurrentCell.RowIndex].Cells[5];
                        
                    }

                    else if (dgvSale.CurrentCell.ColumnIndex == 5)
                    {
                        int charge = Int32.Parse(dgvSale.CurrentRow.Cells[10].Value.ToString() == "" ? "0" : dgvSale.CurrentRow.Cells[10].Value.ToString());
                        dgvSale.CurrentRow.Cells[6].Value = Convert.ToString((Int32.Parse(dgvSale.CurrentRow.Cells[4].Value.ToString()) * Int32.Parse(dgvSale.CurrentRow.Cells[5].Value.ToString())) + (Int32.Parse(dgvSale.CurrentRow.Cells[4].Value.ToString()) * charge));

                        int totalprice = 0;
                        for (int i = 0; i < dgvSale.Rows.Count; i++)
                        {
                            if (dgvSale.Rows[i].Cells[7].Value.ToString() == "False")
                            {
                                totalprice += Int32.Parse(dgvSale.Rows[i].Cells[6].Value.ToString());
                            }
                        }

                        txtTotalAmt.Text = totalprice.ToString();
                        dgvSale.CurrentCell = dgvSale.Rows[dgvSale.CurrentCell.RowIndex].Cells[10];

                    }
                    else if (dgvSale.CurrentCell.ColumnIndex == 6)
                    {
                        dgvSale.CurrentCell = dgvSale.Rows[dgvSale.CurrentCell.RowIndex].Cells[10];
                    }
                    else if (dgvSale.CurrentCell.ColumnIndex == 7)
                    {
                        if (dgvSale.CurrentRow.Cells[7].Value.ToString() == "True")
                        {
                            dgvSale.CurrentRow.Cells[8].Value = "0";
                            dgvSale.CurrentRow.Cells[9].Value = "0";
                            dgvSale.CurrentRow.Cells[8].ReadOnly = true;
                            dgvSale.CurrentRow.Cells[9].ReadOnly = true;
                        }
                        else
                        {
                            dgvSale.CurrentRow.Cells[8].ReadOnly = false;
                            dgvSale.CurrentRow.Cells[9].ReadOnly = false;
                        }
                        dgvSale.CurrentCell = dgvSale.Rows[dgvSale.CurrentCell.RowIndex].Cells[8];
                    }
                    else if (dgvSale.CurrentCell.ColumnIndex == 8)
                    {
                        string DisCheck = "";
                        DisCheck = MoeYanPOS.Function.Validation.isNumberField("Item Discount %", dgvSale.CurrentRow.Cells[8].Value.ToString());
                        if (DisCheck != "")
                        {
                            MessageBox.Show(DisCheck);
                            dgvSale.CurrentRow.Cells[9].Value = "0";
                        }
                        else
                        {
                            int dispercent = 0;
                            dispercent = (Int32.Parse(dgvSale.CurrentRow.Cells[8].Value.ToString()) * Int32.Parse(dgvSale.CurrentRow.Cells[6].Value.ToString())) / 100;
                            dgvSale.CurrentRow.Cells[9].Value = dispercent.ToString();
                        }

                        //if (Int32.Parse(dgvSale.CurrentRow.Cells[8].Value.ToString()) == 100)
                        //{
                        //    MessageBox.Show("Please check your discount percent !!");
                        //    return;
                        //}

                        dgvSale.CurrentCell = dgvSale.Rows[dgvSale.CurrentCell.RowIndex].Cells[9];

                    }
                    else if (dgvSale.CurrentCell.ColumnIndex == 9)
                    {
                        dgvSale.CurrentCell = dgvSale.Rows[dgvSale.CurrentCell.RowIndex].Cells[10];
                        dgvSale.CurrentRow.Cells[10].Value = "0";
                    }
                    else if (dgvSale.CurrentCell.ColumnIndex == 10)
                    {
                        if (dgvSale.CurrentRow.Index == dgvSale.Rows.Count - 1)
                        {
                            dgvSale.Rows.Add(dgvSale.Rows.Count + 1, "0000", "", "", "1", "0", "0", colFOC.Items[1].ToString(), "0", "0", "0");
                            dgvSale.CurrentRow.Cells[1].Selected = true;
                            txtItemCode.Focus();
                            txtItemCode.BackColor = Color.LightGreen;
                            dgvSale.CurrentRow.Cells[10].Value = "0";
                        }
                    }
                    else
                    {
                        string DisCheck = "";
                        DisCheck = MoeYanPOS.Function.Validation.isNumberField("Item Discount", dgvSale.CurrentRow.Cells[9].Value.ToString());
                        if (DisCheck != "")
                        {
                            MessageBox.Show(DisCheck);
                            dgvSale.CurrentRow.Cells[9].Value = 0;
                        }
                        else
                        {
                            //if (Int32.Parse(dgvSale.CurrentRow.Cells[9].Value.ToString()) == Int32.Parse(dgvSale.CurrentRow.Cells[6].Value.ToString()))
                            //{
                            //    MessageBox.Show("Please check your discount amount !!");
                            //    return;
                            //}

                            if (dgvSale.CurrentRow.Index == dgvSale.Rows.Count - 1)
                            {
                                dgvSale.Rows.Add(dgvSale.Rows.Count + 1, "0000", "", "", "1", "0", "0", colFOC.Items[1].ToString(), "0", "0", "");
                                dgvSale.CurrentRow.Cells[1].Selected = true;
                                txtItemCode.Focus();
                            }
                        }

                    }

                    decimal TotalDis = 0; decimal TotalFOC = 0; decimal TotalAmt = 0;

                    for (int i = 0; i < dgvSale.Rows.Count; i++)
                    {
                        if (dgvSale.Rows[i].Cells[9].Value != null)
                        {
                            TotalDis += Decimal.Parse(dgvSale.Rows[i].Cells[9].Value.ToString());
                        }

                    }

                    for (int i = 0; i < dgvSale.Rows.Count; i++)
                    {
                        if (dgvSale.Rows[i].Cells[6].Value != null)
                        {
                            TotalAmt += Decimal.Parse(dgvSale.Rows[i].Cells[6].Value.ToString());
                        }
                    }

                    for (int i = 0; i < dgvSale.Rows.Count; i++)
                    {
                        if (dgvSale.Rows[i].Cells[7].Value != null)
                        {
                            if (dgvSale.Rows[i].Cells[7].Value.ToString() == "True")
                            {
                                if (dgvSale.Rows[i].Cells[6].Value != null)
                                {
                                    TotalFOC += Decimal.Parse(dgvSale.Rows[i].Cells[6].Value.ToString());
                                }
                            }
                        }
                    }

                    txtTotalAmt.Text = TotalAmt.ToString();
                    txtTotalFOC.Text = TotalFOC.ToString();
                    txtItemDiscount.Text = TotalDis.ToString();
                    txtGrandTotal.Text = Convert.ToString(Int32.Parse(txtTotalAmt.Text) - (Int32.Parse(txtAdvance.Text) + Int32.Parse(txtDiscount.Text) + Int32.Parse(txtTotalFOC.Text) + Int32.Parse(txtItemDiscount.Text)));
                    //Save_TempTables();

                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

       
        private void saveTampSale()
        {
            try
            {
                if (lblCustomerID.Text == "0")
                {
                    MessageBox.Show(" Check Customer Name");
                    return;
                }

                BOLSale bolsale = new BOLSale();
                bolsale.TranSaleID = long.Parse(lblTransID.Text);
                bolsale.VoucherNo = txtVoucherNo.Text;
                bolsale.SaleDate = dtpSaleDate.Value;
                bolsale.CustomerID = txtCustomer.Text;
                bolsale.UserID = Int32.Parse(lblUserID.Text);
                bolsale.PaymentType = cboPaymentType.Text;
                bolsale.CurrencyID = Int32.Parse(cbocurrency.SelectedValue.ToString());
                bolsale.DayLimit = Int32.Parse(txtDayLimit.Text);
                bolsale.TotalAmt = Decimal.Parse(txtTotalAmt.Text);
                bolsale.Advance = Decimal.Parse(txtAdvance.Text);
                bolsale.Discount = Decimal.Parse(txtDiscount.Text);
                bolsale.GrandTotal = Decimal.Parse(txtGrandTotal.Text);
                bolsale.TotalFOC = Int32.Parse(txtTotalFOC.Text);
                bolsale.TotalitemDiscount = Decimal.Parse(txtItemDiscount.Text);
                bolsale.ExchangeRate = Decimal.Parse(txtExchangeRate.Text);
                bolsale.LotteryDate = dtpLotteryDate.Value;
                bolsale.LotteryNo = txtLotteryNo.Text;
                bolsale.Remark = txtRemark.Text;
                string strCheck = "";
                strCheck = MoeYanPOS.Function.Validation.isNumberField("Drawing Times", txtDrawingTimes.Text);
                if (strCheck != "")
                {
                    MessageBox.Show(strCheck);
                    txtDrawingTimes.Text = "0";
                    return;
                }
                else
                {
                    bolsale.DrawingTimes = txtDrawingTimes.Text;
                }
                bolsale.SystemVoucherNo = txtSystemVoucherNo.Text;
                bolsale.LocationID = long.Parse(cboLocation.SelectedValue.ToString());

                bolsale.Action = 1;
                int chktemp = 0;
                chktemp = dalsale.ChkSaveTemp(txtVoucherNo.Text);
                if (chktemp == 0)
                {  
                    dalsale.SaveSaleData(bolsale);
                }
                else if (chktemp > 0)
                {                   
                    dalsale.UpdateSaleBySaleID(bolsale);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {

                btnSave_Click(sender, e);  // add by ksaung
                // ***** For Cash Sale

                //if (lblSaleID.Text != "0")
                //{
                    DataSet ds = new DataSet();
                    //ds = dalsale.SelectSaleVoucher(0,1,txtSystemVoucherNo.Text);//lblSaleID.Text               
                    ds = dalsale.SelectSaleVoucher(0, 0, SysVoucherNo); //add by ksaung

                    int k = ds.Tables.Count;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //this.UseWaitCursor = true;
                        ReportDocument l_Report = new ReportDocument();

                        ds.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_SaleVoucher.xsd"); //Edit by KSAung
                        l_Report.Load(Application.StartupPath + @"\Report\RptSale1.rpt");


                        if (LstCheckPrintAndMsgBox.IsmsgforVoucher)
                        {
                            UI.frmMsgVoucher voucher = new UI.frmMsgVoucher(decimal.Parse(txtGrandTotal.Text));
                            voucher.ShowDialog();
                            l_Report.DataDefinition.FormulaFields[0].Text = "Replace(cstr(" + UI.frmMsgVoucher.PaidAmt.ToString() + "),'.00','')";
                            l_Report.DataDefinition.FormulaFields[1].Text = "Replace(cstr(" + UI.frmMsgVoucher.ChangeAmt.ToString() + "),'.00','')";
                        }
                        else
                        {
                            l_Report.DataDefinition.FormulaFields[0].Text = "Replace(cstr(00),'.00','')";
                            l_Report.DataDefinition.FormulaFields[1].Text = "Replace(cstr(00),'.00','')";
                        }

                        l_Report.SetDataSource(ds);
                        l_Report.SetDatabaseLogon("sa", "moeyan");

                        frmReport frmReport = new frmReport();
                        frmReport.rptViewer.ReportSource = l_Report;
                        frmReport.rptViewer.Refresh();
                        frmReport.Show();
                        this.UseWaitCursor = false;


                    }
                    else
                    {
                        MessageBox.Show("No data for preview");
                    }
                //}                       
   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //if (cboPaymentType.Text == "Cash")
                //{

                    btnSave_Click(sender, e);  // add by ksaung

                    MoeYanPOS.Report.RptSale1 rpt = new Report.RptSale1();

                    ReportDocument l_Report = new ReportDocument();
                    ReportDocument l_Report1 = new ReportDocument();

                    DataSet ds = new DataSet();
                    ds = dalsale.SelectSaleVoucher(0, 0, SysVoucherNo); //add by ksaung

                    List<BOLSystem> lstsystem = new List<BOLSystem>();
                    lstsystem = dalSystem.ShowAllSystem();

                    ds.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_SaleCredit.xsd");
                    ds.Tables[0].TableName = "DS_SaleCredit";

                    if (ds.Tables[0].Rows[0]["ShopName"].ToString() == "Star Moeyan Lottery Enterprise")
                    {
                        l_Report.Load(Application.StartupPath + @"\Report\RptSaleCredit.rpt");
                    }
                    else
                    {
                        l_Report.Load(Application.StartupPath + @"\Report\RptSaleCredit(Customer).rpt");
                    }

                    l_Report1.Load(Application.StartupPath + @"\Report\SaleChargesReport.rpt");

                    if (LstCheckPrintAndMsgBox.IsmsgforVoucher)
                    {
                        UI.frmMsgVoucher voucher = new UI.frmMsgVoucher(decimal.Parse(txtGrandTotal.Text));

                        voucher.ShowDialog();
                        //l_Report.DataDefinition.FormulaFields[0].Text = "Replace(cstr(" + UI.frmMsgVoucher.PaidAmt.ToString() + "),'.00','')";
                        //l_Report.DataDefinition.FormulaFields[1].Text = "Replace(cstr(" + UI.frmMsgVoucher.ChangeAmt.ToString() + "),'.00','')";
                        //l_Report1.DataDefinition.FormulaFields[0].Text = "Replace(cstr(" + UI.frmMsgVoucher.PaidAmt.ToString() + "),'.00','')";
                        //l_Report1.DataDefinition.FormulaFields[1].Text = "Replace(cstr(" + UI.frmMsgVoucher.ChangeAmt.ToString() + "),'.00','')";
                    }
                    else
                    {
                        //l_Report.DataDefinition.FormulaFields[0].Text = "Replace(cstr(00),'.00','')";
                        //l_Report.DataDefinition.FormulaFields[1].Text = "Replace(cstr(00),'.00','')";
                        //l_Report1.DataDefinition.FormulaFields[0].Text = "Replace(cstr(00),'.00','')";
                        //l_Report1.DataDefinition.FormulaFields[1].Text = "Replace(cstr(00),'.00','')";
                    }


                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        // remove by ksaung ..     
                        l_Report.SetDataSource(ds);
                        l_Report.SetDatabaseLogon("sa", "sa123");// comment by htzn 

                        l_Report1.SetDataSource(ds);
                        l_Report1.SetDatabaseLogon("sa", "sa123");// comment by htzn 

                        if (lstsystem.Count > 0)
                        {
                            if (lstsystem[0].Printerslip != "")
                            {


                                //CrystalDecisions.Shared.PageMargins margin;
                                //CrystalDecisions.Shared.PageMargins margins = new CrystalDecisions.Shared.PageMargins();

                                ////// Get the PageMargins structure and set the margins for the report.
                                CrystalDecisions.Shared.PageMargins margin = l_Report.PrintOptions.PageMargins;
                                l_Report.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLegal;

                                //Comment by htzn 
                                //margins.leftMargin = 750;
                                //margin.bottomMargin = 1;
                                //margin.leftMargin = 2; // for shop 103
                                //margin.rightMargin = 250;
                                //margin.topMargin = 1;

                                //PaperSize psize = new PaperSize("Custom", 200, 400);

                                ////// Apply the page margins. 


                                //l_Report.PrintOptions.ApplyPageMargins(margin);

                                l_Report.PrintOptions.PrinterName = lstsystem[0].Printervoucher;
                                l_Report.PrintToPrinter(3, false, 0, 0);

                                //l_Report1.PrintOptions.PrinterName = lstsystem[0].Printerslip;
                                //l_Report1.PrintToPrinter(1, false, 0, 0);

                                //l_Report1.PrintOptions.PrinterName = lstsystem[0].Printerslip;
                                //l_Report1.PrintToPrinter(1, false, 0, 0);

                                //frmReport frmReport = new frmReport();
                                //frmReport.rptViewer.ReportSource = l_Report;
                                //frmReport.rptViewer.RefreshReport();
                                //frmReport.Show();
                                //this.UseWaitCursor = false;

                                //frmReport frmReport1 = new frmReport();
                                //frmReport1.rptViewer.ReportSource = l_Report1;
                                //frmReport1.rptViewer.RefreshReport();
                                //frmReport1.Show();
                                //this.UseWaitCursor = false;
                                                             
                            }

                        }
                        else
                        {
                            MessageBox.Show("Please put printer name at System Setting.");
                            return;
                        }
                    }
                //}
                //else if (cboPaymentType.Text == "Credit")
                //{

                //    if (cboPaymentType.Text == "Credit")
                //    {
                //        string myStr = txtCustomer.Text;
                //        string subStr = myStr.Substring(myStr.Length - 6);

                //        if (subStr == "DF0001")
                //        {
                //            MessageBox.Show("Default Customer not allow to credit.", "POS Alert");
                //            txtCustomer.Focus();
                //            return;
                //        }
                //    }

                //    btnA4Print_Click(sender, e);
                //} 
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
                if (strCheck != "")
                {
                    MessageBox.Show(strCheck);
                    txtDiscount.Text = "0";
                }
                else
                {
                    txtGrandTotal.Text = Convert.ToString(Int32.Parse(txtTransportationAmt.Text) + Int32.Parse(txtTotalAmt.Text) - (Int32.Parse(txtAdvance.Text) + Int32.Parse(txtDiscount.Text) + Int32.Parse(txtTotalFOC.Text) + Int32.Parse(txtItemDiscount.Text)));

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

        private void cboPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboPaymentType.Text == "Credit")
                {
                    if (txtCustomer.Text != "")
                  {
                      string subStr = "";
                      List<BOLCustomer> lstcustomer = new List<BOLCustomer>();
                      string Type = cboPaymentType.SelectedItem.ToString();
                      lstcustomer = dalcustomer.GetCustomer(txtCustomer.Text, Type, 0);

                      if (lstcustomer.Count > 0)
                      {
                          string myStr = txtCustomer.Text;
                          subStr = myStr.Substring(myStr.Length - 6);
                      }
                      else
                      {
                          MessageBox.Show("Customer Is not Allowed your Payment Type!", "Not Allowed Payment Type", MessageBoxButtons.OK);
                          txtCustomer.Text = "";
                          txtCustomerName.Text = "";
                          txtCustomer.Focus();
                      }

                    if (subStr == "DF0001")
                    {
                        MessageBox.Show("Default Customer not allow to credit.","POS Alert");
                        txtCustomer.Focus();
                    }
                    else
                    {
                        txtDayLimit.Enabled = true;
                        txtAdvance.Enabled = true;
                        txtDrawingTimes.Focus();
                    }
                  }
                }
                else
                {
                    txtDayLimit.Text = "0";
                    txtAdvance.Enabled = false;
                    txtDrawingTimes.Focus();
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
                if (e.KeyCode == Keys.Enter)
                {
                    lblCustomerID.Text = dgvCustomer.CurrentRow.Cells[0].Value.ToString();
                    txtCustomer.Text = dgvCustomer.CurrentRow.Cells[1].Value.ToString();
                    txtCustomerName.Text = dgvCustomer.CurrentRow.Cells[2].Value.ToString();
                    txtCreditLimit.Text = dgvCustomer.CurrentRow.Cells[5].Value.ToString();
                   bool chkPaymentType = false;
                   chkPaymentType = dalcustomer.ChkPaymentType(Int32.Parse(lblCustomerID.Text));
                   if (chkPaymentType)
                   {
                       cboPaymentType.Enabled = true;
                   }
                   else
                   {
                       cboPaymentType.Enabled = false;
                   }

                   if (dgvCustomer.CurrentRow.Cells[4].Value.ToString() == "True")
                   {
                       chkwholesale.Checked = true;
                       chkwholesale.Enabled = true;
                   }
                   else
                   {
                       chkwholesale.Checked = false;
                       //chkwholesale.Enabled = false;
                   }
                   dgvCustomer.Visible = false;

                   if (cboPaymentType.Enabled = true )
                   {
                        cboPaymentType.Focus();
                   }
                   else
                    {
                        txtDrawingTimes.Focus();
                    }
                   saveTampSale();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    dgvCustomer.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvItemCode_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            //Closed this event by KTMM Because Key Down Event is on
            //try
            //{
            //    List<BOLCustomer> lstcustomer = new List<BOLCustomer>();
            //    lstcustomer = dalcustomer.GetCustomer(txtCustomer.Text);

            //    if (lstcustomer.Count == 0 & txtCustomer.Text == "Auto")
            //    {
            //        DALCustomer dalcust = new DALCustomer();
            //        BOLCustomer bolcustomer = new BOLCustomer();
            //        bolcustomer.Name = "Auto";
            //        dalcust.SaveCustomer(bolcustomer);

            //        List<BOLCustomer> getlstcustomer = new List<BOLCustomer>();
            //        getlstcustomer = dalcustomer.GetCustomer(txtCustomer.Text);
            //        if (getlstcustomer.Count > 0)
            //        {
            //            lblCustomerID.Text = getlstcustomer[0].CustomerID.ToString();
            //            txtCustomer.Text = getlstcustomer[0].Name;
            //        }
            //    }
            //    else if (lstcustomer.Count > 0)
            //    {
            //        dgvCustomer.Rows.Clear();
            //        foreach (BOLCustomer c in lstcustomer)
            //        {
            //            dgvCustomer.Rows.Add(c.CustomerID, c.Name, c.Address, c.Phone);
            //            if (c.Name == "Auto")
            //            {
            //                lblCustomerID.Text = c.CustomerID.ToString();
            //                txtCustomer.Text = c.Name;
            //                dgvCustomer.Visible = false;
            //            }
            //            else
            //            {
            //                dgvCustomer.Visible = true;
            //            }
            //        }

            //        dgvCustomer.Focus();
            //    }

            //    bool isCrdite = false;
            //    isCrdite = dalcustomer.ChkPaymentType(Int32.Parse(lblCustomerID.Text));
            //    if (isCrdite)
            //    {
            //        cboPaymentType.Enabled = true;
            //    }
            //    else
            //    {
            //        cboPaymentType.Enabled = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
            List<BOLCustomer> bolcustomer = new List<BOLCustomer>();
            bolcustomer = dalcustomer.GetCustomer2(txtCustomer.Text);
            txtCreditAmount.Text = "";
            txtCreditLimit.Text = "";

            foreach (BOLCustomer a in bolcustomer)
            {
                txtCreditAmount.Text = a.CreditAmount.ToString();
                txtCreditLimit.Text = a.Creditlimit.ToString();
            }

            int iCreditLimit; int iCreditAmount;
            iCreditAmount = int.Parse(txtCreditAmount.Text);
            iCreditLimit = int.Parse(txtCreditLimit.Text);
            if (iCreditLimit >= iCreditAmount)
            {
                txtCreditAmount.ForeColor = Color.DarkGreen;
            }
            else
            {
                txtCreditAmount.ForeColor = Color.Maroon;
            }            
        }

        private void cboCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnA4Print_Click(object sender, EventArgs e)
        {
            try
            {
                btnPrint_Click(sender, e);
                //if (cboPaymentType.Text == "Credit")
                //{
                //    string myStr = txtCustomer.Text;
                //    string subStr = myStr.Substring(myStr.Length - 6);

                //    if (subStr == "DF0001")
                //    {
                //        MessageBox.Show("Default Customer not allow to credit.", "POS Alert");
                //        txtCustomer.Focus();
                //        return;
                //    }
                //}

                //btnSave_Click(sender, e);
                //MoeYanPOS.Report.RptSale rpt = new Report.RptSale();
                ////this.UseWaitCursor = true;
                //ReportDocument l_Report = new ReportDocument();
                //ReportDocument l_Report1 = new ReportDocument();
                //DataSet ds = new DataSet();
                ////ds = dalsale.SelectSaleVoucher(long.Parse(lblSaleID.Text), 0, ""); remove by ksaung
                //ds = dalsale.SelectSaleVoucherCredit(SysVoucherNo); //add by ksaung

                //// if (ds.Tables[0].Rows.Count > 0)
                //// {

                ////     ds.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_SaleCredit.xsd");
                ////     if (ds.Tables[0].Rows[0]["ShopName"].ToString() == "Star Moeyan Lottery Enterprise")
                ////     {
                ////         l_Report.Load(Application.StartupPath + @"\Report\RptSaleCredit.rpt");
                ////     }
                ////     else
                ////     {
                ////         l_Report.Load(Application.StartupPath + @"\Report\RptSaleCredit(Customer).rpt");
                ////     }

                ////     l_Report.SetDataSource(ds.Tables[0]);
                ////     l_Report.SetDatabaseLogon("sa", "moeyan");



                //// List<BOLSystem> lstsystem = new List<BOLSystem>();
                //// lstsystem = dalSystem.ShowAllSystem();                

                //// if (lstsystem.Count > 0)
                //// {
                ////     List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                ////     lstvoucherSetting = dalVoucher.SelectAllVoucher();

                ////     DataTable dtt = new DataTable();
                ////     DataColumn dc = new DataColumn();
                ////     DataRow dr;
                ////     dc.ColumnName = "Name";
                ////     dc.DataType = System.Type.GetType("System.String");
                ////     dtt.Columns.Add(dc);

                ////     DataColumn dc1 = new DataColumn();
                ////     dc1.ColumnName = "Logo";
                ////     dc1.DataType = System.Type.GetType("System.Byte[]");
                ////     dtt.Columns.Add(dc1);

                ////     if (lstvoucherSetting.Count > 0)
                ////     {
                ////         for (int i = 0; i < lstvoucherSetting.Count; i++)
                ////         {
                ////              dr = dtt.NewRow();
                ////             dr["Name"] = lstvoucherSetting[0].Name;
                ////             dr["Logo"] = lstvoucherSetting[0].Logo;
                ////             dtt.Rows.Add(dr);
                ////         }

                ////         l_Report.Subreports[0].SetDataSource(dtt);
                ////         //l_Report.Subreports[1].SetDataSource(dtt);
                ////     }

                ////     //l_Report.PrintOptions.PrinterName = lstsystem[0].Printervoucher;
                ////     //l_Report.PrintToPrinter(3, false, 0, 0);

                ////     frmReport frmReport = new frmReport();
                ////     frmReport.rptViewer.ReportSource = l_Report;
                ////     frmReport.rptViewer.RefreshReport();
                ////     frmReport.Show();
                ////     this.UseWaitCursor = false;
                //// }
                //// else
                //// {
                ////     MessageBox.Show("Please put printer name at System Setting.");
                ////     return;
                //// }
                ////}
                //// else
                //// {
                ////     MessageBox.Show("No data to print");
                //// }

                //List<BOLSystem> lstsystem = new List<BOLSystem>();
                //lstsystem = dalSystem.ShowAllSystem();

                //if (ds.Tables[0].Rows[0]["ShopName"].ToString() == "Star Moeyan Lottery Enterprise")
                //{
                //    l_Report.Load(Application.StartupPath + @"\Report\RptSaleCredit.rpt");
                //}
                //else
                //{
                //    l_Report.Load(Application.StartupPath + @"\Report\RptSaleCredit(Customer).rpt");
                //}

                //l_Report1.Load(Application.StartupPath + @"\Report\SaleChargesReport.rpt");

                //if (LstCheckPrintAndMsgBox.IsmsgforVoucher)
                //{
                //    UI.frmMsgVoucher voucher = new UI.frmMsgVoucher(decimal.Parse(txtGrandTotal.Text));
                //    voucher.ShowDialog();
                //}
                //else
                //{
                //}


                //if (ds.Tables[0].Rows.Count > 0)
                //{

                //    // remove by ksaung ..     
                //    l_Report.SetDataSource(ds);
                //    l_Report.SetDatabaseLogon("sa", "sa123");// comment by htzn 

                //    l_Report1.SetDataSource(ds);
                //    l_Report1.SetDatabaseLogon("sa", "sa123");// comment by htzn 

                //    if (lstsystem.Count > 0)
                //    {
                //        if (lstsystem[0].Printerslip != "")
                //        {
                //            CrystalDecisions.Shared.PageMargins margin = l_Report.PrintOptions.PageMargins;
                //            l_Report.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLegal;

                //            l_Report.PrintOptions.ApplyPageMargins(margin);

                //            //l_Report.PrintOptions.PrinterName = lstsystem[0].Printervoucher;
                //            //l_Report.PrintToPrinter(3, false, 0, 0);

                //            l_Report1.PrintOptions.PrinterName = lstsystem[0].Printerslip;
                //            l_Report1.PrintToPrinter(1, false, 0, 0);

                //            //frmReport frmReport = new frmReport();
                //            //frmReport.rptViewer.ReportSource = l_Report;
                //            //frmReport.rptViewer.RefreshReport();
                //            //frmReport.Show();
                //            //this.UseWaitCursor = false;

                //            //frmReport frmReport1 = new frmReport();
                //            //frmReport1.rptViewer.ReportSource = l_Report1;
                //            //frmReport1.rptViewer.RefreshReport();
                //            //frmReport1.Show();
                //            //this.UseWaitCursor = false;

                //        }

                //    }
                //}
                //else
                //{
                //    MessageBox.Show("Please put printer name at System Setting.");
                //    return;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }       

        private void cbocurrency_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                int currencyid = Int32.Parse(cbocurrency.SelectedValue.ToString());
                BOLExchange exchange = new BOLExchange();
                exchange = dalexchangerate.GetExchangeRate(currencyid);
                txtExchangeRate.Text = exchange.Exchangerate.ToString();
                lblCurrencyID.Text = cbocurrency.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmSale_Click(object sender, EventArgs e)
        {
            try
            {
                dgvCustomer.Visible = false;
                dgvItemCode.Visible = false;
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
                CleanTextBox();

                string voucherno = DateTime.Now.ToString("yyMMdd");
                string currentloc = dalLocation.GetCurrentLocationCode();
                string voucher = "INV-" + currentloc + MoeYanFunctions.MoeYanPOS_Helper.counterCode + DateTime.Now.ToString("yyMMdd") + daltrans.GetMaxVoucher("sale").ToString();
                txtSystemVoucherNo.Text = voucher;
                txtVoucherNo.Text = voucher;
                //txtSystemVoucherNo.Text = cboLocation.Text + "_" +voucherno + daltrans.GetTransitionID("Sale").ToString();
                lblTransID.Text = daltrans.GetTransitionID("Sale").ToString();
                //txtVoucherNo.Text = txtSystemVoucherNo.Text;
                //Save Transition
                BOLTransition boltrans = new BOLTransition();
                boltrans.TransName = "Sale";
                boltrans.TransID = daltrans.GetTransitionID("Sale");
                daltrans.SaveTransition(boltrans);
                //saveTampSale();

                //LoadTemp();
                
                //dgvSale.Focus();
                dgvSale.CurrentRow.Cells[1].Selected = true;
                //txtItemCode.Focus();
                txtCustomer.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
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

        private void dgvTemp_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex ==3)
                {
                    if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dalsale.DeleteTempRows(1,dgvTemp.CurrentRow.Cells[2].Value.ToString());
                        LoadTemp();
                        //dgvSale.Rows.Clear();
                    }
                }
                else
                {
                    long Tempsaleid = 0;
                    List<BOLSale> bolSaleList = new List<BOLSale>();
                    bolSaleList = dalsale.GetSaleBySaleID(0, 1, dgvTemp.CurrentRow.Cells[2].Value.ToString());
                    dgvSale.Rows.Clear();

                    if (bolSaleList.Count > 0)
                    {
                        lblSaleID.Text = "0";
                        lblTransID.Text = bolSaleList[0].TranSaleID.ToString();
                        Tempsaleid = bolSaleList[0].SaleID;
                        txtSystemVoucherNo.Text = bolSaleList[0].SystemVoucherNo.ToString();
                        lblUserID.Text = bolSaleList[0].UserID.ToString();
                        txtCustomer.Text = bolSaleList[0].CustomerID.ToString();
                        lblCustomerID.Text = bolSaleList[0].CustomerID.ToString();
                        txtVoucherNo.Text = bolSaleList[0].VoucherNo.ToString();
                        cboPaymentType.Text = bolSaleList[0].PaymentType.ToString();
                        cbocurrency.SelectedValue = Int32.Parse(bolSaleList[0].CurrencyID.ToString());
                        txtTotalAmt.Text = bolSaleList[0].TotalAmt.ToString();
                        txtAdvance.Text = bolSaleList[0].Advance.ToString();
                        txtDiscount.Text = bolSaleList[0].Discount.ToString();
                        txtGrandTotal.Text = bolSaleList[0].GrandTotal.ToString();
                        txtTotalFOC.Text = bolSaleList[0].TotalFOC.ToString();
                        txtItemDiscount.Text = bolSaleList[0].TotalitemDiscount.ToString();
                        txtTransportationAmt.Text = bolSaleList[0].TransportationAmt.ToString();
                        txtRemark.Text = bolSaleList[0].Remark.ToString();
                        dgvCustomer.Visible = false;
                        dgvSale.Rows.Clear();
                        //btnSave.Text = "Save";
                        //if (dgvSale.CurrentRow != null)
                        //{

                        List<BOLSale> lstsaledetail = new List<BOLSale>();
                        lstsaledetail = dalsale.GetSaleDetailList(Tempsaleid, 1);
                        dgvSale.Rows.Clear();

                        if (lstsaledetail.Count > 0)
                        {
                            for (int i = 0; i < lstsaledetail.Count; i++)
                            {
                                dgvSale.Rows.Add();
                                dgvSale.Rows[i].Cells[0].Value = i + 1;
                                dgvSale.Rows[i].Cells[1].Value = lstsaledetail[i].ItemCode.ToString();
                                dgvSale.Rows[i].Cells[2].Value = lstsaledetail[i].Description.ToString();
                                dgvSale.Rows[i].Cells[3].Value = lstsaledetail[i].Mtype.ToString();
                                dgvSale.Rows[i].Cells[4].Value = lstsaledetail[i].Qty.ToString();
                                dgvSale.Rows[i].Cells[5].Value = lstsaledetail[i].SalePrice.ToString();
                                dgvSale.Rows[i].Cells[6].Value = lstsaledetail[i].Total.ToString();
                                dgvSale.Rows[i].Cells[7].Value = lstsaledetail[i].FOC.ToString();
                                dgvSale.Rows[i].Cells[8].Value = lstsaledetail[i].ItemDiscountPercent.ToString();
                                dgvSale.Rows[i].Cells[9].Value = lstsaledetail[i].ItemDiscount.ToString();
                                //dgvSale.Rows[i].Cells[10].Value = lstsaledetail[i].SaleDetailID.ToString();

                            }
                        }
                    }

                    dgvSale.Rows.Add(dgvSale.RowCount + 1, "0000", "", "", "1", "0", "0", colFOC.Items[1].ToString(), "0", "0", "");
                    dgvSale.Rows[dgvSale.RowCount - 1].Cells[1].Selected = true;
                    //dgvSale.Focus();
                    txtItemCode.Focus();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvSale_KeyUp(object sender, KeyEventArgs e)  
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtItemCode_TextChanged(object sender, EventArgs e)
       {
            //try
            //{
            //    //Get Stock
            //    List<BOLStock> lstStk = new List<BOLStock>();
            //    lstStk = dalstock.SelectStock(txtItemCode.Text, 0, "",0);
            //    dgvItemCode.Rows.Clear();
            //    if(lstStk.Count>0)
            //    {
            //        for (int i = 0; i <lstStk.Count; i++)
            //        {
            //            dgvItemCode.Rows.Add(lstStk[i].Id, lstStk[i].ItemCode, lstStk[i].NameEng, lstStk[i].NameMM, lstStk[i].Price,lstStk[i].WholeSalePrice);                        
            //        }
            //        dgvItemCode.Visible = true;
            //        dgvItemCode.Focus();
                    
            //    }
                
                
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
        }

        private void dgvItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    dgvSale.CurrentRow.Cells[1].Value = dgvItemCode.CurrentRow.Cells[1].Value.ToString();
                    dgvSale.CurrentRow.Cells[2].Value = dgvItemCode.CurrentRow.Cells[3].Value.ToString();
                    if (chkwholesale.Checked)
                    {
                        dgvSale.CurrentRow.Cells[5].Value = dgvItemCode.CurrentRow.Cells[5].Value.ToString();
                    }
                    else
                    {
                        dgvSale.CurrentRow.Cells[5].Value = dgvItemCode.CurrentRow.Cells[4].Value.ToString();
                    }
                    dgvSale.CurrentRow.Cells[3].Value = dgvItemCode.CurrentRow.Cells[6].Value.ToString();
                    txtItemCode.Text = "";
                    dgvItemCode.Visible = false;                    
                    dgvSale.Focus();
                    dgvSale.CurrentRow.Cells[4].Selected = true;
                    txtItemCode.BackColor = Color.White;
                    //Save_TempTables();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    dgvItemCode.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnEditExchangeRate_Click(object sender, EventArgs e)
        {
            try
            {
                DALCurrency dalcurrency = new DALCurrency();
                BOLCurrency bolcurrency = new BOLCurrency();
                bolcurrency.Id = Int32.Parse(lblCurrencyID.Text);
                bolcurrency.Exchangerate = Int32.Parse(txtExchangeRate.Text);
                bolcurrency.Currency = cbocurrency.Text;                

                if (Int32.Parse(lblCurrencyID.Text) != 0)
                {
                    dalcurrency.EditCurrency(bolcurrency);
                }               


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtVoucherNo_KeyDown(object sender, KeyEventArgs e)
        {
            //try
            //{
            //    if (e.KeyCode == Keys.Enter | e.KeyCode == Keys.LButton)
            //    {
                   
            //        List<BOLSale> getvoucher = new List<BOLSale>();
            //        getvoucher = dalsale.DuplicateVoucher(txtVoucherNo.Text);
            //        if (getvoucher.Count > 0)
            //        {
            //            MessageBox.Show("Duplicate Voucher No");
            //            txtVoucherNo.Focus();
            //            return;
            //        }

            //        cboPaymentType.Enabled = true;
            //        cboPaymentType.Focus();
            //        //dgvSale.Focus();
            //        //dgvSale.Rows[0].Cells[1].Selected = true;

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
        }

        private void pnlHeader_Click(object sender, EventArgs e)
        {
            try
            {
                dgvItemCode.Visible = false;
                dgvCustomer.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtAdvance_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strCheck = "";
                strCheck = MoeYanPOS.Function.Validation.isNumberField("Advance", txtAdvance.Text);
                if (strCheck != "")
                {
                    MessageBox.Show(strCheck);
                    txtAdvance.Text = "0";
                }
                else
                {
                    txtGrandTotal.Text = Convert.ToString(Int32.Parse(txtTotalAmt.Text) - (Int32.Parse(txtAdvance.Text) + Int32.Parse(txtDiscount.Text) + Int32.Parse(txtTotalFOC.Text) + Int32.Parse(txtItemDiscount.Text)));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtTotalAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
               string strCheck = "";
               strCheck=MoeYanPOS.Function.Validation.isNumberField("Total Amount", txtTotalAmt.Text);
               if (strCheck != "")
               {
                   MessageBox.Show(strCheck);                  
               }
            }
             catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtGrandTotal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strCheck = "";
                strCheck = MoeYanPOS.Function.Validation.isNumberField("Grand Total", txtGrandTotal.Text);
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

        private void dgvSale_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (dgvSale.CurrentCell.ColumnIndex == 1)
            //    {
            //        if (dgvSale.CurrentRow.Cells[1].Value.ToString() == "0000")
            //        {
            //            if (cboPaymentType.Text == "Cash")
            //            {
            //                txtAdvance.Enabled = false;
            //                txtDiscount.Focus();
            //            }
            //            else if (cboPaymentType.Text == "Credit")
            //            {
            //                txtAdvance.Enabled = true;
            //                txtAdvance.Focus();
            //            }
            //        }
            //        else
            //        {
            //            List<BOLStock> lstStk = new List<BOLStock>();
            //            lstStk = dalstock.SelectStock("", 0, dgvSale.CurrentRow.Cells[1].Value.ToString());
            //            if (lstStk.Count > 0)
            //            {
            //                dgvSale.CurrentRow.Cells[2].Value = lstStk[0].NameEng;
            //                dgvSale.CurrentRow.Cells[5].Value = lstStk[0].Price;
            //                dgvSale.CurrentCell = dgvSale.CurrentRow.Cells[4];
            //            }
            //            else
            //            {
            //                MessageBox.Show("Enter correct Item Code!");
            //                txtItemCode.Focus();
            //            }

            //        }
            //    }

            //    else if (dgvSale.CurrentCell.ColumnIndex == 2)
            //    {
            //        dgvSale.CurrentCell = dgvSale.CurrentRow.Cells[4];
            //    }

            //    else if (dgvSale.CurrentCell.ColumnIndex == 4)
            //    {
            //        string qtyCheck = "";
            //        qtyCheck = MoeYanPOS.Function.Validation.isNumberField("Qty", dgvSale.CurrentRow.Cells[4].Value.ToString());
            //        if (qtyCheck != "")
            //        {
            //            MessageBox.Show(qtyCheck);
            //            dgvSale.CurrentRow.Cells[4].Value = 1;
            //        }
            //        dgvSale.CurrentCell = dgvSale.CurrentRow.Cells[5];
            //    }

            //    else if (dgvSale.CurrentCell.ColumnIndex == 5)
            //    {
            //        dgvSale.CurrentRow.Cells[6].Value = Convert.ToString(Int32.Parse(dgvSale.CurrentRow.Cells[4].Value.ToString()) * Int32.Parse(dgvSale.CurrentRow.Cells[5].Value.ToString()));

            //        int totalprice = 0;
            //        for (int i = 0; i < dgvSale.Rows.Count; i++)
            //        {
            //            if (dgvSale.Rows[i].Cells[7].Value.ToString() == "False")
            //            {
            //                totalprice += Int32.Parse(dgvSale.Rows[i].Cells[6].Value.ToString());
            //            }
            //        }

            //        txtTotalAmt.Text = totalprice.ToString();
            //        dgvSale.CurrentCell = dgvSale.CurrentRow.Cells[6];

            //    }
            //    else if (dgvSale.CurrentCell.ColumnIndex == 7)
            //    {
            //        if (dgvSale.CurrentRow.Cells[7].Value.ToString() == "True")
            //        {
            //            dgvSale.CurrentRow.Cells[8].ReadOnly = true;
            //            dgvSale.CurrentRow.Cells[9].ReadOnly = true;
            //        }
            //        else
            //        {
            //            dgvSale.CurrentRow.Cells[8].ReadOnly = false;
            //            dgvSale.CurrentRow.Cells[9].ReadOnly = false;
            //        }

            //        dgvSale.CurrentCell = dgvSale.CurrentRow.Cells[8];
            //    }
            //    else if (dgvSale.CurrentCell.ColumnIndex == 8)
            //    {
            //        string DisCheck = "";
            //        DisCheck = MoeYanPOS.Function.Validation.isNumberField("Item Discount %", dgvSale.CurrentRow.Cells[8].Value.ToString());
            //        if (DisCheck != "")
            //        {
            //            MessageBox.Show(DisCheck);
            //            dgvSale.CurrentRow.Cells[9].Value = "0";
            //        }
            //        else
            //        {
            //            int dispercent = 0;
            //            dispercent = (Int32.Parse(dgvSale.CurrentRow.Cells[8].Value.ToString()) * Int32.Parse(dgvSale.CurrentRow.Cells[6].Value.ToString())) / 100;
            //            dgvSale.CurrentRow.Cells[9].Value = dispercent.ToString();
            //        }
            //        dgvSale.CurrentCell = dgvSale.CurrentRow.Cells[9];
            //    }


            //    else if (dgvSale.CurrentCell.ColumnIndex != 9)
            //    {
            //        dgvSale.CurrentCell = dgvSale.CurrentRow.Cells[dgvSale.CurrentCell.ColumnIndex + 1];
            //    }
            //    else
            //    {
            //        string DisCheck = "";
            //        DisCheck = MoeYanPOS.Function.Validation.isNumberField("Item Discount", dgvSale.CurrentRow.Cells[9].Value.ToString());
            //        if (DisCheck != "")
            //        {
            //            MessageBox.Show(DisCheck);
            //            dgvSale.CurrentRow.Cells[9].Value = 0;
            //        }
            //        else
            //        {
            //            dgvSale.Rows.Add();
            //            dgvSale.CurrentCell = dgvSale.CurrentRow.Cells[0];
            //            dgvSale.CurrentRow.Cells[0].Value = dgvSale.Rows.Count;
            //            dgvSale.CurrentRow.Cells[1].Value = "0000";
            //            dgvSale.CurrentRow.Cells[4].Value = "1";
            //            dgvSale.CurrentRow.Cells[5].Value = "0";
            //            dgvSale.CurrentRow.Cells[6].Value = "0";
            //            dgvSale.CurrentRow.Cells[8].Value = "0";
            //            dgvSale.CurrentRow.Cells[9].Value = "0";
            //            dgvSale.CurrentRow.Cells[7].Value = colFOC.Items[1].ToString();


            //            decimal TotalDis = 0; decimal TotalFOC = 0; decimal TotalAmt = 0;

            //            for (int i = 0; i < dgvSale.Rows.Count; i++)
            //            {
            //                TotalDis += Decimal.Parse(dgvSale.Rows[i].Cells[9].Value.ToString());
            //            }

            //            for (int i = 0; i < dgvSale.Rows.Count; i++)
            //            {
            //                //if (dgvSale.Rows[i].Cells[7].Value.ToString() == "False")
            //                //{
            //                TotalAmt += Decimal.Parse(dgvSale.Rows[i].Cells[6].Value.ToString());
            //                //}
            //            }

            //            for (int i = 0; i < dgvSale.Rows.Count; i++)
            //            {
            //                if (dgvSale.Rows[i].Cells[7].Value.ToString() == "True")
            //                {
            //                    TotalFOC += Decimal.Parse(dgvSale.Rows[i].Cells[6].Value.ToString()); ;
            //                }
            //            }

            //            //txtDiscount.Text = TotalDis.ToString();
            //            txtTotalAmt.Text = TotalAmt.ToString();
            //            txtTotalFOC.Text = TotalFOC.ToString();
            //            txtItemDiscount.Text = TotalDis.ToString();
            //            txtGrandTotal.Text = Convert.ToString(Int32.Parse(txtTotalAmt.Text) - (Int32.Parse(txtAdvance.Text) + Int32.Parse(txtDiscount.Text) + Int32.Parse(txtTotalFOC.Text) + Int32.Parse(txtItemDiscount.Text)));
            //            dgvSale.CurrentCell = dgvSale.CurrentRow.Cells[1];
            //            Save_TempTables();
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
        }

        private void dgvSale_CellLeave()
        {
            try
            {
                if (dgvSale.CurrentCell.ColumnIndex == 7)
                    {
                        //dgvSale_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
                        dgvSale.CurrentCell = dgvSale.CurrentRow.Cells[8];
                    }
                if (dgvSale.CurrentCell.ColumnIndex == 9)
                {
                    //dgvSale_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
                    dgvSale.CurrentCell = dgvSale.CurrentRow.Cells[10];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtExchangeRate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strCheck = "";
                strCheck = MoeYanPOS.Function.Validation.isNumberField("Exchange Rate", txtExchangeRate.Text);
                if (strCheck != "")
                {
                    MessageBox.Show(strCheck);
                    txtExchangeRate.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtDayLimit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strCheck = "";
                strCheck = MoeYanPOS.Function.Validation.isNumberField("Exchange Rate", txtExchangeRate.Text);
                if (strCheck != "")
                {
                    MessageBox.Show(strCheck);
                    txtExchangeRate.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmSale_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F5)
                {
                    btnSave_Click(sender, e);
                }
                else if (e.KeyCode == Keys.F7)
                {
                    btnPreviewVoucher_Click(sender, e);
                }
                if (e.KeyCode == Keys.F8)
                {
                    btnNew_Click(sender, e);
                }
                else if (e.KeyCode == Keys.F6)
                {
                    btnA4Print_Click(sender, e);
                }
                else if (e.KeyCode == Keys.F2)
                {
                    txtPending2_Click(sender, e);
                }
                else if (e.KeyCode == Keys.F3)
                {
                    txtPending3_Click(sender, e);
                }
                else if (e.KeyCode == Keys.F10)
                {
                    btnPrint_Click(sender, e);
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    dgvCustomer.Visible = false;
                    dgvItemCode.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnPreviewVoucher_Click(object sender, EventArgs e)
        {
            try
            {
                // ***** For Credit Sale

                //if (lblSaleID.Text != "0")
                //{
                //    MoeYanPOS.Report.RptSale rpt = new Report.RptSale();
                   // this.UseWaitCursor = true;
                    ReportDocument l_Report = new ReportDocument();
                    DataSet ds = new DataSet();
                    ds = dalsale.SelectSaleVoucher(0, 0, SysVoucherNo);

                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        ds.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_Sale.xsd");
                        if (ds.Tables[0].Rows[0]["ShopName"].ToString() == "Star Moeyan Lottery Enterprise")
                        {
                            l_Report.Load(Application.StartupPath + @"\Report\RptSaleCredit.rpt");
                        }
                        else
                        {
                            l_Report.Load(Application.StartupPath + @"\Report\RptSaleCredit(Customer).rpt");
                        }

                        l_Report.SetDataSource(ds.Tables[0]);
                        l_Report.SetDatabaseLogon("sa", "moeyan");

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

                        frmReport frmReport = new frmReport();
                        frmReport.rptViewer.ReportSource = l_Report;
                        frmReport.rptViewer.Refresh();
                        frmReport.Show();
                        this.UseWaitCursor = false;
                    }
                    else
                    {
                        MessageBox.Show("No data for preview");
                    }
                //}
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnSave_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void btnNew_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F5)
                {
                    btnSave_Click(sender, e);
                }

                if (e.KeyCode == Keys.F6)
                {
                    btnPreviewVoucher_Click(sender, e);
                }

                if (e.KeyCode == Keys.F7)
                {
                    btnA4Print_Click(sender, e);
                }

                if (e.KeyCode == Keys.F8)
                {
                    btnNew_Click(sender, e);
                }

                if (e.KeyCode == Keys.F9)
                {
                    btnClose_Click(sender, e);
                }

                if (e.KeyCode == Keys.F2)
                {
                    txtPending2_Click(sender, e);
                }

                if (e.KeyCode == Keys.F3)
                {
                    txtPending3_Click(sender, e);
                }
                else if (e.KeyCode == Keys.F10)
                {
                    btnPrint_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                frmCustomer customer = new frmCustomer();
                customer.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //lblCustomerID.Text = dgvCustomer.CurrentRow.Cells[0].Value.ToString();
                //txtCustomer.Text = dgvCustomer.CurrentRow.Cells[1].Value.ToString();
                //bool chkPaymentType = false;
                //chkPaymentType = dalcustomer.ChkPaymentType(Int32.Parse(lblCustomerID.Text));
                //if (chkPaymentType)
                //{
                //    cboPaymentType.Enabled = true;
                //}
                //else
                //{
                //    cboPaymentType.Enabled = false;
                //}

                //if (dgvCustomer.CurrentRow.Cells[4].Value.ToString() == "True")
                //{
                //    chkwholesale.Checked = true;
                //    chkwholesale.Enabled = true;
                //}
                //else
                //{
                //    chkwholesale.Checked = false;
                //    //chkwholesale.Enabled = false;
                //}
                //dgvCustomer.Visible = false;
                //txtVoucherNo.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvItemCode_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                    //dgvSale.CurrentRow.Cells[1].Value = dgvItemCode.CurrentRow.Cells[1].Value.ToString();
                    //dgvSale.CurrentRow.Cells[2].Value = dgvItemCode.CurrentRow.Cells[2].Value.ToString();
                    //if (chkwholesale.Checked)
                    //{
                    //    dgvSale.CurrentRow.Cells[5].Value = dgvItemCode.CurrentRow.Cells[5].Value.ToString();
                    //}
                    //else
                    //{
                    //    dgvSale.CurrentRow.Cells[5].Value = dgvItemCode.CurrentRow.Cells[4].Value.ToString();
                    //}
                    //txtItemCode.Text = "";
                    //dgvItemCode.Visible = false;
                    //dgvSale.Focus();
                    //dgvSale.CurrentRow.Cells[4].Selected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvTemp_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dalsale.DeleteTempRows(1,dgvTemp.CurrentRow.Cells[2].Value.ToString());
                        LoadTemp();
                        dgvSale.Rows.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvTemp_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dalsale.DeleteTempRows(0,dgvTemp.CurrentRow.Cells[2].Value.ToString());
                        LoadTemp();
                        dgvSale.Rows.Clear();
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
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //Get Stock
                    List<BOLStock> lstStk = new List<BOLStock>();
                    lstStk = dalstock.SelectStock(txtItemCode.Text, 0, "", 0);
                    dgvItemCode.Rows.Clear();
                    if (lstStk.Count > 0)
                    {
                        for (int i = 0; i < lstStk.Count; i++)
                        {
                            dgvItemCode.Rows.Add(lstStk[i].Id, lstStk[i].ItemCode, lstStk[i].NameMM, lstStk[i].NameMM, lstStk[i].Price, lstStk[i].WholeSalePrice, lstStk[i].MBCTypeID);
                        }
                        dgvItemCode.Visible = true;
                        dgvItemCode.Focus();

                    }   
                }
                else if (e.KeyCode == Keys.F5)
                {
                    btnSave_Click(sender, e);
                }
                if (e.KeyCode == Keys.F8)
                {
                    btnNew_Click(sender, e);
                }
                else if (e.KeyCode == Keys.F10)
                {
                    btnPrint_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvTemp_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                long Tempsaleid = 0;
                List<BOLSale> bolSaleList = new List<BOLSale>();
                bolSaleList = dalsale.GetSaleBySaleID(0, 1, dgvTemp.CurrentRow.Cells[2].Value.ToString());
                dgvSale.Rows.Clear();

                if (bolSaleList.Count > 0)
                {
                    lblSaleID.Text = "0";
                    lblTransID.Text = bolSaleList[0].TranSaleID.ToString();
                    Tempsaleid = bolSaleList[0].SaleID;
                    txtSystemVoucherNo.Text = bolSaleList[0].SystemVoucherNo.ToString();
                    lblUserID.Text = bolSaleList[0].UserID.ToString();
                    txtCustomer.Text = bolSaleList[0].CustomerID.ToString();
                    lblCustomerID.Text = bolSaleList[0].CustomerID.ToString();
                    txtVoucherNo.Text = bolSaleList[0].VoucherNo.ToString();
                    cboPaymentType.Text = bolSaleList[0].PaymentType.ToString();
                    cbocurrency.SelectedValue = Int32.Parse(bolSaleList[0].CurrencyID.ToString());
                    txtTotalAmt.Text = bolSaleList[0].TotalAmt.ToString();
                    txtAdvance.Text = bolSaleList[0].Advance.ToString();
                    txtDiscount.Text = bolSaleList[0].Discount.ToString();
                    txtGrandTotal.Text = bolSaleList[0].GrandTotal.ToString();
                    txtTotalFOC.Text = bolSaleList[0].TotalFOC.ToString();
                    txtItemDiscount.Text = bolSaleList[0].TotalitemDiscount.ToString();
                    dgvCustomer.Visible = false;
                    //dgvSale.Rows.Clear();
                    //btnSave.Text = "Save";
                    //if (dgvSale.CurrentRow != null)
                    //{

                    List<BOLSale> lstsaledetail = new List<BOLSale>();
                    lstsaledetail = dalsale.GetSaleDetailList(Tempsaleid, 1);
                    //dgvSale.Rows.Clear();

                    if (lstsaledetail.Count > 0)
                    {
                        //dgvSale.Rows.Clear();
                        for (int i = 0; i < lstsaledetail.Count; i++)
                        {
                            dgvSale.Rows.Add(dgvSale.Rows.Count + 1, "0000", "", "", "1", "0", "0", colFOC.Items[1].ToString(), "0", "0", "");
                            dgvSale.Rows[i].Cells[0].Value = i + 1;
                            dgvSale.Rows[i].Cells[1].Value = lstsaledetail[i].ItemCode.ToString();
                            dgvSale.Rows[i].Cells[2].Value = lstsaledetail[i].Description.ToString();
                            dgvSale.Rows[i].Cells[3].Value = lstsaledetail[i].Mtype.ToString();
                            dgvSale.Rows[i].Cells[4].Value = lstsaledetail[i].Qty.ToString();
                            dgvSale.Rows[i].Cells[5].Value = lstsaledetail[i].SalePrice.ToString();
                            dgvSale.Rows[i].Cells[6].Value = lstsaledetail[i].Total.ToString();
                            dgvSale.Rows[i].Cells[7].Value = lstsaledetail[i].FOC.ToString();
                            dgvSale.Rows[i].Cells[8].Value = lstsaledetail[i].ItemDiscountPercent.ToString();
                            dgvSale.Rows[i].Cells[9].Value = lstsaledetail[i].ItemDiscount.ToString();
                            //dgvSale.Rows[i].Cells[10].Value = lstsaledetail[i].SaleDetailID.ToString();
                        }

                    }
                    dgvSale.Rows.Add(dgvSale.Rows.Count + 1, "0000", "", "", "1", "0", "0", colFOC.Items[1].ToString(), "0", "0", "");
                    dgvSale.CurrentRow.Cells[1].Selected = true;
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
                    string Type = cboPaymentType.SelectedItem.ToString();
                    lstcustomer = dalcustomer.GetCustomer(txtCustomer.Text,Type,0);
                    dgvCustomer.Rows.Clear();

                    foreach (BOLCustomer c in lstcustomer)
                    {
                        dgvCustomer.Rows.Add(c.ID, c.CustomerID, c.MyanmarName, c.Address, c.Wholesaleprice,c.Creditlimit);
                    }
                    
                    dgvCustomer.Visible = true;
                    dgvCustomer.Focus();

                    // Closed this event by HTZN Because Text Change Event is on

                   // List<BOLCustomer> bolcustomer = new List<BOLCustomer>();
                   // bolcustomer = dalcustomer.GetCustomer2(txtCustomer.Text);
                   // txtCreditAmount.Text = "";
                   // txtCreditLimit.Text = "";

                   // foreach (BOLCustomer a in bolcustomer)
                   // {
                   //     txtCreditAmount.Text = a.CreditAmount.ToString();
                   //     txtCreditLimit.Text = a.Creditlimit.ToString();
                   // }

                   // int iCreditLimit; int iCreditAmount;
                   //iCreditAmount = int.Parse(txtCreditAmount.Text);
                   //iCreditLimit = int.Parse(txtCreditLimit.Text);
                   // if ( iCreditLimit >= iCreditAmount) 
                   //{
                   //    txtCreditAmount.ForeColor = Color.DarkGreen;
                   //}
                   //else
                   //{
                   //    txtCreditAmount.ForeColor = Color.Maroon;
                   //}
                }
               if (e.KeyCode == Keys.F5)
               {
                   btnSave_Click(sender, e);
               }

               if (e.KeyCode == Keys.F6)
               {
                   btnPreviewVoucher_Click(sender, e);
               }

               if (e.KeyCode == Keys.F7)
               {
                   btnA4Print_Click(sender, e);
               }

               if (e.KeyCode == Keys.F8)
               {
                   btnNew_Click(sender, e);
               }

               if (e.KeyCode == Keys.F9)
               {
                   btnClose_Click(sender, e);
               }

               if (e.KeyCode == Keys.F2)
               {
                   txtPending2_Click(sender, e);
               }

               if (e.KeyCode == Keys.F3)
               {
                   txtPending3_Click(sender, e);
               }
               else if (e.KeyCode == Keys.F10)
               {
                   btnPrint_Click(sender, e);
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

        private void dgvTemp_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                long Tempsaleid = 0;
                List<BOLSale> bolSaleList = new List<BOLSale>();
                bolSaleList = dalsale.GetSaleBySaleID(0, 1, dgvTemp.CurrentRow.Cells[2].Value.ToString());
                dgvSale.Rows.Clear();

                if (bolSaleList.Count > 0)
                {
                    lblSaleID.Text = "0";
                    lblTransID.Text = bolSaleList[0].TranSaleID.ToString();
                    Tempsaleid = bolSaleList[0].SaleID;
                    txtSystemVoucherNo.Text = bolSaleList[0].SystemVoucherNo.ToString();
                    lblUserID.Text = bolSaleList[0].UserID.ToString();
                    txtCustomer.Text = bolSaleList[0].CustomerID.ToString();
                    lblCustomerID.Text = bolSaleList[0].CustomerID;
                    txtVoucherNo.Text = bolSaleList[0].VoucherNo.ToString();
                    cboPaymentType.Text = bolSaleList[0].PaymentType.ToString();
                    cbocurrency.SelectedValue = Int32.Parse(bolSaleList[0].CurrencyID.ToString());
                    txtTotalAmt.Text = bolSaleList[0].TotalAmt.ToString();
                    txtAdvance.Text = bolSaleList[0].Advance.ToString();
                    txtDiscount.Text = bolSaleList[0].Discount.ToString();
                    txtGrandTotal.Text = bolSaleList[0].GrandTotal.ToString();
                    txtTotalFOC.Text = bolSaleList[0].TotalFOC.ToString();
                    txtRemark.Text = bolSaleList[0].Remark.ToString();
                    txtItemDiscount.Text = bolSaleList[0].TotalitemDiscount.ToString();
                    dgvCustomer.Visible = false;
                    //dgvSale.Rows.Clear();
                    //btnSave.Text = "Save";
                    //if (dgvSale.CurrentRow != null)
                    //{

                    List<BOLSale> lstsaledetail = new List<BOLSale>();
                    lstsaledetail = dalsale.GetSaleDetailList(Tempsaleid, 1);
                    //dgvSale.Rows.Clear();

                    if (lstsaledetail.Count > 0)
                    {
                        //dgvSale.Rows.Clear();
                        for (int i = 0; i < lstsaledetail.Count; i++)
                        {
                            dgvSale.Rows.Add(dgvSale.Rows.Count + 1, "0000", "", "", "1", "0", "0", colFOC.Items[1].ToString(), "0", "0", "");
                            dgvSale.Rows[i].Cells[0].Value = i + 1;
                            dgvSale.Rows[i].Cells[1].Value = lstsaledetail[i].ItemCode.ToString();
                            dgvSale.Rows[i].Cells[2].Value = lstsaledetail[i].Description.ToString();
                            dgvSale.Rows[i].Cells[3].Value = lstsaledetail[i].Mtype.ToString();
                            dgvSale.Rows[i].Cells[4].Value = lstsaledetail[i].Qty.ToString();
                            dgvSale.Rows[i].Cells[5].Value = lstsaledetail[i].SalePrice.ToString();
                            dgvSale.Rows[i].Cells[6].Value = lstsaledetail[i].Total.ToString();
                            dgvSale.Rows[i].Cells[7].Value = lstsaledetail[i].FOC.ToString();
                            dgvSale.Rows[i].Cells[8].Value = lstsaledetail[i].ItemDiscountPercent.ToString();
                            dgvSale.Rows[i].Cells[9].Value = lstsaledetail[i].ItemDiscount.ToString();
                            //dgvSale.Rows[i].Cells[10].Value = lstsaledetail[i].SaleDetailID.ToString();
                        }

                    }
                    dgvSale.Rows.Add(dgvSale.Rows.Count + 1, "0000", "", "", "1", "0", "0", colFOC.Items[1].ToString(), "0", "0", "");
                    dgvSale.CurrentRow.Cells[1].Selected = true;
                    dgvSale.Focus();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void lblCustomerID_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void chkwholesale_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblCustomerID_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvSale_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtTransportationAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strCheck = "";
                strCheck = MoeYanPOS.Function.Validation.isNumberField("Discount", txtDiscount.Text);
                if (strCheck != "")
                {
                    MessageBox.Show(strCheck);
                    txtTransportationAmt.Text = "0";
                }
                else
                {
                    txtGrandTotal.Text = Convert.ToString(Int32.Parse(txtTransportationAmt.Text) + Int32.Parse(txtTotalAmt.Text) - (Int32.Parse(txtAdvance.Text) + Int32.Parse(txtDiscount.Text) + Int32.Parse(txtTotalFOC.Text) + Int32.Parse(txtItemDiscount.Text)));

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtSystemVoucherNo_KeyDown(object sender, KeyEventArgs e)
        {
          
        }

        private void txtVoucherNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPending2_Click(object sender, EventArgs e)
        {  
            System.Windows.Forms.Form f1 = System.Windows.Forms.Application.OpenForms["frmSale2"];
            if (((frmSale2)f1) != null)
            {
                Application.OpenForms["frmSale2"].BringToFront();
            }
            else
            {
                frmSale2 sale2 = new frmSale2();
                sale2.Show();
            }
        }

        private void txtPending3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Form f1 = System.Windows.Forms.Application.OpenForms["frmSale3"];
            if (((frmSale3)f1) != null)
            {
                Application.OpenForms["frmSale3"].BringToFront();
            }
            else
            {
                frmSale3 sale3 = new frmSale3();
                sale3.Show();
            }
        }

        private void txtDrawingTimes_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDrawingTimes_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtItemCode.Focus();
                    txtItemCode.BackColor = Color.LightGreen;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dtpSaleDate_ValueChanged(object sender, EventArgs e)
        {
            txtSystemVoucherNo.Text = "";
            txtSystemVoucherNo.Text = dalsale.GetMaxVoucherNo("sale", dtpSaleDate.Value).ToString();
            txtVoucherNo.Text = dalsale.GetMaxVoucherNo("sale", dtpSaleDate.Value).ToString();
        }

        private void dtpSaleDate_KeyDown(object sender, KeyEventArgs e)
        {
            txtSystemVoucherNo.Text = "";
            txtSystemVoucherNo.Text = dalsale.GetMaxVoucherNo("sale", dtpSaleDate.Value).ToString();
            txtVoucherNo.Text = dalsale.GetMaxVoucherNo("sale", dtpSaleDate.Value).ToString();
        }

        private void frmSale_FormClosed(object sender, FormClosedEventArgs e)
        {
            BOLCounter blocounter = new BOLCounter();
            blocounter.Code = MoeYanFunctions.MoeYanPOS_Helper.counterCode;
            blocounter.Name = MoeYanFunctions.MoeYanPOS_Helper.counterName;
            blocounter.IsthisLocation = false;
            blocounter.IsDelete = false;
            dalcounter.updateCounter(blocounter);
        }
    }
}
