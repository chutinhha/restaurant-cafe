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
            mBanHang = new BANHANG();            
            Copy(banhang.BANHANG, mBanHang);
            mBanHang.TongTien = banhang.TongTien();

        }
        public void Copy(BANHANG bhFrom,BANHANG bhTo)
        {            
            bhTo.BanHangID = bhFrom.BanHangID;
            bhTo.NhanVienID = bhFrom.NhanVienID;
            bhTo.BanID = bhFrom.BanID;
            bhTo.TrangThaiID = bhFrom.TrangThaiID;
            bhTo.NgayBan = bhFrom.NgayBan;
            bhTo.MaHoaDon = bhFrom.MaHoaDon;
            bhTo.TheID = bhFrom.TheID;
            bhTo.KhachHangID = bhFrom.KhachHangID;
            bhTo.GiamGia = bhFrom.GiamGia;
            bhTo.ThueVAT = bhFrom.ThueVAT;
            bhTo.PhiDichVu = bhFrom.PhiDichVu;
            bhTo.TongTien = bhFrom.TongTien;
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
        public decimal TienPhiDichVu
        {
            get
            {
                return mBanHang.PhiDichVu * mBanHang.TongTien / 100;
            }
        }
        public decimal TienGiam 
        {
            get 
            {                
                return mBanHang.GiamGia*mBanHang.TongTien/100; 
            }           
        }        
        public decimal TienThueVAT
        {
            get 
            {
                return  mBanHang.ThueVAT * (mBanHang.TongTien-TienGiam+TienPhiDichVu) / 100;
            }
        }
        public decimal TongTienChuaGiamGia
        {
            get
            {
                return mBanHang.TongTien + TienPhiDichVu + TienThueVAT; 
            }
        }
        public decimal TongTienPhaiTra 
        {
            get 
            {
                return mBanHang.TongTien - TienGiam+TienPhiDichVu+TienThueVAT; 
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
