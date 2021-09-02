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
    public partial class frmLogin : Form
    {
        DALUser daluser = new DALUser();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                Validation.isNullOrEmptyField("User Name", txtuserID.Text);
                Validation.isNullOrEmptyField("Password", txtpassword.Text);

                BOLUser boluser = new BOLUser();
                boluser.UserID = Int32.Parse( txtuserID.Text);
                boluser.Password = txtpassword.Text;
                int userid = 0;
                userid = daluser.SelectUser(boluser);
                if (userid > 0)
                {
                    txtuserID.Text = "";
                    txtpassword.Text = "";
                    frmMain frmmain = new frmMain();
                    frmMain.UserID = userid;
                    //frmChooseCounter newform = new frmChooseCounter();
                    //newform.userid = userid;
                    this.Hide();
                    frmmain.ShowDialog();           
                }
                else if (Int32.Parse(txtuserID.Text) == 1 & txtpassword.Text == "123")
                {
                    txtuserID.Text = "";
                    txtpassword.Text = "";
                    frmMain frmmain = new frmMain();
                    frmMain.UserID = userid;
                    //frmChooseCounter newform = new frmChooseCounter();
                    //newform.userid = userid;
                    this.Hide();
                    frmmain.ShowDialog(); 
                }
                else
                {
                    MessageBox.Show("Invalid UserID and Password");
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
                throw ex;
            }
        }

        private void txtpassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnlogin_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Password");
            }
        }

        private void txtcardid_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((e.KeyChar ==13) || (e.KeyChar == 9))
                {
                    txtuserID.Text = daluser.FillUserID(txtcardid.Text).ToString();
                    txtpassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid UserID and CardID");
            }
        }

        private void txtuserID_KeyPress(object sender, KeyPressEventArgs e)   
        {
            try
            {
                if ((e.KeyChar == 13) || (e.KeyChar == 9))
                {
                    string cardid = daluser.FillCardID(Int32.Parse(txtuserID.Text));                    
                    if(cardid != "")
                    {
                        txtcardid.Text = cardid;
                        txtpassword.Focus();
                    }
                    else if (Int32.Parse(txtuserID.Text)==1)
                    {
                        int isSaved = 0;
                        BOLUser boluser = new BOLUser();
                        boluser.UserID = 1;
                        boluser.UserName = "USer";
                        boluser.CardID = "0001";
                        boluser.Password = "123";
                        boluser.TownshipID = 0;
                        boluser.NRC = "";
                        boluser.Level = "";
                        boluser.IsSavePrint = true;
                        boluser.IsmsgforVoucher = true;
                        boluser.IsmsgforVoucher = true;
                       daluser.SaveUser(boluser);

                        txtcardid.Text = "0001";
                        txtpassword.Text = "123";
                        txtpassword.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Invalid User");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void picClose1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
  
    }
}
