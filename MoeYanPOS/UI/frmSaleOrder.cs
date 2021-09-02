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
namespace MoeYanPOS
{
    public partial class frmSaleOrder : Form
    {
        int total,advance,grandtotal = 0;

        BOLUser LstCheckPrintAndMsgBox = new BOLUser();
        DALSaleOrder dalsaleorder = new DALSaleOrder();
        DALSaleOrderDetail dalsaleorderdetail = new DALSaleOrderDetail();
        DALTransition daltrans = new DALTransition();
        DALStock dalstock = new DALStock();
        DALCustomer dalcustomer = new DALCustomer();
        DALSale dalsale = new DALSale();
        DALMeasurement dalmeasurement = new DALMeasurement();
        DALLocation dallocation = new DALLocation();
        List<Int32> SODetailID = new List<Int32>();

        public frmSaleOrder()
        {
            InitializeComponent();
            SaleOrderSetFormLoad();

            DALCustomer dalcust = new DALCustomer();
            List<BOLCustomer> getlstcustomer = new List<BOLCustomer>();
            getlstcustomer = dalcustomer.GetCustomer("Auto","",0);
            if (getlstcustomer.Count > 0)
            {
                lblCustomerID.Text = getlstcustomer[0].ID.ToString();
                txtCustomer.Text = getlstcustomer[0].CustomerID;
                dgvCustomer.Visible = false;
            }
            else
            {
                BOLCustomer bolcustomer = new BOLCustomer();
                bolcustomer.ID = 0;
                bolcustomer.Name = "Auto";
                bolcustomer.CustomerID = "00001";
                bolcustomer.Memberid = "0";
                bolcustomer.Address = "";
                bolcustomer.Phone = "";
                bolcustomer.Dateofbirth = DateTime.Now;
                bolcustomer.Joindate = DateTime.Now;
                bolcustomer.Currentdate = DateTime.Now;
                bolcustomer.Nrc = "";
                bolcustomer.Email = "";
                bolcustomer.Customerlevel = "";
                bolcustomer.Township = 1;
                bolcustomer.Divisionid = 1;
                bolcustomer.Creditlimit = 0;
                bolcustomer.Iscash = true;
                bolcustomer.Iscredit = false;
                bolcustomer.Shop = "";
                bolcustomer.SortingID = 1;
                bolcustomer.Saletarget = 0;
                bolcustomer.DepositAmount = 0;
                bolcustomer.Contactperson = "";
                bolcustomer.Departmentid = 1;
                dalcust.SaveCustomer(bolcustomer);

                List<BOLCustomer> getcustomer = new List<BOLCustomer>();
                getcustomer = dalcustomer.GetCustomer("Auto", "Cash", 0);
                if (getcustomer.Count > 0 & chkwholesale.Checked != true)
                {
                    lblCustomerID.Text = getcustomer[0].ID.ToString();
                    txtCustomer.Text = getcustomer[0].CustomerID;
                    dgvCustomer.Visible = false;
                }
            }
            dgvsaleorder.Rows.Add();        
            dgvsaleorder.Rows[0].Cells[0].Value = 1;
            dgvsaleorder.Rows[0].Cells[1].Value = "0000";
            dgvsaleorder.Rows[0].Cells[2].Value = "";
            dgvsaleorder.Rows[0].Cells[3].Value = colType.Items[0].ToString();
            dgvsaleorder.Rows[0].Cells[4].Value = "1";
            dgvsaleorder.Rows[0].Cells[5].Value = "0";
            dgvsaleorder.Rows[0].Cells[6].Value = "0";
            dgvsaleorder.Rows[0].Cells[7].Value = "";
            
            dgvsaleorder.Rows[0].Cells[1].Selected = true;
           

        }
        private void SaleOrderSetFormLoad()
        {
            try
            {
                List<BOLMeasurement> lstmeasurement = new List<BOLMeasurement>();
                lstmeasurement = dalmeasurement.SelectAllMeasurement();
                if (lstmeasurement.Count > 0)
                {
                    colType.DisplayMember = "Id";
                    colType.ValueMember = "Measurement";
                    colType.DataSource = lstmeasurement;
                }
                lbluserid.Text = frmMain.UserID.ToString();

                List<BolLocation> lstlocation = new List<BolLocation>();
                lstlocation = dallocation.SelectAllLocation();

                if (lstlocation.Count > 0)
                {
                    cboLocation.DisplayMember = "Location";
                    cboLocation.ValueMember = "ID";
                    cboLocation.DataSource = lstlocation;
                }

                cboLocation.SelectedIndex = 0;

                txtitem.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        public frmSaleOrder(long saleorderid)
        {
            try
            {
                
                InitializeComponent();
                SaleOrderSetFormLoad();
                //lbluserid.Text = frmMain.UserID.ToString();
                List<BOLSaleOrder> bolSaleorderList = new List<BOLSaleOrder>();
                bolSaleorderList = dalsaleorder.GetSaleOrderBySaleID(saleorderid, 0);

                if (bolSaleorderList.Count > 0)
                {
                    lblsaleorderid.Text = saleorderid.ToString();
                    lbluserid.Text = bolSaleorderList[0].Userid.ToString();
                    dtporderdate.Value = bolSaleorderList[0].Orderdate;
                    txtCustomer.Text = bolSaleorderList[0].Customername.ToString();
                    lblCustomerID.Text = bolSaleorderList[0].Customerid.ToString();
                    txtvoucherno.Text = bolSaleorderList[0].Voucherno.ToString();
                    txttotal.Text = bolSaleorderList[0].Totalamt.ToString();
                    txtsystemvoucherno.Text = bolSaleorderList[0].Systemvoucherno.ToString();
                    cboLocation.SelectedValue = bolSaleorderList[0].Locationid;

                    
                    dgvCustomer.Visible = false;
                    dgvsaleorder.Rows.Clear();
                    btnsave.Text = "&Update";
                    //if (dgvSale.CurrentRow != null)
                    //{

                    List<BOLSaleOrder> lstsaleorderdetail = new List<BOLSaleOrder>();
                    lstsaleorderdetail = dalsaleorder.GetSaleDetailList(saleorderid, 0);
                    dgvsaleorder.Rows.Clear();

                    if (lstsaleorderdetail.Count > 0)
                    {
                        for (int i = 0; i < lstsaleorderdetail.Count; i++)
                        {
                            dgvsaleorder.Rows.Add();
                            dgvsaleorder.Rows[i].Cells[0].Value = i + 1;
                            dgvsaleorder.Rows[i].Cells[1].Value = lstsaleorderdetail[i].Itemcode.ToString();
                            dgvsaleorder.Rows[i].Cells[2].Value = lstsaleorderdetail[i].Description.ToString();
                            dgvsaleorder.Rows[i].Cells[3].Value = lstsaleorderdetail[i].Type.ToString();
                            dgvsaleorder.Rows[i].Cells[4].Value = lstsaleorderdetail[i].Qty.ToString();
                            dgvsaleorder.Rows[i].Cells[5].Value = lstsaleorderdetail[i].Saleprice.ToString();
                            dgvsaleorder.Rows[i].Cells[6].Value = lstsaleorderdetail[i].Total.ToString();
                            dgvsaleorder.Rows[i].Cells[7].Value = lstsaleorderdetail[i].Saleorderdetailid.ToString();

                        }

                        // dgvSale.Rows.Add(dgvSale.RowCount + 1, "0000", "", "", "1", "0", 1, colFOC.Items[1].ToString(), "0", "0", "");
                    }
                }

                //dalsale.DeleteTempRows(0);
                btnnew.Visible = true;
            }


            //}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void LoadTemp()
        {
            DateTime voucherno = DateTime.Now;
            string sysVoucherNo = daltrans.GetVoucherNo("SaleOrder",voucherno);
            txtsystemvoucherno.Text = sysVoucherNo.ToString();
            lblTransactionID.Text = daltrans.GetTransitionID("SaleOrder").ToString();

            BOLTransition boltrans = new BOLTransition();
            boltrans.TransName = "SaleOrder";
            boltrans.TransID = daltrans.GetTransitionID("SaleOrder");
            daltrans.SaveTransition(boltrans);

            //if (txtvoucherno.Text == "")
            //{
            //    dgvsaleorder.Rows.Add(dgvsaleorder.Rows.Count + 1, txtsystemvoucherno.Text, txtsystemvoucherno.Text);
            //}
            //else
            //{
            //    dgvsaleorder.Rows.Add(dgvsaleorder.Rows.Count + 1, txtvoucherno.Text, txtsystemvoucherno.Text);
            //}
        }
        private void frmSaleOrder_Load(object sender, EventArgs e)
        {
            try
            {
                LstCheckPrintAndMsgBox = dalsale.CheckIsSavePrint(frmMain.UserID);
                //dgvsaleorder.Rows.Clear();
                if (lblsaleorderid.Text == "0")
                {
                    LoadTemp();
                }
                dgvsaleorder.Focus();
                
                //dgvsaleorder.Rows[0].Cells[0].Value = 1;
                frmMain frm= new frmMain();
                lbluserid.Text = frmMain.UserID.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        //private void dgvsaleorder_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
            //try
            //{
            //    if (e.ColumnIndex == 1)
            //    {
            //        if (dgvsaleorder.Rows[dgvsaleorder.CurrentRow.Index].Cells[1].Value == null)
            //        {
            //            txttotal.Focus();
            //        }
            //    }
            //    if (e.ColumnIndex == 5)
            //    {
            //        dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 2].Cells[6].Value = Convert.ToString(Int32.Parse(dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 2].Cells[4].Value.ToString()) * Int32.Parse(dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 2].Cells[5].Value.ToString()));
            //        dgvsaleorder.CurrentCell = dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 2].Cells[6];
            //        total += Int32.Parse(dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 2].Cells[6].Value.ToString());
            //        txttotal.Text = total.ToString();
            //    }

            //    if (e.ColumnIndex == 6)
            //    {
            //        dgvsaleorder.CurrentCell = dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[0];
            //        dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[0].Value = dgvsaleorder.Rows.Count;

            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
        //}
        private void txttotal_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnsave_Click(sender, e);
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

                if (txttotal.Text == "0" | lblCustomerID.Text == "0")
                {
                    MessageBox.Show(" Please Enter Require Data to Save.");
                    dgvsaleorder.Focus();
                    return;
                }

                int isSaved = 0; int isDetailSaved = 0;

                if (lblsaleorderid.Text == "0")
                {
                    //Save Sale
                    BOLSaleOrder bolsaleorder = new BOLSaleOrder();
                    bolsaleorder.Transsaleorderid = long.Parse(lblTransactionID.Text); //                   
                    bolsaleorder.Voucherno = txtsystemvoucherno.Text;
                    bolsaleorder.Orderdate = dtporderdate.Value;   //  SaleDate;
                    bolsaleorder.Customerid = txtCustomer.Text;
                    bolsaleorder.Userid = Int32.Parse(lbluserid.Text);
                    bolsaleorder.Totalamt = Decimal.Parse(txttotal.Text);
                    bolsaleorder.Systemvoucherno = long.Parse(txtsystemvoucherno.Text);
                    bolsaleorder.Deliverydate = dtpdeliverydate.Value;
                    bolsaleorder.Remark = txtremark.Text;
                    bolsaleorder.Advance = Decimal.Parse(txtadvance.Text);
                    bolsaleorder.Balance = Decimal.Parse(txtbalance.Text);
                    bolsaleorder.Locationid = long.Parse(cboLocation.SelectedValue.ToString());


                    isSaved = dalsaleorder.saveSaleOrderData(bolsaleorder);

                    if (isSaved == 1)
                    {

                        //Save Sale Detail
                        for (int i = 0; i < dgvsaleorder.Rows.Count - 1; i++)
                        {

                            if (dgvsaleorder.Rows.Count > 0)
                            {
                                if (dgvsaleorder.Rows[i].Cells["OrderDetailID"].Value == null)
                                {
                                    BOLSaleOrder bolsaleorderdetail = new BOLSaleOrder();
                                    bolsaleorderdetail.Saleorderid = dalsaleorder.GetMaxSaleOrderID(long.Parse(txtsystemvoucherno.Text));
                                    lblsaleorderid.Text = bolsaleorderdetail.Saleorderid.ToString();
                                    bolsaleorderdetail.Itemcode = dgvsaleorder.Rows[i].Cells["colItem"].Value.ToString();
                                    bolsaleorderdetail.Description = dgvsaleorder.Rows[i].Cells["coldescription"].Value.ToString();
                                    bolsaleorderdetail.Type = "Psc";
                                    bolsaleorderdetail.Qty = Int32.Parse(dgvsaleorder.Rows[i].Cells["colqty"].Value.ToString());
                                    bolsaleorderdetail.Saleprice = Decimal.Parse(dgvsaleorder.Rows[i].Cells["colsaleprice"].Value.ToString());
                                    bolsaleorderdetail.Total = Decimal.Parse(dgvsaleorder.Rows[i].Cells["colTotal"].Value.ToString());

                                    dalsaleorderdetail.SaveOrderDetailData(bolsaleorderdetail);
                                }
                                else if (dgvsaleorder.Rows[i].Cells["OrderDetailID"].Value.ToString() == "")
                                {
                                    BOLSaleOrder bolsaleorderdetail = new BOLSaleOrder();
                                    bolsaleorderdetail.Saleorderid = dalsaleorder.GetMaxSaleOrderID(long.Parse(txtsystemvoucherno.Text));
                                    lblsaleorderid.Text = bolsaleorderdetail.Saleorderid.ToString();
                                    bolsaleorderdetail.Itemcode = dgvsaleorder.Rows[i].Cells["colItem"].Value.ToString();
                                    bolsaleorderdetail.Description = dgvsaleorder.Rows[i].Cells["coldescription"].Value.ToString();
                                    bolsaleorderdetail.Type = "Psc";
                                    bolsaleorderdetail.Qty = Int32.Parse(dgvsaleorder.Rows[i].Cells["colqty"].Value.ToString());
                                    bolsaleorderdetail.Saleprice = Decimal.Parse(dgvsaleorder.Rows[i].Cells["colsaleprice"].Value.ToString());
                                    bolsaleorderdetail.Total = Decimal.Parse(dgvsaleorder.Rows[i].Cells["colTotal"].Value.ToString());

                                    dalsaleorderdetail.SaveOrderDetailData(bolsaleorderdetail);
                                }
                            }
                        }

                        MessageBox.Show("Sale data is successfully saved.");
                        //CleanSaleOrder();

                        if (LstCheckPrintAndMsgBox.IsSavePrint)
                        {
                            //btnPrint_Click(sender, e);
                        }


                        btnnew_Click(sender,e);
                    }
                }
                else
                {

                    if (SODetailID.Count > 0)
                    {
                        for (int k = 0; k < SODetailID.Count; k++)
                        {
                            dalsaleorder.DeleteSaleOrderDetail(long.Parse(SODetailID[k].ToString()), txtsystemvoucherno.Text);
                        }
                    }

                    BOLSaleOrder bolsaleorderupdate = new BOLSaleOrder();
                    bolsaleorderupdate.Saleorderid = long.Parse(lblsaleorderid.Text);
                    bolsaleorderupdate.Userid = Int32.Parse(lbluserid.Text);
                    bolsaleorderupdate.Voucherno = txtsystemvoucherno.Text;
                    bolsaleorderupdate.Editsaledate = dtporderdate.Value;
                    bolsaleorderupdate.Customerid = txtCustomer.Text;
                    bolsaleorderupdate.Edituserid = Int32.Parse(lbluserid.Text);
                    bolsaleorderupdate.Totalamt = Decimal.Parse(txttotal.Text);
                    bolsaleorderupdate.Systemvoucherno = long.Parse(txtsystemvoucherno.Text);
                    bolsaleorderupdate.Locationid = long.Parse(cboLocation.SelectedValue.ToString());

                    isSaved = dalsaleorder.UpdateSaleOrderBySaleID(bolsaleorderupdate);

                    if (isSaved == 1)
                    {
                        //Save Sale Detail
                        for (int i = 0; i < dgvsaleorder.Rows.Count - 1; i++)
                        {

                            if (dgvsaleorder.Rows.Count > 0)
                            {
                                if (dgvsaleorder.Rows[i].Cells["OrderDetailID"].Value != "")
                                {
                                    BOLSaleOrder bolsaleorderdetail = new BOLSaleOrder();
                                    if (dgvsaleorder.Rows[i].Cells["OrderDetailID"].Value != null)
                                    {
                                        bolsaleorderdetail.Saleorderdetailid = long.Parse(dgvsaleorder.Rows[i].Cells["OrderDetailID"].Value.ToString());
                                        bolsaleorderdetail.Itemcode = dgvsaleorder.Rows[i].Cells["colItem"].Value.ToString();
                                        bolsaleorderdetail.Description = dgvsaleorder.Rows[i].Cells["coldescription"].Value.ToString();
                                        bolsaleorderdetail.Type = "Psc";//dgvSale.Rows[i].Cells["colType"].Value.ToString();
                                        bolsaleorderdetail.Qty = Int32.Parse(dgvsaleorder.Rows[i].Cells["colqty"].Value.ToString());
                                        bolsaleorderdetail.Saleprice = Decimal.Parse(dgvsaleorder.Rows[i].Cells["colsaleprice"].Value.ToString());
                                        bolsaleorderdetail.Total = Decimal.Parse(dgvsaleorder.Rows[i].Cells["colTotal"].Value.ToString());

                                        dalsaleorderdetail.UpdateSaleOrderDetailData(bolsaleorderdetail);
                                    }
                                    else
                                    {
                                        //BOLSaleOrder bolsaleorderdetail = new BOLSaleOrder();
                                       // bolsaleorderdetail.Saleorderid = dalsaleorder.GetMaxSaleOrderID(long.Parse(txtsystemvoucherno.Text));
                                       // lblsaleorderid.Text = bolsaleorderdetail.Saleorderid.ToString();
                                        bolsaleorderdetail.Saleorderid =long.Parse(lblsaleorderid.Text);

                                        bolsaleorderdetail.Itemcode = dgvsaleorder.Rows[i].Cells["colItem"].Value.ToString();
                                        //if (dgvsaleorder.Rows[i].Cells["coldescription"].Value.ToString() == null)
                                        bolsaleorderdetail.Description = dgvsaleorder.Rows[i].Cells["coldescription"].Value.ToString();
                                        //}

                                        bolsaleorderdetail.Type = "Psc";//dgvSale.Rows[i].Cells["colType"].Value.ToString();
                                        bolsaleorderdetail.Qty = Int32.Parse(dgvsaleorder.Rows[i].Cells["colqty"].Value.ToString());
                                        bolsaleorderdetail.Saleprice = Decimal.Parse(dgvsaleorder.Rows[i].Cells["colsaleprice"].Value.ToString());
                                        bolsaleorderdetail.Total = Decimal.Parse(dgvsaleorder.Rows[i].Cells["colTotal"].Value.ToString());

                                        dalsaleorderdetail.SaveOrderDetailData(bolsaleorderdetail);
                                    }
                                }
                            }                          
                            // CleanSaleOrder();

                            //btnSave.Text = "&Save";
                            ///CleanTextBox();
                            MessageBox.Show("Sale data is successfully updated.");
                            CleanSaleOrderData();
                        }
                    }
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
       
        private void dgvItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[1].Value = dgvItemCode.CurrentRow.Cells[1].Value.ToString();
                    dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[2].Value = dgvItemCode.CurrentRow.Cells[2].Value.ToString();
                    dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[5].Value = dgvItemCode.CurrentRow.Cells[4].Value.ToString();
                    txtitem.Text = "";
                    dgvItemCode.Visible = false;
                    dgvsaleorder.Focus();
                    dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[0].Selected = false;
                    dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[0].ReadOnly = false;
                    dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[4].Selected = true;
                    dgvsaleorder.CurrentCell = dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[4];
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

        private void dgvCustomer_KeyDown(object sender, KeyEventArgs e){
        try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    lblCustomerID.Text = dgvCustomer.CurrentRow.Cells[0].Value.ToString();
                    txtCustomer.Text = dgvCustomer.CurrentRow.Cells[1].Value.ToString();
                   bool chkPaymentType = false;
                   chkPaymentType = dalcustomer.ChkPaymentType(Int32.Parse(lblCustomerID.Text));
                   if (chkPaymentType)
                   {
                       cbopaymenttype.Enabled = true;
                   }
                   else
                   {
                       cbopaymenttype.Enabled = false;
                   }

                   if (dgvCustomer.CurrentRow.Cells[4].Value.ToString() == "True")
                   {
                       chkwholesale.Checked = true;
                       chkwholesale.Enabled = true;
                   }
                   else
                   {
                       chkwholesale.Checked = false;
                       chkwholesale.Enabled = false;
                   }
                   dgvCustomer.Visible = false;
                   txtvoucherno.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void dgvsaleorder_KeyDown(object sender, KeyEventArgs e)
        {
            int totalamount = 0;
            try
            {
                if (e.KeyCode == Keys.F6)
                {
                    //btnPreview_Click(sender, e);
                }

                if (e.KeyCode == Keys.F7)
                {
                   // btnPreviewVoucher_Click(sender, e);
                }

                if (e.KeyCode == Keys.F8)
                {
                    //btnA4Print_Click(sender, e);
                }

                if (e.KeyCode == Keys.F9)
                {
                    //btnPrint_Click(sender, e);
                }

                if (e.KeyCode == Keys.Delete)
                {
                    foreach (DataGridViewRow row in dgvsaleorder.SelectedRows)
                    {
                        if (row.Cells[7].Value == null)
                        {
                            dgvsaleorder.Rows.Remove(row);
                        }
                        else if (row.Cells[7].Value.ToString() == "")
                        {
                            dgvsaleorder.Rows.Remove(row);
                        }
                        else
                        {
                            //MessageBox.Show("This data is already saved.");
                            if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {

                                SODetailID.Add(Int32.Parse(row.Cells[7].Value.ToString()));
                                dgvsaleorder.Rows.Remove(row);

                                for (int i = 0; i < dgvsaleorder.Rows.Count; i++)
                                {
                                    totalamount += Int32.Parse(dgvsaleorder.Rows[i].Cells[6].Value.ToString());
                                }
                            }

                            txttotal.Text = totalamount.ToString();
                        }
                    }
                }

                if (e.KeyCode == Keys.Enter)
                {
                    if (dgvsaleorder.CurrentCell.ColumnIndex == 1)                    
                        {                            
                            if (dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[1].Value.ToString() != "0000")
                            {
                                List<BOLStock> lstStk = new List<BOLStock>();
                                lstStk = dalstock.SelectStock("", 0, dgvsaleorder.CurrentRow.Cells[1].Value.ToString(),0);
                                if (lstStk.Count > 0)
                                {
                                    dgvsaleorder.CurrentRow.Cells[2].Value = lstStk[0].NameEng;
                                    dgvsaleorder.CurrentRow.Cells[5].Value = lstStk[0].Price;
                                    dgvsaleorder.CurrentCell = dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[4];
                                }
                                else
                                {
                                    MessageBox.Show("Enter correct Item Code!");
                                    txtitem.Text = dgvsaleorder.CurrentRow.Cells[1].Value.ToString();
                                    txtitem.Focus();
                                }
                            }
                            else if (dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[1].Value.ToString() == "0000")
                            {
                                txtadvance.Focus();
                            }
                        }
                        
                        
                        else if (dgvsaleorder.CurrentCell.ColumnIndex == 2)
                        {
                            dgvsaleorder.CurrentCell = dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[4];
                        }

                        else if (dgvsaleorder.CurrentCell.ColumnIndex == 4)
                        {
                            string qtyCheck = "";
                            qtyCheck = MoeYanPOS.Function.Validation.isNumberField("Qty", dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[4].Value.ToString());
                            if (qtyCheck != "")
                            {
                                MessageBox.Show(qtyCheck);
                                dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[4].Value = 1;
                            }
                            dgvsaleorder.CurrentCell = dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[5];
                        }

                        else if (dgvsaleorder.CurrentCell.ColumnIndex == 5)
                        {
                            dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[6].Value = Convert.ToString(Int32.Parse(dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[4].Value.ToString()) * Int32.Parse(dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[5].Value.ToString()));

                            int totalprice = 0;
                            for (int i = 0; i < dgvsaleorder.Rows.Count; i++)
                            {
                                totalprice += Int32.Parse(dgvsaleorder.Rows[i].Cells[6].Value.ToString());
                            }

                            txttotal.Text = totalprice.ToString();
                            dgvsaleorder.CurrentCell = dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[6];

                        }
                        
                        else
                        {
                            dgvsaleorder.Rows.Add();
                            dgvsaleorder.CurrentCell = dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[0];
                            dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[0].Value = dgvsaleorder.Rows.Count;
                            dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[1].Value = "0000";
                            dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[4].Value = "1";
                            dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[5].Value = "0";
                            dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[6].Value = "0";

                            decimal TotalDis = 0; decimal TotalFOC = 0; decimal TotalAmt = 0;

                            for (int i = 0; i < dgvsaleorder.Rows.Count; i++)
                            {
                                TotalAmt += Decimal.Parse(dgvsaleorder.Rows[i].Cells[6].Value.ToString());
                            }


                            txttotal.Text = TotalAmt.ToString();
                            dgvsaleorder.CurrentCell = dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[1];

                        }                        
                    }
                else if (dgvsaleorder.CurrentCell.ColumnIndex == 1)
                {
                    if (dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[1].Value.ToString() == "0000")
                    {
                        txtadvance.Focus();
                    }
                }                 

            }               
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvsaleorder_ImeModeChanged(object sender, EventArgs e)
        {

        }

        private void lblCustomerID_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txttotal_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    string strCheck = "";
            //    strCheck = MoeYanPOS.Function.Validation.isNumberField("Total", txttotal.Text);
            //    if (strCheck != "")
            //    {
            //        MessageBox.Show(strCheck);
            //        txttotal.Text = "0";
            //    }
            //    btnsave_Click(sender, e);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dgvsaleorder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnprint_Click(object sender, EventArgs e)
        {

        }
        private void CleanSaleOrder()
        {
            try
            {

                btnsave.Text = "&Save";
                lblsaleorderid.Text = "0";
                txtvoucherno.Text = "";
                txtremark.Text = "";
                txtCustomer.Text = "";
                lblCustomerID.Text = "0";
                //dtporderdate.Value = DateTime.Parse(DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss ttt"));
                                
                lbluserid.Text = frmMain.UserID.ToString();
                txttotal.Text = "0";
                txtadvance.Text = "0";
                txtbalance.Text = "0";
                dgvItemCode.Rows.Clear();
                dgvCustomer.Visible = false;
                dgvItemCode.Visible = false;
                dgvsaleorder.Rows.Clear();
                dgvsaleorder.Rows.Add();
                dgvsaleorder.Rows[0].Cells[0].Value = 1;
                dgvsaleorder.Rows[0].Cells[1].Value = "0000";
                dgvsaleorder.Rows[0].Cells[2].Value = "";
                dgvsaleorder.Rows[0].Cells[3].Value = colType.Items[0].ToString();
                dgvsaleorder.Rows[0].Cells[4].Value = "1";
                dgvsaleorder.Rows[0].Cells[5].Value = "0";
                dgvsaleorder.Rows[0].Cells[6].Value = "0";
                dgvsaleorder.Rows[0].Cells[7].Value = "";
                dgvsaleorder.Rows[0].Cells[1].Selected = true;
                //LoadTemp();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void btnnew_Click(object sender, EventArgs e)
        {
            try
            {
                CleanSaleOrder();

                string voucherno = DateTime.Now.ToString("yyyyMMdd");
                long sysVoucherNo = Int64.Parse(voucherno + daltrans.GetTransitionID("SaleOrder").ToString());
                txtsystemvoucherno.Text = sysVoucherNo.ToString();
                lblTransactionID.Text = daltrans.GetTransitionID("SaleOrder").ToString();
                //Save Transition
                BOLTransition boltrans = new BOLTransition();
                boltrans.TransName = "Sale";
                boltrans.TransID = daltrans.GetTransitionID("SaleOrder");
                daltrans.SaveTransition(boltrans);
                //saveTampSale();

                //LoadTemp();
                dgvsaleorder.Focus();
                dgvsaleorder.Rows[0].Cells[1].Selected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void CleanSaleOrderData()
        {
            try
            {
                CleanSaleOrder();

                string voucherno = DateTime.Now.ToString("yyyyMMdd");
                long sysVoucherNo = Int64.Parse(voucherno + daltrans.GetTransitionID("SaleOrder").ToString());
                txtsystemvoucherno.Text = sysVoucherNo.ToString();
                lblTransactionID.Text = daltrans.GetTransitionID("SaleOrder").ToString();
                //Save Transition
                BOLTransition boltrans = new BOLTransition();
                boltrans.TransName = "Sale";
                boltrans.TransID = daltrans.GetTransitionID("SaleOrder");
                daltrans.SaveTransition(boltrans);
                //saveTampSale();

                //LoadTemp();
                dgvsaleorder.Focus();
                dgvsaleorder.Rows[0].Cells[1].Selected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void btnpreview_Click(object sender, EventArgs e)
        {
            try
            {
                List<BOLSaleOrder> lst = new List<BOLSaleOrder>();
                for (int i = 0; i < dgvsaleorder.Rows.Count-1; i++)
                {
                    BOLSaleOrder bolsaleorderpreview = new BOLSaleOrder();
                    bolsaleorderpreview.Saleorderid = long.Parse(lblsaleorderid.Text);
                    bolsaleorderpreview.Orderdate = dtporderdate.Value;
                    bolsaleorderpreview.Deliverydate = dtpdeliverydate.Value;
                    bolsaleorderpreview.Customername = txtCustomer.Text;
                    bolsaleorderpreview.Userid = Int32.Parse(lbluserid.Text);
                    //bolsaleorderpreview.Username = bolsaleorderpreview.Username;
                    bolsaleorderpreview.Remark = txtremark.Text;
                    bolsaleorderpreview.Location = cboLocation.Text;
                    bolsaleorderpreview.Itemcode = dgvsaleorder.Rows[i].Cells["colItem"].Value.ToString();                    
                    bolsaleorderpreview.Description = dgvsaleorder.Rows[i].Cells["coldescription"].Value.ToString();                                       
                    bolsaleorderpreview.Qty = Int32.Parse(dgvsaleorder.Rows[i].Cells["colqty"].Value.ToString());                   
                    bolsaleorderpreview.Saleprice = Decimal.Parse(dgvsaleorder.Rows[i].Cells["colsaleprice"].Value.ToString());
                    bolsaleorderpreview.Total = Decimal.Parse(dgvsaleorder.Rows[i].Cells["colTotal"].Value.ToString());

                    if (txtvoucherno.Text =="")
                    {
                        bolsaleorderpreview.Voucherno = txtsystemvoucherno.Text;
                    }
                    else
                    {
                        bolsaleorderpreview.Voucherno = txtvoucherno.Text;
                    }

                    bolsaleorderpreview.Totalamt = Decimal.Parse(txttotal.Text);
                    lst.Add(bolsaleorderpreview);
                }

                if (lst.Count > 0)
                {
                    this.UseWaitCursor = true;
                    ReportDocument od_report = new ReportDocument();
                    od_report.Load(Application.StartupPath + @"\Report\RptSaleOrder.rpt");
                    od_report.SetDataSource(lst);
                    od_report.SetDatabaseLogon("sa", "moeyan");

                    frmReport frmReport = new frmReport();
                    frmReport.rptViewer.ReportSource = od_report;
                    frmReport.rptViewer.Refresh();
                    frmReport.Show();
                    this.UseWaitCursor = false;
                }
                //this.UseWaitCursor = true;
                //ReportDocument l_Report = new ReportDocument();
                //DataSet dts = new DataSet();
                //dts = dalsaleorder.SelectSaleOrderVoucher(long.Parse(txtsystemvoucherno.Text), 1);

                //if (dts.Tables[0].Rows.Count > 0)
                //{
                //    ReportDocument l_Report = new ReportDocument();

                //    dts.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_SaleOrder.xsd");
                //    l_Report.Load(Application.StartupPath + @"\Report\RptSaleOrder.rpt");

                //    DataSet dt = new DataSet();

                //    l_Report.SetDataSource(dts);
                //    l_Report.SetDatabaseLogon("sa", "moeyan");

                //    frmReport frmReport = new frmReport();
                //    frmReport.rptViewer.ReportSource = l_Report;
                //    frmReport.rptViewer.Refresh();
                //    frmReport.Show();
                //    this.UseWaitCursor = false;
                //}
                //else
                //{
                //    MessageBox.Show("No data for Preview");
                //    this.UseWaitCursor = false;
                //}
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

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtadvance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Decimal total = Decimal.Parse(txttotal.Text);
                Decimal advance = Decimal.Parse(txtadvance.Text);
                Decimal balance = total - advance;
                txtbalance.Text = balance.ToString();
                btnsave.Focus();
            }
        }

        private void txttotal_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                frmCustomer customer = new frmCustomer();
                customer.ShowDialog();
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

        private void txtCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    List<BOLCustomer> lstcustomer = new List<BOLCustomer>();
                    lstcustomer = dalcustomer.GetCustomer(txtCustomer.Text, "Cash", 0);

                    //if (lstcustomer.Count == 0 & txtCustomer.Text == "Auto")
                    //{
                    //    DALCustomer dalcust = new DALCustomer();
                    //    BOLCustomer bolcustomer = new BOLCustomer();
                    //    bolcustomer.Name = "Auto";
                    //    bolcustomer.CustomerID = "";
                    //    bolcustomer.Memberid = "0";
                    //    bolcustomer.Address = "";
                    //    bolcustomer.Phone = "";
                    //    bolcustomer.Dateofbirth = DateTime.Now;
                    //    bolcustomer.Joindate = DateTime.Now;
                    //    bolcustomer.Currentdate = DateTime.Now;
                    //    bolcustomer.Nrc = "";
                    //    bolcustomer.Email = "";
                    //    bolcustomer.Customerlevel = "";
                    //    bolcustomer.Township = 1;
                    //    bolcustomer.Divisionid = 1;
                    //    bolcustomer.Creditlimit = 0;
                    //    bolcustomer.Iscash = true;
                    //    bolcustomer.Iscredit = false;
                    //    bolcustomer.Shop = "";
                    //    bolcustomer.SortingID = 1;
                    //    bolcustomer.Saletarget = 0;
                    //    bolcustomer.DepositAmount = 0;
                    //    bolcustomer.Contactperson = "";
                    //    bolcustomer.Departmentid = 1;
                    //    dalcust.SaveCustomer(bolcustomer);

                    //    List<BOLCustomer> getlstcustomer = new List<BOLCustomer>();
                    //    getlstcustomer = dalcustomer.GetCustomer(txtCustomer.Text);
                    //    if (getlstcustomer.Count > 0)
                    //    {
                    //        lblCustomerID.Text = getlstcustomer[0].ID.ToString();
                    //        txtCustomer.Text = getlstcustomer[0].CustomerID;
                    //    }
                    //}
                    //else if (lstcustomer.Count > 0)
                    //{
                    dgvCustomer.Rows.Clear();
                    foreach (BOLCustomer c in lstcustomer)
                    {
                        dgvCustomer.Rows.Add(c.ID, c.CustomerID, c.MyanmarName, c.Address, c.Wholesaleprice);
                        if (c.Name == "Auto" & chkwholesale.Checked == false)
                        {
                            lblCustomerID.Text = c.ID.ToString();
                            txtCustomer.Text = c.Name;
                            dgvCustomer.Visible = false;
                        }
                        else
                        {
                            dgvCustomer.Visible = true;
                        }
                    }

                    dgvCustomer.Focus();
                    dgvCustomer.Visible = true;
                    //}

                    bool isCrdite = false;
                    isCrdite = dalcustomer.ChkPaymentType(Int32.Parse(lblCustomerID.Text));
                    if (isCrdite)
                    {
                        cbopaymenttype.Enabled = true;
                    }
                    else
                    {
                        cbopaymenttype.Enabled = false;
                    }
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
                    dgvItemCode.Rows.Clear();
                    if (lststk.Count > 0)
                    {
                        for (int i = 0; i < lststk.Count; i++)
                        {
                            dgvItemCode.Rows.Add(lststk[i].Id, lststk[i].ItemCode, lststk[i].NameEng, lststk[i].NameMM, lststk[i].Price);
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

        private void dgvsaleorder_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvsaleorder.CurrentCell.ColumnIndex == 1)                    
                {                            
                    if (dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[1].Value.ToString() != "0000")
                    {
                        List<BOLStock> lstStk = new List<BOLStock>();
                        lstStk = dalstock.SelectStock("", 0, dgvsaleorder.CurrentRow.Cells[1].Value.ToString(),0);
                        if (lstStk.Count > 0)
                        {
                            dgvsaleorder.CurrentRow.Cells[2].Value = lstStk[0].NameEng;
                            dgvsaleorder.CurrentRow.Cells[5].Value = lstStk[0].Price;
                            dgvsaleorder.CurrentCell = dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[4];
                        }
                        else
                        {
                            MessageBox.Show("Enter correct Item Code!");
                            txtitem.Text = dgvsaleorder.CurrentRow.Cells[1].Value.ToString();
                            txtitem.Focus();
                        }
                    }
                    else if (dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[1].Value.ToString() == "0000")
                    {
                        txtadvance.Focus();
                    }              
                }   
                    else if (dgvsaleorder.CurrentCell.ColumnIndex == 2)
                    {
                        dgvsaleorder.CurrentCell = dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[4];
                    }
                    else if (dgvsaleorder.CurrentCell.ColumnIndex == 4)
                    {
                        string qtyCheck = "";
                        qtyCheck = MoeYanPOS.Function.Validation.isNumberField("Qty", dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[4].Value.ToString());
                        if (qtyCheck != "")
                        {
                            MessageBox.Show(qtyCheck);
                            dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[4].Value = 1;
                        }
                        //dgvsaleorder.CurrentCell = dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[5];
                        dgvsaleorder.CurrentRow.Cells[6].Value = Convert.ToString(Int32.Parse(dgvsaleorder.CurrentRow.Cells[4].Value.ToString()) * Int32.Parse(dgvsaleorder.CurrentRow.Cells[5].Value.ToString()));
                        dgvsaleorder.CurrentCell = dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[5];
                    }

                    else if (dgvsaleorder.CurrentCell.ColumnIndex == 5)
                    {
                        dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[6].Value = Convert.ToString(Int32.Parse(dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[4].Value.ToString()) * Int32.Parse(dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[5].Value.ToString()));

                        int totalprice = 0;
                        for (int i = 0; i < dgvsaleorder.Rows.Count; i++)
                        {
                            totalprice += Int32.Parse(dgvsaleorder.Rows[i].Cells[6].Value.ToString());
                        }

                        txttotal.Text = totalprice.ToString();
                        dgvsaleorder.CurrentCell = dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[6];

                    }                        
                    else
                    {
                        dgvsaleorder.Rows.Add();
                        dgvsaleorder.CurrentCell = dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[0];
                        dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[0].Value = dgvsaleorder.Rows.Count;
                        dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[1].Value = "0000";
                        dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[4].Value = "1";
                        dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[5].Value = "0";
                        dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[6].Value = "0";

                        decimal TotalDis = 0; decimal TotalFOC = 0; decimal TotalAmt = 0;

                        for (int i = 0; i < dgvsaleorder.Rows.Count; i++)
                        {
                            TotalAmt += Decimal.Parse(dgvsaleorder.Rows[i].Cells[6].Value.ToString());
                        }
                        txttotal.Text = TotalAmt.ToString();
                        //txtbalance.Text = (TotalAmt - Int32.Parse(txtadvance.Text)).ToString();
                        dgvsaleorder.CurrentCell = dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[1];

                    }                        
            }
                // else if (dgvsaleorder.CurrentCell.ColumnIndex == 1)
                //{
                //    if (dgvsaleorder.Rows[dgvsaleorder.Rows.Count - 1].Cells[1].Value.ToString() == "0000")
                //    {
                //        txtadvance.Focus();
                //    }
                //}                 
            //}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmSaleOrder_Click(object sender, EventArgs e)
        {
            try
            {
                dgvItemCode.Visible = false;
                dgvCustomer.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cbopaymenttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvItemCode_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        
    }
}
