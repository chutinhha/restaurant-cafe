using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOBan
    {
        public static List<BAN> GetAll()
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                return ke.BANs.ToList();
            }
        }

        public static int Them(BAN item)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                ke.BANs.AddObject(item);
                ke.SaveChanges();
                return item.BanID;
            }
        }

        public static int Xoa(int BanID)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                BAN item = (from x in ke.BANs where x.BanID == BanID select x).First();
                ke.DeleteObject(item);
                ke.SaveChanges();
                return item.BanID;
            }
        }

        public static int CapNhat(BAN item)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                BAN m = (from x in ke.BANs where x.BanID == item.BanID select x).First();
                m.TenBan = item.TenBan;
                m.KhuID = item.KhuID;
                m.Hinh = item.Hinh;
                m.Height = item.Height;
                m.Width = item.Width;
                m.LocationX = item.LocationX;
                m.LocationY = item.LocationY;
                ke.SaveChanges();
                return item.BanID;
            }
        }
    }
}
