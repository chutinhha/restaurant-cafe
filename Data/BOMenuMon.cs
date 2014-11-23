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

        FrameworkRepository<MENUMON> frmMon = null;
        FrameworkRepository<MENUNHOM> frmNhom = null;

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

        public BOMenuMon(Data.Transit transit)
        {
            frmMon = new FrameworkRepository<MENUMON>(transit.KaraokeEntities, transit.KaraokeEntities.MENUMONs);
            frmNhom = new FrameworkRepository<MENUNHOM>(transit.KaraokeEntities, transit.KaraokeEntities.MENUNHOMs);
        }

        public IQueryable<BOMenuMon> GetAll(int GroupID, bool IsBanHang, Transit mTransit)
        {
            return GetAll(GroupID, IsBanHang, false, mTransit);
        }
        public IQueryable<BOMenuMon> GetAll(int GroupID, bool IsBanHang, bool IsVisual, Transit mTransit)
        {

            var lsArray = from m in frmMon.Query()
                          join n in frmNhom.Query() on (int)m.NhomID equals (int)n.NhomID
                          select new BOMenuMon
                              {
                                  MenuMon = m,
                                  MenuNhom = n
                              };
            if (GroupID > -1)
                lsArray = lsArray.Where(s => s.MenuMon.NhomID == GroupID && s.MenuMon.Deleted == false);
            if (IsBanHang)
                lsArray = lsArray.Where(s => s.MenuMon.SoLuongKichThuocMon > 0);
            if (IsVisual)
                lsArray = lsArray.Where(s => s.MenuMon.Visual == true);
            return lsArray.OrderBy(s => s.MenuMon.SapXep);
        }

        public int Them(BOMenuMon item, Transit mTransit)
        {
            frmMon.AddObject(item.MenuMon);
            SapXep((int)item.MenuMon.NhomID, mTransit);
            UpdateSoLuongMon((int)item.MenuMon.NhomID, mTransit);
            frmMon.Commit();
            return item.MenuMon.MonID;
        }

        public int Xoa(BOMenuMon item, Transit mTransit)
        {
            item.MenuMon.Deleted = true;
            frmMon.Update(item.MenuMon);
            SapXep((int)item.MenuMon.NhomID, mTransit);
            UpdateSoLuongMon((int)item.MenuMon.NhomID, mTransit);
            frmMon.Commit();
            return item.MenuMon.MonID;
        }

        public int Sua(BOMenuMon item, Transit mTransit)
        {
            frmMon.Update(item.MenuMon);
            SapXep((int)item.MenuMon.NhomID, mTransit);
            UpdateSoLuongMon((int)item.MenuMon.NhomID, mTransit);
            frmMon.Commit();
            return item.MenuMon.MonID;

        }

        public void SapXep(int NhomID, Data.Transit mTransit)
        {
            var Parameter_NhomID = new System.Data.SqlClient.SqlParameter("@NhomID", System.Data.SqlDbType.Int);
            Parameter_NhomID.Value = NhomID;
            mTransit.KaraokeEntities.ExecuteStoreCommand("SP_SAPXEP_MENUMON @NhomID", Parameter_NhomID);
        }

        public void UpdateSoLuongMon(int NhomID, Data.Transit mTransit)
        {
            var Parameter_NhomID = new System.Data.SqlClient.SqlParameter("@NhomID", System.Data.SqlDbType.Int);
            Parameter_NhomID.Value = NhomID;
            mTransit.KaraokeEntities.ExecuteStoreCommand("SP_UPDATE_SOLUONGMON @NhomID", Parameter_NhomID);
        }
    }
}
