using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMayIn
    {
        public int MayInID { get; set; }
        public string TenMayIn { get; set; }
        public string TieuDeIn { get; set; }
        public bool HocDungTien { get; set; }
        public int SoLanIn { get; set; }
        FrameworkRepository<MAYIN> frmMayIn;
        private Transit mTransit;
        public BOMayIn()
        { }
        public BOMayIn(Transit transit)
        {
            mTransit = transit;
            frmMayIn = new FrameworkRepository<MAYIN>(mTransit.KaraokeEntities, mTransit.KaraokeEntities.MAYINs);
        }

        public IQueryable<MAYIN> GetAll(Transit mTransit)
        {
            return frmMayIn.Query().Where(s => s.Deleted == false);
        }
        public static IQueryable<MAYIN> GetAllNoTracking(Transit mTransit, bool IsMayInHoaDon)
        {
            return FrameworkRepository<MAYIN>.QueryNoTracking(mTransit.KaraokeEntities.MAYINs).Where(s => s.Deleted == false && s.MayInHoaDon == IsMayInHoaDon);
        }

        private int Them(MAYIN item, Transit mTransit)
        {
            frmMayIn.AddObject(item);
            return item.MayInID;
        }

        private int Xoa(MAYIN item, Transit mTransit)
        {
            item.Deleted = true;
            frmMayIn.Update(item);
            return item.MayInID;
        }

        private int Sua(MAYIN item, Transit mTransit)
        {
            frmMayIn.Update(item);
            return item.MayInID;
        }

        public void Luu(List<MAYIN> lsArray, List<MAYIN> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (MAYIN item in lsArray)
                {
                    if (item.MayInID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (MAYIN item in lsArrayDeleted)
                {
                    Xoa(item, mTransit);
                }
            frmMayIn.Commit();
        }
    }
}
