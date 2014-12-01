using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOBan
    {
        private FrameworkRepository<BAN> frBan;
        private Transit mTransit;
        public CAIDATBAN _CAIDATBAN { get; set; }
        public BOBan(Transit transit)
        {
            mTransit = transit;
            frBan = new FrameworkRepository<BAN>(mTransit.KaraokeEntities, mTransit.KaraokeEntities.BANs);
            _CAIDATBAN = BOCaiDatBan.GetQueryNoTracking(mTransit);
        }
        public static IQueryable<BAN> GetVisual(Transit transit)
        {
            return FrameworkRepository<BAN>.QueryNoTracking(transit.KaraokeEntities.BANs).Where(b => b.Visual == true && b.Deleted == false);
        }
        public IQueryable<BAN> GetAllTablePerArea(KHU khu)
        {
            frBan.Refresh();
            return frBan.Query().Where(o => o.KhuID == khu.KhuID && o.Deleted == false);
        }

        public IQueryable<BAN> GetVisualTablePerArea(KHU khu)
        {
            frBan.Refresh();
            return GetVisualTablePerArea(khu, mTransit);
        }
        public static IQueryable<BAN> GetVisualTablePerArea(KHU khu, Transit transit)
        {
            return from a in GetVisual(transit)
                   where a.KhuID == khu.KhuID
                   select a;
        }
        public static IQueryable<BAN> GetAllTableInOrderPerArea(KHU khu, Transit transit)
        {
            return from a in GetVisual(transit)
                   join b in BOBanHang.GetAllNotCompleted(transit) on a.BanID equals b.BanID
                   where a.KhuID == khu.KhuID
                   select a;
        }
        public static IQueryable<BAN> GetAllTableNotInOrderPerArea(KHU khu, Transit transit)
        {
            var listIn = GetAllTableInOrderPerArea(khu, transit);
            return from a in GetVisual(transit)
                   where !listIn.Contains(a) && a.KhuID == khu.KhuID
                   select a;
        }
        public void Them(BAN ban)
        {
            if (ban.BanID==0)
            {
                frBan.AddObject(ban);
            }
        }
        public void Xoa(BAN ban)
        {
            if (ban.BanID>0)
            {
                ban.Deleted = true;
                frBan.Update(ban);
            }
            else
            {
                frBan.DeleteObject(ban);
            }
        }
        public void Sua(BAN ban)
        {
            if (ban.BanID>0)
            {
                frBan.Update(ban);
            }
        }
        public void Commit()
        {
            frBan.Commit();
        }
    }
}
