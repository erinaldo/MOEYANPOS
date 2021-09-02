using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MoeYanPOS.Function;
using MoeYanPOS.BOL;
using MoeYanPOS.DAL;

namespace MoeYanPOS.UI
{
    public partial class frmCounter : Form
    {
        BOLCounter bolLocation = new BOLCounter();
        DALCounter dalcounter = new DALCounter();
        int issaved = 0;
        public frmCounter()
        {
            InitializeComponent();
        }

        private void CounterTab_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "&Update" & txtCode.Text != "" & txtCode.Text != " ")
                {
                    int update = 0;
                    bolLocation.Code = txtCode.Text; 
                    bolLocation.Name = txtName.Text;

                    update = dalcounter.updateCounter(bolLocation);

                    if (update == 1)
                    {
                        MessageBox.Show("Counter is Successfully Updated");
                        txtName.Text = "";
                        //tabBrand.SelectedIndex = 1;
                        btnSave.Text = "&Save";
                        frmCounter_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("This Record is Already Exist!");
                        txtName.Focus();
                        txtName.SelectAll();
                    }
                }
                if (btnSave.Text == "&Save" & txtCode.Text != "" & txtCode.Text != " ")
                {
                    bolLocation.Code = txtCode.Text;
                    bolLocation.Name = txtName.Text;

                    issaved = dalcounter.SaveCounter(bolLocation);
                    if (issaved == 1)
                    {
                        MessageBox.Show("Counter is Successfully Saved");
                        txtName.Text = "";
                        //tabBrand.SelectedIndex = 1;
                        btnSave.Text = "&Save";
                        frmCounter_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("This Record is Already Exist!");
                        txtName.Focus();
                        txtName.SelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtCode.Enabled = true;
            btnSave.Text = "&Save";
        }

        private void frmCounter_Load(object sender, EventArgs e)
        {
            try
            {
                txtCode.Text = "";
                txtName.Text = "";
                txtCode.Enabled = true;

                dgvCounter.Rows.Clear();
                List<BOLCounter> lstcurrency = new List<BOLCounter>();
                lstcurrency = dalcounter.SelectAllCounter();

                foreach (BOLCounter cu in lstcurrency)
                {
                    dgvCounter.Rows.Add(cu.Id, cu.Code, cu.Name);
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

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvCounter_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try{
                if (e.ColumnIndex == 3)
                {
                    if (e.RowIndex >= 0)
                    {
                        int currencyid = 0;
                        currencyid = Int32.Parse(dgvCounter.Rows[e.RowIndex].Cells[0].Value.ToString());
                        CounterTab.SelectedIndex = 0;

                        txtCode.Text = dgvCounter.Rows[e.RowIndex].Cells[1].Value.ToString();
                        txtName.Text = dgvCounter.Rows[e.RowIndex].Cells[2].Value.ToString();
                        txtCode.Enabled = false;
                        btnSave.Text = "&Update";
                    }
                }
                if (e.ColumnIndex == 4)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string code = "";
                            code = dgvCounter.Rows[e.RowIndex].Cells[1].Value.ToString();
                            int isdelete = 0;
                            isdelete = dalcounter.DeleteCounter(code);
                            if (isdelete == -1)
                            {
                                MessageBox.Show("Successfully Deleted!");
                                frmCounter_Load(sender, e);
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
    }
}
