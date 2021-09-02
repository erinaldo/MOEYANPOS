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
    public partial class frmPurchaseHistory : Form
    {
        DALPurchase dalpurchase = new DALPurchase();
        DALSupplier dalsupplier = new DALSupplier();
        DALStock dalstock = new DALStock();
        DALLocation dallocation = new DALLocation();
        DateTime StartDateTime;
        DateTime EndDateTime;
        int supplierid = 0;
        string voucherno = "";
        string itemcode = "";
        int classid = 0;
        int categoryid = 0;
        int locationid = 0;
        int userid = 0;

        public frmPurchaseHistory()
        {
            InitializeComponent();
            LoadCategory();
            LoadClass();
            LoadUser();
            LoadLocation();
        }
        public frmPurchaseHistory(string Itemcode)
        {
            try
            {
                InitializeComponent();
                LoadCategory();
                LoadClass();
                LoadUser();
                LoadLocation();
                txtitem.Text = Itemcode;
                btnsearch.Focus();
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
        private void LoadClass()
        {
            try
            {
                List<BOLClass> lstclass = new List<BOLClass>();
                lstclass = dalpurchase.SelectAllClass();
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
                lstuser = dalpurchase.SelectAllUser();
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
                lstcategory = dalpurchase.SelectAllCategory();
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
        private void frmPurchaseHistory_Load(object sender, EventArgs e)
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
                supplierid = Int32.Parse(lblsupplierid.Text);
                userid = Int32.Parse(cbouser.SelectedValue.ToString());
                classid = Int32.Parse(cboClass.SelectedValue.ToString());
                categoryid = Int32.Parse(cboCategory.SelectedValue.ToString());
                voucherno = txtvoucher.Text;
                List<BOLPurchase> bolpurchaseList = new List<BOLPurchase>();
                bolpurchaseList = dalpurchase.GetPurchase(StartDateTime, EndDateTime, voucherno, supplierid, userid, classid, categoryid, itemcode,userid);
                dgvpurchase.Rows.Clear();

                if (bolpurchaseList.Count > 0)
                {
                    for (int i = 0; i < bolpurchaseList.Count; i++)
                    {
                        //dgvpurchase.Rows[i].Cells[0].Visible = false;
                        dgvpurchase.Rows.Add(bolpurchaseList[i].Purchaseid, bolpurchaseList[i].Username, bolpurchaseList[i].SupplierName, bolpurchaseList[i].Voucherno,bolpurchaseList[i].Paymenttype, bolpurchaseList[i].Currencyid,bolpurchaseList[i].Totalamt,bolpurchaseList[i].Advance,bolpurchaseList[i].Discount,bolpurchaseList[i].Grandtotal);
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void txtsupplier_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<BOLSupplier> lstsupplier = new List<BOLSupplier>();
                lstsupplier = dalpurchase.GetSupplier(txtsupplier.Text);
                dgvsupplier.Rows.Clear();

                foreach (BOLSupplier s in lstsupplier)
                {
                    dgvsupplier.Rows.Add(s.Supplierid, s.SupplierName, s.Address, s.Phone);
                }

                dgvsupplier.Visible = true;
                dgvsupplier.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvsupplier_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    lblsupplierid.Text = dgvsupplier.CurrentRow.Cells[0].Value.ToString();
                    txtsupplier.Text = dgvsupplier.CurrentRow.Cells[1].Value.ToString();
                    dgvsupplier.Visible = false;
                    txtsupplier.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtitem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<BOLStock> lststk = new List<BOLStock>();
                lststk = dalstock.SelectStock(txtitem.Text, 0, "",0);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }       

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

                if (txtsupplier.Text == "")
                {
                    supplierid = 0;
                }
                else
                {
                    supplierid = Int32.Parse(lblsupplierid.Text);
                }
                voucherno = txtvoucher.Text;                
                StartDateTime = DateTime.Parse(sdate);
                EndDateTime = DateTime.Parse(ldate);
                locationid = Int32.Parse(cbolocation.SelectedValue.ToString());
                classid = Int32.Parse(cboClass.SelectedValue.ToString());
                itemcode = txtitem.Text;
                categoryid = Int32.Parse(cboCategory.SelectedValue.ToString());
                userid = Int32.Parse(cbouser.SelectedValue.ToString());

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

                List<BOLPurchase> bolpurchaselist = new List<BOLPurchase>();
                
                bolpurchaselist = dalpurchase.GetPurchase(StartDateTime, EndDateTime, voucherno, supplierid, classid, categoryid, locationid, itemcode,userid);

                dgvpurchase.Rows.Clear();
                dgvpurchasedetail.Rows.Clear();

                if (bolpurchaselist.Count > 0)
                {
                    for (int i = 0; i < bolpurchaselist.Count; i++)
                    {
                        dgvpurchase.Rows.Add(bolpurchaselist[i].Purchaseid, bolpurchaselist[i].Username, bolpurchaselist[i].SupplierName, bolpurchaselist[i].Voucherno, bolpurchaselist[i].Paymenttype, bolpurchaselist[i].Currencyid, bolpurchaselist[i].Totalamt, bolpurchaselist[i].Advance, bolpurchaselist[i].Discount, bolpurchaselist[i].Grandtotal);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvpurchase_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dgvpurchasedetail.Rows.Clear();

                if (dgvpurchase.CurrentRow != null)
                {
                    int purchaseid = Convert.ToInt32(dgvpurchase.CurrentRow.Cells["PID"].Value);
                    string itemcode = txtitem.Text;
                    //remove by KSAung
                    //string itemcode = Convert.ToString(dgvpurchase.CurrentRow.Cells[""].Value);
                    List<BOLPurchase> lstpurchasedetaillist = new List<BOLPurchase>();
                    lstpurchasedetaillist = dalpurchase.GetPurchaseDetailList(purchaseid,itemcode,0);
                    dgvpurchasedetail.Rows.Clear();

                    if (lstpurchasedetaillist.Count > 0)
                    {
                        for (int i = 0; i < lstpurchasedetaillist.Count; i++)
                        {
                            dgvpurchasedetail.Rows.Add();
                            dgvpurchasedetail.Rows[i].Cells[0].Value = lstpurchasedetaillist[i].Itemcode.ToString();
                            dgvpurchasedetail.Rows[i].Cells[1].Value = lstpurchasedetaillist[i].Description.ToString();
                            dgvpurchasedetail.Rows[i].Cells[2].Value = lstpurchasedetaillist[i].Mtype.ToString();
                            dgvpurchasedetail.Rows[i].Cells[3].Value = lstpurchasedetaillist[i].Qty.ToString();
                            dgvpurchasedetail.Rows[i].Cells[4].Value = lstpurchasedetaillist[i].Purchaseprice.ToString();
                            dgvpurchasedetail.Rows[i].Cells[5].Value = lstpurchasedetaillist[i].Total.ToString();
                            dgvpurchasedetail.Rows[i].Cells[6].Value = lstpurchasedetaillist[i].Foc.ToString();
                            dgvpurchasedetail.Rows[i].Cells[7].Value = lstpurchasedetaillist[i].Itemdiscountpercent.ToString();
                            dgvpurchasedetail.Rows[i].Cells[8].Value = lstpurchasedetaillist[i].Itemdiscount.ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnPurchaseReport_Click(object sender, EventArgs e)
        {
            try
            {

                string sm = dtpfrom.Value.Month.ToString().Length > 1 ? dtpfrom.Value.Month.ToString() : "0" + dtpfrom.Value.Month.ToString();
                string sd = dtpfrom.Value.Day.ToString().Length > 1 ? dtpfrom.Value.Day.ToString() : "0" + dtpfrom.Value.Day.ToString();
                string lm = dtpto.Value.Month.ToString().Length > 1 ? dtpto.Value.Month.ToString() : "0" + dtpto.Value.Month.ToString();
                string ld = dtpto.Value.Day.ToString().Length > 1 ? dtpto.Value.Day.ToString() : "0" + dtpto.Value.Day.ToString();

                string sdate = dtpfrom.Value.Year.ToString() + "-" + sm + "-" + sd + " 00:00:00.000";
                string ldate = dtpto.Value.Year.ToString() + "-" + lm + "-" + ld + " 23:59:59.000";

                StartDateTime = DateTime.Parse(sdate);
                EndDateTime = DateTime.Parse(ldate);
                this.UseWaitCursor = true;
                ReportDocument l_Report = new ReportDocument();
                //l_Report.SetParameterValue("fromdate1", dtpfrom.Value);
                //l_Report.SetParameterValue("ToDate1", dtpfrom.Value);
               
                DataSet dts = new DataSet();
                dts = dalpurchase.PurchaseItemTotal(StartDateTime, EndDateTime);

                string fromdate = dtpfrom.Value.ToString("dd-MMM-yyyy");
                string todate = dtpto.Value.ToString("dd-MMM-yyyy");
                
               
               
                if (dts.Tables[0].Rows.Count > 0)
                {
                    
                    dts.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_Item.xsd");
                    l_Report.Load(Application.StartupPath + @"\Report\RptPurchaseItem.rpt");

                    l_Report.DataDefinition.FormulaFields[0].Text = "#" + dtpfrom.Value .ToString("dd-MM-yyyy") + "#";
                    //l_Report.DataDefinition.FormulaFields[0].Text=F(fromdate, "\Date(yyyy,mm,dd)");
                    l_Report.DataDefinition.FormulaFields[1].Text = "#" + dtpto.Value.ToString("dd-MM-yyyy") + "#";

                    l_Report.SetDataSource(dts);
                    l_Report.SetDatabaseLogon("sa", "moeyan");
                    
                    frmReport frmReport = new frmReport();
                    frmReport.rptViewer.ReportSource = l_Report;
                    frmReport.rptViewer.Refresh();
                    frmReport.Show();
                    this.UseWaitCursor = false;
                }
                else
                {
                    MessageBox.Show("No data for Preview");
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

        private void btndetailreport_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    List<BOLPurchase> lst = new List<BOLPurchase>();
            //    for (int i = 0; i < dgvpurchase.Rows.Count - 1; i++)
            //    {
            //        BOLPurchase bolpurchasepreview = new BOLPurchase();
            //        //bolpurchasepreview.Purchaseid = long.Parse(dgvpurchase.ro.Text);
            //        //bolpurchasepreview.SupplierName = txtsupplier.Text;
            //        //if (txtVoucherNo.Text == "")
            //        //{
            //        //    bolpurchasepreview.Voucherno = txtsystemvoucherno.Text;
            //        //}
            //        //else
            //        //{
            //        //    bolpurchasepreview.Voucherno = txtVoucherNo.Text;
            //        //}
            //        bolpurchasepreview.Voucherno = dgvpurchase.Rows[i].Cells[3].Value.ToString();
            //        bolpurchasepreview.Description = dgvpurchasedetail.Rows[i].Cells[2].Value.ToString();
            //        bolpurchasepreview.Total = Decimal.Parse(dgvpurchase.Rows[i].Cells[6].Value.ToString());
            //        bolpurchasepreview.Qty = Int32.Parse(dgvpurchasedetail.Rows[i].Cells[3].Value.ToString());
            //        bolpurchasepreview.Purchaseprice = Decimal.Parse(dgvpurchasedetail.Rows[i].Cells[4].Value.ToString());

            //        bolpurchasepreview.Discount = Decimal.Parse(dgvpurchasedetail.Rows[i].Cells[8].Value.ToString());
                    

            //        lst.Add(bolpurchasepreview);

            //    }
            //    if (lst.Count > 0)
            //    {
            //        this.UseWaitCursor = true;
            //        ReportDocument cu_Report = new ReportDocument();
            //        cu_Report.Load(Application.StartupPath + @"\Report\RptPurchaseDetail.rpt");
            //        cu_Report.SetDataSource(lst);
            //        cu_Report.SetDatabaseLogon("sa", "moeyan");

            //        frmReport frmReport = new frmReport();
            //        frmReport.rptViewer.ReportSource = cu_Report;
            //        frmReport.rptViewer.Refresh();
            //        frmReport.Show();
            //        this.UseWaitCursor = false;
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
                string ldate = dtpto.Value.Year.ToString() + "-" + lm + "-" + ld + " 23:59:59.000";

                StartDateTime = DateTime.Parse(sdate);
                EndDateTime = DateTime.Parse(ldate);
                DataSet ds = new DataSet();
                ds = dalpurchase.GetPurchaseDetailReport(StartDateTime, EndDateTime, voucherno, supplierid, classid, categoryid, locationid, itemcode, userid);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //for(int i=0;i<ds.Tables[0].Rows.Count;i++)
                    //{
                        this.UseWaitCursor = true;
                        ReportDocument l_Report = new ReportDocument();

                        ds.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_PurchaseDetailReport.xsd");
                        l_Report.Load(Application.StartupPath + @"\Report\RptPurchaseDetailReport.rpt");

                        string ToDate = dtpto.Value.ToString("dd-MM-yyyy");
                        string FromDate = dtpfrom.Value.ToString("dd-MM-yyyy");

                        l_Report.DataDefinition.FormulaFields[4].Text = "#" + dtpfrom.Value.ToString("dd-MM-yyyy") + "#";
                        l_Report.DataDefinition.FormulaFields[5].Text = "#" + dtpto.Value.ToString("dd-MM-yyyy") + "#";

                        l_Report.SetDataSource(ds.Tables[0]);
                        l_Report.SetDatabaseLogon("sa", "moeyan");


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

        private void frmPurchaseHistory_Click(object sender, EventArgs e)
        {
            try
            {
                dgvsupplier.Visible = false;
                dgvitem.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvpurchase_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 10)
                {
                    if (e.RowIndex >= 0)
                    {
                        long purchaseid = long.Parse(dgvpurchase.Rows[e.RowIndex].Cells[0].Value.ToString());
                        string itemcode = txtitem.Text;
                        frmPurchase purchase = new frmPurchase(purchaseid, itemcode);
                        purchase.ShowDialog();
                        btnsearch_Click(sender, e);
                    }
                }
                if (e.ColumnIndex == 11)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int purchaseid = 0;
                            purchaseid = Int32.Parse(dgvpurchase.Rows[e.RowIndex].Cells[0].Value.ToString());
                            int isdelete = 0;
                            isdelete = dalpurchase.DeletePurchase(purchaseid);
                            if (isdelete > 0)
                            {
                                MessageBox.Show("Successfully deleted!");
                                dgvpurchase.Rows.Remove(dgvpurchase.CurrentRow);
                                //dgvpurchase.Rows.Clear();
                                dgvpurchasedetail.Rows.Clear();
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

        private void dgvpurchase_Click(object sender, EventArgs e)
        {

        }

        private void picClose1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtsupplier_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.KeyCode == Keys.Enter)
                //{
                    List<BOLSupplier> lstsupplier = new List<BOLSupplier>();
                    lstsupplier = dalpurchase.GetSupplier(txtsupplier.Text);

                    //txtsupplier.Text = "Auto";

                    //if (lstsupplier.Count == 0 & txtsupplier.Text == "Auto")
                    //{
                    //    DALPurchase dalpur = new DALPurchase();
                    //    BOLSupplier bolsupplier = new BOLSupplier();
                    //    bolsupplier.SupplierName = "Auto";

                    //    dalsupplier.SaveSupplier(bolsupplier);

                    //    List<BOLSupplier> getlstsupplier = new List<BOLSupplier>();
                    //    getlstsupplier = dalsupplier.GetSupplier(txtsupplier.Text);

                    //    if (getlstsupplier.Count > 0)
                    //    {
                    //        lblsupplierid.Text = getlstsupplier[0].Supplierid.ToString();
                    //        txtsupplier.Text = getlstsupplier[0].SupplierName;
                    //    }
                    //    dgvsupplier.Focus();
                    //}
                    //else if (lstsupplier.Count > 0)
                    //{
                        dgvsupplier.Rows.Clear();
                        foreach (BOLSupplier s in lstsupplier)
                        {
                            dgvsupplier.Rows.Add(s.Supplierid, s.SupplierName, s.Address, s.Phone);

                            if (s.SupplierName == "Auto")
                            {
                                lblsupplierid.Text = s.Supplierid.ToString();
                                txtsupplier.Text = s.SupplierName;
                                dgvsupplier.Visible = false;
                            }
                            else
                            {
                                dgvsupplier.Visible = true;
                            }
                        }
                        bool isCrdite = false;
                        isCrdite = dalsupplier.ChkPaymentType(Int32.Parse(lblsupplierid.Text));
                        //if (isCrdite)
                        //{
                        //    cbopaymentby.Enabled = true;
                        //}
                        //else
                        //{
                        //    cbopaymentby.Enabled = false;
                        //}
                        dgvsupplier.Focus();
                    }
                //}

            //}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvpurchase_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
