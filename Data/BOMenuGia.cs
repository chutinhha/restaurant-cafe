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
        public BOMenuGia()
        {
            MenuGia = new MENUGIA();
            LoaiGia = new MENULOAIGIA();
        }

        public BOMenuGia(Data.Transit transit)
        {
            transit.KaraokeEntities = new KaraokeEntities();
            frmMenuGia = new FrameworkRepository<MENUGIA>(transit.KaraokeEntities, transit.KaraokeEntities.MENUGIAs);
            frmLoaiGia = new FrameworkRepository<MENULOAIGIA>(transit.KaraokeEntities, transit.KaraokeEntities.MENULOAIGIAs);
        }
        FrameworkRepository<MENUGIA> frmMenuGia = null;
        FrameworkRepository<MENULOAIGIA> frmLoaiGia = null;

        public List<BOMenuGia> GetAll(int KichThuocMonID, Transit mTransit)
        {
            var res = (from g in frmMenuGia.Query()
                       join l in frmLoaiGia.Query() on g.LoaiGiaID equals l.LoaiGiaID
                       where g.KichThuocMonID == KichThuocMonID
                       select new BOMenuGia
                       {
                           MenuGia = g,
                           LoaiGia = l
                       }).ToList();
            return res;
        }

        private int Sua(BOMenuGia item, Transit mTransit)
        {
            frmMenuGia.Update(item.MenuGia);            
            return item.MenuGia.GiaID;
        }

        public void Luu(List<BOMenuGia> lsArray, Transit mTransit)
        {
            foreach (BOMenuGia item in lsArray)
            {
                Sua(item, mTransit);
            }
            frmMenuGia.Commit();
        }
    }
}
