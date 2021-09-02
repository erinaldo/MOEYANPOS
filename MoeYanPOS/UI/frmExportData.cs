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
using System.IO;

namespace MoeYanPOS.UI
{
    public partial class frmExportData : Form
    {
        DALExportData dalexportdata = new DALExportData();
        BOLExportData bolexportdata = new BOLExportData();
        DALUser user = new DALUser();

        public frmExportData()
        {
            InitializeComponent();

            txtFilePath.Text = @"D:\MoeYanPOSDataBackup\";

            dtpFromDate.Value = DateTime.Now;
            dtpToDate.Value = DateTime.Now;

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileSelectPopUp = new OpenFileDialog();
            fileSelectPopUp.Title = "";
            fileSelectPopUp.InitialDirectory = @"D:\MoeYanPOSDataBackup\";
            //fileSelectPopUp.Filter = "All BackUp FILES (*.bak*)|All files (*.*)|*.*";
            fileSelectPopUp.FilterIndex = 2;
            fileSelectPopUp.RestoreDirectory = true;
            if (fileSelectPopUp.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = fileSelectPopUp.FileName;
            }
        }

        private void frmExportData_Load(object sender, EventArgs e)
        {

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

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                BOLExportData bolexportdata = new BOLExportData();
                bolexportdata.DtpFromDate = dtpFromDate.Value;
                bolexportdata.DtpToDate = dtpToDate.Value;
                
                dalexportdata.ExportData(bolexportdata);

                if (File.Exists(@"D:\MoeYanPOSDataBackup"))
                {
                    if (File.Exists(@"D:\MoeYanPOSDataBackup\MoeYanPOSExportData"))
                    {
                        File.Delete(@"D:\MoeYanPOSDataBackup\MoeYanPOSExportData");
                    }
                }
                else
                {
                    System.IO.Directory.CreateDirectory(@"D:\MoeYanPOSDataBackup");
                }


                user.BackupExport();

                MessageBox.Show(" Export Data Successful.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
