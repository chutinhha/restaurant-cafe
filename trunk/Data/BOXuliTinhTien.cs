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
            b.GiamGia = banhang.GiamGia;            
            return b;
        }
        public int GiamGiaPhanTram 
        {
            get 
            {
                return mBanHang.GiamGia;
            }
            set
            {
                mBanHang.GiamGia = value;
                TinhTienTraLai();
            }
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
                return mBanHang.GiamGia*mBanHang.TongTien/100; 
            }           
        }
        public decimal TongTienPhaiTra 
        {
            get 
            {                
                return (decimal)(mBanHang.TongTien - TienGiam); 
            }
        }
        public decimal TienKhachDua 
        {
            get { return (decimal)mBanHang.TienKhacHang; }
            set 
            {
                mBanHang.TienKhacHang = value;                
                TinhTienTraLai();   
            }
        }
        public decimal TienThe 
        {
            get { return (decimal)mBanHang.TienThe; }
            set
            {
                mBanHang.TienThe = value;                            
                TinhTienTraLai();   
            }
        }
        public decimal TienTraLai 
        {
            get { return (decimal)mBanHang.TienTraLai; }
        }
        private void TinhTienTraLai()
        {
            if (mBanHang.TienThe<=TongTienPhaiTra && (mBanHang.TienThe+mBanHang.TienKhacHang)>TongTienPhaiTra)
            {
                mBanHang.TienTraLai = mBanHang.TienKhacHang+mBanHang.TienThe - TongTienPhaiTra;                    
            }            
            else
            {
                mBanHang.TienTraLai = 0;
            }
        }
    }
}
