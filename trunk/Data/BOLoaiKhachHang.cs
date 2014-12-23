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

        public IQueryable<LOAIKHACHHANG> GetAll()
        {
            return mKaraokeEntities.LOAIKHACHHANGs.Where(s => s.Deleted == false);
        }
        public static IQueryable<LOAIKHACHHANG> GetAllVisual(KaraokeEntities kara)
        {
            return kara.LOAIKHACHHANGs.Where(s => s.Deleted == false && s.Visual==true);
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
        public void Refresh()
        {
            mKaraokeEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, mKaraokeEntities.LOAIKHACHHANGs);
        }

    }
}
