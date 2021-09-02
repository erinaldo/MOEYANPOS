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
    public partial class frmSaleOrderHistory : Form
    {
        DALVoucherSetting dalVoucher = new DALVoucherSetting();
        DALSaleOrder dalsaleOrder = new DALSaleOrder();
        DALStock dalstock = new DALStock();
        DALCustomer dalcustomer = new DALCustomer();
        DALSystem dalSystem = new DALSystem();
        DALClass dalclass = new DALClass();
        DALCategory dalcategory = new DALCategory();
        DateTime StartDateTime;
        DateTime EndDateTime;
        long customerid = 0; 
        int userid = 0;
        int categoryid = 0;
        string voucherno = "";
        int classid = 0; int catid = 0; string itemcode = "";
        int locationid = 0;

        public frmSaleOrderHistory()
        {
            InitializeComponent();
            LoadCategory();
            LoadClass();
            LoadUser();
        }
        private void LoadClass()
        {
            try
            {
                List<BOLClass> lstclass = new List<BOLClass>();
                lstclass = dalsaleOrder.SelectAllClass();
                BOLClass bolclass = new BOLClass();
                bolclass.Id = 0;
                bolclass.ClassName = "";
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
        private void LoadUser()
        {            
            try
            {
                List<BOLUser> lstuser = new List<BOLUser>();
                lstuser = dalsaleOrder.SelectAllUser();
                BOLUser boluser = new BOLUser();
                boluser.UserID = 0;
                boluser.UserName = "";
                boluser.UserName = "<Select a User>";
                lstuser.Insert(0, boluser);
                cbouser.ValueMember = "UserID";
                cbouser.DisplayMember = "UserName";
                cbouser.DataSource = lstuser;
                if (lstuser.Count > 0)
                {
                    cbouser.SelectedIndex = 0;
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
                lstcategory = dalsaleOrder.SelectAllCategory();
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
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                getData();

                List<BOLSaleOrder> bolsaleorderlist = new List<BOLSaleOrder>();
                bolsaleorderlist = dalsaleOrder.GetSaleOrder(StartDateTime, EndDateTime, voucherno, customerid, userid, classid, catid, itemcode,locationid);

                dgvSaleOrderList.Rows.Clear();
                dgvSaleOrderDetail.Rows.Clear();

                if (bolsaleorderlist.Count > 0)
                {
                    for (int i = 0; i < bolsaleorderlist.Count; i++)
                    {
                        dgvSaleOrderList.Rows.Add( bolsaleorderlist[i].Saleorderid,bolsaleorderlist[i].Locationid,bolsaleorderlist[i].Username, bolsaleorderlist[i].Customerid, bolsaleorderlist[i].Voucherno, bolsaleorderlist[i].Totalamt);
                    }
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
                ds = dalsaleOrder.GetSaleOrderDetailReport(dtpFromDate.Value, dtpToDate.Value, voucherno, customerid,classid,catid,itemcode);

                if (ds.Tables.Count > 0)
                {
                    this.UseWaitCursor = true;
                    ReportDocument l_Report = new ReportDocument();

                    ds.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_SaleOrderReport.xsd");
                    l_Report.Load(Application.StartupPath + @"\Report\RptSaleOrderDetailReport.rpt");

                    l_Report.SetDataSource(ds);
                    l_Report.SetDatabaseLogon("sa", "moeyanserver");

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
                    }
                    else
                    {
                        MessageBox.Show("Please put printer name at System Setting.");
                        return;
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

        private void btnSaleDetailReport_Click(object sender, EventArgs e)
        {
            try
            {
                txtCustomer.Text = "";
                lblCustomerID.Text = "0";
                txtVoucherNo.Text = "";
                txtStock.Text = "";
                cboCategory.SelectedIndex = 0;
                cboClass.SelectedIndex = 0;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmSaleOrderHistory_Load(object sender, EventArgs e)
        {
            try
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
                    userid = Int32.Parse(cbouser.SelectedValue.ToString());
                    classid = Int32.Parse(cboClass.SelectedValue.ToString());
                    categoryid = Int32.Parse(cboCategory.SelectedValue.ToString());
                    voucherno = txtVoucherNo.Text;
                    List<BOLSaleOrder> bolSaleOrderList = new List<BOLSaleOrder>();
                    bolSaleOrderList = dalsaleOrder.GetSaleOrder(StartDateTime, EndDateTime, voucherno, customerid,userid, classid, catid, itemcode,locationid);
                    dgvSaleOrderList.Rows.Clear();

                    if (bolSaleOrderList.Count > 0)
                    {
                        for (int i = 0; i < bolSaleOrderList.Count; i++)
                        {
                            dgvSaleOrderList.Rows.Add(bolSaleOrderList[i].Saleorderid,bolSaleOrderList[i].Locationid, bolSaleOrderList[i].Username, bolSaleOrderList[i].Customername, bolSaleOrderList[i].Voucherno, bolSaleOrderList[i].Totalamt);
                        }
                    }

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

       

       

        private void dgvSaleOrderList_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dgvSaleOrderDetail.Rows.Clear();

                if (dgvSaleOrderList.CurrentRow != null)
                {
                    int saleorderID = Convert.ToInt32(dgvSaleOrderList.CurrentRow.Cells["SaleOrderID"].Value);
                    List<BOLSaleOrder> lstsaleorderdetail = new List<BOLSaleOrder>();
                    lstsaleorderdetail = dalsaleOrder.GetSaleOrderDetailList(saleorderID, 0);
                    dgvSaleOrderDetail.Rows.Clear();

                    if (lstsaleorderdetail.Count > 0)
                    {
                        for (int i = 0; i < lstsaleorderdetail.Count; i++)
                        {
                            dgvSaleOrderDetail.Rows.Add();
                            dgvSaleOrderDetail.Rows[i].Cells[0].Value = lstsaleorderdetail[i].Itemcode.ToString();
                            dgvSaleOrderDetail.Rows[i].Cells[1].Value = lstsaleorderdetail[i].Description.ToString();
                            dgvSaleOrderDetail.Rows[i].Cells[2].Value = lstsaleorderdetail[i].Type.ToString();
                            dgvSaleOrderDetail.Rows[i].Cells[3].Value = lstsaleorderdetail[i].Qty.ToString();
                            dgvSaleOrderDetail.Rows[i].Cells[4].Value = lstsaleorderdetail[i].Saleprice.ToString();
                            dgvSaleOrderDetail.Rows[i].Cells[5].Value = lstsaleorderdetail[i].Total.ToString();                           

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

        private void frmSaleOrderHistory_Click(object sender, EventArgs e)
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

        private void dgvSaleOrderList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 6)
                {
                    if (e.RowIndex >= 0)
                    {
                        long saleid = long.Parse(dgvSaleOrderList.Rows[e.RowIndex].Cells[0].Value.ToString());
                        frmSaleOrder sale = new frmSaleOrder(saleid);
                        sale.ShowDialog();
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
                            id = Int32.Parse(dgvSaleOrderList.Rows[e.RowIndex].Cells[0].Value.ToString());
                            int isdelete = 0;
                            isdelete = dalsaleOrder.DeleteSaleOrder(id);
                            if (isdelete >0)
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

        private void getData()
        {
            try
            {
                string sm = dtpFromDate.Value.Month.ToString().Length > 1 ? dtpFromDate.Value.Month.ToString() : "0" + dtpFromDate.Value.Month.ToString();
                string sd = dtpFromDate.Value.Day.ToString().Length > 1 ? dtpFromDate.Value.Day.ToString() : "0" + dtpFromDate.Value.Day.ToString();
                string lm = dtpToDate.Value.Month.ToString().Length > 1 ? dtpToDate.Value.Month.ToString() : "0" + dtpToDate.Value.Month.ToString();
                string ld = dtpToDate.Value.Day.ToString().Length > 1 ? dtpToDate.Value.Day.ToString() : "0" + dtpToDate.Value.Day.ToString();

                string sdate = dtpFromDate.Value.Year.ToString() + "-" + sm + "-" + sd + " 00:00:00.000";
                string ldate = dtpToDate.Value.Year.ToString() + "-" + lm + "-" + ld + " 23:59:59.000";

                customerid = Int32.Parse(lblCustomerID.Text);
                voucherno = txtVoucherNo.Text;
                StartDateTime = DateTime.Parse(sdate);
                EndDateTime = DateTime.Parse(ldate);
                classid = Int32.Parse(cboClass.SelectedValue.ToString());
                catid = Int32.Parse(cboCategory.SelectedValue.ToString());
                itemcode = txtStock.Text;
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

        private void btnClose_Click(object sender, EventArgs e)
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

        private void label9_Click(object sender, EventArgs e)
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
                    lstcustomer = dalcustomer.GetCustomer(txtCustomer.Text,"Cash",0);
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dgvSaleOrderList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvItemCode_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
