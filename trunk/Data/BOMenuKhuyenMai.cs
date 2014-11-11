using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuKhuyenMai
    {
        public static List<MENUKHUYENMAI> GetAll(Transit mTransit)
        {
            var res = (from g in mTransit.KaraokeEntities.MENUKHUYENMAIs
                       join l in mTransit.KaraokeEntities.MENUKICHTHUOCMONs on g.KichThuocMonTang equals l.KichThuocMonID
                       select new
                       {
                           MENUKHUYENMAIs = g,
                           MENUKICHTHUOCMONs = l
                       }).ToList().Select(s => s.MENUKHUYENMAIs);
            return res.ToList();
        }

        public static List<MENUKHUYENMAI> GetAll(int KichThuocMonID, Transit mTransit)
        {
            var res = (from g in mTransit.KaraokeEntities.MENUKHUYENMAIs
                       join l in mTransit.KaraokeEntities.MENUKICHTHUOCMONs on g.KichThuocMonTang equals l.KichThuocMonID
                       where g.KichThuocMonID == KichThuocMonID
                       select new
                       {
                           MENUKHUYENMAIs = g,
                           MENUKICHTHUOCMONs = l
                       }).ToList().Select(s => s.MENUKHUYENMAIs);
            return res.ToList();
        }

        public static int Them(MENUKHUYENMAI item, Transit mTransit)
        {
            mTransit.KaraokeEntities.MENUKHUYENMAIs.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.ID;
        }

        public static int Xoa(int ID, Transit mTransit)
        {
            MENUKHUYENMAI item = (from x in mTransit.KaraokeEntities.MENUKHUYENMAIs where x.ID == ID select x).First();
            mTransit.KaraokeEntities.MENUKHUYENMAIs.DeleteObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.ID;
        }

        public static int Sua(MENUKHUYENMAI item, Transit mTransit)
        {
            MENUKHUYENMAI m = (from x in mTransit.KaraokeEntities.MENUKHUYENMAIs where x.ID == item.ID select x).First();
            m.KichThuocMonID = item.KichThuocMonID;
            m.KichThuocMonTang = item.KichThuocMonTang;
            m.TenKhuyenMai = item.TenKhuyenMai;
            mTransit.KaraokeEntities.SaveChanges();
            return item.ID;
        }

        public static void Luu(List<MENUKHUYENMAI> lsArray, List<MENUKHUYENMAI> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (MENUKHUYENMAI item in lsArray)
                {
                    if (item.ID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (MENUKHUYENMAI item in lsArrayDeleted)
                {
                    Xoa(item.ID, mTransit);
                }
        }

        public static void Luu(List<MENUKHUYENMAI> lsArray, Transit mTransit)
        {
            foreach (MENUKHUYENMAI item in lsArray)
            {
                Sua(item, mTransit);
            }
        }
    }
}
