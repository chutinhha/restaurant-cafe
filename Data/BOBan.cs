using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOBan
    {        
        public static List<BAN> GetTablePerArea(Transit transit, KHU khu)
        {            
            var list=transit.KaraokeEntities.BANs.Where(o => o.KhuID == khu.KhuID).ToList();            
            return list;
        }
        public static List<BAN> GetAll(Transit mTransit)
        {
            return mTransit.KaraokeEntities.BANs.ToList();
        }

        public static int Them(BAN item, Transit mTransit)
        {
            mTransit.KaraokeEntities.BANs.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.BanID;
        }

        public static int Xoa(int BanID, Transit mTransit)
        {
            BAN item = (from x in mTransit.KaraokeEntities.BANs where x.BanID == BanID select x).First();
            mTransit.KaraokeEntities.DeleteObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.BanID;
        }

        public static int CapNhat(BAN item, Transit mTransit)
        {            
            BAN m = (from x in mTransit.KaraokeEntities.BANs where x.BanID == item.BanID select x).First();
            m.TenBan = item.TenBan;
            m.KhuID = item.KhuID;
            m.Hinh = item.Hinh;
            m.Height = item.Height;
            m.Width = item.Width;
            m.LocationX = item.LocationX;
            m.LocationY = item.LocationY;
            mTransit.KaraokeEntities.SaveChanges();            
            return item.BanID;
        }
    }
}
