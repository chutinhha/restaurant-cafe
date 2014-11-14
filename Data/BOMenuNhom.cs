using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuNhom
    {
        public Data.MENUNHOM MenuNhom { get; set; }
        public Data.MENULOAINHOM MenuLoaiNhom { get; set; }
        public BOMenuNhom()
        {
            MenuNhom = new MENUNHOM();
            MenuLoaiNhom = new MENULOAINHOM();
        }

        public BOMenuNhom(Data.MENUNHOM menuNhom, Data.MENULOAINHOM menuLoaiNhom)
        {
            MenuNhom = menuNhom;
            MenuLoaiNhom = menuLoaiNhom;
        }
        public static List<BOMenuNhom> GetAll(int LoaiNhomID, bool IsBanHang, Transit mTransit)
        {
            return GetAll(LoaiNhomID, IsBanHang, false, mTransit);
        }
        public static List<BOMenuNhom> GetAll(int LoaiNhomID, bool IsBanHang, bool IsVisual, Transit mTransit)
        {
            FrameworkRepository<MENUNHOM> frm = new FrameworkRepository<MENUNHOM>(mTransit.KaraokeEntities, mTransit.KaraokeEntities.MENUNHOMs);

            var lsArray = from n in frm.Query() select new BOMenuNhom { MenuNhom = n };
            if (LoaiNhomID > 0)
                lsArray = lsArray.Where(s => s.MenuNhom.LoaiNhomID == LoaiNhomID && s.MenuNhom.Deleted == false).OrderBy(s => s.MenuNhom.SapXep);
            if (IsBanHang)
                lsArray = lsArray.Where(s => s.MenuNhom.Visual == true && s.MenuNhom.SoLuongMon > 0).OrderBy(s => s.MenuNhom.SapXep);
            if (IsVisual)
                lsArray = lsArray.Where(s => s.MenuNhom.Visual == true).OrderBy(s => s.MenuNhom.SapXep);

            return lsArray.ToList();

        }

        public static int Them(BOMenuNhom item, Transit mTransit)
        {
            FrameworkRepository<MENUNHOM> frm = new FrameworkRepository<MENUNHOM>(mTransit.KaraokeEntities, mTransit.KaraokeEntities.MENUNHOMs);
            frm.AddObject(item.MenuNhom);
            frm.Commit();
            return item.MenuNhom.NhomID;
        }

        public static int Xoa(BOMenuNhom item, Transit mTransit)
        {
            FrameworkRepository<MENUNHOM> frm = new FrameworkRepository<MENUNHOM>(mTransit.KaraokeEntities, mTransit.KaraokeEntities.MENUNHOMs);
            frm.DeleteObject(item.MenuNhom);
            frm.Commit();
            return item.MenuNhom.NhomID;
        }

        public static int CapNhat(BOMenuNhom item, Transit mTransit)
        {
            FrameworkRepository<MENUNHOM> frm = new FrameworkRepository<MENUNHOM>(mTransit.KaraokeEntities, mTransit.KaraokeEntities.MENUNHOMs);
            frm.Update(item.MenuNhom);
            frm.Commit();
            MENUNHOM m = (from x in mTransit.KaraokeEntities.MENUNHOMs where x.NhomID == item.MenuNhom.NhomID select x).First();
            return item.MenuNhom.NhomID;
        }
    }
}
