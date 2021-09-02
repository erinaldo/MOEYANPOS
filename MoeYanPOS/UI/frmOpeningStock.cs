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

namespace MoeYanPOS.UI
{
    public partial class frmOpeningStock : Form
    {
        DALOpeningStock dalOpeningStock = new DALOpeningStock();
        DALStock dalstock = new DALStock();
        DALClass dalclass = new DALClass();
        DALCategory dalcategory = new DALCategory();
        DALTransition daltrans = new DALTransition();
        BOLOpeningStock BolOpeningStockforUpdate = new BOLOpeningStock();
        DALExchangeRate dalexchangerate = new DALExchangeRate();
        DALLocation dalLocation = new DALLocation();
        List<Int32> SOpenDetailID = new List<Int32>();


        public frmOpeningStock(long ID)
        {
            try
            {
                
                InitializeComponent();
                LoadLocation();
                BolOpeningStockforUpdate = dalOpeningStock.GetOpeningStock(ID, 0);
                BolOpeningStockforUpdate.ID = ID;
                if (BolOpeningStockforUpdate.ID > 0)
                {
                    lblID.Text = ID.ToString();
                    txtTempID.Text = ID.ToString();
                    //txtItemCode.Text = BolOpeningStockforUpdate.ItemCode;
                    dtpOpeningDate.Value = BolOpeningStockforUpdate.OpeningDate;
                    //txtQty.Text = BolOpeningStockforUpdate.Qty.ToString();
                    //txtSalePrice.Text = BolOpeningStockforUpdate.SalePrice.ToString();
                    //txtPurchasePrice.Text = BolOpeningStockforUpdate.PurchasePrice.ToString();
                    cboCurrency.SelectedValue = BolOpeningStockforUpdate.CurrencyID;
                    //lblStockName.Text = BolOpeningStockforUpdate.Name;
                    dgvOpeningStock.Rows.Add(ID, BolOpeningStockforUpdate.OpeningDate, BolOpeningStockforUpdate.ItemCode, BolOpeningStockforUpdate.Name, BolOpeningStockforUpdate.Qty.ToString(), BolOpeningStockforUpdate.SalePrice.ToString(), BolOpeningStockforUpdate.PurchasePrice.ToString(), "");
                    btnSave.Text = "Update";
                    dgvItemCode.Visible = false;
                    pnlByItemCode.Enabled = false;
                    //dgvOpeningStock.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }


        public frmOpeningStock()
        {
            try
            {
                InitializeComponent();
                LoadLocation();
                LoadTemp();
                //LoadStock();                
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                    if (dgvOpeningStock.Rows.Count == 0)
                        {
                            MessageBox.Show("Please enter require data.");
                            return;
                        }
                       
                             for (int i = 0; i < dgvOpeningStock.Rows.Count; i++)
                             {
                                    BOLOpeningStock bolOpeningStock = new BOLOpeningStock();                        
                                    bolOpeningStock.ID = long.Parse(dgvOpeningStock.Rows[i].Cells[0].Value.ToString());
                                    bolOpeningStock.OpeningDate = DateTime.Parse(dtpOpeningDate.Value.ToString()); //DateTime.Parse(dgvOpeningStock.Rows[i].Cells[1].Value.ToString());
                                    bolOpeningStock.ItemCode = dgvOpeningStock.Rows[i].Cells[2].Value.ToString();
                                    bolOpeningStock.Qty = Int32.Parse(dgvOpeningStock.Rows[i].Cells[4].Value.ToString());
                                    bolOpeningStock.Name = dgvOpeningStock.Rows[i].Cells[3].Value.ToString();
                                    bolOpeningStock.SalePrice = decimal.Parse(dgvOpeningStock.Rows[i].Cells[5].Value.ToString());
                                    bolOpeningStock.PurchasePrice = decimal.Parse(dgvOpeningStock.Rows[i].Cells[6].Value.ToString());
                                    bolOpeningStock.CurrencyID = Int32.Parse(cboCurrency.SelectedValue.ToString());
                                    bolOpeningStock.LocationID = long.Parse(cboLocation.SelectedValue.ToString());

                                    if (SOpenDetailID.Count > 0)
                                    {
                                        for (int k = 0; k < SOpenDetailID.Count; k++)
                                        {
                                            dalOpeningStock.DeleteOpeningStock(long.Parse(SOpenDetailID[k].ToString()),0);
                                        }
                                    }

                                  if (lblID.Text == "0")
                                    {
                                      //int issaved=0;
                                      //issaved=
                                      dalOpeningStock.SaveOpeningStock(bolOpeningStock,0);

                                      //if (issaved == 0 | issaved < 0)
                                      //{
                                      //    MessageBox.Show("This stock is already exist in Opening Stock Table.");
                                      //}
                                      //else
                                      //{
                                      //    MessageBox.Show("Successfully saved.");
                                      //}
                                   }
                                  else
                                  {
                                      dalOpeningStock.UpdateOpeningStock(bolOpeningStock, 0);
                                      dalOpeningStock.UpdateOpeningStockDate(bolOpeningStock);
                                      this.Close();
                                     // MessageBox.Show("Successfully updated.");
                                  }                                    
                             }

                             if (lblID.Text == "0")
                             {
                                 MessageBox.Show("Successfully saved.");
                             }
                             else
                             {
                                 MessageBox.Show("Successfully updated.");
                             }
                             dalOpeningStock.DeleteOpeningTemp();
                             btnclear_Click(sender, e);
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
                btnSave.Text = "Save";
                if (cboCurrency.Items.Count > 0)
                {
                    cboCurrency.SelectedIndex = 0;
                }
                if (cboLocation.Items.Count > 0)
                {
                    cboLocation.SelectedIndex = 0;
                }
                chkAutoFill.Checked = false;
                txtItemCode.Text = "";
                txtQty.Text = "0";                
                lblID.Text = "0";
                dgvOpeningStock.Rows.Clear();
                txtSalePrice.Text = "0";
                txtPurchasePrice.Text = "0";
                dgvItemCode.Visible = false;
                saveTampOpening();
                pnlByItemCode.Enabled = true;
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

        private void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            
        }       

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            try
            {
                dgvOpeningStock.Rows.Clear();
                chkAutoFill.Checked = false;
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

                List<BOLStock> lstStk = new List<BOLStock>();

                if (chkAutoFill.Checked)
                {
                    if (chkClass.Checked | chkCategory.Checked)
                    {                        
                        if (chkClass.Checked)
                        {
                            lstStk = dalstock.SelectStock("", 0, "", Int32.Parse(cboClass.SelectedValue.ToString()));
                        }

                        if (chkCategory.Checked)
                        {
                            lstStk = dalstock.SelectStock("", Int32.Parse(cboCategory.SelectedValue.ToString()), "", 0);
                        }
                        
                    }

                    else
                    {
                        lstStk = dalstock.SelectStock("", 0,"", 0);
                    }

                    if (lstStk.Count > 0)
                    {
                        for (int i = 0; i < lstStk.Count; i++)
                        {
                            dgvOpeningStock.Rows.Add(0, dtpOpeningDate.Value.ToString(), lstStk[i].ItemCode,lstStk[i].NameEng,0,lstStk[i].WholeSalePrice,lstStk[i].PurchasePrice, "");
                        }
                        saveTampOpening();
                        MessageBox.Show("Adding stock to table is finish.You can choose another Class or Category.");
                    }

                    else
                    {
                        MessageBox.Show("There is no stock for Class or Category.");
                    }
                }
                else
                {
                    int isExist = 0;
                    isExist = dalstock.CheckStock(txtItemCode.Text);

                    if(txtItemCode.Text=="")
                    {
                        MessageBox.Show("Please enter Stock Code.");
                    }
                    
                    else if (isExist == 0)
                    {
                        MessageBox.Show(" This stock code doesn't exist.");
                        return;
                    }

                    else
                    {
                        //if (lblID.Text == "0")
                        //{
                        //         int count = 0;
                        //count = dalOpeningStock.ChkSaveOpening(txtItemCode.Text);
                        //if (count > 0)
                        //{
                        //    MessageBox.Show("That Item is already exist in Opening Stock Table !!");
                        //    txtItemCode.Focus();
                        //    return;
                        //}
                        //else
                        //{
                        //    for (int i = 0; i < dgvOpeningStock.Rows.Count; i++)
                        //    {
                        //        if (dgvOpeningStock.Rows[i].Cells[2].Value.ToString() == txtItemCode.Text)
                        //        {
                        //            MessageBox.Show("That Item is already added to Table !!");
                        //            txtItemCode.Focus();
                        //            return;
                        //        }
                        //    }
                        //}
                        //}
                        if (BolOpeningStockforUpdate.ID == 0)
                        {
                            dgvOpeningStock.Rows.Add(0, dtpOpeningDate.Value.ToString(), txtItemCode.Text, lblStockName.Text, txtQty.Text, txtSalePrice.Text, txtPurchasePrice.Text, "");
                        }
                        else
                        {
                            dgvOpeningStock.Rows.Add(BolOpeningStockforUpdate.ID, dtpOpeningDate.Value.ToString(), txtItemCode.Text, lblStockName.Text, txtQty.Text, txtSalePrice.Text, txtPurchasePrice.Text, "");
                        }

                        saveTampOpening();
                        txtSalePrice.Text = "0";
                        txtItemCode.Text = "0";
                        txtQty.Text = "0";
                        txtPurchasePrice.Text = "0";
                        txtItemCode.Focus();
                        dgvItemCode.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void chkAutoFill_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
               if(chkAutoFill.Checked)
               {
                    pnlByItemCode.Enabled = false;
               }
               else
               {
                   pnlByItemCode.Enabled = true;
               }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void chkClass_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                chkCategory.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void chkCategory_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                chkClass.Checked = false;
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
                                dgvItemCode.Rows.Add(lstStk[i].Id, lstStk[i].ItemCode, lstStk[i].NameEng, lstStk[i].NameMM, lstStk[i].WholeSalePrice, lstStk[i].PurchasePrice);
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

        private void frmOpeningStock_Load(object sender, EventArgs e)
        {
            try
            {
                LoadCategory();
                LoadClass();                
                LoadCurrency();
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
                bolcategory.CategoryName ="<Select a Category>";
                lstcategory.Insert(0,bolcategory);
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

        private void LoadCurrency()
        {
            try
            {
                 //Get Currency
                List<BOLCurrency> lstCurrency = new List<BOLCurrency>();
                lstCurrency = dalexchangerate.FillCurrency();
               if (lstCurrency.Count > 0)
                {
                    cboCurrency.DisplayMember = "Currency";
                    cboCurrency.ValueMember = "Id";
                    cboCurrency.DataSource = lstCurrency;
                    cboCurrency.SelectedIndex = 0;   
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
                        txtSalePrice.Focus();
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

        private void dgvItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    txtItemCode.Text = dgvItemCode.CurrentRow.Cells[1].Value.ToString();
                    lblStockName.Text = dgvItemCode.CurrentRow.Cells[2].Value.ToString();
                    txtSalePrice.Text = dgvItemCode.CurrentRow.Cells[4].Value.ToString();
                    txtPurchasePrice.Text = dgvItemCode.CurrentRow.Cells[5].Value.ToString();
                    dgvItemCode.Visible = false;
                    txtQty.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmOpeningStock_Click(object sender, EventArgs e)
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


        private void LoadTemp()
        {
            try
            { 
                DataSet ds = new DataSet();
                ds = dalOpeningStock.GetOpeningStockTemp();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dgvOpeningStock.Rows.Add(ds.Tables[0].Rows[i].ItemArray[0].ToString(), ds.Tables[0].Rows[i].ItemArray[1].ToString(), ds.Tables[0].Rows[i].ItemArray[2].ToString(), ds.Tables[0].Rows[i].ItemArray[3].ToString(), ds.Tables[0].Rows[i].ItemArray[4].ToString(), ds.Tables[0].Rows[i].ItemArray[5].ToString(), ds.Tables[0].Rows[i].ItemArray[6].ToString(), "");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void saveTampOpening()
        {
            try
            {
                if (lblID.Text == "0")
                {
                    for (int i = 0; i < dgvOpeningStock.Rows.Count; i++)
                    {
                    BOLOpeningStock bolOpeningStock = new BOLOpeningStock();
                    bolOpeningStock.ID = long.Parse(dgvOpeningStock.Rows[i].Cells[0].Value.ToString());
                    bolOpeningStock.OpeningDate = DateTime.Parse(dgvOpeningStock.Rows[i].Cells[1].Value.ToString());
                    bolOpeningStock.ItemCode = dgvOpeningStock.Rows[i].Cells[2].Value.ToString();
                    bolOpeningStock.Qty = Int32.Parse(dgvOpeningStock.Rows[i].Cells[4].Value.ToString());
                    bolOpeningStock.Name = dgvOpeningStock.Rows[i].Cells[3].Value.ToString();
                    bolOpeningStock.SalePrice = decimal.Parse(dgvOpeningStock.Rows[i].Cells[5].Value.ToString());
                    bolOpeningStock.PurchasePrice = decimal.Parse(dgvOpeningStock.Rows[i].Cells[6].Value.ToString());
                    bolOpeningStock.CurrencyID = Int32.Parse(cboCurrency.SelectedValue.ToString());
                    int chktemp = 0;
                    chktemp = dalOpeningStock.ChkSaveOpeningTemp(bolOpeningStock.ID);
                    if (chktemp == 0)
                    {
                        
                        dalOpeningStock.SaveOpeningStock(bolOpeningStock, 1);
                    }
                    else if (chktemp > 0)
                    {
                        dalOpeningStock.UpdateOpeningStock(bolOpeningStock, 1);
                    }
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
                if (e.ColumnIndex == 7)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (MessageBox.Show("Are you sure to delete ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int OpeningStockid = 0;                            
                            OpeningStockid = Int32.Parse(dgvOpeningStock.Rows[e.RowIndex].Cells[0].Value.ToString());
                            int isdelete = 0;
                            if (OpeningStockid != 0)
                            {
                                //isdelete = dalOpeningStock.DeleteOpeningStock(OpeningStockid,1);
                                if (1 == 1)
                                {
                                    //MessageBox.Show("Successfully Deleted!");
                                    SOpenDetailID.Add(Convert.ToInt32(dgvOpeningStock.Rows[e.RowIndex].Cells[0].Value.ToString()));
                                    dgvOpeningStock.Rows.Remove(dgvOpeningStock.CurrentRow);
                                }
                            }
                            else
                            {
                                dgvOpeningStock.Rows.Remove(dgvOpeningStock.CurrentRow);
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
        

        private void dgvOpeningStock_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void txtSalePrice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                     txtPurchasePrice.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtSalePrice_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    string strCheck = "";
            //    strCheck = MoeYanPOS.Function.Validation.isNumberField("SalePrice", txtSalePrice.Text);
            //    if (strCheck != "")
            //    {
            //        MessageBox.Show(strCheck);
            //        txtSalePrice.Text = "0";
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
        }

        private void txtPurchasePrice_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    string strCheck = "";
            //    strCheck = MoeYanPOS.Function.Validation.isNumberField("PurchasePrice", txtPurchasePrice.Text);
            //    if (strCheck != "")
            //    {
            //        MessageBox.Show(strCheck);
            //        txtPurchasePrice.Text = "0";
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
        }

        private void dgvOpeningStock_KeyDown_1(object sender, KeyEventArgs e)
        {

        }

        private void dgvOpeningStock_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dgvOpeningStock_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    if (dgvOpeningStock.CurrentCell.ColumnIndex == 4 | dgvOpeningStock.CurrentCell.ColumnIndex == 5 | dgvOpeningStock.CurrentCell.ColumnIndex == 6)
                    {
                        string strSalePrice = ""; string QtyCheck = ""; string strPurchasePrice = "";
                        strSalePrice = MoeYanPOS.Function.Validation.isNumberField("Sale Price", dgvOpeningStock.CurrentRow.Cells[5].Value.ToString());
                        QtyCheck = MoeYanPOS.Function.Validation.isNumberField("Quantity", dgvOpeningStock.CurrentRow.Cells[4].Value.ToString());
                        strPurchasePrice = MoeYanPOS.Function.Validation.isNumberField("Purchase Price", dgvOpeningStock.CurrentRow.Cells[6].Value.ToString());

                        if (strSalePrice != "")
                        {
                            MessageBox.Show(strSalePrice);
                            dgvOpeningStock.CurrentRow.Cells[5].Value = "0";
                            return;
                        }

                        if (QtyCheck != "")
                        {
                            MessageBox.Show(QtyCheck);
                            dgvOpeningStock.CurrentRow.Cells[4].Value = "0";
                            return;
                        }

                        if (strPurchasePrice != "")
                        {
                            MessageBox.Show(strPurchasePrice);
                            dgvOpeningStock.CurrentRow.Cells[6].Value = "0";
                            return;
                        }
                    }

                    BOLOpeningStock bolOpeningStock = new BOLOpeningStock();
                    bolOpeningStock.ID = long.Parse(dgvOpeningStock.CurrentRow.Cells[0].Value.ToString());
                    bolOpeningStock.OpeningDate = DateTime.Parse(dgvOpeningStock.CurrentRow.Cells[1].Value.ToString());
                    bolOpeningStock.ItemCode = dgvOpeningStock.CurrentRow.Cells[2].Value.ToString();
                    bolOpeningStock.Qty = Int32.Parse(dgvOpeningStock.CurrentRow.Cells[4].Value.ToString());
                    bolOpeningStock.Name = dgvOpeningStock.CurrentRow.Cells[3].Value.ToString();
                    bolOpeningStock.SalePrice = decimal.Parse(dgvOpeningStock.CurrentRow.Cells[5].Value.ToString());
                    bolOpeningStock.PurchasePrice = decimal.Parse(dgvOpeningStock.CurrentRow.Cells[6].Value.ToString());
                    bolOpeningStock.CurrencyID = Int32.Parse(cboCurrency.SelectedValue.ToString());
                    int chktemp = 0;
                    chktemp = dalOpeningStock.ChkSaveOpeningTemp(bolOpeningStock.ID);

                    if (chktemp == 0)
                    {
                        dalOpeningStock.SaveOpeningStock(bolOpeningStock, 1);
                    }
                    else if (chktemp > 0)
                    {
                        dalOpeningStock.UpdateOpeningStock(bolOpeningStock, 1);
                    }
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
                txtItemCode.Text = dgvItemCode.CurrentRow.Cells[1].Value.ToString();
                dgvItemCode.Visible = false;
                txtQty.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtPurchasePrice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnAdd_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    string strCheck = "";
            //    strCheck = MoeYanPOS.Function.Validation.isNumberField("Quantity", txtQty.Text);
            //    if (strCheck != "")
            //    {
            //        MessageBox.Show(strCheck);
            //        txtQty.Text = "0";
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
        }
    }
}
