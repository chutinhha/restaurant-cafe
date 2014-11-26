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
        private Data.BOMenuLoaiGia mBOMenuLoaiGia;
        private Data.BOMenuGia mBOMenuGia;
        private Data.Transit mTransit;        
        private PrinterServer.ProcessPrinter mProcessPrinter;
        public Data.BOChiTietBanHang CurrentChiTietBanHang { get; set; }        
        public Data.BANHANG BanHang
        {
            get { return mBanHang.BANHANG; }
        }        
        public List<Data.BOChiTietBanHang> ListChiTietBanHang
        {
            get { return mBanHang._ListChiTietBanHang; }
        }
        public ProcessOrder(Data.Transit transit)
        {            
            mTransit = transit;
            mBOMenuGia = new Data.BOMenuGia(mTransit);                        
            mBanHang = new Data.BOBanHang(mTransit);
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
            int lichSuBanHangId= mBanHang.GuiNhaBep();
            if (lichSuBanHangId>0)
            {
                mProcessPrinter.InHoaDon(lichSuBanHangId);
            }
            return lichSuBanHangId;
        }
        public int TamTinh()
        {
            if (BanHang.BanHangID>0)
            {
                int banHangID = mBanHang.TamTinh();
                mProcessPrinter.InBill(true, mBanHang.BANHANG.BanHangID);
                return banHangID;    
            }
            else
            {
                int lichSuBanHangID = mBanHang.GuiNhaBep();
                if (lichSuBanHangID > 0)
                {
                    mProcessPrinter.InHoaDon(lichSuBanHangID);
                }
                int banHangID = mBanHang.TamTinh();
                if (banHangID > 0)
                {
                    mProcessPrinter.InBill(true, banHangID);
                }
                return banHangID;
            }
        }
        public void TinhTien()
        {
            if (BanHang.BanHangID > 0)
            {
                int banHangID = mBanHang.TinhTien();
                if (banHangID > 0)
                {
                    mProcessPrinter.InBill(false, banHangID);
                }
            }
            else
            {
                int lichSuBanHangID = mBanHang.GuiNhaBep();                
                if (lichSuBanHangID > 0)
                {
                    mProcessPrinter.InHoaDon(lichSuBanHangID);
                }
                int banHangID = mBanHang.TinhTien();                
                if (banHangID > 0)
                {
                    mProcessPrinter.InBill(false, banHangID);
                }
            }
        }
        public bool KiemTraHoaDonDaHoanThanh()
        {
            if (mBanHang.BANHANG.TrangThaiID==4||mBanHang.BANHANG.TrangThaiID==0)
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
                if (item.IsDeleted==false)
                {
                    count++;
                }
            }
            return count;
        }    
        public void XoaChiTietBanHang(Data.BOChiTietBanHang chitiet)
        {
            chitiet.IsDeleted = true;
        }
        public int AddChiTietBanHang(Data.BOChiTietBanHang chitiet)
        {
            return mBanHang.AddChiTietBanHang(chitiet);
        }             
    }
}
