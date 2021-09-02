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
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using System.Configuration;

namespace MoeYanPOS.UI
{
    public partial class frmCashReport : Form
    {
        DALCashReport dalCashReport = new DALCashReport();
        DALLocation dallocation = new DALLocation();
        DateTime CashReceiveDate;
        DateTime ToDate;
        DALSystem dalSystem = new DALSystem();
        DALVoucherSetting dalVoucher = new DALVoucherSetting();
        int ReportType = 0;
        public frmCashReport()
        {
            InitializeComponent();
            label2.Visible = false;
            dtpTodate.Visible = false;
        }
        public frmCashReport(int RType)
        {
            InitializeComponent();
            ReportType = RType;
            label1.Text = "From";
        }

        private void frmCashReport_Load(object sender, EventArgs e)
        {
            LocationLoad();
        }
        private void LocationLoad()
        {
            try
            {
                List<BolLocation> lstlocation = new List<BolLocation>();
                lstlocation = dallocation.GetAllLocation();

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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (ReportType != 1)
                {
                    string sm = dtpCashReceiveDate.Value.Month.ToString().Length > 1 ? dtpCashReceiveDate.Value.Month.ToString() : "0" + dtpCashReceiveDate.Value.Month.ToString();
                    string sd = dtpCashReceiveDate.Value.Day.ToString().Length > 1 ? dtpCashReceiveDate.Value.Day.ToString() : "0" + dtpCashReceiveDate.Value.Day.ToString();
                    string sdate = dtpCashReceiveDate.Value.Year.ToString() + "-" + sm + "-" + sd + " 00:00:00.000";
                    CashReceiveDate = DateTime.Parse(sdate);
                    DataSet ds = new DataSet();
                    ds = dalCashReport.GetDailyCashStatementSummary(CashReceiveDate, long.Parse(cboLocation.SelectedValue.ToString()));
                    dgvCashReport.Rows.Clear();

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            //dgvCashReport.Rows.Add(ds.Tables[0].Rows[i].ItemArray[3].ToString(), ds.Tables[0].Rows[i].ItemArray[4].ToString(), ds.Tables[0].Rows[i].ItemArray[5].ToString());
                            dgvCashReport.Rows.Add(ds.Tables[0].Rows[i].ItemArray[1].ToString(), ds.Tables[0].Rows[i].ItemArray[2].ToString(), ds.Tables[0].Rows[i].ItemArray[3].ToString());
                        }

                    }
                }
                if (ReportType == 1)
                {
                    string sm = dtpCashReceiveDate.Value.Month.ToString().Length > 1 ? dtpCashReceiveDate.Value.Month.ToString() : "0" + dtpCashReceiveDate.Value.Month.ToString();
                    string sd = dtpCashReceiveDate.Value.Day.ToString().Length > 1 ? dtpCashReceiveDate.Value.Day.ToString() : "0" + dtpCashReceiveDate.Value.Day.ToString();
                    string sdate = dtpCashReceiveDate.Value.Year.ToString() + "-" + sm + "-" + sd + " 00:00:00.000";
                    CashReceiveDate = DateTime.Parse(sdate);
                    string fm = dtpTodate.Value.Month.ToString().Length > 1 ? dtpTodate.Value.Month.ToString() : "0" + dtpTodate.Value.Month.ToString();
                    string fd = dtpTodate.Value.Day.ToString().Length > 1 ? dtpTodate.Value.Day.ToString() : "0" + dtpTodate.Value.Day.ToString();
                    string fdate = dtpTodate.Value.Year.ToString() + "-" + sm + "-" + sd + " 00:00:00.000";
                    ToDate = DateTime.Parse(fdate);
                    DataSet ds = new DataSet();
                    ds = dalCashReport.GetProfitAndLoss(CashReceiveDate,ToDate, long.Parse(cboLocation.SelectedValue.ToString()));
                    dgvCashReport.Rows.Clear();

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            dgvCashReport.Rows.Add(ds.Tables[0].Rows[i].ItemArray[3].ToString(), ds.Tables[0].Rows[i].ItemArray[4].ToString(), ds.Tables[0].Rows[i].ItemArray[5].ToString());
                        }

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

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                MoeYanPOS.Report.Rpt_DailyCashStatementSummery rpt = new MoeYanPOS.Report.Rpt_DailyCashStatementSummery();
                ReportDocument l_Report = new ReportDocument();
                string sm = dtpCashReceiveDate.Value.Month.ToString().Length > 1 ? dtpCashReceiveDate.Value.Month.ToString() : "0" + dtpCashReceiveDate.Value.Month.ToString();
                string sd = dtpCashReceiveDate.Value.Day.ToString().Length > 1 ? dtpCashReceiveDate.Value.Day.ToString() : "0" + dtpCashReceiveDate.Value.Day.ToString();
                string sdate = dtpCashReceiveDate.Value.Year.ToString() + "-" + sm + "-" + sd + " 00:00:00.000";
                CashReceiveDate = DateTime.Parse(sdate);
                DataSet ds = new DataSet();
                ds = dalCashReport.GetDailyCashStatementSummaryReport(CashReceiveDate, long.Parse(cboLocation.SelectedValue.ToString()));
                //dgvCashReport.Rows.Clear();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_CashReport.xsd");
                    l_Report.Load(Application.StartupPath + @"\Report\RptCashReport.rpt");

                    l_Report.SetDataSource(ds.Tables[0]);
                    l_Report.SetDatabaseLogon("sa", "moeyan");



                    //List<BOLSystem> lstsystem = new List<BOLSystem>();
                    //lstsystem = dalSystem.ShowAllSystem();

                    //if (lstsystem.Count > 0)
                    //{
                    //    List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                    //    lstvoucherSetting = dalVoucher.SelectAllVoucher();

                    //    DataTable dtt = new DataTable();
                    //    DataColumn dc = new DataColumn();
                    //    dc.ColumnName = "Name";
                    //    dc.DataType = System.Type.GetType("System.String");
                    //    dtt.Columns.Add(dc);

                    //    if (lstvoucherSetting.Count > 0)
                    //    {
                    //        for (int i = 0; i < lstvoucherSetting.Count; i++)
                    //        {
                    //            DataRow dr = dtt.NewRow();
                    //            dr["Name"] = lstvoucherSetting[0].Name;
                    //            dtt.Rows.Add(dr);
                    //        }

                    //        l_Report.Subreports[0].SetDataSource(dtt);
                    //    }

                        //l_Report.PrintOptions.PrinterName = lstsystem[0].Printervoucher;
                        //l_Report.PrintToPrinter(1, false, 0, 0);
                        frmReport frmReport = new frmReport();
                        frmReport.rptViewer.ReportSource = l_Report;
                        frmReport.rptViewer.Refresh();
                        frmReport.Show();
                        this.UseWaitCursor = false;
                    //}                    
                }
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
                 MoeYanPOS.Report.Rpt_DailyCashStatementSummery rpt = new MoeYanPOS.Report.Rpt_DailyCashStatementSummery();
                ReportDocument l_Report = new ReportDocument();
                string sm = dtpCashReceiveDate.Value.Month.ToString().Length > 1 ? dtpCashReceiveDate.Value.Month.ToString() : "0" + dtpCashReceiveDate.Value.Month.ToString();
                string sd = dtpCashReceiveDate.Value.Day.ToString().Length > 1 ? dtpCashReceiveDate.Value.Day.ToString() : "0" + dtpCashReceiveDate.Value.Day.ToString();
                string sdate = dtpCashReceiveDate.Value.Year.ToString() + "-" + sm + "-" + sd + " 00:00:00.000";
                CashReceiveDate = DateTime.Parse(sdate);
                DataSet ds = new DataSet();
                ds = dalCashReport.GetDailyCashStatementSummaryReport(CashReceiveDate, long.Parse(cboLocation.SelectedValue.ToString()));
                //dgvCashReport.Rows.Clear();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_CashReport.xsd");
                    l_Report.Load(Application.StartupPath + @"\Report\RptCashReport.rpt");

                    l_Report.SetDataSource(ds.Tables[0]);
                    l_Report.SetDatabaseLogon("sa", "moeyan");



                    List<BOLSystem> lstsystem = new List<BOLSystem>();
                    lstsystem = dalSystem.ShowAllSystem();

                    if (lstsystem.Count > 0)
                    {
                    //    List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                    //    lstvoucherSetting = dalVoucher.SelectAllVoucher();

                    //    DataTable dtt = new DataTable();
                    //    DataColumn dc = new DataColumn();
                    //    dc.ColumnName = "Name";
                    //    dc.DataType = System.Type.GetType("System.String");
                    //    dtt.Columns.Add(dc);

                    //    if (lstvoucherSetting.Count > 0)
                    //    {
                    //        for (int i = 0; i < lstvoucherSetting.Count; i++)
                    //        {
                    //            DataRow dr = dtt.NewRow();
                    //            dr["Name"] = lstvoucherSetting[0].Name;
                    //            dtt.Rows.Add(dr);
                    //        }

                    //        l_Report.Subreports[0].SetDataSource(dtt);
                    //    }

                        l_Report.PrintOptions.PrinterName = lstsystem[0].Printervoucher;
                        l_Report.PrintToPrinter(1, false, 0, 0);
                        //frmReport frmReport = new frmReport();
                        //frmReport.rptViewer.ReportSource = l_Report;
                        //frmReport.rptViewer.Refresh();
                        //frmReport.Show();
                        //this.UseWaitCursor = false;
                    //}                    
                }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
