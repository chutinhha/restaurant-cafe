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
        FrameworkRepository<MENUNHOM> frmNhom = null;
        FrameworkRepository<MENULOAINHOM> frmLoaiNhom = null;
        public BOMenuNhom(Data.Transit transit)
        {
            frmNhom = new FrameworkRepository<MENUNHOM>(transit.KaraokeEntities, transit.KaraokeEntities.MENUNHOMs);
            frmLoaiNhom = new FrameworkRepository<MENULOAINHOM>(transit.KaraokeEntities, transit.KaraokeEntities.MENULOAINHOMs);
        }
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
        public IQueryable<BOMenuNhom> GetAll(int LoaiNhomID, bool IsBanHang, Transit mTransit)
        {
            return GetAll(LoaiNhomID, IsBanHang, false, mTransit);
        }
        public IQueryable<BOMenuNhom> GetAll(int LoaiNhomID, bool IsBanHang, bool IsVisual, Transit mTransit)
        {
            frmNhom.Refresh();
            var lsArray = from n in frmNhom.Query() select new BOMenuNhom { MenuNhom = n };
            if (LoaiNhomID > 0)
                lsArray = lsArray.Where(s => s.MenuNhom.LoaiNhomID == LoaiNhomID && s.MenuNhom.Deleted == false).OrderBy(s => s.MenuNhom.SapXep);
            if (IsBanHang)
                lsArray = lsArray.Where(s => s.MenuNhom.Visual == true && s.MenuNhom.SoLuongMon > 0).OrderBy(s => s.MenuNhom.SapXep);
            if (IsVisual)
                lsArray = lsArray.Where(s => s.MenuNhom.Visual == true).OrderBy(s => s.MenuNhom.SapXep);

            return lsArray;

        }

        public int Them(BOMenuNhom item, Transit mTransit)
        {
            frmNhom.AddObject(item.MenuNhom);
            frmNhom.Commit();
            SapXep(item.MenuLoaiNhom.LoaiNhomID, mTransit);
            return item.MenuNhom.NhomID;
        }

        public int Xoa(BOMenuNhom item, Transit mTransit)
        {
            frmNhom.DeleteObject(item.MenuNhom);
            frmNhom.Commit();
            SapXep(item.MenuLoaiNhom.LoaiNhomID, mTransit);
            return item.MenuNhom.NhomID;
        }

        public int Sua(BOMenuNhom item, Transit mTransit)
        {
            frmNhom.Update(item.MenuNhom);
            frmNhom.Commit();
            SapXep(item.MenuLoaiNhom.LoaiNhomID, mTransit);
            return item.MenuNhom.NhomID;
        }

        public void SapXep(int LoaiNhomID, Data.Transit mTransit)
        {
            var Parameter_LoaiNhomID = new System.Data.SqlClient.SqlParameter("@LoaiNhomID", System.Data.SqlDbType.Int);
            Parameter_LoaiNhomID.Value = LoaiNhomID;
            mTransit.KaraokeEntities.ExecuteStoreCommand("SP_SAPXEP_NHOM @LoaiNhomID", Parameter_LoaiNhomID);
        }
    }
}
