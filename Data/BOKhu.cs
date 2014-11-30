using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOKhu
    {
        private Transit mTransit;
        public Data.KHU Khu { get; set; }
        public string TenKhu
        {
            get { return Khu.TenKhu; }
        }
        public int KhuID
        {
            get { return Khu.KhuID; }
        }
        private FrameworkRepository<KHU> frmKhu;
        public BOKhu(Transit transit)
        {
            mTransit = transit;
            frmKhu = new FrameworkRepository<KHU>(mTransit.KaraokeEntities, mTransit.KaraokeEntities.KHUs);
        }
        public BOKhu()
        {
            Khu = new KHU();
        }

        public static IQueryable<KHU> GetAllVisual(Transit transit)
        {
            var query = FrameworkRepository<KHU>.QueryNoTracking(transit.KaraokeEntities.KHUs).Where(o => o.Deleted == false && o.Visual == true);
            return query;
        }
        public static IQueryable<KHU> GetAllNoTracking(Transit transit)
        {
            return FrameworkRepository<KHU>.QueryNoTracking(transit.KaraokeEntities.KHUs).Where(k => k.Deleted == false);
        }
        public static List<KHU> GetAllNoTrackingToList(Transit transit)
        {
            return FrameworkRepository<KHU>.QueryNoTracking(transit.KaraokeEntities.KHUs).Where(k => k.Deleted == false).ToList();
        }


        public IQueryable<BOKhu> GetAll(Transit transit)
        {
            return from k in frmKhu.Query()                   
                   where k.Deleted == false
                   select new BOKhu
                   {
                       Khu = k
                   };

        }

        private int Them(BOKhu item, Transit mTransit)
        {
            frmKhu.AddObject(item.Khu);
            return item.Khu.KhuID;
        }

        private int Xoa(BOKhu item, Transit mTransit)
        {
            item.Khu.Deleted = true;
            frmKhu.Update(item.Khu);
            return item.Khu.KhuID;
        }

        private int Sua(BOKhu item, Transit mTransit)
        {
            frmKhu.Update(item.Khu);
            return item.Khu.KhuID;
        }

        public void Luu(List<BOKhu> lsArray, List<BOKhu> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (BOKhu item in lsArray)
                {
                    if (item.Khu.KhuID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (BOKhu item in lsArrayDeleted)
                {
                    Xoa(item, mTransit);
                }
            frmKhu.Commit();
        }

    }
}
