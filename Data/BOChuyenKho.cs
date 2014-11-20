using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOChuyenKho
    {
        //public Data.CHUYENKHO ChuyenKho { get; set; }
        //public Data.KHO KhoDen { get; set; }
        //public Data.KHO KhoDi { get; set; }
        //public Data.NHANVIEN NhanVien { get; set; }
        //public List<Data.CHITIETHUKHO> ListChiTietHuKho { get; set; }

        //FrameworkRepository<Data.KHO> frmKho = null;
        //FrameworkRepository<Data.NHANVIEN> frmNhanVien = null;
        //FrameworkRepository<Data.CHUYENKHO> frmChuyenKho = null;

        //public BOChuyenKho(Data.Transit transit)
        //{
        //    transit.KaraokeEntities = new KaraokeEntities();
        //    frmKho = new FrameworkRepository<KHO>(transit.KaraokeEntities, transit.KaraokeEntities.KHOes);
        //    frmNhanVien = new FrameworkRepository<NHANVIEN>(transit.KaraokeEntities, transit.KaraokeEntities.NHANVIENs);
        //    frmChuyenKho = new FrameworkRepository<CHUYENKHO>(transit.KaraokeEntities, transit.KaraokeEntities.CHUYENKHOes);
        //}
        //public BOChuyenKho()
        //{
        //    KhoDen = new KHO();
        //    KhoDi = new KHO();
        //    ChuyenKho = new CHUYENKHO();
        //    NhanVien = new NHANVIEN();
        //    ListChiTietHuKho = new List<CHITIETHUKHO>();
        //}

        //public IQueryable<BOChuyenKho> GetAll(Transit mTransit, DateTime dt)
        //{
        //    frmChuyenKho.Refresh();
        //    return (from hk in frmChuyenKho.Query()
        //            join ke in frmKho.Query() on hk.KhoDenID equals ke.KhoID
        //            join ki in frmKho.Query() on hk.KhoDiID equals ki.KhoID
        //            join nv in frmNhanVien.Query() on hk.NhanVienID equals nv.NhanVienID
        //            select new BOChuyenKho
        //            {
        //                ChuyenKho = hk,
        //                KhoDen = ke,
        //                KhoDi = ki,
        //                NhanVien = nv
        //            }
        //                );

        //}

        //public int Them(BOChuyenKho item, List<BOChuyenKho> lsArray, Transit mTransit)
        //{
        //    ThemMoi(item, lsArray, mTransit);
        //    frmChuyenKho.AddObject(item.HuKho);
        //    frmChuyenKho.Commit();
        //    return item.HuKho.HuKhoID;
        //}

        //private int ThemMoi(BOChuyenKho item, List<> lsArray, Transit mTransit)
        //{
        //    if (lsArray != null)
        //    {
        //        foreach (BOChiTietHuKho line in lsArray)
        //        {

        //        }
        //    }
        //    return item.HuKho.HuKhoID;
        //}
        //private int Them(BOChuyenKho item, Transit mTransit)
        //{
        //    frmChuyenKho.AddObject(item.chuy);
        //    return item.HuKho.HuKhoID;
        //}

        //private int Xoa(BOHuKho item, Transit mTransit)
        //{
        //    item.HuKho.Deleted = true;
        //    frmChuyenKho.Update(item.HuKho);
        //    return item.HuKho.HuKhoID;
        //}

        //private int Sua(BOHuKho item, Transit mTransit)
        //{
        //    item.HuKho.Edit = false;
        //    frmChuyenKho.Update(item.HuKho);
        //    return item.HuKho.HuKhoID;
        //}

        //public void Luu(List<BOHuKho> lsArray, List<BOHuKho> lsArrayDeleted, Transit mTransit)
        //{
        //    if (lsArray != null)
        //        foreach (BOHuKho item in lsArray)
        //        {
        //            if (item.HuKho.HuKhoID > 0)
        //                Sua(item, mTransit);
        //            else
        //                ThemMoi(item, null, mTransit);
        //        }
        //    if (lsArrayDeleted != null)
        //        foreach (BOHuKho item in lsArrayDeleted)
        //        {
        //            Xoa(item, mTransit);
        //        }
        //    mTransit.KaraokeEntities.SaveChanges();
        //}
    }
}
