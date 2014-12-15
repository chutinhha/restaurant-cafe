using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace ProcessOrder
{
    public class ProcessOrder
    {
        private Data.BOBanHang mBanHang;
        private Data.BOMenuGia mBOMenuGia;
        private Data.BOQuanLyKho mBOQuanLyKho;
        private Data.Transit mTransit;
        private PrinterServer.ProcessPrinter mProcessPrinter;
        private PriceManager mPriceManager;
        public Data.BOChiTietBanHang CurrentChiTietBanHang { get; set; }
        public Data.BANHANG BanHang
        {
            get { return mBanHang.BANHANG; }
        }
        public List<Data.BOChiTietBanHang> ListChiTietBanHang
        {
            get { return mBanHang._ListChiTietBanHang; }
        }
        public List<Data.BOMenuGia> _ListMenuGia
        {
            get { return mPriceManager._ListMenuGia; }
        }
        public ProcessOrder(Data.Transit transit)
        {
            mTransit = transit;
            mPriceManager = new PriceManager(mTransit);
            mBOMenuGia = new Data.BOMenuGia(mTransit);
            mBanHang = new Data.BOBanHang(mTransit);
            mBOQuanLyKho = new Data.BOQuanLyKho(mTransit);
            mBanHang.LoadBanHang();
            mProcessPrinter = new PrinterServer.ProcessPrinter(mTransit);
        }
        public Data.BOBanHang GetBanHang()
        {
            return mBanHang;
        }
        public int DongBan()
        {
            return mBanHang.DongBan();
        }
        public int SendOrder()
        {
            int lichSuBanHangId = mBanHang.GuiNhaBep();
            //mBOQuanLyKho.LuuTonKho(mBanHang._ListChiTietBanHang);
            if (lichSuBanHangId > 0)
            {
                mProcessPrinter.InHoaDon(lichSuBanHangId);
            }
            return lichSuBanHangId;
        }
        public int TamTinh()
        {
            int lichSuBanHangID = mBanHang.GuiNhaBep();
            if (lichSuBanHangID > 0)
            {
                mProcessPrinter.InHoaDon(lichSuBanHangID);
            }
            int banHangID = mBanHang.TamTinh();
            if (banHangID > 0)
            {
                mProcessPrinter.InBill(PrinterServer.PrinterBillOrder.PrinterBillOrderType.TamTinh, banHangID);
            }
            return banHangID;
        }
        public void TinhTien()
        {
            int lichSuBanHangID = mBanHang.GuiNhaBep();
            if (lichSuBanHangID > 0)
            {
                mProcessPrinter.InHoaDon(lichSuBanHangID);
            }
            int banHangID = mBanHang.TinhTien();
            if (banHangID > 0)
            {
                mProcessPrinter.InBill(PrinterServer.PrinterBillOrder.PrinterBillOrderType.HoaDon, banHangID);
            }
        }
        public int HuyBan()
        {
            return mBanHang.HuyBan();
        }
        public bool KiemTraHoaDonDaHoanThanh()
        {
            if (mBanHang.BANHANG.TrangThaiID == 4 || mBanHang.BANHANG.TrangThaiID == 0)
            {
                return true;
            }
            return false;
        }
        public int KiemTraDanhSachMon()
        {
            int count = 0;
            foreach (var item in mBanHang._ListChiTietBanHang)
            {
                count++;
            }
            return count;
        }
        public void XoaChiTietBanHang(Data.BOChiTietBanHang chitiet)
        {
            mBanHang.DeleteChiTietBanHang(chitiet);
        }
        public void XoaAllXoaChiTietBanHang()
        {
            mBanHang.XoaAllXoaChiTietBanHang();
        }
        public int AddChiTietBanHang(Data.BOChiTietBanHang chitiet)
        {
            //mPriceManager.LoadPrice(chitiet);
            return mBanHang.AddChiTietBanHang(chitiet);
        }
        public bool KiemTraKho(Data.BOChiTietBanHang chitiet)
        {
            return mBOQuanLyKho.KiemTraTonKhoTong(mTransit, chitiet) >= chitiet.ChiTietBanHang.SoLuongBan ? true : false;
        }
        public bool CheckMutiablePrice(Data.BOChiTietBanHang chitiet)
        {
            return mPriceManager.CheckMutiablePrice(chitiet);
        }
    }
}
