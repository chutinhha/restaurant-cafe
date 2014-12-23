using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOBaoCaoLichSuBanHang
    {
        public string TenBan { get; set; }
        public DateTime? NgayBan { get; set; }
        public decimal SoTien { get; set; }
        public string TrangThai { get; set; }
        KaraokeEntities mKaraokeEntities = null;
        Data.Transit mTransit = null;
        public BOBaoCaoLichSuBanHang() { }
        public BOBaoCaoLichSuBanHang(Data.Transit transit)
        {
            mTransit = transit;
            mKaraokeEntities = new KaraokeEntities();
        }

        public IQueryable<BOBaoCaoLichSuBanHang> GetLichSuBanHang(DateTime dtFrom, DateTime dtTo)
        {
            return from a in mKaraokeEntities.BANHANGs
                   //join b in mKaraokeEntities.BANs on a.BanID equals b.BanID
                   //join c in mKaraokeEntities.TRANGTHAIs on a.TrangThaiID equals c.TrangThaiID
                   where dtFrom.CompareTo(a.NgayBan.Value) <= 0 && dtTo.CompareTo(a.NgayBan.Value) >= 0
                   select new BOBaoCaoLichSuBanHang
                   {
                       TenBan=a.BAN.TenBan,
                       NgayBan=a.NgayBan,
                       SoTien=a.TongTien,
                       TrangThai=a.TRANGTHAI.TenTrangThai
                   };
        }

        public IQueryable<CAIDATTHONGTINCONGTY> GetCaiDatThongTinCongTy()
        {
            return mKaraokeEntities.CAIDATTHONGTINCONGTies;
        }

        public IQueryable<BAOCAOLICHSUBANHANG> GetBaoCaoLichSuBanHang(DateTime dtFrom, DateTime dtTo)
        {
            return from x in mKaraokeEntities.BAOCAOLICHSUBANHANGs
                   where dtFrom.CompareTo(x.NgayBan.Value) <= 0 && dtTo.CompareTo(x.NgayBan.Value) >= 0
                   orderby x.NgayBan
                   select x;
        }

    }
}
