using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuGia
    {
        public static List<MENUGIA> GetAll(int KichThuocMonID)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                var res = (from g in ke.MENUGIAs
                           join l in ke.MENULOAIGIAs on g.LoaiGiaID equals l.LoaiGiaID
                           where g.KichThuocMonID == KichThuocMonID
                           select new
                           {
                               MENUGIAs = g,
                               MENULOAIGIAs = l
                           }).ToList().Select(s => s.MENUGIAs);
                return res.ToList();
            }
        }

        public static int Them(MENUGIA item)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                ke.MENUGIAs.AddObject(item);
                ke.SaveChanges();
                return item.GiaID;
            }
        }

        public static MENUGIA GetByID(int LoaiGiaID, int KichThuocMonID)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                List<MENUGIA> lsArray = (from x in ke.MENUGIAs where x.LoaiGiaID == LoaiGiaID && x.KichThuocMonID == KichThuocMonID select x).ToList();
                if (lsArray.Count > 0)
                    return lsArray[0];
                else
                    return null;
            }
        }

        public static int Xoa(int GiaID)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                MENUGIA item = (from x in ke.MENUGIAs where x.GiaID == GiaID select x).First();
                ke.MENUGIAs.DeleteObject(item);
                ke.SaveChanges();
                return item.GiaID;
            }
        }

        public static int CapNhat(MENUGIA item)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                MENUGIA m = (from x in ke.MENUGIAs where x.GiaID == item.GiaID select x).First();
                m.LoaiGiaID = item.LoaiGiaID;
                m.Gia = item.Gia;
                m.KichThuocMonID = item.KichThuocMonID;
                ke.SaveChanges();
                return item.GiaID;
            }
        }
    }
}
