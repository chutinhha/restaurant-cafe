using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuNhom
    {
        public static List<MENUNHOM> GetAll(int LoaiNhomID)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                if (LoaiNhomID > -1)
                    return ke.MENUNHOMs.Where(s => s.LoaiNhomID == LoaiNhomID && s.Deleted == false).OrderBy(s => s.SapXep).ToList();
                else
                    return ke.MENUNHOMs.ToList();
            }

        }

        public static int Them(MENUNHOM item)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                ke.MENUNHOMs.AddObject(item);
                ke.SaveChanges();
                return item.NhomID;
            }
        }

        public static int Xoa(int NhomID)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                MENUNHOM item = (from x in ke.MENUNHOMs where x.NhomID == NhomID select x).First();
                item.Deleted = true;
                ke.SaveChanges();
                return item.NhomID;
            }
        }

        public static int CapNhat(MENUNHOM item)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                MENUNHOM m = (from x in ke.MENUNHOMs where x.NhomID == item.NhomID select x).First();
                m.SapXep = item.SapXep;
                m.TenDai = item.TenDai;
                m.TenNgan = item.TenNgan;
                ke.SaveChanges();
                return item.NhomID;
            }
        }
    }
}
