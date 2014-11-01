using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuGia
    {
        public static List<MENUGIA> GetAll(int KichThuocMonID, Transit mTransit)
        {
            var res = (from g in mTransit.KaraokeEntities.MENUGIAs
                       join l in mTransit.KaraokeEntities.MENULOAIGIAs on g.LoaiGiaID equals l.LoaiGiaID
                       where g.KichThuocMonID == KichThuocMonID
                       select new
                       {
                           MENUGIAs = g,
                           MENULOAIGIAs = l
                       }).ToList().Select(s => s.MENUGIAs);
            return res.ToList();
        }

        public static int Them(MENUGIA item, Transit mTransit)
        {
            mTransit.KaraokeEntities.MENUGIAs.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.GiaID;
        }

        public static MENUGIA GetByID(int LoaiGiaID, int KichThuocMonID, Transit mTransit)
        {
            List<MENUGIA> lsArray = (from x in mTransit.KaraokeEntities.MENUGIAs where x.LoaiGiaID == LoaiGiaID && x.KichThuocMonID == KichThuocMonID select x).ToList();
            if (lsArray.Count > 0)
                return lsArray[0];
            else
                return null;
        }

        public static int Xoa(int GiaID, Transit mTransit)
        {
            MENUGIA item = (from x in mTransit.KaraokeEntities.MENUGIAs where x.GiaID == GiaID select x).First();
            mTransit.KaraokeEntities.MENUGIAs.DeleteObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.GiaID;
        }

        public static int CapNhat(MENUGIA item, Transit mTransit)
        {
            MENUGIA m = (from x in mTransit.KaraokeEntities.MENUGIAs where x.GiaID == item.GiaID select x).First();
            m.LoaiGiaID = item.LoaiGiaID;
            m.Gia = item.Gia;
            m.KichThuocMonID = item.KichThuocMonID;
            mTransit.KaraokeEntities.SaveChanges();
            return item.GiaID;
        }
    }
}
