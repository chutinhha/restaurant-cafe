using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuKhuyenMai
    {
        public Data.MENUKHUYENMAI MenuKhuyenMai { get; set; }
        public Data.MENUKICHTHUOCMON KichThuocMonChinh { get; set; }
        public Data.MENUKICHTHUOCMON KichThuocMonTang { get; set; }
        public Data.MENUMON MenuMonChinh { get; set; }
        public Data.MENUMON MenuMonTang { get; set; }

        private FrameworkRepository<MENUKHUYENMAI> frmMenuKhuyenMai = null;
        private FrameworkRepository<MENUKICHTHUOCMON> frmKichThuocMon = null;
        private FrameworkRepository<MENUMON> frmMenuMon = null;
        public BOMenuKhuyenMai()
        {
            MenuKhuyenMai = new MENUKHUYENMAI();
            KichThuocMonChinh = new MENUKICHTHUOCMON();
            KichThuocMonTang = new MENUKICHTHUOCMON();
            MenuMonChinh = new MENUMON();
            MenuMonTang = new MENUMON();
        }

        public BOMenuKhuyenMai(Data.Transit transit)
        {
            transit.KaraokeEntities = new KaraokeEntities();
            frmMenuKhuyenMai = new FrameworkRepository<MENUKHUYENMAI>(transit.KaraokeEntities, transit.KaraokeEntities.MENUKHUYENMAIs);
            frmKichThuocMon = new FrameworkRepository<MENUKICHTHUOCMON>(transit.KaraokeEntities, transit.KaraokeEntities.MENUKICHTHUOCMONs);
            frmMenuMon = new FrameworkRepository<MENUMON>(transit.KaraokeEntities, transit.KaraokeEntities.MENUMONs);
        }

        public IQueryable<BOMenuKichThuocMon> GetDanhSachKichThuocMon(Data.Transit transit)
        {
            return from km in frmMenuKhuyenMai.Query()
                   join ktm in frmKichThuocMon.Query() on km.KichThuocMonTang equals ktm.KichThuocMonID
                   join mm in frmMenuMon.Query() on ktm.MonID equals mm.MonID
                   select new BOMenuKichThuocMon
                   {
                       MenuMon = mm,
                       MenuKichThuocMon = ktm
                   };

        }

        public void GetAll(Data.BOMenuKichThuocMon item, Data.Transit transit)
        {
            var res = from km in frmMenuKhuyenMai.Query()
                      join ktm in frmKichThuocMon.Query() on km.KichThuocMonTang equals ktm.KichThuocMonID
                      join mm in frmMenuMon.Query() on ktm.MonID equals mm.MonID
                      where km.KichThuocMonID == item.MenuKichThuocMon.KichThuocMonID
                      select new BOMenuKhuyenMai
                      {
                          MenuKhuyenMai = km,
                          KichThuocMonTang = ktm,
                          MenuMonTang = mm
                      };
            foreach (var line in res)
            {
                item.DanhSachKhuyenMai.Add(line);
            }
        }


        public IQueryable<BOMenuKhuyenMai> GetAllGroupBy(Data.Transit transit)
        {
            return from km in frmMenuKhuyenMai.Query()
                   group new { } by new { km.KichThuocMonID, km } into resultSet
                   select new BOMenuKhuyenMai()
                      {
                          MenuKhuyenMai = resultSet.Key.km
                      };

        }

        public void Luu(List<BOMenuKhuyenMai> lsArray, List<BOMenuKhuyenMai> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (BOMenuKhuyenMai item in lsArray)
                {
                    if (item.MenuKhuyenMai.ID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (BOMenuKhuyenMai item in lsArrayDeleted)
                {
                    Xoa(item, mTransit);
                }
            frmMenuKhuyenMai.Commit();
        }

        private int Them(BOMenuKhuyenMai item, Transit mTransit)
        {
            frmMenuKhuyenMai.AddObject(item.MenuKhuyenMai);
            return item.MenuKhuyenMai.ID;
        }

        private int Xoa(BOMenuKhuyenMai item, Transit mTransit)
        {
            item.MenuKhuyenMai.Deleted = true;
            frmMenuKhuyenMai.Update(item.MenuKhuyenMai);
            return item.MenuKhuyenMai.ID;
        }

        private int Sua(BOMenuKhuyenMai item, Transit mTransit)
        {
            item.MenuKhuyenMai.Edit = false;
            frmMenuKhuyenMai.Update(item.MenuKhuyenMai);
            return item.MenuKhuyenMai.ID;
        }

    }
}
