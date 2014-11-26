using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLichBieuDinhKy
    {
        public LICHBIEUDINHKY LichBieuDinhKy { get; set; }
        public MENULOAIGIA MenuLoaiGia { get; set; }
        FrameworkRepository<LICHBIEUDINHKY> frmLichBieuDinhKy = null;
        FrameworkRepository<MENULOAIGIA> frmMenuLoaiGia = null;
        FrameworkRepository<LOAILICHBIEU> frmLoaiLichBieu;
        public BOLichBieuDinhKy(Transit transit)
        {
            transit.KaraokeEntities = new KaraokeEntities();
            frmLichBieuDinhKy = new FrameworkRepository<LICHBIEUDINHKY>(transit.KaraokeEntities, transit.KaraokeEntities.LICHBIEUDINHKies);
            frmMenuLoaiGia = new FrameworkRepository<MENULOAIGIA>(transit.KaraokeEntities, transit.KaraokeEntities.MENULOAIGIAs);
            frmLoaiLichBieu = new FrameworkRepository<LOAILICHBIEU>(transit.KaraokeEntities, transit.KaraokeEntities.LOAILICHBIEUx);
        }
        public BOLichBieuDinhKy()
        {
            LichBieuDinhKy = new LICHBIEUDINHKY();
            MenuLoaiGia = new MENULOAIGIA();
        }
        public IQueryable<LICHBIEUDINHKY> GetAllVisual()
        {
            DateTime dt = DateTime.Now;
            int dayOfWeek = (int)dt.DayOfWeek;
            TimeSpan ts = new TimeSpan(dt.Hour, dt.Minute, dt.Second);
            //tim theo ngay trong tuan
            var query1 = from b in frmLichBieuDinhKy.Query()
                         where
                            b.Deleted == false &&
                            b.Visual == true &&
                            b.TheLoaiID == 1 &&
                            (
                                (dayOfWeek >= b.GiaTriBatDau && dayOfWeek <= b.GiaTriKetThuc && b.GiaTriBatDau < b.GiaTriKetThuc) ||
                                (
                                    (dayOfWeek >= b.GiaTriBatDau && dayOfWeek <= 6) || (dayOfWeek <= b.GiaTriKetThuc && dayOfWeek >= 0) && b.GiaTriBatDau > b.GiaTriKetThuc
                                )
                            )
                         select b;
            //select a;
            //tim theo ngay trong thang
            var query2 = from b in frmLichBieuDinhKy.Query()
                         where
                            b.Deleted == false &&
                            b.Visual == true &&
                             b.TheLoaiID == 2 &&
                             dt.Day >= b.GiaTriBatDau && dt.Day <= b.GiaTriKetThuc
                         select b;
            //select a;
            //tim theo ngay trong nam
            var query3 = from b in frmLichBieuDinhKy.Query()
                         where
                            b.Deleted == false &&
                            b.Visual == true &&
                             b.TheLoaiID == 3 &&
                             b.GiaTriBatDau == dt.Day && b.GiaTriKetThuc == dt.Month
                         select b;
            return query1.Union(query2).Union(query3).Distinct();
            //select a;
        }
        public IQueryable<BOLichBieuDinhKy> GetMenuLoaiGia()
        {
            
            DateTime dt=DateTime.Now;
            int dayOfWeek = (int)dt.DayOfWeek;            
            TimeSpan ts = new TimeSpan(dt.Hour, dt.Minute, dt.Second);
            var querya=from a in frmMenuLoaiGia.Query()
                       where
                            a.Visual==true &&
                            a.Deleted==false
                        select a;
            var queryb= from b in frmLichBieuDinhKy.Query() 
                        where 
                            ts.CompareTo(b.GioBatDau.Value) >= 0 && ts.CompareTo(b.GioKetThuc.Value) <= 0 &&
                            b.Deleted==false && 
                            b.Visual==true                           
                        select b;

            //tim theo ngay trong tuan
            var query1 = from a in querya
                         join b in queryb on a.LoaiGiaID equals b.LoaiGiaID
                         where
                            b.TheLoaiID == 1 &&
                            (
                                (dayOfWeek >= b.GiaTriBatDau && dayOfWeek <= b.GiaTriKetThuc && b.GiaTriBatDau < b.GiaTriKetThuc) ||
                                (
                                    (dayOfWeek >= b.GiaTriBatDau && dayOfWeek <= 6) || (dayOfWeek <= b.GiaTriKetThuc && dayOfWeek >= 0) && b.GiaTriBatDau > b.GiaTriKetThuc
                                )
                            )
                         select new BOLichBieuDinhKy
                         {
                             MenuLoaiGia = a,
                             LichBieuDinhKy = b
                         };
                         //select a;
            //tim theo ngay trong thang
            var query2 = from a in querya
                         join b in queryb on a.LoaiGiaID equals b.LoaiGiaID
                         where
                             b.TheLoaiID == 2 &&
                             dt.Day >= b.GiaTriBatDau && dt.Day <= b.GiaTriKetThuc
                         select new BOLichBieuDinhKy
                          {
                              MenuLoaiGia = a,
                              LichBieuDinhKy = b
                          };
                         //select a;
            //tim theo ngay trong nam
            var query3 = from a in querya
                         join b in queryb on a.LoaiGiaID equals b.LoaiGiaID
                         where
                             b.TheLoaiID == 3 &&
                             b.GiaTriBatDau == dt.Day && b.GiaTriKetThuc == dt.Month
                         select new BOLichBieuDinhKy
                         {
                             MenuLoaiGia = a,
                             LichBieuDinhKy = b
                         };
                         //select a;
            return 
                    from a in query1.Union(query2).Union(query3).Distinct() select a;
        }
        public IQueryable<BOLichBieuDinhKy> GetAll(Transit mTransit)
        {
            var res = (from lb in frmLichBieuDinhKy.Query()
                       join l in frmMenuLoaiGia.Query() on lb.LoaiGiaID equals l.LoaiGiaID
                       where lb.LoaiGiaID == l.LoaiGiaID
                       orderby lb.UuTien ascending, l.Ten ascending, lb.TenLichBieu ascending
                       select new BOLichBieuDinhKy
                       {
                           LichBieuDinhKy = lb,
                           MenuLoaiGia = l
                       });
            return res;

        }

        public int Them(BOLichBieuDinhKy item, Transit mTransit)
        {
            frmLichBieuDinhKy.AddObject(item.LichBieuDinhKy);
            return item.LichBieuDinhKy.LichBieuDinhKyID;
        }

        public int Xoa(BOLichBieuDinhKy item, Transit mTransit)
        {
            item.LichBieuDinhKy.Deleted = true;
            frmLichBieuDinhKy.Update(item.LichBieuDinhKy);
            return item.LichBieuDinhKy.LichBieuDinhKyID;
        }

        public int Sua(BOLichBieuDinhKy item, Transit mTransit)
        {
            frmLichBieuDinhKy.Update(item.LichBieuDinhKy);
            return item.LichBieuDinhKy.LichBieuDinhKyID;
        }

        public void Luu(List<BOLichBieuDinhKy> lsArray, List<BOLichBieuDinhKy> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (BOLichBieuDinhKy item in lsArray)
                {
                    if (item.LichBieuDinhKy.LichBieuDinhKyID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (BOLichBieuDinhKy item in lsArrayDeleted)
                {
                    Xoa(item, mTransit);
                }

            frmLichBieuDinhKy.Commit();
        }
    }
}
