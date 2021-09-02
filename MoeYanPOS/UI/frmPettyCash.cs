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

namespace MoeYanPOS.UI
{
    public partial class frmPettyCash : Form
    {
        DALLocation dallocation = new DALLocation();
        DALPettyCash dalpettycash = new DALPettyCash();
        public frmPettyCash()
        {
            InitializeComponent();
            LocationLoad();
            rdoGetAmt.Checked = true;
            //LoadOutletCash();
            lbluserid.Text = frmMain.UserID.ToString();
        }
        public frmPettyCash(long ID)
        {
            try
            {
                InitializeComponent();
                LocationLoad();
                //LoadOutletCash();
                List<BOLPettyCash> bolpettycashlist = new List<BOLPettyCash>();
                bolpettycashlist = dalpettycash.GetPettyCashByPettyCashID(ID);

                if (bolpettycashlist.Count > 0)
                {
                    if (ID != 0)
                    {
                        lblID.Text = ID.ToString();
                        lbluserid.Text = bolpettycashlist[0].UserID.ToString();
                        txtAmt.Text = bolpettycashlist[0].Amount.ToString();
                        txtRemark.Text = bolpettycashlist[0].Remark.ToString();
                        if (bolpettycashlist[0].IsGetAmt)
                        {
                            rdoGetAmt.Checked = true;
                        }
                        else
                        {
                            rdoPayAmt.Checked = true;
                        }
                        cboType.Text = bolpettycashlist[0].Type.ToString();
                        dtppurchasedate.Text = bolpettycashlist[0].Date.ToString();
                        //cboLocation.SelectedValue = Int32.Parse(bolpettycashlist[0].LocationID.ToString());
                        cboLocation.Text = bolpettycashlist[0].LocationID.ToString();
                        txtVoucherNo.Text = bolpettycashlist[0].VoucherNo.ToString();
                        btnSave.Text = "&Update";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        //private void LoadOutletCash()
        //{
        //    try
        //    {
        //        DALOutletCashHeader dal = new DALOutletCashHeader();
        //        List<BOLOutLetCashHeader> lstOutletCashHeader = new List<BOLOutLetCashHeader>();
        //        lstOutletCashHeader = dal.ShowAllOutLetCashHeader();

        //        if (lstOutletCashHeader.Count > 0)
        //        {
        //            cboType.DisplayMember = "Header";
        //            cboType.ValueMember = "ID";
        //            cboType.DataSource = lstOutletCashHeader;
        //            cboType.SelectedIndex = 0;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message.ToString());
        //    }
        //}
        private void LocationLoad()
        {
            try
            {
                List<BolLocation> lstlocation = new List<BolLocation>();
                lstlocation = dallocation.SelectAllLocation();

                if (lstlocation.Count > 0)
                {
                    cboLocation.DisplayMember = "Location";
                    cboLocation.ValueMember = "ID";
                    cboLocation.DataSource = lstlocation;
                    for (int i = 0; i < lstlocation.Count; i++)
                    {
                        if (lstlocation[i].IsThisLocation)
                        {
                            cboLocation.SelectedValue = lstlocation[i].ID;
                        }
                    }
                    cboLocation.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void frmPettyCash_Load(object sender, EventArgs e)
        {
            txtVoucherNo.Focus();
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "&Save")
                {
                    int issaved = 0;
                    BOLPettyCash bolpettycash = new BOLPettyCash();
                    bolpettycash.ID = long.Parse(lblID.Text);
                    bolpettycash.Date = dtppurchasedate.Value;
                    bolpettycash.LocationID = Int32.Parse(cboLocation.SelectedValue.ToString());
                    bolpettycash.Amount = Decimal.Parse(txtAmt.Text);
                    bolpettycash.UserID = Int32.Parse(lbluserid.Text);
                    bolpettycash.Remark = txtRemark.Text;
                    bolpettycash.IsGetAmt = rdoGetAmt.Checked;
                    bolpettycash.IsPaidAmt = rdoPayAmt.Checked;
                    bolpettycash.Type=cboType.Text;
                    bolpettycash.VoucherNo = txtVoucherNo.Text;

                    issaved = dalpettycash.SavePettyCash(bolpettycash);

                    if (issaved == 1)
                    {
                        MessageBox.Show("Petty Cash is Successfully Saved");
                        CleanTextBox();
                    }
                }
                if (btnSave.Text == "&Update")
                {
                    int isupdate = 0;
                    BOLPettyCash bolpettycash = new BOLPettyCash();
                    bolpettycash.ID = long.Parse(lblID.Text);
                    bolpettycash.Date = dtppurchasedate.Value;
                    bolpettycash.LocationID = Int32.Parse(cboLocation.SelectedValue.ToString());
                    bolpettycash.Amount = Decimal.Parse(txtAmt.Text);
                    bolpettycash.Remark = txtRemark.Text;
                    bolpettycash.IsGetAmt = rdoGetAmt.Checked;
                    bolpettycash.IsPaidAmt = rdoPayAmt.Checked;
                    bolpettycash.Type=cboType.Text;
                    bolpettycash.VoucherNo = txtVoucherNo.Text;

                    isupdate = dalpettycash.UpdatePettyCashByPettyCashID(bolpettycash);

                    if (isupdate == 1)
                    {
                        MessageBox.Show(" Petty Cash is Successfully Updated ");
                        CleanTextBox();
                        lblID.Text = "0";
                        this.Close();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void CleanTextBox()
        {
            dtppurchasedate.Value = DateTime.Now;
            txtAmt.Text = "0";
            txtRemark.Text = "";
            if (cboLocation.Items.Count > 0)
            {
                cboLocation.SelectedIndex = 0;
            }
            lblID.Text = "0";
            lbluserid.Text = "0";
            rdoGetAmt.Checked = false;
            rdoPayAmt.Checked = false;
            txtVoucherNo.Text = "";
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            CleanTextBox();
        }

        private void txtAmt_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    string strCheck = "";
            //    strCheck = MoeYanPOS.Function.Validation.isNumberField("Amount", txtAmt.Text);
            //    if (strCheck != "")
            //    {
            //        MessageBox.Show(strCheck);
            //        txtAmt.Text = "0";
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
        }

        private void rdoPayAmt_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //cboType.Enabled = true;
                DALOutletCashHeader dal = new DALOutletCashHeader();
                List<BOLOutLetCashHeader> lstOutletCashHeader = new List<BOLOutLetCashHeader>();
                lstOutletCashHeader = dal.GetOutletCashHeaderByType(rdoPayAmt.Text);

                if (lstOutletCashHeader.Count > 0)
                {
                    cboType.DisplayMember = "Header";
                    cboType.ValueMember = "ID";
                    cboType.DataSource = lstOutletCashHeader;
                    cboType.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void rdoGetAmt_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //cboType.Enabled = false;
                DALOutletCashHeader dal = new DALOutletCashHeader();
                List<BOLOutLetCashHeader> lstOutletCashHeader = new List<BOLOutLetCashHeader>();
                lstOutletCashHeader = dal.GetOutletCashHeaderByType(rdoGetAmt.Text);

                if (lstOutletCashHeader.Count > 0)
                {
                    cboType.DisplayMember = "Header";
                    cboType.ValueMember = "ID";
                    cboType.DataSource = lstOutletCashHeader;
                    cboType.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtAmt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string strCheck = "";
                    strCheck = MoeYanPOS.Function.Validation.isNumberField("Amount", txtAmt.Text);
                    if (strCheck != "")
                    {
                        MessageBox.Show(strCheck);
                        txtAmt.Text = "0";
                    }
                    txtRemark.Focus();
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
                    txtAmt.Focus();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
