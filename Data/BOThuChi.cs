using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOThuChi
    {
        public THUCHI ThuChi { get; set; }
        public NHANVIEN NhanVien { get; set; }
        public LOAITHUCHI LoaiThuChi { get; set; }
        KaraokeEntities mKaraokeEntities = null;
        public BOThuChi(Data.Transit transit)
        {
            mKaraokeEntities = new KaraokeEntities();
        }


        public BOThuChi()
        {
            ThuChi = new THUCHI();
            NhanVien = new NHANVIEN();
            LoaiThuChi = new LOAITHUCHI();
        }

        public IQueryable<BOThuChi> GetAll()
        {
            return from t in mKaraokeEntities.THUCHIs
                   join l in mKaraokeEntities.LOAITHUCHIs on t.LoaiThuChiID equals l.LoaiThuChiID
                   join n in mKaraokeEntities.NHANVIENs on t.NhanVienID equals n.NhanVienID
                   where t.NgayGhiSo.Value.Day == DateTime.Now.Day && t.NgayGhiSo.Value.Month == DateTime.Now.Month && t.NgayGhiSo.Value.Year == DateTime.Now.Year
                   select new BOThuChi
                   {
                       ThuChi = t,
                       LoaiThuChi = l,
                       NhanVien = n
                   };
        }
        public static IQueryable<THUCHI> GetAllNoTracking(Transit mTransit)
        {
            return FrameworkRepository<THUCHI>.QueryNoTracking(mTransit.KaraokeEntities.THUCHIs).Where(s => s.Deleted == false);
        }

        public void Luu(List<BOThuChi> lsArray)
        {
            foreach (BOThuChi item in lsArray)
            {
                if (item.ThuChi.ID == 0)
                {
                    mKaraokeEntities.THUCHIs.AddObject(item.ThuChi);
                }

            }
            mKaraokeEntities.SaveChanges();
        }
        public void Refresh()
        {
            mKaraokeEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, mKaraokeEntities.THUCHIs);
        }

        public IQueryable<LOAITHUCHI> GetLoaiThuChi()
        {
            return BOLoaiThuChi.GetAllNoTracking(mKaraokeEntities);
        }

        public LOAITHUCHI GetLoaiThuChi(int ID)
        {
            return GetLoaiThuChi().Where(s => s.LoaiThuChiID == ID).FirstOrDefault();
        }

    }
}
