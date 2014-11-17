using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace PrinterServer
{
    
    //public class PrintBarServer
    //{
    //    public int mCurrentItemGroupType;
    //    public bool mCurrentItemGroupTypeFirst = true;
    //    private readonly string _nameClass = "POS::Printer::BarPrinterServer::";
    //    //private static string[] changeType = { "", "ADD", "CAN" };

    //    private Connection.Connection m_con;
    //    private System.Drawing.Font m_font;

    //    private Printer m_printer;
    //    private BecasData.Order.Order mOrder;

    //    //private int mOrderID;
    //    private Queue<PrintData> mQueue;

    //    private BecasData.Transit mTransit;
    //    private int PRINT_BAR_KITCHEN = 1;
    //    private int PRINT_BILL_TAXINVOICE = 2;

    //    //private SocketServer mSocketServer;
    //    public PrintBarServer()
    //    {
    //        Init();
    //    }

    //    public PrintBarServer(Printer printer, Connection.Connection con, BecasData.Transit transit)
    //    {
    //        Init();
    //        mTransit = transit;
    //        m_con = con;
    //        m_printer = printer;
    //        BarName = m_printer.printDocument.PrinterSettings.PrinterName;
    //        m_printer.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument_PrintPage);
    //        mQueue = new Queue<PrintData>();
    //    }

    //    public delegate void ChangedDataCompletedHander(int changed);

    //    public delegate void EnqueueEvenHander(PrintData data);

    //    public event ChangedDataCompletedHander ChangedCompleted;

    //    public event EnqueueEvenHander Enqueue;

    //    public string BarName { get; set; }

    //    public string BillName { get; set; }

    //    public string KitchenName { get; set; }

    //    public string PDABillName { get; set; }

    //    public void AddChangedCompleted()
    //    {
    //        OnChangedCompleted(1);
    //    }

    //    public void AddPrintData(PrintData data)
    //    {
    //        mQueue.Enqueue(data);
    //        if (Enqueue != null)
    //        {
    //            Enqueue(data);
    //        }
    //    }

    //    public FontStyle CheckTA(string str)
    //    {
    //        str = str.Trim();
    //        if (str.Length > 1)
    //        {
    //            string s = str.Substring(str.Length - 2, 2);
    //            if (s == "TA")
    //                return FontStyle.Bold;
    //            else
    //                return FontStyle.Regular;
    //        }
    //        return FontStyle.Regular;
    //    }   
    //    public List<BecasData.Order.ItemOrderK> getListOrder(PrintData data)
    //    {
    //        if (data.Function == 16)
    //        {
    //            return BecasData.Order.ChangeOrder.getChangeOrder_D(data.Order, data.CurrentPrinter, mTransit);
    //        }
    //        else
    //        {
    //            return BecasData.Order.ChangeOrder.getOrderOfTable_D(data.Order, data.CurrentPrinter, mTransit);
    //        }
    //    }

    //    public BecasData.Order.Order getOrderByOrderID(string orderID)
    //    {
    //        return BecasData.Order.ProcessOrder.getOrderByOrderID(Convert.ToInt32(orderID), mTransit);
    //    }

    //    public void Init()
    //    {
    //        //m_font= new System.Drawing.Font("Arial", 12);
    //    }

    //    public void OnChangedCompleted(int completed)
    //    {
    //        if (this.ChangedCompleted != null)
    //        {
    //            ChangedCompleted(completed);
    //        }
    //    }

    //    public void print(string orderID, int function, int Language)
    //    {
    //        try
    //        {
    //            BecasData.Order.Order order = BecasData.Order.ProcessOrder.getOrderByOrderID(Convert.ToInt32(orderID), mTransit);
    //            order.LanguageID = Language;
    //            if (order != null)
    //            {
    //                if (function == 14)
    //                {
    //                    printData(new PrintData(PRINT_BILL_TAXINVOICE, function, true, order));
    //                }
    //                else if (function == 18)
    //                {
    //                    if (order.Completed == 3)
    //                    {
    //                        printData(new PrintData(PRINT_BILL_TAXINVOICE, function, true, order));
    //                    }
    //                    else if (order.Completed == 1)
    //                    {
    //                        printData(new PrintData(PRINT_BILL_TAXINVOICE, function, false, order));
    //                    }
    //                }
    //                else
    //                {
    //                    PrintData data = new PrintData(PRINT_BAR_KITCHEN, function, false, order);
    //                    data.POSPrintDevice = mTransit.ListPrintDatabse;
    //                    printData(data);
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            SystemLog.LogPOS.WriteLog(_nameClass + "print::" + ex.Message);
    //        }
    //    }

    //    public void printBuilt(POSPrinter printer, System.Drawing.Printing.PrintPageEventArgs e)
    //    {
    //        PrintData data = (PrintData)printer.Tag;
    //        printer.IsDrawLine = mTransit.ReadConfig.IsDrawLineTaxInvoce; //! Có in line không ?Sau khi gán xong phải trả lại = true, vì khai báo chung
    //        System.Drawing.Font font11 = new System.Drawing.Font("Arial", 11);

    //        float l_y = 0;

    //        l_y = printer.DrawString(mTransit.DBConfig.Header, e, new System.Drawing.Font("Arial", mTransit.ReadConfig.HeaderFontSize), l_y, TextAlign.Center);
    //        if (mTransit.ReadConfig.HeaderMore != "")
    //        {
    //            l_y = printer.DrawString(mTransit.ReadConfig.HeaderMore, e, new System.Drawing.Font("Arial", 14), l_y, TextAlign.Center);
    //        }
    //        l_y = printer.DrawString(mTransit.DBConfig.BankCode, e, font11, l_y, TextAlign.Center);
    //        l_y = printer.DrawString(mTransit.DBConfig.Address, e, new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Italic), l_y, TextAlign.Center);
    //        l_y = printer.DrawString(mTransit.DBConfig.Tell, e, new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Italic), l_y, TextAlign.Center);

    //        l_y += printer.GetHeightPrinterLine();

    //        l_y = printer.DrawLine("", null, e, System.Drawing.Drawing2D.DashStyle.DashDot, l_y, TextAlign.Left);

    //        l_y += printer.GetHeightPrinterLine();
    //        DateTime dateTime = DateTime.Now;
    //        if (mTransit.ReadConfig.EnableDateTime == 1)
    //        {
    //            l_y = printer.DrawString(dateTime.Day + "/" + dateTime.Month + "/" + dateTime.Year + " " + dateTime.ToShortTimeString(), e, font11, l_y, TextAlign.Right);
    //        }
    //        printer.DrawString("STAFF# " + data.Order.StaffName, e, font11, l_y, TextAlign.Right);
    //        List<BecasData.Order.ItemOrderK> list = BecasData.Order.ChangeOrder.getOrderBill(data.Order, mTransit, data.IsSliptBill);
    //        if (list.Count <= 0)
    //        {
    //            e.Cancel = true;
    //            return;
    //        }
    //        l_y = printer.DrawString("ORDER# " + data.Order.OrderID, e, font11, l_y, TextAlign.Left);
    //        if (!data.Order.TableID.Contains("TKA-") && data.Order.TableID != "")
    //        {
    //            l_y = printer.DrawString("TABLE# " + data.Order.TableID, e, font11, l_y, TextAlign.Left);
    //        }
    //        if (data.Order.CustomerCode != null)
    //            l_y = printer.DrawString("CUST# " + data.Order.CustomerName, e, font11, l_y, TextAlign.Left);
    //        l_y += printer.GetHeightPrinterLine();
    //        l_y = printer.DrawLine("", null, e, System.Drawing.Drawing2D.DashStyle.DashDot, l_y, TextAlign.Left);
    //        l_y += printer.GetHeightPrinterLine();
    //        string sBuilt = "TAX INVOICE";
    //        if (data.IsBuiltName)
    //        {
    //            sBuilt = "BILL";
    //        }
    //        if (data.IsSliptBill)
    //        {
    //            sBuilt = "HISTORY TAX INVOICE";
    //        }
    //        l_y = printer.DrawString(sBuilt, e, new System.Drawing.Font("Arial", 13, System.Drawing.FontStyle.Bold), l_y, TextAlign.Center);

    //        l_y += printer.GetHeightPrinterLine();

    //        double subTotal = 0;
    //        double percent = 0;
    //        if (data.Order.SubTotal > 0)
    //        {
    //            percent = data.Order.Discount / data.Order.SubTotal;
    //        }
    //        bool checkPrinter = false;
    //        if (data.IsSliptBill)
    //        {
    //            try
    //            {
    //                System.Drawing.Font fontTotal = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
    //                double Tong = 0;
    //                int GroupBill = 0;
    //                bool printTong = false;

    //                foreach (BecasData.Order.ItemOrderK item in list)
    //                {
    //                    if (item.GroupBill > GroupBill)
    //                    {
    //                        if (GroupBill != 0)
    //                            printTong = true;
    //                        GroupBill = item.GroupBill;
    //                    }

    //                    if (printTong)
    //                    {
    //                        printTong = false;
    //                        l_y = printer.DrawLine("", null, e, System.Drawing.Drawing2D.DashStyle.Dot, l_y, TextAlign.Left);
    //                        printer.DrawString("Total:", e, fontTotal, l_y, TextAlign.Left);
    //                        l_y = printer.DrawString("$" + mTransit.MoneyFortmat.Format(Tong), e, fontTotal, l_y, TextAlign.Right);
    //                        l_y += printer.GetHeightPrinterLine();
    //                        Tong = 0;
    //                    }

    //                    if (item.GroupBill > 0)
    //                    {
    //                        float yStart = l_y;
    //                        if (item.ItemType != 1 || item.Qty != 0 || item.Price != 0)
    //                        {
    //                            l_y = printer.DrawLine("", null, e, System.Drawing.Drawing2D.DashStyle.Dot, l_y, TextAlign.Left);
    //                            yStart = l_y;
    //                            l_y = printer.DrawString(item.Qty + new String(' ', 3) + item.Name, e, new System.Drawing.Font("Arial", 8), l_y, TextAlign.Left);
    //                        }
    //                        foreach (BecasData.Order.ItemOptionID option in item.Option)
    //                        {
    //                            //l_y = printer.DrawString(new String('-', 2*option.ItemLevel) + option.Name + " (" + option.Qty + ")" + "($" + mTransit.MoneyFortmat.Format(option.Price) + ")", e, new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Italic), l_y, TextAlign.Left);
    //                            string strName = "";
    //                            if (option.Qty > 1)
    //                            {
    //                                strName = new String('-', 2 * option.ItemLevel) + option.Name + " (" + option.Qty + ")" + "($" + mTransit.MoneyFortmat.Format(option.Price) + ")";
    //                            }
    //                            else
    //                            {
    //                                strName = new String('-', 2 * option.ItemLevel) + option.Name + "($" + mTransit.MoneyFortmat.Format(option.Price) + ")";
    //                            }
    //                            l_y = printer.DrawString(strName, e, new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Italic), l_y, TextAlign.Left);
    //                            item.Price += option.Price;
    //                            Tong += option.Price;
    //                        }
    //                        if (item.isRefund == "1" && (item.ItemType != 1 || item.Qty != 0 || item.Price != 0))
    //                        {
    //                            printer.DrawString("Ref (-" + mTransit.MoneyFortmat.Format(item.Price * (1 - percent)) + ") ", e, new System.Drawing.Font("Arial", 11), yStart, TextAlign.Right);
    //                            Tong += item.Price;
    //                        }
    //                        else if (item.ItemType != 1 || item.Qty != 0 || item.Price != 0)
    //                        {
    //                            printer.DrawString(mTransit.MoneyFortmat.Format(item.Price), e, new System.Drawing.Font("Arial", 11), yStart, TextAlign.Right);
    //                            Tong += item.Price;
    //                        }
    //                    }
    //                }
    //                l_y = printer.DrawLine("", null, e, System.Drawing.Drawing2D.DashStyle.Dot, l_y, TextAlign.Left);
    //                printer.DrawString("Total:", e, fontTotal, l_y, TextAlign.Left);
    //                l_y = printer.DrawString("$" + mTransit.MoneyFortmat.Format(Tong), e, fontTotal, l_y, TextAlign.Right);
    //                l_y += printer.GetHeightPrinterLine();
    //                Tong = 0;
    //            }
    //            catch (Exception ex)
    //            {
    //                SystemLog.LogPOS.WriteLog(_nameClass + "printBuilt::" + ex.Message);
    //            }
    //        }
    //        else
    //        {
    //            try
    //            {
    //                foreach (BecasData.Order.ItemOrderK item in list)
    //                {
    //                    checkPrinter = false;
    //                    if (data.Order.CheckSliptOrders)
    //                    {
    //                        if (item.GroupBill == data.Order.GroupBill)
    //                        {
    //                            checkPrinter = true;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        checkPrinter = true;
    //                    }

    //                    if (checkPrinter)
    //                    {
    //                        float yStart = l_y;
    //                        if (item.ItemType != 1 || item.Qty != 0 || item.Price != 0)
    //                        {
    //                            l_y = printer.DrawLine("", null, e, System.Drawing.Drawing2D.DashStyle.Dot, l_y, TextAlign.Left);
    //                            yStart = l_y;
    //                            l_y = printer.DrawString(item.Qty + new String(' ', 3) + item.Name + "---" + item.TypeSale, e, new System.Drawing.Font("Arial", 8), l_y, TextAlign.Left);

    //                        }
    //                        foreach (BecasData.Order.ItemOptionID option in item.Option)
    //                        {
    //                            string strName = "";
    //                            if (option.Qty > 1)
    //                            {
    //                                strName = new String('-', 2 * option.ItemLevel) + option.Name + "(" + option.Qty + ")" + "($" + mTransit.MoneyFortmat.Format(option.Price) + ")";
    //                            }
    //                            else
    //                            {
    //                                strName = new String('-', 2 * option.ItemLevel) + option.Name + "($" + mTransit.MoneyFortmat.Format(option.Price) + ")";
    //                            }
    //                            l_y = printer.DrawString(strName, e, new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Italic), l_y, TextAlign.Left);
    //                            item.Price += option.Price;

    //                            //l_y = printer.DrawString(new String('-', 2 * option.ItemLevel) + option.Name + "($" + mTransit.MoneyFortmat.Format(option.Price) + ")", e, new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Italic), l_y, TextAlign.Left);
    //                            //item.Price += option.Price;
    //                        }
    //                        if (item.isRefund == "1" && (item.ItemType != 1 || item.Qty != 0 || item.Price != 0))
    //                        {
    //                            printer.DrawString("Ref (-" + mTransit.MoneyFortmat.Format(item.Price * (1 - percent)) + ") ", e, new System.Drawing.Font("Arial", 11), yStart, TextAlign.Right);
    //                        }
    //                        else if (item.ItemType != 1 || item.Qty != 0 || item.Price != 0)
    //                        {
    //                            printer.DrawString(mTransit.MoneyFortmat.Format(item.Price), e, new System.Drawing.Font("Arial", 11), yStart, TextAlign.Right);
    //                        }

    //                        subTotal += item.Price;
    //                    }
    //                }
    //                if (data.Order.SubTotal > 0)
    //                {
    //                    percent = data.Order.Discount / data.Order.SubTotal;
    //                }
    //                l_y = printer.DrawLine("", null, e, System.Drawing.Drawing2D.DashStyle.Dot, l_y, TextAlign.Left);

    //                l_y += printer.GetHeightPrinterLine() / 2;

    //                if (data.Order.Refund > 0)
    //                {
    //                    printer.DrawString("Refund:", e, new System.Drawing.Font("Arial", 11), l_y, TextAlign.Left);
    //                    l_y = printer.DrawString("$" + mTransit.MoneyFortmat.Format(mOrder.Refund), e, new System.Drawing.Font("Arial", 11), l_y, TextAlign.Right);
    //                }

    //                System.Drawing.Font fontTotal = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
    //                if (data.Order.Banquet != 0 && data.Order.NumberOfBanquet != 0 && data.Order.NumberOfBanquet != 1)
    //                {
    //                    printer.DrawString("Total:", e, fontTotal, l_y, TextAlign.Left);
    //                    l_y = printer.DrawString("$" + mTransit.MoneyFortmat.Format(data.Order.Banquet), e, fontTotal, l_y, TextAlign.Right);
    //                    printer.DrawString("Number of Banquet:", e, fontTotal, l_y, TextAlign.Left);
    //                    l_y = printer.DrawString("" + data.Order.NumberOfBanquet, e, fontTotal, l_y, TextAlign.Right);
    //                    printer.DrawString("Total:", e, fontTotal, l_y, TextAlign.Left);
    //                    l_y = printer.DrawString("$" + mTransit.MoneyFortmat.Format(data.Order.SubTotal), e, fontTotal, l_y, TextAlign.Right);
    //                }
    //                else
    //                {
    //                    printer.DrawString("Total:", e, fontTotal, l_y, TextAlign.Left);
    //                    l_y = printer.DrawString("$" + mTransit.MoneyFortmat.Format(data.Order.SubTotal - data.Order.Refund), e, fontTotal, l_y, TextAlign.Right);
    //                }
    //                l_y += printer.GetHeightPrinterLine();

    //                if ((data.Order.Discount != 0 || data.Order.SurCharge != 0 || data.Order.Openning != 0) && !data.IsBuiltName)
    //                {
    //                    l_y += m_printer.GetHeightPrinterLine() / 2;
    //                    if (data.Order.Discount != 0)
    //                    {
    //                        fontTotal = new System.Drawing.Font("Arial", 11);
    //                        printer.DrawString("Discount:", e, fontTotal, l_y, TextAlign.Left);
    //                        l_y = printer.DrawString("$" + mTransit.MoneyFortmat.Format(data.Order.Discount), e, fontTotal, l_y, TextAlign.Right);
    //                    }

    //                    if (data.Order.SurCharge != 0)
    //                    {
    //                        fontTotal = new System.Drawing.Font("Arial", 11);
    //                        DrawString("Sur Charge:", e, fontTotal, l_y, TextAlign.Left);
    //                        l_y = DrawString("$" + mTransit.MoneyFortmat.Format(data.Order.SurCharge), e, fontTotal, l_y, TextAlign.Right);
    //                        data.Order.SurChargePercent = (int)(data.Order.SurCharge * 100 / data.Order.SubTotal);
    //                    }

    //                    if (data.Order.Openning != 0)
    //                    {
    //                        fontTotal = new System.Drawing.Font("Arial", 11);
    //                        DrawString("Openning:", e, fontTotal, l_y, TextAlign.Left);
    //                        l_y = DrawString("$" + mTransit.MoneyFortmat.Format(data.Order.Openning), e, fontTotal, l_y, TextAlign.Right);
    //                        data.Order.OpenningPercent = (int)(data.Order.Openning * 100 / data.Order.SubTotal);
    //                    }

    //                    fontTotal = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
    //                    printer.DrawString("Balance:", e, fontTotal, l_y, TextAlign.Left);
    //                    l_y = printer.DrawString("$" + mTransit.MoneyFortmat.Format(data.Order.SubTotal - data.Order.Discount - data.Order.Refund + data.Order.SurCharge - data.Order.Openning), e, fontTotal, l_y, TextAlign.Right);
    //                }

    //                fontTotal = new System.Drawing.Font("Arial", 11);
    //                printer.DrawString("GST(included in total):", e, fontTotal, l_y, TextAlign.Left);
    //                l_y = printer.DrawString("$" + mTransit.MoneyFortmat.Format((data.Order.SubTotal - data.Order.Discount - data.Order.Refund) / 11), e, fontTotal, l_y, TextAlign.Right);
    //                ///=========================================================================
    //                //l_y = DrawString(mTransit.MoneyFortmat.Format((mOrder.SubTotal-mOrder.Discount-GetTotalInclueRefund(list)) / 11), e, fontTotal, l_y, TextAlign.Right);
    //                //========================================================================

    //                if (data.Order.Deposit > 0 && data.Order.Refund == 0)
    //                {
    //                    fontTotal = new System.Drawing.Font("Arial", 11);
    //                    printer.DrawString("Deposit:", e, fontTotal, l_y, TextAlign.Left);
    //                    l_y = printer.DrawString("$" + mTransit.MoneyFortmat.Format(data.Order.Deposit), e, fontTotal, l_y, TextAlign.Right);
    //                }

    //                if (mTransit.DBConfig.EnableTip == 1)
    //                {
    //                    if (data.Order.Card > 0 && data.Order.Refund == 0)
    //                    {
    //                        fontTotal = new System.Drawing.Font("Arial", 11);
    //                        foreach (BecasData.Card ucCard in data.Order.ListCard)
    //                        {
    //                            printer.DrawString("* " + ucCard.CardName + " : ", e, new System.Drawing.Font("Arial", 13, System.Drawing.FontStyle.Bold), l_y, TextAlign.Left);
    //                            l_y = printer.DrawString(" ------------------------------", e, fontTotal, l_y, TextAlign.Right);
    //                            if (ucCard.Tip != 0)
    //                            {
    //                                printer.DrawString(" Sub Total : ", e, fontTotal, l_y, TextAlign.Left);
    //                                l_y = DrawString("$" + mTransit.MoneyFortmat.Format(ucCard.Subtotal), e, fontTotal, l_y, TextAlign.Right);
    //                                printer.DrawString(" Tip : ", e, fontTotal, l_y, TextAlign.Left);
    //                                l_y = printer.DrawString("$" + mTransit.MoneyFortmat.Format(ucCard.Tip), e, fontTotal, l_y, TextAlign.Right);
    //                            }
    //                            printer.DrawString(" Total : ", e, fontTotal, l_y, TextAlign.Left);
    //                            l_y = printer.DrawString("$" + mTransit.MoneyFortmat.Format(ucCard.Subtotal + ucCard.Tip), e, fontTotal, l_y, TextAlign.Right);
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    if (data.Order.Refund == 0)
    //                    {
    //                        if (data.Order.Card != 0)
    //                        {
    //                            fontTotal = new System.Drawing.Font("Arial", 11);
    //                            printer.DrawString("Card:", e, fontTotal, l_y, TextAlign.Left);
    //                            l_y = printer.DrawString("$" + mTransit.MoneyFortmat.Format(data.Order.Card), e, fontTotal, l_y, TextAlign.Right);
    //                        }
    //                    }
    //                }

    //                if (data.Order.Tips > 0)
    //                {
    //                    fontTotal = new System.Drawing.Font("Arial", 11);
    //                    printer.DrawString("Tips:", e, fontTotal, l_y, TextAlign.Left);
    //                    l_y = printer.DrawString("$" + mTransit.MoneyFortmat.Format(data.Order.Tips), e, fontTotal, l_y, TextAlign.Right);
    //                }

    //                try
    //                {
    //                    if (data.Order.CusID != 0 && data.Order.Refund == 0)
    //                    {
    //                        Connection.Connection conn = new Connection.Connection();
    //                        DataTable dtSourceCustomer = new DataTable();
    //                        dtSourceCustomer = conn.Select("SELECT c.Name,c.memberNo FROM customers c WHERE c.custID = " + data.Order.CusID);
    //                        l_y = printer.DrawString("ACC      # " + dtSourceCustomer.Rows[0]["memberNo"].ToString().Substring(0, 3) + "..." +
    //                            dtSourceCustomer.Rows[0]["memberNo"].ToString().Substring(dtSourceCustomer.Rows[0]["memberNo"].ToString().Length - 3, 3), e, font11, l_y, TextAlign.Left);
    //                        l_y = printer.DrawString("NAME # " + dtSourceCustomer.Rows[0]["Name"].ToString().ToUpper(), e, font11, l_y, TextAlign.Left);
    //                        if (data.Order.MemID != 0)
    //                        {
    //                            dtSourceCustomer = conn.Select("select Name from managemember where MemID ='" + data.Order.MemID + "'");
    //                            l_y = printer.DrawString("MEMBER NAME # " + dtSourceCustomer.Rows[0][0].ToString().ToUpper(), e, font11, l_y, TextAlign.Left);
    //                        }
    //                    }
    //                }
    //                catch
    //                {
    //                }
    //                if (data.Order.Account > 0 && data.Order.Refund == 0)
    //                {
    //                    fontTotal = new System.Drawing.Font("Arial", 11);
    //                    Connection.Connection conn = new Connection.Connection();
    //                    DataTable dt = new DataTable();
    //                    dt = conn.Select("select debt from customers where custID = '" + data.Order.CusID + "'");
    //                    DrawString("Openning Balance:", e, fontTotal, l_y, TextAlign.Left);
    //                    l_y = printer.DrawString("$" + mTransit.MoneyFortmat.Format((Convert.ToDouble(dt.Rows[0][0].ToString())) * -1), e, fontTotal, l_y, TextAlign.Right);

    //                    DrawString("Pay Amount:", e, fontTotal, l_y, TextAlign.Left);
    //                    l_y = printer.DrawString("$" + mTransit.MoneyFortmat.Format(data.Order.Account), e, fontTotal, l_y, TextAlign.Right);
    //                    DrawString("Closing Balance:", e, fontTotal, l_y, TextAlign.Left);
    //                    if (Convert.ToDouble(dt.Rows[0][0].ToString()) > 0)
    //                    {
    //                        l_y = printer.DrawString("$" + mTransit.MoneyFortmat.Format((data.Order.Account + Convert.ToDouble(dt.Rows[0][0].ToString())) * -1), e, fontTotal, l_y, TextAlign.Right);
    //                    }
    //                    else
    //                        l_y = printer.DrawString("$" + mTransit.MoneyFortmat.Format((data.Order.Account + Convert.ToDouble(dt.Rows[0][0].ToString())) * -1), e, fontTotal, l_y, TextAlign.Right);
    //                }

    //                if (data.Order.Tendered > 0 && data.Order.Refund == 0)
    //                {
    //                    fontTotal = new System.Drawing.Font("Arial", 11);
    //                    printer.DrawString("Cash:", e, fontTotal, l_y, TextAlign.Left);
    //                    l_y = printer.DrawString("$" + mTransit.MoneyFortmat.Format(data.Order.Tendered) + "", e, fontTotal, l_y, TextAlign.Right);
    //                }
    //                else
    //                {
    //                    double tendered = data.Order.Cash + data.Order.ChangeAmount;
    //                    data.Order.Tendered = tendered;
    //                    if (tendered > 0 && data.Order.Refund == 0)
    //                    {
    //                        fontTotal = new System.Drawing.Font("Arial", 11);
    //                        printer.DrawString("Cash:", e, fontTotal, l_y, TextAlign.Left);
    //                        l_y = printer.DrawString("$" + mTransit.MoneyFortmat.Format(data.Order.Tendered) + "", e, fontTotal, l_y, TextAlign.Right);
    //                    }
    //                }

    //                if (data.Order.ChangeAmount > 0 && data.Order.Refund == 0)
    //                {
    //                    fontTotal = new System.Drawing.Font("Arial", 11);
    //                    printer.DrawString("Change:", e, fontTotal, l_y, TextAlign.Left);
    //                    l_y = printer.DrawString("$" + mTransit.MoneyFortmat.Format(data.Order.ChangeAmount) + "", e, fontTotal, l_y, TextAlign.Right);
    //                }

    //                l_y += printer.GetHeightPrinterLine() * 2;

    //                if (data.Order.OpenningPercent > 0 && !data.IsBuiltName)
    //                {
    //                    fontTotal = new System.Drawing.Font("Arial", 11);
    //                    l_y = DrawString("Opening Discount: " + data.Order.OpenningPercent + "%", e, fontTotal, l_y, TextAlign.Center);
    //                }

    //                if (data.Order.SurChargePercent > 0 && !data.IsBuiltName)
    //                {
    //                    fontTotal = new System.Drawing.Font("Arial", 11);
    //                    l_y = DrawString("SurCharge: " + data.Order.SurChargePercent + "%", e, fontTotal, l_y, TextAlign.Center);
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                SystemLog.LogPOS.WriteLog(_nameClass + "printBuilt::" + ex.Message);
    //            }
    //        }
    //        l_y += m_printer.GetHeightPrinterLine();

    //        l_y = printer.DrawLine("", null, e, System.Drawing.Drawing2D.DashStyle.DashDot, l_y, TextAlign.Left);

    //        l_y += printer.GetHeightPrinterLine();

    //        l_y = printer.DrawString(mTransit.DBConfig.Website, e, new System.Drawing.Font("Arial", 9), l_y, TextAlign.Center);
    //        if (mTransit.ReadConfig.FooterMore != "")
    //        {
    //            l_y = printer.DrawString(mTransit.ReadConfig.FooterMore, e, new System.Drawing.Font("Arial", 9), l_y, TextAlign.Center);
    //        }
    //        l_y = printer.DrawString(mTransit.DBConfig.Thankyou, e, new System.Drawing.Font("Arial", 9), l_y, TextAlign.Center);
    //        printer.IsDrawLine = true;
    //    }
    //    public void printBuilt(bool isBuildName, BecasData.Order.Order order)
    //    {
    //        try
    //        {
    //            printData(new PrintData(2, 1, isBuildName, order.CoppyOrder()));
    //        }
    //        catch (Exception ex)
    //        {
    //            SystemLog.LogPOS.WriteLog(_nameClass + "printBuilt::" + ex.Message);
    //        }
    //    }

    //    /// <summary>
    //    /// Hàm print Kitchen or Bar nè
    //    /// </summary>
    //    /// <param name="printer"></param>
    //    /// <param name="e"></param>
    //    private void PrintChickenOrBar(POSPrinter printer, System.Drawing.Printing.PrintPageEventArgs e)
    //    {
    //        PrintData data = (PrintData)printer.Tag;

    //        float l_y = 0;
    //        printer.IsDrawLine = mTransit.ReadConfig.IsDrawLineBar;
    //        string head = data.CurrentPrinter.HeaderName;
    //        l_y += printer.GetHeightPrinterLine() * mTransit.ReadConfig.HeightHeader;

    //        l_y = printer.DrawString(head, e, new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold), l_y, TextAlign.Center);

    //        if (data.Function == 17)
    //        {
    //            l_y = printer.DrawString("(REPRINT)", e, new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold), l_y, TextAlign.Center);
    //        }

    //        l_y += printer.GetHeightPrinterLine();

    //        l_y = printer.DrawLine("", null, e, System.Drawing.Drawing2D.DashStyle.DashDotDot, l_y, TextAlign.Left);

    //        l_y += printer.GetHeightPrinterLine();

    //        if (mTransit.ReadConfig.EnableDateTimePrinter)
    //        {
    //            if (mTransit.DBConfig.Database.ToUpper() != "CRONULLA")
    //            {
    //                DateTime dt = DateTime.Now;
    //                string time = dt.ToShortTimeString();
    //                string date = dt.Day + "/" + dt.Month + "/" + dt.Year;
    //                l_y = printer.DrawString(date + " " + time, e, new System.Drawing.Font("Arial", 13), l_y, TextAlign.Left);
    //            }
    //            else
    //            {
    //                DateTime dt = DateTime.Now;
    //                string time = dt.ToShortTimeString();
    //                l_y = printer.DrawString(time, e, new System.Drawing.Font("Arial", 13), l_y, TextAlign.Left);
    //            }
    //        }

    //        if (mTransit.ReadConfig.EnableOperator)
    //        {
    //            try
    //            {
    //                l_y = printer.DrawString("OPERATOR#" + data.Order.EmployeeName, e, new System.Drawing.Font("Arial", 13), l_y, TextAlign.Left);
    //            }
    //            catch (Exception ex)
    //            {
    //                SystemLog.LogPOS.WriteLog(_nameClass + "PrintChickenOrBar::" + ex.Message);
    //            }
    //        }

    //        List<BecasData.Order.ItemOrderK> list = data.ListItemOrder;
    //        if (list.Count <= 0)
    //        {
    //            e.Cancel = true;
    //            return;
    //        }
    //        string strChangeType = "";
    //        bool ChangeType1 = false;
    //        bool ChangeType2 = false;
    //        foreach (BecasData.Order.ItemOrderK item in list)
    //        {
    //            if (item.ChangeStatus == 1)
    //            {
    //                ChangeType1 = true;
    //                strChangeType = "ADD";
    //            }
    //            else if (item.ChangeStatus == 2)
    //            {
    //                ChangeType2 = true;
    //                strChangeType = "REMOVE";
    //            }
    //            if (ChangeType1 && ChangeType2)
    //            {
    //                strChangeType = "ADD/REMOVE";
    //                break;
    //            }
    //        }

    //        l_y += printer.GetHeightPrinterLine() / 5;

    //        if (data.Order.PhoneType != 0)
    //        {
    //            switch (data.Order.PhoneType)
    //            {
    //                case 1:
    //                case 2:
    //                    l_y = printer.DrawString("PHONE ORDERS", e, new System.Drawing.Font("Arial", 13, FontStyle.Bold), l_y, TextAlign.Left);
    //                    break;

    //                case 3:
    //                    l_y = printer.DrawString("ORDER ONLINE", e, new System.Drawing.Font("Arial", 13, FontStyle.Bold), l_y, TextAlign.Left);
    //                    break;
    //            }
    //        }
    //        else
    //        {
    //            if (!data.Order.TableID.Contains("TKA-") && data.Order.TableID != "")
    //            {
    //                l_y = printer.DrawString("TABLE#   " + data.Order.TableID, e, new System.Drawing.Font("Arial", mTransit.ReadConfig.TableFontSize, FontStyle.Bold), l_y, TextAlign.Left);
    //            }
    //            else
    //            {
    //                l_y = printer.DrawString("TAKE AWAY", e, new System.Drawing.Font("Arial", 13, FontStyle.Bold), l_y, TextAlign.Left);
    //            }
    //        }
    //        if (mTransit.ReadConfig.EnableOrderIDPrinter)
    //            printer.DrawString("ORDER# " + data.Order.OrderID, e, new System.Drawing.Font("Arial", 12), l_y, TextAlign.Right);
    //        {
    //            if (!data.Order.TableID.Contains("W-"))
    //                if (data.Order.EmployeeName != "")
    //                    l_y = printer.DrawString("STAFF# " + data.Order.EmployeeName, e, new System.Drawing.Font("Arial", 12), l_y, TextAlign.Left);
    //        }
    //        if (strChangeType != "")
    //        {
    //            printer.DrawString(strChangeType, e, new System.Drawing.Font("Arial", 13), l_y, TextAlign.Left);
    //            if (mTransit.ReadConfig.EnablePeople)
    //            {
    //                if (data.Order.NumPeople > 0 && data.Order.PhoneType == 0)
    //                {
    //                    l_y = printer.DrawString(strChangeType, e, new System.Drawing.Font("Arial", 13), l_y, TextAlign.Left);
    //                }
    //            }
    //        }

    //        if (mTransit.ReadConfig.EnablePeople)
    //        {
    //            if (data.Order.NumPeople > 0 && data.Order.PhoneType == 0)
    //            {
    //                printer.DrawString("PEOPLE# " + data.Order.NumPeople, e, new System.Drawing.Font("Arial", 13), l_y, TextAlign.Left);
    //            }
    //        }

    //        if (data.Order.Banquet != 0 && data.Order.NumberOfBanquet != 0 && data.Order.NumberOfBanquet != 1)
    //        {
    //            printer.DrawString("Number of Banquet: " + data.Order.NumberOfBanquet, e, new System.Drawing.Font("Arial", 15, FontStyle.Bold), l_y, TextAlign.Left);
    //        }

    //        l_y += printer.GetHeightPrinterLine() * 2;
    //        FontStyle fontStyle = System.Drawing.FontStyle.Regular;
    //        foreach (BecasData.Order.ItemOrderK item in list)
    //        {
    //            float y_cancel_start = l_y;
    //            if (!mCurrentItemGroupTypeFirst)
    //            {
    //                if (mCurrentItemGroupType != item.ItemGroupType)
    //                {
    //                    //draw line
    //                    l_y = printer.DrawLineWithCheck("", null, e, System.Drawing.Drawing2D.DashStyle.Solid, l_y, TextAlign.Left, true);
    //                }
    //            }
    //            if (mCurrentItemGroupTypeFirst)
    //            {
    //                mCurrentItemGroupTypeFirst = false;
    //            }
    //            l_y = printer.DrawLine("", null, e, System.Drawing.Drawing2D.DashStyle.Dot, l_y, TextAlign.Left);
    //            if (item.ItemType == 1 && item.Qty == 0 && item.Price == 0)
    //            {
    //                fontStyle = CheckTA(item.Name);
    //                l_y = printer.DrawLine(e, new System.Drawing.Font("Arial", mTransit.ReadConfig.ItemFontSize), l_y, TextAlign.Left);
    //                l_y = printer.DrawString(item.Name, e, new System.Drawing.Font("Arial", Convert.ToInt32(mTransit.ReadConfig.BarOrKitChenFontSize), fontStyle), true, l_y, TextAlign.Left);
    //                l_y = printer.DrawLine(e, new System.Drawing.Font("Arial", mTransit.ReadConfig.ItemFontSize), l_y, TextAlign.Left);
    //            }
    //            else
    //            {
    //                if (item.SellSize != 1 && item.SellSize != 0)
    //                {
    //                    fontStyle = CheckTA(item.Name);
    //                    l_y = printer.DrawLine(e, new System.Drawing.Font("Arial", mTransit.ReadConfig.ItemFontSize), l_y, TextAlign.Left);
    //                    l_y = printer.DrawString(item.Qty + new String(' ', 3) + item.Name, e, new System.Drawing.Font("Arial", Convert.ToInt32(mTransit.ReadConfig.BarOrKitChenFontSize), fontStyle), l_y, TextAlign.Left);
    //                    l_y = printer.DrawLine(e, new System.Drawing.Font("Arial", mTransit.ReadConfig.ItemFontSize), l_y, TextAlign.Left);
    //                }
    //                else
    //                {
    //                    fontStyle = CheckTA(item.Name);
    //                    l_y = printer.DrawLine(e, new System.Drawing.Font("Arial", mTransit.ReadConfig.ItemFontSize), l_y, TextAlign.Left);
    //                    if (item.TypeSale != "")
    //                    {
    //                        l_y = printer.DrawString(item.Qty + new String(' ', 3) + item.Name + "---" + item.TypeSale, e, new System.Drawing.Font("Arial", Convert.ToInt32(mTransit.ReadConfig.BarOrKitChenFontSize), fontStyle), l_y, TextAlign.Left);
    //                    }
    //                    else
    //                        l_y = printer.DrawString(item.Qty + new String(' ', 3) + item.Name, e, new System.Drawing.Font("Arial", Convert.ToInt32(mTransit.ReadConfig.BarOrKitChenFontSize), GetFontStyleBold(mTransit.ReadConfig.FontStyleBoldItem)), l_y, TextAlign.Left);
    //                    l_y = printer.DrawLine(e, new System.Drawing.Font("Arial", mTransit.ReadConfig.ItemFontSize), l_y, TextAlign.Left);
    //                }


    //            }

    //            foreach (BecasData.Order.ItemOptionID option in item.Option)
    //            {
    //                l_y = printer.DrawLine(e, new System.Drawing.Font("Arial", mTransit.ReadConfig.ItemFontSize), l_y, TextAlign.Left);
    //                string strName;
    //                if (option.Qty > 1)
    //                {
    //                    strName = new String('-', 2 * option.ItemLevel) + option.Name + " (" + option.Qty + ")";
    //                }
    //                else
    //                {
    //                    strName = new String('-', 2 * option.ItemLevel) + option.Name;
    //                }
    //                l_y = printer.DrawString(strName, e, new System.Drawing.Font("Arial", Convert.ToInt32(mTransit.ReadConfig.BarOrKitChenFontSize) + 1, GetFontStyleBold(mTransit.ReadConfig.FontStyleBoldLine)), l_y, TextAlign.Left);
    //                l_y = printer.DrawLine(e, new System.Drawing.Font("Arial", mTransit.ReadConfig.ItemFontSize), l_y, TextAlign.Left);
    //            }

    //            if (item.ChangeStatus == 2)
    //            {
    //                printer.DrawCancelLine(e, y_cancel_start, l_y);
    //            }
    //        }
    //        l_y = printer.DrawLine("", null, e, System.Drawing.Drawing2D.DashStyle.Dot, l_y, TextAlign.Left);
    //        int itemcount = 0;
    //        for (int i = 0; i < list.Count; i++)
    //        {
    //            itemcount += list[i].Qty;
    //        }
    //        l_y += printer.GetHeightPrinterLine() * 2;
    //        if (mTransit.ReadConfig.EnableTotalItemPrinter)
    //        {
    //            string total = "TOTAL ITEMS:" + itemcount;
    //            System.Drawing.Font l_font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
    //            l_y = printer.DrawLine(total, l_font, e, System.Drawing.Drawing2D.DashStyle.Custom, l_y, TextAlign.Left);
    //            l_y = printer.DrawString(total, e, l_font, l_y, TextAlign.Left);
    //            l_y = printer.DrawLine(total, l_font, e, System.Drawing.Drawing2D.DashStyle.Custom, l_y, TextAlign.Left);
    //        }
    //        l_y += printer.GetHeightPrinterLine() * mTransit.ReadConfig.HeightBottom;
    //        l_y = printer.DrawString("-", e, new System.Drawing.Font("Arial", 12), l_y, TextAlign.Center);
    //    }

    //    public System.Drawing.FontStyle GetFontStyleBold(bool value)
    //    {
    //        if (value)
    //            return System.Drawing.FontStyle.Bold;
    //        else
    //            return FontStyle.Regular;
    //    }

    //    public void printData(PrintData data)
    //    {
    //        if (data.PrintType == PRINT_BAR_KITCHEN)
    //        {
    //            Connection.Connection con;                
    //            for (int i = 0; i < data.POSPrintDevice.Count; i++)
    //            {
    //                try
    //                {
    //                    data.CurrentPrinter = data.POSPrintDevice[i];
    //                    data.ListItemOrder = getListOrder(data);
    //                    if (data.ListItemOrder.Count > 0)
    //                    {
    //                        con = new Connection.Connection();
    //                        try
    //                        {
    //                            con.Open();
    //                            string sql =
    //                                "insert into kds_order(orderID,tableID,status,staffID,printerType)" +
    //                                "values(" + data.Order.OrderID + ",'" + data.Order.TableID + "',0," + data.Order.StaffID + "," + data.POSPrintDevice[i].ID + ")";
    //                            con.ExecuteNonQuery(sql);
    //                            int kdsId = Convert.ToInt32(con.ExecuteScalar("select LAST_INSERT_ID();"));

    //                            foreach (var item in data.ListItemOrder)
    //                            {
    //                                sql = "insert into kds_order_line(itemID,qty,kds_order_id,itemName,status)" +
    //                                        "values(" + item.ItemID + "," + item.Qty + "," + kdsId + ",'" + item.Name + "',"+item.ChangeStatus+")";
    //                                con.ExecuteNonQuery(sql);
    //                                foreach (var option in item.Option)
    //                                {
    //                                    sql = "insert into kds_order_line(itemID,qty,kds_order_id,itemName,status,itemLevel)" +
    //                                        "values(" + option.OptionID + "," + option.Qty + "," + kdsId + ",'" + option.Name + "',"+item.ChangeStatus+",1)";
    //                                    con.ExecuteNonQuery(sql);
    //                                }
    //                            }                                
    //                        }
    //                        catch (Exception ex)
    //                        {
    //                            SystemLog.LogPOS.WriteLog("printData::"+ex.Message);
    //                            con.Close();
    //                        }
    //                        for (int j = 0; j < data.POSPrintDevice[i].NumOfTicket; j++)
    //                        {
    //                            if (data.Order.TableID.ToString().Contains("TKA") && data.POSPrintDevice[i].bAllowPrintTKA == false)
    //                            {
    //                                break;
    //                            }
    //                            PrintOnEachPrinterThread(data);
    //                        }
    //                    }
    //                }
    //                catch (Exception ex)
    //                {
    //                    SystemLog.LogPOS.WriteLog(_nameClass + "printData::" + ex.Message);
    //                }
    //            }
    //            con = new Connection.Connection();
    //            try
    //            {
    //                con.Open();
    //                con.ExecuteNonQuery("update config set `value`=`value`+1 where varname='POS_KDS'");
    //            }
    //            catch
    //            {
    //                con.Close();
    //            }
    //        }
    //        else
    //        {
    //            data.IsPrintBill = true;
    //            PrintOnEachPrinterThread(data);
    //        }
    //    }

    //    public void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    //    {
    //        POSPrinter printer = (POSPrinter)sender;
    //        if (printer.Tag != null)
    //        {
    //            PrintData data = (PrintData)printer.Tag;
    //            if (data.Order.TableID != null)
    //            {
    //                if (data.IsPrintBill)
    //                {
    //                    printBuilt(printer, e);
    //                }
    //                else
    //                {
    //                    PrintChickenOrBar(printer, e);
    //                }
    //            }
    //            else
    //            {
    //                e.Cancel = true;
    //            }
    //        }
    //    }

    //    public void printer_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    //    {
    //        POSPrinter lprinter = (POSPrinter)sender;
    //        string[] list = (string[])lprinter.Tag;
    //        System.Drawing.Font font11 = new System.Drawing.Font("Arial", 11);
    //        float l_y = 0;
    //        DateTime dateTime = DateTime.Now;

    //        lprinter.DrawString(dateTime.Day + "/" + dateTime.Month + "/" + dateTime.Year + " " + dateTime.ToShortTimeString(), e, font11, l_y, TextAlign.Right);
    //        l_y += lprinter.GetHeightPrinterLine() * 2;

    //        l_y = lprinter.DrawLine("", null, e, System.Drawing.Drawing2D.DashStyle.Dash, l_y, TextAlign.Left);
    //        l_y += lprinter.GetHeightPrinterLine();

    //        l_y = lprinter.DrawString("TRANSFER TABLE", e, new Font("Arial", 15, FontStyle.Bold), l_y, TextAlign.Center);
    //        l_y += lprinter.GetHeightPrinterLine();
    //        l_y = lprinter.DrawString("FROM : " + list[0], e, font11, l_y, TextAlign.Left);
    //        l_y += lprinter.GetHeightPrinterLine();
    //        l_y = lprinter.DrawString("TO : " + list[1], e, font11, l_y, TextAlign.Left);
    //    }

    //    public void printer_PrintPageJoinTable(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    //    {
    //        POSPrinter lprinter = (POSPrinter)sender;
    //        string[] list = (string[])lprinter.Tag;
    //        System.Drawing.Font font11 = new System.Drawing.Font("Arial", 11);
    //        float l_y = 0;
    //        DateTime dateTime = DateTime.Now;

    //        lprinter.DrawString(dateTime.Day + "/" + dateTime.Month + "/" + dateTime.Year + " " + dateTime.ToShortTimeString(), e, font11, l_y, TextAlign.Right);
    //        l_y += lprinter.GetHeightPrinterLine() * 2;

    //        l_y = lprinter.DrawLine("", null, e, System.Drawing.Drawing2D.DashStyle.Dash, l_y, TextAlign.Left);
    //        l_y += lprinter.GetHeightPrinterLine();

    //        l_y = lprinter.DrawString("JOIN TABLE", e, new Font("Arial", 15, FontStyle.Bold), l_y, TextAlign.Center);
    //        l_y += lprinter.GetHeightPrinterLine();
    //        l_y = lprinter.DrawString("FROM TABLE LIST: " + list[0], e, font11, l_y, TextAlign.Left);
    //        l_y += lprinter.GetHeightPrinterLine();
    //        l_y = lprinter.DrawString("TO TABLE: " + list[1], e, font11, l_y, TextAlign.Left);
    //    }

    //    public void PrintOnEachPrinter(PrintData data)
    //    {
    //        if (data.PrintType == PRINT_BAR_KITCHEN)
    //        {
    //            POSPrinter printer = new POSPrinter(mTransit);
    //            printer.Tag = data;
    //            printer.SetPrinterName(data.CurrentPrinter.PrinterName);
    //            printer.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument_PrintPage);
    //            printer.Print();
    //        }
    //        else
    //        {
    //            POSPrinter printer = new POSPrinter(mTransit);
    //            printer.Tag = data;
    //            printer.SetPrinterName(data.Function == 14 ? mTransit.ReadConfig.PDABillPrinter : mTransit.ReadConfig.BillPrinter);
    //            printer.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument_PrintPage);
    //            printer.Print();
    //        }
    //    }

    //    public void PrintOnEachPrinterThread(PrintData data)
    //    {
    //        PrintOnEachPrinter(data);
    //    }

    //    public void printSliptBill(BecasData.Order.Order mOrder)
    //    {
    //        PrintData p = new PrintData(2, 1, false, mOrder.CoppyOrder());
    //        p.IsSliptBill = true;
    //        printData(p);
    //    }

    //    public void PrintTicketJoin(string from, string to)
    //    {
    //        string[] list = { from, to };

    //        POSPrinter printer = new POSPrinter(mTransit);
    //        printer.Tag = list;
    //        printer.SetPrinterName(mTransit.DBConfig.BillPrinter);
    //        printer.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printer_PrintPageJoinTable);
    //        printer.Print();
    //    }

    //    public void PrintTicketTransfer(string from, string to)
    //    {
    //        string[] list = { from, to };
    //        POSPrinter printer = new POSPrinter(mTransit);
    //        printer.Tag = list;
    //        printer.SetPrinterName(mTransit.DBConfig.BillPrinter);
    //        printer.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printer_PrintPage);
    //        printer.Print();
    //    }

    //    public void RePrintOrderOnline(int OrderID)
    //    {
    //        //mSocketServer.RePrintOrderOnline(OrderID);
    //    }

    //    public List<string> SplitStringLine(string str, System.Drawing.Printing.PrintPageEventArgs e, System.Drawing.Font font)
    //    {
    //        List<string> list = new List<string>();
    //        string[] s = str.Split(' ');
    //        string resuilt = "";
    //        for (int i = 0; i < s.Length; i++)
    //        {
    //            if (s[i].Length > 0)
    //            {
    //                if (e.Graphics.MeasureString(resuilt + s[i], font).Width > e.PageBounds.Width && resuilt.Length > 0)
    //                {
    //                    list.Add(resuilt);
    //                    i--;
    //                    resuilt = "";
    //                }
    //                else if (e.Graphics.MeasureString(s[i], font).Width > e.PageBounds.Width)
    //                {
    //                    list.Add(s[i]);
    //                    resuilt = "";
    //                }
    //                else
    //                {
    //                    resuilt += s[i] + " ";
    //                }
    //            }
    //        }
    //        if (resuilt.Length > 0)
    //        {
    //            list.Add(resuilt);
    //        }
    //        return list;
    //    }
    //}
}