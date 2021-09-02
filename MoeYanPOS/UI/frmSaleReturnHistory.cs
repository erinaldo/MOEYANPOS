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
    public partial class frmSaleReturnHistory : Form
    {
        DALSaleReturn dalsalereturn = new DALSaleReturn();
        DALCustomer dalcustomer = new DALCustomer();
        DALStock dalstock = new DALStock();
        DALLocation dallocation = new DALLocation();
        DALSystem dalsystem=new DALSystem();
        DALVoucherSetting dalVoucher = new DALVoucherSetting();
        DateTime StartDateTime;
        DateTime EndDateTime;
        int customerid = 0;
        string voucherno = "";
        string itemcode = "";
        int userid = 0;
        int locationid = 0;
        int categoryid = 0;
        int classid = 0;
        public frmSaleReturnHistory()
        {
            InitializeComponent();
            LoadCategory();
            LoadUser();
            LoadClass();
            LoadLocation();
        }
        public frmSaleReturnHistory(string ItemCode)
        {
            try
            {
                InitializeComponent();
                LoadCategory();
                LoadUser();
                LoadClass();
                LoadLocation();
                txtitem.Text = ItemCode;
                btnsearch.Focus();
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
                lstclass = dalsalereturn.SelectAllClass();
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
                lstuser = dalsalereturn.SelectAllUser();
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
                lstcategory = dalsalereturn.SelectAllCategory();
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
                List<BolLocation> lstlocation = new List<BolLocation>();
                lstlocation = dallocation.GetAllLocation();

                BolLocation bollocation = new BolLocation();
                bollocation.ID = 0;
                bollocation.Location = "";
                bollocation.Location = "<Select a Location>";
                lstlocation.Insert(0, bollocation);
                cbolocation.ValueMember = "ID";
                cbolocation.DisplayMember = "Location";
                cbolocation.DataSource = lstlocation;

                if (lstlocation.Count > 0)
                {
                    cbolocation.SelectedIndex = 0;
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
                string sm = dtpfrom.Value.Month.ToString().Length > 1 ? dtpfrom.Value.Month.ToString() : "0" + dtpfrom.Value.Month.ToString();
                string sd = dtpfrom.Value.Day.ToString().Length > 1 ? dtpfrom.Value.Day.ToString() : "0" + dtpfrom.Value.Day.ToString();
                string lm = dtpto.Value.Month.ToString().Length > 1 ? dtpto.Value.Month.ToString() : "0" + dtpto.Value.Month.ToString();
                string ld = dtpto.Value.Day.ToString().Length > 1 ? dtpto.Value.Day.ToString() : "0" + dtpto.Value.Day.ToString();

                string sdate = dtpfrom.Value.Year.ToString() + "-" + sm + "-" + sd + " 00:00:00.000";
                string ldate = dtpto.Value.Year.ToString() + "-" + lm + "-" + ld + " 23:59:59.000";

                customerid = Int32.Parse(lblcustomerid.Text);
                voucherno = txtvoucher.Text;
                itemcode = txtitem.Text;
                StartDateTime = DateTime.Parse(sdate);
                EndDateTime = DateTime.Parse(ldate);
                userid = Int32.Parse(cbouser.SelectedValue.ToString());
                categoryid = Int32.Parse(cboCategory.SelectedValue.ToString());
                classid = Int32.Parse(cboClass.SelectedValue.ToString());
                locationid = Int32.Parse(cbolocation.SelectedValue.ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtcustomer_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dgvcustomer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    lblcustomerid.Text = dgvcustomer.CurrentRow.Cells[0].Value.ToString();
                    txtcustomer.Text = dgvcustomer.CurrentRow.Cells[1].Value.ToString();
                    dgvcustomer.Visible = false;
                    txtcustomer.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtitem_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void dgvitem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //lblsupplierid.Text = dgvsupplier.CurrentRow.Cells[0].Value.ToString();
                    txtitem.Text = dgvitem.CurrentRow.Cells[1].Value.ToString();
                    dgvitem.Visible = false;
                    txtitem.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                getData();

                List<BOLSaleReturn> bolsalereturnlist = new List<BOLSaleReturn>();
                bolsalereturnlist = dalsalereturn.GetSaleReturn(StartDateTime, EndDateTime, voucherno,  customerid,userid,classid,categoryid,itemcode,locationid);

                dgvsaleReturn.Rows.Clear();
                dgvsalereturndetail.Rows.Clear();

                if (bolsalereturnlist.Count > 0)
                {
                    for (int i = 0; i < bolsalereturnlist.Count; i++)
                    {
                        dgvsaleReturn.Rows.Add(bolsalereturnlist[i].Salereturnid, bolsalereturnlist[i].Date, bolsalereturnlist[i].Userid, bolsalereturnlist[i].Customerid, bolsalereturnlist[i].Customername, bolsalereturnlist[i].Systemvoucherno, bolsalereturnlist[i].Paymenttype, bolsalereturnlist[i].Currencyid, bolsalereturnlist[i].Totalamt, bolsalereturnlist[i].Location);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvsaleReturn_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dgvsalereturndetail.Rows.Clear();

                if (dgvsaleReturn.CurrentRow != null)
                {
                    int salereturnid = Convert.ToInt32(dgvsaleReturn.CurrentRow.Cells[0].Value.ToString());
                    string itemcode = txtitem.Text;
                    List<BOLSaleReturn> lstsalereturndetaillist = new List<BOLSaleReturn>();
                    lstsalereturndetaillist = dalsalereturn.GetSaleReturnDetailList(salereturnid,  0);
                    dgvsalereturndetail.Rows.Clear();

                    if (lstsalereturndetaillist.Count > 0)
                    {
                        for (int i = 0; i < lstsalereturndetaillist.Count; i++)
                        {
                            dgvsalereturndetail.Rows.Add();
                            dgvsalereturndetail.Rows[i].Cells[0].Value = lstsalereturndetaillist[i].Itemcode.ToString();
                            dgvsalereturndetail.Rows[i].Cells[1].Value = lstsalereturndetaillist[i].Description.ToString();
                            dgvsalereturndetail.Rows[i].Cells[2].Value = lstsalereturndetaillist[i].Type.ToString();
                            dgvsalereturndetail.Rows[i].Cells[3].Value = lstsalereturndetaillist[i].Qty.ToString();
                            dgvsalereturndetail.Rows[i].Cells[4].Value = lstsalereturndetaillist[i].Saleprice.ToString();
                            dgvsalereturndetail.Rows[i].Cells[5].Value = lstsalereturndetaillist[i].Total.ToString();
                            

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvsaleReturn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 10)
                {
                    if (e.RowIndex >= 0)
                    {
                        long salereturnid = long.Parse(dgvsaleReturn.Rows[e.RowIndex].Cells[0].Value.ToString());
                        string itemcode = dgvsaleReturn.Rows[e.RowIndex].Cells[7].Value.ToString();
                        frmSaleReturn salereturn = new frmSaleReturn(salereturnid, itemcode);
                        salereturn.ShowDialog();
                        btnsearch_Click(sender, e);
                    }
                }
                if (e.ColumnIndex == 11)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int salereturnid = 0;
                            salereturnid = Int32.Parse(dgvsaleReturn.Rows[e.RowIndex].Cells[0].Value.ToString());
                            int isdelete = 0;
                            isdelete = dalsalereturn.DeleteSaleReturn(salereturnid);
                            if (isdelete >0)
                            {
                                MessageBox.Show("Successfully deleted!");
                                dgvsaleReturn.Rows.Remove(dgvsaleReturn.CurrentRow);
                                //dgvpurchase.Rows.Clear();
                                dgvsalereturndetail.Rows.Clear();
                                //PurchaseHistoryLoad();
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

        private void dgvsaleReturn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmSaleReturnHistory_Load(object sender, EventArgs e)
        {
            try
            {
                int supplierid = 0;
                string voucherno = "";
                string sm = dtpfrom.Value.Month.ToString().Length > 1 ? dtpfrom.Value.Month.ToString() : "0" + dtpfrom.Value.Month.ToString();
                string sd = dtpfrom.Value.Day.ToString().Length > 1 ? dtpfrom.Value.Day.ToString() : "0" + dtpfrom.Value.Day.ToString();
                string lm = dtpto.Value.Month.ToString().Length > 1 ? dtpto.Value.Month.ToString() : "0" + dtpto.Value.Month.ToString();
                string ld = dtpto.Value.Day.ToString().Length > 1 ? dtpto.Value.Day.ToString() : "0" + dtpto.Value.Day.ToString();

                string sdate = dtpfrom.Value.Year.ToString() + "-" + sm + "-" + sd + " 00:00:00.000";
                string ldate = dtpto.Value.Year.ToString() + "-" + lm + "-" + ld + " 23:59:59.000";

                StartDateTime = DateTime.Parse(sdate);
                EndDateTime = DateTime.Parse(ldate);
                customerid = Int32.Parse(lblcustomerid.Text);
                userid = Int32.Parse(cbouser.SelectedValue.ToString());
                classid = Int32.Parse(cboClass.SelectedValue.ToString());
                categoryid = Int32.Parse(cboCategory.SelectedValue.ToString());
                voucherno = txtvoucher.Text;
                List<BOLSaleReturn> bolsalereturnlist = new List<BOLSaleReturn>();
                bolsalereturnlist = dalsalereturn.GetSaleReturn(StartDateTime, EndDateTime, voucherno, customerid, userid, classid, categoryid, itemcode, locationid);
                dgvsaleReturn.Rows.Clear();

                if (bolsalereturnlist.Count > 0)
                {
                    for (int i = 0; i < bolsalereturnlist.Count; i++)
                    {
                        dgvsaleReturn.Rows.Add(bolsalereturnlist[i].Salereturnid, bolsalereturnlist[i].Username, bolsalereturnlist[i].Customerid, bolsalereturnlist[i].Customername, bolsalereturnlist[i].Systemvoucherno, bolsalereturnlist[i].Paymenttype, bolsalereturnlist[i].Currencyid, bolsalereturnlist[i].Totalamt, bolsalereturnlist[i].Location);
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
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

        private void btnreport_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    this.UseWaitCursor = true;
            //    ReportDocument l_Report = new ReportDocument();
            //    //l_Report.SetParameterValue("fromdate1", dtpfrom.Value);
            //    //l_Report.SetParameterValue("ToDate1", dtpfrom.Value);

            //    DataSet dts = new DataSet();
            //    dts = dalsalereturn.SaleReturnItemTotal();

            //    string fromdate = dtpfrom.Value.ToString("dd-MMM-yyyy");
            //    string todate = dtpto.Value.ToString("dd-MMM-yyyy");



            //    if (dts.Tables[0].Rows.Count > 0)
            //    {

            //        //dts.WriteXmlSchema(Application.StartupPath + @"\DataSets\SP_SaleReturnItemTotal.xsd");
            //        l_Report.Load(Application.StartupPath + @"\Report\RtpSaleItemReturn.rpt");

            //        //l_Report.DataDefinition.FormulaFields[3].Text = "#" + dtpfrom.Value.ToString("dd-MM-yyyy") + "#";
            //        //////l_Report.DataDefinition.FormulaFields[0].Text=F(fromdate, "\Date(yyyy,mm,dd)");
            //        //l_Report.DataDefinition.FormulaFields[4].Text = "#" + dtpto.Value.ToString("dd-MM-yyyy") + "#";

            //        l_Report.SetDataSource(dts);
            //        l_Report.SetDatabaseLogon("sa", "moeyan");

            //        frmReport frmReport = new frmReport();
            //        frmReport.rptViewer.ReportSource = l_Report;
            //        frmReport.rptViewer.Refresh();
            //        frmReport.Show();
            //        this.UseWaitCursor = false;
            //    }
            //    else
            //    {
            //        MessageBox.Show("No data for Preview");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}

            try
            {
                string sm = dtpfrom.Value.Month.ToString().Length > 1 ? dtpfrom.Value.Month.ToString() : "0" + dtpfrom.Value.Month.ToString();
                string sd = dtpfrom.Value.Day.ToString().Length > 1 ? dtpfrom.Value.Day.ToString() : "0" + dtpfrom.Value.Day.ToString();
                string lm = dtpto.Value.Month.ToString().Length > 1 ? dtpto.Value.Month.ToString() : "0" + dtpto.Value.Month.ToString();
                string ld = dtpto.Value.Day.ToString().Length > 1 ? dtpto.Value.Day.ToString() : "0" + dtpto.Value.Day.ToString();

                string sdate = dtpfrom.Value.Year.ToString() + "-" + sm + "-" + sd + " 00:00:00.000";
                string ldate = dtpto.Value.Year.ToString() + "-" + lm + "-" + ld + " 12:00:00.000";

                DataSet ds = new DataSet();
                ds = dalsalereturn.GetSaleReturnDetailReport(dtpfrom.Value, dtpto.Value, voucherno, customerid, classid, categoryid, locationid, itemcode, userid);

                if (ds.Tables[0].Rows.Count > 0)
                {                    
                        this.UseWaitCursor = true;
                        ReportDocument l_Report = new ReportDocument();

                        //ds.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_Purchase.xsd");
                        l_Report.Load(Application.StartupPath + @"\Report\RptSaleReturnDetail.rpt");

                        l_Report.SetDataSource(ds.Tables[0]);
                        l_Report.SetDatabaseLogon("sa", "moeyan");

                        List<BOLVoucherSetting> lstvoucherSetting = new List<BOLVoucherSetting>();
                        lstvoucherSetting = dalVoucher.SelectAllVoucher();

                        DataTable dtt = new DataTable();
                        DataColumn dc = new DataColumn();
                        dc.ColumnName = "Name";
                        dc.DataType = System.Type.GetType("System.String");
                        dtt.Columns.Add(dc);

                        //if (lstvoucherSetting.Count > 0)
                        //{
                        //    //for (int i = 0; i < lstvoucherSetting.Count; i++)
                        //    //{
                        //    //    DataRow dr = dtt.NewRow();
                        //    //    dr["Name"] = lstvoucherSetting[0].Name;
                        //    //    dtt.Rows.Add(dr);
                        //    //}

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

        private void txtitem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    List<BOLStock> lststk = new List<BOLStock>();
                    lststk = dalstock.SelectStock(txtitem.Text, 0, "", 0);
                    dgvitem.Rows.Clear();
                    if (lststk.Count > 0)
                    {
                        for (int i = 0; i < lststk.Count; i++)
                        {
                            dgvitem.Rows.Add(lststk[i].Id, lststk[i].ItemCode, lststk[i].NameEng, lststk[i].NameMM, lststk[i].Price);
                        }
                        dgvitem.Visible = true;
                        dgvitem.Focus();
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
            this.Close();
        }

        private void txtcustomer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    List<BOLCustomer> lstcustomer = new List<BOLCustomer>();
                    lstcustomer = dalcustomer.GetCustomer(txtcustomer.Text,"Cash",0);
                    dgvcustomer.Rows.Clear();

                    foreach (BOLCustomer s in lstcustomer)
                    {
                        dgvcustomer.Rows.Add(s.ID,s.CustomerID, s.MyanmarName, s.Address);
                    }

                    dgvcustomer.Visible = true;
                    dgvcustomer.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void picClose1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
