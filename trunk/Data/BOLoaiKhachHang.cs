using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLoaiKhachHang
    {

        KaraokeEntities mKaraokeEntities = new KaraokeEntities();
        public BOLoaiKhachHang(Data.Transit transit)
        {
            mKaraokeEntities = new KaraokeEntities();
        }
        public static IQueryable<LOAIKHACHHANG> GetAll(KaraokeEntities kara)
        {
            return FrameworkRepository<LOAIKHACHHANG>.QueryNoTracking(kara.LOAIKHACHHANGs);
        }
        public IQueryable<LOAIKHACHHANG> GetAll()
        {
            return mKaraokeEntities.LOAIKHACHHANGs.Where(s => s.Deleted == false);
        }
        public static IQueryable<LOAIKHACHHANG> GetAllNoTracking(Transit mTransit)
        {
            return FrameworkRepository<LOAIKHACHHANG>.QueryNoTracking(mTransit.KaraokeEntities.LOAIKHACHHANGs).Where(s => s.Deleted == false);
        }

        public static IQueryable<LOAIKHACHHANG> GetQueryNoTracking(KaraokeEntities karaokeEntities)
        {
            return karaokeEntities.LOAIKHACHHANGs.Where(s => s.Deleted == false);
        }

        public void Luu(List<LOAIKHACHHANG> lsArray)
        {
            foreach (LOAIKHACHHANG item in lsArray)
            {
                if (item.LoaiKhachHangID == 0)
                {
                    mKaraokeEntities.LOAIKHACHHANGs.AddObject(item);
                }

            }
            mKaraokeEntities.SaveChanges();
        }
    }
}
