﻿using System;
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
        //private Data.BOQuanLyKho mBOQuanLyKho;
        private Data.Transit mTransit;
        private Data.KaraokeEntities mKaraokeEntities;
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
            mKaraokeEntities = new Data.KaraokeEntities();
            mPriceManager = new PriceManager(mTransit,mKaraokeEntities);
            mBOMenuGia = new Data.BOMenuGia(mTransit);
            mBanHang = new Data.BOBanHang(mTransit,mKaraokeEntities);
            //mBOQuanLyKho = new Data.BOQuanLyKho(mTransit);
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
        public bool KiemTraGioKaraoke(int? monID)
        {
            return mBanHang.KiemTraGioKaraoke(monID);
        }
        public int AddChiTietBanHang(Data.BOChiTietBanHang chitiet)
        {
            //mPriceManager.LoadPrice(chitiet);   
            chitiet.LoadKhuyenMai(mKaraokeEntities);
            return mBanHang.AddChiTietBanHang(chitiet);
        }                
        public Data.BOMenuKichThuocMon LayMonKaraoke(int? monID)
        {
            return mBanHang.LayMonKaraoke(monID);
        }
        public Data.BOMenuKichThuocMon LayChiTietTheoMaVach(string mavach)
        {
            return Data.BOMenuKichThuocMon.GetKTMByBarcode(mavach,mKaraokeEntities); ;
        }
        public void TinhGioKaraoke(Data.BOChiTietBanHang chitiet)
        {
            //TimeSpan ts = DateTime.Now - mBanHang.BANHANG.NgayBan.Value;
            //if (mTransit.CaiDatBanHang.SoPhutToiThieu==0)
            //{
            //    mTransit.CaiDatBanHang.SoPhutToiThieu = 1;
            //}
            //int sogiay = mTransit.CaiDatBanHang.SoPhutToiThieu * 60;
            //int time = (int)ts.TotalSeconds/sogiay;
            //time = time * sogiay;
            //if (time<ts.TotalSeconds)
            //{
            //    time += sogiay;
            //}
            chitiet.ChiTietBanHang.KichThuocLoaiBan = Utilities.DateTimeConverter.GetSecond(mBanHang.BANHANG.NgayBan.Value,mTransit.CaiDatBanHang.SoPhutToiThieu);
            chitiet.ChangeQtyChiTietBanHang(1);
        }
        //public bool KiemTraKho(Data.BOChiTietBanHang chitiet)
        //{
        //    return mBOQuanLyKho.KiemTraTonKhoTong(mTransit, chitiet) >= chitiet.ChiTietBanHang.SoLuongBan ? true : false;
        //}
        public bool CheckMutiablePrice(Data.BOChiTietBanHang chitiet)
        {
            return mPriceManager.CheckMutiablePrice(chitiet);
        }       
    }
}
