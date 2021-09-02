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
    public partial class frmSaleHistory : Form
    {
        DALSale dalsale = new DALSale();
        DALStock dalstock = new DALStock();
        DALCustomer dalcustomer = new DALCustomer();
        DALSystem dalSystem = new DALSystem();
        DALClass dalclass = new DALClass();
        DALCategory dalcategory = new DALCategory();
        DALLocation dalLocation = new DALLocation();
        DALUser daluser = new DALUser();
        DateTime StartDateTime ;
        DateTime EndDateTime;
        long customerid = 0;
        string voucherno = "";
        int classid = 0; int catid = 0; string itemcode = ""; int userid = 0; long locationid = 0; string paymenttype = "";
        public frmSaleHistory()
        {           

            InitializeComponent();
        }
        public frmSaleHistory(string Itemcode)
        {
            try
            {
                InitializeComponent();
                txtStock.Text = Itemcode;
                dgvItemCode.Visible = false;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmSaleHistory_Load(object sender, EventArgs e)
        {
            try
            {                

                int customerid = 0;
                string voucherno = "";
                string sm = dtpFromDate.Value.Month.ToString().Length > 1 ? dtpFromDate.Value.Month.ToString() : "0" + dtpFromDate.Value.Month.ToString();
                string sd = dtpFromDate.Value.Day.ToString().Length > 1 ? dtpFromDate.Value.Day.ToString() : "0" + dtpFromDate.Value.Day.ToString();
                string lm = dtpToDate.Value.Month.ToString().Length > 1 ? dtpToDate.Value.Month.ToString() : "0" + dtpToDate.Value.Month.ToString();
                string ld = dtpToDate.Value.Day.ToString().Length > 1 ? dtpToDate.Value.Day.ToString() : "0" + dtpToDate.Value.Day.ToString();

                string sdate = dtpFromDate.Value.Year.ToString() + "-" + sm + "-" + sd + " 00:00:00.000";
                string ldate = dtpToDate.Value.Year.ToString() + "-" + lm + "-" + ld + " 23:59:59.000";

                StartDateTime = DateTime.Parse(sdate);
                EndDateTime = DateTime.Parse(ldate);
                customerid = Int32.Parse(lblCustomerID.Text);
                voucherno = txtVoucherNo.Text;
                if (cboPaymentType.Text != "Select Payment Type")
                {
                    paymenttype = cboPaymentType.Text;
                }
                //userid =  Int32.Parse(cboUser.SelectedValue.ToString());
                //locationid = Int32.Parse(cboLocation.SelectedValue.ToString());
                List<BOLSale> bolSaleList = new List<BOLSale>();
                bolSaleList = dalsale.GetSale(StartDateTime, EndDateTime, voucherno, customerid, classid, catid, itemcode, userid, locationid, paymenttype);
                dgvSaleList.Rows.Clear();
                decimal totalamt = 0;
                if (bolSaleList.Count > 0)
                {
                    for (int i = 0; i < bolSaleList.Count; i++)
                    {
                        dgvSaleList.Rows.Add(bolSaleList[i].SaleID, bolSaleList[i].SaleDate.ToString(), bolSaleList[i].UserID, bolSaleList[i].CustomerName, bolSaleList[i].VoucherNo, bolSaleList[i].PaymentType, bolSaleList[i].Currency, bolSaleList[i].TotalAmt, bolSaleList[i].Advance, bolSaleList[i].Discount, bolSaleList[i].GrandTotal);
                        totalamt += bolSaleList[i].TotalAmt;
                    }
                    txtTotalAmt.Text = totalamt.ToString();
                }
                LoadCategory();
                LoadClass();
                LoadLocation();
                LoadUser();
                cboPaymentType.SelectedIndex = 0;
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
                LstLocation = dalLocation.SelectAllLocation();

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

        private void LoadUser()
        {
            try
            {
                List<BOLUser> LstUser = new List<BOLUser>();
                LstUser = daluser.SelectAllUser();

                BOLUser bolUser = new BOLUser();
                bolUser.UserID = 0;
                bolUser.UserName = "<Select a User>";
                LstUser.Insert(0, bolUser);
                cboUser.DisplayMember = "UserName";
                cboUser.ValueMember = "UserID";
                cboUser.DataSource = LstUser;
                if (LstUser.Count > 0)
                {
                    cboUser.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvSaleList_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dgvSaleDetail.Rows.Clear();

                if (dgvSaleList.CurrentRow != null)
                {
                    long saleID = Convert.ToInt64(dgvSaleList.CurrentRow.Cells["colSaleID"].Value.ToString());
                    List<BOLSale> lstsaledetail = new List<BOLSale>();
                    lstsaledetail = dalsale.GetSaleDetailList(saleID,0);
                    dgvSaleDetail.Rows.Clear();

                    if (lstsaledetail.Count > 0)
                    {
                        for (int i = 0; i < lstsaledetail.Count; i++)
                        {
                            dgvSaleDetail.Rows.Add();
                            dgvSaleDetail.Rows[i].Cells[0].Value=lstsaledetail[i].ItemCode.ToString();
                            dgvSaleDetail.Rows[i].Cells[1].Value=lstsaledetail[i].Description.ToString();
                            dgvSaleDetail.Rows[i].Cells[2].Value=lstsaledetail[i].Mtype.ToString();
                            dgvSaleDetail.Rows[i].Cells[3].Value=lstsaledetail[i].Qty.ToString(); 
                            dgvSaleDetail.Rows[i].Cells[4].Value=lstsaledetail[i].SalePrice.ToString();
                            dgvSaleDetail.Rows[i].Cells[5].Value = lstsaledetail[i].Charge.ToString();
                            dgvSaleDetail.Rows[i].Cells[6].Value = lstsaledetail[i].Total.ToString();
                            dgvSaleDetail.Rows[i].Cells[7].Value=lstsaledetail[i].FOC.ToString();
                            dgvSaleDetail.Rows[i].Cells[8].Value = lstsaledetail[i].ItemDiscountPercent.ToString();
                            dgvSaleDetail.Rows[i].Cells[9].Value = lstsaledetail[i].ItemDiscount.ToString();
                            
                        }
                    }
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                getData();              

                List<BOLSale> bolSaleList = new List<BOLSale>();
                bolSaleList = dalsale.GetSale(StartDateTime, EndDateTime, voucherno, customerid, classid, catid, itemcode, userid, locationid, paymenttype);
                dgvSaleList.Rows.Clear();
                dgvSaleDetail.Rows.Clear();
                decimal totalamt = 0;
                if (bolSaleList.Count > 0)
                {
                    for (int i = 0; i < bolSaleList.Count; i++)
                    {
                        dgvSaleList.Rows.Add(bolSaleList[i].SaleID, bolSaleList[i].SaleDate.ToString(), bolSaleList[i].UserID, bolSaleList[i].CustomerName, bolSaleList[i].VoucherNo, bolSaleList[i].PaymentType, bolSaleList[i].Currency, bolSaleList[i].TotalAmt, bolSaleList[i].Advance, bolSaleList[i].Discount, bolSaleList[i].GrandTotal);
                        totalamt += bolSaleList[i].TotalAmt;
                    }
                    txtTotalAmt.Text = totalamt.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void frmSaleHistory_Click(object sender, EventArgs e)
        {
            try
            {
                dgvCustomer.Visible = false;
                dgvItemCode.Visible = false;
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
                    bool chkPaymentType = false;
                    chkPaymentType = dalcustomer.ChkPaymentType(Int32.Parse(lblCustomerID.Text));                    
                    dgvCustomer.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dgvSaleList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 11)
                {
                    if (e.RowIndex >= 0)
                    {
                        long saleid = long.Parse(dgvSaleList.Rows[e.RowIndex].Cells[0].Value.ToString());
                        int chkTrans = 0;
                        chkTrans = dalsale.ChkTransactionForSaleEditDelete(dgvSaleList.Rows[e.RowIndex].Cells["colVoucherNo"].Value.ToString());
                        if (chkTrans == 0)
                        {

                            DateTime dt = new DateTime();
                            dt = Convert.ToDateTime(dgvSaleList.Rows[e.RowIndex].Cells[1].Value.ToString());
                            DateTime checkdate = new DateTime();
                            checkdate = DateTime.Now.Date.AddDays(dalsale.GetEditEnableDate());
                            if (checkdate < dt)
                            {
                                //frmSale sale = new frmSale(saleid);
                                frmPOS sale = new frmPOS(saleid);
                                sale.ShowDialog();
                                btnSearch_Click(sender, e);
                            }
                            else
                            {
                                MessageBox.Show(" This data cant edit because sale date is over 2 days ago.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("This VoucherNo has transactions and cannot edit.");
                            return;
                        }
                    }
                }
                if (e.ColumnIndex == 12)
                {
                    long saleid = long.Parse(dgvSaleList.Rows[e.RowIndex].Cells[0].Value.ToString());
                     int chkTrans = 0;
                        chkTrans = dalsale.ChkTransactionForSaleEditDelete(dgvSaleList.Rows[e.RowIndex].Cells["colVoucherNo"].Value.ToString());
                        if (chkTrans == 0)
                        {
                            if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                dalsale.DeleteSale(saleid);
                                btnSearch_Click(sender, e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("This Voucher No has transactions and cannot delete.");
                            return;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }       

        private void btnSaleDetailReport_Click(object sender, EventArgs e)
        {
            try
            {
                getData();

                DataSet ds = new DataSet();
                ds = dalsale.GetSaleDetailReport(StartDateTime, EndDateTime, voucherno, customerid, classid, catid, itemcode, paymenttype);//dtpFromDate.Value, dtpToDate.Value

                if (ds.Tables.Count > 0)
                {                    
                    this.UseWaitCursor = true;
                    ReportDocument l_Report = new ReportDocument();

                    ds.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_Sale.xsd");
                    l_Report.Load(Application.StartupPath + @"\Report\RptSaleDetail.rpt");

                    string ToDate = dtpToDate.Value.ToString("dd-MM-yyyy");
                    string FromDate = dtpFromDate.Value.ToString("dd-MM-yyyy");

                    l_Report.DataDefinition.FormulaFields[9].Text = "#" + dtpFromDate.Value.ToString("dd-MM-yyyy") + "#";
                    l_Report.DataDefinition.FormulaFields[8].Text = "#" + dtpToDate.Value.ToString("dd-MM-yyyy") + "#";
                    
                    l_Report.SetDataSource(ds.Tables[0]);
                    l_Report.SetDatabaseLogon("sa", "moeyan");

                    List<BOLSystem> lstsystem = new List<BOLSystem>();
                    lstsystem = dalSystem.ShowAllSystem();

                    //DataTable dtt = new DataTable();
                    //DataColumn dc = new DataColumn();
                    //dc.ColumnName = "Companyname";
                    //dc.DataType = System.Type.GetType("System.String");
                    //dtt.Columns.Add(dc);
                    //if (lstsystem.Count > 0)
                    //{
                    //    for (int i = 0; i < lstsystem.Count; i++)
                    //    {
                    //        DataRow dr = dtt.NewRow();
                    //        dr["Companyname"] = lstsystem[0].Companyname;
                    //        dtt.Rows.Add(dr);
                    //    }

                    //    l_Report.Subreports[0].SetDataSource(dtt);

                    //}

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

        private void btnSaleReport_Click(object sender, EventArgs e)
        {

            try
            {
                getData();
                DataSet ds = new DataSet();
                ds = dalsale.SelectSaleVoucherHistory(StartDateTime, EndDateTime, voucherno, customerid, classid, catid, itemcode, userid, locationid, paymenttype);//dtpFromDate.Value, dtpToDate.Value
                string fromdate = dtpFromDate.Value.ToString("dd-MMM-yyyy");
                string todate = dtpToDate.Value.ToString("dd-MMM-yyyy");

                if (ds.Tables.Count > 0)
                {
                    this.UseWaitCursor = true;
                    ReportDocument l_Report = new ReportDocument();

                    ds.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_Sale.xsd");
                    l_Report.Load(Application.StartupPath + @"\Report\RptSaleReport.rpt");

                    l_Report.DataDefinition.FormulaFields[11].Text = "#" + fromdate+ "#";

                    //l_Report.DataDefinition.FormulaFields[0].Text=F(fromdate, "\Date(yyyy,mm,dd)");
                    l_Report.DataDefinition.FormulaFields[12].Text = "#" + todate+ "#";

                    l_Report.SetDataSource(ds);
                    l_Report.SetDatabaseLogon("sa", "moeyanserver");

                    List<BOLSystem> lstsystem = new List<BOLSystem>();
                    lstsystem = dalSystem.ShowAllSystem();

                    DataTable dtt = new DataTable();
                    DataColumn dc = new DataColumn();
                    dc.ColumnName = "Companyname";
                    dc.DataType = System.Type.GetType("System.String");
                    dtt.Columns.Add(dc);
                    if (lstsystem.Count > 0)
                    {
                        for (int i = 0; i < lstsystem.Count; i++)
                        {
                            DataRow dr = dtt.NewRow();
                            dr["Companyname"] = lstsystem[0].Companyname;
                            dtt.Rows.Add(dr);
                        }

                        l_Report.Subreports[0].SetDataSource(dtt);
                        
                    }


                    frmReport frmReport = new frmReport();
                    frmReport.rptViewer.ReportSource = l_Report;                    
                    frmReport.rptViewer.RefreshReport();
                    frmReport.Show();
                    this.UseWaitCursor = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void getData()
        {
            try
            {
                txtTotalAmt.Text = "";
                string sm = dtpFromDate.Value.Month.ToString().Length > 1 ? dtpFromDate.Value.Month.ToString() : "0" + dtpFromDate.Value.Month.ToString();
                string sd = dtpFromDate.Value.Day.ToString().Length > 1 ? dtpFromDate.Value.Day.ToString() : "0" + dtpFromDate.Value.Day.ToString();
                string lm = dtpToDate.Value.Month.ToString().Length > 1 ? dtpToDate.Value.Month.ToString() : "0" + dtpToDate.Value.Month.ToString();
                string ld = dtpToDate.Value.Day.ToString().Length > 1 ? dtpToDate.Value.Day.ToString() : "0" + dtpToDate.Value.Day.ToString();

                string sdate = dtpFromDate.Value.Year.ToString() + "-" + sm + "-" + sd + " 00:00:00.000";
                string ldate = dtpToDate.Value.Year.ToString() + "-" + lm + "-" + ld + " 23:59:59.000";

                customerid = long.Parse(lblCustomerID.Text);
                voucherno = txtVoucherNo.Text;
                StartDateTime = DateTime.Parse(sdate);
                EndDateTime = DateTime.Parse(ldate);
                classid = Int32.Parse(cboClass.SelectedValue.ToString());
                catid = Int32.Parse(cboCategory.SelectedValue.ToString());
                itemcode = txtStock.Text;
                userid = Int32.Parse(cboUser.SelectedValue.ToString());
                locationid = Int32.Parse(cboLocation.SelectedValue.ToString());
                if (cboPaymentType.Text != "Select Payment Type")
                {
                    paymenttype = cboPaymentType.Text;
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void picClose1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    List<BOLCustomer> lstcustomer = new List<BOLCustomer>();
                    lstcustomer = dalcustomer.GetCustomer(txtCustomer.Text, "Cash", 0);
                    dgvCustomer.Rows.Clear();
                    foreach (BOLCustomer c in lstcustomer)
                    {
                        dgvCustomer.Rows.Add(c.ID,c.CustomerID, c.MyanmarName, c.Address);
                    }
                    dgvCustomer.Visible = true;
                    dgvCustomer.Focus();
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
                            dgvItemCode.Rows.Add(lstStk[i].Id, lstStk[i].ItemCode, lstStk[i].NameMM, lstStk[i].NameMM, lstStk[i].Price);
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
                txtTotalAmt.Text = "";
                lblCustomerID.Text="0";
                txtVoucherNo.Text=" ";
                cboClass.SelectedIndex=0;
                cboCategory.SelectedIndex = 0;
                txtStock.Text="";
                cboUser.SelectedIndex = 0;
                cboLocation.SelectedIndex = 0;
                txtCustomer.Text = "";
                cboPaymentType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvSaleList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}
