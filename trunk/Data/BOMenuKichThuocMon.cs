using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuKichThuocMon
    {
        public static List<MENUKICHTHUOCMON> GetAll(int MonID)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                return ke.MENUKICHTHUOCMONs.Where(s => s.MonID == MonID).ToList();
            }
        }

        public static List<MENUKICHTHUOCMON> GetAllName(int MonID)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                var res = (from k in ke.MENUKICHTHUOCMONs
                           join l in ke.LOAIBANs on k.LoaiBanID equals l.LoaiBanID
                           where k.MonID == MonID && k.Deleted == false
                           select new
                           {
                               MENUKICHTHUOCMON = k,
                               LOAIBAN = l
                           }).ToList().Select(s => s.MENUKICHTHUOCMON);
                return res.ToList();
            }
        }

        public static int Them(MENUKICHTHUOCMON item)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                ke.MENUKICHTHUOCMONs.AddObject(item);
                ke.SaveChanges();
                return item.KichThuocMonID;
            }
        }

        public static int CapNhat(MENUKICHTHUOCMON item)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                MENUKICHTHUOCMON m = (from x in ke.MENUKICHTHUOCMONs where x.KichThuocMonID == item.KichThuocMonID select x).First();
                m.MonID = item.MonID;
                m.TonKhoToiDa = item.TonKhoToiDa;
                m.TonKhoToiThieu = item.TonKhoToiThieu;
                m.Deleted = item.Deleted;
                m.Visual = item.Visual;
                m.KichThuocBan = item.KichThuocBan;
                m.KichThuocMon = item.KichThuocMon;
                ke.SaveChanges();
                return item.KichThuocMonID;
            }
        }

        public static int Xoa(int KichThuocMonID)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                MENUKICHTHUOCMON item = (from x in ke.MENUKICHTHUOCMONs where x.KichThuocMonID == KichThuocMonID select x).First();
                item.Deleted = true;
                ke.SaveChanges();
                return item.KichThuocMonID;
            }
        }

        
    }
}
