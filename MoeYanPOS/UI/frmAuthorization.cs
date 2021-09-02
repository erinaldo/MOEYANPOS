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
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using System.Configuration;

namespace MoeYanPOS.UI
{
    public partial class frmAuthorization : Form
    {

        DALUser daluser = new DALUser();
        int userid = 0;
        Boolean saleentry,purchaseentry,salereturn,saleorder,stockajustment,stocktransfer,openingstock,stockbalance,payforcredit,pettycash,salehistory,
        purchasehistory,salereturnhistory,saleorderhistory,stockajustmenthistory,stocktransferhistory,openingstockhistory,stockhistory,payforcredithistory,
        pettycashhistory,user,division,department,township,location,customer,supplier,measurement,brand,class_,category,stock,currency,systemsetting,
        cashreport, salereport, purchasereport, stockreport, customerreport, supplierreport, setlocation, exportnimport,Authorization,
        vouchersetting, stockadjustmenttype, outletcashheader = false;
        
        public frmAuthorization()
        {
            InitializeComponent();
        }

        private void frmAuthorization_Load(object sender, EventArgs e)
        {
            loadUser();
            chkuser.Focus();
        }
        private void loadUser()
        {
            try
            {
                List<BOLUser> lstuser = new List<BOLUser>();
                lstuser = daluser.SelectAllUser();

                BOLUser boluser = new BOLUser();
                boluser.UserID = 0;
                boluser.UserName = "<Select UserName>";
                lstuser.Insert(0, boluser);
                cbouser.DisplayMember = "UserName";
                cbouser.ValueMember = "UserID";
                cbouser.DataSource = lstuser;
                if (lstuser.Count > 0)
                {
                    cbouser.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CheckList()
        {
            userid = Int32.Parse(cbouser.SelectedValue.ToString());
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblid.Text == "0")
                {

                    int issave = 0;
                    BOLAuthorization au = new BOLAuthorization();
                    DALAuthorization dalauthorize = new DALAuthorization();
                    if (cbouser.SelectedIndex != 0)
                    {
                        au.Userid = Int32.Parse(cbouser.SelectedValue.ToString());
                    }
                    if (chksaleentry.Checked)
                    {
                        au.Saleentry = true;
                    }
                    if (chkpurchaseentry.Checked)
                    {
                        au.Purchaseentry = true;
                    }
                    if (chksalereturn.Checked)
                    {
                        au.Salereturn = true;
                    }
                    if (chksaleorder.Checked)
                    {
                        au.Saleorder = true;
                    }
                    if (chkstockajustment.Checked)
                    {
                        au.Stockajustment = true;
                    }
                    if (chkstocktransfer.Checked)
                    {
                        au.Stocktransfer = true;
                    }
                    if (chkopeningstock.Checked)
                    {
                        au.Openingstock = true;
                    }
                    if (chkstockbalance.Checked)
                    {
                        au.Stockbalance = true;
                    }
                    if (chkpayforcredit.Checked)
                    {
                        au.Payforcredit = true;
                    }
                    if (chkpettycash.Checked)
                    {
                        au.Pettycash = true;
                    }
                    if (chksalehistory.Checked)
                    {
                        au.Salehistory = true;
                    }
                    if (chkpurchasehistory.Checked)
                    {
                        au.Purchasehistory = true;
                    }
                    if (chksalereturnhistory.Checked)
                    {
                        au.Salereturnhistory = true;
                    }
                    if (chksaleorderhistory.Checked)
                    {
                        au.Saleorderhistory = true;
                    }
                    if (chkstockajustmenthistory.Checked)
                    {
                        au.Stockajustmenthistory = true;
                    }
                    if (chkstocktransferhistory.Checked)
                    {
                        au.Stocktransferhistory = true;
                    }
                    if (chkopeningstockhistory.Checked)
                    {
                        au.Openingstockhistory = true;
                    }
                    if (chkstockhistory.Checked)
                    {
                        au.Stockhistory = true;
                    }
                    if (chkpayforcredithistory.Checked)
                    {
                        au.Payforcredithistory = true;
                    }
                    if (chkpettycashhistory.Checked)
                    {
                        au.Pettycashhistory = true;
                    }
                    if (chkuser.Checked)
                    {
                        au.User = true;
                    }
                    if (chkdivision.Checked)
                    {
                        au.Division = true;
                    }
                    if (chkdepartment.Checked)
                    {
                        au.Department = true;
                    }
                    if (chktownship.Checked)
                    {
                        au.Township = true;
                    }
                    if (chklocation.Checked)
                    {
                        au.Location = true;
                    }
                    if (chkcustomer.Checked)
                    {
                        au.Customer = true;
                    }
                    if (chksupplier.Checked)
                    {
                        au.Supplier = true;
                    }
                    if (chkmeasurement.Checked)
                    {
                        au.Measurement = true;
                    }
                    if (chkbrand.Checked)
                    {
                        au.Brand = true;
                    }
                    if (chkclass.Checked)
                    {
                        au.Class_ = true;
                    }
                    if (chkcategory.Checked)
                    {
                        au.Category = true;
                    }
                    if (chkstock.Checked)
                    {
                        au.Stock = true;
                    }
                    if (chkcurrency.Checked)
                    {
                        au.Currency = true;
                    }
                    if (chksystemsetting.Checked)
                    {
                        au.Systemsetting = true;
                    }
                    if (chkcashreport.Checked)
                    {
                        au.Cashreport = true;
                    }
                    if (chksalereport.Checked)
                    {
                        au.Salereport = true;
                    }
                    if (chkpurchasereport.Checked)
                    {
                        au.Purchasereport = true;
                    }
                    if (chkstockreport.Checked)
                    {
                        au.Stockreport = true;
                    }
                    if (chkcustomerreport.Checked)
                    {
                        au.Customerreport = true;
                    }
                    if (chksupplierreport.Checked)
                    {
                        au.Supplierreport = true;
                    }
                    if (chksetlocation.Checked)
                    {
                        au.Setlocation = true;
                    }
                    if (chkexpandimp.Checked)
                    {
                        au.Exportnimport = true;
                    }
                    if (chkAuthorization.Checked)
                    {
                        au.Authorization = true;
                    }
                    if (chkVoucherSetting.Checked)
                    {
                        au.VoucherSetting = true;
                    }
                    if (chkStockAdjustmentType.Checked)
                    {
                        au.StockAdjustmentType = true;
                    }
                    if (chkOutletCashHeader.Checked)
                    { 
                        au.OutletCashHeader = true;
                    }

                    //List<BOLAuthorization> bolauthorizelist = new List<BOLAuthorization>();
                    issave = dalauthorize.saveauthorize(au);

                    if (issave == 1)
                    {
                        MessageBox.Show("Successfully Saved");
                    }
                }
                else
                {
                    //update

                    int isupdate = 0;
                    BOLAuthorization au = new BOLAuthorization();
                    DALAuthorization dalauthorize = new DALAuthorization();
                    if (cbouser.SelectedIndex != 0)
                    {
                        au.Userid = Int32.Parse(cbouser.SelectedValue.ToString());
                    }
                    if (chksaleentry.Checked)
                    {
                        au.Saleentry = true;
                    }
                    if (chkpurchaseentry.Checked)
                    {
                        au.Purchaseentry = true;
                    }
                    if (chksalereturn.Checked)
                    {
                        au.Salereturn = true;
                    }
                    if (chksaleorder.Checked)
                    {
                        au.Saleorder = true;
                    }
                    if (chkstockajustment.Checked)
                    {
                        au.Stockajustment = true;
                    }

                    if (chkstocktransfer.Checked)
                    {
                        au.Stocktransfer = true;
                    }
                    if (chkopeningstock.Checked)
                    {
                        au.Openingstock = true;
                    }
                    if (chkstockbalance.Checked)
                    {
                        au.Stockbalance = true;
                    }
                    if (chkpayforcredit.Checked)
                    {
                        au.Payforcredit = true;
                    }
                    if (chkpettycash.Checked)
                    {
                        au.Pettycash = true;
                    }
                    if (chksalehistory.Checked)
                    {
                        au.Salehistory = true;
                    }
                    if (chkpurchasehistory.Checked)
                    {
                        au.Purchasehistory = true;
                    }
                    if (chksalereturnhistory.Checked)
                    {
                        au.Salereturnhistory = true;
                    }
                    if (chksaleorderhistory.Checked)
                    {
                        au.Saleorderhistory = true;
                    }
                    if (chkstockajustmenthistory.Checked)
                    {
                        au.Stockajustmenthistory = true;
                    }
                    if (chkstocktransferhistory.Checked)
                    {
                        au.Stocktransferhistory = true;
                    }
                    if (chkopeningstockhistory.Checked)
                    {
                        au.Openingstockhistory = true;
                    }
                    if (chkstockhistory.Checked)
                    {
                        au.Stockhistory = true;
                    }

                    if (chkpayforcredithistory.Checked)
                    {
                        au.Payforcredithistory = true;
                    }
                    if (chkpettycashhistory.Checked)
                    {
                        au.Pettycashhistory = true;
                    }
                    if (chkuser.Checked)
                    {
                        au.User = true;
                    }
                    if (chkdivision.Checked)
                    {
                        au.Division = true;
                    }
                    if (chkdepartment.Checked)
                    {
                        au.Department = true;
                    }
                    if (chktownship.Checked)
                    {
                        au.Township = true;
                    }
                    if (chklocation.Checked)
                    {
                        au.Location = true;
                    }
                    if (chkcustomer.Checked)
                    {
                        au.Customer = true;
                    }
                    if (chksupplier.Checked)
                    {
                        au.Supplier = true;
                    }
                    if (chkmeasurement.Checked)
                    {
                        au.Measurement = true;
                    }
                    if (chkbrand.Checked)
                    {
                        au.Brand = true;
                    }
                    if (chkclass.Checked)
                    {
                        au.Class_ = true;
                    }
                    if (chkcategory.Checked)
                    {
                        au.Category = true;
                    }
                    if (chkstock.Checked)
                    {
                        au.Stock = true;
                    }
                    if (chkcurrency.Checked)
                    {
                        au.Currency = true;
                    }
                    if (chksystemsetting.Checked)
                    {
                        au.Systemsetting = true;
                    }
                    if (chkcashreport.Checked)
                    {
                        au.Cashreport = true;
                    }
                    if (chksalereport.Checked)
                    {
                        au.Salereport = true;
                    }
                    if (chkpurchasereport.Checked)
                    {
                        au.Purchasereport = true;
                    }
                    if (chkstockreport.Checked)
                    {
                        au.Stockreport = true;
                    }
                    if (chkcustomerreport.Checked)
                    {
                        au.Customerreport = true;
                    }
                    if (chksupplierreport.Checked)
                    {
                        au.Supplierreport = true;
                    }
                    if (chksetlocation.Checked)
                    {
                        au.Setlocation = true;
                    }
                    if (chkexpandimp.Checked)
                    {
                        au.Exportnimport = true;
                    }
                    if (chkAuthorization.Checked)
                    {
                        au.Authorization = true;
                    }
                    if (chkVoucherSetting.Checked)
                    {
                        au.VoucherSetting = true;
                    }
                    if (chkStockAdjustmentType.Checked)
                    {
                        au.StockAdjustmentType = true;
                    }
                    if (chkOutletCashHeader.Checked)
                    {
                        au.OutletCashHeader = true;
                    }

                    //List<BOLAuthorization> bolauthorizelist = new List<BOLAuthorization>();
                    isupdate = dalauthorize.updateauthorize(au);

                    if (isupdate == 1)
                    {
                        MessageBox.Show("Successfully Updated");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cbouser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //int user_id = 0;
                clearall();
                DALAuthorization dalauthorize = new DALAuthorization();
                List<BOLAuthorization> lstauthorize = new List<BOLAuthorization>();
                userid = Int32.Parse(cbouser.SelectedValue.ToString());
                
                lstauthorize = dalauthorize.SelectAllAuth(userid);

                if (lstauthorize.Count > 0)
                {
                    lblid.Text = userid.ToString();
    
                    if (lstauthorize[0].Saleentry)
                    {
                        chksaleentry.Checked = true;
                    }
                    else
                    {
                        chksaleentry.Checked = false;
                    }
                    if (lstauthorize[0].Purchaseentry)
                    {
                        chkpurchaseentry.Checked = true;
                    }
                    else
                    {
                        chkpurchaseentry.Checked = false;
                    }
                    if (lstauthorize[0].Salereturn)
                    {
                        chksalereturn.Checked = true;
                    }
                    else
                    {
                        chksalereturn.Checked = false;
                    }
                    if (lstauthorize[0].Saleorder)
                    {
                        chksaleorder.Checked = true;
                    }
                    else
                    {
                        chksaleorder.Checked = false;
                    }
                    if (lstauthorize[0].Stockajustment)
                    {
                        chkstockajustment.Checked = true;
                    }
                    else
                    {
                        chkstockajustment.Checked = false;
                    }
                    if (lstauthorize[0].Stocktransfer)
                    {
                        chkstocktransfer.Checked = true;
                    }
                    else
                    {
                        chkstocktransfer.Checked = false;
                    }
                    if (lstauthorize[0].Openingstock)
                    {
                        chkopeningstock.Checked = true;
                    }
                    else
                    {
                        chkopeningstock.Checked = false;
                    }
                    if (lstauthorize[0].Stockbalance)
                    {
                        chkstockbalance.Checked = true;
                    }
                    else
                    {
                        chkstockbalance.Checked = false;
                    }
                    if (lstauthorize[0].Payforcredit)
                    {
                        chkpayforcredit.Checked = true;
                    }
                    else
                    {
                        chkpayforcredit.Checked = false;
                    }
                    if (lstauthorize[0].Pettycash)
                    {
                        chkpettycash.Checked = true;
                    }
                    else
                    {
                        chkpettycash.Checked = false;
                    }
                    if (lstauthorize[0].Salehistory)
                    {
                        chksalehistory.Checked = true;
                    }
                    else
                    {
                        chksalehistory.Checked = false;
                    }
                    if (lstauthorize[0].Purchasehistory)
                    {
                        chkpurchasehistory.Checked = true;
                    }
                    else
                    {
                        chkpurchasehistory.Checked = false;
                    }
                    if (lstauthorize[0].Salereturnhistory)
                    {
                        chksalereturnhistory.Checked = true;
                    }
                    else
                    {
                        chksalereturnhistory.Checked = false;
                    }
                    if (lstauthorize[0].Saleorderhistory)
                    {
                        chksaleorderhistory.Checked = true;
                    }
                    else
                    {
                        chksaleorderhistory.Checked = false;
                    }
                    if (lstauthorize[0].Stockajustmenthistory)
                    {
                        chkstockajustmenthistory.Checked = true;
                    }
                    else
                    {
                        chkstockajustmenthistory.Checked = false;
                    }
                    if (lstauthorize[0].Stocktransferhistory)
                    {
                        chkstocktransferhistory.Checked = true;
                    }
                    else
                    {
                        chkstocktransferhistory.Checked = false;
                    }
                    if (lstauthorize[0].Openingstockhistory)
                    {
                        chkopeningstockhistory.Checked = true;
                    }
                    else
                    {
                        chkopeningstockhistory.Checked = false;
                    }
                    if (lstauthorize[0].Stockhistory)
                    {
                        chkstockhistory.Checked = true;
                    }
                    else
                    {
                        chkstockhistory.Checked = false;
                    }
                    if (lstauthorize[0].Payforcredithistory)
                    {
                        chkpayforcredithistory.Checked = true;
                    }
                    else
                    {
                        chkpayforcredithistory.Checked = false;
                    }
                    if (lstauthorize[0].Pettycashhistory)
                    {
                        chkpettycashhistory.Checked = true;
                    }
                    else
                    {
                        chkpettycashhistory.Checked = false;
                    }
                    if (lstauthorize[0].User)
                    {
                        chkuser.Checked = true;
                    }
                    else
                    {
                        chkuser.Checked = false;
                    }
                    if (lstauthorize[0].Division)
                    {
                        chkdivision.Checked = true;
                    }
                    else
                    {
                        chkdivision.Checked = false;
                    }
                    if (lstauthorize[0].Department)
                    {
                        chkdepartment.Checked = true;
                    }
                    else
                    {
                        chkdepartment.Checked = false;
                    }
                    if (lstauthorize[0].Township)
                    {
                        chktownship.Checked = true;
                    }
                    else
                    {
                        chktownship.Checked = false;
                    }
                    if (lstauthorize[0].Location)
                    {
                        chklocation.Checked = true;
                    }
                    else
                    {
                        chklocation.Checked = false;
                    }
                    if (lstauthorize[0].Customer)
                    {
                        chkcustomer.Checked = true;
                    }
                    else
                    {
                        chkcustomer.Checked = false;
                    }
                    if (lstauthorize[0].Supplier)
                    {
                        chksupplier.Checked = true;
                    }
                    else
                    {
                        chksupplier.Checked = false;
                    }
                    if (lstauthorize[0].Measurement)
                    {
                        chkmeasurement.Checked = true;
                    }
                    else
                    {
                        chkmeasurement.Checked = false;
                    }
                    if (lstauthorize[0].Brand)
                    {
                        chkbrand.Checked = true;
                    }
                    else
                    {
                        chkbrand.Checked = false;
                    }
                    if (lstauthorize[0].Class_)
                    {
                        chkclass.Checked = true;
                    }
                    else
                    {
                        chkclass.Checked = false;
                    }
                    if (lstauthorize[0].Category)
                    {
                        chkcategory.Checked = true;
                    }
                    else
                    {
                        chkcategory.Checked = false;
                    }
                    if (lstauthorize[0].Stock)
                    {
                        chkstock.Checked = true;
                    }
                    else
                    {
                        chkstock.Checked = false;
                    }
                    if (lstauthorize[0].Currency)
                    {
                        chkcurrency.Checked = true;
                    }
                    else
                    {
                        chkcurrency.Checked = false;
                    }
                    if (lstauthorize[0].Systemsetting)
                    {
                        chksystemsetting.Checked = true;
                    }
                    else
                    {
                        chksystemsetting.Checked = false;
                    }
                    if (lstauthorize[0].Cashreport)
                    {
                        chkcashreport.Checked = true;
                    }
                    else
                    {
                        chkcashreport.Checked = false;
                    }
                    if (lstauthorize[0].Salereport)
                    {
                        chksalereport.Checked = true;
                    }
                    else
                    {
                        chksalereport.Checked = false;
                    }
                    if (lstauthorize[0].Purchasereport)
                    {
                        chkpurchasereport.Checked = true;
                    }
                    else
                    {
                        chkpurchasereport.Checked = false;
                    }
                    if (lstauthorize[0].Stockreport)
                    {
                        chkstockreport.Checked = true;
                    }
                    else
                    {
                        chkstockreport.Checked = false;
                    }
                    if (lstauthorize[0].Customerreport)
                    {
                        chkcustomerreport.Checked = true;
                    }
                    else
                    {
                        chkcustomerreport.Checked = false;
                    }
                    if (lstauthorize[0].Supplierreport)
                    {
                        chksupplierreport.Checked = true;
                    }
                    else
                    {
                        chksupplierreport.Checked = false;
                    }
                    if (lstauthorize[0].Setlocation)
                    {
                        chksetlocation.Checked = true;
                    }
                    else
                    {
                        chksetlocation.Checked = false;
                    }
                    if (lstauthorize[0].Exportnimport)
                    {
                        chkexpandimp.Checked = true;
                    }
                    else
                    {
                        chkexpandimp.Checked = false;
                    }
                    if (lstauthorize[0].Authorization)
                    {
                        chkAuthorization.Checked = true;
                    }
                    else
                    {
                        chkAuthorization.Checked = false;
                    }
                    if (lstauthorize[0].VoucherSetting)
                    {
                        chkVoucherSetting.Checked = true;
                    }
                    else
                    {
                        chkVoucherSetting.Checked = false;
                    }
                    if (lstauthorize[0].StockAdjustmentType)
                    {
                        chkStockAdjustmentType.Checked = true;
                    }
                    else
                    {
                        chkStockAdjustmentType.Checked = false;
                    }
                    if (lstauthorize[0].OutletCashHeader)
                    {
                        chkOutletCashHeader.Checked = true;
                    }
                    else
                    {
                        chkOutletCashHeader.Checked = false;
                    }
                }
                else
                {
                    lblid.Text = "0";
                }
                
                //List<BOLUser> boluser = new List<BOLUser>();
                //DALUser daluser = new DALUser();
                //userid = Int32.Parse(cbouser.SelectedValue.ToString());
                //boluser = daluser.GetUserByUserID(userid);
                ////cbouser.DisplayMember = "Name";
                ////cbouser.ValueMember = "ID";
                ////cbouser.DataSource = lstauthorize;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void clearall()
        {
            try
            {
                chksaleentry.Checked = false;
                chkpurchaseentry.Checked = false;
                chksalereturn.Checked = false;
                chksaleorder.Checked = false;
                chkstockajustment.Checked = false;
                chkstocktransfer.Checked = false;
                chkopeningstock.Checked = false;
                chkstockbalance.Checked = false;
                chkpayforcredit.Checked = false;
                chkpettycash.Checked = false;
                chksalehistory.Checked = false;
                chkpurchasehistory.Checked = false;
                chksalereturnhistory.Checked = false;
                chksaleorderhistory.Checked = false;
                chkstockajustmenthistory.Checked = false;
                chkstocktransferhistory.Checked = false;
                chkopeningstockhistory.Checked = false;
                chkstockhistory.Checked = false;
                chkpayforcredithistory.Checked = false;
                chkpettycashhistory.Checked = false;
                chkuser.Checked = false;
                chkdivision.Checked = false;
                chkdepartment.Checked = false;
                chktownship.Checked = false;
                chklocation.Checked = false;
                chkcustomer.Checked = false;
                chksupplier.Checked = false;
                chkmeasurement.Checked = false;
                chkbrand.Checked = false;
                chkclass.Checked = false;
                chkcategory.Checked = false;
                chkstock.Checked = false;
                chkcurrency.Checked = false;
                chksystemsetting.Checked = false;
                chkcashreport.Checked = false;
                chksalereport.Checked = false;
                chkpurchasereport.Checked = false;
                chkstockreport.Checked = false;
                chkcustomerreport.Checked = false;
                chksupplierreport.Checked = false;
                chksetlocation.Checked = false;
                chkexpandimp.Checked = false;
                chkAuthorization.Checked = false;
                chkVoucherSetting.Checked = false;
                chkStockAdjustmentType.Checked = false;
                chkOutletCashHeader.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnCheckAll.Text == "Check All")
                {
                    btnCheckAll.Text = "Uncheck All";
                    chksaleentry.Checked = true;
                    chkpurchaseentry.Checked = true;
                    chksalereturn.Checked = true;
                    chksaleorder.Checked = true;
                    chkstockajustment.Checked = true;
                    chkstocktransfer.Checked = true;
                    chkopeningstock.Checked = true;
                    chkstockbalance.Checked = true;
                    chkpayforcredit.Checked = true;
                    chkpettycash.Checked = true;
                    chksalehistory.Checked = true;
                    chkpurchasehistory.Checked = true;
                    chksalereturnhistory.Checked = true;
                    chksaleorderhistory.Checked = true;
                    chkstockajustmenthistory.Checked = true;
                    chkstocktransferhistory.Checked = true;
                    chkopeningstockhistory.Checked = true;
                    chkstockhistory.Checked = true;
                    chkpayforcredithistory.Checked = true;
                    chkpettycashhistory.Checked = true;
                    chkuser.Checked = true;
                    chkdivision.Checked = true;
                    chkdepartment.Checked = true;
                    chktownship.Checked = true;
                    chklocation.Checked = true;
                    chkcustomer.Checked = true;
                    chksupplier.Checked = true;
                    chkmeasurement.Checked = true;
                    chkbrand.Checked = true;
                    chkclass.Checked = true;
                    chkcategory.Checked = true;
                    chkstock.Checked = true;
                    chkcurrency.Checked = true;
                    chksystemsetting.Checked = true;
                    chkcashreport.Checked = true;
                    chksalereport.Checked = true;
                    chkpurchasereport.Checked = true;
                    chkstockreport.Checked = true;
                    chkcustomerreport.Checked = true;
                    chksupplierreport.Checked = true;
                    chksetlocation.Checked = true;
                    chkexpandimp.Checked = true;
                    chkAuthorization.Checked = true;
                    chkVoucherSetting.Checked = true;
                    chkStockAdjustmentType.Checked = true;
                    chkOutletCashHeader.Checked = true;
                }
                else
                {
                    btnCheckAll.Text = "Check All";
                    chksaleentry.Checked = false;
                    chkpurchaseentry.Checked = false;
                    chksalereturn.Checked = false;
                    chksaleorder.Checked = false;
                    chkstockajustment.Checked = false;
                    chkstocktransfer.Checked = false;
                    chkopeningstock.Checked = false;
                    chkstockbalance.Checked = false;
                    chkpayforcredit.Checked = false;
                    chkpettycash.Checked = false;
                    chksalehistory.Checked = false;
                    chkpurchasehistory.Checked = false;
                    chksalereturnhistory.Checked = false;
                    chksaleorderhistory.Checked = false;
                    chkstockajustmenthistory.Checked = false;
                    chkstocktransferhistory.Checked = false;
                    chkopeningstockhistory.Checked = false;
                    chkstockhistory.Checked = false;
                    chkpayforcredithistory.Checked = false;
                    chkpettycashhistory.Checked = false;
                    chkuser.Checked = false;
                    chkdivision.Checked = false;
                    chkdepartment.Checked = false;
                    chktownship.Checked = false;
                    chklocation.Checked = false;
                    chkcustomer.Checked = false;
                    chksupplier.Checked = false;
                    chkmeasurement.Checked = false;
                    chkbrand.Checked = false;
                    chkclass.Checked = false;
                    chkcategory.Checked = false;
                    chkstock.Checked = false;
                    chkcurrency.Checked = false;
                    chksystemsetting.Checked = false;
                    chkcashreport.Checked = false;
                    chksalereport.Checked = false;
                    chkpurchasereport.Checked = false;
                    chkstockreport.Checked = false;
                    chkcustomerreport.Checked = false;
                    chksupplierreport.Checked = false;
                    chksetlocation.Checked = false;
                    chkexpandimp.Checked = false;
                    chkAuthorization.Checked = false;
                    chkVoucherSetting.Checked = false;
                    chkStockAdjustmentType.Checked = false;
                    chkOutletCashHeader.Checked = false;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
