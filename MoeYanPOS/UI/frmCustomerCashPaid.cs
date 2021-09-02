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

namespace MoeYanPOS.UI
{
    public partial class frmCustomerCashPaid : Form
    {
        DALSale dalsale = new DALSale();
        DALCustomer dalcustomer = new DALCustomer();
        DALLocation dalLocation = new DALLocation();
        DALPayForCredit dalpaycredit = new DALPayForCredit();
        DALVoucherSetting dalVoucher = new DALVoucherSetting();
        DALSystem dalSystem = new DALSystem();
        string CRPaymentType = "";

        public frmCustomerCashPaid(string VoucherNo,string CustomerName,long CustomerID,long LocationID)
        {
            try
            {                
                InitializeComponent();
                LoadLocation();
                txtCreditTotal.Text = dalpaycredit.GetTotalCreditAmount(VoucherNo).ToString();
                txtCustomer.Text = CustomerName;
                lblCustomerID.Text = CustomerID.ToString();
                string currentloc = dalLocation.GetCurrentLocationCode();
                string voucher = currentloc + DateTime.Now.ToString("yyMMdd") + dalsale.GetMaxVoucher("payforcredit").ToString(); 
                txtVoucherNo.Text = voucher;
                txtSaleVoucherNo.Text = VoucherNo;
                if (LocationID != 0)
                {
                    cboLocation.SelectedValue = LocationID;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public frmCustomerCashPaid()
        {
            try
            {
                InitializeComponent();
                LoadLocation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void LoadLocation()
        {
            try
            {
                List<BolLocation> LstLocation = new List<BolLocation>();
                LstLocation = dalLocation.SelectAllLocation();
                cboLocation.DisplayMember = "Location";
                cboLocation.ValueMember = "ID";
                cboLocation.DataSource = LstLocation;
                for (int i = 0; i < LstLocation.Count; i++)
                {
                    if (LstLocation[i].IsThisLocation)
                    {
                        cboLocation.SelectedValue = LstLocation[i].ID;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        public frmCustomerCashPaid(long ID,DateTime DT)
        {
            try
            {
                InitializeComponent();
                LoadLocation();
                List<BOLPayForCredit> bolpayforcredit = new List<BOLPayForCredit>();
                bolpayforcredit = dalpaycredit.GetPayByPayID(ID);

                if (bolpayforcredit.Count > 0)
                {
                    if (ID != 0)
                    {
                        lblID.Text = ID.ToString();
                        dtppurchasedate.Text =DT.ToString();
                        txtCreditTotal.Text= dalpaycredit.GetTotalCreditAmount(bolpayforcredit[0].VoucherNo.ToString()).ToString();
                        lblCustomerID.Text = bolpayforcredit[0].Cid.ToString();
                        txtAmt.Text = bolpayforcredit[0].Amt.ToString();
                        txtCustomer.Text = bolpayforcredit[0].Name.ToString();
                        txtSaleVoucherNo.Text = bolpayforcredit[0].SaleVoucher.ToString();
                        txtVoucherNo.Text = bolpayforcredit[0].VoucherNo.ToString();
                        cboLocation.SelectedValue = bolpayforcredit[0].LocationID;
                        txtCashReceiveVoucherNo.Text = bolpayforcredit[0].CashReceiveVoucherNo.ToString();
                        CRPaymentType = bolpayforcredit[0].CRPaymenttype;

                        if (CRPaymentType == "Cash")
                        { 
                            rdoCash.Checked =true;
                        }
                        else
                        {
                             rdoCheque.Checked =true; 
                        }

                        btnSave.Text = "&Update";

                        //if (btnSave.Text == "&Update")
                        //{
                        //    txtAmt.Focus();
                        //}
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
                    List<BOLCustomer> lstcustomer = new List<BOLCustomer>();
                    lstcustomer = dalcustomer.GetCustomer(txtCustomer.Text, "Cash",0);

                    dgvCustomer.Rows.Clear();
                    foreach (BOLCustomer c in lstcustomer)
                    {
                        dgvCustomer.Rows.Add(c.ID, c.CustomerID, c.Name, c.Address, c.Wholesaleprice);
                    }

                    dgvCustomer.Focus();
                    dgvCustomer.Visible = true;
                    
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
                    dgvCustomer.Visible = false;
                    txtSaleVoucherNo.Focus();
                   
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
                    txtCreditTotal.Focus();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtCreditTotal_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtAmt.Focus();
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

        private void txtCreditTotal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strCheck = "";
                strCheck = MoeYanPOS.Function.Validation.isNumberField("Total Credit", txtCreditTotal.Text);
                if (strCheck != "")
                {
                    MessageBox.Show(strCheck);
                    txtCreditTotal.Text = "0";
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strCheck = "";
                strCheck = MoeYanPOS.Function.Validation.isNumberField("Amount", txtAmt.Text);
                if (strCheck != "")
                {
                    MessageBox.Show(strCheck);
                    txtAmt.Text = "0";
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmCustomerCashPaid_Load(object sender, EventArgs e)
        {
            if (btnSave.Text == "&Update")
            {
                txtAmt.Focus();
            }
            else
            {
                txtCashReceiveVoucherNo.Focus();
            }
            //rdoCash.Checked = true; remove by ksaung
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (btnSave.Text.Equals("&Save"))
            {
                if (txtAmt.Text == "")
                {
                    MessageBox.Show("Enter Cash Receive Voucher No.");
                    return;
                }

                // disable by ksaung bcoz we request to paid more then credit amounts.
                //if (Convert.ToInt32(Int32.Parse(txtCreditTotal.Text)) < Convert.ToInt32(Int32.Parse(txtAmt.Text)))
                //{
                //    MessageBox.Show("More than Credit Total");
                //    txtAmt.Focus();
                //    return;
                //}

                int issaved = 0;
                BOLPayForCredit bolpaycredit = new BOLPayForCredit();
                bolpaycredit.ID = long.Parse(lblID.Text);
                bolpaycredit.Date = dtppurchasedate.Value;
                bolpaycredit.Cid = long.Parse(lblCustomerID.Text);
                bolpaycredit.VoucherNo = txtVoucherNo.Text;
                bolpaycredit.SaleVoucher = txtSaleVoucherNo.Text;
                bolpaycredit.Amt = Decimal.Parse(txtAmt.Text);
                bolpaycredit.LocationID = long.Parse(cboLocation.SelectedValue.ToString());
                bolpaycredit.UserID = frmMain.UserID;
                bolpaycredit.CashReceiveVoucherNo = txtCashReceiveVoucherNo.Text;

                if (rdoCash.Checked == true)
                {
                    CRPaymentType = "Cash";
                }
                else
                {
                    CRPaymentType = "Cheque";
                }

                bolpaycredit.CRPaymenttype = CRPaymentType;

                issaved = dalpaycredit.SavePayForCredit(bolpaycredit);

                if (issaved == 1)
                {
                    MessageBox.Show("Data is Successfully Saved.");
                    btnPreview_Click(sender, e);
                    Clear();
                }
            }
            if (btnSave.Text == "&Update")
            {
                int isupdate = 0;
                BOLPayForCredit bolpaycredit = new BOLPayForCredit();
                bolpaycredit.ID = long.Parse(lblID.Text);
                bolpaycredit.Date = dtppurchasedate.Value;
                bolpaycredit.LocationID = Int32.Parse(cboLocation.SelectedValue.ToString());
                bolpaycredit.Amt = Decimal.Parse(txtAmt.Text);
                bolpaycredit.VoucherNo = txtSaleVoucherNo.Text;
                bolpaycredit.Cid =long.Parse(lblCustomerID.Text);

                //Added By KSAung
                if (rdoCash.Checked == true)
                {
                    CRPaymentType = "Cash";
                }
                else
                {
                    CRPaymentType = "Cheque";
                }

                bolpaycredit.CRPaymenttype = CRPaymentType;

                isupdate = dalpaycredit.UpdatePayForCredit(bolpaycredit);

                if (isupdate == 1)
                {
                    MessageBox.Show("Data is Successful Update.");
                    Clear();

                }
            }
            this.Close();
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
        private void Clear()
        {
            cboLocation.SelectedValue = 0;
            txtCustomer.Text = "";
            txtCreditTotal.Text = "0";
            txtSaleVoucherNo.Text = "";
            txtAmt.Text = "0";
            lblCustomerID.Text = "0";
            lblID.Text = "0";
            txtCashReceiveVoucherNo.Text = "";
            rdoCash.Checked = true;

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

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtAmt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnSave_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtCashReceiveVoucherNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtAmt.Focus();
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
                List<BOLPayForCredit> lst = new List<BOLPayForCredit>();
                List<BOLSystem> lstsystem = new List<BOLSystem>();
                lstsystem = dalSystem.ShowAllSystem();
                BOLPayForCredit bolCustomerCashPaid = new BOLPayForCredit();

                //bolCustomerCashPaid.Date = Convert.ToDateTime(dtppurchasedate.ToString());
                //bolCustomerCashPaid.CashReceiveVoucherNo = txtCashReceiveVoucherNo.Text;
                bolCustomerCashPaid.VoucherNo = txtVoucherNo.Text;
                bolCustomerCashPaid.Amount = Decimal.Parse(txtAmt.Text);
                bolCustomerCashPaid.Name = txtCustomer.Text;
                bolCustomerCashPaid.Location = cboLocation.Text;
                string ReceiableAmount = txtCreditTotal.Text;
                string Times = txtCashReceiveVoucherNo.Text;
                string SaleVoucher = txtSaleVoucherNo.Text;

                lst.Add(bolCustomerCashPaid);

                if (lst.Count > 0)
                {
                    this.UseWaitCursor = true;
                    ReportDocument cu_Report = new ReportDocument();
                    cu_Report.Load(Application.StartupPath + @"\Report\RptPayForCredit(ByCustomer).rpt");
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
                            cu_Report.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLegal;
                            cu_Report.PrintOptions.ApplyPageMargins(margin);
                        }
                    }

                    cu_Report.DataDefinition.FormulaFields[5].Text = "Replace(cstr(" + ReceiableAmount + "),'.00','')";
                    cu_Report.DataDefinition.FormulaFields[4].Text = "Replace(cstr('" + Times + "'),'.00','')";
                    cu_Report.DataDefinition.FormulaFields[7].Text = "Replace(cstr('" + SaleVoucher + "'),'.00','')";

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
        }
    }
}
