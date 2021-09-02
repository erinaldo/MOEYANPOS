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
    public partial class frmCustomer : Form
    {
        BOLCustomer bolcustomer;
        DALCustomer dalcustomer = new DALCustomer();
        DALTownship daltownship = new DALTownship();

        public frmCustomer()
        {
                try
                {
                    InitializeComponent();
                    lblid.Text = dalcustomer.GetCustomerID().ToString();
                    DALDepartment daldepartment = new DALDepartment();
                    List<BOLDepartment> lstdepartment = new List<BOLDepartment>();
                    lstdepartment = daldepartment.FillDepartment();

                    cbodepartment.DisplayMember = "Departmentname";
                    cbodepartment.ValueMember = "Id";
                    cbodepartment.DataSource = lstdepartment;
                    if (lstdepartment.Count > 0)
                        cbodepartment.SelectedIndex = 0;


                    DALDivision daldivision = new DALDivision();
                    List<BOLDivision> lstdivision = new List<BOLDivision>();
                    lstdivision = daldivision.SelectAllDivision();

                    cbodivision.DisplayMember = "Division";
                    cbodivision.ValueMember = "Id";
                    cbodivision.DataSource = lstdivision;
                    if (lstdivision.Count > 0)
                    {
                        cbodivision.SelectedIndex = 0;
                        List<BOLTownship> lstTownship = new List<BOLTownship>();
                        lstTownship = daltownship.GetTownshipByDivisionID(Int32.Parse(cbodivision.SelectedValue.ToString()));
                        cboTownship.DisplayMember = "Township";
                        cboTownship.ValueMember = "ID";
                        cboTownship.DataSource = lstTownship;
                    }
                }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        public frmCustomer(int customerid)
        {
            try
            {
                InitializeComponent();

                DALDepartment daldepartment = new DALDepartment();
                List<BOLDepartment> lstdepartment = new List<BOLDepartment>();
                lstdepartment = daldepartment.FillDepartment();

                cbodepartment.DisplayMember = "Departmentname";
                cbodepartment.ValueMember = "Id";
                cbodepartment.DataSource = lstdepartment;
                if (lstdepartment.Count > 0)
                    cbodepartment.SelectedIndex = 0;


                DALDivision daldivision = new DALDivision();
                List<BOLDivision> lstdivision = new List<BOLDivision>();
                lstdivision = daldivision.SelectAllDivision();

                cbodivision.DisplayMember = "Division";
                cbodivision.ValueMember = "Id";
                cbodivision.DataSource = lstdivision;
                if (lstdivision.Count > 0)
                    cbodivision.SelectedIndex = 0;

                BOLCustomer bolcustomerlist = new BOLCustomer();
                bolcustomerlist = dalcustomer.GetCustomerByCustomerID(customerid);

                lblid.Text = bolcustomerlist.CustomerID.ToString();
                txtcustomername.Text = bolcustomerlist.Name.ToString();
                txtCustomerMyanmarName.Text = bolcustomer.MyanmarName.ToString();
                txtmemberid.Text = bolcustomerlist.Memberid.ToString();
                cbodivision.SelectedValue = bolcustomerlist.Divisionid.ToString();
                txtemail.Text = bolcustomerlist.Email.ToString();
                txtphone.Text = bolcustomerlist.Phone.ToString();
                txtshop.Text = bolcustomerlist.Shop.ToString();
                cboTownship.SelectedValue = bolcustomerlist.Township;
                txtcustomerlevel.Text = bolcustomerlist.Customerlevel.ToString();
                txtcreditlimit.Text = bolcustomerlist.Creditlimit.ToString();
                txtaddress.Text = bolcustomerlist.Address.ToString();
                dtpdob.Text = bolcustomerlist.Dateofbirth.ToString();
                dtpjoinDate.Text = bolcustomerlist.Joindate.ToString();
                txtnrc.Text = bolcustomerlist.Nrc.ToString();
                txtsaletarget.Text = bolcustomerlist.Saletarget.ToString();
                txtdepositamount.Text = bolcustomerlist.DepositAmount.ToString();
                txtcontactperson.Text = bolcustomerlist.Contactperson.ToString();
                cbodepartment.Text = bolcustomerlist.Departmentid.ToString();
                //if (bolcustomerlist.Credittype == "Cash")
                //    chkcash.Checked = true;
                //else
                //    chkcredit.Checked = false;
                btnsave.Text = "Update";

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
               
                //DateTime joindate=dtpjoinDate.Value.Date;
                
                //DateTime today = DateTime.Today;
                //DateTime dob = dtpdob.Value;
                // int age = today.Year - dob.Year;
                if (Validation.isNullOrEmptyField(" Customer ID", txtCustomerID.Text) != "")
                {
                    //lblcustomer.Text = Validation.isNullOrEmptyField(" Customer Name ", txtcustomername.Text);
                    //lblCustomerID.Visible = true;
                }
                else
                {
                   // lblCustomerID.Visible = false;
                }
                if (Validation.isNullOrEmptyField(" Customer Name ", txtcustomername.Text) != "")
                {
                    //lblcustomer.Text = Validation.isNullOrEmptyField(" Customer Name ", txtcustomername.Text);
                   // lblcustomer.Visible = true;
                }
                else
                {
                    //lblcustomer.Visible = false;
                }
                if (Validation.isNullOrEmptyField(" Member ID ", txtmemberid.Text) != "")
                {
                    //lblmemeberid.Text = Validation.isNullOrEmptyField(" Member ID ", txtmemberid.Text);
                    //lblmemeberid.Visible = true;
                }
                else
                {
                    //lblmemeberid.Visible = false;
                }

                if (Validation.isNullOrEmptyField(" Address ", txtaddress.Text) != "")
                {
                   // lbladdress.Text = Validation.isNullOrEmptyField(" Address ", txtaddress.Text);
                   // lbladdress.Visible = true;
                }
                else
                {
                    //lbladdress.Visible = false;
                }
                if (Validation.isNullOrEmptyField(" Phone ", txtphone.Text) != "")
                {
                    //lblphone.Text = Validation.isNullOrEmptyField(" Phone ", txtphone.Text);
                   // lblphone.Visible = true;
                }
                else
                {
                    //lblphone.Visible = false;
                }
                if (Validation.isNullOrEmptyField(" EMail ", txtemail.Text) != "")
                {
                    //lblemail.Text = Validation.isNullOrEmptyField(" EMail ", txtemail.Text);
                    //lblemail.Visible = true;
                }
                else
                {
                    //lblemail.Visible = false;
                }
                if (Validation.isNullOrEmptyField(" Customer Level ", txtcustomerlevel.Text) != "")
                {
                   // lblcustomerlevel.Text = Validation.isNullOrEmptyField(" Customer Level ", txtcustomerlevel.Text);
                    //lblcustomerlevel.Visible = true;
                }
                else
                {
                    //lblcustomerlevel.Visible = false;
                }
              
                
                if (Validation.isNullOrEmptyField(" Shop ", txtshop.Text) != "")
                {
                    //lblshop.Text = Validation.isNullOrEmptyField(" Shop ", txtshop.Text);
                   // lblshop.Visible = true;
                }
                else
                {
                    //lblshop.Visible = false;
                }
                if (Validation.isNullOrEmptyField(" Contact Person ", txtcontactperson.Text) != "")
                {
                   // lblcontactperson.Text = Validation.isNullOrEmptyField(" Contact Person ", txtcontactperson.Text);
                   // lblcontactperson.Visible = true;
                }
                else
                {
                   // lblcontactperson.Visible = false;
                }
                if (Int32.Parse(txtsaletarget.Text)<=0)
                {
                    //lblsaletarget.Text = "Sale Target must be Greater than 0";
                    //lblsaletarget.Visible = true;
                }
                else
                {
                   // lblsaletarget.Visible = false;
                }

                if (btnsave.Text == "Update" & txtcustomername.Text != "") //& txtmemberid.Text != "" & txtaddress.Text != "" & txtphone.Text != "" & txtemail.Text != "" & txtcustomerlevel.Text != "" &  txtshop.Text != "" & Int32.Parse(txtsaletarget.Text) > 0 
                {
                    int isupdate=0;
                    BOLCustomer bolcustomer=new BOLCustomer();
                    bolcustomer.ID = long.Parse(lblid.Text);
                    bolcustomer.CustomerID=txtCustomerID.Text;
                    bolcustomer.Name=txtcustomername.Text;
                    bolcustomer.MyanmarName = txtCustomerMyanmarName.Text;
                    bolcustomer.Memberid=txtmemberid.Text;
                    bolcustomer.Address=txtaddress.Text;
                    bolcustomer.Phone=txtphone.Text;
                    bolcustomer.Dateofbirth = dtpdob.Value;
                    bolcustomer.Joindate = dtpjoinDate.Value;
                    bolcustomer.Currentdate = dtpcurrentdate.Value;
                    bolcustomer.Nrc = txtnrc.Text;
                    bolcustomer.Email=txtemail.Text;
                    bolcustomer.Customerlevel=txtcustomerlevel.Text;
                    bolcustomer.Township = Int32.Parse(cboTownship.SelectedValue.ToString());
                    bolcustomer.Divisionid=Int32.Parse(cbodivision.SelectedValue.ToString());
                    bolcustomer.Creditlimit=Decimal.Parse(txtcreditlimit.Text);
                    bolcustomer.Iscash = chkcash.Checked;
                    bolcustomer.Iscredit = chkcredit.Checked;
                    bolcustomer.Shop = txtshop.Text;
                    bolcustomer.SortingID = Int32.Parse(txtSortingID.Text);
                    bolcustomer.Saletarget = Decimal.Parse(txtsaletarget.Text);
                    bolcustomer.DepositAmount = Decimal.Parse(txtdepositamount.Text);
                    bolcustomer.Contactperson = txtcontactperson.Text;
                    bolcustomer.Departmentid = Int32.Parse(cbodepartment.SelectedValue.ToString());
                    bolcustomer.Iscash = chkcash.Checked;
                    bolcustomer.Iscredit = chkcredit.Checked;
                    bolcustomer.Wholesaleprice = chkwholesale.Checked;
                    bolcustomer.Retailsaleprice = chkretail.Checked;
                    bolcustomer.CreditOpeningAmt = Decimal.Parse(txtCreditOpeningAmt.Text);
                    isupdate=dalcustomer.UpdateCustomerByCustomerID(bolcustomer);

                    if(isupdate==1)
                    {
                        MessageBox.Show(" Customer is Successfully Updated ");
                        btnsearch_Click(sender, e);
                        cleanTextBox();
                        tabcustomer.SelectedIndex=1;
                        frmCustomer_Load(sender,e);
                        lblid.Text = "0";// dalcustomer.GetCustomerID().ToString();
                    }
                    else 
                    {
                        MessageBox.Show(" Customer Name is already exist ");
                        txtcustomername.Focus();
                        txtcustomername.SelectAll();
                    }
                }
                if (btnsave.Text == "&Save" & txtcustomername.Text != "") //& txtmemberid.Text != "" & txtaddress.Text != "" & txtphone.Text != "" & txtemail.Text != "" & txtcustomerlevel.Text != "" & txtshop.Text != ""  & Int32.Parse(txtsaletarget.Text) > 0
                {
                    int issaved = 0;
                    bolcustomer = new BOLCustomer();
                    bolcustomer.ID = long.Parse(lblid.Text);
                    bolcustomer.CustomerID = txtCustomerID.Text;
                    bolcustomer.Name = txtcustomername.Text;
                    bolcustomer.MyanmarName = txtCustomerMyanmarName.Text;
                    bolcustomer.Memberid = txtmemberid.Text;
                    bolcustomer.Address = txtaddress.Text;
                    bolcustomer.Phone = txtphone.Text;
                    bolcustomer.Dateofbirth = dtpdob.Value;
                    bolcustomer.Joindate = dtpjoinDate.Value;
                    bolcustomer.Currentdate = dtpcurrentdate.Value;
                    bolcustomer.Nrc = txtnrc.Text;
                    bolcustomer.Email = txtemail.Text;
                    bolcustomer.Customerlevel = txtcustomerlevel.Text;
                    bolcustomer.Township = Int32.Parse(cboTownship.SelectedValue.ToString());
                    bolcustomer.Divisionid = Int32.Parse(cbodivision.SelectedValue.ToString());
                    bolcustomer.Creditlimit = Decimal.Parse(txtcreditlimit.Text);
                    bolcustomer.Shop = txtshop.Text;
                    bolcustomer.SortingID = Int32.Parse(txtSortingID.Text);
                    bolcustomer.Contactperson = txtcontactperson.Text;
                    bolcustomer.DepositAmount = Decimal.Parse(txtdepositamount.Text);
                    bolcustomer.Departmentid = Int32.Parse(cbodepartment.SelectedValue.ToString());
                    bolcustomer.Saletarget = Decimal.Parse(txtsaletarget.Text);
                    bolcustomer.Wholesaleprice = chkwholesale.Checked;
                    bolcustomer.Retailsaleprice = chkretail.Checked;
                    bolcustomer.Iscash = chkcash.Checked;
                    bolcustomer.Iscredit = chkcredit.Checked;
                    bolcustomer.Wholesaleprice = chkwholesale.Checked;
                    bolcustomer.Retailsaleprice = chkretail.Checked;
                    bolcustomer.CreditOpeningAmt = Decimal.Parse(txtCreditOpeningAmt.Text);
                    issaved = dalcustomer.SaveCustomer(bolcustomer);
                    

                    if (issaved == 1)
                    {
                        MessageBox.Show("Customer is Successfully Saved");
                        cleanTextBox();
                        lblid.Text="0";//dalcustomer.GetCustomerID().ToString();
                        tabcustomer.SelectedIndex = 1;
                        frmCustomer_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show(" CustomerID is already Exist!");
                        txtcustomername.Text="";
                        txtcustomername.Focus();
                        txtcustomername.SelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cleanTextBox()
        {
            try
            {
                btnsave.Text = "&Save";
                txtCustomerID.Text = "";
                txtCustomerID.Enabled = true;
                txtcustomername.Text = "";
                txtCustomerMyanmarName.Text = "";
                txtmemberid.Text = "";
                txtaddress.Text = "";
                txtphone.Text = "";
                txtnrc.Text = "";
                dtpdob.Text = DateTime.Today.ToString();
                dtpjoinDate.Text = DateTime.Today.ToString();
                dtpcurrentdate.Text = DateTime.Today.ToString();
                txtemail.Text = "";
                txtcustomerlevel.Text = "";
                cbodivision.SelectedIndex=0;
                cbodepartment.SelectedIndex = 0;
                txtcreditlimit.Text = "0";
                chkcash.Checked = false;
                chkcredit.Checked = false;
                txtshop.Text = "";
                txtSortingID.Text = "0";
                txtdivision.Text = "";
                txtcontactperson.Text = "";
                txtsaletarget.Text = "0";
                txtdepositamount.Text = "0";
                cbodepartment.SelectedIndex = 0;
                txtCreditOpeningAmt.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void chkcash_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkcash.Checked)
            //{
            //    chkcredit.Checked = false;
            //}
        }

        private void frmCustomer_Load(object sender, EventArgs e)
        {
            try
            {
                txtCustomerID.Text = dalcustomer.GetCustomerCode();
                txtcustomername.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvcustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 27)
                {
                    if (e.RowIndex >= 0)
                    {
                        int id = 0;
                        id = Int32.Parse(dgvcustomer.Rows[e.RowIndex].Cells[1].Value.ToString());
                        tabcustomer.SelectedIndex = 0;

                        lblid.Text = dgvcustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                        txtCustomerID.Text = dgvcustomer.Rows[e.RowIndex].Cells["CustomerID"].Value.ToString();
                        txtCustomerID.Enabled = false;
                        txtcustomername.Text = dgvcustomer.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                        txtCustomerMyanmarName.Text = dgvcustomer.Rows[e.RowIndex].Cells["MyanmarName"].Value.ToString();
                        txtmemberid.Text = dgvcustomer.Rows[e.RowIndex].Cells["Memberid"].Value.ToString();
                        txtaddress.Text = dgvcustomer.Rows[e.RowIndex].Cells["Address"].Value.ToString();
                        txtphone.Text = dgvcustomer.Rows[e.RowIndex].Cells["Phone"].Value.ToString();
                        dtpdob.Value = DateTime.Parse(dgvcustomer.Rows[e.RowIndex].Cells["Dateofbirth"].Value.ToString());
                        dtpjoinDate.Text = dgvcustomer.Rows[e.RowIndex].Cells["Joindate"].Value.ToString();
                        dtpcurrentdate.Text = dgvcustomer.Rows[e.RowIndex].Cells["Currentdate"].Value.ToString();
                        txtnrc.Text = dgvcustomer.Rows[e.RowIndex].Cells["Nrc"].Value.ToString();
                        txtemail.Text = dgvcustomer.Rows[e.RowIndex].Cells["Email"].Value.ToString();
                        txtcustomerlevel.Text = dgvcustomer.Rows[e.RowIndex].Cells["Customerlevel"].Value.ToString();
                        cbodivision.SelectedValue = Int32.Parse( dgvcustomer.Rows[e.RowIndex].Cells["DivisionID"].Value.ToString());

                        List<BOLTownship> lstTownship = new List<BOLTownship>();
                        lstTownship = daltownship.GetTownshipByDivisionID(Int32.Parse(cbodivision.SelectedValue.ToString()));
                        cboTownship.DisplayMember = "Township";
                        cboTownship.ValueMember = "ID";
                        cboTownship.DataSource = lstTownship;
                        cboTownship.SelectedValue = Int32.Parse(dgvcustomer.Rows[e.RowIndex].Cells["TownshipID"].Value.ToString());    
                    
                        txtcreditlimit.Text = dgvcustomer.Rows[e.RowIndex].Cells["Creditlimit"].Value.ToString();                        
                        txtshop.Text = dgvcustomer.Rows[e.RowIndex].Cells["Shop"].Value.ToString();
                        txtSortingID.Text = dgvcustomer.Rows[e.RowIndex].Cells["colSortingID"].Value.ToString();
                        txtdepositamount.Text = dgvcustomer.Rows[e.RowIndex].Cells["DepositAmount"].Value.ToString();
                        cbodepartment.SelectedValue = Int32.Parse(dgvcustomer.Rows[e.RowIndex].Cells["DepartmentID"].Value.ToString());
                        txtcontactperson.Text = dgvcustomer.Rows[e.RowIndex].Cells["Contactperson"].Value.ToString();
                        txtsaletarget.Text = dgvcustomer.Rows[e.RowIndex].Cells["Saletarget"].Value.ToString();
                        txtCreditOpeningAmt.Text = dgvcustomer.Rows[e.RowIndex].Cells["CreditOpeningAmt"].Value.ToString();

                        if (dgvcustomer.Rows[e.RowIndex].Cells["Iscash"].Value.ToString() == "True")                                                                                                    
                        {
                            chkcash.Checked = true;
                        }
                        else
                        {
                            chkcash.Checked = false;
                        }

                        if (dgvcustomer.Rows[e.RowIndex].Cells["Iscredit"].Value.ToString() == "True")
                        {
                            chkcredit.Checked = true;
                        }
                        else
                        {
                            chkcredit.Checked = false;
                        }
                        if (dgvcustomer.Rows[e.RowIndex].Cells["Wholesaleprice"].Value.ToString() == "True")
                        {
                            chkwholesale.Checked = true;
                        }
                        else
                        {
                            chkwholesale.Checked = false;
                        }
                        if (dgvcustomer.Rows[e.RowIndex].Cells["Retailsaleprice"].Value.ToString() == "True")
                        {
                            chkretail.Checked = true;
                        }
                        else
                        {
                            chkretail.Checked = false;
                        }
                       
                        
                        btnsave.Text = "Update";

                    }
                }
                if (e.ColumnIndex == 28)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int customerid = 0;
                            customerid = Int32.Parse(dgvcustomer.Rows[e.RowIndex].Cells[1].Value.ToString());
                            int isdelete = 0;
                            isdelete = dalcustomer.DeleteCustomer(customerid);
                            if (isdelete == 1)
                            {
                                MessageBox.Show("Successfully deleted!");
                                dgvcustomer.Rows.Clear();
                                frmCustomer_Load(sender, e);
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

        private void btnpreview_Click(object sender, EventArgs e)
        {
            try
            {
                List<BOLCustomer> lst = new List<BOLCustomer>();
                for (int i = 0; i < dgvcustomer.Rows.Count ; i++)
                {
                    BOLCustomer bolcustomerpreview = new BOLCustomer();
                    if (dgvcustomer.Rows[i].Cells["chkcustomer"].Value.ToString() == "True")
                    {
                        bolcustomerpreview.CustomerID = dgvcustomer.Rows[i].Cells["CustomerID"].Value.ToString();
                        bolcustomerpreview.Name = dgvcustomer.Rows[i].Cells["Name"].Value.ToString();
                        bolcustomerpreview.MyanmarName = dgvcustomer.Rows[i].Cells["MyanmarName"].Value.ToString();
                        bolcustomerpreview.Memberid = dgvcustomer.Rows[i].Cells["MemberID"].Value.ToString();
                        bolcustomerpreview.Address = dgvcustomer.Rows[i].Cells["Address"].Value.ToString();
                        bolcustomerpreview.Phone = dgvcustomer.Rows[i].Cells["Phone"].Value.ToString();
                        bolcustomerpreview.Email = dgvcustomer.Rows[i].Cells["EMail"].Value.ToString();
                        bolcustomerpreview.Customerlevel = dgvcustomer.Rows[i].Cells["CustomerLevel"].Value.ToString();
                        bolcustomerpreview.TownshipName = dgvcustomer.Rows[i].Cells["Township"].Value.ToString();
                        bolcustomerpreview.Division = dgvcustomer.Rows[i].Cells["Division"].Value.ToString();
                        bolcustomerpreview.Shop = dgvcustomer.Rows[i].Cells["Shop"].Value.ToString();
                        bolcustomerpreview.Creditlimit = decimal.Parse( dgvcustomer.Rows[i].Cells["CreditLimit"].Value.ToString());
                        lst.Add(bolcustomerpreview);
                    }
                                        
                }

                if (lst.Count > 0)
                {
                    this.UseWaitCursor = true;
                    ReportDocument cu_Report = new ReportDocument();
                    cu_Report.Load(Application.StartupPath + @"\Report\RptCustomer.rpt");
                    cu_Report.SetDataSource(lst);
                    cu_Report.SetDatabaseLogon("sa", "moeyan");

                    frmReport frmReport = new frmReport();
                    frmReport.rptViewer.ReportSource = cu_Report;
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

        private void chkcash_Click(object sender, EventArgs e)
        {
            //chkcredit.Checked = false;
        }
        private void txtcustomername_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DALCustomer customer = new DALCustomer();
                    List<BOLCustomer> lst = new List<BOLCustomer>();
                    lst = customer.SelectCustomer(txtcustomername.Text,txtemail.Text);
                    if (lst.Count > 0)
                    {
                        lblcustomer.Text = "This Customer Name is already exist";
                        lblcustomer.Visible = true;
                        txtcustomername.SelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtemail_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar ==13)
                {
                    DALCustomer customer = new DALCustomer();
                    List<BOLCustomer> lst = new List<BOLCustomer>();
                    lst = customer.SelectCustomer(txtcustomername.Text, txtemail.Text);
                    if (lst.Count > 0)
                    {
                        lblemail.Text = "This Customer Email is already exist";
                        lblemail.Visible = true;
                        txtemail.SelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            try
            {
                cleanTextBox();
                btnsave.Text = "&Save";
                lblid.Text = dalcustomer.GetCustomerID().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void chkcredit_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkcredit.Checked)
            //{
            //    txtcreditlimit.Enabled = true;
            //    txtcreditlimit.Focus();
            //}
        }

        private void lbldepartment_Click(object sender, EventArgs e)
        {

        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                string division = "";
                division = txtdivision.Text;


                List<BOLCustomer> bolcustomerlist = new List<BOLCustomer>();
                bolcustomerlist = dalcustomer.SearchCustomer(division);
                dgvcustomer.Rows.Clear();

                if (bolcustomerlist.Count > 0)
                {
                    for (int i = 0; i < bolcustomerlist.Count; i++)
                    {
                        dgvcustomer.Rows.Add(false, bolcustomerlist[i].ID, bolcustomerlist[i].CustomerID, bolcustomerlist[i].Name, bolcustomerlist[i].MyanmarName, bolcustomerlist[i].Memberid, bolcustomerlist[i].Address, bolcustomerlist[i].Phone, bolcustomerlist[i].Dateofbirth, bolcustomerlist[i].Joindate, bolcustomerlist[i].Currentdate, bolcustomerlist[i].Nrc, bolcustomerlist[i].Email, bolcustomerlist[i].Customerlevel, bolcustomerlist[i].TownshipName, bolcustomerlist[i].Division, bolcustomerlist[i].Creditlimit, bolcustomerlist[i].Iscash, bolcustomerlist[i].Iscredit, bolcustomerlist[i].Shop, bolcustomerlist[i].SortingID, bolcustomerlist[i].DepositAmount, bolcustomerlist[i].DepartmentName, bolcustomerlist[i].Contactperson, bolcustomerlist[i].Saletarget, bolcustomerlist[i].Wholesaleprice, bolcustomerlist[i].Retailsaleprice, "", "", bolcustomerlist[i].Township, bolcustomerlist[i].Divisionid, bolcustomerlist[i].Departmentid, bolcustomerlist[i].CreditOpeningAmt);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void picClose1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtsaletarget_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblcustomerid_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void cbodivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<BOLTownship> lstTownship = new List<BOLTownship>();
                lstTownship = daltownship.GetTownshipByDivisionID(Int32.Parse(cbodivision.SelectedValue.ToString()));
                cboTownship.DisplayMember = "Township";
                cboTownship.ValueMember = "ID";
                cboTownship.DataSource = lstTownship;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvcustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSelectAll.Checked)
                {
                    for (int i = 0; i < dgvcustomer.Rows.Count; i++)
                    {
                        dgvcustomer.Rows[i].Cells["chkcustomer"].Value = true;
                    }

                }
                else
                {
                    for (int i = 0; i < dgvcustomer.Rows.Count; i++)
                    {
                        dgvcustomer.Rows[i].Cells["chkcustomer"].Value = false;
                    }
                }
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
                List<BOLCustomer> lstcustomer = new List<BOLCustomer>();
                dgvcustomer.Rows.Clear();
                lstcustomer = dalcustomer.ShowAllCustomerList();
                foreach (BOLCustomer c in lstcustomer)
                {
                    dgvcustomer.Rows.Add(false, c.ID, c.CustomerID, c.Name, c.MyanmarName, c.Memberid, c.Address, c.Phone, c.Dateofbirth, c.Joindate, c.Currentdate, c.Nrc, c.Email, c.Customerlevel, c.TownshipName, c.Division, c.Creditlimit, c.Iscash, c.Iscredit, c.Shop, c.SortingID, c.DepositAmount, c.DepartmentName, c.Contactperson, c.Saletarget, c.Wholesaleprice, c.Retailsaleprice, "", "",c.Township,c.Divisionid,c.Departmentid,c.CreditOpeningAmt);
                }

                txtcustomername.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cboTownship_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void chkcash_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        
    } 
}