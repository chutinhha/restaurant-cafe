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
        public static IQueryable<MENUGIA> GetAll(KaraokeEntities kara)
        {
            return kara.MENUGIAs;
        }
        public static IQueryable<BOMenuGia> GetAllByKichThuocMonVaLoaiGia(KaraokeEntities kara,MENUKICHTHUOCMON ktm,IQueryable<MENULOAIGIA> loaiGia)
        {
            return from a in loaiGia
                   join b in GetAll(kara) on a.LoaiGiaID equals b.LoaiGiaID
                   where b.KichThuocMonID==ktm.KichThuocMonID
                   select new BOMenuGia
                   {
                       LoaiGia = a,
                       MenuGia = b
                   };
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
        public IQueryable<BOMenuGia> GetAllBOMenuGia(int KichThuocMonID, Transit mTransit)
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

        public string TenGiaFull
        {
            get { return String.Format("{0}({1})", LoaiGia.Ten, Utilities.MoneyFormat.ConvertToStringFull(MenuGia.Gia)); }
        }
        public string TenLoaiGia 
        {
            get { return LoaiGia.Ten; }
        }
        public decimal Gia 
        {
            get { return MenuGia.Gia; }
        }
    }
}
