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
    public partial class frmStockTransfer : Form
    {
        bool IsTvClick = false;
        string VoucherNo; string todaydate;
        DALStock dalstock = new DALStock();
        DALClass dalclass = new DALClass();
        DALCategory dalcategory = new DALCategory();
        DALBrand dalbrand = new DALBrand();
        DALMeasurement dalmeasurement = new DALMeasurement();
        DALStockTransfer dalStockTransfer = new DALStockTransfer();
        DALStockTransferDetail daladjustmentDetail = new DALStockTransferDetail();
        DALLocation dalLocation = new DALLocation();
        DALSystem dalSystem = new DALSystem();
        DALVoucherSetting dalVoucher = new DALVoucherSetting();
        List<Int32> STDetailID = new List<Int32>();

        public frmStockTransfer()
        {
            try
            {
                InitializeComponent();
                LoadStock();
                LoadLocation();
                LoadToLocation();
                txtVoucherNo.Focus();
                txtVoucherNo.Enabled = true;
                todaydate = DateTime.Now.ToString("yyMMdd");
                txtVoucherNo.Text = "WT" + todaydate + dalStockTransfer.GetStockTransferMaxID().ToString();
                VoucherNo = txtVoucherNo.Text;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        public frmStockTransfer(long id)
        {
            try
            {
                InitializeComponent();
                LoadStock();
                LoadLocation();
                LoadToLocation();

                BOLStockTransfer bolStockTransfer = new BOLStockTransfer();
                bolStockTransfer = dalStockTransfer.GetStockTransfer(id);

                if (bolStockTransfer.ID != 0)
                {
                    dtpAdjDate.Value = bolStockTransfer.Date;
                    lblID.Text = bolStockTransfer.ID.ToString();
                    cboFromLocation.SelectedValue = bolStockTransfer.LID;
                    cboToLocation.SelectedValue = bolStockTransfer.ToLID;
                    txtVoucherNo.Text = bolStockTransfer.VoucherNo;
                    txtTimes.Text = bolStockTransfer.Times.ToString();
                    txtVoucherNo.Enabled = false;
                    DataSet ds = new DataSet();
                    ds = daladjustmentDetail.GetStockTransfer(id);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            dgvStockTransferDetail.Rows.Add(ds.Tables[0].Rows[i].ItemArray[1].ToString(), ds.Tables[0].Rows[i].ItemArray[2].ToString(), ds.Tables[0].Rows[i].ItemArray[3].ToString(), ds.Tables[0].Rows[i].ItemArray[4].ToString(), ds.Tables[0].Rows[i].ItemArray[0].ToString(), ds.Tables[0].Rows[i].ItemArray[5].ToString(), ds.Tables[0].Rows[i].ItemArray[6].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void LoadStock()
        {
            try
            {
                //Get Stock

                List<BOLClass> lstClass = new List<BOLClass>();
                lstClass = dalclass.SelectAllClass();
                tvStockList.Nodes.Clear();
                for (int i = 0; i < lstClass.Count; i++)
                {
                    TreeNode ParentNode = new TreeNode();
                    ParentNode.Text = lstClass[i].ClassName.ToString();
                    ParentNode.Name = "ParentNode" + i.ToString();
                    ParentNode.ForeColor = Color.Black;
                    //ParentNode.BackColor = Color.White;
                    ParentNode.ImageIndex = 0;
                    ParentNode.SelectedImageIndex = 0;
                    tvStockList.Nodes.Add(ParentNode);

                    List<BOLCategory> lstCategory = new List<BOLCategory>();
                    lstCategory = dalcategory.GetCategoryByClassID(Int32.Parse(lstClass[i].Id.ToString()));

                    for (int j = 0; j < lstCategory.Count; j++)
                    {
                        TreeNode ChildNode = new TreeNode();
                        ChildNode.Text = lstCategory[j].CategoryName;
                        ChildNode.ForeColor = Color.Black;
                        //ChildNode.BackColor = Color.White;
                        ChildNode.Name = "ChildNode" + j.ToString();
                        ChildNode.ImageIndex = 0;
                        ChildNode.SelectedImageIndex = 0;
                        ParentNode.Nodes.Add(ChildNode);

                        List<BOLStock> lstStk = new List<BOLStock>();
                        lstStk = dalstock.SelectStock("", Int32.Parse(lstCategory[j].Id.ToString()), "", 0);

                        for (int k = 0; k < lstStk.Count; k++)
                        {
                            TreeNode ChildNodeItemCode = new TreeNode();
                            ChildNodeItemCode.Text = lstStk[k].ItemCode + " " + lstStk[k].NameMM;
                            ChildNodeItemCode.Name = lstStk[k].ItemCode;
                            ChildNodeItemCode.ForeColor = Color.Black;
                            //ChildNodeItemCode.BackColor = Color.White;
                            ChildNode.Name = "ChildNodeItemCode" + k.ToString();
                            ChildNodeItemCode.ImageIndex = 0;
                            ChildNodeItemCode.SelectedImageIndex = 0;
                            ChildNode.Nodes.Add(ChildNodeItemCode);
                        }
                    }
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
                LstLocation = dalLocation.SelectAllLocations();

                cboFromLocation.DisplayMember = "Location";
                cboFromLocation.ValueMember = "ID";
                cboFromLocation.DataSource = LstLocation;
                if (LstLocation.Count > 0)
                {
                    cboFromLocation.SelectedIndex = 0;
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
                LstLocation = dalLocation.SelectAllLocations();

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

        private void frmStockTransfer_Load(object sender, EventArgs e)
        {

        }

        private void tvStockList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node.Level == 2)
                {
                    IsTvClick = true;
                    txtItemCode.Text = e.Node.Name;
                    List<BOLStock> lstStk = new List<BOLStock>();
                    lstStk = dalstock.SelectStock("", 0, e.Node.Name, 0);
                    if (lstStk.Count > 0)
                    {
                        txtPrice.Text = lstStk[0].Price.ToString();
                        txtName.Text = lstStk[0].NameMM.ToString();

                    }
                    txtQty.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void dgvStockTransferDetail_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string strCheck = ""; string QtyCheck = "";
                    strCheck = MoeYanPOS.Function.Validation.isNumberField("Price", dgvStockTransferDetail.CurrentRow.Cells[3].Value.ToString());
                    QtyCheck = MoeYanPOS.Function.Validation.isNumberField("Price", dgvStockTransferDetail.CurrentRow.Cells[4].Value.ToString());
                    if (strCheck != "")
                    {
                        MessageBox.Show(strCheck);
                        dgvStockTransferDetail.CurrentRow.Cells[3].Value = "0";
                    }
                    else if (QtyCheck != "")
                    {
                        MessageBox.Show(QtyCheck);
                        dgvStockTransferDetail.CurrentRow.Cells[4].Value = "0";
                    }
                    else
                    {
                        if (dgvStockTransferDetail.CurrentCell.ColumnIndex == 3 | dgvStockTransferDetail.CurrentCell.ColumnIndex == 4)
                        {
                            dgvStockTransferDetail.CurrentRow.Cells[5].Value = Convert.ToString(decimal.Parse(dgvStockTransferDetail.CurrentRow.Cells[3].Value.ToString()) * Int32.Parse(dgvStockTransferDetail.CurrentRow.Cells[4].Value.ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvStockTransferDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 7)
                {
                    if (dgvStockTransferDetail.Rows[e.RowIndex].Cells[0].Value.ToString() == "0")
                    {
                        dgvStockTransferDetail.Rows.Remove(dgvStockTransferDetail.Rows[e.RowIndex]);
                    }
                    else
                    {
                        //daladjustmentDetail.DeleteStockTransferDetail(long.Parse(dgvStockTransferDetail.Rows[e.RowIndex].Cells[0].Value.ToString()));
                        STDetailID.Add(Int32.Parse(dgvStockTransferDetail.Rows[e.RowIndex].Cells[0].Value.ToString()));
                        dgvStockTransferDetail.Rows.Remove(dgvStockTransferDetail.Rows[e.RowIndex]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvStockTransferDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string strCheck = ""; string QtyCheck = "";
                strCheck = MoeYanPOS.Function.Validation.isNumberField("Price", dgvStockTransferDetail.CurrentRow.Cells[3].Value.ToString());
                QtyCheck = MoeYanPOS.Function.Validation.isNumberField("Price", dgvStockTransferDetail.CurrentRow.Cells[4].Value.ToString());
                if (strCheck != "")
                {
                    MessageBox.Show(strCheck);
                    dgvStockTransferDetail.CurrentRow.Cells[3].Value = "0";
                }
                else if (QtyCheck != "")
                {
                    MessageBox.Show(QtyCheck);
                    dgvStockTransferDetail.CurrentRow.Cells[4].Value = "0";
                }
                else
                {
                    if (dgvStockTransferDetail.CurrentCell.ColumnIndex == 3 | dgvStockTransferDetail.CurrentCell.ColumnIndex == 4)
                    {
                        dgvStockTransferDetail.CurrentRow.Cells[5].Value = Convert.ToString(decimal.Parse(dgvStockTransferDetail.CurrentRow.Cells[3].Value.ToString()) * Int32.Parse(dgvStockTransferDetail.CurrentRow.Cells[4].Value.ToString()));
                    }
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
                    if (IsTvClick == false)
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


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strCheck = "";
                strCheck = MoeYanPOS.Function.Validation.isNumberField("Quantity", txtQty.Text);
                if (strCheck != "")
                {
                    MessageBox.Show(strCheck);
                    txtQty.Text = "1";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    btnAdd.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    string qtyCheck = "";
                    qtyCheck = MoeYanPOS.Function.Validation.isNumberField("Quantity", txtQty.Text);
                    if (qtyCheck != "")
                    {
                        MessageBox.Show(qtyCheck);
                        txtQty.Focus();
                    }
                    else
                    {
                        txtAmount.Text = Convert.ToString(Int32.Parse(txtQty.Text) * decimal.Parse(txtPrice.Text));
                        txtRemark.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtItemCode.Text != "" | txtQty.Text != "0")
                {
                    int isExist = 0;
                    isExist = dalstock.CheckStock(txtItemCode.Text);
                    if (isExist == 0)
                    {
                        MessageBox.Show(" This stock code doesn't exist.");
                        return;
                    }
                    else
                    {
                        dgvStockTransferDetail.Rows.Add(0, txtItemCode.Text,txtName.Text,txtPrice.Text, txtQty.Text, txtAmount.Text, txtRemark.Text, "");
                        txtItemCode.Text = "";
                        txtQty.Text = "1";
                        txtRemark.Text = "";
                        txtItemCode.Focus();
                        txtName.Text = "";
                        IsTvClick = false;
                        dgvItemCode.Visible = false;
                        txtPrice.Text = "0";
                        txtAmount.Text = "0";
                    }
                }
                else
                {
                    MessageBox.Show("Enter require data.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            try
            {
                txtItemCode.Text = "";
                txtQty.Text = "1";
                txtRemark.Text = "";
                dgvStockTransferDetail.Rows.Clear();
                txtPrice.Text = "0";
                txtAmount.Text = "0";
                txtVoucherNo.Text = "";
                txtName.Text = "";
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

        private void dgvItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    txtItemCode.Text = dgvItemCode.CurrentRow.Cells[1].Value.ToString();
                    txtPrice.Text = dgvItemCode.CurrentRow.Cells[4].Value.ToString();
                    txtName.Text = dgvItemCode.CurrentRow.Cells[3].Value.ToString();
                    dgvItemCode.Visible = false;
                    txtQty.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvStockTransferDetail.Rows.Count == 0)
                {
                    MessageBox.Show("Please enter require data.");
                    return;
                }
                else
                {
                    BOLStockTransfer bolStockTransfer = new BOLStockTransfer();
                    bolStockTransfer.Date = dtpAdjDate.Value;                   
                    bolStockTransfer.UserID = frmMain.UserID;
                    bolStockTransfer.ID = long.Parse(lblID.Text);
                    bolStockTransfer.LID = long.Parse(cboFromLocation.SelectedValue.ToString());
                    bolStockTransfer.ToLID = long.Parse(cboToLocation.SelectedValue.ToString());
                    bolStockTransfer.VoucherNo = txtVoucherNo.Text;
                    bolStockTransfer.Times = Int32.Parse(txtTimes.Text);

                    if (bolStockTransfer.LID == bolStockTransfer.ToLID)
                    {
                        MessageBox.Show(" Location for From and To can't be the same.");
                        return;
                    }

                    if (bolStockTransfer.ID == 0)
                    {
                        int issaved = 0;
                        issaved = dalStockTransfer.SaveStockTransfer(bolStockTransfer);
                        if (issaved == 1)
                        {
                            for (int i = 0; i < dgvStockTransferDetail.Rows.Count; i++)
                            {
                                BOLStockTransfer bolStockTransferdetail = new BOLStockTransfer();                               
                                bolStockTransferdetail.ID = long.Parse(dgvStockTransferDetail.Rows[i].Cells[0].Value.ToString());
                                bolStockTransferdetail.TransferID = dalStockTransfer.GetStockTransferID();
                                bolStockTransferdetail.ItemCode = dgvStockTransferDetail.Rows[i].Cells[1].Value.ToString();
                                bolStockTransferdetail.Remark = dgvStockTransferDetail.Rows[i].Cells[6].Value.ToString();
                                if (dalLocation.GetLocationByID(bolStockTransfer.LID).IsThisLocation)
                                {
                                    bolStockTransferdetail.Qty = Int32.Parse("-" + dgvStockTransferDetail.Rows[i].Cells[4].Value.ToString());
                                }
                                else
                                {
                                    bolStockTransferdetail.Qty = Int32.Parse(dgvStockTransferDetail.Rows[i].Cells[4].Value.ToString());
                                }
                                bolStockTransferdetail.Price = decimal.Parse(dgvStockTransferDetail.Rows[i].Cells[3].Value.ToString());
                                bolStockTransferdetail.Amount = decimal.Parse(dgvStockTransferDetail.Rows[i].Cells[5].Value.ToString());
                                daladjustmentDetail.InsertStockTransferDetail(bolStockTransferdetail);
                            }
                            MessageBox.Show("Successfully saved.");

                            VoucherNo = txtVoucherNo.Text;

                            btnclear_Click(sender, e);

                            todaydate = DateTime.Now.ToString("yyMMdd");
                            txtVoucherNo.Text = "WT" + todaydate + dalStockTransfer.GetStockTransferMaxID().ToString();
                            //VoucherNo = txtVoucherNo.Text; 
                        }
                    }
                    else
                    {
                        //int isupdate = 0;
                        dalStockTransfer.UpdateStockTransfer(bolStockTransfer);
                        //if (isupdate == 1)
                        //{
                        for (int i = 0; i < dgvStockTransferDetail.Rows.Count; i++)
                        {
                            BOLStockTransfer bolStockTransferdetail = new BOLStockTransfer();
                            bolStockTransferdetail.TransferID = long.Parse(lblID.Text);
                            bolStockTransferdetail.ID = long.Parse(dgvStockTransferDetail.Rows[i].Cells[0].Value.ToString());
                            bolStockTransferdetail.ItemCode = dgvStockTransferDetail.Rows[i].Cells[1].Value.ToString();
                            bolStockTransferdetail.Remark = dgvStockTransferDetail.Rows[i].Cells[6].Value.ToString();
                            if (dalLocation.GetLocationByID(bolStockTransfer.LID).IsThisLocation)
                            {
                                bolStockTransferdetail.Qty = Int32.Parse("-" + dgvStockTransferDetail.Rows[i].Cells[4].Value.ToString());
                            }
                            else
                            {
                                bolStockTransferdetail.Qty = Int32.Parse(dgvStockTransferDetail.Rows[i].Cells[4].Value.ToString());
                            }
                            bolStockTransferdetail.Price = decimal.Parse(dgvStockTransferDetail.Rows[i].Cells[3].Value.ToString());
                            bolStockTransferdetail.Amount = decimal.Parse(dgvStockTransferDetail.Rows[i].Cells[5].Value.ToString());

                            if (STDetailID.Count > 0)
                            {
                                for (int k = 0; k < STDetailID.Count; k++)
                                {
                                    daladjustmentDetail.DeleteStockTransferDetail(long.Parse(STDetailID[k].ToString()));
                                }
                            }

                            if (bolStockTransferdetail.ID == 0)
                            {
                                daladjustmentDetail.InsertStockTransferDetail(bolStockTransferdetail);
                            }
                            else
                            {
                                daladjustmentDetail.UpdateStockTransferDetail(bolStockTransferdetail);
                            }
                        }
                        MessageBox.Show("Successfully updated.");

                        VoucherNo = txtVoucherNo.Text;

                        btnclear_Click(sender, e);

                        todaydate = DateTime.Now.ToString("yyMMdd");
                        txtVoucherNo.Text = "WT" + todaydate + dalStockTransfer.GetStockTransferMaxID().ToString();
                        //VoucherNo = txtVoucherNo.Text;                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //List<BOLStock> lstStk = new List<BOLStock>();
                //lstStk = dalstock.SelectStock("", 0, txtItemCode.Text, 0);
                //dgvItemCode.Rows.Clear();
                //if (lstStk.Count > 0)
                //{
                //    txtName.Text = lstStk[0].NameEng;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtQty.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtVoucherNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtTimes.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cboFromLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cboToLocation.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cboToLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtVoucherNo.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnA4Print_Click(object sender, EventArgs e)
        {
            try
            {
                btnsave_Click(sender, e);                   
                MoeYanPOS.Report.RptStockTransferReport rpt = new MoeYanPOS.Report.RptStockTransferReport();
                //this.UseWaitCursor = true;
                ReportDocument l_Report = new ReportDocument();
                DataSet ds = new DataSet();
                //ds = dalsale.SelectSaleVoucher(long.Parse(lblSaleID.Text), 0, ""); remove by ksaung
                ds = dalStockTransfer.SelectStockTransferVoucher(VoucherNo); //add by ksaung

                if (ds.Tables[0].Rows.Count > 0)
                {

                    ds.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_StockTransferReport.xsd");
                    l_Report.Load(Application.StartupPath + @"\Report\RptStockTransferReport.rpt");

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
                        DataRow dr;
                        dc.ColumnName = "Name";
                        dc.DataType = System.Type.GetType("System.String");
                        dtt.Columns.Add(dc);

                        DataColumn dc1 = new DataColumn();
                        dc1.ColumnName = "Logo";
                        dc1.DataType = System.Type.GetType("System.Byte[]");
                        dtt.Columns.Add(dc1);

                        if (lstvoucherSetting.Count > 0)
                        {
                            for (int i = 0; i < lstvoucherSetting.Count; i++)
                            {
                                dr = dtt.NewRow();
                                dr["Name"] = lstvoucherSetting[0].Name;
                                dr["Logo"] = lstvoucherSetting[0].Logo;
                                dtt.Rows.Add(dr);
                            }

                            l_Report.Subreports[0].SetDataSource(dtt);
                        }

                        l_Report.PrintOptions.PrinterName = lstsystem[0].Printervoucher;
                        l_Report.PrintToPrinter(1, false, 0, 0);
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

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void txtVoucherNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTimes_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtItemCode.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvItemCode_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtRemark_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvStockTransferDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
