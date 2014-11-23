using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuKhuyenMai
    {
        public Data.MENUKHUYENMAI MenuKhuyenMai { get; set; }
        public Data.BOMenuKichThuocMon KichThuocMonChinh { get; set; }
        public Data.BOMenuKichThuocMon KichThuocMonTang { get; set; }

        private FrameworkRepository<MENUKHUYENMAI> frmMenuKhuyenMai = null;
        private FrameworkRepository<MENUKICHTHUOCMON> frmKichThuocMon = null;
        private FrameworkRepository<MENUMON> frmMenuMon = null;
        public BOMenuKhuyenMai()
        {
            MenuKhuyenMai = new MENUKHUYENMAI();
            KichThuocMonChinh = new BOMenuKichThuocMon();
            KichThuocMonTang = new BOMenuKichThuocMon();
        }

        public BOMenuKhuyenMai(Data.Transit transit)
        {
            transit.KaraokeEntities = new KaraokeEntities();
            frmMenuKhuyenMai = new FrameworkRepository<MENUKHUYENMAI>(transit.KaraokeEntities, transit.KaraokeEntities.MENUKHUYENMAIs);
            frmKichThuocMon = new FrameworkRepository<MENUKICHTHUOCMON>(transit.KaraokeEntities, transit.KaraokeEntities.MENUKICHTHUOCMONs);
            frmMenuMon = new FrameworkRepository<MENUMON>(transit.KaraokeEntities, transit.KaraokeEntities.MENUMONs);
        }

        public List<BOMenuKichThuocMon> GetDanhSachKichThuocMon(Data.Transit transit)
        {
            var res = (from km in frmMenuKhuyenMai.Query().Where(s => s.Deleted == false).GroupBy(s => s.KichThuocMonID)
                       join ktm in frmKichThuocMon.Query() on km.FirstOrDefault().KichThuocMonID equals ktm.KichThuocMonID
                       join mm in frmMenuMon.Query() on ktm.MonID equals mm.MonID
                       //where km.FirstOrDefault().Deleted == false
                       select new BOMenuKichThuocMon
                       {
                           MenuMon = mm,
                           MenuKichThuocMon = ktm,
                       }).ToList();
            foreach (var item in res)
            {
                GetAll(item, transit);
            }
            return res;

        }

        public void GetAll(Data.BOMenuKichThuocMon item, Data.Transit transit)
        {
            var res = from km in frmMenuKhuyenMai.Query()
                      join ktm in frmKichThuocMon.Query() on km.KichThuocMonTang equals ktm.KichThuocMonID
                      join mm in frmMenuMon.Query() on ktm.MonID equals mm.MonID
                      where km.KichThuocMonID == item.MenuKichThuocMon.KichThuocMonID && km.Deleted == false
                      select new BOMenuKhuyenMai
                      {
                          MenuKhuyenMai = km,
                          KichThuocMonTang = new BOMenuKichThuocMon() { MenuMon = mm, MenuKichThuocMon = ktm }

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
                    if (item.MenuKhuyenMai.KhuyenMaiID > 0)
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
            return item.MenuKhuyenMai.KhuyenMaiID;
        }

        private int Xoa(BOMenuKhuyenMai item, Transit mTransit)
        {
            item.MenuKhuyenMai.Deleted = true;
            frmMenuKhuyenMai.Update(item.MenuKhuyenMai);
            return item.MenuKhuyenMai.KhuyenMaiID;
        }

        private int Sua(BOMenuKhuyenMai item, Transit mTransit)
        {
            item.MenuKhuyenMai.Edit = false;
            frmMenuKhuyenMai.Update(item.MenuKhuyenMai);
            return item.MenuKhuyenMai.KhuyenMaiID;
        }

    }
}
