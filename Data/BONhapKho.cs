using System;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public class BONhapKho
    {
        private KaraokeEntities mKaraokeEntities;
        public List<BOChiTietNhapKho> ListChiTietNhapKho { get; set; }        

        public Data.KHO Kho { get; set; }

        public Data.NHACUNGCAP NhaCungCap { get; set; }

        public Data.NHANVIEN NhanVien { get; set; }

        public Data.NHAPKHO NhapKho { get; set; }

        public BONhapKho(KaraokeEntities kara)
        {
            mKaraokeEntities = kara;
            InitData();
        }

        public BONhapKho()
        {
            
            InitData();
        }
        private void InitData()
        {
            NhapKho = new NHAPKHO();
            Kho = new KHO();
            ListChiTietNhapKho = new List<BOChiTietNhapKho>();
        }
        public static IQueryable<BONhapKho> GetAllByDate(KaraokeEntities kara, DateTime dt)
        {            
            return 
                    from nk in kara.NHAPKHOes
                    join k in kara.KHOes on nk.KhoID equals k.KhoID
                    join nv in kara.NHANVIENs on nk.NhanVienID equals nv.NhanVienID
                    join ncc in kara.NHACUNGCAPs on nk.NhaCungCapID equals ncc.NhaCungCapID
                    where 
                        nk.ThoiGian.Value.Day==dt.Day &&
                        nk.ThoiGian.Value.Month==dt.Month &&
                        nk.ThoiGian.Value.Year==dt.Year
                    select new BONhapKho
                    {
                        NhapKho = nk,
                        Kho = k,
                        NhanVien = nv,
                        NhaCungCap = ncc
                    };                        
        }

        public IQueryable<BONhapKho> GetAll(Transit mTransit, DateTime dt)
        {
            return (from nk in mKaraokeEntities.NHAPKHOes
                    join k in mKaraokeEntities.KHOes on nk.KhoID equals k.KhoID
                    join nv in mKaraokeEntities.NHANVIENs on nk.NhanVienID equals nv.NhanVienID
                    join ncc in mKaraokeEntities.NHACUNGCAPs on nk.NhaCungCapID equals ncc.NhaCungCapID
                    where nk.ThoiGian.Value.Year == dt.Year && nk.ThoiGian.Value.Month == dt.Month && nk.ThoiGian.Value.Day == dt.Day
                    select new BONhapKho
                    {
                        NhapKho = nk,
                        Kho = k,
                        NhanVien = nv,
                        NhaCungCap = ncc
                    }
                        );
        }
        public void LuuNhapKho()
        {
            foreach (var item in ListChiTietNhapKho)
            {
                NhapKho.CHITIETNHAPKHOes.Add(item.ChiTietNhapKho);
                NhapKho.TongTien += item.ChiTietNhapKho.GiaNhap;
                BOLichSuTonKho.NhapKho(mKaraokeEntities, item,this);
                BOTonKho.NhapKhoTong(mKaraokeEntities, item, this);
            }
            mKaraokeEntities.NHAPKHOes.AddObject(NhapKho);            
            mKaraokeEntities.SaveChanges();
        }
    }
}