using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrinterServer
{
    class PrinterFont
    {
        private static string FONT_NAME = "Times New Roman";
        private Data.BOXuliMayIn mBOXuliMayIn;
        public System.Drawing.Font FontHeader1;
        public System.Drawing.Font FontHeader2;
        public System.Drawing.Font FontHeader3;
        public System.Drawing.Font FontHeader4;
        public System.Drawing.Font FontTitle;
        public System.Drawing.Font FontInfo;
        public System.Drawing.Font FontItemHeader;
        public System.Drawing.Font FontItemBody;
        public System.Drawing.Font FontItemBodyNote;
        public System.Drawing.Font FontSum;
        public System.Drawing.Font FontBig;
        public System.Drawing.Font FontFooter1;
        public System.Drawing.Font FontFooter2;
        public System.Drawing.Font FontFooter3;
        public System.Drawing.Font FontFooter4;
        public PrinterFont(Data.BOXuliMayIn xuli)
        {
            mBOXuliMayIn = xuli;
            FontHeader1 = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextFontSize1, (System.Drawing.FontStyle)mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextFontStyle1);
            FontHeader2 = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextFontSize2, (System.Drawing.FontStyle)mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextFontStyle2);
            FontHeader3 = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextFontSize3, (System.Drawing.FontStyle)mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextFontStyle3);
            FontHeader4 = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextFontSize4, (System.Drawing.FontStyle)mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextFontStyle4);

            FontTitle = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.TitleTextFontSize, (System.Drawing.FontStyle)mBOXuliMayIn._CAIDATMAYINHOADON.TitleTextFontStyle);
            FontInfo = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.InfoTextFontSize, (System.Drawing.FontStyle)mBOXuliMayIn._CAIDATMAYINHOADON.InfoTextFontStyle);
            FontItemHeader = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.ItemFontSize, System.Drawing.FontStyle.Bold);
            FontItemBody = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.ItemFontSize, System.Drawing.FontStyle.Regular);
            FontItemBodyNote = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.ItemFontSize, System.Drawing.FontStyle.Italic);
            FontSum = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.SumanyFontSize, (System.Drawing.FontStyle)mBOXuliMayIn._CAIDATMAYINHOADON.SumanyFontStyle);
            FontBig = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.SumanyFontSizeBig, (System.Drawing.FontStyle)mBOXuliMayIn._CAIDATMAYINHOADON.SumanyFontStyleBig);
            FontFooter1 = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextFontSize1, (System.Drawing.FontStyle)mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextFontStyle1);
            FontFooter2 = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextFontSize2, (System.Drawing.FontStyle)mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextFontStyle2);
            FontFooter3 = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextFontSize3, (System.Drawing.FontStyle)mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextFontStyle3);
            FontFooter4 = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextFontSize4, (System.Drawing.FontStyle)mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextFontStyle4);
        }
    }
}
