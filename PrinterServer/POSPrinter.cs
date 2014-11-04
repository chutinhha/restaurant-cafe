using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;

namespace PrintServer
{
    class POSPrinter:PrintDocument
    {
        public object _Tag { get; set; }

        public POSPrinter()
        {
            this.PrintController = new StandardPrintController();
        }

        public void SetPrinterName(string name)
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
        public float DrawMessenge(string s, System.Drawing.Printing.PrintPageEventArgs e,Color color, System.Drawing.Font font, float y)
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
                if (e.Graphics.MeasureString(resuilt + " " + data, font).Width > e.PageBounds.Width)
                {                    
                    y = DrawString(resuilt, e, font, color, y, TextAlign.Left, 0);
                    resuilt = data;
                }
                else
                {
                    resuilt += data + " ";
                    if (i == (list.Length - 1))
                    {                        
                        y = DrawString(resuilt, e, font, color, y, TextAlign.Left, 0);
                    }
                }
            }
            SizeF size = e.Graphics.MeasureString(s, font);
            int x = (int)size.Width;
            return y;
        }

        public int GetHeightPrinterLine()
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

        public float DrawString(string s, System.Drawing.Printing.PrintPageEventArgs e, System.Drawing.Font font, Color color, float y, TextAlign textAlign, int MarginsRight)
        {
            float x = 0;
            List<string> list = SplitStringLine(s, e, font);
            foreach (string item in list)
            {
                switch (textAlign)
                {
                    case TextAlign.Left:
                        x = 0;
                        break;
                    case TextAlign.Center:
                        x = (float)Math.Abs(((float)e.PageBounds.Width - e.Graphics.MeasureString(item, font).Width) / 2);
                        break;
                    case TextAlign.Right:
                        x = (e.PageBounds.Width - MarginsRight) - e.Graphics.MeasureString(item, font).Width;
                        break;
                    default:
                        break;
                }                                
                e.Graphics.DrawString(item, font, new SolidBrush(color), x, y);
                y += e.Graphics.MeasureString(item, font).Height;
            }

            return y;
        }        
        public void DrawCancelLine(System.Drawing.Printing.PrintPageEventArgs e, float y_start, float y_end)
        {
            System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Brushes.Black);            
            float y=(y_start + y_end) / 2;
            e.Graphics.DrawLine(pen, 0, y, e.PageBounds.Width, y);
            e.Graphics.DrawLine(pen, 0, y+3, e.PageBounds.Width, y+3);
        }
        public void DrawLine(System.Drawing.Printing.PrintPageEventArgs e,Color color, System.Drawing.Drawing2D.DashStyle dashStyle, float y,float fFloat, TextAlign textAlign)
        {
            float width = e.PageBounds.Width * fFloat;
            float x = GetXWithAlign(e,textAlign,width);
            System.Drawing.Pen pen = new System.Drawing.Pen(color);
            e.Graphics.DrawLine(pen, x, y, x + width, y);
        }
        
        private float GetXWithAlign(System.Drawing.Printing.PrintPageEventArgs e, TextAlign textAlign, float width)
        {
            float x=0;
            switch (textAlign)
            {
                case TextAlign.Left:
                    x = 0;
                    break;
                case TextAlign.Center:
                    x = (float)Math.Abs(((float)e.PageBounds.Width - width) / 2);
                    break;
                case TextAlign.Right:
                    x = e.PageBounds.Width - width;
                    break;
                default:
                    break;
            }
            return x;
        }        
        public float DrawBarcode(string dataBarcode, System.Drawing.Printing.PrintPageEventArgs e, float y,Color color, TextAlign textAlign)
        {
            if (dataBarcode.Length != 12)
            {
                y = DrawString("", e, new Font("Arial", 11), color, y, textAlign, 0);
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
                    x = e.PageBounds.Width - ean13.Width;
                    if (x < 0)
                    {
                        x = 0;
                    }
                    x = x / 2;
                    break;
                case TextAlign.Right:
                    x = e.PageBounds.Width - ean13.Width;
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
        private List<string> SplitStringLine(string str, System.Drawing.Printing.PrintPageEventArgs e, System.Drawing.Font font)
        {
            List<string> list = new List<string>();
            string[] s = str.Split(' ');
            string resuilt = "";
            //float width = e.Graphics.MeasureString("ADD",font).Width;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Length > 0)
                {
                    if (e.Graphics.MeasureString(resuilt + s[i], font).Width > e.PageBounds.Width && resuilt.Length > 0)
                    {
                        list.Add(resuilt);
                        i--;
                        resuilt = "";
                    }
                    else if (e.Graphics.MeasureString(s[i], font).Width > e.PageBounds.Width)
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
    }
}
