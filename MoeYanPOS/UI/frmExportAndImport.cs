using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using System.IO;
using MoeYanPOS.BOL;
using MoeYanPOS.DAL;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Xml.Linq;
using MoeYanPOS.Function;
using System.Configuration;



namespace MoeYanPOS.UI
{
    public partial class frmExportAndImport : Form
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr = MoeYanConfiguration.GetConnection();
        DALSale dalsale = new DALSale();
        int isSave = 0;
        #endregion

        DALSale dalSale = new DALSale();
        DateTime StartDateTime;
        DateTime EndDateTime;

        public frmExportAndImport()
        {
            InitializeComponent();
        }

        private void getData()
        {
            try
            {
                
                string sm = dtpFromDate.Value.Month.ToString().Length > 1 ? dtpFromDate.Value.Month.ToString() : "0" + dtpFromDate.Value.Month.ToString();
                string sd = dtpFromDate.Value.Day.ToString().Length > 1 ? dtpFromDate.Value.Day.ToString() : "0" + dtpFromDate.Value.Day.ToString();
                string lm = dtpToDate.Value.Month.ToString().Length > 1 ? dtpToDate.Value.Month.ToString() : "0" + dtpToDate.Value.Month.ToString();
                string ld = dtpToDate.Value.Day.ToString().Length > 1 ? dtpToDate.Value.Day.ToString() : "0" + dtpToDate.Value.Day.ToString();

                string sdate = dtpFromDate.Value.Year.ToString() + "-" + sm + "-" + sd + " 00:00:00.000";
                string ldate = dtpToDate.Value.Year.ToString() + "-" + lm + "-" + ld + " 23:59:59.000";

                StartDateTime = DateTime.Parse(sdate);
                EndDateTime = DateTime.Parse(ldate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void export_Sale()
        {

            try
            {
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                Worksheet worksheet = null;
                Workbook workbook = null;
                Range worksheet_range = null;

                app.Visible = true; // or false if you don't want to show app.
                workbook = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                worksheet = (Worksheet)workbook.Worksheets[1];

                worksheet_range = worksheet.get_Range("A1", "AD1");
                worksheet_range.Merge();
                worksheet_range.Font.Bold = 1;
                worksheet_range.Font.Underline = 2;
                worksheet_range.FormulaR1C1 = "Moe Yan POS";

                worksheet_range = worksheet.get_Range("A2", "AD2");
                worksheet_range.Merge();
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = String.Format("{0:MMMM}", DateTime.Now) + "  " + DateTime.Now.Year.ToString() + "  ထီ အေရာင္းစာရင္း";


                // worksheet.Cells[3, 1] = "Date";

                worksheet_range = worksheet.get_Range("B3");
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "SaleID";

                worksheet_range = worksheet.get_Range("C3");
                worksheet_range.ColumnWidth = 15;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "UserID";

                worksheet_range = worksheet.get_Range("D3");
                worksheet_range.ColumnWidth = 17;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "UserName";

                worksheet_range = worksheet.get_Range("E3");
                worksheet_range.ColumnWidth = 15;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "CustomerName";

                worksheet_range = worksheet.get_Range("F3");
                worksheet_range.ColumnWidth = 15;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "CustomerID";

                worksheet_range = worksheet.get_Range("G3");
                worksheet_range.ColumnWidth = 20;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "VoucherNo";

                worksheet_range = worksheet.get_Range("H3");
                worksheet_range.ColumnWidth = 15;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "PaymentType";

                worksheet_range = worksheet.get_Range("I3");
                worksheet_range.ColumnWidth = 15;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "CurrencyID";

                worksheet_range = worksheet.get_Range("J3");
                worksheet_range.ColumnWidth = 15;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "DayLimit";

                //................

                worksheet_range = worksheet.get_Range("K3");
                worksheet_range.ColumnWidth = 20;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "Date";

                worksheet_range = worksheet.get_Range("L3");
                worksheet_range.ColumnWidth = 20;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "Total Amount";

                worksheet_range = worksheet.get_Range("M3");
                worksheet_range.ColumnWidth = 15;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "Advance";

                worksheet_range = worksheet.get_Range("N3");
                worksheet_range.ColumnWidth = 15;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "Discount";

                worksheet_range = worksheet.get_Range("O3");
                worksheet_range.ColumnWidth = 20;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "GrandTotal";

                worksheet_range = worksheet.get_Range("P3");
                worksheet_range.ColumnWidth = 15;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "Total FOC";

                worksheet_range = worksheet.get_Range("Q3");
                worksheet_range.ColumnWidth = 20;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "Total Item Discount";

                worksheet_range = worksheet.get_Range("R3");
                worksheet_range.ColumnWidth = 15;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "Location";

                worksheet_range = worksheet.get_Range("S3");
                worksheet_range.ColumnWidth = 15;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "ID";

                worksheet_range = worksheet.get_Range("T3");
                worksheet_range.ColumnWidth = 20;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "Currency";

                worksheet_range = worksheet.get_Range("U3");
                worksheet_range.ColumnWidth = 10;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "No";

                worksheet_range = worksheet.get_Range("V3");
                worksheet_range.ColumnWidth = 15;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "Item Code";

                worksheet_range = worksheet.get_Range("W3");
                worksheet_range.ColumnWidth = 35;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "Description";

                worksheet_range = worksheet.get_Range("X3");
                worksheet_range.ColumnWidth = 10;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "Type";

                worksheet_range = worksheet.get_Range("Y3");
                worksheet_range.ColumnWidth = 10;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "Quantity";

                worksheet_range = worksheet.get_Range("Z3");
                worksheet_range.ColumnWidth = 15;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "SalePrice";

                worksheet_range = worksheet.get_Range("AA3");
                worksheet_range.ColumnWidth = 15;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "Total";

                worksheet_range = worksheet.get_Range("AB3");
                worksheet_range.ColumnWidth = 15;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "FOC";

                worksheet_range = worksheet.get_Range("AC3");
                worksheet_range.ColumnWidth = 15;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "Item Discount";

                worksheet_range = worksheet.get_Range("AD3");
                worksheet_range.ColumnWidth = 30;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = "Item Discount Percent";


                worksheet_range = worksheet.get_Range("A1", "AD100");
                worksheet_range.Font.Name = "Zawgyi-One";
                worksheet_range.Font.Size = 9;
                worksheet_range.HorizontalAlignment = Constants.xlCenter;
                worksheet_range.Borders.Weight = 2;

                //****** Table Start

                Decimal sumTotalAmount = 0; Decimal sumDiscount = 0; Decimal sumGrandTotal = 0;
                Decimal sumTotalItemDis = 0; Decimal sumSalePrice = 0; Decimal sumTotal = 0; Decimal sumItemDis = 0;
                int sumItemDisPercent = 0; int sumQty = 0; int sumTotalFOC = 0;

                DataSet ds = new DataSet();
                ds = dalSale.SelectGetAllSaleList(StartDateTime, EndDateTime);

                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        string data = ds.Tables[0].Rows[i].ItemArray[j].ToString();

                        app.Cells[i + 4, j + 2] = data;
                    }

                    sumTotalAmount += Decimal.Parse(ds.Tables[0].Rows[i].ItemArray[10].ToString());
                    sumDiscount += Decimal.Parse(ds.Tables[0].Rows[i].ItemArray[12].ToString());
                    sumGrandTotal += Decimal.Parse(ds.Tables[0].Rows[i].ItemArray[13].ToString());
                    sumTotalFOC += Int32.Parse(ds.Tables[0].Rows[i].ItemArray[14].ToString());
                    sumTotalItemDis += Decimal.Parse(ds.Tables[0].Rows[i].ItemArray[15].ToString());
                    sumQty += Int32.Parse(ds.Tables[0].Rows[i].ItemArray[23].ToString());
                    sumSalePrice += Decimal.Parse(ds.Tables[0].Rows[i].ItemArray[24].ToString());
                    sumTotal += Decimal.Parse(ds.Tables[0].Rows[i].ItemArray[25].ToString());
                    sumItemDis += Decimal.Parse(ds.Tables[0].Rows[i].ItemArray[27].ToString());
                    sumItemDisPercent += Int32.Parse(ds.Tables[0].Rows[i].ItemArray[28].ToString());

                }

                worksheet_range = worksheet.get_Range("L" + Convert.ToString(5 + ds.Tables[0].Rows.Count));
                worksheet_range.ColumnWidth = 15;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = sumTotalAmount;

                worksheet_range = worksheet.get_Range("N" + Convert.ToString(5 + ds.Tables[0].Rows.Count));
                worksheet_range.ColumnWidth = 15;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = sumDiscount;

                worksheet_range = worksheet.get_Range("O" + Convert.ToString(5 + ds.Tables[0].Rows.Count));
                worksheet_range.ColumnWidth = 15;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = sumGrandTotal;


                worksheet_range = worksheet.get_Range("P" + Convert.ToString(5 + ds.Tables[0].Rows.Count));
                worksheet_range.ColumnWidth = 15;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = sumTotalFOC;


                worksheet_range = worksheet.get_Range("Q" + Convert.ToString(5 + ds.Tables[0].Rows.Count));
                worksheet_range.ColumnWidth = 15;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = sumTotalItemDis;

                worksheet_range = worksheet.get_Range("Y" + Convert.ToString(5 + ds.Tables[0].Rows.Count));
                worksheet_range.ColumnWidth = 15;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = sumQty;

                worksheet_range = worksheet.get_Range("Z" + Convert.ToString(5 + ds.Tables[0].Rows.Count));
                worksheet_range.ColumnWidth = 15;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = sumSalePrice;


                worksheet_range = worksheet.get_Range("AA" + Convert.ToString(5 + ds.Tables[0].Rows.Count));
                worksheet_range.ColumnWidth = 15;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = sumTotal;

                worksheet_range = worksheet.get_Range("AC" + Convert.ToString(5 + ds.Tables[0].Rows.Count));
                worksheet_range.ColumnWidth = 15;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = sumItemDis;

                worksheet_range = worksheet.get_Range("AD" + Convert.ToString(5 + ds.Tables[0].Rows.Count));
                worksheet_range.ColumnWidth = 15;
                worksheet_range.Font.Bold = 1;
                worksheet_range.FormulaR1C1 = sumItemDisPercent;


                workbook.SaveAs(@"D:\ExcelReport\" + String.Format("{0:yyyy_MMMM}", dtpFromDate.Value) + "SaleforMoeYanPOS.xls");

                GC.Collect();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }            
       
        }

        public static void ExportExcel(string aFileName, System.Data.DataSet ds)
        {
            System.Data.DataTable aTable = ds.Tables[0];
            System.Data.DataTable Deatil = ds.Tables[1];

            try
            {
                Microsoft.Office.Interop.Excel.Application l_excel = new Microsoft.Office.Interop.Excel.Application();
                var workbook = l_excel.Workbooks.Add();
                Worksheet sht1, sht2;
                sht1 = ((Worksheet)workbook.Sheets[1]);
                if (workbook.Sheets.Count < 2)
                {
                    sht2 = (Worksheet)workbook.Sheets.Add();
                }
                else
                {
                    sht2 = ((Worksheet)workbook.Sheets[2]);
                }
                l_excel.Visible = true;
                sht1.Move(sht2);

                sht1.Name = "SaleHeader";
                sht2.Name = "SaleDetail";

                #region"SaleHeader"

                #region "Excel Header"
                sht1.Cells[1, 1] = "VoucherNo";
                sht1.Cells[1, 2] = "Date";
                sht1.Cells[1, 3] = "CustomerID";
                sht1.Cells[1, 4] = "UserID";
                sht1.Cells[1, 5] = "PaymentType";
                sht1.Cells[1, 6] = "CurrencyID";
                sht1.Cells[1, 7] = "DayLimit";
                sht1.Cells[1, 8] = "TotalAmt";
                sht1.Cells[1, 9] = "Advance";
                sht1.Cells[1, 10] = "Discount";
                sht1.Cells[1, 11] = "GrandTotal";
                sht1.Cells[1, 12] = "TotalFOC";
                sht1.Cells[1, 13] = "TotalItemDiscount";
                sht1.Cells[1, 14] = "EditUserID";
                sht1.Cells[1, 15] = "EditSaleDate";
                sht1.Cells[1, 16] = "ExchangeRate";
                sht1.Cells[1, 17] = "SystemVoucherNo";
                sht1.Cells[1, 18] = "LotteryDate";
                sht1.Cells[1, 19] = "DrawingTimes";
                sht1.Cells[1, 20] = "LotteryNo";
                sht1.Cells[1, 21] = "LocationID";
                sht1.Cells[1, 22] = "TransportationAmt";
                sht1.Cells[1, 23] = "Remark";
                ////l_excel.Cells[1, 24] = "No";
                ////l_excel.Cells[1, 25] = "ItemCode";
                ////l_excel.Cells[1, 26] = "Description";
                ////l_excel.Cells[1, 27] = "Type";
                ////l_excel.Cells[1, 28] = "Qty";
                ////l_excel.Cells[1, 29] = "SalePrice";
                ////l_excel.Cells[1, 30] = "Total";
                ////l_excel.Cells[1, 31] = "FOC";
                ////l_excel.Cells[1, 32] = "ItemDiscount";
                ////l_excel.Cells[1, 33] = "ItemDiscountPercent";
                ////l_excel.Cells[1, 34] = "Charges";
                #endregion

                #region"DataBind"
                int l_row_index = 1;
                for (int i = 0; i < aTable.Rows.Count; i++)
                {
                            l_row_index = l_row_index + 1;

                            sht1.Cells[l_row_index, 1] = aTable.Rows[i]["VoucherNo"].ToString();
                            sht1.Cells[l_row_index, 2] = aTable.Rows[i]["Date"].ToString();
                            sht1.Cells[l_row_index, 3] = aTable.Rows[i]["CustomerID"].ToString();
                            sht1.Cells[l_row_index, 4] = aTable.Rows[i]["UserID"].ToString();
                            sht1.Cells[l_row_index, 5] = aTable.Rows[i]["PaymentType"].ToString();
                            sht1.Cells[l_row_index, 6] = aTable.Rows[i]["CurrencyID"].ToString();
                            sht1.Cells[l_row_index, 7] = aTable.Rows[i]["DayLimit"].ToString();
                            sht1.Cells[l_row_index, 8] = aTable.Rows[i]["TotalAmt"].ToString();
                            sht1.Cells[l_row_index, 9] = aTable.Rows[i]["Advance"].ToString();
                            sht1.Cells[l_row_index, 10] = aTable.Rows[i]["Discount"].ToString();
                            sht1.Cells[l_row_index, 11] = aTable.Rows[i]["GrandTotal"].ToString();
                            sht1.Cells[l_row_index, 12] = aTable.Rows[i]["TotalFOC"].ToString();
                            sht1.Cells[l_row_index, 13] = aTable.Rows[i]["TotalItemDiscount"].ToString();
                            sht1.Cells[l_row_index, 14] = aTable.Rows[i]["EditUserID"].ToString();
                            sht1.Cells[l_row_index, 15] = aTable.Rows[i]["EditSaleDate"].ToString();
                            sht1.Cells[l_row_index, 16] = aTable.Rows[i]["ExchangeRate"].ToString();
                            sht1.Cells[l_row_index, 17] = aTable.Rows[i]["SystemVoucherNo"].ToString();
                            sht1.Cells[l_row_index, 18] = aTable.Rows[i]["LotteryDate"].ToString();
                            sht1.Cells[l_row_index, 19] = aTable.Rows[i]["DrawingTimes"].ToString();
                            sht1.Cells[l_row_index, 20] = aTable.Rows[i]["LotteryNo"].ToString();
                            sht1.Cells[l_row_index, 21] = aTable.Rows[i]["LocationID"].ToString();
                            sht1.Cells[l_row_index, 22] = aTable.Rows[i]["TransportationAmt"].ToString();
                            sht1.Cells[l_row_index, 23] = aTable.Rows[i]["Remark"].ToString();
                //            //l_excel.Cells[l_row_index, 24] = aTable.Rows[i]["No"].ToString();
                //            //l_excel.Cells[l_row_index, 25] = aTable.Rows[i]["ItemCode"].ToString();
                //            //l_excel.Cells[l_row_index, 26] = aTable.Rows[i]["Description"].ToString();
                //            //l_excel.Cells[l_row_index, 27] = aTable.Rows[i]["Type"].ToString();
                //            //l_excel.Cells[l_row_index, 28] = aTable.Rows[i]["Qty"].ToString();
                //            //l_excel.Cells[l_row_index, 29] = aTable.Rows[i]["SalePrice"].ToString();
                //            //l_excel.Cells[l_row_index, 30] = aTable.Rows[i]["Total"].ToString();
                //            //l_excel.Cells[l_row_index, 31] = aTable.Rows[i]["FOC"].ToString();
                //            //l_excel.Cells[l_row_index, 32] = aTable.Rows[i]["ItemDiscount"].ToString();
                //            //l_excel.Cells[l_row_index, 33] = aTable.Rows[i]["ItemDiscountPercent"].ToString();
                //            //l_excel.Cells[l_row_index, 34] = aTable.Rows[i]["Charges"].ToString();
                }
                #endregion

                #endregion

                #region"SaleDetail"
                #region "Excel Header"
                sht2.Cells[1, 1] = "No";
                sht2.Cells[1, 2] = "ItemCode";
                sht2.Cells[1, 3] = "Description";
                sht2.Cells[1, 4] = "Type";
                sht2.Cells[1, 5] = "Qty";
                sht2.Cells[1, 6] = "SalePrice";
                sht2.Cells[1, 7] = "Total";
                sht2.Cells[1, 8] = "FOC";
                sht2.Cells[1, 9] = "ItemDiscount";
                sht2.Cells[1, 10] = "ItemDiscountPercent";
                sht2.Cells[1, 11] = "Charges";
                #endregion

                #region"DataBind"
                int l_row_index1 = 1;
                for (int i = 0; i < Deatil.Rows.Count; i++)
                {
                    l_row_index1 = l_row_index1 + 1;

                    sht2.Cells[l_row_index1, 1] = Deatil.Rows[i]["No"].ToString();
                    sht2.Cells[l_row_index1, 2] = Deatil.Rows[i]["ItemCode"].ToString();
                    sht2.Cells[l_row_index1, 3] = Deatil.Rows[i]["Description"].ToString();
                    sht2.Cells[l_row_index1, 4] = Deatil.Rows[i]["Type"].ToString();
                    sht2.Cells[l_row_index1, 5] = Deatil.Rows[i]["Qty"].ToString();
                    sht2.Cells[l_row_index1, 6] = Deatil.Rows[i]["SalePrice"].ToString();
                    sht2.Cells[l_row_index1, 7] = Deatil.Rows[i]["Total"].ToString();
                    sht2.Cells[l_row_index1, 8] = Deatil.Rows[i]["FOC"].ToString();
                    sht2.Cells[l_row_index1, 9] = Deatil.Rows[i]["ItemDiscount"].ToString();
                    sht2.Cells[l_row_index1, 10] = Deatil.Rows[i]["ItemDiscountPercent"].ToString();
                    sht2.Cells[l_row_index1, 11] = Deatil.Rows[i]["Charges"].ToString();
                }
                #endregion
                #endregion
                sht1.SaveAs(aFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, "", "", false, false, false, false, false, false);
                sht2.SaveAs(aFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, "", "", false, false, false, false, false, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private void frmExportAndImport_Load(object sender, EventArgs e)
        {
            btnExportSaleHistory.Visible = false;
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

        private DateTime getfirstday(DateTime dateTime)
        {
            DateTime dd;
            dd = new DateTime(dateTime.Year, dateTime.Month, 1);
            return dd;
        }
        private DateTime getlastday(DateTime dateTime)
        {
            DateTime dd;
            dd = new DateTime(dateTime.Year, dateTime.Month, 1);
            return dd.AddMonths(1).AddDays(-1);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                getData();
                DataSet ds = new DataSet();
                System.Data.DataTable tbl1 = GetExportSaleHeader();
                System.Data.DataTable tbl2 = GetExportSaleDetail();
                ds.Tables.Add(tbl1);
                ds.Tables.Add(tbl2);
                if (Directory.Exists(@"D:\ExcelReport") == false)
                {
                    Directory.CreateDirectory(@"D:\ExcelReport");
                }

                DateTime today = dtpFromDate.Value;
                DateTime firstday = getfirstday(today);
                DateTime lastday = getlastday(today);

                //export_Sale();   //comment by htzn

                ExportExcel(@"D:\testexport.xls", ds);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void exportSaleHistory()
        {

            try
            {
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                Worksheet worksheet = null;
                Workbook workbook = null;
                Range worksheet_range = null;

                app.Visible = true; // or false if you don't want to show app.
                workbook = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                worksheet = (Worksheet)workbook.Worksheets[1];

                worksheet_range = worksheet.get_Range("A1", "AD100");
                worksheet_range.Font.Name = "Zawgyi-One";
                worksheet_range.Font.Size = 9;

                //****** Table Start

                DataSet ds = new DataSet();
                ds = dalSale.SP_GetOnlySaleHistory(StartDateTime, EndDateTime);

                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        string data = ds.Tables[0].Rows[i].ItemArray[j].ToString();

                        app.Cells[i + 1, j + 1] = data;
                    }
                }
                //Microsoft.Office.Interop.Excel.Range cel = (Range)worksheet_range.Cells[5+ ds.Tables[0].Rows.Count, ds.Tables[0].Columns.Count];
                //cel.Delete();

                workbook.SaveAs(@"D:\ExcelReport\" + String.Format("{0:yyyy_MMMM}", dtpFromDate.Value) + "SaleHistoryforMoeYanPOS.xls");

                GC.Collect();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void btnExportSaleHistory_Click(object sender, EventArgs e)
        {
            try
            {
                getData();
                if (Directory.Exists(@"D:\ExcelReport") == false)
                {
                    Directory.CreateDirectory(@"D:\ExcelReport");
                }

                DateTime today = dtpFromDate.Value;
                DateTime firstday = getfirstday(today);
                DateTime lastday = getlastday(today);

                exportSaleHistory();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                BOLSale bolsale = new BOLSale();
                //String strConnection = @"Data Source=NAN-PC;Initial Catalog=MoeYanPOS;User ID=sa;Password=moeyan";
                string path = txtFilePath.Text;

                string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;Persist Security Info=False";
                OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                OleDbCommand cmd = new OleDbCommand("Select [VoucherNo],[Date],[CustomerID],[UserID],[PaymentType],[CurrencyID],[DayLimit],[TotalAmt],[Advance],[Discount],[GrandTotal],[TotalFOC],[TotalItemDiscount],[EditUserID],[EditSaleDate],[ExchangeRate],[SystemVoucherNo],[LotteryDate],[DrawingTimes],[LotteryNo],[LocationID],[TransportationAmt],[Remark] from [Sheet1$] WHERE [VoucherNo] IS NOT NULL", excelConnection);
                excelConnection.Open();
                var adapter = new OleDbDataAdapter(cmd);
                var ds = new DataSet();
                adapter.Fill(ds);

                if (ds != null)
                {
                    System.Data.DataTable excelTbl = new System.Data.DataTable();
                    excelTbl = ds.Tables[0];

                    for (int i = 0; i <= excelTbl.Rows.Count; i++)
                    {
                        bolsale.TranSaleID = 1; //   
                        bolsale.VoucherNo = "";
                        bolsale.SaleDate = DateTime.Parse(excelTbl.Rows[i][""].ToString());//  SaleDate;                    
                        bolsale.UserID = Int32.Parse(excelTbl.Rows[i][""].ToString());
                        bolsale.PaymentType = excelTbl.Rows[i][""].ToString();
                        bolsale.CurrencyID = Int32.Parse(excelTbl.Rows[i][""].ToString());
                        bolsale.DayLimit = Int32.Parse(excelTbl.Rows[i][""].ToString());
                        bolsale.TotalAmt = Decimal.Parse(excelTbl.Rows[i][""].ToString());
                        bolsale.Advance = Decimal.Parse(excelTbl.Rows[i][""].ToString());
                        bolsale.Discount = Decimal.Parse(excelTbl.Rows[i][""].ToString());
                        bolsale.GrandTotal = Decimal.Parse(excelTbl.Rows[i][""].ToString());
                        bolsale.TotalFOC = Int32.Parse(excelTbl.Rows[i][""].ToString());
                        bolsale.TotalitemDiscount = Decimal.Parse(excelTbl.Rows[i][""].ToString());
                        bolsale.ExchangeRate = Decimal.Parse(excelTbl.Rows[i][""].ToString());
                        bolsale.SystemVoucherNo = excelTbl.Rows[i][""].ToString();  
                        bolsale.LotteryDate = DateTime.Parse(excelTbl.Rows[i][""].ToString());  
                        bolsale.LotteryNo = excelTbl.Rows[i][""].ToString();  
                        bolsale.DrawingTimes = excelTbl.Rows[i][""].ToString();
                        bolsale.LocationID = long.Parse(excelTbl.Rows[i][""].ToString());
                        bolsale.TransportationAmt = Int32.Parse(excelTbl.Rows[i][""].ToString());
                        bolsale.Remark = excelTbl.Rows[i][""].ToString();  

                        isSave = dalsale.SaveSaleData(bolsale);
                    }
                }

                excelConnection.Close();
                MessageBox.Show("Successfully Imported.","Information");
                txtFilePath.Text="";
                btnImport.Enabled = false;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileSelectPopUp = new OpenFileDialog();
            fileSelectPopUp.Title = "";
            fileSelectPopUp.InitialDirectory = @"D:\ExcelReport\";
            fileSelectPopUp.Filter = "All EXCEL FILES (*.xlsx*)|*.xlsx*|All files (*.*)|*.*";
            fileSelectPopUp.FilterIndex = 2;
            fileSelectPopUp.RestoreDirectory = true;
            if (fileSelectPopUp.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = fileSelectPopUp.FileName;
                btnImport.Enabled = true;
            }
        }

        private void txtFilePath_TextChanged(object sender, EventArgs e)
        {

        }

        public System.Data.DataTable GetExportSaleHeader()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetExportSaleHeader", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public System.Data.DataTable GetExportSaleDetail()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetExportSaleDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
                   
    }
}
