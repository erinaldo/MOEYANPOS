using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MoeYanPOS.UI
{
    public partial class frmMsgVoucher : Form
    {
        public static decimal PaidAmt=0; public static decimal ChangeAmt=0;
        public frmMsgVoucher(decimal Total)
        {
            try
            {
                InitializeComponent();

                txtTotal.Text = Total.ToString();
                txtPaidAmt.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                txtPaidAmt.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmMsgVoucher_Load(object sender, EventArgs e)
        {
            btnOK.DialogResult = DialogResult.OK;
            btnCancel.DialogResult = DialogResult.Cancel;
        }

        private void txtPaidAmt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtChange.Text = Convert.ToString(decimal.Parse(txtPaidAmt.Text) - decimal.Parse(txtTotal.Text));
                    txtChange.Focus();
                    PaidAmt = decimal.Parse(txtPaidAmt.Text);
                    ChangeAmt = decimal.Parse(txtChange.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                PaidAmt = decimal.Parse(txtPaidAmt.Text);
                ChangeAmt = decimal.Parse(txtChange.Text);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtChange_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    PaidAmt = decimal.Parse(txtPaidAmt.Text);
                    ChangeAmt = decimal.Parse(txtChange.Text);
                    btnOK.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtChange_TextChanged(object sender, EventArgs e)
        {

        }

        private void picClose1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
