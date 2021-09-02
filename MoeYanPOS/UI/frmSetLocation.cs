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
    public partial class frmSetLocation : Form
    {
        DALLocation dalLocation = new DALLocation();

        public frmSetLocation()
        {
            InitializeComponent();
        }

        private void Load_Location()
        {
            try
            {
                List<BolLocation> LstLocation = new List<BolLocation>();
                LstLocation = dalLocation.GetAllLocation();

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
               int update = 0;
                BolLocation bolLocation = new BolLocation();
                if (cboLocation.SelectedValue != null)
                {
                    bolLocation.ID = long.Parse(cboLocation.SelectedValue.ToString());
                    update = dalLocation.updateIsThisLocation(bolLocation);

                    MessageBox.Show("Set Location is Successfully Updated");
                    if (cboLocation.Items.Count > 0)
                    {
                        cboLocation.SelectedIndex = 0;
                    }
                    frmSetLocation_Load(sender, e);
                    lblLocationID.Text = "0";
                    lblLocationID.Visible = false;
                }
                else
                {
                    MessageBox.Show("Please go to Location Entry and Add locations for System.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmSetLocation_Load(object sender, EventArgs e)
        {
            Load_Location();
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
    }
}
