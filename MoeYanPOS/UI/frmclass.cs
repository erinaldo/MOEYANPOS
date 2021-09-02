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
    public partial class frmclass : Form
    {
        DALClass dalclass = new DALClass();
        BOLClass bolclass = new BOLClass();
        public frmclass()
        {
            InitializeComponent();
            lblid.Text = dalclass.GetClassID().ToString();
        }

        private void frmclass_Load(object sender, EventArgs e)
        {
            try
            {
                dgvclass.Rows.Clear();
                List<BOLClass> lstclass = new List<BOLClass>();
                lstclass = dalclass.SelectAllClass();

                foreach (BOLClass c in lstclass)
                {
                    dgvclass.Rows.Add(c.Id, c.ClassName,c.MBCClassID);
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
                if (Validation.isNullOrEmptyField(" Class Name ", txtclassname.Text) != "")
                {
                    lblError.Text = Validation.isNullOrEmptyField(" Class Name ", txtclassname.Text);
                    lblError.Visible = true;
                }
                if (Validation.isNullOrEmptyField(" MBC Class ID ", txtMBCClassID.Text) != "")
                {
                    lblMBCClassID.Text = Validation.isNullOrEmptyField(" MBC Class ID ", txtMBCClassID.Text);
                    lblMBCClassID.Visible = true;
                }
                if (btnsave.Text == "Update")
                {
                    BOLClass bolclassupdate = new BOLClass();
                    bolclassupdate.Id = Int32.Parse(lblid.Text);
                    bolclassupdate.ClassName = txtclassname.Text;
                    bolclass.MBCClassID = txtMBCClassID.Text;

                    dalclass.updateClassByClassID(bolclassupdate);

                    MessageBox.Show(" Class is Successfully Update");
                    txtclassname.Text = "";
                    txtMBCClassID.Text = "";
                    tabclass.SelectedIndex = 1;
                    frmclass_Load(sender, e);
                }
                else
                {
                    int issaved = 0;
                    bolclass = new BOLClass();
                    bolclass.ClassName = txtclassname.Text;
                    bolclass.MBCClassID = txtMBCClassID.Text;
                    issaved = dalclass.SaveClass(bolclass);

                    if (issaved == 1)
                    {
                        MessageBox.Show("Class Name is Successfully Saved");
                        txtclassname.Text = "";
                        txtMBCClassID.Text = "";
                        lblid.Text = dalclass.GetClassID().ToString();
                        lblError.Visible = false;
                        lblMBCClassID.Visible = false;
                    }
                    
                }
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

        private void dgvclass_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    if (e.RowIndex >= 0)
                    {
                        int classid = 0; string MBCClassID = "";
                        classid = Int32.Parse(dgvclass.Rows[e.RowIndex].Cells[0].Value.ToString());
                        MBCClassID = dgvclass.Rows[e.RowIndex].Cells[2].Value.ToString();

                        tabclass.SelectedIndex = 0;

                        lblid.Text=dgvclass.Rows[e.RowIndex].Cells[0].Value.ToString();
                        txtclassname.Text=dgvclass.Rows[e.RowIndex].Cells[1].Value.ToString();
                        txtMBCClassID.Text = dgvclass.Rows[e.RowIndex].Cells[2].Value.ToString();

                        btnsave.Text="Update";
                        lblError.Visible = false;
                        lblMBCClassID.Visible = false;
                    }
                }
                if (e.ColumnIndex == 4)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int classid = 0; string MBCClassID = "";
                            classid = Int32.Parse(dgvclass.Rows[e.RowIndex].Cells[0].Value.ToString());
                            MBCClassID = dgvclass.Rows[e.RowIndex].Cells[2].Value.ToString();
                            int isdelete = 0;
                            isdelete = dalclass.DeleteClass(classid);
                            if (isdelete == 1)
                            {
                                MessageBox.Show("Successfully Deleted!");
                                frmclass_Load(sender, e);
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
