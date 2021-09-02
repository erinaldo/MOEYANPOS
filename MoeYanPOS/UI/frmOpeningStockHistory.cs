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
    public partial class frmOpeningStockHistory : Form
    {
        DALStock dalstock = new DALStock();
        DALOpeningStock dalopeiningStock = new DALOpeningStock();
        DALClass dalclass = new DALClass();
        DALCategory dalcategory = new DALCategory();
        DALSystem dalSystem = new DALSystem();
        DALVoucherSetting dalVoucher = new DALVoucherSetting();
        DALExchangeRate dalexchangerate = new DALExchangeRate();
        DALLocation dalLocation = new DALLocation();

        public frmOpeningStockHistory()
        {
            InitializeComponent();
        }

        private void txtStock_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dgvItemCode_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {                   
                    txtStock.Text = dgvItemCode.CurrentRow.Cells[1].Value.ToString();
                    dgvItemCode.Visible = false;                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmOpeningStockHistory_Load(object sender, EventArgs e)
        {
            try
            {
                LoadCategory();
                LoadClass();
                LoadCurrency();
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

        private void LoadCurrency()
        {
            try
            {
                List<BOLCurrency> lstCurrency = new List<BOLCurrency>();
                lstCurrency = dalexchangerate.FillCurrency();
                BOLCurrency bolCurrency = new BOLCurrency();
                bolCurrency.Id = 0;
                bolCurrency.Currency = "<Select a Currency>";
                lstCurrency.Insert(0, bolCurrency);
                cbocurrency.DisplayMember = "Currency";
                cbocurrency.ValueMember = "Id";
                cbocurrency.DataSource = lstCurrency;
                if (lstCurrency.Count > 0)
                {
                    
                    cbocurrency.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void LoadClass()
        {
            try
            {
                List<BOLClass> lstclass = new List<BOLClass>();
                lstclass = dalclass.SelectAllClass();
                BOLClass bolclass = new BOLClass();
                bolclass.Id = 0;
                bolclass.ClassName = "<Select a Class>";
                lstclass.Insert(0, bolclass);
                cboClass.ValueMember = "ID";
                cboClass.DisplayMember = "ClassName";
                cboClass.DataSource = lstclass;
                if (lstclass.Count > 0)
                {
                    cboClass.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void LoadCategory()
        {
            try
            {
                List<BOLCategory> lstcategory = new List<BOLCategory>();
                lstcategory = dalcategory.ShowAllCategory();
                BOLCategory bolcategory = new BOLCategory();
                bolcategory.Id = 0;
                bolcategory.Classname = "";
                bolcategory.CategoryName = "<Select a Category>";
                lstcategory.Insert(0, bolcategory);
                cboCategory.ValueMember = "Id";
                cboCategory.DisplayMember = "CategoryName";
                cboCategory.DataSource = lstcategory;
                if (lstcategory.Count > 0)
                {
                    cboCategory.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmOpeningStockHistory_Click(object sender, EventArgs e)
        {
            try
            {              
               dgvItemCode.Visible = false;
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
                string ItemCode = ""; int CatID = 0; int ClassID = 0; int CurrencyID = 0;long LocationID=0;

                if (txtStock.Text != "")
                {
                    ItemCode = txtStock.Text;
                }
                else
                {
                    ItemCode = "";
                }

                if (cboClass.Text != "<Select a Class>")
                {
                    ClassID = Int32.Parse(cboClass.SelectedValue.ToString());
                }

                if (cboCategory.Text != "<Select a Category>")
                {
                    CatID = Int32.Parse(cboCategory.SelectedValue.ToString());
                }

                if (cbocurrency.Text != "<Select a Currency>")
                {
                    CurrencyID = Int32.Parse(cbocurrency.SelectedValue.ToString());
                }

                if (cboLocation.Text != "<Select a Location>")
                {
                    LocationID = Int32.Parse(cboLocation.SelectedValue.ToString());
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
                ds = dalopeiningStock.ShowAllOpeningStock(ItemCode, StartDateTime, EndDateTime, ClassID, CatID, CurrencyID, LocationID);
                dgvOpeningStock.Rows.Clear();
                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvOpeningStock.Rows.Add(ds.Tables[0].Rows[i].ItemArray[0].ToString(), ds.Tables[0].Rows[i].ItemArray[1].ToString(), ds.Tables[0].Rows[i].ItemArray[2].ToString(), ds.Tables[0].Rows[i].ItemArray[3].ToString(), ds.Tables[0].Rows[i].ItemArray[4].ToString(), ds.Tables[0].Rows[i].ItemArray[5].ToString(), ds.Tables[0].Rows[i].ItemArray[6].ToString(), ds.Tables[0].Rows[i].ItemArray[7].ToString(), ds.Tables[0].Rows[i].ItemArray[11].ToString(), ds.Tables[0].Rows[i].ItemArray[8].ToString(), ds.Tables[0].Rows[i].ItemArray[9].ToString(), ds.Tables[0].Rows[i].ItemArray[10].ToString(), "", "");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvOpeningStock_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 12)
                {
                    if (e.RowIndex >= 0)
                    {
                        long id = 0;
                        id = long.Parse(dgvOpeningStock.Rows[e.RowIndex].Cells[0].Value.ToString());
                        frmOpeningStock OpeningStock = new frmOpeningStock(id);
                        OpeningStock.ShowDialog();
                        btnSearch_Click(sender, e);
                    }
                }
                if (e.ColumnIndex == 13)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int id = 0;
                            id = Int32.Parse(dgvOpeningStock.Rows[e.RowIndex].Cells[0].Value.ToString());
                            int isdelete = 0;
                            isdelete = dalopeiningStock.DeleteOpeningStock(id,0);
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
                string ItemCode = ""; int CatID = 0; int ClassID = 0; int CurrencyID = 0; long LocationID = 0;

                if (txtStock.Text != "")
                {
                    ItemCode = txtStock.Text;
                }
                else
                {
                    ItemCode = "";
                }

                if (cboClass.Text != "<Select a Class>")
                {
                    ClassID = Int32.Parse(cboClass.SelectedValue.ToString());
                }

                if (cboCategory.Text != "<Select a Category>")
                {
                    CatID = Int32.Parse(cboCategory.SelectedValue.ToString());
                }

                if (cboCategory.Text != "<Select a Currency>")
                {
                    CurrencyID = Int32.Parse(cbocurrency.SelectedValue.ToString());
                }

                if (cboLocation.Text != "<Select a Location>")
                {
                    LocationID = Int32.Parse(cboLocation.SelectedValue.ToString());
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
                ds = dalopeiningStock.ShowAllOpeningStock(ItemCode, StartDateTime, EndDateTime, ClassID, CatID, CurrencyID, LocationID);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    ds.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_OpeningStock.xsd");
                    l_Report.Load(Application.StartupPath + @"\Report\RptOpeningStock.rpt");

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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string ItemCode = ""; int CatID = 0; int ClassID = 0; int CurrencyID = 0; long LocationID = 0;

                if (txtStock.Text != "")
                {
                    ItemCode = txtStock.Text;
                }
                else
                {
                    ItemCode = "";
                }

                if (cboClass.Text != "<Select a Class>")
                {
                    ClassID = Int32.Parse(cboClass.SelectedValue.ToString());
                }

                if (cboCategory.Text != "<Select a Category>")
                {
                    CatID = Int32.Parse(cboCategory.SelectedValue.ToString());
                }
                 if (cboCategory.Text != "<Select a Currency>")
                {
                    CurrencyID = Int32.Parse(cbocurrency.SelectedValue.ToString());
                }

                 if (cboLocation.Text != "<Select a Location>")
                 {
                     LocationID = Int32.Parse(cboLocation.SelectedValue.ToString());
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

                MoeYanPOS.Report.RptOpeningStock rpt = new MoeYanPOS.Report.RptOpeningStock();
                ReportDocument l_Report = new ReportDocument();
                DataSet ds = new DataSet();
                ds = dalopeiningStock.ShowAllOpeningStock(ItemCode, StartDateTime, EndDateTime, ClassID, CatID, CurrencyID, LocationID);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    ds.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_OpeningStock.xsd");
                    l_Report.Load(Application.StartupPath + @"\Report\RptOpeningStock.rpt");

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

        private void txtStock_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //Get Stock
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
               
                txtStock.Text = "";
                if (cboCategory.Items.Count > 0)
                {
                    cboCategory.SelectedIndex = 0;
                }
                if (cboClass.Items.Count > 0)
                {
                    cboClass.SelectedIndex = 0;
                }
                if (cbocurrency.Items.Count > 0)
                {
                    cbocurrency.SelectedIndex = 0;
                }
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
    }
}
