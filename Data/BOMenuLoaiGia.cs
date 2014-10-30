using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuLoaiGia
    {
        public static List<MENULOAIGIA> GetAll()
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                return ke.MENULOAIGIAs.Where(s => s.Deleted == false).OrderBy(s => s.Ten).ToList();
            }
        }

        public static int Them(MENULOAIGIA item)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                ke.MENULOAIGIAs.AddObject(item);
                ke.SaveChanges();
                return item.LoaiGiaID;
            }
        }

        public static int Xoa(int LoaiGiaID)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                MENULOAIGIA item = (from x in ke.MENULOAIGIAs where x.LoaiGiaID == LoaiGiaID select x).First();
                item.Deleted = true;
                ke.SaveChanges();
                return item.LoaiGiaID;
            }
        }

        public static int CapNhat(MENULOAIGIA item)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                MENULOAIGIA m = (from x in ke.MENULOAIGIAs where x.LoaiGiaID == item.LoaiGiaID select x).First();
                m.DienGiai = item.DienGiai;
                m.Ten = item.Ten;
                m.Visual = item.Visual;
                m.Deleted = item.Deleted;
                ke.SaveChanges();
                return item.LoaiGiaID;
            }
        }
    }
}
