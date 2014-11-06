using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BONhaCungCap
    {
        public static List<NHACUNGCAP> GetAll(Transit mTransit)
        {
            return mTransit.KaraokeEntities.NHACUNGCAPs.Where(s => s.Deleted == false).ToList();
        }

        public static int Them(NHACUNGCAP item, Transit mTransit)
        {
            mTransit.KaraokeEntities.NHACUNGCAPs.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.NhaCungCapID;
        }

        public static int Xoa(int NhaCungCapID, Transit mTransit)
        {
            NHACUNGCAP item = (from x in mTransit.KaraokeEntities.NHACUNGCAPs where x.NhaCungCapID == NhaCungCapID select x).First();
            mTransit.KaraokeEntities.NHACUNGCAPs.DeleteObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.NhaCungCapID;
        }

        public static int Sua(NHACUNGCAP item, Transit mTransit)
        {
            NHACUNGCAP m = (from x in mTransit.KaraokeEntities.NHACUNGCAPs where x.NhaCungCapID == item.NhaCungCapID select x).First();
            m.TenNhaCungCap = item.TenNhaCungCap;
            m.SoNha = item.SoNha;
            m.TenDuong = item.TenDuong;
            m.Mobile = item.Mobile;
            m.Phone = item.Phone;
            m.Fax = item.Fax;
            m.MaSoThue = item.MaSoThue;
            m.Email = item.Email;
            m.Visual = item.Visual;
            m.Edit = false;
            mTransit.KaraokeEntities.SaveChanges();
            return item.NhaCungCapID;
        }

        public static void Luu(List<NHACUNGCAP> lsArray, List<NHACUNGCAP> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (NHACUNGCAP item in lsArray)
                {
                    if (item.NhaCungCapID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (NHACUNGCAP item in lsArrayDeleted)
                {
                    Xoa(item.NhaCungCapID, mTransit);
                }
        }
    }
}
