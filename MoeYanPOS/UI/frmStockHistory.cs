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
    public partial class frmStockHistory : Form
    {
        DALSale dalsale = new DALSale();
        DALStock dalstock = new DALStock();
        DALCustomer dalcustomer = new DALCustomer();
        DALSystem dalSystem = new DALSystem();
        DALClass dalclass = new DALClass();
        DALCategory dalcategory = new DALCategory();
        DALLocation dalLocation = new DALLocation();
        DALUser daluser = new DALUser();
        DALVoucherSetting dalVoucher = new DALVoucherSetting();
        DateTime StartDateTime;
        DateTime EndDateTime;
        long customerid = 0;
        string voucherno = "";
        int classid = 0; int catid = 0; string itemcode = ""; int userid = 0; long locationid = 0;

        public frmStockHistory()
        {
            InitializeComponent();
            this.dgvStockBalance.ColumnHeadersDefaultCellStyle.BackColor = Color.Yellow;         
        }
        private void ChangeColumnWidth()
        {
            dgvStockBalance.Columns[0].Width = 100;
        }
        private void frmStockHistory_Load(object sender, EventArgs e)
        {
            try
            {
                LoadCategory();
                LoadClass();
                LoadLocation();
                

                //for (int j = 0; j < this.dgvStockBalance.ColumnCount; j++)
                //{
                //    this.dgvStockBalance.Columns[j].Width = 100;
                //}

                this.dgvStockBalance.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                this.dgvStockBalance.ColumnHeadersHeight = this.dgvStockBalance.ColumnHeadersHeight * 2;
                this.dgvStockBalance.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                this.dgvStockBalance.CellPainting += new DataGridViewCellPaintingEventHandler(dgvStockBalance_CellPainting);
                this.dgvStockBalance.Paint += new PaintEventHandler(dgvStockBalance_Paint);
                this.dgvStockBalance.Scroll += new ScrollEventHandler(dgvStockBalance_Scroll);
                this.dgvStockBalance.ColumnWidthChanged += new DataGridViewColumnEventHandler(dgvStockBalance_ColumnWidthChanged);
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

        private void dgvItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.KeyCode == Keys.Enter)
                //{
                //    List<BOLStock> lstStk = new List<BOLStock>();
                //    lstStk = dalstock.SelectStock("", 0, txtStock.Text, 0);
                //    dgvItemCode.Rows.Clear();
                //    if (lstStk.Count > 0)
                //    {
                //        for (int i = 0; i < lstStk.Count; i++)
                //        {
                //            dgvItemCode.Rows.Add(lstStk[i].Id, lstStk[i].ItemCode, lstStk[i].NameEng, lstStk[i].NameMM, lstStk[i].Price);
                //        }
                //        dgvItemCode.Visible = true;
                //        dgvItemCode.Focus();
                //    }
                //}
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
                int CatID = 0; int ClassID = 0; 
                string ItemCode = ""; long LocationID = 0;
                DateTime StartDateTime; DateTime EndDateTime;
                
                if (cboClass.Text != "<Select a Class>")
                {
                    ClassID = Int32.Parse(cboClass.SelectedValue.ToString());
                }

                if (cboCategory.Text != "<Select a Category>")
                {
                    CatID = Int32.Parse(cboCategory.SelectedValue.ToString());
                }

                if (txtStock.Text != "")
                {
                    ItemCode = txtStock.Text;
                }

                if (cboLocation.Text != "<Select a Location>")
                {
                    LocationID = long.Parse(cboLocation.SelectedValue.ToString());
                }

                string sm = dtpFromDate.Value.Month.ToString().Length > 1 ? dtpFromDate.Value.Month.ToString() : "0" + dtpFromDate.Value.Month.ToString();
                string sd = dtpFromDate.Value.Day.ToString().Length > 1 ? dtpFromDate.Value.Day.ToString() : "0" + dtpFromDate.Value.Day.ToString();
                string lm = dtpToDate.Value.Month.ToString().Length > 1 ? dtpToDate.Value.Month.ToString() : "0" + dtpToDate.Value.Month.ToString();
                string ld = dtpToDate.Value.Day.ToString().Length > 1 ? dtpToDate.Value.Day.ToString() : "0" + dtpToDate.Value.Day.ToString();

                string sdate = dtpFromDate.Value.Year.ToString() + "-" + sm + "-" + sd +" 00:00:00.000";
                string ldate = dtpToDate.Value.Year.ToString() + "-" + lm + "-" + ld +" 23:59:59.000";

                StartDateTime = DateTime.Parse(sdate);
                EndDateTime = DateTime.Parse(ldate);

                DataSet ds = new DataSet();
                ds = dalstock.GetStockHistory(StartDateTime, EndDateTime, ClassID, CatID, LocationID, ItemCode);
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
                        dgvStockBalance.Rows.Add(ds.Tables[0].Rows[i].ItemArray[0].ToString(), ds.Tables[0].Rows[i].ItemArray[1].ToString(), ds.Tables[0].Rows[i].ItemArray[2].ToString(), ds.Tables[0].Rows[i].ItemArray[3].ToString(), ds.Tables[0].Rows[i].ItemArray[4].ToString(), ds.Tables[0].Rows[i].ItemArray[5].ToString(), ds.Tables[0].Rows[i].ItemArray[6].ToString(), ds.Tables[0].Rows[i].ItemArray[7].ToString(), ds.Tables[0].Rows[i].ItemArray[10].ToString(), ds.Tables[0].Rows[i].ItemArray[8].ToString(), ds.Tables[0].Rows[i].ItemArray[9].ToString(), ds.Tables[0].Rows[i].ItemArray[11].ToString());
                    }
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

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                MoeYanPOS.Report.RptStockBalance rpt = new MoeYanPOS.Report.RptStockBalance();
                ReportDocument l_Report = new ReportDocument();
               
                int CatID = 0; int ClassID = 0;
                string ItemCode = ""; long LocationID = 0;
                DateTime StartDateTime; DateTime EndDateTime;

                if (cboClass.Text != "<Select a Class>")
                {
                    ClassID = Int32.Parse(cboClass.SelectedValue.ToString());
                }

                if (cboCategory.Text != "<Select a Category>")
                {
                    CatID = Int32.Parse(cboCategory.SelectedValue.ToString());
                }

                if (txtStock.Text != "")
                {
                    ItemCode = txtStock.Text;
                }

                if (cboLocation.Text != "<Select a Location>")
                {
                    LocationID = long.Parse(cboLocation.SelectedValue.ToString());
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
                ds = dalstock.GetStockHistory(StartDateTime, EndDateTime, ClassID, CatID, LocationID, ItemCode);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    ds.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_StockHistory.xsd");
                    l_Report.Load(Application.StartupPath + @"\Report\RptStockHistory.rpt");

                    string ToDate = dtpToDate.Value.ToString("MM-dd-yyyy");
                    string FromDate = dtpFromDate.Value.ToString("dd-MM-yyyy");

                    l_Report.DataDefinition.FormulaFields[1].Text = "#" + dtpFromDate.Value.ToString("MM-dd-yyyy") + "#";
                    l_Report.DataDefinition.FormulaFields[0].Text = "#" + dtpToDate.Value.ToString() + "#";

                    l_Report.SetDataSource(ds.Tables[0]);
                    l_Report.SetDatabaseLogon("sa", "moeyan");

                    List<BOLSystem> lstsystem = new List<BOLSystem>();
                    lstsystem = dalSystem.ShowAllSystem();

                    if (lstsystem.Count > 0)
                    {
                        List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                        lstvoucherSetting = dalVoucher.SelectAllVoucher();

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

        private void dgvStockBalance_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string Itemcode = dgvStockBalance.CurrentRow.Cells[0].Value.ToString();
                if (Itemcode != "")
                {
                    if (e.ColumnIndex == 7)
                    {
                        //Sale
                        frmSaleHistory salehistory = new frmSaleHistory(Itemcode);
                        salehistory.ShowDialog();
                    }
                    if (e.ColumnIndex == 3)
                    {
                        //purchase
                        frmPurchaseHistory purchaseHistory = new frmPurchaseHistory(Itemcode);
                        purchaseHistory.ShowDialog();
                    }
                    if (e.ColumnIndex == 6| e.ColumnIndex == 9)
                    {
                        //Adjustment
                        frmAdjustmentHistory adjustmenthistory = new frmAdjustmentHistory(Itemcode);
                        adjustmenthistory.ShowDialog();
                    }
                    if (e.ColumnIndex == 4)
                    {
                        //Sale Return
                        frmSaleReturnHistory saleReturnhistory = new frmSaleReturnHistory(Itemcode);
                        saleReturnhistory.ShowDialog();
                    }
                    if (e.ColumnIndex == 5 | e.ColumnIndex == 8)
                    {
                        //Stock Transfer
                        frmStockTransferHistory stockTransferhistory = new frmStockTransferHistory(Itemcode);
                        stockTransferhistory.ShowDialog();
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                MoeYanPOS.Report.RptStockBalance rpt = new MoeYanPOS.Report.RptStockBalance();
                ReportDocument l_Report = new ReportDocument();

                int CatID = 0; int ClassID = 0;
                string ItemCode = ""; long LocationID = 0;
                DateTime StartDateTime; DateTime EndDateTime;

                if (cboClass.Text != "<Select a Class>")
                {
                    ClassID = Int32.Parse(cboClass.SelectedValue.ToString());
                }

                if (cboCategory.Text != "<Select a Category>")
                {
                    CatID = Int32.Parse(cboCategory.SelectedValue.ToString());
                }

                if (txtStock.Text != "")
                {
                    ItemCode = txtStock.Text;
                }

                if (cboLocation.Text != "<Select a Location>")
                {
                    LocationID = long.Parse(cboLocation.SelectedValue.ToString());
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
                ds = dalstock.GetStockHistory(StartDateTime, EndDateTime, ClassID, CatID, LocationID, ItemCode);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    ds.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_StockHistory.xsd");
                    l_Report.Load(Application.StartupPath + @"\Report\RptStockHistory.rpt");
                    string ToDate = dtpToDate.Value.ToString("dd-MM-yyyy");
                    string FromDate = dtpFromDate.Value.ToString("dd-MM-yyyy");
                    l_Report.DataDefinition.FormulaFields[1].Text = "#" + FromDate + "#";
                    l_Report.DataDefinition.FormulaFields[0].Text = "#" + ToDate + "#";

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

                            //l_Report.Subreports[0].SetDataSource(dtt);
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

        private void dgvStockBalance_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1 && e.ColumnIndex > -1)
                {
                    Rectangle r2 = e.CellBounds;
                    r2.Y += e.CellBounds.Height / 2;
                    r2.Height = e.CellBounds.Height / 2;
                    e.PaintBackground(r2, true);
                    e.PaintContent(r2);
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvStockBalance_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                //string[] monthes = { "AA", "BB", "CC", "DD", "EE" };
                //for (int j =1; j < 10; )
                //{                   
                //        Rectangle r1 = this.dgvStockBalance.GetCellDisplayRectangle(j, -1, true);
                //        int w2 = this.dgvStockBalance.GetCellDisplayRectangle(j +1, -1, true).Width;
                //        r1.X += 2;
                //        r1.Y += 1;
                //        r1.Width = r1.Width + w2 - 2;
                //        r1.Height = r1.Height / 2 - 2;
                //        e.Graphics.FillRectangle(new SolidBrush(this.dgvStockBalance.ColumnHeadersDefaultCellStyle.BackColor), r1);
                //        StringFormat format = new StringFormat();
                //        format.Alignment = StringAlignment.Center;
                //        format.LineAlignment = StringAlignment.Center;
                //        e.Graphics.DrawString(monthes[j / 3],
                //        this.dgvStockBalance.ColumnHeadersDefaultCellStyle.Font,
                //        new SolidBrush(this.dgvStockBalance.ColumnHeadersDefaultCellStyle.ForeColor),
                //        r1,
                //        format);
                //        j +=3;

                string[] monthes = { "Issue", "Receive" };
                int k = 3;
                    Rectangle r1 = this.dgvStockBalance.GetCellDisplayRectangle(k, -1, true);
                    Rectangle r2 = this.dgvStockBalance.GetCellDisplayRectangle(k, -1, true);
                    int w2 = this.dgvStockBalance.GetCellDisplayRectangle(k - 1, -1, true).Width;
                    r1.X += 1;
                    r1.Y += 1;
                    r2.X += 400;
                    r2.Y += 1;
                    r2.Width = (r2.Width + w2-50) * 2;
                    r2.Height = r2.Height / 2 - 2;
                    r1.Width = (r1.Width + w2 - 3) * 2;
                    r1.Height = r1.Height / 2 - 2;

                    e.Graphics.FillRectangle(new SolidBrush(SystemColors.Control), r1);
                    e.Graphics.FillRectangle(new SolidBrush(SystemColors.Control), r2);
                    //for (int j = 3; j < 8; )
                    //{
                       
                            StringFormat format = new StringFormat();
                            format.Alignment = StringAlignment.Center;
                            format.LineAlignment = StringAlignment.Center;
                       
                            e.Graphics.DrawString(monthes[k / 3],
                            this.dgvStockBalance.ColumnHeadersDefaultCellStyle.Font,
                            new SolidBrush(this.dgvStockBalance.ColumnHeadersDefaultCellStyle.ForeColor),
                            r1,
                            format);

                            int j = 5;
                           
                            e.Graphics.DrawString(monthes[j / 6],
                            this.dgvStockBalance.ColumnHeadersDefaultCellStyle.Font,
                            new SolidBrush(this.dgvStockBalance.ColumnHeadersDefaultCellStyle.ForeColor),
                            r2,
                            format);
                            //j += 4;
                            
                //}
                }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvStockBalance_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                Rectangle rtHeader = this.dgvStockBalance.DisplayRectangle;
                rtHeader.Height = this.dgvStockBalance.ColumnHeadersHeight / 2;                
                this.dgvStockBalance.Invalidate(rtHeader);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvStockBalance_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            try
            {
                Rectangle rtHeader = this.dgvStockBalance.DisplayRectangle;
                rtHeader.Height = this.dgvStockBalance.ColumnHeadersHeight / 50;
                //rtHeader.Width = this.dgvStockBalance.Width *5;
                this.dgvStockBalance.Invalidate(rtHeader);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmStockHistory_Click(object sender, EventArgs e)
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
    }
}
