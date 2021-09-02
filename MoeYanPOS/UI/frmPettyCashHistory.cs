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
    public partial class frmPettyCashHistory : Form
    {
        DALPettyCash dalpettycash = new DALPettyCash();
        DALLocation dalLocation = new DALLocation();
        DALSystem dalsystem = new DALSystem();
        DALVoucherSetting dalVoucher = new DALVoucherSetting();

        long locationid = 0;
        string remark = "";
        Boolean IsByDate = false;
        DateTime StartDateTime;
        DateTime EndDateTime;

        public frmPettyCashHistory()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
    
                if (txtRemark.Text != "")
                {
                    remark = txtRemark.Text;
                }
                else
                {
                    remark = "";
                }

                if (cboLocation.Text != "<Select a Location>")
                {
                    locationid = Int32.Parse(cboLocation.SelectedValue.ToString());
                }
                else
                {
                    locationid = 0;
                }

                DateTime StartDateTime; DateTime EndDateTime;

                string sm = dtpFromDate.Value.Month.ToString().Length > 1 ? dtpFromDate.Value.Month.ToString() : "0" + dtpFromDate.Value.Month.ToString();
                string sd = dtpFromDate.Value.Day.ToString().Length > 1 ? dtpFromDate.Value.Day.ToString() : "0" + dtpFromDate.Value.Day.ToString();
                string lm = dtpToDate.Value.Month.ToString().Length > 1 ? dtpToDate.Value.Month.ToString() : "0" + dtpToDate.Value.Month.ToString();
                string ld = dtpToDate.Value.Day.ToString().Length > 1 ? dtpToDate.Value.Day.ToString() : "0" + dtpToDate.Value.Day.ToString();

                string sdate = dtpFromDate.Value.Year.ToString() + "-" + sm + "-" + sd + " 00:00:00.000";
                string ldate = dtpToDate.Value.Year.ToString() + "-" + lm + "-" + ld + " 23:59:59.000";

                StartDateTime = DateTime.Parse(sdate);
                EndDateTime = DateTime.Parse(ldate);

                DataSet ds = new DataSet();
                ds = dalpettycash.GetSearchPettyCashHistory(StartDateTime, EndDateTime, remark, locationid);
                dgvStockBalance.Rows.Clear();
                dgvStockBalance.Rows.Clear();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvStockBalance.Rows.Add(ds.Tables[0].Rows[i].ItemArray[0].ToString(), ds.Tables[0].Rows[i].ItemArray[1].ToString(), ds.Tables[0].Rows[i].ItemArray[2].ToString(), ds.Tables[0].Rows[i].ItemArray[3].ToString(), ds.Tables[0].Rows[i].ItemArray[4].ToString(), ds.Tables[0].Rows[i].ItemArray[5].ToString(), "", "");
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void showall()
        {
            try
            {
                string remark = "";
                string sm = dtpFromDate.Value.Month.ToString().Length > 1 ? dtpFromDate.Value.Month.ToString() : "0" + dtpFromDate.Value.Month.ToString();
                string sd = dtpFromDate.Value.Day.ToString().Length > 1 ? dtpFromDate.Value.Day.ToString() : "0" + dtpFromDate.Value.Day.ToString();
                string lm = dtpToDate.Value.Month.ToString().Length > 1 ? dtpToDate.Value.Month.ToString() : "0" + dtpToDate.Value.Month.ToString();
                string ld = dtpToDate.Value.Day.ToString().Length > 1 ? dtpToDate.Value.Day.ToString() : "0" + dtpToDate.Value.Day.ToString();

                string sdate = dtpFromDate.Value.Year.ToString() + "-" + sm + "-" + sd + " 00:00:00.000";
                string ldate = dtpToDate.Value.Year.ToString() + "-" + lm + "-" + ld + " 23:59:59.000";

                StartDateTime = DateTime.Parse(sdate);
                EndDateTime = DateTime.Parse(ldate);
                
                remark = txtRemark.Text;
                List<BOLPettyCash> bolpettycashlist = new List<BOLPettyCash>();
                bolpettycashlist = dalpettycash.GetPettyCashHistroy(StartDateTime, EndDateTime, locationid, remark, IsByDate);
                dgvStockBalance.Rows.Clear();

                if (bolpettycashlist.Count > 0)
                {
                    for (int i = 0; i < bolpettycashlist.Count; i++)
                    {
                        dgvStockBalance.Rows.Add(bolpettycashlist[i].ID, bolpettycashlist[i].Date,bolpettycashlist[i].Type, bolpettycashlist[i].Amount, bolpettycashlist[i].Location, bolpettycashlist[i].Remark);
                    }
                }


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
                txtRemark.Text = "";
                if (cboLocation.Items.Count > 0)
                {
                    cboLocation.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void frmPettyCashHistory_Load(object sender, EventArgs e)
        {
            loadlocation();
            showall();
        }
        private void loadlocation()
        {
            try
            {
                List<BolLocation> LstLocation = new List<BolLocation>();
                LstLocation = dalLocation.GetAllLocation();

                BolLocation bolLocation = new BolLocation();
                bolLocation.ID = 0;
                bolLocation.Location = "<Select a Location>";
                LstLocation.Insert(0, bolLocation);
                cboLocation.DisplayMember = "Location";
                cboLocation.ValueMember = "ID";
                cboLocation.DataSource = LstLocation;
                if (LstLocation.Count > 0)
                {
                    cboLocation.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        
        private void dgvStockBalance_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvStockBalance_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 6)
                {
                    if (e.RowIndex >= 0)
                    {
                        //long locationid = long.Parse(dgvStockBalance.Rows[e.RowIndex].Cells[3].Value.ToString());
                        //string remark = dgvStockBalance.Rows[e.RowIndex].Cells[4].Value.ToString();
                        frmPettyCash pettycash = new frmPettyCash(long.Parse(dgvStockBalance.Rows[e.RowIndex].Cells[0].Value.ToString()));
                        pettycash.ShowDialog();
                        btnSearch_Click(sender, e);
                    }
                }
                if (e.ColumnIndex == 7)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int id = 0;
                            id = Int32.Parse(dgvStockBalance.Rows[e.RowIndex].Cells[0].Value.ToString());
                            int isdelete = 0;
                            isdelete = dalpettycash.DeletePettyCash(id);
                            if (isdelete > 0)
                            {
                                MessageBox.Show("Successfully deleted!");
                                dgvStockBalance.Rows.Remove(dgvStockBalance.CurrentRow);
                                //dgvpurchase.Rows.Clear();
                                
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

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {

                string sm = dtpFromDate.Value.Month.ToString().Length > 1 ? dtpFromDate.Value.Month.ToString() : "0" + dtpFromDate.Value.Month.ToString();
                string sd = dtpFromDate.Value.Day.ToString().Length > 1 ? dtpFromDate.Value.Day.ToString() : "0" + dtpFromDate.Value.Day.ToString();
                string lm = dtpToDate.Value.Month.ToString().Length > 1 ? dtpToDate.Value.Month.ToString() : "0" + dtpToDate.Value.Month.ToString();
                string ld = dtpToDate.Value.Day.ToString().Length > 1 ? dtpToDate.Value.Day.ToString() : "0" + dtpToDate.Value.Day.ToString();

                string sdate = dtpFromDate.Value.Year.ToString() + "-" + sm + "-" + sd + " 00:00:00.000";
                string ldate = dtpToDate.Value.Year.ToString() + "-" + lm + "-" + ld + " 23:59:59.000";
                StartDateTime = DateTime.Parse(sdate);
                EndDateTime = DateTime.Parse(ldate);
                DataSet ds = new DataSet();
                ds = dalpettycash.GetSearchPettyCashHistory(StartDateTime, EndDateTime, remark, locationid);
                

                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.UseWaitCursor = true;
                    ReportDocument l_Report = new ReportDocument();

                    ds.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_PettyCashHistory.xsd");
                    l_Report.Load(Application.StartupPath + @"\Report\RptPettyCashHistory.rpt");

                    l_Report.SetDataSource(ds.Tables[0]);
                    l_Report.SetDatabaseLogon("sa", "moeyan");

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

                        l_Report.Subreports[0].SetDataSource(dtt);
                    }

                    frmReport frmReport = new frmReport();
                    frmReport.rptViewer.ReportSource = l_Report;

                    frmReport.rptViewer.Refresh();
                    frmReport.Show();
                    this.UseWaitCursor = false;

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
                string sm = dtpFromDate.Value.Month.ToString().Length > 1 ? dtpFromDate.Value.Month.ToString() : "0" + dtpFromDate.Value.Month.ToString();
                string sd = dtpFromDate.Value.Day.ToString().Length > 1 ? dtpFromDate.Value.Day.ToString() : "0" + dtpFromDate.Value.Day.ToString();
                string lm = dtpToDate.Value.Month.ToString().Length > 1 ? dtpToDate.Value.Month.ToString() : "0" + dtpToDate.Value.Month.ToString();
                string ld = dtpToDate.Value.Day.ToString().Length > 1 ? dtpToDate.Value.Day.ToString() : "0" + dtpToDate.Value.Day.ToString();

                string sdate = dtpFromDate.Value.Year.ToString() + "-" + sm + "-" + sd + " 00:00:00.000";
                string ldate = dtpToDate.Value.Year.ToString() + "-" + lm + "-" + ld + " 23:59:59.000";
                StartDateTime = DateTime.Parse(sdate);
                EndDateTime = DateTime.Parse(ldate);
                DataSet ds = new DataSet();
                ds = dalpettycash.GetSearchPettyCashHistory(StartDateTime, EndDateTime, remark, locationid);
                

                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.UseWaitCursor = true;
                    ReportDocument l_Report = new ReportDocument();

                    ds.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_PettyCashHistory.xsd");
                    l_Report.Load(Application.StartupPath + @"\Report\RptPettyCashHistory.rpt");

                    l_Report.SetDataSource(ds.Tables[0]);
                    l_Report.SetDatabaseLogon("sa", "moeyan");



                    List<BOLSystem> lstsystem = new List<BOLSystem>();
                    lstsystem = dalsystem.ShowAllSystem();

                    if (lstsystem.Count > 0)
                    {
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

                            l_Report.Subreports[0].SetDataSource(dtt);
                        }

                        l_Report.PrintOptions.PrinterName = lstsystem[0].Printervoucher;
                        l_Report.PrintToPrinter(1, false, 0, 0);
                        //frmReport frmReport = new frmReport();
                        //frmReport.rptViewer.ReportSource = l_Report;
                        //frmReport.rptViewer.Refresh();
                        //frmReport.Show();
                        //this.UseWaitCursor = false;
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
    }
}
