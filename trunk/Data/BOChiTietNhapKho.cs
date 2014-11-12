using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOChiTietNhapKho
    {
        public static List<CHITIETNHAPKHO> GetAll(int NhapKhoID, Transit mTransit)
        {
            var res = (from g in mTransit.KaraokeEntities.CHITIETNHAPKHOes
                       join l in mTransit.KaraokeEntities.MENUMONs on g.MonID equals l.MonID
                       where g.NhapKhoID == NhapKhoID
                       select new
                       {
                           CHITIETNHAPKHOes = g,
                           MENUMONs = l
                       }).ToList().Select(s => s.CHITIETNHAPKHOes);
            return res.ToList();
        }

        public static int Them(CHITIETNHAPKHO item, Transit mTransit)
        {
            mTransit.KaraokeEntities.CHITIETNHAPKHOes.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.ChiTietNhapKhoID;
        }        

        public static int Xoa(int ChiTietNhapKhoID, Transit mTransit)
        {
            CHITIETNHAPKHO item = (from x in mTransit.KaraokeEntities.CHITIETNHAPKHOes where x.ChiTietNhapKhoID == ChiTietNhapKhoID select x).First();
            mTransit.KaraokeEntities.CHITIETNHAPKHOes.DeleteObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.ChiTietNhapKhoID;
        }

        public static int Sua(CHITIETNHAPKHO item, Transit mTransit)
        {
            CHITIETNHAPKHO m = (from x in mTransit.KaraokeEntities.CHITIETNHAPKHOes where x.ChiTietNhapKhoID == item.ChiTietNhapKhoID select x).First();
            m.LoaiBanID = item.LoaiBanID;
            m.KichThuocBan = item.KichThuocBan;
            m.MonID = item.MonID;
            mTransit.KaraokeEntities.SaveChanges();
            return item.ChiTietNhapKhoID;
        }

        public static void Luu(List<CHITIETNHAPKHO> lsArray, List<CHITIETNHAPKHO> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (CHITIETNHAPKHO item in lsArray)
                {
                    if (item.ChiTietNhapKhoID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (CHITIETNHAPKHO item in lsArrayDeleted)
                {
                    Xoa(item.ChiTietNhapKhoID, mTransit);
                }
        }
    }
}
