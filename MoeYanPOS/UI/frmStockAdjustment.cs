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
    public partial class frmStockAdjustment : Form
    {
        bool IsTvClick = false;
        DALStock dalstock = new DALStock();
        DALClass dalclass = new DALClass();
        DALCategory dalcategory = new DALCategory();
        DALBrand dalbrand = new DALBrand();
        DALMeasurement dalmeasurement = new DALMeasurement();
        DALAdjustment daladjustment = new DALAdjustment();
        DALAdjustmentDetail daladjustmentDetail = new DALAdjustmentDetail();
        DALAdjustmentType dalAdjustmentType = new DALAdjustmentType();
        DALLocation dalLocation = new DALLocation();
        List<Int32> SADetailID = new List<Int32>();

        public frmStockAdjustment()
        {
            try
            {
                InitializeComponent();
                LoadStock();
                LoadAdjType();
                LoadLocation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        public frmStockAdjustment(long ID)
        {
            try
            {
                InitializeComponent();
                LoadStock();
                LoadAdjType();
                LoadLocation();
                BOLAdjustment boladjustment = new BOLAdjustment();
                boladjustment = daladjustment.GetAdjustment(ID);

                if (boladjustment.ID != 0)
                {
                    dtpAdjDate.Value = boladjustment.AdjDate;
                    lblID.Text = boladjustment.ID.ToString();
                    cboLocation.SelectedValue = boladjustment.LID;
                    DataSet ds = new DataSet();
                    ds = daladjustmentDetail.GetAdjustmentDetail(ID);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            dgvAdjDetail.Rows.Add(ds.Tables[0].Rows[i].ItemArray[1].ToString(), ds.Tables[0].Rows[i].ItemArray[2].ToString(), ds.Tables[0].Rows[i].ItemArray[3].ToString(), ds.Tables[0].Rows[i].ItemArray[0].ToString(), ds.Tables[0].Rows[i].ItemArray[4].ToString(), ds.Tables[0].Rows[i].ItemArray[5].ToString());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmStockAdjustment_Load(object sender, EventArgs e)
        {
            try
            {
                //txtItemCode.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void LoadAdjType()
        {
            try
            {
                DALAdjustmentType dal = new DALAdjustmentType();
                List<BOLAdjustmentType> lstAdjustmentType = new List<BOLAdjustmentType>();
                lstAdjustmentType = dal.ShowAllAdjustmentType();

                if (lstAdjustmentType.Count > 0)
                {
                    cboHeader.DisplayMember = "Header";
                    cboHeader.ValueMember = "ID";
                    cboHeader.DataSource = lstAdjustmentType;
                    cboHeader.SelectedIndex = 0;
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
                        lstStk = dalstock.SelectStock("", Int32.Parse(lstCategory[j].Id.ToString()), "",0);

                        for (int k = 0; k < lstStk.Count; k++)
                        {
                            TreeNode ChildNodeItemCode = new TreeNode();
                            ChildNodeItemCode.Text = lstStk[k].ItemCode + " "+ lstStk[k].NameEng;
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
                LstLocation = dalLocation.SelectAllLocation();
                
                cboLocation.DisplayMember = "Location";
                cboLocation.ValueMember = "ID";
                cboLocation.DataSource = LstLocation;
                if (LstLocation.Count > 0)
                {
                    cboLocation.SelectedIndex = 0;
                    for (int i = 0; i < LstLocation.Count; i++)
                    {
                        if (LstLocation[i].IsThisLocation)
                        {
                            cboLocation.SelectedValue = LstLocation[i].ID;
                        }
                    }
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
                dgvAdjDetail.Rows.Clear();
                txtPrice.Text = "0";
                txtAmount.Text = "0";
               
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

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                //string adjDate = dtpAdjDate.Value.ToString("dd-MMM-yyyy");

                if (dgvAdjDetail.Rows.Count ==0)
                {
                    MessageBox.Show("Please enter require data.");
                    return;
                }
                else
                {
                    BOLAdjustment bolAdjustment = new BOLAdjustment();
                    bolAdjustment.AdjDate = dtpAdjDate.Value;
                    bolAdjustment.AdjustmentTypeID = Int32.Parse(cboHeader.SelectedValue.ToString());
                    bolAdjustment.UserID = frmMain.UserID;
                    bolAdjustment.ID = long.Parse(lblID.Text);
                    bolAdjustment.LID = long.Parse(cboLocation.SelectedValue.ToString());

                    if (bolAdjustment.ID == 0)
                    {
                        int issaved = 0;
                        issaved=daladjustment.SaveAdjustment(bolAdjustment);
                        if (issaved == 1)
                        {
                            for (int i = 0; i < dgvAdjDetail.Rows.Count; i++)
                            {
                                BOLAdjustment bolAdjustmentdetail = new BOLAdjustment();

                                string adjType = dalAdjustmentType.CheckAdjustmentType(bolAdjustment.AdjustmentTypeID);
                                if (adjType != "")
                                {
                                    if (adjType == "In")
                                    {
                                        bolAdjustmentdetail.Qty = Int32.Parse(dgvAdjDetail.Rows[i].Cells[3].Value.ToString());
                                    }
                                    if (adjType == "Out")
                                    {
                                        bolAdjustmentdetail.Qty = Int32.Parse("-"+dgvAdjDetail.Rows[i].Cells[3].Value.ToString());
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(" Please Check Adjustment Type !! ");
                                }
                                bolAdjustmentdetail.ID = long.Parse(dgvAdjDetail.Rows[i].Cells[0].Value.ToString());
                                bolAdjustmentdetail.AdjustmentID = daladjustment.GetAdjustmentID();
                                bolAdjustmentdetail.ItemCode = dgvAdjDetail.Rows[i].Cells[1].Value.ToString();
                                bolAdjustmentdetail.Remark = dgvAdjDetail.Rows[i].Cells[5].Value.ToString();
                                bolAdjustmentdetail.Price = decimal.Parse(dgvAdjDetail.Rows[i].Cells[2].Value.ToString());
                                bolAdjustmentdetail.Amount = decimal.Parse(dgvAdjDetail.Rows[i].Cells[4].Value.ToString());
                                daladjustmentDetail.InsertAdjustmentDetail(bolAdjustmentdetail);
                                
                                
                            }
                            MessageBox.Show("Successfully saved.");
                            btnclear_Click(sender, e);   
                        }
                    }
                    else
                    {
                        //int isupdate = 0;
                        daladjustment.UpdateAdjustment(bolAdjustment);
                        //if (isupdate == 1)
                        //{
                            for (int i = 0; i < dgvAdjDetail.Rows.Count; i++)
                            {
                                BOLAdjustment bolAdjustmentdetail = new BOLAdjustment();
                                bolAdjustmentdetail.AdjustmentID = long.Parse(lblID.Text);
                                bolAdjustmentdetail.ID = long.Parse(dgvAdjDetail.Rows[i].Cells[0].Value.ToString());
                                bolAdjustmentdetail.ItemCode = dgvAdjDetail.Rows[i].Cells[1].Value.ToString();
                                bolAdjustmentdetail.Qty = Int32.Parse(dgvAdjDetail.Rows[i].Cells[3].Value.ToString());
                                bolAdjustmentdetail.Remark = dgvAdjDetail.Rows[i].Cells[5].Value.ToString();
                                bolAdjustmentdetail.Price = decimal.Parse(dgvAdjDetail.Rows[i].Cells[2].Value.ToString());
                                bolAdjustmentdetail.Amount = decimal.Parse(dgvAdjDetail.Rows[i].Cells[4].Value.ToString());

                                if (SADetailID.Count > 0)
                                {
                                    for (int k = 0; k < SADetailID.Count; k++)
                                    {
                                        daladjustmentDetail.DeleteAdjustmentDetail(long.Parse(SADetailID[k].ToString()));
                                    }
                                }

                                if (bolAdjustmentdetail.ID == 0)
                                {
                                    daladjustmentDetail.InsertAdjustmentDetail(bolAdjustmentdetail);
                                }
                                else
                                {
                                    daladjustmentDetail.UpdateAdjustmentDetail(bolAdjustmentdetail);
                                }
                            }
                            MessageBox.Show("Successfully updated.");
                            btnclear_Click(sender, e);
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
                        dgvAdjDetail.Rows.Add(0, txtItemCode.Text, txtPrice.Text, txtQty.Text, txtAmount.Text, txtRemark.Text, "");
                        txtItemCode.Text = "";
                        txtQty.Text = "1";
                        txtRemark.Text = "";
                        txtItemCode.Focus();
                        IsTvClick = false;
                        dgvItemCode.Visible = false;
                        txtPrice.Text = "0";
                        txtAmount.Text = "0";
                        txtName.Text = "";
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

        private void dgvAdjDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 6)
                {
                    if (dgvAdjDetail.Rows[e.RowIndex].Cells[0].Value.ToString() == "0")
                    {
                        dgvAdjDetail.Rows.Remove(dgvAdjDetail.Rows[e.RowIndex]);
                    }
                    else
                    {
                        //daladjustmentDetail.DeleteAdjustmentDetail(long.Parse(dgvAdjDetail.Rows[e.RowIndex].Cells[0].Value.ToString()));
                        SADetailID.Add(Int32.Parse(dgvAdjDetail.Rows[e.RowIndex].Cells[0].Value.ToString()));
                        dgvAdjDetail.Rows.Remove(dgvAdjDetail.Rows[e.RowIndex]);
                    }
                }
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
                if (e.Node.Level == 2)
                {
                    IsTvClick = true;
                    txtItemCode.Text = e.Node.Name;
                    List<BOLStock> lstStk = new List<BOLStock>();
                    lstStk = dalstock.SelectStock("", 0, e.Node.Name, 0);                   
                    if (lstStk.Count > 0)
                    {
                        txtPrice.Text=lstStk[0].Price.ToString();
                        
                    }
                    txtQty.Focus();
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

        private void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //List<BOLStock> lstStk = new List<BOLStock>();
                //lstStk = dalstock.SelectStock("", 0, txtItemCode.Text, 0);
                //    dgvItemCode.Rows.Clear();
                //    if (lstStk.Count > 0)
                //    {
                //            txtName.Text= lstStk[0].NameEng;
                //    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmStockAdjustment_Click(object sender, EventArgs e)
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

        private void dgvItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    txtItemCode.Text = dgvItemCode.CurrentRow.Cells[1].Value.ToString();
                    txtPrice.Text = dgvItemCode.CurrentRow.Cells[4].Value.ToString();
                    txtName.Text = dgvItemCode.CurrentRow.Cells[2].Value.ToString();
                    dgvItemCode.Visible = false;
                    txtQty.Focus();
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
                    qtyCheck = MoeYanPOS.Function.Validation.isNumberField("Quantity",txtQty.Text);
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

        private void cboHeader_SelectedIndexChanged(object sender, EventArgs e)
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

        private void dgvAdjDetail_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string strCheck = "";string QtyCheck="";
                    strCheck = MoeYanPOS.Function.Validation.isNumberField("Price", dgvAdjDetail.CurrentRow.Cells[2].Value.ToString());
                    QtyCheck = MoeYanPOS.Function.Validation.isNumberField("Price", dgvAdjDetail.CurrentRow.Cells[3].Value.ToString());
                    if (strCheck != "")
                    {
                        MessageBox.Show(strCheck);
                        dgvAdjDetail.CurrentRow.Cells[2].Value = "0";
                    }
                    else if (QtyCheck != "")
                    {
                        MessageBox.Show(QtyCheck);
                        dgvAdjDetail.CurrentRow.Cells[3].Value = "0";
                    }
                    else
                    {
                        if (dgvAdjDetail.CurrentCell.ColumnIndex == 2 | dgvAdjDetail.CurrentCell.ColumnIndex == 3)
                        {
                            dgvAdjDetail.CurrentRow.Cells[4].Value = Convert.ToString(decimal.Parse(dgvAdjDetail.CurrentRow.Cells[2].Value.ToString()) * Int32.Parse(dgvAdjDetail.CurrentRow.Cells[3].Value.ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvAdjDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string strCheck = ""; string QtyCheck = "";
                strCheck = MoeYanPOS.Function.Validation.isNumberField("Price", dgvAdjDetail.CurrentRow.Cells[2].Value.ToString());
                QtyCheck = MoeYanPOS.Function.Validation.isNumberField("Price", dgvAdjDetail.CurrentRow.Cells[3].Value.ToString());
                if (strCheck != "")
                {
                    MessageBox.Show(strCheck);
                    dgvAdjDetail.CurrentRow.Cells[2].Value = "0";
                }
                else if (QtyCheck != "")
                {
                    MessageBox.Show(QtyCheck);
                    dgvAdjDetail.CurrentRow.Cells[3].Value = "0";
                }
                else
                {
                    if (dgvAdjDetail.CurrentCell.ColumnIndex == 2 | dgvAdjDetail.CurrentCell.ColumnIndex == 3)
                    {
                        dgvAdjDetail.CurrentRow.Cells[4].Value = Convert.ToString(decimal.Parse(dgvAdjDetail.CurrentRow.Cells[2].Value.ToString()) * Int32.Parse(dgvAdjDetail.CurrentRow.Cells[3].Value.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
