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
        public List<BOMenuKhuyenMai> DanhSachKhuyenMai { get; set; }

        public int KichThuocLoaiBan
        {
            get
            {
                if (MenuKichThuocMon != null && LoaiBan != null)
                    return (int)(MenuKichThuocMon.KichThuocLoaiBan / LoaiBan.KichThuocBan);
                return 0;
            }
        }

        public string TenMon
        {
            get
            {
                if (MenuMon != null && MenuKichThuocMon != null)
                    return MenuMon.TenDai + " (" + MenuKichThuocMon.TenLoaiBan + ")";
                else
                    return "";
            }


        }

        FrameworkRepository<MENUKICHTHUOCMON> frmKichThuocMon = null;
        FrameworkRepository<MENUMON> frmmenuMon = null;
        FrameworkRepository<LOAIBAN> frmLoaiBan = null;

        public BOMenuKichThuocMon()
        {
            MenuKichThuocMon = new MENUKICHTHUOCMON();
            MenuMon = new MENUMON();
            LoaiBan = new LOAIBAN();
            DanhSachKhuyenMai = new List<BOMenuKhuyenMai>();
        }

        public BOMenuKichThuocMon(Data.Transit transit)
        {
            transit.KaraokeEntities = new KaraokeEntities();
            frmKichThuocMon = new FrameworkRepository<MENUKICHTHUOCMON>(transit.KaraokeEntities, transit.KaraokeEntities.MENUKICHTHUOCMONs);
            frmmenuMon = new FrameworkRepository<MENUMON>(transit.KaraokeEntities, transit.KaraokeEntities.MENUMONs);
            frmLoaiBan = new FrameworkRepository<LOAIBAN>(transit.KaraokeEntities, transit.KaraokeEntities.LOAIBANs);
        }

        public BOMenuKichThuocMon(Data.MENUKICHTHUOCMON menuKichThuocMon, Data.MENUMON menuMon)
        {
            MenuKichThuocMon = menuKichThuocMon;
            MenuMon = menuMon;
        }
        public IQueryable<BOMenuKichThuocMon> GetAll(int MonID, Transit mTransit)
        {
            return (from k in frmKichThuocMon.Query()
                    join m in frmmenuMon.Query() on k.MonID equals m.MonID
                    join l in frmLoaiBan.Query() on k.LoaiBanID equals l.LoaiBanID
                    where k.MonID == MonID && k.Deleted == false
                    select new BOMenuKichThuocMon
                    {
                        MenuKichThuocMon = k,
                        MenuMon = m,
                        LoaiBan = l
                    });
        }

        private int Them(BOMenuKichThuocMon item, Transit mTransit)
        {
            frmKichThuocMon.AddObject(item.MenuKichThuocMon);
            return item.MenuKichThuocMon.KichThuocMonID;
        }

        private int Sua(BOMenuKichThuocMon item, Transit mTransit)
        {
            frmKichThuocMon.Update(item.MenuKichThuocMon);
            return item.MenuKichThuocMon.KichThuocMonID;
        }

        private int Xoa(BOMenuKichThuocMon item, Transit mTransit)
        {
            frmKichThuocMon.DeleteObject(item.MenuKichThuocMon);
            return item.MenuKichThuocMon.KichThuocMonID;
        }

        public void Luu(List<BOMenuKichThuocMon> lsArray, List<BOMenuKichThuocMon> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (BOMenuKichThuocMon item in lsArray)
                {
                    if (item.MenuKichThuocMon.KichThuocMonID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (BOMenuKichThuocMon item in lsArrayDeleted)
                {
                    Xoa(item, mTransit);
                }
            frmKichThuocMon.Commit();
        }

    }
}
