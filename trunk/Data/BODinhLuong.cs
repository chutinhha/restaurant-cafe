using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BODinhLuong
    {
        public static List<DINHLUONG> GetAll(int KichThuocMonID, Transit mTransit)
        {
            var res = (from g in mTransit.KaraokeEntities.DINHLUONGs
                       join l in mTransit.KaraokeEntities.MENUMONs on g.MonID equals l.MonID
                       where g.KichThuocMonChinhID == KichThuocMonID
                       select new
                       {
                           DINHLUONGs = g,
                           MENUMONs = l
                       }).ToList().Select(s => s.DINHLUONGs);
            return res.ToList();
        }

        public static int Them(DINHLUONG item, Transit mTransit)
        {
            mTransit.KaraokeEntities.DINHLUONGs.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.ID;
        }

        public static DINHLUONG GetByID(int MonID, int KichThuocMonID, Transit mTransit)
        {
            List<DINHLUONG> lsArray = (from x in mTransit.KaraokeEntities.DINHLUONGs where x.MonID == MonID && x.KichThuocMonChinhID == KichThuocMonID select x).ToList();
            if (lsArray.Count > 0)
                return lsArray[0];
            else
                return null;
        }

        public static int Xoa(int ID, Transit mTransit)
        {
            DINHLUONG item = (from x in mTransit.KaraokeEntities.DINHLUONGs where x.ID == ID select x).First();
            mTransit.KaraokeEntities.DINHLUONGs.DeleteObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.ID;
        }

        public static int Sua(DINHLUONG item, Transit mTransit)
        {
            DINHLUONG m = (from x in mTransit.KaraokeEntities.DINHLUONGs where x.ID == item.ID select x).First();
            m.LoaiBanID = item.LoaiBanID;
            m.KichThuocBan = item.KichThuocBan;
            m.MonID = item.MonID;
            mTransit.KaraokeEntities.SaveChanges();
            return item.ID;
        }

        public static void Luu(List<DINHLUONG> lsArray, List<DINHLUONG> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (DINHLUONG item in lsArray)
                {
                    if (item.ID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (DINHLUONG item in lsArrayDeleted)
                {
                    Xoa(item.ID, mTransit);
                }
        }
    }
}
