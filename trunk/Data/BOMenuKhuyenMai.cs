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
        KaraokeEntities mKaraokeEntities = null;

        public BOMenuKhuyenMai()
        {
            MenuKhuyenMai = new MENUKHUYENMAI();
            KichThuocMonChinh = new BOMenuKichThuocMon();
            KichThuocMonTang = new BOMenuKichThuocMon();
        }

        public BOMenuKhuyenMai(Data.Transit transit)
        {
            mKaraokeEntities = new KaraokeEntities();
        }

        public List<BOMenuKichThuocMon> GetDanhSachKichThuocMon()
        {
            var res = (from km in mKaraokeEntities.MENUKHUYENMAIs.Where(s => s.Deleted == false).GroupBy(s => s.KichThuocMonID)
                       join ktm in mKaraokeEntities.MENUKICHTHUOCMONs on km.FirstOrDefault().KichThuocMonID equals ktm.KichThuocMonID
                       join mm in mKaraokeEntities.MENUMONs on ktm.MonID equals mm.MonID
                       select new BOMenuKichThuocMon
                       {
                           MenuMon = mm,
                           MenuKichThuocMon = ktm,
                       }).ToList();
            foreach (var item in res)
            {
                GetAll(item);
            }
            return res;

        }

        public void GetAll(Data.BOMenuKichThuocMon item)
        {
            var res = from km in mKaraokeEntities.MENUKHUYENMAIs
                      join ktm in mKaraokeEntities.MENUKICHTHUOCMONs on km.KichThuocMonTang equals ktm.KichThuocMonID
                      join mm in mKaraokeEntities.MENUMONs on ktm.MonID equals mm.MonID
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


        public IQueryable<BOMenuKhuyenMai> GetAllGroupBy()
        {
            return from km in mKaraokeEntities.MENUKHUYENMAIs
                   group new { } by new { km.KichThuocMonID, km } into resultSet
                   select new BOMenuKhuyenMai()
                      {
                          MenuKhuyenMai = resultSet.Key.km
                      };

        }

        public void Luu(List<BOMenuKhuyenMai> lsArray)
        {
            foreach (BOMenuKhuyenMai item in lsArray)
            {
                if (item.MenuKhuyenMai.KhuyenMaiID == 0)
                {
                    mKaraokeEntities.MENUKHUYENMAIs.AddObject(item.MenuKhuyenMai);
                }
                else if (item.MenuKhuyenMai.Deleted == true)
                {
                    mKaraokeEntities.MENUKHUYENMAIs.DeleteObject(item.MenuKhuyenMai);
                }
            }
            mKaraokeEntities.SaveChanges();
        }

        public void Luu(List<BOMenuKichThuocMon> lsArray)
        {
            foreach (BOMenuKichThuocMon item in lsArray)
            {
                foreach (BOMenuKhuyenMai line in item.DanhSachKhuyenMai)
                {
                    if (line.MenuKhuyenMai.KhuyenMaiID == 0)
                    {
                        mKaraokeEntities.MENUKHUYENMAIs.AddObject(line.MenuKhuyenMai);
                    }
                    else if (line.MenuKhuyenMai.Deleted == true)
                    {
                        mKaraokeEntities.MENUKHUYENMAIs.DeleteObject(line.MenuKhuyenMai);
                    }
                }
            }
            mKaraokeEntities.SaveChanges();
        }


        public void Refresh()
        {
            mKaraokeEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, mKaraokeEntities.MENUKHUYENMAIs);
        }

    }
}
