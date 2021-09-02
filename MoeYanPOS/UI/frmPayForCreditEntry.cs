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
    public partial class frmPayForCreditEntry : Form
    {
        // Token: 0x04000BCB RID: 3019
        private DALCustomer dalCustomer = new DALCustomer();

        // Token: 0x04000BCC RID: 3020
        private DALPayForCredit dalPayForCredit = new DALPayForCredit();

        // Token: 0x04000BCD RID: 3021
        private DALLocation dalLocation = new DALLocation();

        // Token: 0x04000BCE RID: 3022
        private DALVoucherSetting dalVoucher = new DALVoucherSetting();

        // Token: 0x04000BCF RID: 3023
        private DALSystem dalSystem = new DALSystem();

        // Token: 0x04000BD0 RID: 3024
        private DALClass dalClass = new DALClass();

        // Token: 0x04000BD1 RID: 3025
        private long locationID = 0L;

        // Token: 0x04000BD2 RID: 3026
        private long customerID = 0L;

        // Token: 0x04000BD3 RID: 3027
        private string voucherNo = "";

        // Token: 0x04000BD4 RID: 3028
        private bool isByDate = false;

        // Token: 0x04000BD5 RID: 3029
        private long classid = 0L;

        // Token: 0x04000BD6 RID: 3030
        private DateTime StartDateT;

        // Token: 0x04000BD7 RID: 3031
        private DateTime EndDateT;

        // Token: 0x04000BD8 RID: 3032
        private string CrPaymentType = "";

        public frmPayForCreditEntry()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            showAll();     
        }

        public void showAll()
        {
            try
            {
                string key = (this.dtpFromDate.Value.Month.ToString().Length > 1) ? this.dtpFromDate.Value.Month.ToString() : ("0" + this.dtpFromDate.Value.Month.ToString());
                string text = (this.dtpFromDate.Value.Day.ToString().Length > 1) ? this.dtpFromDate.Value.Day.ToString() : ("0" + this.dtpFromDate.Value.Day.ToString());
                string text2 = (this.dtpToDate.Value.Month.ToString().Length > 1) ? this.dtpToDate.Value.Month.ToString() : ("0" + this.dtpToDate.Value.Month.ToString());
                string text3 = (this.dtpToDate.Value.Day.ToString().Length > 1) ? this.dtpToDate.Value.Day.ToString() : ("0" + this.dtpToDate.Value.Day.ToString());
                string s = string.Concat(new string[]
				{
					this.dtpFromDate.Value.Year.ToString(),
					"-",
					key,
					"-",
					text,
					" 00:00:00.000"
				});
                string s2 = string.Concat(new string[]
				{
					this.dtpToDate.Value.Year.ToString(),
					"-",
					text2,
					"-",
					text3,
					" 23:59:59.000"
				});
                this.StartDateT = DateTime.Parse(s);
                this.EndDateT = DateTime.Parse(s2);
                if (this.cboLocation.Text != "<Select a Location>")
                {
                    this.locationID = long.Parse(this.cboLocation.SelectedValue.ToString());
                }
                if (this.cboClass.Text != "<Select a Location>")
                {
                    this.classid = long.Parse(this.cboClass.SelectedValue.ToString());
                }
                if (this.txtCustomer.Text != "")
                {
                    this.customerID = long.Parse(this.lblCustomerID.Text.ToString());
                }
                else
                {
                    this.customerID = 0L;
                }
                if (this.txtVoucherNo.Text != "")
                {
                    this.voucherNo = this.txtVoucherNo.Text;
                }
                else
                {
                    this.voucherNo = "";
                }
                if (this.chkIsByDate.Checked)
                {
                    this.isByDate = true;
                }
                else
                {
                    this.isByDate = false;
                }
                DataSet dataSet = new DataSet();
                dataSet = this.dalPayForCredit.GetCreditAmountByVoucher(this.locationID, this.classid, this.customerID, this.voucherNo, this.StartDateT, this.EndDateT, this.isByDate);
                this.dgvPayForCredit.Rows.Clear();
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        this.dgvPayForCredit.Rows.Add(new object[]
						{
							dataSet.Tables[0].Rows[i].ItemArray[0].ToString(),
							dataSet.Tables[0].Rows[i].ItemArray[1].ToString(),
							dataSet.Tables[0].Rows[i].ItemArray[2].ToString(),
							dataSet.Tables[0].Rows[i].ItemArray[3].ToString(),
							dataSet.Tables[0].Rows[i].ItemArray[4].ToString(),
							dataSet.Tables[0].Rows[i].ItemArray[5].ToString(),
							dataSet.Tables[0].Rows[i].ItemArray[6].ToString(),
							dataSet.Tables[0].Rows[i].ItemArray[8].ToString(),
							"",
							""
						});
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmPayForCreditEntry_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadLocation();
                this.LoadClass();
                this.showAll();
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
                List<BOLClass> schemaChangedHandler = new List<BOLClass>();
                schemaChangedHandler = this.dalClass.SelectAllClass();
                schemaChangedHandler.Insert(0, new BOLClass
                {
                    Id = 0,
                    ClassName = "<Select a Class>"
                });
                this.cboClass.DisplayMember = "ClassName";
                this.cboClass.ValueMember = "Id";
                this.cboClass.DataSource = schemaChangedHandler;
                if (schemaChangedHandler.Count > 0)
                {
                    this.cboClass.SelectedIndex = 0;
                }
            }
            catch (Exception ds)
            {
                MessageBox.Show(ds.Message.ToString());
            }
        }
        private void LoadLocation()
        {
            try
            {
                List<BolLocation> LstLocation = new List<BolLocation>();
                LstLocation = dalLocation.SelectAllLocation();

                BolLocation bolLocation = new BolLocation();
                bolLocation.ID = 0;
                bolLocation.Location = "<Select a Location>";
                LstLocation.Insert(0, bolLocation);
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

        private void dgvCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtCustomer.Text = dgvCustomer.CurrentRow.Cells[1].Value.ToString();
                    lblCustomerID.Text = dgvCustomer.CurrentRow.Cells[0].Value.ToString();
                    dgvCustomer.Visible = false;
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    dgvCustomer.Visible = false;
                    txtCustomer.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    List<BOLCustomer> lstcustomer = new List<BOLCustomer>();
                    lstcustomer = dalCustomer.GetCustomer(txtCustomer.Text,"",0);
                    dgvCustomer.Rows.Clear();
                    foreach (BOLCustomer c in lstcustomer)
                    {
                        dgvCustomer.Rows.Add(c.ID, c.CustomerID, c.Name, c.Address);
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

        private void dgvPayForCredit_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
             try
            {
                frmCustomerCashPaid CustomerCashPaid = new frmCustomerCashPaid(dgvPayForCredit.Rows[e.RowIndex].Cells[3].Value.ToString(), dgvPayForCredit.Rows[e.RowIndex].Cells[1].Value.ToString(), long.Parse(dgvPayForCredit.Rows[e.RowIndex].Cells[2].Value.ToString()), long.Parse(dgvPayForCredit.Rows[e.RowIndex].Cells[5].Value.ToString()));
                CustomerCashPaid.ShowDialog();
                btnSearch_Click(sender, e);
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtVoucherNo.Text = "";
                txtCustomer.Text = "";
                lblCustomerID.Text = "0";
                chkIsByDate.Checked = false;
                if (cboLocation.Items.Count > 0)
                {
                    cboLocation.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvPayForCredit_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtCustomer_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
