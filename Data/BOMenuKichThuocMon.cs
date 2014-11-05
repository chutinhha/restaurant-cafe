using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuKichThuocMon
    {
        public static List<MENUKICHTHUOCMON> GetAll(int MonID, Transit mTransit)
        {
            return mTransit.KaraokeEntities.MENUKICHTHUOCMONs.Where(s => s.MonID == MonID).ToList();
        }

        public static List<MENUKICHTHUOCMON> GetAllName(int MonID, Transit mTransit)
        {
            var res = (from k in mTransit.KaraokeEntities.MENUKICHTHUOCMONs
                       join l in mTransit.KaraokeEntities.LOAIBANs on k.LoaiBanID equals l.LoaiBanID
                       where k.MonID == MonID && k.Deleted == false
                       select new
                       {
                           MENUKICHTHUOCMON = k,
                           LOAIBAN = l
                       }).ToList().Select(s => s.MENUKICHTHUOCMON);
            return res.ToList();

        }

        public static int Them(MENUKICHTHUOCMON item, Transit mTransit)
        {
            mTransit.KaraokeEntities.MENUKICHTHUOCMONs.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.KichThuocMonID;
        }

        public static int Sua(MENUKICHTHUOCMON item, Transit mTransit)
        {
            MENUKICHTHUOCMON m = (from x in mTransit.KaraokeEntities.MENUKICHTHUOCMONs where x.KichThuocMonID == item.KichThuocMonID select x).First();
            m.MonID = item.MonID;
            m.TonKhoToiDa = item.TonKhoToiDa;
            m.LoaiBanID = item.LoaiBanID;
            m.TenLoaiBan = item.TenLoaiBan;
            m.SoLuongBanBan = item.SoLuongBanBan;
            m.GiaBanMacDinh = item.GiaBanMacDinh;
            m.KichThuocLoaiBan = item.KichThuocLoaiBan;
            m.TonKhoToiThieu = item.TonKhoToiThieu;
            m.KichThuocLoaiBan = item.KichThuocLoaiBan;
            m.Deleted = item.Deleted;
            m.Visual = item.Visual;
            mTransit.KaraokeEntities.SaveChanges();
            return item.KichThuocMonID;
        }

        public static int Xoa(int KichThuocMonID, Transit mTransit)
        {
            MENUKICHTHUOCMON item = (from x in mTransit.KaraokeEntities.MENUKICHTHUOCMONs where x.KichThuocMonID == KichThuocMonID select x).First();
            item.Deleted = true;
            mTransit.KaraokeEntities.SaveChanges();
            return item.KichThuocMonID;
        }

        public static void Luu(List<MENUKICHTHUOCMON> lsArray, List<MENUKICHTHUOCMON> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (MENUKICHTHUOCMON item in lsArray)
                {
                    if (item.KichThuocMonID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (MENUKICHTHUOCMON item in lsArrayDeleted)
                {
                    Xoa(item.KichThuocMonID, mTransit);
                }
        }

    }
}
