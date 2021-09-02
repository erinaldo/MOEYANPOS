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
using System.IO;
using MoeYanPOS.Function;

namespace MoeYanPOS.UI
{
    public partial class frmVoucherSetting : Form
    {
        DALVoucherSetting dalvoucher = new DALVoucherSetting();
        BOLVoucherSetting bolvoucher = new BOLVoucherSetting();
        string imagename; String path; byte[] logoupdate;
        public frmVoucherSetting()
        {
            InitializeComponent();
            lblid.Text = dalvoucher.GetVoucherSettingID().ToString();
        }
        private void LoadVoucher()
        {
            try
            {
                List<BOLVoucherSetting> lstvoucher = new List<BOLVoucherSetting>();
                lstvoucher = dalvoucher.SelectAllVoucher();
                dgvvoucher.Rows.Clear();
                if (lstvoucher.Count > 0)
                {
                    for (int i = 0; i < lstvoucher.Count; i++)
                    {
                        dgvvoucher.Rows.Add(lstvoucher[i].Id, lstvoucher[i].Name, lstvoucher[i].Address, lstvoucher[i].Phone, lstvoucher[i].Message, lstvoucher[i].Logo);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void frmVoucherSetting_Load(object sender, EventArgs e)
        {
            try
            {
                LoadVoucher();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnbrowse_Click(object sender, EventArgs e)
        {

            try
            {
                FileDialog fldlg = new OpenFileDialog();
                fldlg.InitialDirectory = @":D\";
                fldlg.Filter = "Image File(*.jpg;*.jpeg;*.bmp;*.gif)|*.jpg;*.jpeg;*.bmp;*.gif";

                if (fldlg.ShowDialog() == DialogResult.OK)
                {
                    path = fldlg.FileName.ToString();
                    txtlogo.Text = path;
                    pictureBox1.ImageLocation = path;

                    if (txtlogo.Text !="")
                    {
                        logoupdate = System.IO.File.ReadAllBytes(txtlogo.Text); 
                    }
                }
            }
            catch (System.ArgumentException ae)
            {
                MessageBox.Show(ae.Message.ToString());
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
                if (Validation.isNullOrEmptyField(" Name ", txtname.Text) != "")
                {
                    lblname.Text = Validation.isNullOrEmptyField(" Name ", txtname.Text);
                    lblname.Visible = true;
                }
                else
                {
                    lblname.Visible = false;
                }
                if (Validation.isNullOrEmptyField(" Address ", txtAddress.Text) != "")
                {
                    lbladdress.Text = Validation.isNullOrEmptyField(" Address ", txtAddress.Text);
                    lbladdress.Visible = true;
                }
                else
                {
                    lbladdress.Visible = false;
                }
                if (Validation.isNullOrEmptyField(" Phone ", txtphone.Text) != "")
                {
                    lblphone.Text = Validation.isNullOrEmptyField(" Phone ", txtphone.Text);
                    lblphone.Visible = true;
                }
                else
                {
                    lblphone.Visible = false;
                }
                if (Validation.isNullOrEmptyField(" Message ", txtmessage.Text) != "")
                {
                    lblMessage.Text = Validation.isNullOrEmptyField(" Message ", txtmessage.Text);
                    lblMessage.Visible = true;
                }
                else
                {
                    lblMessage.Visible = false;
                }
                if (btnsave.Text == "&Update" & txtname.Text!="" & txtAddress.Text!="" & txtphone.Text!="" & txtmessage.Text!="")
                {
                    int isupdate = 0;
                    BOLVoucherSetting bolvoucher = new BOLVoucherSetting();
                    bolvoucher.Id = Int32.Parse(lblid.Text);
                    bolvoucher.Name = txtname.Text;
                    bolvoucher.Address = txtAddress.Text;
                    bolvoucher.Phone = txtphone.Text;
                    bolvoucher.Message = txtmessage.Text;

                    bolvoucher.Logo = logoupdate;
                    
                    isupdate = dalvoucher.UpdateVoucherSetting(bolvoucher);

                    if (isupdate == 1)
                    {
                        MessageBox.Show(" Voucher Setting is Successfully Updated ");
                        //tabvoucher.SelectedIndex = 1;
                        btnsave.Text = "&Save";
                        frmVoucherSetting_Load(sender, e);
                        lblid.Text = dalvoucher.GetVoucherSettingID().ToString();
                    }
                }
                else if (btnsave.Text == "&Save" & txtname.Text != "" & txtAddress.Text != "" & txtphone.Text != "" & txtmessage.Text != "")
                {
                    int issaved = 0;
                    bolvoucher = new BOLVoucherSetting();
                    bolvoucher.Name = txtname.Text;
                    bolvoucher.Address = txtAddress.Text;
                    bolvoucher.Phone = txtphone.Text;
                    bolvoucher.Message = txtmessage.Text;
                    bolvoucher.Logo = System.IO.File.ReadAllBytes(path);

                    int chkVoucherSetting = 1;

                    chkVoucherSetting = dalvoucher.CheckVoucherSetting();

                    if (chkVoucherSetting == 0)
                    {
                        issaved = dalvoucher.SaveVoucherSetting(bolvoucher);

                        if (issaved == 1)
                        {
                            MessageBox.Show(" Voucher Setting is Successfully Saved ");

                        }
                        else
                        {
                            MessageBox.Show(" Voucher Setting is Already Exist ");
                        }
                    }

                    else
                    {
                        MessageBox.Show("Only one Record can Save.");
                    }
                                      
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvvoucher_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 6)
                {
                    if (e.RowIndex >= 0)
                    {
                        int voucherid = 0;
                        voucherid = Int32.Parse(dgvvoucher.Rows[e.RowIndex].Cells[0].Value.ToString());
                        tabvoucher.SelectedIndex = 0;

                        lblid.Text = dgvvoucher.Rows[e.RowIndex].Cells[0].Value.ToString();
                        txtname.Text = dgvvoucher.Rows[e.RowIndex].Cells[1].Value.ToString();
                        txtAddress.Text = dgvvoucher.Rows[e.RowIndex].Cells[2].Value.ToString();
                        txtphone.Text = dgvvoucher.Rows[e.RowIndex].Cells[3].Value.ToString();
                        txtmessage.Text = dgvvoucher.Rows[e.RowIndex].Cells[4].Value.ToString();
                        pictureBox1.Image = null;
                        if (dgvvoucher.Rows[e.RowIndex].Cells[5].Value != System.DBNull.Value)
                        {
                            byte[] photoarray = (byte[])dgvvoucher.Rows[e.RowIndex].Cells[5].Value;
                            MemoryStream ms = new MemoryStream(photoarray);
                            pictureBox1.Image = Image.FromStream(ms);

                            logoupdate = (byte[])dgvvoucher.Rows[e.RowIndex].Cells[5].Value;
                        }
                        txtlogo.Text = pictureBox1.ImageLocation;
                        btnsave.Text = "&Update";
                    }
                }
                if (e.ColumnIndex == 7)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (MessageBox.Show("Are you sure want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int voucherid = 0;
                            voucherid = Int32.Parse(dgvvoucher.Rows[e.RowIndex].Cells[0].Value.ToString());
                            int isdelete = 0;
                            isdelete = dalvoucher.DeleteVoucher(voucherid);

                            if (isdelete == 1)
                            {
                                MessageBox.Show("Successfully Deleted!");
                                frmVoucherSetting_Load(sender, e);
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
        private void CleanTextBox()
        {
            txtname.Text = "";
            txtphone.Text = "";
            txtAddress.Text = "";
            txtlogo.Text = "";
            txtmessage.Text = "";
            pictureBox1.Image =null;
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

        private void btnclear_Click(object sender, EventArgs e)
        {
            try
            {
                CleanTextBox();
                btnsave.Text = "&Save";
                lblid.Text = dalvoucher.GetVoucherSettingID().ToString();
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

        private void lblname_Click(object sender, EventArgs e)
        {

        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
