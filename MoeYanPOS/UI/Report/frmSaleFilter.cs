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
using System.Data.SqlClient;
using System.Configuration;
using MoeYanPOS.Report;

namespace MoeYanPOS.UI.Report
{
    public partial class frmSaleFilter : Form
    {
        DALStock dalstock = new DALStock();
        DALClass dalclass = new DALClass();
        DALCategory dalcategory = new DALCategory();
        DALBrand dalbrand = new DALBrand();
        DALExchangeRate dalexchangerate = new DALExchangeRate();
        DALCustomer dalcustomer = new DALCustomer();
        DALSupplier dalsupplier = new DALSupplier();
        DALPurchase dalpurchase = new DALPurchase();
        DALUser daluser = new DALUser();
        DateTime StartDateTime; DateTime EndDateTime;
        DALSaleSummary dalsalesummary = new DALSaleSummary();
        DALVoucherSetting dalvoucher = new DALVoucherSetting();
        DALSystem dalSystem = new DALSystem();
        DALLocation dallocation = new DALLocation();
        int ReportType = 0;
        String Filter = " ";

        // Token: 0x06001358 RID: 4952 RVA: 0x000DE3E8 File Offset: 0x000DC5E8
        public frmSaleFilter()
        {
            this.InitializeComponent();
        }

        // Token: 0x06001359 RID: 4953 RVA: 0x000DE4B0 File Offset: 0x000DC6B0
        public frmSaleFilter(int reportType)
        {
            this.InitializeComponent();
            this.ReportType = reportType;
            if (reportType == 3)
            {
                this.txtCustomer.Visible = false;
                this.label3.Visible = false;
            }
        }

        // Token: 0x0600135A RID: 4954 RVA: 0x000DE5A4 File Offset: 0x000DC7A4
        private void frmSaleFilter_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.ReportType == 11)
                {
                    this.label19.Visible = true;
                    this.cboLocation.Visible = true;
                }
                this.LoadCategory();
                this.LoadCurrency();
                this.LoadClass();
                this.LoadBrand();
                this.LoadUser();
                this.LoadLocation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // Token: 0x0600135B RID: 4955 RVA: 0x000DE634 File Offset: 0x000DC834
        private void LoadLocation()
        {
            try
            {
                List<BolLocation> lstlocation = new List<BolLocation>();
                lstlocation = this.dallocation.GetAllLocation();
                lstlocation.Insert(0, new BolLocation
                {
                    ID = 0,
                    Location = "<Select a Location>"
                });
                this.cboLocation.ValueMember = "ID";
                this.cboLocation.DisplayMember = "Location";
                this.cboLocation.DataSource = lstlocation;
                if (lstlocation.Count > 0)
                {
                    this.cboLocation.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // Token: 0x0600135C RID: 4956 RVA: 0x000DE700 File Offset: 0x000DC900
        private void LoadCurrency()
        {
            try
            {
                List<BOLCurrency> lstCurrency = new List<BOLCurrency>();
                lstCurrency = this.dalexchangerate.FillCurrency();
                if (lstCurrency.Count > 0)
                {
                    lstCurrency.Insert(0, new BOLCurrency
                    {
                        Id = 0,
                        Currency = "<Select a Currency>"
                    });
                    this.cbocurrency.DisplayMember = "Currency";
                    this.cbocurrency.ValueMember = "Id";
                    this.cbocurrency.DataSource = lstCurrency;
                    this.cbocurrency.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // Token: 0x0600135D RID: 4957 RVA: 0x000DE7BC File Offset: 0x000DC9BC
        private void LoadBrand()
        {
            try
            {
                List<BOLBrand> lstbrand = new List<BOLBrand>();
                lstbrand = this.dalbrand.ShowAllBrand(0);
                lstbrand.Insert(0, new BOLBrand
                {
                    Id = 0,
                    Brandname = "<Select a Brand>"
                });
                this.cboBrand.ValueMember = "Id";
                this.cboBrand.DisplayMember = "Brandname";
                this.cboBrand.DataSource = lstbrand;
                if (lstbrand.Count > 0)
                {
                    this.cboBrand.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // Token: 0x0600135E RID: 4958 RVA: 0x000DE87C File Offset: 0x000DCA7C
        private void LoadPaymentType()
        {
            try
            {
                List<string> lstbrand = new List<string>();
                lstbrand.Add("<Select a PaymentType>");
                lstbrand.Add("Cash");
                lstbrand.Add("Credit");
                this.cboLocation.DataSource = lstbrand;
                this.cboLocation.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // Token: 0x0600135F RID: 4959 RVA: 0x000DE8FC File Offset: 0x000DCAFC
        private void LoadClass()
        {
            try
            {
                List<BOLClass> lstclass = new List<BOLClass>();
                lstclass = this.dalclass.SelectAllClass();
                lstclass.Insert(0, new BOLClass
                {
                    Id = 0,
                    ClassName = "<Select a Class>"
                });
                this.cboClass.ValueMember = "ID";
                this.cboClass.DisplayMember = "ClassName";
                this.cboClass.DataSource = lstclass;
                if (lstclass.Count > 0)
                {
                    this.cboClass.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // Token: 0x06001360 RID: 4960 RVA: 0x000DE9B8 File Offset: 0x000DCBB8
        private void LoadUser()
        {
            try
            {
                List<BOLUser> lstUSer = new List<BOLUser>();
                lstUSer = this.daluser.SelectAllUser();
                lstUSer.Insert(0, new BOLUser
                {
                    UserID = 0,
                    UserName = "<Select a User>"
                });
                this.cboUser.ValueMember = "UserID";
                this.cboUser.DisplayMember = "UserName";
                this.cboUser.DataSource = lstUSer;
                if (lstUSer.Count > 0)
                {
                    this.cboUser.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // Token: 0x06001361 RID: 4961 RVA: 0x000DEA74 File Offset: 0x000DCC74
        private void LoadCategory()
        {
            try
            {
                List<BOLCategory> lstcategory = new List<BOLCategory>();
                lstcategory = this.dalcategory.ShowAllCategory();
                lstcategory.Insert(0, new BOLCategory
                {
                    Id = 0,
                    Classname = "",
                    CategoryName = "<Select a Category>"
                });
                this.cboCategory.ValueMember = "Id";
                this.cboCategory.DisplayMember = "CategoryName";
                this.cboCategory.DataSource = lstcategory;
                if (lstcategory.Count > 0)
                {
                    this.cboCategory.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // Token: 0x06001362 RID: 4962 RVA: 0x000DEB3C File Offset: 0x000DCD3C
        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        // Token: 0x06001363 RID: 4963 RVA: 0x000DEB40 File Offset: 0x000DCD40
        private void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<BOLStock> lstStk = new List<BOLStock>();
                lstStk = this.dalstock.SelectStock(this.txtItemCode.Text, 0, "", 0);
                this.dgvItemCode.Rows.Clear();
                if (lstStk.Count > 0)
                {
                    for (int i = 0; i < lstStk.Count; i++)
                    {
                        this.dgvItemCode.Rows.Add(new object[]
						{
							lstStk[i].Id,
							lstStk[i].ItemCode,
							lstStk[i].NameEng,
							lstStk[i].NameMM,
							lstStk[i].Price
						});
                    }
                    this.dgvItemCode.Visible = true;
                    this.dgvItemCode.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // Token: 0x06001364 RID: 4964 RVA: 0x000DEC6C File Offset: 0x000DCE6C
        private void txtCustomer_TextChanged(object sender, EventArgs e)
        {
        }

        // Token: 0x06001365 RID: 4965 RVA: 0x000DEC70 File Offset: 0x000DCE70
        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // Token: 0x06001366 RID: 4966 RVA: 0x000DECAC File Offset: 0x000DCEAC
        private void txtsupplier_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<BOLSupplier> lstsupplier = new List<BOLSupplier>();
                lstsupplier = this.dalpurchase.GetSupplier(this.txtsupplier.Text);
                if (lstsupplier.Count > 0)
                {
                    this.dgvsupplier.Rows.Clear();
                    foreach (BOLSupplier s in lstsupplier)
                    {
                        this.dgvsupplier.Rows.Add(new object[]
						{
							s.Supplierid,
							s.SupplierName,
							s.Address,
							s.Phone
						});
                        this.dgvsupplier.Visible = true;
                    }
                    this.dgvsupplier.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // Token: 0x06001367 RID: 4967 RVA: 0x000DEDC8 File Offset: 0x000DCFC8
        private void picClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        // Token: 0x06001368 RID: 4968 RVA: 0x000DEDD4 File Offset: 0x000DCFD4
        private void dgvItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    this.txtItemCode.Text = this.dgvItemCode.CurrentRow.Cells[1].Value.ToString();
                    this.dgvItemCode.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // Token: 0x06001369 RID: 4969 RVA: 0x000DEE5C File Offset: 0x000DD05C
        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        // Token: 0x0600136A RID: 4970 RVA: 0x000DEE60 File Offset: 0x000DD060
        private void dgvsupplier_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    this.txtsupplier.Text = this.dgvsupplier.CurrentRow.Cells[1].Value.ToString();
                    this.lblsupplierid.Text = this.dgvsupplier.CurrentRow.Cells[0].Value.ToString();
                    this.dgvsupplier.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // Token: 0x0600136B RID: 4971 RVA: 0x000DEF14 File Offset: 0x000DD114
        private void dgvCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    this.txtCustomer.Text = this.dgvCustomer.CurrentRow.Cells[1].Value.ToString();
                    this.lblCustomerID.Text = this.dgvCustomer.CurrentRow.Cells[0].Value.ToString();
                    this.dgvCustomer.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // Token: 0x0600136C RID: 4972 RVA: 0x000DEFC8 File Offset: 0x000DD1C8
        private void GetData()
        {
            string sm = (this.dtpFromDate.Value.Month.ToString().Length > 1) ? this.dtpFromDate.Value.Month.ToString() : ("0" + this.dtpFromDate.Value.Month.ToString());
            string sd = (this.dtpFromDate.Value.Day.ToString().Length > 1) ? this.dtpFromDate.Value.Day.ToString() : ("0" + this.dtpFromDate.Value.Day.ToString());
            string lm = (this.dtpToDate.Value.Month.ToString().Length > 1) ? this.dtpToDate.Value.Month.ToString() : ("0" + this.dtpToDate.Value.Month.ToString());
            string ld = (this.dtpToDate.Value.Day.ToString().Length > 1) ? this.dtpToDate.Value.Day.ToString() : ("0" + this.dtpToDate.Value.Day.ToString());
            string sdate = string.Concat(new string[]
			{
				this.dtpFromDate.Value.Year.ToString(),
				"-",
				sm,
				"-",
				sd,
				" 00:00:00.000"
			});
            string ldate = string.Concat(new string[]
			{
				this.dtpToDate.Value.Year.ToString(),
				"-",
				lm,
				"-",
				ld,
				" 23:59:59.000"
			});
            this.StartDateTime = DateTime.Parse(sdate);
            this.EndDateTime = DateTime.Parse(ldate);
        }

        // Token: 0x0600136D RID: 4973 RVA: 0x000DF250 File Offset: 0x000DD450
        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                this.GetData();
                int ClassID = 0;
                int CategoryID = 0;
                string ItemCode = "";
                long CustomerID = 0L;
                int CurrencyID = 0;
                int BrandID = 0;
                int LocationID = 0;
                if (this.cboClass.Text != "<Select a Class>")
                {
                    this.Filter += this.cboClass.Text;
                    ClassID = int.Parse(this.cboClass.SelectedValue.ToString());
                }
                if (this.cboCategory.Text != "<Select a Category>")
                {
                    if (this.Filter != " ")
                    {
                        this.Filter = this.Filter + " - " + this.cboCategory.Text;
                    }
                    else
                    {
                        this.Filter += this.cboCategory.Text;
                    }
                    CategoryID = int.Parse(this.cboCategory.SelectedValue.ToString());
                }
                if (this.cboBrand.Text != "<Select a Brand>")
                {
                    BrandID = int.Parse(this.cboBrand.SelectedValue.ToString());
                }
                if (this.cbocurrency.Text != "<Select a Currency>")
                {
                    CurrencyID = int.Parse(this.cbocurrency.SelectedValue.ToString());
                }
                if (this.cboLocation.Text != "<Select a Location>")
                {
                    LocationID = int.Parse(this.cboLocation.SelectedValue.ToString());
                }
                if (this.txtItemCode.Text != "")
                {
                    ItemCode = this.txtItemCode.Text;
                }
                if (this.txtCustomer.Text != "")
                {
                    CustomerID = long.Parse(this.lblCustomerID.Text);
                }
                string PaymentType;
                if (this.cboLocation.SelectedIndex != 0)
                {
                    PaymentType = this.cboLocation.Text;
                }
                else
                {
                    PaymentType = "";
                }
                if (this.ReportType == 1)
                {
                    DataSet ds = new DataSet();
                    ds = this.dalsalesummary.SelectSaleSummaryCash(this.StartDateTime, this.EndDateTime, ClassID, CategoryID, ItemCode, PaymentType, CustomerID, CurrencyID, BrandID);
                    string fromdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                    string todate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                    if (ds.Tables.Count > 0)
                    {
                        ReportDocument l_Report = new ReportDocument();
                        ds.WriteXmlSchema(Application.StartupPath + "\\DataSets\\DS_Summary.xsd");
                        RptSaleSummary crystal = new RptSaleSummary();
                        string fdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                        string tdate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                        crystal.DataDefinition.FormulaFields[0].Text = "#" + fdate + "#";
                        crystal.DataDefinition.FormulaFields[1].Text = "#" + tdate + "#";
                        crystal.SetDataSource(ds.Tables[0]);
                        List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                        lstvoucherSetting = this.dalvoucher.SelectAllVoucher();
                        frmReport frmReport = new frmReport();
                        frmReport.rptViewer.ReportSource = crystal;
                        frmReport.rptViewer.RefreshReport();
                        frmReport.Show();
                        base.UseWaitCursor = false;
                    }
                    else
                    {
                        MessageBox.Show(" There is no data.");
                    }
                }
                if (this.ReportType == 2)
                {
                    ReportDocument l_Report = new ReportDocument();
                    DataSet dsCustomerLedger = new DataSet();
                    dsCustomerLedger = this.dalsalesummary.GetCustomerLedger(this.StartDateTime, this.EndDateTime, CustomerID);
                    string fromdate = this.dtpFromDate.Value.ToString("dd-MMM-yyyy");
                    string todate = this.dtpToDate.Value.ToString("dd-MMM-yyyy");
                    if (dsCustomerLedger.Tables[0].Rows.Count > 0)
                    {
                        DataSet dds = new DataSet();
                        DataTable dtt = new DataTable();
                        DataColumn Date = new DataColumn();
                        Date.ColumnName = "Date";
                        Date.DataType = Type.GetType("System.String");
                        dtt.Columns.Add(Date);
                        DataColumn Description = new DataColumn();
                        Description.ColumnName = "Description";
                        Description.DataType = Type.GetType("System.String");
                        dtt.Columns.Add(Description);
                        DataColumn dc = new DataColumn();
                        dc.ColumnName = "CustomerID";
                        dc.DataType = Type.GetType("System.String");
                        dtt.Columns.Add(dc);
                        DataColumn Name = new DataColumn();
                        Name.ColumnName = "Name";
                        Name.DataType = Type.GetType("System.String");
                        dtt.Columns.Add(Name);
                        DataColumn VoucherNo = new DataColumn();
                        VoucherNo.ColumnName = "VoucherNo";
                        VoucherNo.DataType = Type.GetType("System.String");
                        dtt.Columns.Add(VoucherNo);
                        DataColumn TotalAmt = new DataColumn();
                        TotalAmt.ColumnName = "TotalAmt";
                        TotalAmt.DataType = Type.GetType("System.Decimal");
                        dtt.Columns.Add(TotalAmt);
                        DataColumn PaidAmt = new DataColumn();
                        PaidAmt.ColumnName = "PaidAmt";
                        PaidAmt.DataType = Type.GetType("System.Decimal");
                        dtt.Columns.Add(PaidAmt);
                        DataColumn ReturnAmt = new DataColumn();
                        ReturnAmt.ColumnName = "ReturnAmt";
                        ReturnAmt.DataType = Type.GetType("System.Decimal");
                        dtt.Columns.Add(ReturnAmt);
                        DataColumn CreditAmt = new DataColumn();
                        CreditAmt.ColumnName = "CreditAmt";
                        CreditAmt.DataType = Type.GetType("System.Decimal");
                        dtt.Columns.Add(CreditAmt);
                        decimal Opening = Convert.ToDecimal(dsCustomerLedger.Tables[0].Rows[0].ItemArray[8].ToString());
                        decimal balance = Convert.ToDecimal(dsCustomerLedger.Tables[0].Rows[0].ItemArray[8].ToString());
                        for (int i = 0; i < dsCustomerLedger.Tables[0].Rows.Count; i++)
                        {
                            if (dsCustomerLedger.Tables[0].Rows[i].ItemArray[1].ToString() == "Direct Sale")
                            {
                                balance += Convert.ToDecimal(dsCustomerLedger.Tables[0].Rows[i].ItemArray[5].ToString());
                            }
                            if (dsCustomerLedger.Tables[0].Rows[i].ItemArray[1].ToString() == "Cash Receipt")
                            {
                                balance -= Convert.ToDecimal(dsCustomerLedger.Tables[0].Rows[i].ItemArray[6].ToString());
                            }
                            if (dsCustomerLedger.Tables[0].Rows[i].ItemArray[1].ToString() == " Sale Return")
                            {
                                balance -= Convert.ToDecimal(dsCustomerLedger.Tables[0].Rows[i].ItemArray[7].ToString());
                            }
                            DataRow dr = dtt.NewRow();
                            dr["Date"] = dsCustomerLedger.Tables[0].Rows[i].ItemArray[0].ToString();
                            dr["Description"] = dsCustomerLedger.Tables[0].Rows[i].ItemArray[1].ToString();
                            dr["CustomerID"] = dsCustomerLedger.Tables[0].Rows[i].ItemArray[2].ToString();
                            dr["Name"] = dsCustomerLedger.Tables[0].Rows[i].ItemArray[3].ToString();
                            dr["VoucherNo"] = dsCustomerLedger.Tables[0].Rows[i].ItemArray[4].ToString();
                            dr["TotalAmt"] = dsCustomerLedger.Tables[0].Rows[i].ItemArray[5].ToString();
                            dr["PaidAmt"] = dsCustomerLedger.Tables[0].Rows[i].ItemArray[6].ToString();
                            dr["ReturnAmt"] = dsCustomerLedger.Tables[0].Rows[i].ItemArray[7].ToString();
                            dr["CreditAmt"] = balance;
                            dtt.Rows.Add(dr);
                        }
                        dds.Tables.Add(dtt);
                        dsCustomerLedger.Tables[0].TableName = "Table1";
                        dds.WriteXmlSchema(Application.StartupPath + "\\DataSets\\DSLedgerCustomer.xsd");
                        l_Report.Load(Application.StartupPath + "\\Report\\RptCustomerLedger.rpt");
                        string aa = this.dtpToDate.Value.ToString("M-d-yyyy");
                        l_Report.DataDefinition.FormulaFields[0].Text = "#" + this.dtpFromDate.Value.ToString("M-d-yyyy") + "#";
                        l_Report.DataDefinition.FormulaFields[1].Text = "#" + aa + "#";
                        l_Report.DataDefinition.FormulaFields["OpeningAmt"].Text = "'" + Opening + "'";
                        l_Report.DataDefinition.FormulaFields["ClosingAmt"].Text = "'" + balance + "'";
                        l_Report.SetDataSource(dds);
                        List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                        lstvoucherSetting = this.dalvoucher.SelectAllVoucher();
                        frmReport frmReport = new frmReport();
                        frmReport.rptViewer.ReportSource = l_Report;
                        frmReport.rptViewer.RefreshReport();
                        frmReport.Show();
                        base.UseWaitCursor = false;
                    }
                    else
                    {
                        MessageBox.Show(" There is no data.");
                    }
                }
                if (this.ReportType == 3)
                {
                    ReportDocument l_Report = new ReportDocument();
                    DataSet dsAllCustomerLedger = new DataSet();
                    dsAllCustomerLedger = this.dalsalesummary.GetAllCustomerLedger(this.StartDateTime, this.EndDateTime);
                    dsAllCustomerLedger.Tables[0].TableName = "DS_AllCustomerLedger";
                    string fromdate = this.dtpFromDate.Value.ToString("dd-MMM-yyyy");
                    string todate = this.dtpToDate.Value.ToString("dd-MMM-yyyy");
                    if (dsAllCustomerLedger.Tables[0].Rows.Count > 0)
                    {
                        dsAllCustomerLedger.WriteXmlSchema(Application.StartupPath + "\\DataSets\\DSReportforAllCustomerLedger.xsd");
                        l_Report.Load(Application.StartupPath + "\\Report\\RptAllCustomerLedger.rpt");
                        string aa = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                        l_Report.DataDefinition.FormulaFields[0].Text = "#" + this.dtpFromDate.Value.ToString("dd-MM-yyyy") + "#";
                        l_Report.DataDefinition.FormulaFields[1].Text = "#" + aa + "#";
                        l_Report.SetDataSource(dsAllCustomerLedger);
                        List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                        lstvoucherSetting = this.dalvoucher.SelectAllVoucher();
                        frmReport frmReport = new frmReport();
                        frmReport.rptViewer.ReportSource = l_Report;
                        frmReport.rptViewer.Refresh();
                        frmReport.Show();
                        base.UseWaitCursor = false;
                    }
                    else
                    {
                        MessageBox.Show(" There is no data.");
                    }
                }
                if (this.ReportType == 4 || this.ReportType == 12)
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    DataTable dt2 = new DataTable();
                    dt = this.dalsalesummary.SelectNetSale(this.StartDateTime, this.EndDateTime, ClassID, CategoryID, ItemCode, PaymentType, CustomerID, CurrencyID, BrandID);
                    decimal discount = this.dalsalesummary.GetAllDiscountForNetSale(this.StartDateTime, this.EndDateTime, PaymentType, CustomerID, CurrencyID, BrandID);
                    ds.Tables.Add(dt);
                    ds.Tables.Add(dt2);
                    ds.Tables[0].TableName = "DS_NetSale";
                    string fromdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                    string todate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                    if (ds.Tables.Count > 0)
                    {
                        ReportDocument l_Report = new ReportDocument();
                        ds.WriteXmlSchema(Application.StartupPath + "\\DataSets\\DS_Sale.xsd");
                        if (this.ReportType == 4)
                        {
                            l_Report.Load(Application.StartupPath + "\\Report\\RptNetSale.rpt");
                        }
                        else
                        {
                            l_Report.Load(Application.StartupPath + "\\Report\\RptNetSaleByPcs.rpt");
                        }
                        string fdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                        string tdate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                        string location = (this.cboLocation.Text == "<Select a Location>") ? "All_Location" : this.cboLocation.Text;
                        string category = (this.cboCategory.Text == "<Select a Category>") ? "All_Category" : this.cboCategory.Text;
                        string customer = this.txtCustomer.Text;
                        l_Report.DataDefinition.FormulaFields[0].Text = "#" + fdate + "#";
                        l_Report.DataDefinition.FormulaFields[1].Text = "#" + tdate + "#";
                        l_Report.DataDefinition.FormulaFields[8].Text = "'" + location + "'";
                        l_Report.DataDefinition.FormulaFields[9].Text = "'" + category + "'";
                        l_Report.DataDefinition.FormulaFields[10].Text = "'" + customer + "'";
                        l_Report.SetDataSource(ds);
                        List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                        lstvoucherSetting = this.dalvoucher.SelectAllVoucher();
                        frmReport frmReport = new frmReport();
                        frmReport.rptViewer.ReportSource = l_Report;
                        frmReport.rptViewer.RefreshReport();
                        frmReport.Show();
                        base.UseWaitCursor = false;
                    }
                    else
                    {
                        MessageBox.Show(" There is no data.");
                    }
                }
                if (this.ReportType == 5)
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    dt = this.dalsalesummary.SelectNetSaleHeader(this.StartDateTime, this.EndDateTime, PaymentType, CustomerID, CurrencyID, BrandID);
                    ds.Tables.Add(dt);
                    ds.Tables[0].TableName = "DS_NetSaleHeader";
                    string fromdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                    string todate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                    if (ds.Tables.Count > 0)
                    {
                        ReportDocument l_Report = new ReportDocument();
                        ds.WriteXmlSchema(Application.StartupPath + "\\DataSets\\DS_Sale.xsd");
                        l_Report.Load(Application.StartupPath + "\\Report\\RptNetSaleHeader.rpt");
                        string fdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                        string tdate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                        l_Report.DataDefinition.FormulaFields[0].Text = "#" + fdate + "#";
                        l_Report.DataDefinition.FormulaFields[1].Text = "#" + tdate + "#";
                        l_Report.SetDataSource(ds);
                        List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                        lstvoucherSetting = this.dalvoucher.SelectAllVoucher();
                        frmReport frmReport = new frmReport();
                        frmReport.rptViewer.ReportSource = l_Report;
                        frmReport.rptViewer.RefreshReport();
                        frmReport.Show();
                        base.UseWaitCursor = false;
                    }
                    else
                    {
                        MessageBox.Show(" There is no data.");
                    }
                }
                if (this.ReportType == 6)
                {
                    DataSet ds = new DataSet();
                    ds = this.dalsalesummary.SelectPurchaseSummaryCash(this.StartDateTime, this.EndDateTime, ClassID, CategoryID, ItemCode, PaymentType, CustomerID, CurrencyID, BrandID);
                    ds.Tables[0].TableName = "DS_Summary";
                    string fromdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                    string todate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                    if (ds.Tables.Count > 0)
                    {
                        ReportDocument l_Report = new ReportDocument();
                        ds.WriteXmlSchema(Application.StartupPath + "\\DataSets\\DS_Summary.xsd");
                        l_Report.Load(Application.StartupPath + "\\Report\\RptPurchaseSummaryByStock.rpt");
                        string fdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                        string tdate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                        l_Report.DataDefinition.FormulaFields[0].Text = "#" + fdate + "#";
                        l_Report.DataDefinition.FormulaFields[1].Text = "#" + tdate + "#";
                        l_Report.SetDataSource(ds.Tables[0]);
                        List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                        lstvoucherSetting = this.dalvoucher.SelectAllVoucher();
                        DataTable dtt = new DataTable();
                        DataColumn dc = new DataColumn();
                        dc.ColumnName = "Name";
                        dc.DataType = Type.GetType("System.String");
                        dtt.Columns.Add(dc);
                        DataColumn dc2 = new DataColumn();
                        dc2.ColumnName = "Message";
                        dc2.DataType = Type.GetType("System.String");
                        dtt.Columns.Add(dc2);
                        DataColumn dc3 = new DataColumn();
                        dc3.ColumnName = "Logo";
                        dc3.DataType = Type.GetType("System.Byte[]");
                        dtt.Columns.Add(dc3);
                        DataColumn dc4 = new DataColumn();
                        dc4.ColumnName = "Address";
                        dc4.DataType = Type.GetType("System.String");
                        dtt.Columns.Add(dc4);
                        DataColumn dc5 = new DataColumn();
                        dc5.ColumnName = "Phone";
                        dc5.DataType = Type.GetType("System.String");
                        dtt.Columns.Add(dc5);
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
                            l_Report.Subreports[0].SetDataSource(dtt);
                        }
                        l_Report.SetDataSource(ds);
                        frmReport frmReport = new frmReport();
                        frmReport.rptViewer.ReportSource = l_Report;
                        frmReport.rptViewer.RefreshReport();
                        frmReport.Show();
                        base.UseWaitCursor = false;
                    }
                    else
                    {
                        MessageBox.Show(" There is no data.");
                    }
                }
                if (this.ReportType == 7)
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    dt = this.dalsalesummary.SelectNetPurchaseHeader(this.StartDateTime, this.EndDateTime, PaymentType, CustomerID, CurrencyID, BrandID);
                    ds.Tables.Add(dt);
                    ds.Tables[0].TableName = "DS_NetSaleHeader";
                    string fromdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                    string todate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                    if (ds.Tables.Count > 0)
                    {
                        ReportDocument l_Report = new ReportDocument();
                        ds.WriteXmlSchema(Application.StartupPath + "\\DataSets\\DS_Sale.xsd");
                        l_Report.Load(Application.StartupPath + "\\Report\\RptNetPurchaseHeader.rpt");
                        string fdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                        string tdate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                        l_Report.DataDefinition.FormulaFields[0].Text = "#" + fdate + "#";
                        l_Report.DataDefinition.FormulaFields[1].Text = "#" + tdate + "#";
                        l_Report.SetDataSource(ds);
                        List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                        lstvoucherSetting = this.dalvoucher.SelectAllVoucher();
                        frmReport frmReport = new frmReport();
                        frmReport.rptViewer.ReportSource = l_Report;
                        frmReport.rptViewer.RefreshReport();
                        frmReport.Show();
                        base.UseWaitCursor = false;
                    }
                    else
                    {
                        MessageBox.Show(" There is no data.");
                    }
                }
                if (this.ReportType == 8)
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    DataTable dt2 = new DataTable();
                    dt = this.dalsalesummary.SelectNetPurchase(this.StartDateTime, this.EndDateTime, ClassID, CategoryID, ItemCode, PaymentType, CustomerID, CurrencyID, BrandID);
                    decimal discount = this.dalsalesummary.GetAllDiscountForNetPurchase(this.StartDateTime, this.EndDateTime, PaymentType, CustomerID, CurrencyID, BrandID);
                    ds.Tables.Add(dt);
                    ds.Tables.Add(dt2);
                    ds.Tables[0].TableName = "DS_NetSale";
                    string fromdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                    string todate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                    if (ds.Tables.Count > 0)
                    {
                        ReportDocument l_Report = new ReportDocument();
                        ds.WriteXmlSchema(Application.StartupPath + "\\DataSets\\DS_Sale.xsd");
                        l_Report.Load(Application.StartupPath + "\\Report\\RptNetPurchase.rpt");
                        string fdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                        string tdate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                        l_Report.DataDefinition.FormulaFields[0].Text = "#" + fdate + "#";
                        l_Report.DataDefinition.FormulaFields[1].Text = "#" + tdate + "#";
                        l_Report.DataDefinition.FormulaFields["Discount"].Text = "'" + discount + "'";
                        l_Report.SetDataSource(ds);
                        List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                        lstvoucherSetting = this.dalvoucher.SelectAllVoucher();
                        frmReport frmReport = new frmReport();
                        frmReport.rptViewer.ReportSource = l_Report;
                        frmReport.rptViewer.RefreshReport();
                        frmReport.Show();
                        base.UseWaitCursor = false;
                    }
                    else
                    {
                        MessageBox.Show(" There is no data.");
                    }
                }
                if (this.ReportType == 9)
                {
                    DataSet ds = new DataSet();
                    ds = this.dalsalesummary.SelectSaleSummaryByStock(this.StartDateTime, this.EndDateTime, ClassID, CategoryID, ItemCode, PaymentType, CustomerID, CurrencyID, BrandID);
                    ds.Tables[0].TableName = "DS_Summary";
                    string fromdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                    string todate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                    if (ds.Tables.Count > 0)
                    {
                        ReportDocument l_Report = new ReportDocument();
                        ds.WriteXmlSchema(Application.StartupPath + "\\DataSets\\DS_Summary.xsd");
                        l_Report.Load(Application.StartupPath + "\\Report\\RptSaleSummaryByStock.rpt");
                        string fdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                        string tdate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                        l_Report.DataDefinition.FormulaFields[0].Text = "#" + fdate + "#";
                        l_Report.DataDefinition.FormulaFields[1].Text = "#" + tdate + "#";
                        List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                        lstvoucherSetting = this.dalvoucher.SelectAllVoucher();
                        DataTable dtt = new DataTable();
                        DataColumn dc = new DataColumn();
                        dc.ColumnName = "Name";
                        dc.DataType = Type.GetType("System.String");
                        dtt.Columns.Add(dc);
                        DataColumn dc2 = new DataColumn();
                        dc2.ColumnName = "Message";
                        dc2.DataType = Type.GetType("System.String");
                        dtt.Columns.Add(dc2);
                        DataColumn dc3 = new DataColumn();
                        dc3.ColumnName = "Logo";
                        dc3.DataType = Type.GetType("System.Byte[]");
                        dtt.Columns.Add(dc3);
                        DataColumn dc4 = new DataColumn();
                        dc4.ColumnName = "Address";
                        dc4.DataType = Type.GetType("System.String");
                        dtt.Columns.Add(dc4);
                        DataColumn dc5 = new DataColumn();
                        dc5.ColumnName = "Phone";
                        dc5.DataType = Type.GetType("System.String");
                        dtt.Columns.Add(dc5);
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
                            l_Report.Subreports[0].SetDataSource(dtt);
                        }
                        l_Report.SetDataSource(ds);
                        frmReport frmReport = new frmReport();
                        frmReport.rptViewer.ReportSource = l_Report;
                        frmReport.rptViewer.RefreshReport();
                        frmReport.Show();
                        base.UseWaitCursor = false;
                    }
                    else
                    {
                        MessageBox.Show(" There is no data.");
                    }
                }
                if (this.ReportType == 10)
                {
                    DataSet ds = new DataSet();
                    ds = this.dalsalesummary.MonthlySaleSummaryByCustomer(this.StartDateTime, this.EndDateTime, ClassID, CategoryID, ItemCode, PaymentType, CustomerID);
                    ds.Tables[0].TableName = "DS_MonthlySale";
                    string fromdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                    string todate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                    if (ds.Tables.Count > 0)
                    {
                        ReportDocument l_Report = new ReportDocument();
                        ds.WriteXmlSchema(Application.StartupPath + "\\DataSets\\DS_SaleCredit.xsd");
                        l_Report.Load(Application.StartupPath + "\\Report\\SaleReportByMonthly.rpt");
                        string fdate = this.dtpFromDate.Value.ToShortDateString();
                        string tdate = this.dtpToDate.Value.ToShortDateString();
                        string filter = string.Concat(new string[]
						{
							"Date From : ",
							fdate,
							"   To : ",
							tdate,
							" "
						});
                        if (this.cboClass.SelectedIndex != 0)
                        {
                            filter = filter + " ; Class : " + this.cboClass.Text;
                        }
                        if (this.cboCategory.SelectedIndex != 0)
                        {
                            filter = filter + " ; Category : " + this.cboCategory.Text;
                        }
                        if (this.txtItemCode.Text != "")
                        {
                            filter = filter + " ; ItemCode : " + this.txtItemCode.Text;
                        }
                        if (this.txtCustomer.Text != "")
                        {
                            filter = filter + " ; Customer : " + this.txtCustomer.Text + " ";
                        }
                        l_Report.DataDefinition.FormulaFields[1].Text = "'" + filter + "'";
                        List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                        lstvoucherSetting = this.dalvoucher.SelectAllVoucher();
                        DataTable dtt = new DataTable();
                        DataColumn dc = new DataColumn();
                        dc.ColumnName = "Name";
                        dc.DataType = Type.GetType("System.String");
                        dtt.Columns.Add(dc);
                        DataColumn dc2 = new DataColumn();
                        dc2.ColumnName = "Message";
                        dc2.DataType = Type.GetType("System.String");
                        dtt.Columns.Add(dc2);
                        DataColumn dc3 = new DataColumn();
                        dc3.ColumnName = "Logo";
                        dc3.DataType = Type.GetType("System.Byte[]");
                        dtt.Columns.Add(dc3);
                        DataColumn dc4 = new DataColumn();
                        dc4.ColumnName = "Address";
                        dc4.DataType = Type.GetType("System.String");
                        dtt.Columns.Add(dc4);
                        DataColumn dc5 = new DataColumn();
                        dc5.ColumnName = "Phone";
                        dc5.DataType = Type.GetType("System.String");
                        dtt.Columns.Add(dc5);
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
                            l_Report.Subreports[0].SetDataSource(dtt);
                        }
                        l_Report.SetDataSource(ds);
                        frmReport frmReport = new frmReport();
                        frmReport.rptViewer.ReportSource = l_Report;
                        frmReport.rptViewer.RefreshReport();
                        frmReport.Show();
                        base.UseWaitCursor = false;
                    }
                    else
                    {
                        MessageBox.Show(" There is no data.");
                    }
                }
                if (this.ReportType == 11)
                {
                    DataSet ds = new DataSet();
                    ds = this.dalsalesummary.GetStockMovementData(this.StartDateTime, this.EndDateTime, ClassID, CategoryID, ItemCode, LocationID);
                    ds.Tables[0].TableName = "DS_StockHistory";
                    string fromdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                    string todate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                    if (ds.Tables.Count > 0)
                    {
                        ReportDocument l_Report = new ReportDocument();
                        ds.WriteXmlSchema(Application.StartupPath + "\\DataSets\\DS_StockHistory.xsd");
                        l_Report.Load(Application.StartupPath + "\\Report\\RptStockMovement.rpt");
                        string fdate = this.dtpFromDate.Value.ToShortDateString();
                        string tdate = this.dtpToDate.Value.ToShortDateString();
                        string filter = string.Concat(new string[]
						{
							"Date From : ",
							fdate,
							"   To : ",
							tdate,
							" "
						});
                        if (this.cboClass.SelectedIndex != 0)
                        {
                            filter = filter + " ; Class : " + this.cboClass.Text;
                        }
                        if (this.cboCategory.SelectedIndex != 0)
                        {
                            filter = filter + " ; Category : " + this.cboCategory.Text;
                        }
                        if (this.txtItemCode.Text != "")
                        {
                            filter = filter + " ; ItemCode : " + this.txtItemCode.Text;
                        }
                        l_Report.DataDefinition.FormulaFields[2].Text = "'" + filter + "'";
                        l_Report.SetDataSource(ds);
                        frmReport frmReport = new frmReport();
                        frmReport.rptViewer.ReportSource = l_Report;
                        frmReport.rptViewer.RefreshReport();
                        frmReport.Show();
                        base.UseWaitCursor = false;
                    }
                    else
                    {
                        MessageBox.Show(" There is no data.");
                    }
                }
                this.Filter = " ";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // Token: 0x0600136E RID: 4974 RVA: 0x000E17D0 File Offset: 0x000DF9D0
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                this.GetData();
                int ClassID = 0;
                int CategoryID = 0;
                string ItemCode = "";
                long CustomerID = 0L;
                int CurrencyID = 0;
                int BrandID = 0;
                if (this.cboClass.Text != "<Select a Class>")
                {
                    this.Filter += this.cboCategory.Text;
                    ClassID = int.Parse(this.cboClass.SelectedValue.ToString());
                }
                if (this.cboCategory.Text != "<Select a Category>")
                {
                    if (this.Filter != " ")
                    {
                        this.Filter = this.Filter + " - " + this.cboCategory.Text;
                    }
                    else
                    {
                        this.Filter += this.cboCategory.Text;
                    }
                    CategoryID = int.Parse(this.cboCategory.SelectedValue.ToString());
                }
                if (this.cboBrand.Text != "<Select a Brand>")
                {
                    BrandID = int.Parse(this.cboBrand.SelectedValue.ToString());
                }
                if (this.cbocurrency.Text != "<Select a Currency>")
                {
                    CurrencyID = int.Parse(this.cbocurrency.SelectedValue.ToString());
                }
                if (this.txtItemCode.Text != "")
                {
                    ItemCode = this.txtItemCode.Text;
                }
                if (this.txtCustomer.Text != "")
                {
                    CustomerID = long.Parse(this.lblCustomerID.Text);
                }
                string PaymentType = this.cboLocation.Text;
                if (this.ReportType == 1)
                {
                    DataSet ds = new DataSet();
                    ds = this.dalsalesummary.SelectSaleSummaryCash(this.StartDateTime, this.EndDateTime, ClassID, CategoryID, ItemCode, PaymentType, CustomerID, CurrencyID, BrandID);
                    string fromdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                    string todate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                    if (ds.Tables.Count > 0)
                    {
                        ReportDocument l_Report = new ReportDocument();
                        ds.WriteXmlSchema(Application.StartupPath + "\\DataSets\\DS_Summary.xsd");
                        RptSaleSummary crystal = new RptSaleSummary();
                        string fdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                        string tdate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                        crystal.DataDefinition.FormulaFields[0].Text = "#" + fdate + "#";
                        crystal.DataDefinition.FormulaFields[1].Text = "#" + tdate + "#";
                        crystal.SetDataSource(ds.Tables[0]);
                        List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                        lstvoucherSetting = this.dalvoucher.SelectAllVoucher();
                        frmReport frmReport = new frmReport();
                        frmReport.rptViewer.ReportSource = crystal;
                        frmReport.rptViewer.RefreshReport();
                        frmReport.Show();
                        base.UseWaitCursor = false;
                    }
                    else
                    {
                        MessageBox.Show(" There is no data.");
                    }
                }
                if (this.ReportType == 2)
                {
                    DataSet dsCustomerLedger = new DataSet();
                    dsCustomerLedger = this.dalsalesummary.GetCustomerLedger(this.StartDateTime, this.EndDateTime, CustomerID);
                    string fromdate = this.dtpFromDate.Value.ToString("dd-MMM-yyyy");
                    string todate = this.dtpToDate.Value.ToString("dd-MMM-yyyy");
                    if (dsCustomerLedger.Tables[0].Rows.Count > 0)
                    {
                        DataSet dds = new DataSet();
                        DataTable dtt = new DataTable();
                        DataColumn Date = new DataColumn();
                        Date.ColumnName = "Date";
                        Date.DataType = Type.GetType("System.String");
                        dtt.Columns.Add(Date);
                        DataColumn Description = new DataColumn();
                        Description.ColumnName = "Description";
                        Description.DataType = Type.GetType("System.String");
                        dtt.Columns.Add(Description);
                        DataColumn dc = new DataColumn();
                        dc.ColumnName = "CustomerID";
                        dc.DataType = Type.GetType("System.String");
                        dtt.Columns.Add(dc);
                        DataColumn Name = new DataColumn();
                        Name.ColumnName = "Name";
                        Name.DataType = Type.GetType("System.String");
                        dtt.Columns.Add(Name);
                        DataColumn VoucherNo = new DataColumn();
                        VoucherNo.ColumnName = "VoucherNo";
                        VoucherNo.DataType = Type.GetType("System.String");
                        dtt.Columns.Add(VoucherNo);
                        DataColumn TotalAmt = new DataColumn();
                        TotalAmt.ColumnName = "TotalAmt";
                        TotalAmt.DataType = Type.GetType("System.Decimal");
                        dtt.Columns.Add(TotalAmt);
                        DataColumn PaidAmt = new DataColumn();
                        PaidAmt.ColumnName = "PaidAmt";
                        PaidAmt.DataType = Type.GetType("System.Decimal");
                        dtt.Columns.Add(PaidAmt);
                        DataColumn CreditAmt = new DataColumn();
                        CreditAmt.ColumnName = "CreditAmt";
                        CreditAmt.DataType = Type.GetType("System.Decimal");
                        dtt.Columns.Add(CreditAmt);
                        decimal Opening = Convert.ToDecimal(dsCustomerLedger.Tables[0].Rows[0].ItemArray[7].ToString());
                        decimal balance = Convert.ToDecimal(dsCustomerLedger.Tables[0].Rows[0].ItemArray[7].ToString());
                        for (int i = 0; i < dsCustomerLedger.Tables[0].Rows.Count; i++)
                        {
                            if (dsCustomerLedger.Tables[0].Rows[i].ItemArray[1].ToString() == "Direct Sale")
                            {
                                balance += Convert.ToDecimal(dsCustomerLedger.Tables[0].Rows[i].ItemArray[5].ToString());
                            }
                            if (dsCustomerLedger.Tables[0].Rows[i].ItemArray[1].ToString() == "Cash Receipt")
                            {
                                balance -= Convert.ToDecimal(dsCustomerLedger.Tables[0].Rows[i].ItemArray[6].ToString());
                            }
                            DataRow dr = dtt.NewRow();
                            dr["Date"] = dsCustomerLedger.Tables[0].Rows[i].ItemArray[0].ToString();
                            dr["Description"] = dsCustomerLedger.Tables[0].Rows[i].ItemArray[1].ToString();
                            dr["CustomerID"] = dsCustomerLedger.Tables[0].Rows[i].ItemArray[2].ToString();
                            dr["Name"] = dsCustomerLedger.Tables[0].Rows[i].ItemArray[3].ToString();
                            dr["VoucherNo"] = dsCustomerLedger.Tables[0].Rows[i].ItemArray[4].ToString();
                            dr["TotalAmt"] = dsCustomerLedger.Tables[0].Rows[i].ItemArray[5].ToString();
                            dr["PaidAmt"] = dsCustomerLedger.Tables[0].Rows[i].ItemArray[6].ToString();
                            dr["CreditAmt"] = balance;
                            dtt.Rows.Add(dr);
                        }
                        dds.Tables.Add(dtt);
                        dds.WriteXmlSchema(Application.StartupPath + "\\DataSets\\DSLedgerCustomer.xsd");
                        RptCustomerLedger crystal2 = new RptCustomerLedger();
                        string aa = this.dtpToDate.Value.ToString("M-d-yyyy");
                        crystal2.DataDefinition.FormulaFields[0].Text = "#" + this.dtpFromDate.Value.ToString("M-d-yyyy") + "#";
                        crystal2.DataDefinition.FormulaFields[1].Text = "#" + aa + "#";
                        crystal2.DataDefinition.FormulaFields["OpeningAmt"].Text = "'" + Opening + "'";
                        crystal2.DataDefinition.FormulaFields["ClosingAmt"].Text = "'" + balance + "'";
                        crystal2.SetDataSource(dds);
                        List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                        lstvoucherSetting = this.dalvoucher.SelectAllVoucher();
                        frmReport frmReport = new frmReport();
                        frmReport.rptViewer.ReportSource = crystal2;
                        frmReport.rptViewer.RefreshReport();
                        frmReport.Show();
                        base.UseWaitCursor = false;
                    }
                    else
                    {
                        MessageBox.Show(" There is no data.");
                    }
                }
                if (this.ReportType == 3)
                {
                    ReportDocument l_Report = new ReportDocument();
                    DataSet dsAllCustomerLedger = new DataSet();
                    dsAllCustomerLedger = this.dalsalesummary.GetAllCustomerLedger(this.StartDateTime, this.EndDateTime);
                    dsAllCustomerLedger.Tables[0].TableName = "DS_AllCustomerLedger";
                    string fromdate = this.dtpFromDate.Value.ToString("dd-MMM-yyyy");
                    string todate = this.dtpToDate.Value.ToString("dd-MMM-yyyy");
                    if (dsAllCustomerLedger.Tables[0].Rows.Count > 0)
                    {
                        dsAllCustomerLedger.WriteXmlSchema(Application.StartupPath + "\\DataSets\\DSReportforAllCustomerLedger.xsd");
                        l_Report.Load(Application.StartupPath + "\\Report\\RptAllCustomerLedger.rpt");
                        string aa = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                        l_Report.DataDefinition.FormulaFields[0].Text = "#" + this.dtpFromDate.Value.ToString("dd-MM-yyyy") + "#";
                        l_Report.DataDefinition.FormulaFields[1].Text = "#" + aa + "#";
                        l_Report.SetDataSource(dsAllCustomerLedger);
                        List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                        lstvoucherSetting = this.dalvoucher.SelectAllVoucher();
                        frmReport frmReport = new frmReport();
                        frmReport.rptViewer.ReportSource = l_Report;
                        frmReport.rptViewer.Refresh();
                        frmReport.Show();
                        base.UseWaitCursor = false;
                    }
                    else
                    {
                        MessageBox.Show(" There is no data.");
                    }
                }
                if (this.ReportType == 4)
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    DataTable dt2 = new DataTable();
                    dt = this.dalsalesummary.SelectNetSale(this.StartDateTime, this.EndDateTime, ClassID, CategoryID, ItemCode, PaymentType, CustomerID, CurrencyID, BrandID);
                    decimal discount = this.dalsalesummary.GetAllDiscountForNetSale(this.StartDateTime, this.EndDateTime, PaymentType, CustomerID, CurrencyID, BrandID);
                    ds.Tables.Add(dt);
                    ds.Tables.Add(dt2);
                    ds.Tables[0].TableName = "DS_NetSale";
                    string fromdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                    string todate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                    if (ds.Tables.Count > 0)
                    {
                        ReportDocument l_Report = new ReportDocument();
                        ds.WriteXmlSchema(Application.StartupPath + "\\DataSets\\DS_Sale.xsd");
                        l_Report.Load(Application.StartupPath + "\\Report\\RptNetSale.rpt");
                        string fdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                        string tdate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                        l_Report.DataDefinition.FormulaFields[0].Text = "#" + fdate + "#";
                        l_Report.DataDefinition.FormulaFields[1].Text = "#" + tdate + "#";
                        l_Report.DataDefinition.FormulaFields["Discount"].Text = "'" + discount + "'";
                        l_Report.SetDataSource(ds);
                        List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                        lstvoucherSetting = this.dalvoucher.SelectAllVoucher();
                        frmReport frmReport = new frmReport();
                        frmReport.rptViewer.ReportSource = l_Report;
                        frmReport.rptViewer.RefreshReport();
                        frmReport.Show();
                        base.UseWaitCursor = false;
                    }
                    else
                    {
                        MessageBox.Show(" There is no data.");
                    }
                }
                if (this.ReportType == 5)
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    dt = this.dalsalesummary.SelectNetSaleHeader(this.StartDateTime, this.EndDateTime, PaymentType, CustomerID, CurrencyID, BrandID);
                    ds.Tables.Add(dt);
                    ds.Tables[0].TableName = "DS_NetSaleHeader";
                    string fromdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                    string todate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                    if (ds.Tables.Count > 0)
                    {
                        ReportDocument l_Report = new ReportDocument();
                        ds.WriteXmlSchema(Application.StartupPath + "\\DataSets\\DS_Sale.xsd");
                        l_Report.Load(Application.StartupPath + "\\Report\\RptNetSaleHeader.rpt");
                        string fdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                        string tdate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                        l_Report.DataDefinition.FormulaFields[0].Text = "#" + fdate + "#";
                        l_Report.DataDefinition.FormulaFields[1].Text = "#" + tdate + "#";
                        l_Report.SetDataSource(ds);
                        List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                        lstvoucherSetting = this.dalvoucher.SelectAllVoucher();
                        frmReport frmReport = new frmReport();
                        frmReport.rptViewer.ReportSource = l_Report;
                        frmReport.rptViewer.RefreshReport();
                        frmReport.Show();
                        base.UseWaitCursor = false;
                    }
                    else
                    {
                        MessageBox.Show(" There is no data.");
                    }
                }
                if (this.ReportType == 6)
                {
                    DataSet ds = new DataSet();
                    ds = this.dalsalesummary.SelectPurchaseSummaryCash(this.StartDateTime, this.EndDateTime, ClassID, CategoryID, ItemCode, PaymentType, CustomerID, CurrencyID, BrandID);
                    string fromdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                    string todate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                    if (ds.Tables.Count > 0)
                    {
                        ReportDocument l_Report = new ReportDocument();
                        ds.WriteXmlSchema(Application.StartupPath + "\\DataSets\\DS_Summary.xsd");
                        l_Report.Load(Application.StartupPath + "\\Report\\RptPurchaseSummary.rpt");
                        string fdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                        string tdate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                        l_Report.DataDefinition.FormulaFields[0].Text = "#" + fdate + "#";
                        l_Report.DataDefinition.FormulaFields[1].Text = "#" + tdate + "#";
                        l_Report.SetDataSource(ds.Tables[0]);
                        List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                        lstvoucherSetting = this.dalvoucher.SelectAllVoucher();
                        frmReport frmReport = new frmReport();
                        frmReport.rptViewer.ReportSource = l_Report;
                        frmReport.rptViewer.RefreshReport();
                        frmReport.Show();
                        base.UseWaitCursor = false;
                    }
                    else
                    {
                        MessageBox.Show(" There is no data.");
                    }
                }
                if (this.ReportType == 7)
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    dt = this.dalsalesummary.SelectNetPurchaseHeader(this.StartDateTime, this.EndDateTime, PaymentType, CustomerID, CurrencyID, BrandID);
                    ds.Tables.Add(dt);
                    ds.Tables[0].TableName = "DS_NetSaleHeader";
                    string fromdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                    string todate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                    if (ds.Tables.Count > 0)
                    {
                        ReportDocument l_Report = new ReportDocument();
                        ds.WriteXmlSchema(Application.StartupPath + "\\DataSets\\DS_Sale.xsd");
                        l_Report.Load(Application.StartupPath + "\\Report\\RptNetPurchaseHeader.rpt");
                        string fdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                        string tdate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                        l_Report.DataDefinition.FormulaFields[0].Text = "#" + fdate + "#";
                        l_Report.DataDefinition.FormulaFields[1].Text = "#" + tdate + "#";
                        l_Report.SetDataSource(ds);
                        List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                        lstvoucherSetting = this.dalvoucher.SelectAllVoucher();
                        frmReport frmReport = new frmReport();
                        frmReport.rptViewer.ReportSource = l_Report;
                        frmReport.rptViewer.RefreshReport();
                        frmReport.Show();
                        base.UseWaitCursor = false;
                    }
                    else
                    {
                        MessageBox.Show(" There is no data.");
                    }
                }
                if (this.ReportType == 8)
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    DataTable dt2 = new DataTable();
                    dt = this.dalsalesummary.SelectNetPurchase(this.StartDateTime, this.EndDateTime, ClassID, CategoryID, ItemCode, PaymentType, CustomerID, CurrencyID, BrandID);
                    decimal discount = this.dalsalesummary.GetAllDiscountForNetPurchase(this.StartDateTime, this.EndDateTime, PaymentType, CustomerID, CurrencyID, BrandID);
                    ds.Tables.Add(dt);
                    ds.Tables.Add(dt2);
                    ds.Tables[0].TableName = "DS_NetSale";
                    string fromdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                    string todate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                    if (ds.Tables.Count > 0)
                    {
                        ReportDocument l_Report = new ReportDocument();
                        ds.WriteXmlSchema(Application.StartupPath + "\\DataSets\\DS_Sale.xsd");
                        l_Report.Load(Application.StartupPath + "\\Report\\RptNetPurchase.rpt");
                        string fdate = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                        string tdate = this.dtpToDate.Value.ToString("dd-MM-yyyy");
                        l_Report.DataDefinition.FormulaFields[0].Text = "#" + fdate + "#";
                        l_Report.DataDefinition.FormulaFields[1].Text = "#" + tdate + "#";
                        l_Report.DataDefinition.FormulaFields["Discount"].Text = "'" + discount + "'";
                        l_Report.SetDataSource(ds);
                        List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                        lstvoucherSetting = this.dalvoucher.SelectAllVoucher();
                        frmReport frmReport = new frmReport();
                        frmReport.rptViewer.ReportSource = l_Report;
                        frmReport.rptViewer.RefreshReport();
                        frmReport.Show();
                        base.UseWaitCursor = false;
                    }
                    else
                    {
                        MessageBox.Show(" There is no data.");
                    }
                }
                this.Filter = " ";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // Token: 0x0600136F RID: 4975 RVA: 0x000E2E34 File Offset: 0x000E1034
        private void txtCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    List<BOLCustomer> lstcustomer = new List<BOLCustomer>();
                    lstcustomer = this.dalcustomer.GetCustomer(this.txtCustomer.Text, "", 0);
                    this.dgvCustomer.Rows.Clear();
                    foreach (BOLCustomer c in lstcustomer)
                    {
                        this.dgvCustomer.Rows.Add(new object[]
						{
							c.ID,
							c.CustomerID,
							c.Name,
							c.Address,
							c.Wholesaleprice
						});
                    }
                    this.dgvCustomer.Visible = true;
                    this.dgvCustomer.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // Token: 0x06001370 RID: 4976 RVA: 0x000E2F68 File Offset: 0x000E1168
        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    List<BOLStock> lstStk = new List<BOLStock>();
                    lstStk = this.dalstock.SelectStock(this.txtItemCode.Text, 0, "", 0);
                    this.dgvItemCode.Rows.Clear();
                    if (lstStk.Count > 0)
                    {
                        for (int i = 0; i < lstStk.Count; i++)
                        {
                            this.dgvItemCode.Rows.Add(new object[]
							{
								lstStk[i].Id,
								lstStk[i].ItemCode,
								lstStk[i].NameEng,
								lstStk[i].NameMM,
								lstStk[i].Price
							});
                        }
                        this.dgvItemCode.Visible = true;
                        this.dgvItemCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // Token: 0x06001371 RID: 4977 RVA: 0x000E30B8 File Offset: 0x000E12B8
        private void txtsupplier_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                List<BOLSupplier> lstsupplier = new List<BOLSupplier>();
                lstsupplier = this.dalpurchase.GetSupplier(this.txtsupplier.Text);
                this.dgvsupplier.Rows.Clear();
                foreach (BOLSupplier s in lstsupplier)
                {
                    this.dgvsupplier.Rows.Add(new object[]
					{
						s.Supplierid,
						s.SupplierName,
						s.Address,
						s.Phone
					});
                    if (s.SupplierName == "Auto")
                    {
                        this.lblsupplierid.Text = s.Supplierid.ToString();
                        this.txtsupplier.Text = s.SupplierName;
                        this.dgvsupplier.Visible = false;
                    }
                    else
                    {
                        this.dgvsupplier.Visible = true;
                    }
                }
                bool isCrdite = this.dalsupplier.ChkPaymentType(int.Parse(this.lblsupplierid.Text));
                this.dgvsupplier.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cboClass_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
