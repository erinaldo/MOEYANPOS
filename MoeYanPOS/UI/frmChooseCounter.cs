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
    public partial class frmChooseCounter : Form
    {
        BOLCounter bolLocation = new BOLCounter();
        DALCounter dalcounter = new DALCounter();
        DALUser daluser = new DALUser();
        public int userid = 0;

        public frmChooseCounter()
        {
            InitializeComponent();
        }

        private void frmChooseCounter_Load(object sender, EventArgs e)
        {
            dgvCounter.Rows.Clear();
            List<BOLCounter> lstcurrency = new List<BOLCounter>();
            lstcurrency = dalcounter.SelectAllCounter();
            int i = 1;
            foreach (BOLCounter cu in lstcurrency)
            {
                dgvCounter.Rows.Add(i++,cu.Code,cu.Name,!cu.IsthisLocation);
            }
            if (dgvCounter.Rows.Count > 0)
            { 
            dgvCounter.CurrentCell = dgvCounter.Rows[0].Cells[4];
            }
        }

        private void picClose1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmChooseCounter_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void dgvCounter_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                BOLCounter blocounter = new BOLCounter();
                string code = dgvCounter.CurrentRow.Cells[1].Value.ToString();
                bool status = dalcounter.CheckCounterStatus(code);
                if (status == true)
                {
                    MessageBox.Show("Choose another counter!", "Counter is already used!");
                }
                else 
                {
                    MoeYanFunctions.MoeYanPOS_Helper.counterCode = dgvCounter.CurrentRow.Cells[1].Value.ToString();
                    MoeYanFunctions.MoeYanPOS_Helper.counterName = dgvCounter.CurrentRow.Cells[2].Value.ToString();
                    blocounter.Code = code;
                    blocounter.Name = dgvCounter.CurrentRow.Cells[2].Value.ToString();
                    blocounter.IsthisLocation = true;
                    blocounter.IsDelete = false;
                    dalcounter.updateCounter(blocounter);

                    frmPOS frmmain = new frmPOS();
                    //frmMain.UserID = userid;
                    this.Hide();
                    //frmmain.ShowDialog();
                    frmmain.Show();
                }
            }
        }

        private void dgvCounter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                    BOLCounter blocounter = new BOLCounter();
                    string code = dgvCounter.CurrentRow.Cells[1].Value.ToString();
                    bool status = dalcounter.CheckCounterStatus(code);
                    if (status == true)
                    {
                        MessageBox.Show("Choose another counter!", "Counter is already used!");
                    }
                    else
                    {
                        MoeYanFunctions.MoeYanPOS_Helper.counterCode = dgvCounter.CurrentRow.Cells[1].Value.ToString();
                        MoeYanFunctions.MoeYanPOS_Helper.counterName = dgvCounter.CurrentRow.Cells[2].Value.ToString();
                        blocounter.Code = code;
                        blocounter.Name = dgvCounter.CurrentRow.Cells[2].Value.ToString();
                        blocounter.IsthisLocation = true;
                        blocounter.IsDelete = false;
                        dalcounter.updateCounter(blocounter);

                        frmPOS frmmain = new frmPOS();
                        //frmMain.UserID = userid;
                        this.Hide();
                        //frmmain.ShowDialog();
                        frmmain.Show();
                    }
            }
        }
    }
}
