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

namespace MoeYanPOS
{
    public partial class frmUser : Form
    {
        BOLUser boluser;
        DALUser daluser = new DALUser();
        UI.frmLogin login = new UI.frmLogin();
        DALTownship daltownship = new DALTownship();
        DALAuthorization dalauthorization = new DALAuthorization();
        DALStaff dalstaff = new DALStaff();

        public frmUser()
        {
            try
            {
                InitializeComponent();
                lbluserid.Text = daluser.GetUserID().ToString();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (Validation.isNullOrEmptyField(" User Name", txtName.Text) != "")
                {
                    lblname.Text = Validation.isNullOrEmptyField(" User Name ", txtName.Text);
                    lblname.Visible = true;
                }
                else
                {
                    lblname.Visible = false;
                }
                if (Validation.isNullOrEmptyField(" Password", txtPassword.Text) != "")
                {
                    lblpassword.Text = Validation.isNullOrEmptyField(" Password ", txtPassword.Text);
                    lblpassword.Visible = true;
                }
                else
                {
                    lblpassword.Visible = false;
                }
                if (Validation.isNullOrEmptyField(" Card ID ", txtCardID.Text) != "")
                {
                    lblcardid.Text = Validation.isNullOrEmptyField(" Card ID ", txtCardID.Text);
                    lblcardid.Visible = true;
                }
                else
                {
                    lblcardid.Visible = false;
                }

                if (Validation.isNullOrEmptyField(" NRC ", txtNRC.Text) != "")
                {
                    lblnrc.Text = Validation.isNullOrEmptyField(" NRC ", txtNRC.Text);
                    lblnrc.Visible = true;
                }
                else
                {
                    lblnrc.Visible = false;
                }
                if (Validation.isNullOrEmptyField(" Level ", txtlevel.Text) != "")
                {
                    lbllevel.Text = Validation.isNullOrEmptyField(" Level ", txtlevel.Text);
                    lbllevel.Visible = true;
                }
                else
                {
                    lbllevel.Visible = false;
                }
                if (btnSave.Text == "Update" & txtName.Text != "" & txtPassword.Text != "" & txtCardID.Text != "" & txtNRC.Text != "" & txtlevel.Text != "")
                {
                    BOLUser boluserupdate = new BOLUser();
                    int isupdated = 0;
                    boluserupdate.UserID = Int32.Parse(lbluserid.Text);
                    boluserupdate.UserName = txtName.Text;
                    boluserupdate.CardID = txtCardID.Text;
                    boluserupdate.NRC = txtNRC.Text;
                    boluserupdate.Password = txtPassword.Text;
                    boluserupdate.TownshipID = Int32.Parse(cboTownship.SelectedValue.ToString());
                    boluserupdate.Level = txtlevel.Text;
                    boluserupdate.IsSavePrint = chkIsSavePrint.Checked;
                    boluserupdate.IsmsgforVoucher = chkMsg.Checked;
                    boluserupdate.StaffID = cboStaffName.SelectedValue.ToString();

                    isupdated = daluser.updateUserByUserID(boluserupdate);

                    if (isupdated == 1)
                    {
                        MessageBox.Show("User is Successfully Updated");
                        btnSave.Text = "&Save";
                        CleanTextBox();
                        //tabUser.SelectedIndex = 1;
                        Form1_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("User is Already Exist");
                        txtName.Focus();
                        txtName.SelectAll();
                    }
                }
                if (btnSave.Text == "&Save" & txtName.Text != "" & txtPassword.Text != "" & txtCardID.Text != "" & txtNRC.Text != "" & txtlevel.Text != "")
                {
                    int isSaved = 0;
                    boluser = new BOLUser();
                    boluser.UserID = Int32.Parse(lbluserid.Text);
                    boluser.UserName = txtName.Text;
                    boluser.CardID = txtCardID.Text;
                    boluser.Password = txtPassword.Text;
                    boluser.TownshipID = Int32.Parse(cboTownship.SelectedValue.ToString());
                    boluser.NRC = txtNRC.Text;
                    boluser.Level = txtlevel.Text;
                    boluser.StaffID = cboStaffName.SelectedValue.ToString();

                    if (chkIsSavePrint.Checked)
                    {
                        boluser.IsSavePrint = true;
                    }
                    else
                    {
                        boluser.IsSavePrint = true;
                    }
                    if (chkMsg.Checked)
                    {
                        boluser.IsmsgforVoucher = true;
                    }
                    else
                    {
                        boluser.IsmsgforVoucher = true;
                    }

                    isSaved = daluser.SaveUser(boluser);

                    BOLAuthorization bolauthorization = new BOLAuthorization();
                    bolauthorization.Userid = Int32.Parse(lbluserid.Text);
                    isSaved = dalauthorization.saveauthorize(bolauthorization);

                    if (isSaved == 1)
                    {
                        MessageBox.Show("User is successfully saved.");
                        CleanTextBox();
                        lbluserid.Text = daluser.GetUserID().ToString();
                        // tabUser.SelectedIndex = 1;
                    }
                    else
                    {
                        MessageBox.Show("User Is Already Exist ");
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

        private void CleanTextBox()
        {
            try
            {

                txtName.Text = "";
                txtCardID.Text = "";
                txtPassword.Text = "";
                txtNRC.Text = "";
                txtlevel.Text = "";
                chkIsSavePrint.Checked = false;
                chkMsg.Checked = false;
                lblcardid.Text = "";
                lbllevel.Text = "";
                lblname.Text = "";
                lblnrc.Text = "";
                lblpassword.Text = "";
                lbltownship.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {

                List<BOLTownship> lstTownship = new List<BOLTownship>();
                lstTownship = daltownship.GetAllTownship();
                cboTownship.DisplayMember = "Township";
                cboTownship.ValueMember = "ID";
                cboTownship.DataSource = lstTownship;

                List<BOLStaff> lstStaff = new List<BOLStaff>();
                lstStaff = dalstaff.ShowAllStaff();
                cboStaffName.DisplayMember = "StaffName";
                cboStaffName.ValueMember = "StaffID";
                cboStaffName.DataSource = lstStaff;

                dgvuser.Rows.Clear();
                List<BOLUser> lstuser = new List<BOLUser>();
                lstuser = daluser.SelectAllUser();

                foreach (BOLUser u in lstuser)
                {
                    //if (u.UserID == frmMain.UserID)
                    //{
                    dgvuser.Rows.Add(u.UserID, u.UserName, u.CardID, u.Password, u.Township, u.NRC, u.Level,u.StaffName, "", "", u.IsSavePrint, u.IsmsgforVoucher);
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgvuser_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.ColumnIndex == 8)
                {
                    if (e.RowIndex >= 0)
                    {
                        int userid = 0;
                        userid = Int32.Parse(dgvuser.Rows[e.RowIndex].Cells[0].Value.ToString());
                        tabUser.SelectedIndex = 0;

                        lbluserid.Text = dgvuser.Rows[e.RowIndex].Cells[0].Value.ToString();
                        txtName.Text = dgvuser.Rows[e.RowIndex].Cells[1].Value.ToString();
                        txtCardID.Text = dgvuser.Rows[e.RowIndex].Cells[2].Value.ToString();
                        txtPassword.Text = dgvuser.Rows[e.RowIndex].Cells[3].Value.ToString();
                        cboTownship.Text = dgvuser.Rows[e.RowIndex].Cells[4].Value.ToString();
                        txtNRC.Text = dgvuser.Rows[e.RowIndex].Cells[5].Value.ToString();
                        txtlevel.Text = dgvuser.Rows[e.RowIndex].Cells[6].Value.ToString();
                        cboStaffName.Text = dgvuser.Rows[e.RowIndex].Cells[7].ToString();

                        if (dgvuser.Rows[e.RowIndex].Cells[9].Value.ToString() == "True")
                        {
                            chkIsSavePrint.Checked = true;
                        }
                        else
                        {
                            chkIsSavePrint.Checked = false;
                        }

                        if (dgvuser.Rows[e.RowIndex].Cells[10].Value.ToString() == "True")
                        {
                            chkMsg.Checked = true;
                        }
                        else
                        {
                            chkMsg.Checked = false;
                        }


                        btnSave.Text = "Update";
                    }
                }
                if (e.ColumnIndex == 9)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int userid = 0;
                            userid = Int32.Parse(dgvuser.Rows[e.RowIndex].Cells[0].Value.ToString());
                            int isdelete = 0;
                            isdelete = daluser.DeleteUser(userid);
                            if (isdelete == 1)
                            {
                                MessageBox.Show("Successfully deleted!");
                                Form1_Load(sender, e);
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

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DALUser user = new DALUser();
                    List<BOLUser> lst = new List<BOLUser>();
                    lst = user.SelectUser(txtName.Text);

                    if (lst.Count > 0)
                    {
                        lblname.Text = "This User Name is already Exist";
                        lblname.Visible = true;
                        txtName.SelectAll();
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
                CleanTextBox();
                btnSave.Text = "&Save";
                lbluserid.Text = daluser.GetUserID().ToString();
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

    }
}
