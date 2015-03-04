using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOChuyenKho
    {
        public CHUYENKHO ChuyenKho { get; set; }
        public TONKHO TonKho { get; set; }
        public int SoLuong { get; set; }
        public int? KhoDenID { get; set; }        
        public int? NhanVienID { get; set; }

        public KHO KhoDi { get; set; }
        public KHO KhoDen { get; set; }
        public NHANVIEN NhanVien { get; set; }

        private KaraokeEntities mKaraokeEntities;

        public BOChuyenKho(Data.Transit transit,KaraokeEntities kara)
        {            
            mKaraokeEntities = kara;
            ChuyenKho = new CHUYENKHO();
        }
        public BOChuyenKho()
        {            
            ChuyenKho = new CHUYENKHO();            
        }

        public void Chuyen()
        {
            CHUYENKHO chuyenKho = new CHUYENKHO();
            chuyenKho.KhoDiID = TonKho.KhoID;
            chuyenKho.KhoDenID = KhoDenID;
            chuyenKho.NhanVienID = NhanVienID;
            chuyenKho.NgayChuyen = DateTime.Now;
            chuyenKho.Visual = true;
            chuyenKho.Deleted = false;
            
            CHITIETCHUYENKHO chieiet = new CHITIETCHUYENKHO();
            chieiet.MonID = TonKho.MonID;
            chieiet.NgaySanXuat = TonKho.NgaySanXuat;
            chieiet.NgayHetHan = TonKho.NgayHetHan;
            chieiet.SoLuongChuyen = SoLuong;
            chieiet.GiaNhap = TonKho.GiaNhap;
            chieiet.GiaBan = TonKho.GiaBan;
            chuyenKho.CHITIETCHUYENKHOes.Add(chieiet);
            BOTonKho.ChuyenKhoTong(mKaraokeEntities, TonKho, KhoDenID, SoLuong);
            mKaraokeEntities.CHUYENKHOes.AddObject(chuyenKho);
            BOLichSuTonKho.ChuyenKho(mKaraokeEntities, this);
            mKaraokeEntities.SaveChanges();
        }

        public IQueryable<BOChuyenKho> GetAllByDate(DateTime dateTime)
        {
            var query = from a in mKaraokeEntities.CHUYENKHOes
                        join b in mKaraokeEntities.KHOes on a.KhoDenID equals b.KhoID
                        join c in mKaraokeEntities.KHOes on a.KhoDiID equals c.KhoID
                        join d in mKaraokeEntities.NHANVIENs on a.NhanVienID equals d.NhanVienID
                        where 
                            a.Visual == true && 
                            a.Deleted == false &&
                            a.NgayChuyen.Value.Day==dateTime.Day&&
                            a.NgayChuyen.Value.Month==dateTime.Month&&
                            a.NgayChuyen.Value.Year==dateTime.Year
                        select new BOChuyenKho
                        {
                            ChuyenKho = a,
                            KhoDen = b,
                            KhoDi = c,
                            NhanVien = d
                        };
            return query;
        }

        public string ThoiGian
        {
            get { return Utilities.DateTimeConverter.ConvertToTimeString(ChuyenKho.NgayChuyen.Value); }
        }
    }
}
