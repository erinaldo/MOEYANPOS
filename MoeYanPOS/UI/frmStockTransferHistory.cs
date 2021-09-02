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
using CrystalDecisions.CrystalReports.Engine;

namespace MoeYanPOS.UI
{
    public partial class frmStockTransferHistory : Form
    {
        DALStock dalstock=new DALStock();
        DALLocation dallocation = new DALLocation();
        DALStockTransferHistory dalstockhistory = new DALStockTransferHistory();
        DALSystem dalsystem = new DALSystem();
        DALVoucherSetting dalvoucher = new DALVoucherSetting();
        public frmStockTransferHistory()
        {
            InitializeComponent();
        }
        public frmStockTransferHistory(string ItemCode)
        {
            InitializeComponent();
            txtStock.Text = ItemCode;
            btnSearch.Focus();

        }
        private void LoadLocation()
        {
            try
            {
                List<BolLocation> LstLocation = new List<BolLocation>();
                LstLocation = dallocation.GetAllLocation();

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
        private void LoadToLocation()
        {
            try
            {
                List<BolLocation> LstLocation = new List<BolLocation>();
                LstLocation = dallocation.GetAllLocation();

                BolLocation bolLocation = new BolLocation();
                bolLocation.ID = 0;
                bolLocation.Location = "<Select a Location>";
                LstLocation.Insert(0, bolLocation);
                cboToLocation.DisplayMember = "Location";
                cboToLocation.ValueMember = "ID";
                cboToLocation.DataSource = LstLocation;
                if (LstLocation.Count > 0)
                {
                    cboToLocation.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void txtStock_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    List<BOLStock> lstStk = new List<BOLStock>();
                    lstStk = dalstock.SelectStock(txtStock.Text, 0, "", 0);
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
        private void GetReport()
        {
            try
            {
                string ItemCode = ""; long locationID = 0; long ToLocationID = 0;
                int AdjustmentTypeID = 0;

                if (txtStock.Text != "")
                {
                    ItemCode = txtStock.Text;
                }
                else
                {
                    ItemCode = "";
                }
              
                if (cboLocation.Text != "<Select a Location>")
                {
                    locationID = Int32.Parse(cboLocation.SelectedValue.ToString());
                }
                else
                {
                    locationID = 0;
                }

                if (cboToLocation.Text != "<Select a Location>")
                {
                    ToLocationID = Int32.Parse(cboToLocation.SelectedValue.ToString());
                }
                else
                {
                    ToLocationID = 0;
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
                ds = dalstockhistory.GetStockTransferHistory(StartDateTime, EndDateTime, ItemCode, locationID, ToLocationID);
                dgvAdjustment.Rows.Clear();
                dgvAdjDetail.Rows.Clear();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvAdjustment.Rows.Add(ds.Tables[0].Rows[i].ItemArray[0].ToString(), ds.Tables[0].Rows[i].ItemArray[1].ToString(), ds.Tables[0].Rows[i].ItemArray[2].ToString(),ds.Tables[0].Rows[i].ItemArray[3].ToString(),  "", "");
                    }
                }


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
                txtStock.Text = dgvItemCode.CurrentRow.Cells[1].Value.ToString();
                dgvItemCode.Visible = false;
                btnSearch.Focus();
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
                    txtStock.Text = dgvItemCode.CurrentRow.Cells[1].Value.ToString();
                    dgvItemCode.Visible = false;
                    btnSearch.Focus();
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

        private void frmStockTransferHistory_Load(object sender, EventArgs e)
        {
            try
            {
                LoadLocation();
                LoadToLocation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetReport();
        }

        private void dgvAdjustment_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                long ID = 0;
                ID = long.Parse(dgvAdjustment.Rows[e.RowIndex].Cells[0].Value.ToString());
                DataSet ds = new DataSet();
                ds = dalstockhistory.GetStockTransferDetail(ID);
                dgvAdjDetail.Rows.Clear();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvAdjDetail.Rows.Add(ds.Tables[0].Rows[i].ItemArray[1].ToString(), ds.Tables[0].Rows[i].ItemArray[2].ToString(), ds.Tables[0].Rows[i].ItemArray[3].ToString(), ds.Tables[0].Rows[i].ItemArray[0].ToString(), ds.Tables[0].Rows[i].ItemArray[4].ToString(), ds.Tables[0].Rows[i].ItemArray[5].ToString(), ds.Tables[0].Rows[i].ItemArray[6].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvAdjustment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4)
                {
                    if (e.RowIndex >= 0)
                    {
                        long id = 0;
                        id = long.Parse(dgvAdjustment.Rows[e.RowIndex].Cells[0].Value.ToString());
                        frmStockTransfer frmStockTransfer = new frmStockTransfer(id);
                        frmStockTransfer.ShowDialog();
                        btnSearch_Click(sender, e);
                    }
                }
                if (e.ColumnIndex == 5)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int id = 0;
                            id = Int32.Parse(dgvAdjustment.Rows[e.RowIndex].Cells[0].Value.ToString());
                            int isdelete = 0;
                            isdelete = dalstockhistory.DeleteStockTranserHistory(id);
                            if (isdelete == 1)
                            {
                                MessageBox.Show("Successfully Deleted!");
                                btnSearch_Click(sender, e);
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

        private void btnAdjustmentReport_Click(object sender, EventArgs e)
        {
            try
            {
                string ItemCode = ""; long locationID = 0; long TolocationID = 0;              

                if (txtStock.Text != "")
                {
                    ItemCode = txtStock.Text;
                }
                else
                {
                    ItemCode = "";
                }                

                if (cboLocation.Text != "<Select a Location>")
                {
                    locationID = Int32.Parse(cboLocation.SelectedValue.ToString());
                }
                else
                {
                    locationID = 0;
                }

                if (cboToLocation.Text != "<Select a Location>")
                {
                    TolocationID = Int32.Parse(cboToLocation.SelectedValue.ToString());
                }
                else
                {
                    TolocationID = 0;
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

                MoeYanPOS.Report.RptStockTransfer rpt = new MoeYanPOS.Report.RptStockTransfer();
                ReportDocument l_Report = new ReportDocument();
                DataSet ds = new DataSet();
                ds = dalstockhistory.GetStockTransferReport(StartDateTime, EndDateTime, ItemCode, locationID, TolocationID);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    ds.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_StockTransfer.xsd");
                    l_Report.Load(Application.StartupPath + @"\Report\RptStockTransfer.rpt");

                    string fromDate = dtpFromDate.Value.ToString("dd-MM-yyyy");
                    string toDate = dtpToDate.Value.ToString("dd-MM-yyyy");
                    l_Report.DataDefinition.FormulaFields[2].Text = "#" + fromDate + "#";
                    l_Report.DataDefinition.FormulaFields[3].Text = "#" + toDate + "#";

                    l_Report.SetDataSource(ds.Tables[0]);
                    l_Report.SetDatabaseLogon("sa", "moeyan");

                    List<BOLSystem> lstsystem = new List<BOLSystem>();
                    lstsystem = dalsystem.ShowAllSystem();

                    if (lstsystem.Count > 0)
                    {
                        List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                        lstvoucherSetting = dalvoucher.SelectAllVoucher();

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

                        //l_Report.PrintOptions.PrinterName = lstsystem[0].Printervoucher;
                        //l_Report.PrintToPrinter(1, false, 0, 0);
                        frmReport frmReport = new frmReport();
                        frmReport.rptViewer.ReportSource = l_Report;
                        frmReport.rptViewer.Refresh();
                        frmReport.Show();
                        this.UseWaitCursor = false;
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

        private void btnAdjustmentDetailReport_Click(object sender, EventArgs e)
        {
            try
            {
                string ItemCode = ""; long locationID = 0; long TolocationID = 0;
                int AdjustmentTypeID = 0;

                if (lblItemCode.Text != "")
                {
                    ItemCode = txtStock.Text;
                }
                else
                {
                    ItemCode = "";
                }                

                if (cboLocation.Text != "<Select a Location>")
                {
                    locationID = Int32.Parse(cboLocation.SelectedValue.ToString());
                }
                else
                {
                    locationID = 0;
                }

                if (cboToLocation.Text != "<Select a Location>")
                {
                    TolocationID = Int32.Parse(cboToLocation.SelectedValue.ToString());
                }
                else
                {
                    TolocationID = 0;
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

                MoeYanPOS.Report.rptAdjustmentDetail rpt = new MoeYanPOS.Report.rptAdjustmentDetail();
                ReportDocument l_Report = new ReportDocument();
                DataSet ds = new DataSet();
                ds =dalstockhistory.GetStockTransferReport(StartDateTime, EndDateTime, ItemCode,  locationID,TolocationID);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    ds.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_StockTransfer.xsd");
                    l_Report.Load(Application.StartupPath + @"\Report\rptStockTransferDetail.rpt");

                    string fromDate = dtpFromDate.Value.ToString("dd-MM-yyyy");
                    string toDate = dtpToDate.Value.ToString("dd-MM-yyyy");
                    l_Report.DataDefinition.FormulaFields[3].Text = "#" + fromDate + "#";
                    l_Report.DataDefinition.FormulaFields[4].Text = "#" + toDate + "#";
                    string location = "From ";
                    if (cboLocation.SelectedIndex == 0)
                    {
                        location+= " All Outlet";
                    }
                    else
                    {
                        location += cboLocation.Text;
                    }
                    location += " To ";
                    if (cboToLocation.SelectedIndex == 0)
                    {
                        location += " All Outlet";
                    }
                    else
                    {
                        location += cboToLocation.Text;
                    }
                    l_Report.DataDefinition.FormulaFields[5].Text = "'" + location + "'";
                    l_Report.SetDataSource(ds.Tables[0]);
                    l_Report.SetDatabaseLogon("sa", "moeyan");

                    List<BOLSystem> lstsystem = new List<BOLSystem>();
                    lstsystem = dalsystem.ShowAllSystem();

                    if (lstsystem.Count > 0)
                    {
                        List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                        lstvoucherSetting = dalvoucher.SelectAllVoucher();

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

                        //l_Report.PrintOptions.PrinterName = lstsystem[0].Printervoucher;
                        //l_Report.PrintToPrinter(1, false, 0, 0);
                        frmReport frmReport = new frmReport();
                        frmReport.rptViewer.ReportSource = l_Report;
                        frmReport.rptViewer.Refresh();
                        frmReport.Show();
                        this.UseWaitCursor = false;
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

        private void dgvAdjustment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
