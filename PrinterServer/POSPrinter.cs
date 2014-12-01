using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;

namespace PrinterServer
{
    class POSPrinter:PrintDocument
    {        
        public object _Tag { get; set; }

        public POSPrinter()
        {
            this.PrintController = new StandardPrintController();                                                
        }

        public void POSSetPrinterName(string name)
        {
            this.PrinterSettings.PrinterName = name;
        }
        /// <summary>
        /// Ve mot chuoi text
        /// </summary>
        /// <param name="s">chuoi can ve</param>
        /// <param name="e"></param>
        /// <param name="font"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public float POSDrawMessenge(string s, System.Drawing.Printing.PrintPageEventArgs e, Color color, System.Drawing.Font font, float y,TextAlign align,float margin)
        {
            while (s.Contains("  "))
            {
                s = s.Replace("  ", " ");
            }
            string[] list = s.Split(' ');
            string resuilt = "";
            for (int i = 0; i < list.Length; i++)            
            {
                string data = list[i];
                if (e.Graphics.MeasureString(resuilt + " " + data, font).Width > POSGetWidthPrinter(e))
                {
                    y = POSDrawString(resuilt, e, font, color, y, align, margin);
                    resuilt = data;
                }
                else
                {
                    resuilt += data + " ";
                    if (i == (list.Length - 1))
                    {
                        y = POSDrawString(resuilt, e, font, color, y, align, margin);
                    }
                }
            }
            SizeF size = e.Graphics.MeasureString(s, font);
            int x = (int)size.Width;
            return y;
        }

        public int POSGetHeightPrinterLine()
        {
            string s = PrinterSettings.PrinterName.ToUpper();
            if (s.Contains("PRP"))
            {
                return 100;
            }
            else
            {
                return 10;
            }
        }
        public float POSDrawImage(Image img, System.Drawing.Printing.PrintPageEventArgs e, float x, float y, float width,float height)
        {
            e.Graphics.DrawImage(img, x, y,width,height);
            return y += height;
        }
        public float POSDrawString(string s, System.Drawing.Printing.PrintPageEventArgs e, System.Drawing.Font font, Color color, float x,float y,float width, TextAlign textAlign)
        {            
            List<string> list = POSSplitStringLine(s,width,e, font);
            foreach (string item in list)
            {
                switch (textAlign)
                {                    
                    case TextAlign.Center:
                        x += (float)Math.Abs((width - e.Graphics.MeasureString(item, font).Width) / 2);
                        break;
                    case TextAlign.Right:
                        x += width - e.Graphics.MeasureString(item, font).Width;
                        break;
                    default:
                        break;
                }
                e.Graphics.DrawString(item, font, new SolidBrush(color), x, y);
                y += e.Graphics.MeasureString(item, font).Height;
            }

            return y;
        }
        public float POSDrawString(string s, System.Drawing.Printing.PrintPageEventArgs e, System.Drawing.Font font, Color color, float y, TextAlign textAlign, float margin)
        {
            float x = 0;
            List<string> list = POSSplitStringLine(s, e, font);
            foreach (string item in list)
            {
                switch (textAlign)
                {
                    case TextAlign.Left:
                        x = margin;
                        break;
                    case TextAlign.Center:
                        x = (float)Math.Abs((POSGetWidthPrinter(e) - e.Graphics.MeasureString(item, font).Width) / 2);
                        break;
                    case TextAlign.Right:
                        x = (POSGetWidthPrinter(e) - margin) - e.Graphics.MeasureString(item, font).Width;
                        break;
                    default:
                        break;
                }                    
                e.Graphics.DrawString(item, font, new SolidBrush(color), x, y);                                
                y += e.Graphics.MeasureString(item, font).Height;
            }

            return y;
        }
        public void POSDrawCancelLine(System.Drawing.Printing.PrintPageEventArgs e, float y_start, float y_end,float margin)
        {
            System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Brushes.Black);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            //float y=(y_start + y_end) / 2-3;
            e.Graphics.DrawLine(pen, margin, y_start, POSGetWidthPrinter(e)- margin, y_end);
            e.Graphics.DrawLine(pen, margin, y_end, POSGetWidthPrinter(e) - margin, y_start);
            //POSDrawLine(e, Color.Black, System.Drawing.Drawing2D.DashStyle.Dash, y, 1, TextAlign.Center, margin);            
            //POSDrawLine(e, Color.Black, System.Drawing.Drawing2D.DashStyle.Dash, y+6, 1, TextAlign.Center, margin);
        }
        public void POSDrawLine(System.Drawing.Printing.PrintPageEventArgs e, Color color, System.Drawing.Drawing2D.DashStyle dashStyle, float y, float fFloat, TextAlign textAlign,float margin)
        {
            float realWidth = POSGetWidthPrinter(e) - margin * 2;
            float width = POSGetWidthPrinter(e) * fFloat;
            if (width > realWidth)
            {
                width = realWidth;
            }
            float x = POSGetXWithAlign(e, textAlign, width,margin);
            System.Drawing.Pen pen = new System.Drawing.Pen(color);
            pen.DashStyle = dashStyle;                     
            e.Graphics.DrawLine(pen, x, y, x + width, y);            
        }
        public float POSGetWidthPrinter(System.Drawing.Printing.PrintPageEventArgs e)
        {            
            return e.PageSettings.PrintableArea.Width;
            //return e.PageSettings.PaperSize.Width - e.PageSettings.Margins.Left - e.PageSettings.Margins.Right;
        }
        private float POSGetXWithAlign(System.Drawing.Printing.PrintPageEventArgs e, TextAlign textAlign, float width,float margin)
        {
            float x=0;            
            switch (textAlign)
            {
                case TextAlign.Left:
                    x = margin;
                    break;
                case TextAlign.Center:
                    x = (float)Math.Abs((POSGetWidthPrinter(e) - width) / 2);
                    break;
                case TextAlign.Right:
                    x = POSGetWidthPrinter(e) - width - margin;
                    break;
                default:
                    break;
            }
            return x;
        }
        public float POSDrawBarcode(string dataBarcode, System.Drawing.Printing.PrintPageEventArgs e, float y, Color color, TextAlign textAlign)
        {
            if (dataBarcode.Length != 12)
            {
                y = POSDrawString("", e, new Font("Arial", 11), color, y, textAlign, 0);
                return y;
            }
            Ean13 ean13 = new Ean13(dataBarcode);
            float x = 0;
            switch (textAlign)
            {
                case TextAlign.Left:
                    x = 0;
                    break;
                case TextAlign.Center:
                    x = POSGetWidthPrinter(e) - ean13.Width;
                    if (x < 0)
                    {
                        x = 0;
                    }
                    x = x / 2;
                    break;
                case TextAlign.Right:
                    x = POSGetWidthPrinter(e) - ean13.Width;
                    if (x < 0)
                    {
                        x = 0;
                    }
                    break;
                default:
                    break;
            }
            ean13.DrawEan13Barcode(e.Graphics, new Point((int)x, (int)y));
            y += ean13.Height + 2;
            return y;
        }
        private List<string> POSSplitStringLine(string str, System.Drawing.Printing.PrintPageEventArgs e, System.Drawing.Font font)
        {
            List<string> list = new List<string>();
            string[] s = str.Split(' ');
            string resuilt = "";            
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Length > 0)
                {
                    if (e.Graphics.MeasureString(resuilt + s[i], font).Width > POSGetWidthPrinter(e) && resuilt.Length > 0)
                    {
                        list.Add(resuilt);
                        i--;
                        resuilt = "";
                    }
                    else if (e.Graphics.MeasureString(s[i], font).Width > POSGetWidthPrinter(e))
                    {
                        list.Add(s[i]);
                        resuilt = "";
                    }
                    else
                    {
                        resuilt += s[i] + " ";
                    }
                }
            }
            if (resuilt.Length > 0)
            {
                list.Add(resuilt);
            }
            return list;
        }
        private List<string> POSSplitStringLine(string str,float width ,System.Drawing.Printing.PrintPageEventArgs e, System.Drawing.Font font)
        {
            List<string> list = new List<string>();
            string[] s = str.Split(' ');
            string resuilt = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Length > 0)
                {
                    if (e.Graphics.MeasureString(resuilt + s[i], font).Width > width && resuilt.Length > 0)
                    {
                        list.Add(resuilt);
                        i--;
                        resuilt = "";
                    }
                    else if (e.Graphics.MeasureString(s[i], font).Width > width)
                    {
                        list.Add(s[i]);
                        resuilt = "";
                    }
                    else
                    {
                        resuilt += s[i] + " ";
                    }
                }
            }
            if (resuilt.Length > 0)
            {
                list.Add(resuilt);
            }
            return list;
        }
        public float POSGetFloat(float f)
        {
            return f;
        }
    }
}
