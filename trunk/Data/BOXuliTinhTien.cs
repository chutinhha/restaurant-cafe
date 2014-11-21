using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOXuliTinhTien
    {
        private BANHANG mBanHang;
        public BANHANG BanHang 
        {
            get { return mBanHang; }
        }
        private Transit mTransit;
        public BOXuliTinhTien(Transit transit,BOBanHang banhang)
        {
            mTransit = transit;
            mBanHang = CreateBHFromBH(banhang.BANHANG);
            mBanHang.TongTien = banhang.TongTien();            
        }
        private BANHANG CreateBHFromBH(BANHANG banhang)
        {
            BANHANG b = new BANHANG();
            b.BanHangID = banhang.BanHangID;
            b.NhanVienID = banhang.NhanVienID;
            b.BanID = banhang.BanID;
            b.TrangThaiID = banhang.TrangThaiID;
            b.NgayBan = banhang.NgayBan;
            b.MaHoaDon = banhang.MaHoaDon;            
            b.TheID = banhang.TheID;
            b.KhachHangID = banhang.KhachHangID;
            return b;
        }
        public decimal TongTien 
        {
            get 
            {                
                return (decimal)mBanHang.TongTien; 
            }
        }
        public decimal TienGiam 
        {
            get 
            {                
                return (decimal)mBanHang.TienGiam; 
            }
            set 
            { 
                mBanHang.TienGiam = value;
                TinhTienTraLai();
            }
        }
        public decimal TongTienPhaiTra 
        {
            get 
            {                
                return (decimal)(mBanHang.TongTien - mBanHang.TienGiam); 
            }
        }
        public decimal TienKhachDua 
        {
            get { return (decimal)mBanHang.TienKhacHang; }
            set 
            {
                mBanHang.TienKhacHang = value;
                mBanHang.TienThe = 0;
                TinhTienTraLai();   
            }
        }
        public decimal TienThe 
        {
            get { return (decimal)mBanHang.TienThe; }
            set
            {
                mBanHang.TienThe = value;
                mBanHang.TienKhacHang = 0;                
                TinhTienTraLai();   
            }
        }
        public decimal TienTraLai 
        {
            get { return (decimal)mBanHang.TienTraLai; }
        }
        private void TinhTienTraLai()
        {
            if (mBanHang.TienKhacHang > TongTienPhaiTra)
            {
                mBanHang.TienTraLai = mBanHang.TienKhacHang - TongTienPhaiTra;
            }
            else
            {
                mBanHang.TienTraLai = 0;
            }
        }
    }
}
