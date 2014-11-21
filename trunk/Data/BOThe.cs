using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOThe
    {
        public int TheID { get; set; }
        public string TenThe { get; set; }
        public BOThe()
        {
        }
        FrameworkRepository<THE> frmLoaiKhachHang = null;
        public BOThe(Data.Transit transit)
        {            
            frmLoaiKhachHang = new FrameworkRepository<THE>(transit.KaraokeEntities, transit.KaraokeEntities.THEs);
        }        
        public IQueryable<THE> GetAll(Transit mTransit)
        {
            return frmLoaiKhachHang.Query().Where(s => s.Deleted == false);
        }
        public static IQueryable<THE> GetAllNoTracking(Transit mTransit)
        {
            return FrameworkRepository<THE>.QueryNoTracking(mTransit.KaraokeEntities.THEs).Where(s => s.Deleted == false);
        }
        public static IQueryable<BOThe> GetAllVisual(Transit mTransit)
        {
            return from x in FrameworkRepository<THE>.QueryNoTracking(mTransit.KaraokeEntities.THEs)
                   where x.Visual == true && x.Deleted == false
                   select new BOThe
                   {
                       TheID=x.TheID,
                       TenThe=x.TenThe
                   };
        }
        private int Them(THE item, Transit mTransit)
        {
            frmLoaiKhachHang.AddObject(item);
            return item.TheID;
        }

        private int Xoa(THE item, Transit mTransit)
        {
            item.Deleted = true;
            frmLoaiKhachHang.Update(item);
            return item.TheID;
        }

        private int Sua(THE item, Transit mTransit)
        {
            item.Deleted = true;
            frmLoaiKhachHang.Update(item);
            return item.TheID;
        }

        public void Luu(List<THE> lsArray, List<THE> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (THE item in lsArray)
                {
                    if (item.TheID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (THE item in lsArrayDeleted)
                {
                    Xoa(item, mTransit);
                }
            frmLoaiKhachHang.Commit();
        }
    }
}
