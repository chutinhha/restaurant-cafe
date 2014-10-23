using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuMon
    {
        public static List<MENUMON> GetAll(int GroupID)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                if (GroupID > -1)
                    return ke.MENUMONs.Where(s => s.NhomID == GroupID && s.Deleted == false).OrderBy(s => s.SapXep).ToList();
                else
                    return ke.MENUMONs.ToList();
            }
        }

        public static int Them(MENUMON item)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                ke.MENUMONs.AddObject(item);
                ke.SaveChanges();
                return item.MonID;
            }
        }

        public static int Xoa(int MonID)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                MENUMON item = (from x in ke.MENUMONs where x.MonID == MonID select x).First();
                item.Deleted = true;                
                ke.SaveChanges();
                return item.MonID;
            }
        }

        public static int CapNhat(MENUMON item)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                MENUMON m = (from x in ke.MENUMONs where x.MonID == item.MonID select x).First();
                m.SapXep = item.SapXep;
                m.TenDai = item.TenDai;
                m.TenNgan = item.TenNgan;
                ke.SaveChanges();
                return item.MonID;
            }
        }
    }
}
