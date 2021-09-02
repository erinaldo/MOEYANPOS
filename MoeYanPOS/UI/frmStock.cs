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
using System.Data.SqlClient;
using System.Configuration;
using MoeYanPOS.Report;

namespace MoeYanPOS.UI
{
    public partial class frmStock : Form
    {
        DALStock dalstock = new DALStock();
        DALClass dalclass = new DALClass();
        DALCategory dalcategory = new DALCategory();
        DALBrand dalbrand = new DALBrand();
        DALMeasurement dalmeasurement = new DALMeasurement();
        TreeNode node = null;
        public frmStock()
        {
            InitializeComponent();
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

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {               

                if (Int32.Parse(lblStkID.Text) > 0)
                {
                    if (Int32.Parse(lblEClassId.Text) == 0)
                    {
                        MessageBox.Show(" Please Select a Class!");
                        cboEClass.Focus();
                        return;
                    }

                    if (Int32.Parse(lblECategoryId.Text) == 0)
                    {
                        MessageBox.Show(" Please Select a Category!");
                        cboEcategory.Focus();
                        return;
                    }

                    if (txtEItemCode.Text == "")
                    {
                        MessageBox.Show(" Please fill Item Code!");
                        txtEItemCode.Focus();
                        return;
                    }

                    if (txtEItemBarCode.Text == "")
                    {
                        MessageBox.Show(" Please fill Barcode!");
                        txtEItemBarCode.Focus();
                        return;
                    }

                    if (txtEName.Text == "")
                    {
                        MessageBox.Show(" Please fill Stock Name!");
                        txtEName.Focus();
                        return;
                    }

                    BOLStock bolStock = new BOLStock();
                    bolStock.Id = Int32.Parse(lblStkID.Text);
                    //bolStock.ClassID = Int32.Parse(cboEClass.SelectedValue.ToString());
                    bolStock.CategoryID = Int32.Parse(cboEcategory.SelectedValue.ToString());
                    bolStock.BrandID = Int32.Parse(cboEBrand.SelectedValue.ToString());
                    bolStock.ItemCode = txtEItemCode.Text;
                    bolStock.NameEng = txtEName.Text;
                    bolStock.NameMM = txtEAlianceName.Text;
                    bolStock.Price = decimal.Parse(txtEPrice.Text);
                    bolStock.WholeSalePrice = decimal.Parse(txtEWholeSalePrice.Text);
                    bolStock.MinQty = Int32.Parse(txtEMinQty.Text);
                    bolStock.MaxQty = Int32.Parse(txtEMaxQty.Text);
                    bolStock.TypeID = Int32.Parse(cboEType.SelectedValue.ToString());
                    bolStock.PurchasePrice = decimal.Parse(txtEPurchasePrice.Text);
                    bolStock.UnitQty = Int32.Parse(txtEUnitQty.Text);
                    bolStock.ItemBarCode = txtEItemBarCode.Text;
                    bolStock.Charges = Int32.Parse(txtCharges1.Text);

                    if (chkEIsStock.Checked)
                    {
                        bolStock.IsStock = true;
                    }
                    if (chkEInActive.Checked)
                    {
                        bolStock.InActive = true;
                    }
                    bolStock.Action = 1;

                    if (lblStkID.Text == "0")
                    {
                        MessageBox.Show(" Please Select Stock Code.");
                    }
                    else
                    {
                        dalstock.SaveStock(bolStock);
                        MessageBox.Show("Stock Code is Successfully Updated!", "Sucessful", MessageBoxButtons.OK);
                    }
                        CleanTextBoxes();    
                }
                else
                {

                    if (Int32.Parse(lblClassId.Text) == 0)
                    {
                        MessageBox.Show(" Please Select a Class!");
                        cboClass.Focus();
                        return;
                    }

                    if (Int32.Parse(lblCategoryId.Text) == 0)
                    {
                        MessageBox.Show(" Please Select a Category!");
                        cboCategory.Focus();
                        return;
                    }

                    if (txtItemCode.Text == "")
                    {
                        MessageBox.Show(" Please fill Item Code!");
                        txtItemCode.Focus();
                        return;
                    }

                    if (txtNameInEnglish.Text == "")
                    {
                        MessageBox.Show(" Please fill Stock Name!");
                        txtNameInEnglish.Focus();
                        return;
                    }

                     //save
                    BOLStock bolStock = new BOLStock();
                    bolStock.Id = Int32.Parse(lblStkID.Text);
                    //bolStock.ClassID = Int32.Parse( cboClass.SelectedValue.ToString());
                    bolStock.CategoryID = Int32.Parse(cboCategory.SelectedValue.ToString());
                    bolStock.BrandID = Int32.Parse(cboBrand.SelectedValue.ToString());
                    bolStock.ItemCode = txtItemCode.Text;
                    bolStock.NameEng = txtNameInEnglish.Text;
                    bolStock.NameMM = txtNameInMM.Text;
                    bolStock.Price = Int32.Parse(txtPrice.Text);
                    bolStock.WholeSalePrice = decimal.Parse(txtWholeSalePrice.Text);
                    bolStock.MinQty = Int32.Parse(txtMinQty.Text);
                    bolStock.MaxQty = Int32.Parse(txtMaxQty.Text);
                    bolStock.TypeID = Int32.Parse(cboType.SelectedValue.ToString());
                    bolStock.PurchasePrice= decimal.Parse(txtPurchasePrice.Text);
                    bolStock.UnitQty = Int32.Parse(txtUnitQty.Text);
                    bolStock.ItemBarCode = txtItemBarCode.Text;
                    bolStock.Charges = Int32.Parse(txtCharges.Text);

                    if (chkIsStock.Checked)
                    {
                        bolStock.IsStock = true;
                    }
                    if (chkInActive.Checked)
                    {
                        bolStock.InActive = true;
                    }                
                    bolStock.Action = 0;
               
                    dalstock.SaveStock(bolStock);
                    MessageBox.Show("Stock Code is Successfully Saved!", "Sucessful", MessageBoxButtons.OK);
                    CleanTextBoxes();
                    LoadStock();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void CleanTextBoxes()
        {
            try
            {
                txtUnitQty.Text = "0";
                txtEUnitQty.Text = "0";
                lblCategoryId.Text = "0";
                lblClassId.Text = "0";
                lblEClassId.Text = "0";
                lblECategoryId.Text = "0";

                txtWholeSalePrice.Text = "0";
                txtEWholeSalePrice.Text = "0";

                lblStkID.Text = "0";
                cboClass.SelectedIndex = 0;
                cboCategory.SelectedIndex = 0;
                cboBrand.SelectedIndex = 0;
                txtItemCode.Text = "";
                txtItemBarCode.Text = "";
                txtNameInEnglish.Text = "";
                txtNameInMM.Text="";
                txtPrice.Text="0";
                txtMinQty.Text="0";
                txtMaxQty.Text="0";
                cboType.SelectedIndex = 0;
                chkIsStock.Checked = false;
                chkIsStock.Checked = false;
                txtPurchasePrice.Text = "0";
                txtEPurchasePrice.Text = "0";
                cboEClass.SelectedIndex = 0;
                cboEcategory.SelectedIndex = 0;
                cboEBrand.SelectedIndex = 0;
                txtEItemCode.Text = "";
                txtEItemBarCode.Text = "";
                txtEName.Text = "";
                txtEAlianceName.Text = "";
                txtEPrice.Text = "0";
                txtEMinQty.Text = "0";
                txtEMaxQty.Text = "0";
                cboEType.SelectedIndex = 0;
                chkEIsStock.Checked = false;
                chkEIsStock.Checked = false;
                btnsave.Text = "&Save";
                
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
                int isExist = 0;
                isExist = dalstock.CheckStock(txtItemCode.Text);
                if(isExist>0)
                {
                    MessageBox.Show("Stock Code is already exist.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmStock_Load(object sender, EventArgs e)
        {
            try
            {
                LoadStock();
                LoadCategory();
                LoadClass();
                LoadBrand();
                LoadMeasurement();

                LoadECategory();
                LoadEClass();
                LoadEBrand();
                LoadEMeasurement();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void LoadBrand()
        {
            try
            {
                List<BOLBrand> lstbrand = new List<BOLBrand>();
                lstbrand = dalbrand.ShowAllBrand(0);
                cboBrand.ValueMember = "Id";
                cboBrand.DisplayMember = "Brandname";
                cboBrand.DataSource = lstbrand;
                if (lstbrand.Count > 0)
                {
                    cboBrand.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void LoadEBrand()
        {
            try
            {
                List<BOLBrand> lstbrand = new List<BOLBrand>();
                lstbrand = dalbrand.ShowAllBrand(0);               
                cboEBrand.ValueMember = "Id";
                cboEBrand.DisplayMember = "Brandname";
                cboEBrand.DataSource = lstbrand;
                if (lstbrand.Count > 0)
                {
                    cboEBrand.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void LoadMeasurement()
        {
            try
            {
                List<BOLMeasurement> lstmeasurement = new List<BOLMeasurement>();
                lstmeasurement = dalmeasurement.SelectAllMeasurement();
                cboType.ValueMember = "Id";
                cboType.DisplayMember = "Measurement";
                cboType.DataSource = lstmeasurement;
                if (lstmeasurement.Count > 0)
                {
                    cboType.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void LoadEMeasurement()
        {
            try
            {
                List<BOLMeasurement> lstmeasurement = new List<BOLMeasurement>();
                lstmeasurement = dalmeasurement.SelectAllMeasurement();
               
                cboEType.ValueMember = "Id";
                cboEType.DisplayMember = "Measurement";
                cboEType.DataSource = lstmeasurement;
                if (lstmeasurement.Count > 0)
                {
                    cboEType.SelectedIndex = 0;
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

        private void LoadEClass()
        {
            try
            {
                List<BOLClass> lstclass = new List<BOLClass>();
                lstclass = dalclass.SelectAllClass();
                cboEClass.ValueMember = "ID";
                cboEClass.DisplayMember = "ClassName";
                cboEClass.DataSource = lstclass;
                if (lstclass.Count > 0)
                {
                    cboEClass.SelectedIndex = 0;
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

        private void LoadECategory()
        {
            try
            {
                List<BOLCategory> lstcategory = new List<BOLCategory>();
                lstcategory = dalcategory.ShowAllCategory();               

                cboEcategory.ValueMember = "Id";
                cboEcategory.DisplayMember = "CategoryName";
                cboEcategory.DataSource = lstcategory;
                if (lstcategory.Count > 0)
                {
                    cboEcategory.SelectedIndex = 0;
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
                     TreeNode ParentNode= new TreeNode();
                     ParentNode.Text = lstClass[i].ClassName.ToString();
                     ParentNode.Name = "ParentNode" + i.ToString();
                     ParentNode.ForeColor = Color.Black;
                     ParentNode.BackColor = Color.White;
                     ParentNode.ImageIndex = 0;
                     ParentNode.SelectedImageIndex = 0;
                     tvStockList.Nodes.Add(ParentNode);

                     List<BOLCategory> lstCategory = new List<BOLCategory>();
                     lstCategory = dalcategory.GetCategoryByClassID(Int32.Parse(lstClass[i].Id.ToString()));

                     for(int j=0;j<lstCategory.Count;j++)
                     {
                         TreeNode ChildNode = new TreeNode();
                         ChildNode.Text = lstCategory[j].CategoryName;
                         ChildNode.ForeColor = Color.Black;
                         ChildNode.BackColor = Color.White;
                         ChildNode.Name = "ChildNode" + j.ToString();
                         ChildNode.ImageIndex = 0;
                         ChildNode.SelectedImageIndex = 0;
                         ParentNode.Nodes.Add(ChildNode);

                         List<BOLStock> lstStk = new List<BOLStock>();
                         lstStk = dalstock.GetAllStockByCategory(Int32.Parse(lstCategory[j].Id.ToString()));

                         for(int k=0;k<lstStk.Count;k++)
                         {
                             //TreeNode ChildNodeItemCode= new TreeNode();
                             //ChildNodeItemCode.Text = lstStk[k].ItemCode;
                             //ChildNodeItemCode.ForeColor = Color.Black;
                             //ChildNodeItemCode.BackColor = Color.White;
                             //ChildNode.Name = "ChildNodeItemCode" + k.ToString();
                             //ChildNodeItemCode.ImageIndex = 0;
                             //ChildNodeItemCode.SelectedImageIndex = 0;
                             //ChildNode.Nodes.Add(ChildNodeItemCode);
                             TreeNode ChildNodeItemCode = new TreeNode();
                             ChildNodeItemCode.Text = lstStk[k].ItemCode + " " + lstStk[k].NameEng;
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

        private void tvStockList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void tvStockList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {

                for(int i=0;i<tvStockList.Nodes.Count;i++)
                {
                    for (int j = 0; j < tvStockList.Nodes[i].Nodes.Count; j++)
                    {
                        for (int k = 0; k < tvStockList.Nodes[i].Nodes[j].Nodes.Count; k++)
                        {
                            tvStockList.Nodes[i].Nodes[j].Nodes[k].BackColor = Color.Lavender;
                        }
                    }
                }
                string ItemCode = e.Node.Name;
                List<BOLStock> bolStock = new List<BOLStock>();
                //bolStock = dalstock.GetAllStockByItemCode(ItemCode);
                bolStock = dalstock.SelectStock("", 0, e.Node.Name, 0);
                e.Node.BackColor = Color.White;
                
                if (bolStock.Count > 0)
                {
                    //btnsave.Text = "Update";
                    lblStkID.Text = bolStock[0].Id.ToString();
                    cboEClass.SelectedValue = Int32.Parse(bolStock[0].ClassID.ToString());
                    lblEClassId.Text = bolStock[0].ClassID.ToString();
                    cboEcategory.SelectedValue = Int32.Parse(bolStock[0].CategoryID.ToString());
                    lblECategoryId.Text = bolStock[0].CategoryID.ToString();
                    cboEBrand.SelectedValue = Int32.Parse(bolStock[0].BrandID.ToString());
                    txtEItemCode.Text = bolStock[0].ItemCode;
                    txtEName.Text = bolStock[0].NameEng;
                    txtEAlianceName.Text = bolStock[0].NameMM;
                    txtEPrice.Text = bolStock[0].Price.ToString();
                    txtEMinQty.Text = bolStock[0].MinQty.ToString();
                    txtEMaxQty.Text = bolStock[0].MaxQty.ToString();
                    txtEUnitQty.Text = bolStock[0].UnitQty.ToString();
                    cboEType.SelectedValue = Int32.Parse(bolStock[0].TypeID.ToString());
                    txtEItemBarCode.Text = bolStock[0].ItemBarCode;

                    if (bolStock[0].IsStock.ToString() == "True")
                    {
                        chkEIsStock.Checked = true;
                    }
                    else
                    {
                        chkEIsStock.Checked = false;
                    }
                    if (bolStock[0].InActive.ToString() == "True")
                    {
                        chkEInActive.Checked = true;
                    }
                    else
                    {
                        chkEInActive.Checked = false;
                    }
                    txtEWholeSalePrice.Text = bolStock[0].WholeSalePrice.ToString();
                    txtEPurchasePrice.Text = bolStock[0].PurchasePrice.ToString();
                    btnsave.Text = "&Update";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                CleanTextBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                  

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtEPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strCheck = "";
                strCheck = MoeYanPOS.Function.Validation.isNumberField("Price", txtEPrice.Text);
                if (strCheck != "")
                {
                    MessageBox.Show(strCheck);
                    txtEPrice.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtEMinQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strCheck = "";
                strCheck = MoeYanPOS.Function.Validation.isNumberField("Min Qty", txtEMinQty.Text);
                if (strCheck != "")
                {
                    MessageBox.Show(strCheck);
                    txtEMinQty.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtEMaxQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strCheck = "";
                strCheck = MoeYanPOS.Function.Validation.isNumberField("Max Qty", txtEMaxQty.Text);
                if (strCheck != "")
                {
                    MessageBox.Show(strCheck);
                    txtEMaxQty.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strCheck = "";
                strCheck = MoeYanPOS.Function.Validation.isNumberField("Price", txtPrice.Text);
                if (strCheck != "")
                {
                    MessageBox.Show(strCheck);
                    txtPrice.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtMinQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strCheck = "";
                strCheck = MoeYanPOS.Function.Validation.isNumberField("Min Qty", txtMinQty.Text);
                if (strCheck != "")
                {
                    MessageBox.Show(strCheck);
                    txtMinQty.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtMaxQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strCheck = "";
                strCheck = MoeYanPOS.Function.Validation.isNumberField("Max Qty", txtMaxQty.Text);
                if (strCheck != "")
                {
                    MessageBox.Show(strCheck);
                    txtMaxQty.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cboClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //cboCategory.Text = dalcategory.GetCategoryByClassID(Int32.Parse(cboClass.Text)).ToString();
                List<BOLCategory> lstcategory = new List<BOLCategory>();
                lstcategory = dalcategory.GetCategoryByClassID(Int32.Parse(cboClass.SelectedValue.ToString()));
                lblClassId.Text = cboClass.SelectedValue.ToString();
                if (lstcategory.Count > 0)
                {
                    cboCategory.ValueMember = "Id";
                    cboCategory.DisplayMember = "CategoryName";
                    cboCategory.DataSource = lstcategory;
                    cboCategory.SelectedIndex = 0;
                    cboCategory.Focus();
                }
                else
                {
                    cboCategory.SelectedIndex = -1;
                    lblCategoryId.Text = "0";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cboEClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<BOLCategory> lstcategory = new List<BOLCategory>();
                lstcategory = dalcategory.GetCategoryByClassID(Int32.Parse(cboEClass.SelectedValue.ToString()));
                lblEClassId.Text = cboEClass.SelectedValue.ToString();
                cboEcategory.ValueMember = "Id";
                cboEcategory.DisplayMember = "CategoryName";
                cboEcategory.DataSource = lstcategory;
                if (lstcategory.Count > 0)
                {
                    cboEcategory.SelectedIndex = 0;
                    cboEcategory.Focus();
                }
                else
                {
                    cboEcategory.SelectedIndex = -1;
                    cboEcategory.Text = "";
                    lblECategoryId.Text = "0";
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
                    txtNameInEnglish.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtNameInEnglish_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtNameInMM.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtNameInMM_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cboType.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cboBrand.Focus();
                lblCategoryId.Text = cboCategory.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cboBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtItemCode.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtPrice.Focus();
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
                    txtMinQty.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtMinQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtMaxQty.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtMaxQty_KeyDown(object sender, KeyEventArgs e)
        {
            btnsave.Focus();
        }

        private void cboEcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboEBrand.Focus();
            lblECategoryId.Text = cboEcategory.SelectedValue.ToString();
        }

        private void cboEBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtEItemCode.Focus();
        }

        private void txtEItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtEName.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtEName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtEAlianceName.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtEAlianceName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cboEType.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cboEType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtEPrice.Focus();
        }

        private void txtEPrice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtEMinQty.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtEMinQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtEMaxQty.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtEMaxQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnsave.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtNameInEnglish_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int isExist = 0;
                isExist = dalstock.CheckStockName(txtNameInEnglish.Text);
                if (isExist > 0)
                {
                    MessageBox.Show("Stock Name is already exist.");
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

        private void txtItemCode_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtNameInEnglish.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtNameInEnglish_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtNameInMM.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtNameInMM_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                   cboType.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cboType_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                txtUnitQty.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtUnitQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtPrice.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtPrice_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtWholeSalePrice.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtWholeSalePrice_KeyDown(object sender, KeyEventArgs e)
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

        private void txtPurchasePrice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtMinQty.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtMinQty_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtMaxQty.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtMaxQty_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnsave.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void tvStockList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (null != node)
            {
                node.BackColor = Color.Lavender;
            }
            node = e.Node;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dalstock.GetStockReport("");
                dgvstock.Rows.Clear();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvstock.Rows.Add(false, ds.Tables[0].Rows[i].ItemArray[0].ToString(), ds.Tables[0].Rows[i].ItemArray[1].ToString(), ds.Tables[0].Rows[i].ItemArray[2].ToString(), ds.Tables[0].Rows[i].ItemArray[3].ToString(), ds.Tables[0].Rows[i].ItemArray[4].ToString(), ds.Tables[0].Rows[i].ItemArray[5].ToString(), ds.Tables[0].Rows[i].ItemArray[6].ToString(), ds.Tables[0].Rows[i].ItemArray[7].ToString(), ds.Tables[0].Rows[i].ItemArray[8].ToString(), ds.Tables[0].Rows[i].ItemArray[9].ToString());
                    }
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
                DataSet ds = new DataSet();
                ds = dalstock.GetStockReport(txtSearch.Text);
                dgvstock.Rows.Clear();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvstock.Rows.Add(false, ds.Tables[0].Rows[i].ItemArray[0].ToString(), ds.Tables[0].Rows[i].ItemArray[1].ToString(), ds.Tables[0].Rows[i].ItemArray[2].ToString(), ds.Tables[0].Rows[i].ItemArray[3].ToString(), ds.Tables[0].Rows[i].ItemArray[4].ToString(), ds.Tables[0].Rows[i].ItemArray[5].ToString(), ds.Tables[0].Rows[i].ItemArray[6].ToString(), ds.Tables[0].Rows[i].ItemArray[7].ToString(), ds.Tables[0].Rows[i].ItemArray[8].ToString(), ds.Tables[0].Rows[i].ItemArray[9].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSelectAll.Checked)
                {
                    for (int i = 0; i < dgvstock.Rows.Count; i++)
                    {
                        dgvstock.Rows[i].Cells[0].Value = true;
                    }

                }
                else
                {
                    for (int i = 0; i < dgvstock.Rows.Count; i++)
                    {
                        dgvstock.Rows[i].Cells[0].Value = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnpreview_Click(object sender, EventArgs e)
        {
            try
            {
                    DataSet dsstock= new DataSet();
                    DataTable dtt = new DataTable();

                    DataColumn ClassName = new DataColumn();
                    ClassName.ColumnName = "ClassName";
                    ClassName.DataType = System.Type.GetType("System.String");
                    dtt.Columns.Add(ClassName);

                    DataColumn Category = new DataColumn();
                    Category.ColumnName = "Category";
                    Category.DataType = System.Type.GetType("System.String");
                    dtt.Columns.Add(Category);
                    
                    DataColumn BrandName = new DataColumn();
                    BrandName.ColumnName = "BrandName";
                    BrandName.DataType = System.Type.GetType("System.String");
                    dtt.Columns.Add(BrandName);
                    
                    DataColumn ItemCode = new DataColumn();
                    ItemCode.ColumnName = "ItemCode";
                    ItemCode.DataType = System.Type.GetType("System.String");
                    dtt.Columns.Add(ItemCode);
                    
                    DataColumn NameEng = new DataColumn();
                    NameEng.ColumnName = "NameEng";
                    NameEng.DataType = System.Type.GetType("System.String");
                    dtt.Columns.Add(NameEng);
                    
                    DataColumn Price = new DataColumn();
                    Price.ColumnName = "Price";
                    Price.DataType = System.Type.GetType("System.String");
                    dtt.Columns.Add(Price);

                     DataColumn WholeSalePrice = new DataColumn();
                    WholeSalePrice.ColumnName = "WholeSalePrice";
                    WholeSalePrice.DataType = System.Type.GetType("System.String");
                    dtt.Columns.Add(WholeSalePrice);
                    
                     DataColumn PurchasePrice = new DataColumn();
                    PurchasePrice.ColumnName = "PurchasePrice";
                    PurchasePrice.DataType = System.Type.GetType("System.String");
                    dtt.Columns.Add(PurchasePrice);

                    DataColumn UnitQty = new DataColumn();
                    UnitQty.ColumnName = "UnitQty";
                    UnitQty.DataType = System.Type.GetType("System.String");
                    dtt.Columns.Add(UnitQty);

                for (int i = 0; i < dgvstock.Rows.Count; i++)
                {
                    if (dgvstock.Rows[i].Cells[0].Value.ToString() == "True")
                    {
                        DataRow dr = dtt.NewRow();
                        dr["ClassName"] = dgvstock.Rows[i].Cells[1].Value.ToString();
                        dr["Category"] = dgvstock.Rows[i].Cells[2].Value.ToString();
                        dr["BrandName"] = dgvstock.Rows[i].Cells[3].Value.ToString();
                        dr["ItemCode"] = dgvstock.Rows[i].Cells[4].Value.ToString();
                        dr["NameEng"] = dgvstock.Rows[i].Cells[5].Value.ToString();
                        dr["NameMM"] = dgvstock.Rows[i].Cells[6].Value.ToString();
                        dr["Price"] = dgvstock.Rows[i].Cells[7].Value.ToString();
                        dr["WholeSalePrice"] = dgvstock.Rows[i].Cells[8].Value.ToString();
                        dr["PurchasePrice"] = dgvstock.Rows[i].Cells[9].Value.ToString();
                        dr["UnitQty"] = dgvstock.Rows[i].Cells[10].Value.ToString();
                        dtt.Rows.Add(dr);
                    }
                   
                }
                dsstock.Tables.Add(dtt);
                if (dsstock.Tables[0].Rows.Count > 0)
                {
                    this.UseWaitCursor = true;
                    RptStockReport stkReport = new RptStockReport();
                    dsstock.WriteXmlSchema(Application.StartupPath + @"\DataSets\DS_StockReport.xsd");
                    stkReport.SetDataSource(dsstock);
                    stkReport.SetDatabaseLogon("sa", "moeyan");

                    frmReport frmReport = new frmReport();
                    frmReport.rptViewer.ReportSource = stkReport;
                    frmReport.rptViewer.Refresh();
                    frmReport.Show();
                    this.UseWaitCursor = false;
                }
                else
                {
                    MessageBox.Show(" Please Select Stock for preview.");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                btnsearch_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtPrice_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void txtMaxQty_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void txtMinQty_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void chkIsStock_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkInActive_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtNameInEnglish_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void txtItemCode_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void dgvstock_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 12)
            {
                try
                {
                    string ItemCode = dgvstock.CurrentRow.Cells[4].Value.ToString();
                    List<BOLStock> bolStock = new List<BOLStock>();
                    bolStock = dalstock.SelectStock("", 0, ItemCode, 0);
                    if (bolStock.Count > 0)
                    {
                        //btnsave.Text = "Update";
                        lblStkID.Text = bolStock[0].Id.ToString();
                        cboEClass.SelectedValue = Int32.Parse(bolStock[0].ClassID.ToString());
                        lblEClassId.Text = bolStock[0].ClassID.ToString();
                        cboEcategory.SelectedValue = Int32.Parse(bolStock[0].CategoryID.ToString());
                        lblECategoryId.Text = bolStock[0].CategoryID.ToString();
                        cboEBrand.SelectedValue = Int32.Parse(bolStock[0].BrandID.ToString());
                        txtEItemCode.Text = bolStock[0].ItemCode;
                        txtEName.Text = bolStock[0].NameEng;
                        txtEAlianceName.Text = bolStock[0].NameMM;
                        txtEPrice.Text = bolStock[0].Price.ToString();
                        txtEMinQty.Text = bolStock[0].MinQty.ToString();
                        txtEMaxQty.Text = bolStock[0].MaxQty.ToString();
                        txtEUnitQty.Text = bolStock[0].UnitQty.ToString();
                        cboEType.SelectedValue = Int32.Parse(bolStock[0].TypeID.ToString());
                        txtEItemBarCode.Text = bolStock[0].ItemBarCode;

                        if (bolStock[0].IsStock.ToString() == "True")
                        {
                            chkEIsStock.Checked = true;
                        }
                        else
                        {
                            chkEIsStock.Checked = false;
                        }
                        if (bolStock[0].InActive.ToString() == "True")
                        {
                            chkEInActive.Checked = true;
                        }
                        else
                        {
                            chkEInActive.Checked = false;
                        }
                        txtEWholeSalePrice.Text = bolStock[0].WholeSalePrice.ToString();
                        txtEPurchasePrice.Text = bolStock[0].PurchasePrice.ToString();
                        txtCharges1.Text = bolStock[0].Charges.ToString(); 
                        tabPage2.Show();
                        btnsave.Text = "&Update";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }        

    }
}
