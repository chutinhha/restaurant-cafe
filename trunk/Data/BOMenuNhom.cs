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
        
        public static IQueryable<MENUNHOM> GetAll(KaraokeEntities kara)
        {
            return kara.MENUNHOMs.Where(o =>o.Deleted == false);
        }
        public static IQueryable<MENUNHOM> GetAll(KaraokeEntities kara,MENULOAINHOM loaiNhom)
        {
            return kara.MENUNHOMs.Where(o=>o.Visual==true && o.Deleted==false&&o.LoaiNhomID==loaiNhom.LoaiNhomID);
        }
        public IQueryable<BOMenuNhom> GetAll(int LoaiNhomID, bool IsBanHang, bool IsSoLuongChoPhepTonKho, bool IsSoLuongKhongChoPhepTonKho, Transit mTransit)
        {
            return GetAll(LoaiNhomID, IsBanHang, IsSoLuongChoPhepTonKho, IsSoLuongKhongChoPhepTonKho, false, mTransit);
        }
        public IQueryable<BOMenuNhom> GetAll(int LoaiNhomID, bool IsBanHang, bool IsSoLuongChoPhepTonKho, bool IsSoLuongKhongChoPhepTonKho, bool IsVisual, Transit mTransit)
        {
            var lsArray = from n in frmNhom.Query().Where(o=>o.Deleted==false) select new BOMenuNhom { MenuNhom = n };
            if (LoaiNhomID > 0)
                lsArray = lsArray.Where(s => (s.MenuNhom.LoaiNhomID == LoaiNhomID || LoaiNhomID==0) && s.MenuNhom.Deleted == false);
            if (IsBanHang)
            {
                if (IsSoLuongChoPhepTonKho && IsSoLuongKhongChoPhepTonKho)
                    lsArray = lsArray.Where(s => s.MenuNhom.Visual == true && s.MenuNhom.SLMonChoPhepTonKho > 0 || s.MenuNhom.SLMonKhongChoPhepTonKho > 0);
                else if (IsSoLuongChoPhepTonKho)
                    lsArray = lsArray.Where(s => s.MenuNhom.Visual == true && s.MenuNhom.SLMonChoPhepTonKho > 0);
                else if (IsSoLuongKhongChoPhepTonKho)
                    lsArray = lsArray.Where(s => s.MenuNhom.Visual == true && s.MenuNhom.SLMonKhongChoPhepTonKho > 0);


            }
            else
            {
                if (!IsSoLuongChoPhepTonKho)
                    lsArray = lsArray.Where(s => s.MenuNhom.SLMonKhongChoPhepTonKho > 0);
                if (!IsSoLuongKhongChoPhepTonKho)
                    lsArray = lsArray.Where(s => s.MenuNhom.SLMonChoPhepTonKho > 0);
            }
            if (IsVisual)
                lsArray = lsArray.Where(s => s.MenuNhom.Visual == true);
            return lsArray.OrderBy(s => s.MenuNhom.SapXep);

        }

        public int Them(BOMenuNhom item, Transit mTransit)
        {
            frmNhom.AddObject(item.MenuNhom);
            frmNhom.Commit();
            SapXep((int)item.MenuNhom.LoaiNhomID, mTransit);
            return item.MenuNhom.NhomID;
        }

        public int Xoa(BOMenuNhom item, Transit mTransit)
        {
            item.MenuNhom.Deleted = true;
            frmNhom.Update(item.MenuNhom);
            frmNhom.Commit();
            SapXep((int)item.MenuNhom.LoaiNhomID, mTransit);
            return item.MenuNhom.NhomID;
        }

        public int Sua(BOMenuNhom item, Transit mTransit)
        {
            frmNhom.Update(item.MenuNhom);
            frmNhom.Commit();
            if (item.MenuNhom.LoaiNhomID!=null)
            {
                SapXep((int)item.MenuNhom.LoaiNhomID, mTransit);
            }
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
