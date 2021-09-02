using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MoeYanPOS.DAL;
using MoeYanPOS.BOL;
using MoeYanPOS.UI;
using System.Data.SqlClient;
using MoeYanPOS.Function;

namespace MoeYanPOS
{
    public partial class frmMain : Form
    {
        DALUser user = new DALUser();
        DALAuthorization dalAuth = new DALAuthorization();
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr = MoeYanConfiguration.GetConnection();

        public static int UserID;
        public frmMain()
        {
            InitializeComponent();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnucustomer_Click(object sender, EventArgs e)
        {
            frmCustomer customer = new frmCustomer();
            customer.Show();
        }

        

        private void mnuuser_Click(object sender, EventArgs e)
        {
            frmUser user = new frmUser();
            user.Show();
        }

        private void mnusale_Click(object sender, EventArgs e)
        {
            frmChooseCounter newform = new frmChooseCounter();
            newform.Show();
            //frmSale sale = new frmSale();
            //sale.Show();
        }

        private void mnusalereturn_Click(object sender, EventArgs e)
        {
            frmSaleReturn salereturn = new frmSaleReturn();
            salereturn.Show();
        }

        private void mnusaleorder_Click(object sender, EventArgs e)
        {
            frmSaleOrder saleorder = new frmSaleOrder();
            saleorder.Show();
        }

        private void mnuEntry_Click(object sender, EventArgs e)
        {

        }

        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mnucurrency_Click(object sender, EventArgs e)
        {
            UI.frmCurrency currency = new UI.frmCurrency();
            currency.Show();
        }
        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        private void mnuexchangerate_Click(object sender, EventArgs e)
        {
           
        }

        private void saleLissToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmSaleHistory salehistory = new UI.frmSaleHistory();
            salehistory.Show();
        }

        private void currencyHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void exchangeHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void uToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void customerHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmStock stock = new UI.frmStock();
            stock.Show();
        }

        private void classToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmclassentry frmclass = new UI.frmclassentry();
            frmclass.Show();
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmCategory category = new UI.frmCategory();
            category.Show();
        }

        private void brandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmBrand brand = new UI.frmBrand();
            brand.Show();
        }

        private void measurementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmMeasurement  measurement = new UI.frmMeasurement();
            measurement.Show();
        }

        private void systemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmSystemEntry SystemEntry = new UI.frmSystemEntry();
            SystemEntry.Show();
        }

        
        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void voucherSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmVoucherSetting Voucher = new UI.frmVoucherSetting();
            Voucher.Show();
        }

        private void tetToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void divisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmDivision division = new UI.frmDivision();
            division.Show();
        }

        private void departmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
             UI.frmDepartment department = new UI.frmDepartment();
             department.Show();
        }

        private void stockAdjustmentTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmAdjustmentType adjustment = new UI.frmAdjustmentType();
            adjustment.Show();
        }

        private void stockAdjustmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmStockAdjustment adjustment = new UI.frmStockAdjustment();
            adjustment.Show();
        }

        private void stockAdjustmentHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmAdjustmentHistory adjustment = new UI.frmAdjustmentHistory();
            adjustment.Show();
        }

        private void openingStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmOpeningStock openingStk = new UI.frmOpeningStock();
            openingStk.Show();
        }

        private void stockBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmStockBalance stkBalance = new UI.frmStockBalance();
            stkBalance.Show();
        }

        private void openingStockHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmOpeningStockHistory openingStkHistory = new UI.frmOpeningStockHistory();
            openingStkHistory.Show();
        }

        private void saleReportForCreditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(1);
            frmSaleFilter.Show();
        }

        private void customerReportForToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(2);
            frmSaleFilter.ShowDialog();
        }

        private void townshipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmTownship Township = new UI.frmTownship();
            Township.Show();
        }

        private void saleOrderHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmSaleOrderHistory SaleOrderHistory = new UI.frmSaleOrderHistory();
            SaleOrderHistory.Show();
        }

        private void bestSellingStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter();
            frmSaleFilter.ShowDialog();
        }

        private void saleReportForCashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(1);
            frmSaleFilter.ShowDialog();
        }

        private void saleReportForFOCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(1);
            frmSaleFilter.ShowDialog();
        }

        private void saleReportForDiscountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(1);
            frmSaleFilter.ShowDialog();
        }

        private void saleReportByCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(1);
            frmSaleFilter.ShowDialog();
        }

        private void saleReportByCurrencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(1);
            frmSaleFilter.ShowDialog();
        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmPurchase frmPurchase = new UI.frmPurchase();
            frmPurchase.Show();
        }

        private void purchaseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmPurchaseHistory frmPurchaseHistory = new UI.frmPurchaseHistory();
            frmPurchaseHistory.Show();
        }

        private void purchaseReportForCreditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(2);
            frmSaleFilter.ShowDialog();
        }

        private void saleReturnHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmSaleReturnHistory frmsalereturnhistory = new UI.frmSaleReturnHistory();
            frmsalereturnhistory.Show();
        }

        private void purchaseReportForCashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(2);
            frmSaleFilter.ShowDialog();
        }

        private void purchaseReportForFOCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(2);
            frmSaleFilter.ShowDialog();
        }

        private void purchaseReportForDiscountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(2);
            frmSaleFilter.ShowDialog();
        }

        private void purchaseReportBySupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(2);
            frmSaleFilter.ShowDialog();
        }

        private void stockInHandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter();
            frmSaleFilter.ShowDialog();
        }

        private void stockForSaleOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter();
            frmSaleFilter.ShowDialog();
        }

        private void stockForPurchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter();
            frmSaleFilter.ShowDialog();
        }

        private void customerReportForSaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(2);
            frmSaleFilter.Show();
        }

        private void customerReportByTownshipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(3);
            frmSaleFilter.Show();
        }

        private void customerReportForCreditSaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(3);
            frmSaleFilter.ShowDialog();
        }

        private void customerReportForCashSaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(3);
            frmSaleFilter.ShowDialog();
        }

        private void customerReportBySaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(4);
            frmSaleFilter.ShowDialog();
        }

        private void customerReportByDivisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(4);
            frmSaleFilter.ShowDialog();
        }

        private void customerReportByTownshipToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(4);
            frmSaleFilter.ShowDialog();
        }

        private void customerReportForCreditSaleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(4);
            frmSaleFilter.ShowDialog();
        }

        private void customerReportForCashSaleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(4);
            frmSaleFilter.ShowDialog();
        }

        private void locationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmLocation frmLocation = new UI.frmLocation();
            frmLocation.Show();
        }

        private void supplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmSupplier frmSupplier = new UI.frmSupplier();
            frmSupplier.Show();
        }

        private void stockTransferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmStockTransfer frmStockTransfer = new UI.frmStockTransfer();
            frmStockTransfer.Show();
        }

        private void stockHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmStockHistory frmStockHistory = new UI.frmStockHistory();
            frmStockHistory.Show();
        }

        private void backUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void stockTransferHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmStockTransferHistory frmStockTransferHistory = new UI.frmStockTransferHistory();
            frmStockTransferHistory.Show();
        }

        private void cashReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void payForCreditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmPayForCreditEntry frmPayForCreditEntry = new UI.frmPayForCreditEntry();
            frmPayForCreditEntry.Show();
        }

        private void pettyCashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmPettyCash frmPettyCash = new UI.frmPettyCash();
            frmPettyCash.Show();
        }

        private void payForCreditHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmPayForCredit frmPayForCredit = new UI.frmPayForCredit();
            frmPayForCredit.Show();
        }

        private void pettyCashHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmPettyCashHistory frmPettyCashHistory = new UI.frmPettyCashHistory();
            frmPettyCashHistory.Show();
        }

        private void authorizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmAuthorization frmauthorize = new UI.frmAuthorization();
            frmauthorize.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                DALAuthorization dalauthorize = new DALAuthorization();
                List<BOLAuthorization> lstauthorize = new List<BOLAuthorization>();

                lstauthorize = dalauthorize.SelectAllAuth(UserID);

                if (lstauthorize.Count > 0)
                {
                    if (lstauthorize[0].Authorization)
                    {
                        authorizationToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        authorizationToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Setlocation)
                    {
                        setLocationToolStripMenuItem.Enabled = true;
                    }
                    else 
                    {
                        setLocationToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Saleentry)
                    {
                        mnusale.Enabled = true;
                    }
                    else
                    {
                        mnusale.Enabled = false;
                    }
                    if (lstauthorize[0].Purchaseentry)
                    {
                        purchaseToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        purchaseToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Salereturn)
                    {
                        mnusalereturn.Enabled = true;
                    }
                    else
                    {
                        mnusalereturn.Enabled = false;
                    }
                    if (lstauthorize[0].Saleorder)
                    {
                        mnusaleorder.Enabled = true;
                    }
                    else
                    {
                        mnusaleorder.Enabled = false;
                    }
                    if (lstauthorize[0].Stockajustment)
                    {
                        stockAdjustmentToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        stockAdjustmentToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Stocktransfer)
                    {
                        stockTransferToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        stockTransferToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Openingstock)
                    {
                        openingStockToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        openingStockToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Stockbalance)
                    {
                        stockBalanceToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        stockBalanceToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Payforcredit)
                    {
                        payForCreditToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        payForCreditToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Pettycash)
                    {
                        pettyCashToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        pettyCashToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Salehistory)
                    {
                        saleLissToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        saleLissToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Purchasehistory)
                    {
                        purchaseHistoryToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        purchaseHistoryToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Salereturnhistory)
                    {
                        saleReturnHistoryToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        saleReturnHistoryToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Saleorderhistory)
                    {
                        saleOrderHistoryToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        saleOrderHistoryToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Stockajustmenthistory)
                    {
                        stockAdjustmentHistoryToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        stockAdjustmentHistoryToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Stocktransferhistory)
                    {
                        stockTransferHistoryToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        stockTransferHistoryToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Openingstockhistory)
                    {
                        openingStockHistoryToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        openingStockHistoryToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Stockhistory)
                    {
                        stockHistoryToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        stockHistoryToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Payforcredithistory)
                    {
                        payForCreditHistoryToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        payForCreditHistoryToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Pettycashhistory)
                    {
                        pettyCashHistoryToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        pettyCashHistoryToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].User)
                    {
                        mnuuser.Enabled = true;
                    }
                    else
                    {
                        mnuuser.Enabled = false;
                    }
                    if (lstauthorize[0].Division)
                    {
                        divisionToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        divisionToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Department)
                    {
                        departmentToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        departmentToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Township)
                    {
                        townshipToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        townshipToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Location)
                    {
                        locationToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        locationToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Customer)
                    {
                        mnucustomer.Enabled = true;
                    }
                    else
                    {
                        mnucustomer.Enabled = false;
                    }
                    if (lstauthorize[0].Supplier)
                    {
                        supplierToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        supplierToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Measurement)
                    {
                        measurementToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        measurementToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Brand)
                    {
                        brandToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        brandToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Class_)
                    {
                        classToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        classToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Category)
                    {
                        categoryToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        categoryToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Stock)
                    {
                        stockToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        stockToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Currency)
                    {
                        mnucurrency.Enabled = true;
                    }
                    else
                    {
                        mnucurrency.Enabled = false;
                    }
                    if (lstauthorize[0].Systemsetting)
                    {
                        systemToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        systemToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Cashreport)
                    {
                        cashReportToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        cashReportToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Salereport)
                    {
                        saleReportToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        saleReportToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Purchasereport)
                    {
                        purchaseReportToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        purchaseReportToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Stockreport)
                    {
                        stockReportToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        stockReportToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Customerreport)
                    {
                        customerReportToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        customerReportToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].Supplierreport)
                    {
                        supplierReportToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        supplierReportToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].VoucherSetting)
                    {
                        voucherSettingToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        voucherSettingToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].StockAdjustmentType)
                    {
                        stockAdjustmentTypeToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        stockAdjustmentTypeToolStripMenuItem.Enabled = false;
                    }
                    if (lstauthorize[0].OutletCashHeader)
                    {
                        outletCashHeaderToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        outletCashHeaderToolStripMenuItem.Enabled = false;
                    }
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

     

        private void backUpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(@"D:\MoeYanPOSDataBackup"))
                {
                    if (File.Exists(@"D:\MoeYanPOSDataBackup\MoeYanPOSData.bak"))
                    {
                        File.Delete(@"D:\MoeYanPOSDataBackup\MoeYanPOSData.bak");
                    }
                }
                else
                {
                    System.IO.Directory.CreateDirectory(@"D:\MoeYanPOSDataBackup");
                }


                user.Backup();
                MessageBox.Show("Successfully backup your data.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void setLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmSetLocation SetLocation = new UI.frmSetLocation();
            SetLocation.Show();
        }

        private void outletCashHeaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmOutletCashHeader OutletCashHeader = new UI.frmOutletCashHeader();
            OutletCashHeader.Show();
        }

        private void customerReportForToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(3);
            frmSaleFilter.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmUSer frmuser = new frmUSer();
            //frmuser.ShowDialog();
        }

        private void mnufile_Click(object sender, EventArgs e)
        {

        }

        private void exportAndImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //UI.frmExportData frmexportdata = new UI.frmExportData();
            //frmexportdata.Show();
            UI.frmExportAndImport newform = new frmExportAndImport();
            newform.Show();
        }

        private void bankChequeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmBankCheque frmbankcheque = new UI.frmBankCheque();
            frmbankcheque.Show();     
        }

        private void staffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmStaff frmstaff = new UI.frmStaff();
            frmstaff.Show();     
        }

        private void saleSummaryByUserToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void netSaleReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void netSaleReportHeaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(5);
            frmSaleFilter.Show();
        }

        private void netSaleReportDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(4);
            frmSaleFilter.Show();
        }

        private void purchaseSummaryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter newform = new UI.Report.frmSaleFilter(6);
            newform.ShowDialog();
        }

        private void netPurchaseReportHeaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter newform = new UI.Report.frmSaleFilter(7);
            newform.ShowDialog();
        }

        private void netPurchaseReportDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter newform = new UI.Report.frmSaleFilter(8);
            newform.ShowDialog();
        }

        private void stmiSaleSummaryByStock_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter newform = new UI.Report.frmSaleFilter(9);
            newform.Show();
        }

        private void mnupurreturn_Click(object sender, EventArgs e)
        {
            frmPurchaseReturn newform = new frmPurchaseReturn();
            newform.Show();
        }

        private void mnupurreturnhistory_Click(object sender, EventArgs e)
        {
            UI.frmPurchaseReturnHistory newform = new UI.frmPurchaseReturnHistory();
            newform.Show();
        }

        private void mnupurchaseSummaryByStockReport_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter newform = new UI.Report.frmSaleFilter(6);
            newform.Show();
        }

        private void mnunetPurchaseReportBySupplier_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter newform = new UI.Report.frmSaleFilter(7);
            newform.Show();
        }

        private void mnunetPurchaseReportDetail_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter newform = new UI.Report.frmSaleFilter(8);
            newform.Show();
        }

        private void dailyCashReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmCashReport frmCashReport = new UI.frmCashReport();
            frmCashReport.Show();
        }

        private void profitLossReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmCashReport frmCashReport = new UI.frmCashReport(1);
            frmCashReport.Show();
        }

        private void importPriceListToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exportPriceListToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void counterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmCounter frmcounter = new frmCounter();
            frmcounter.Show();
        }

        private void mnutransaction_Click(object sender, EventArgs e)
        {

        }

        private void resetAllCounterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetCounter();
        }

        private void ResetCounter()
        { 
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_ResetAllCounter", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();                
                cmd.ExecuteNonQuery();

                MessageBox.Show("Reset Successfully!", "Reset Counter");

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        private void monthlySaleSummaryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(10);
            frmSaleFilter.Show();
        }

        private void stockMovementReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(11);
            frmSaleFilter.Show();
        }

        private void pOSSaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChooseCounter newform = new frmChooseCounter();
            newform.Show();
            //UI.frmPOS newform = new frmPOS();
            //newform.Show();
        }

        private void netSaleReportByPcsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Report.frmSaleFilter frmSaleFilter = new UI.Report.frmSaleFilter(12);
            frmSaleFilter.Show();
        }
    }
}
