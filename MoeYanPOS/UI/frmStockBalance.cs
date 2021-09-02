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
using CrystalDecisions.Shared;

namespace MoeYanPOS.UI
{
    public partial class frmStockBalance : Form
    {
        DALStock dalstock = new DALStock();
        DALClass dalclass = new DALClass();
        DALCategory dalcategory = new DALCategory();
        DALSystem dalSystem = new DALSystem();
        DALVoucherSetting dalVoucher = new DALVoucherSetting();
        DALLocation dalLocation = new DALLocation();

        public frmStockBalance()
        {
            InitializeComponent();
        }

        private void txtStock_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ShowAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void ShowAll()
        {
            try
            {
                int CatID=0;int ClassID=0;int Value=0;
                string ItemCode = ""; string BalanceType = ""; long LocationID = 0;   
                DateTime StartDateTime; DateTime EndDateTime;

                if (cboClass.Text != "<Select a Class>")
                {
                    ClassID = Int32.Parse(cboClass.SelectedValue.ToString());
                }

                if (cboCategory.Text != "<Select a Category>")
                {
                    CatID = Int32.Parse(cboCategory.SelectedValue.ToString());
                }

                if (txtItemCode.Text != "")
                {
                    ItemCode = txtItemCode.Text;
                }

                if (cboLocation.Text != "<Select a Location>")
                {
                    LocationID = long.Parse(cboLocation.SelectedValue.ToString());
                }
                else
                {
                    List<BolLocation> LstLocation = new List<BolLocation>();
                    LstLocation = dalLocation.SelectAllLocation();
                    for (int i = 0; i < LstLocation.Count; i++)
                    {
                        if (LstLocation[i].IsThisLocation)
                        {
                            LocationID = LstLocation[i].ID;
                        }
                    }
                    
                }

                if (chkBalanceType.Checked)
                {
                    if (chkPositive.Checked)
                    {
                        if (txtGraterThanOrEqual.Text != "0")
                        {
                            Value = Int32.Parse(txtGraterThanOrEqual.Text);
                        }
                        else
                        {
                            if (BalanceType == "")
                            {
                                BalanceType += "Where Qty >0";
                            }
                            else
                            {
                                BalanceType += "OR Qty >0";
                            }
                        }
                    }                    

                    if (chkNegative.Checked)
                    {
                        if (BalanceType == "")
                        {
                            BalanceType += " Where Qty <0";
                        }
                        else
                        {
                            BalanceType += " OR Qty <0";
                        }
                        
                    }

                    if (chkZero.Checked)
                    {
                        if (BalanceType == "")
                        {
                            BalanceType += "Where Qty=0";
                        }
                        else
                        {
                            BalanceType += "OR Qty=0";
                        }
                        
                    }
                }

                string sm = dtpFromDate.Value.Month.ToString().Length > 1 ? dtpFromDate.Value.Month.ToString() : "0" + dtpFromDate.Value.Month.ToString();
                string sd = dtpFromDate.Value.Day.ToString().Length > 1 ? dtpFromDate.Value.Day.ToString() : "0" + dtpFromDate.Value.Day.ToString();
                string lm = dtpToDate.Value.Month.ToString().Length > 1 ? dtpToDate.Value.Month.ToString() : "0" + dtpToDate.Value.Month.ToString();
                string ld = dtpToDate.Value.Day.ToString().Length > 1 ? dtpToDate.Value.Day.ToString() : "0" + dtpToDate.Value.Day.ToString();

                string sdate = dtpFromDate.Value.Year.ToString() + "-" + sm + "-" + sd + " 00:00:00.000";
                string ldate = dtpToDate.Value.Year.ToString() + "-" + lm + "-" + ld + " 23:59:59.000";

                StartDateTime = DateTime.Parse(sdate);
                EndDateTime = DateTime.Parse(ldate);

                DataSet ds = new DataSet();
                ds = dalstock.GetStockBalance(StartDateTime, EndDateTime, CatID, ClassID, ItemCode, BalanceType, Value,LocationID);
                dgvStockBalance.Rows.Clear();

                //string input = head.ToString();
                //string sub;
                //if (input != "")
                //    sub = input.Substring(0, 4);
                //else
                //    sub = "";

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //dgvsearchreport.Rows.Clear();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvStockBalance.Rows.Add(ds.Tables[0].Rows[i].ItemArray[0].ToString(), ds.Tables[0].Rows[i].ItemArray[1].ToString(), ds.Tables[0].Rows[i].ItemArray[2].ToString(), ds.Tables[0].Rows[i].ItemArray[3].ToString(), ds.Tables[0].Rows[i].ItemArray[4].ToString(), ds.Tables[0].Rows[i].ItemArray[7].ToString(), ds.Tables[0].Rows[i].ItemArray[5].ToString(), ds.Tables[0].Rows[i].ItemArray[6].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmStockBalance_Load(object sender, EventArgs e)
        {
            try
            {
                LoadCategory();
                LoadClass();
                LoadLocation();
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

        private void chkPositive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkPositive.Checked)
                {
                    txtGraterThanOrEqual.Enabled = true;                    
                }
                else
                {
                    txtGraterThanOrEqual.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void picClose_Click(object sender, EventArgs e)
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

        private void chkNegative_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //if (chkNegative.Checked)
                //{                   
                //    chkPositive.Checked = false;
                //    chkZero.Checked = false;
                //}                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void chkZero_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //if (chkZero.Checked)
                //{
                //    chkPositive.Checked = false;
                //    chkNegative.Checked = false;
                //}
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
                MoeYanPOS.Report.RptStockBalance rpt = new MoeYanPOS.Report.RptStockBalance();
                ReportDocument l_Report = new ReportDocument();
                DataSet ds = new DataSet();

                int CatID = 0; int ClassID = 0; int Value = 0;long LocationID=0;
                string ItemCode = ""; string BalanceType = "";
                DateTime StartDateTime; DateTime EndDateTime;

                if (cboClass.Text != "<Select a Class>")
                {
                    ClassID = Int32.Parse(cboClass.SelectedValue.ToString());
                }

                if (cboCategory.Text != "<Select a Category>")
                {
                    CatID = Int32.Parse(cboCategory.SelectedValue.ToString());
                }

                if (txtItemCode.Text != "")
                {
                    ItemCode = txtItemCode.Text;
                }

                if (cboLocation.Text != "<Select a Location>")
                {
                    LocationID = long.Parse(cboLocation.SelectedValue.ToString());
                }
                else
                {
                    List<BolLocation> LstLocation = new List<BolLocation>();
                    LstLocation = dalLocation.SelectAllLocation();
                    for (int i = 0; i < LstLocation.Count; i++)
                    {
                        if (LstLocation[i].IsThisLocation)
                        {
                            LocationID = LstLocation[i].ID;
                        }
                    }

                }

                if (chkBalanceType.Checked)
                {
                    if (chkPositive.Checked)
                    {
                        if (txtGraterThanOrEqual.Text != "0")
                        {
                            Value = Int32.Parse(txtGraterThanOrEqual.Text);
                        }
                        else
                        {
                            if (BalanceType == "")
                            {
                                BalanceType += "Where Qty >0";
                            }
                            else
                            {
                                BalanceType += "OR Qty >0";
                            }
                        }
                    }

                    if (chkNegative.Checked)
                    {
                        if (BalanceType == "")
                        {
                            BalanceType += " Where Qty <0";
                        }
                        else
                        {
                            BalanceType += " OR Qty <0";
                        }

                    }

                    if (chkZero.Checked)
                    {
                        if (BalanceType == "")
                        {
                            BalanceType += "Where Qty=0";
                        }
                        else
                        {
                            BalanceType += "OR Qty=0";
                        }

                    }
                }

                string sm = dtpFromDate.Value.Month.ToString().Length > 1 ? dtpFromDate.Value.Month.ToString() : "0" + dtpFromDate.Value.Month.ToString();
                string sd = dtpFromDate.Value.Day.ToString().Length > 1 ? dtpFromDate.Value.Day.ToString() : "0" + dtpFromDate.Value.Day.ToString();
                string lm = dtpToDate.Value.Month.ToString().Length > 1 ? dtpToDate.Value.Month.ToString() : "0" + dtpToDate.Value.Month.ToString();
                string ld = dtpToDate.Value.Day.ToString().Length > 1 ? dtpToDate.Value.Day.ToString() : "0" + dtpToDate.Value.Day.ToString();

                string sdate = dtpFromDate.Value.Year.ToString() + "-" + sm + "-" + sd + " 00:00:00.000";
                string ldate = dtpToDate.Value.Year.ToString() + "-" + lm + "-" + ld + " 23:59:59.000";

                StartDateTime = DateTime.Parse(sdate);
                EndDateTime = DateTime.Parse(ldate);

                //DataSet ds = new DataSet();
                ds = dalstock.GetStockBalance(StartDateTime, EndDateTime, CatID, ClassID, ItemCode, BalanceType, Value, LocationID);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    ds.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_StockBalance.xsd");
                    l_Report.Load(Application.StartupPath + @"\Report\RptStockBalance.rpt");
                    string ToDate = dtpToDate.Value.ToString("dd-MM-yyyy");

                    if (cboLocation.Text == "<Select a Location>")
                    {
                        List<BolLocation> lstLocation = new List<BolLocation>();
                        lstLocation = dalLocation.SelectAllLocation();
                        l_Report.DataDefinition.FormulaFields[4].Text = "'" + lstLocation[0].Location + "'";
                    }
                    else
                    {
                        l_Report.DataDefinition.FormulaFields[4].Text = "'" + cboLocation.Text+ "'";
                    }
                    l_Report.DataDefinition.FormulaFields[5].Text = "#" + ToDate + "#";
                    
                   

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

                        CrystalDecisions.Shared.ParameterFields lstPfSoci = new CrystalDecisions.Shared.ParameterFields();
                        CrystalDecisions.Shared.ParameterField pfSoci = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue disvalueSoci = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        pfSoci = new ParameterField();
                        disvalueSoci = new ParameterDiscreteValue();
                        pfSoci.Name = "DateRange";
                        disvalueSoci.Value = dtpToDate.Text;
                        pfSoci.CurrentValues.Add(disvalueSoci);
                        lstPfSoci.Add(pfSoci);

                        frmReport frmReport = new frmReport();
                        frmReport.rptViewer.ParameterFieldInfo = lstPfSoci;
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
                MoeYanPOS.Report.RptStockBalance rpt = new MoeYanPOS.Report.RptStockBalance();
                ReportDocument l_Report = new ReportDocument();
                DataSet ds = new DataSet();

                int CatID = 0; int ClassID = 0; int Value = 0; long LocationID = 0;
                string ItemCode = ""; string BalanceType = "";
                DateTime StartDateTime; DateTime EndDateTime;

                if (cboClass.Text != "<Select a Class>")
                {
                    ClassID = Int32.Parse(cboClass.SelectedValue.ToString());
                }

                if (cboCategory.Text != "<Select a Category>")
                {
                    CatID = Int32.Parse(cboCategory.SelectedValue.ToString());
                }

                if (txtItemCode.Text != "")
                {
                    ItemCode = txtItemCode.Text;
                }

                if (cboLocation.Text != "<Select a Location>")
                {
                    LocationID = long.Parse(cboLocation.SelectedValue.ToString());
                }

                if (chkBalanceType.Checked)
                {
                    if (chkPositive.Checked)
                    {
                        if (txtGraterThanOrEqual.Text != "0")
                        {
                            Value = Int32.Parse(txtGraterThanOrEqual.Text);
                        }
                        else
                        {
                            if (BalanceType == "")
                            {
                                BalanceType += "Where Qty >0";
                            }
                            else
                            {
                                BalanceType += "OR Qty >0";
                            }
                        }
                    }

                    if (chkNegative.Checked)
                    {
                        if (BalanceType == "")
                        {
                            BalanceType += " Where Qty <0";
                        }
                        else
                        {
                            BalanceType += " OR Qty <0";
                        }

                    }

                    if (chkZero.Checked)
                    {
                        if (BalanceType == "")
                        {
                            BalanceType += "Where Qty=0";
                        }
                        else
                        {
                            BalanceType += "OR Qty=0";
                        }

                    }
                }

                string sm = dtpFromDate.Value.Month.ToString().Length > 1 ? dtpFromDate.Value.Month.ToString() : "0" + dtpFromDate.Value.Month.ToString();
                string sd = dtpFromDate.Value.Day.ToString().Length > 1 ? dtpFromDate.Value.Day.ToString() : "0" + dtpFromDate.Value.Day.ToString();
                string lm = dtpToDate.Value.Month.ToString().Length > 1 ? dtpToDate.Value.Month.ToString() : "0" + dtpToDate.Value.Month.ToString();
                string ld = dtpToDate.Value.Day.ToString().Length > 1 ? dtpToDate.Value.Day.ToString() : "0" + dtpToDate.Value.Day.ToString();

                string sdate = dtpFromDate.Value.Year.ToString() + "-" + sm + "-" + sd + " 00:00:00.000";
                string ldate = dtpFromDate.Value.Year.ToString() + "-" + lm + "-" + ld + " 12:00:00.000";

                StartDateTime = DateTime.Parse(sdate);
                EndDateTime = DateTime.Parse(ldate);

                //DataSet ds = new DataSet();
                ds = dalstock.GetStockBalance(StartDateTime, EndDateTime, CatID, ClassID, ItemCode, BalanceType, Value,LocationID);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    ds.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_StockBalance.xsd");
                    l_Report.Load(Application.StartupPath + @"\Report\RptStockBalance.rpt");
                    string ToDate = dtpToDate.Text;

                    if (cboLocation.Text != "<Select a Location>")
                    {
                        List<BolLocation> lstLocation = new List<BolLocation>();
                        lstLocation = dalLocation.SelectAllLocation();
                        l_Report.DataDefinition.FormulaFields[4].Text = "'" + lstLocation[0].Location + "'";
                    }
                    else
                    {
                        l_Report.DataDefinition.FormulaFields[4].Text = "'" + cboLocation.Text + "'";
                    }
                    l_Report.DataDefinition.FormulaFields[5].Text = "#" + ToDate + "#";

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

        private void dgvItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtItemCode.Text = dgvItemCode.CurrentRow.Cells[1].Value.ToString();
                    dgvItemCode.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvStockBalance_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                cboCategory.SelectedIndex = 0;
                cboClass.SelectedIndex = 0;
                chkBalanceType.Checked = false;
                chkPositive.Checked = false;
                chkNegative.Checked = false;
                chkZero.Checked = false;
                txtGraterThanOrEqual.Text = "";
                txtItemCode.Text = "";
                dgvItemCode.Visible = false;
                dgvStockBalance.Rows.Clear();
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

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    List<BOLStock> lstStk = new List<BOLStock>();
                    lstStk = dalstock.SelectStock(txtItemCode.Text, 0, "", 0);
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

        private void frmStockBalance_Click(object sender, EventArgs e)
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

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dgvItemCode_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
