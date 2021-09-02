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
    public partial class frmPayForCredit : Form
    {
        private DALCustomer dalCustomer = new DALCustomer();

        // Token: 0x04000D0F RID: 3343
        private DALPayForCredit dalPayForCredit = new DALPayForCredit();

        // Token: 0x04000D10 RID: 3344
        private DALLocation dalLocation = new DALLocation();

        // Token: 0x04000D11 RID: 3345
        private DALClass dalClass = new DALClass();

        // Token: 0x04000D12 RID: 3346
        private DALVoucherSetting dalVoucher = new DALVoucherSetting();

        // Token: 0x04000D13 RID: 3347
        private DALSystem dalSystem = new DALSystem();

        // Token: 0x04000D14 RID: 3348
        private long locationID = 0L;

        // Token: 0x04000D15 RID: 3349
        private long customerID = 0L;

        // Token: 0x04000D16 RID: 3350
        private string voucherNo = "";

        // Token: 0x04000D17 RID: 3351
        private bool isByDate = false;

        // Token: 0x04000D18 RID: 3352
        private long classid = 0L;

        // Token: 0x04000D19 RID: 3353
        private DateTime StartDateT;

        // Token: 0x04000D1A RID: 3354
        private DateTime EndDateT;

        public frmPayForCredit()
        {
            InitializeComponent();
        }

        private void LoadLocation()
        {
            try
            {
                List<BolLocation> list = new List<BolLocation>();
                list = this.dalLocation.GetAllLocation();
                list.Insert(0, new BolLocation
                {
                    ID = 0L,
                    Location = "<Select a Location>"
                });
                this.cboLocation.DisplayMember = "Location";
                this.cboLocation.ValueMember = "ID";
                this.cboLocation.DataSource = list;
                if (list.Count > 0)
                {
                    this.cboLocation.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // Token: 0x06001537 RID: 5431 RVA: 0x000F1C08 File Offset: 0x000EFE08
        private void txtCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    List<BOLCustomer> e2 = new List<BOLCustomer>();
                    e2 = this.dalCustomer.GetCustomer(this.txtCustomer.Text, "", 0);
                    this.dgvCustomer.Rows.Clear();
                    foreach (BOLCustomer bolcustomer in e2)
                    {
                        this.dgvCustomer.Rows.Add(new object[]
						{
							bolcustomer.ID,
							bolcustomer.CustomerID,
							bolcustomer.Name,
							bolcustomer.Address
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

        // Token: 0x06001538 RID: 5432 RVA: 0x000F1D2C File Offset: 0x000EFF2C
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

        // Token: 0x06001539 RID: 5433 RVA: 0x000F1DE0 File Offset: 0x000EFFE0
        public void showAll()
        {
            try
            {
                string e = (this.dtpFromDate.Value.Month.ToString().Length > 1) ? this.dtpFromDate.Value.Month.ToString() : ("0" + this.dtpFromDate.Value.Month.ToString());
                string text = (this.dtpFromDate.Value.Day.ToString().Length > 1) ? this.dtpFromDate.Value.Day.ToString() : ("0" + this.dtpFromDate.Value.Day.ToString());
                string text2 = (this.dtpToDate.Value.Month.ToString().Length > 1) ? this.dtpToDate.Value.Month.ToString() : ("0" + this.dtpToDate.Value.Month.ToString());
                string text3 = (this.dtpToDate.Value.Day.ToString().Length > 1) ? this.dtpToDate.Value.Day.ToString() : ("0" + this.dtpToDate.Value.Day.ToString());
                string s = string.Concat(new string[]
				{
					this.dtpFromDate.Value.Year.ToString(),
					"-",
					e,
					"-",
					text,
					" 00:00:00.000"
				});
                string s2 = string.Concat(new string[]
				{
					this.dtpToDate.Value.Year.ToString(),
					"-",
					text2,
					"-",
					text3,
					" 23:59:59.000"
				});
                this.StartDateT = DateTime.Parse(s);
                this.EndDateT = DateTime.Parse(s2);
                if (this.cboLocation.Text != "<Select a Location>")
                {
                    this.locationID = long.Parse(this.cboLocation.SelectedValue.ToString());
                }
                if (this.cboClass.Text != "<Select a Class>")
                {
                    this.classid = long.Parse(this.cboClass.SelectedValue.ToString());
                }
                if (this.txtCustomer.Text != "")
                {
                    this.customerID = long.Parse(this.lblCustomerID.Text.ToString());
                }
                else
                {
                    this.customerID = 0L;
                }
                if (this.txtVoucherNo.Text != "")
                {
                    this.voucherNo = this.txtVoucherNo.Text;
                }
                else
                {
                    this.voucherNo = "";
                }
                if (this.chkIsByDate.Checked)
                {
                    this.isByDate = true;
                }
                List<BOLPayForCredit> list = new List<BOLPayForCredit>();
                list = this.dalPayForCredit.GetPayForCredits(this.locationID, this.classid, this.customerID, this.voucherNo, this.StartDateT, this.EndDateT, this.isByDate);
                this.dgvPayForCredit.Rows.Clear();
                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        this.dgvPayForCredit.Rows.Add(new object[]
						{
							list[i].ID,
							list[i].Date,
							list[i].VoucherNo,
							list[i].CustomerID,
							list[i].Name,
							list[i].Location,
							list[i].Amt,
							list[i].CRPaymenttype
						});
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // Token: 0x0600153A RID: 5434 RVA: 0x000F2300 File Offset: 0x000F0500
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.showAll();
        }

        // Token: 0x0600153B RID: 5435 RVA: 0x000F230C File Offset: 0x000F050C
        private void frmPayForCredit_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadLocation();
                this.LoadClass();
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message.ToString());
            }
        }

        // Token: 0x0600153C RID: 5436 RVA: 0x000F2354 File Offset: 0x000F0554
        private void LoadClass()
        {
            try
            {
                List<BOLClass> list = new List<BOLClass>();
                list = this.dalClass.SelectAllClass();
                list.Insert(0, new BOLClass
                {
                    Id = 0,
                    ClassName = "<Select a Class>"
                });
                this.cboClass.DisplayMember = "ClassName";
                this.cboClass.ValueMember = "Id";
                this.cboClass.DataSource = list;
                if (list.Count > 0)
                {
                    this.cboClass.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // Token: 0x0600153D RID: 5437 RVA: 0x000F2410 File Offset: 0x000F0610
        private void picClose_Click(object sender, EventArgs e)
        {
            try
            {
                base.Close();
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message.ToString());
            }
        }

        // Token: 0x0600153E RID: 5438 RVA: 0x000F2450 File Offset: 0x000F0650
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                this.txtCustomer.Text = "";
                this.txtVoucherNo.Text = "";
                if (this.cboLocation.Items.Count > 0)
                {
                    this.cboLocation.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // Token: 0x0600153F RID: 5439 RVA: 0x000F24D4 File Offset: 0x000F06D4
        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.cboLocation.Text != "<Select a Location>")
                {
                    this.locationID = long.Parse(this.cboLocation.SelectedValue.ToString());
                }
                if (this.cboClass.Text != "<Select a Class>")
                {
                    this.classid = long.Parse(this.cboClass.SelectedValue.ToString());
                }
                if (this.txtCustomer.Text != "")
                {
                    this.customerID = long.Parse(this.lblCustomerID.Text.ToString());
                }
                else
                {
                    this.customerID = 0L;
                }
                if (this.txtVoucherNo.Text != "")
                {
                    this.voucherNo = this.txtVoucherNo.Text;
                }
                else
                {
                    this.voucherNo = "";
                }
                if (this.chkIsByDate.Checked)
                {
                    this.isByDate = true;
                }
                string e2 = (this.dtpFromDate.Value.Month.ToString().Length > 1) ? this.dtpFromDate.Value.Month.ToString() : ("0" + this.dtpFromDate.Value.Month.ToString());
                string text = (this.dtpFromDate.Value.Day.ToString().Length > 1) ? this.dtpFromDate.Value.Day.ToString() : ("0" + this.dtpFromDate.Value.Day.ToString());
                string text2 = (this.dtpToDate.Value.Month.ToString().Length > 1) ? this.dtpToDate.Value.Month.ToString() : ("0" + this.dtpToDate.Value.Month.ToString());
                string text3 = (this.dtpToDate.Value.Day.ToString().Length > 1) ? this.dtpToDate.Value.Day.ToString() : ("0" + this.dtpToDate.Value.Day.ToString());
                string s = string.Concat(new string[]
				{
					this.dtpFromDate.Value.Year.ToString(),
					"-",
					e2,
					"-",
					text,
					" 00:00:00.000"
				});
                string s2 = string.Concat(new string[]
				{
					this.dtpToDate.Value.Year.ToString(),
					"-",
					text2,
					"-",
					text3,
					" 23:59:59.000"
				});
                this.StartDateT = DateTime.Parse(s);
                this.EndDateT = DateTime.Parse(s2);
                DataSet dataSet = new DataSet();
                dataSet = this.dalPayForCredit.GetPayForCreditsHistory(this.locationID, this.classid, this.customerID, this.voucherNo, this.StartDateT, this.EndDateT, this.isByDate);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    base.UseWaitCursor = true;
                    ReportDocument reportDocument = new ReportDocument();
                    dataSet.WriteXmlSchema(Application.StartupPath + "\\DataSets\\DS_PayForCredit.xsd");
                    reportDocument.Load(Application.StartupPath + "\\Report\\RptPayForCredit.rpt");
                    string str = this.dtpFromDate.Value.ToString("MM-dd-yyyy");
                    string str2 = this.dtpToDate.Value.ToString("MM-dd-yyyy");
                    reportDocument.DataDefinition.FormulaFields[1].Text = "#" + str + "#";
                    reportDocument.DataDefinition.FormulaFields[2].Text = "#" + str2 + "#";
                    reportDocument.SetDataSource(dataSet.Tables[0]);
                    reportDocument.SetDatabaseLogon("sa", "moeyan");
                    List<BOLVoucherSetting> list = new List<BOLVoucherSetting>();
                    list = this.dalVoucher.SelectAllVoucher();
                    DataTable dataTable = new DataTable();
                    DataColumn dataColumn = new DataColumn();
                    dataColumn.ColumnName = "Name";
                    dataColumn.DataType = Type.GetType("System.String");
                    dataTable.Columns.Add(dataColumn);
                    if (list.Count > 0)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            dataRow["Name"] = list[0].Name;
                            dataTable.Rows.Add(dataRow);
                        }
                        reportDocument.Subreports[0].SetDataSource(dataTable);
                    }
                    frmReport frmReport = new frmReport();
                    frmReport.rptViewer.ReportSource = reportDocument;
                    frmReport.rptViewer.Refresh();
                    frmReport.Show();
                    base.UseWaitCursor = false;
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

        // Token: 0x06001540 RID: 5440 RVA: 0x000F2B0C File Offset: 0x000F0D0C
        private void dgvPayForCredit_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 8)
                {
                    if (e.RowIndex >= 0)
                    {
                        DateTime dt = default(DateTime);
                        long id = long.Parse(this.dgvPayForCredit.Rows[e.RowIndex].Cells[0].Value.ToString());
                        dt = Convert.ToDateTime(this.dgvPayForCredit.Rows[e.RowIndex].Cells[1].Value.ToString());
                        frmCustomerCashPaid frmCustomerCashPaid = new frmCustomerCashPaid(id, dt);
                        frmCustomerCashPaid.ShowDialog();
                        this.btnSearch_Click(sender, e);
                    }
                }
                if (e.ColumnIndex == 9)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int num = int.Parse(this.dgvPayForCredit.Rows[e.RowIndex].Cells[0].Value.ToString());
                            int num2 = this.dalPayForCredit.DeletePayForCreditHistory((long)num);
                            if (num2 == 1)
                            {
                                MessageBox.Show("Successfully Deleted!");
                                this.btnSearch_Click(sender, e);
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

        // Token: 0x06001541 RID: 5441 RVA: 0x000F2CB8 File Offset: 0x000F0EB8
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string e2 = (this.dtpFromDate.Value.Month.ToString().Length > 1) ? this.dtpFromDate.Value.Month.ToString() : ("0" + this.dtpFromDate.Value.Month.ToString());
                string text = (this.dtpFromDate.Value.Day.ToString().Length > 1) ? this.dtpFromDate.Value.Day.ToString() : ("0" + this.dtpFromDate.Value.Day.ToString());
                string text2 = (this.dtpToDate.Value.Month.ToString().Length > 1) ? this.dtpToDate.Value.Month.ToString() : ("0" + this.dtpToDate.Value.Month.ToString());
                string text3 = (this.dtpToDate.Value.Day.ToString().Length > 1) ? this.dtpToDate.Value.Day.ToString() : ("0" + this.dtpToDate.Value.Day.ToString());
                string s = string.Concat(new string[]
				{
					this.dtpFromDate.Value.Year.ToString(),
					"-",
					e2,
					"-",
					text,
					" 00:00:00.000"
				});
                string s2 = string.Concat(new string[]
				{
					this.dtpToDate.Value.Year.ToString(),
					"-",
					text2,
					"-",
					text3,
					" 23:59:59.000"
				});
                this.StartDateT = DateTime.Parse(s);
                this.EndDateT = DateTime.Parse(s2);
                if (this.cboLocation.Text != "<Select a Location>")
                {
                    this.locationID = long.Parse(this.cboLocation.SelectedValue.ToString());
                }
                if (this.cboClass.Text != "<Select a Class>")
                {
                    this.classid = long.Parse(this.cboClass.SelectedValue.ToString());
                }
                if (this.txtCustomer.Text != "")
                {
                    this.customerID = long.Parse(this.lblCustomerID.Text.ToString());
                }
                else
                {
                    this.customerID = 0L;
                }
                if (this.txtVoucherNo.Text != "")
                {
                    this.voucherNo = this.txtVoucherNo.Text;
                }
                else
                {
                    this.voucherNo = "";
                }
                if (this.chkIsByDate.Checked)
                {
                    this.isByDate = true;
                }
                MoeYanPOS.Report.RptOpeningStock rptOpeningStock = new MoeYanPOS.Report.RptOpeningStock();
                ReportDocument reportDocument = new ReportDocument();
                DataSet dataSet = new DataSet();
                dataSet = this.dalPayForCredit.GetPayForCreditsHistory(this.locationID, this.classid, this.customerID, this.voucherNo, this.StartDateT, this.EndDateT, this.isByDate);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    dataSet.WriteXmlSchema(Application.StartupPath + "\\DataSets\\DS_PayForCredit.xsd");
                    reportDocument.Load(Application.StartupPath + "\\Report\\RptPayForCredit.rpt");
                    string str = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                    string str2 = this.dtpFromDate.Value.ToString("dd-MM-yyyy");
                    reportDocument.DataDefinition.FormulaFields[1].Text = "#" + str + "#";
                    reportDocument.DataDefinition.FormulaFields[2].Text = "#" + str2 + "#";
                    reportDocument.SetDataSource(dataSet.Tables[0]);
                    reportDocument.SetDatabaseLogon("sa", "moeyan");
                    List<BOLSystem> list = new List<BOLSystem>();
                    list = this.dalSystem.ShowAllSystem();
                    if (list.Count > 0)
                    {
                        List<BOLVoucherSetting> list2 = new List<BOLVoucherSetting>();
                        list2 = this.dalVoucher.SelectAllVoucher();
                        DataTable dataTable = new DataTable();
                        DataColumn dataColumn = new DataColumn();
                        dataColumn.ColumnName = "Name";
                        dataColumn.DataType = Type.GetType("System.String");
                        dataTable.Columns.Add(dataColumn);
                        if (list2.Count > 0)
                        {
                            for (int i = 0; i < list2.Count; i++)
                            {
                                DataRow dataRow = dataTable.NewRow();
                                dataRow["Name"] = list2[0].Name;
                                dataTable.Rows.Add(dataRow);
                            }
                            reportDocument.Subreports[0].SetDataSource(dataTable);
                        }
                        reportDocument.PrintOptions.PrinterName = list[0].Printervoucher;
                        reportDocument.PrintToPrinter(1, false, 0, 0);
                    }
                    else
                    {
                        MessageBox.Show("Please put printer name at System Setting.");
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

        // Token: 0x06001542 RID: 5442 RVA: 0x000F3320 File Offset: 0x000F1520
        private void frmPayForCredit_Click(object sender, EventArgs e)
        {
            try
            {
                this.dgvCustomer.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // Token: 0x06001543 RID: 5443 RVA: 0x000F3368 File Offset: 0x000F1568
        private void dgvPayForCredit_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        // Token: 0x06001544 RID: 5444 RVA: 0x000F336B File Offset: 0x000F156B
        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
