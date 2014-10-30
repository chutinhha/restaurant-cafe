using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMayIn
    {
        public static List<MAYIN> GetAll()
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                return ke.MAYINs.ToList();
            }
        }

        public static List<MAYIN> GetAll(int[] IDs)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                if (IDs != null)
                    return ke.MAYINs.Where(s => !IDs.Contains(s.MayInID)).ToList();
                else
                    return ke.MAYINs.ToList();
            }
        }

        public static int Them(MAYIN item)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                ke.MAYINs.AddObject(item);
                ke.SaveChanges();
                return item.MayInID;
            }
        }

        public static int Xoa(int MayInID)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                MAYIN item = (from x in ke.MAYINs where x.MayInID == MayInID select x).First();
                ke.MAYINs.DeleteObject(item);
                ke.SaveChanges();
                return item.MayInID;
            }
        }

        public static int Sua(MAYIN item)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                MAYIN m = (from x in ke.MAYINs where x.MayInID == item.MayInID select x).First();
                m.TenMayIn = item.TenMayIn;
                m.TieuDeIn = item.TieuDeIn;
                m.Visual = item.Visual;
                m.HopDungTien = item.HopDungTien;
                m.SoLanIn = item.SoLanIn;
                m.Visual = item.Visual;
                ke.SaveChanges();
                return item.MayInID;
            }
        }
    }
}
