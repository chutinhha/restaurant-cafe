using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuKichThuocMon
    {
        public Data.MENUKICHTHUOCMON MenuKichThuocMon { get; set; }
        public Data.MENUMON MenuMon { get; set; }
        public Data.LOAIBAN LoaiBan { get; set; }
        public BOMenuKichThuocMon()
        {
            MenuKichThuocMon = new MENUKICHTHUOCMON();
            MenuMon = new MENUMON();
            LoaiBan = new LOAIBAN();
        }

        public BOMenuKichThuocMon(Data.MENUKICHTHUOCMON menuKichThuocMon, Data.MENUMON menuMon)
        {
            MenuKichThuocMon = menuKichThuocMon;
            MenuMon = menuMon;
        }
        public static List<BOMenuKichThuocMon> GetAll(int MonID, Transit mTransit)
        {
            FrameworkRepository<MENUKICHTHUOCMON> frmKichThuocMon = new FrameworkRepository<MENUKICHTHUOCMON>(mTransit.KaraokeEntities);
            FrameworkRepository<MENUMON> frmmenuMon = new FrameworkRepository<MENUMON>(mTransit.KaraokeEntities);
            FrameworkRepository<LOAIBAN> frmLoaiBan = new FrameworkRepository<LOAIBAN>(mTransit.KaraokeEntities);

            return (from k in frmKichThuocMon.Query()
                    join m in frmmenuMon.Query() on k.MonID equals m.MonID
                    join l in frmLoaiBan.Query() on k.LoaiBanID equals l.LoaiBanID
                    where k.MonID == MonID && k.Deleted == false
                    select new BOMenuKichThuocMon
                    {
                        MenuKichThuocMon = k,
                        MenuMon = m,
                        LoaiBan = l
                    }).ToList();
        }

        private static int Them(BOMenuKichThuocMon item, Transit mTransit, FrameworkRepository<MENUKICHTHUOCMON> frm)
        {
            frm.AddObject(item.MenuKichThuocMon);
            return item.MenuKichThuocMon.KichThuocMonID;
        }

        private static int Sua(BOMenuKichThuocMon item, Transit mTransit, FrameworkRepository<MENUKICHTHUOCMON> frm)
        {
            frm.Update(item.MenuKichThuocMon);
            return item.MenuKichThuocMon.KichThuocMonID;
        }

        private static int Xoa(BOMenuKichThuocMon item, Transit mTransit, FrameworkRepository<MENUKICHTHUOCMON> frm)
        {
            frm.DeleteObject(item.MenuKichThuocMon);
            return item.MenuKichThuocMon.KichThuocMonID;
        }

        public static void Luu(List<BOMenuKichThuocMon> lsArray, List<BOMenuKichThuocMon> lsArrayDeleted, Transit mTransit)
        {
            FrameworkRepository<MENUKICHTHUOCMON> frm = new FrameworkRepository<MENUKICHTHUOCMON>(mTransit.KaraokeEntities, mTransit.KaraokeEntities.MENUKICHTHUOCMONs);
            if (lsArray != null)
                foreach (BOMenuKichThuocMon item in lsArray)
                {
                    if (item.MenuKichThuocMon.KichThuocMonID > 0)
                        Sua(item, mTransit, frm);
                    else
                        Them(item, mTransit, frm);
                }
            if (lsArrayDeleted != null)
                foreach (BOMenuKichThuocMon item in lsArrayDeleted)
                {
                    Xoa(item, mTransit, frm);
                }
            frm.Commit();
        }

    }
}
