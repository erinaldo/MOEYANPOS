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
    public partial class frmAdjustmentHistory : Form
    {
        DALStock dalstock = new DALStock();
        DALClass dalclass = new DALClass();
        DALCategory dalcategory = new DALCategory();
        DALBrand dalbrand = new DALBrand();
        DALMeasurement dalmeasurement = new DALMeasurement();
        DALAdjustment daladjustment = new DALAdjustment();
        DALAdjustmentDetail daladjustmentDetail = new DALAdjustmentDetail();
        DALAdjustmentType dalAdjustmentType = new DALAdjustmentType();
        DALSystem dalSystem = new DALSystem();
        DALVoucherSetting dalVoucher = new DALVoucherSetting();
        DALLocation dalLocation = new DALLocation();

        public frmAdjustmentHistory()
        {
            InitializeComponent();
        }

        public frmAdjustmentHistory(string Itemcode)
        {
            try
            {
                InitializeComponent();
                txtStock.Text = Itemcode;
                btnSearch.Focus();
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

        private void GetReport()
        {
            try
            {
                string ItemCode = ""; long locationID = 0;
                int AdjustmentTypeID = 0;
                
                if (lblItemCode.Text!="")
                {
                    ItemCode = txtStock.Text;
                }
                else
                {
                    ItemCode = "";
                }

                if (cboHeader.Text != "<Select a Class>")
                {
                    AdjustmentTypeID = Int32.Parse(cboHeader.SelectedValue.ToString());
                }
                else
                {
                    AdjustmentTypeID = 0;
                }

                if (cboLocation.Text != "<Select a Location>")
                {
                    locationID = Int32.Parse(cboLocation.SelectedValue.ToString());
                }
                else
                {
                    locationID = 0;
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
                ds = daladjustment.GetAdjustmentHistory(StartDateTime, EndDateTime, ItemCode, AdjustmentTypeID, locationID);
                dgvAdjustment.Rows.Clear();
                dgvAdjDetail.Rows.Clear();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvAdjustment.Rows.Add(ds.Tables[0].Rows[i].ItemArray[0].ToString(), ds.Tables[0].Rows[i].ItemArray[1].ToString(), ds.Tables[0].Rows[i].ItemArray[2].ToString(), ds.Tables[0].Rows[i].ItemArray[3].ToString(), ds.Tables[0].Rows[i].ItemArray[4].ToString(), "", "");
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvAdjustment_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                long ID = 0;
                ID = long.Parse(dgvAdjustment.Rows[e.RowIndex].Cells[0].Value.ToString());
                DataSet ds = new DataSet();
                ds = daladjustmentDetail.GetAdjustmentDetail(ID);
                dgvAdjDetail.Rows.Clear();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvAdjDetail.Rows.Add( ds.Tables[0].Rows[i].ItemArray[1].ToString(), ds.Tables[0].Rows[i].ItemArray[2].ToString(), ds.Tables[0].Rows[i].ItemArray[3].ToString(),ds.Tables[0].Rows[i].ItemArray[0].ToString(), ds.Tables[0].Rows[i].ItemArray[4].ToString(), ds.Tables[0].Rows[i].ItemArray[5].ToString());
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
                        frmStockAdjustment adjustment = new frmStockAdjustment(id);
                        adjustment.ShowDialog();
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
                            isdelete = daladjustment.DeleteAdjustment(id);
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

        private void txtStock_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void frmAdjustmentHistory_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAdjType();
                LoadLocation();
                GetReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void LoadAdjType()
        {
            try
            {
                DALAdjustmentType dal = new DALAdjustmentType();
                List<BOLAdjustmentType> lstAdjustmentType = new List<BOLAdjustmentType>();
                lstAdjustmentType = dal.ShowAllAdjustmentType();
                BOLAdjustmentType bolAdjustmentType = new BOLAdjustmentType();
                bolAdjustmentType.Header = "<Select Adjustment Type>";
                lstAdjustmentType.Insert(0, bolAdjustmentType);
                if (lstAdjustmentType.Count > 0)
                {
                    cboHeader.DisplayMember = "Header";
                    cboHeader.ValueMember = "ID";
                    cboHeader.DataSource = lstAdjustmentType;
                    cboHeader.SelectedIndex = 0;
                }

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

        private void btnAdjustmentReport_Click(object sender, EventArgs e)
        {
            try
            {
                string ItemCode = ""; long locationID = 0;
                int AdjustmentTypeID = 0;

                if (lblItemCode.Text != "")
                {
                    ItemCode = txtStock.Text;
                }
                else
                {
                    ItemCode = "";
                }

                if (cboHeader.Text != "<Select a Class>")
                {
                    AdjustmentTypeID = Int32.Parse(cboHeader.SelectedValue.ToString());
                }
                else
                {
                    AdjustmentTypeID = 0;
                }

                if (cboLocation.Text != "<Select a Location>")
                {
                    locationID = Int32.Parse(cboLocation.SelectedValue.ToString());
                }
                else
                {
                    locationID = 0;
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
                ds = daladjustment.GetAdjustmentReport(StartDateTime, EndDateTime, ItemCode, AdjustmentTypeID, locationID);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    ds.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_Adjustment.xsd");
                    l_Report.Load(Application.StartupPath + @"\Report\RptAdjustment.rpt");

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
                string ItemCode = ""; long locationID = 0;
                int AdjustmentTypeID = 0;

                if (lblItemCode.Text != "")
                {
                    ItemCode = txtStock.Text;
                }
                else
                {
                    ItemCode = "";
                }

                if (cboHeader.Text != "<Select a Class>")
                {
                    AdjustmentTypeID = Int32.Parse(cboHeader.SelectedValue.ToString());
                }
                else
                {
                    AdjustmentTypeID = 0;
                }

                if (cboLocation.Text != "<Select a Location>")
                {
                    locationID = Int32.Parse(cboLocation.SelectedValue.ToString());
                }
                else
                {
                    locationID = 0;
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
                ds = daladjustment.GetAdjustmentReport(StartDateTime, EndDateTime, ItemCode, AdjustmentTypeID,locationID);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    ds.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_Adjustment.xsd");
                    l_Report.Load(Application.StartupPath + @"\Report\rptAdjustmentDetail.rpt");

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
    }
}
