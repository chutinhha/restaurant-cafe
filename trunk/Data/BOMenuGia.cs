using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuGia
    {
        public MENUGIA MenuGia { get; set; }
        public MENULOAIGIA LoaiGia { get; set; }
        private Transit mTransit;
        FrameworkRepository<MENUGIA> frmMenuGia;
        FrameworkRepository<MENULOAIGIA> frmLoaiGia;
        public BOMenuGia()
        {
            MenuGia = new MENUGIA();
            LoaiGia = new MENULOAIGIA();
        }

        public BOMenuGia(Data.Transit transit)
        {
            mTransit = transit;
            frmMenuGia = new FrameworkRepository<MENUGIA>(transit.KaraokeEntities, transit.KaraokeEntities.MENUGIAs);
            frmLoaiGia = new FrameworkRepository<MENULOAIGIA>(transit.KaraokeEntities, transit.KaraokeEntities.MENULOAIGIAs);
        }
        public IQueryable<BOMenuGia> GetAllByKichThuocMon(MENUKICHTHUOCMON ktm)
        {
            return from a in frmLoaiGia.Query()
                   join b in frmMenuGia.Query() on a.LoaiGiaID equals b.LoaiGiaID
                   where b.KichThuocMonID == ktm.KichThuocMonID
                   select new BOMenuGia
                   {
                       LoaiGia = a,
                       MenuGia = b
                   };
        }
        //public IQueryable<BOMenuGia> GetAllKichThuocMonVaLoaiGia(IQueryable<MENULOAIGIA> query, MENUKICHTHUOCMON ktm)
        //{
        //    return from a in frmMenuGia.Query()
        //           join b in frmLoaiGia.Query() on a.LoaiGiaID equals b.LoaiGiaID
        //           where query.Contains(b) && a.KichThuocMonID==ktm.KichThuocMonID
        //           select new BOMenuGia
        //           {
        //               MenuGia = a,
        //               LoaiGia = b
        //           };
        //}
        public IQueryable<BOMenuGia> GetAll(int KichThuocMonID, Transit mTransit)
        {
            var res = (from g in frmMenuGia.Query()
                       join l in frmLoaiGia.Query() on g.LoaiGiaID equals l.LoaiGiaID
                       where g.KichThuocMonID == KichThuocMonID
                       select new BOMenuGia
                       {
                           MenuGia = g,
                           LoaiGia = l
                       });
            return res;
        }

        private int Sua(BOMenuGia item, Transit mTransit)
        {
            frmMenuGia.Update(item.MenuGia);
            return item.MenuGia.GiaID;
        }
        private int Xoa(BOMenuGia item, Transit mTransit)
        {
            frmMenuGia.DeleteObject(item.MenuGia);
            return item.MenuGia.GiaID;
        }
        private int Them(BOMenuGia item, Transit mTransit)
        {
            frmMenuGia.AddObject(item.MenuGia);
            return item.MenuGia.GiaID;
        }



        public void Luu(List<BOMenuGia> lsArray, Transit mTransit)
        {
            foreach (BOMenuGia item in lsArray)
            {
                if (item.MenuGia.Gia == 0 && item.MenuGia.GiaID > 0)
                    Xoa(item, mTransit);
                else if (item.MenuGia.Gia != 0 && item.MenuGia.GiaID == 0)
                    Them(item, mTransit);
                else if (item.MenuGia.Gia != 0 && item.MenuGia.GiaID > 0)
                    Sua(item, mTransit);
            }
            frmMenuGia.Commit();
        }

        public string TenGia
        {
            get { return String.Format("{0}({1})", LoaiGia.Ten, Utilities.MoneyFormat.ConvertToStringFull(MenuGia.Gia)); }
        }
        public decimal Gia
        {
            get { return MenuGia.Gia; }
        }
    }
}
