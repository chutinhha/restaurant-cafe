using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuMon
    {
        public Data.MENUMON MenuMon { get; set; }
        public Data.MENUNHOM MenuNhom { get; set; }
        public BOMenuMon()
        {
            MenuMon = new MENUMON();
            MenuNhom = new MENUNHOM();
        }

        public BOMenuMon(Data.MENUMON menuMon, Data.MENUNHOM menuNhom)
        {
            MenuMon = menuMon;
            MenuNhom = menuNhom;
        }

        public static List<BOMenuMon> GetAll(int GroupID, bool IsBanHang, Transit mTransit)
        {
            return GetAll(GroupID, IsBanHang, false, mTransit);
        }
        public static List<BOMenuMon> GetAll(int GroupID, bool IsBanHang, bool IsVisual, Transit mTransit)
        {
            FrameworkRepository<MENUMON> frm = new FrameworkRepository<MENUMON>(mTransit.KaraokeEntities, mTransit.KaraokeEntities.MENUMONs);
            FrameworkRepository<MENUNHOM> frmNhom = new FrameworkRepository<MENUNHOM>(mTransit.KaraokeEntities, mTransit.KaraokeEntities.MENUNHOMs);
            var lsArray = from m in frm.Query()
                          join n in frmNhom.Query() on (int)m.NhomID equals (int)n.NhomID
                          select new BOMenuMon
                              {
                                  MenuMon = m,
                                  MenuNhom = n
                              };
            if (GroupID > -1)
                lsArray = lsArray.Where(s => s.MenuMon.NhomID == GroupID && s.MenuMon.Deleted == false).OrderBy(s => s.MenuMon.SapXep);
            if (IsBanHang)
                lsArray = lsArray.Where(s => s.MenuMon.SoLuongKichThuocMon > 0).OrderBy(s => s.MenuMon.SapXep);
            if (IsVisual)
                lsArray = lsArray.Where(s => s.MenuMon.Visual == true).OrderBy(s => s.MenuMon.SapXep);
            return lsArray.OrderBy(s => s.MenuMon.SapXep).ToList();
        }

        public static int Them(BOMenuMon item, Transit mTransit)
        {
            FrameworkRepository<MENUMON> frm = new FrameworkRepository<MENUMON>(mTransit.KaraokeEntities, mTransit.KaraokeEntities.MENUMONs);
            frm.AddObject(item.MenuMon);
            frm.Commit();
            return item.MenuMon.MonID;
        }

        public static int Xoa(BOMenuMon item, Transit mTransit)
        {
            FrameworkRepository<MENUMON> frm = new FrameworkRepository<MENUMON>(mTransit.KaraokeEntities, mTransit.KaraokeEntities.MENUMONs);
            frm.DeleteObject(item.MenuMon);
            frm.Commit();
            return item.MenuMon.MonID;
        }

        public static int CapNhat(BOMenuMon item, Transit mTransit)
        {
            FrameworkRepository<MENUMON> frm = new FrameworkRepository<MENUMON>(mTransit.KaraokeEntities, mTransit.KaraokeEntities.MENUMONs);
            frm.Update(item.MenuMon);
            frm.Commit();
            return item.MenuMon.MonID;

        }
    }
}
