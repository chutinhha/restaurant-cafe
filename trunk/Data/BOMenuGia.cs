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
        public static List<BOMenuGia> GetAll(int KichThuocMonID, Transit mTransit)
        {
            FrameworkRepository<MENUGIA> frmMenuGia = new FrameworkRepository<MENUGIA>(mTransit.KaraokeEntities);
            FrameworkRepository<MENULOAIGIA> frmLoaiGia = new FrameworkRepository<MENULOAIGIA>(mTransit.KaraokeEntities);

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

        private static int Sua(BOMenuGia item, Transit mTransit, FrameworkRepository<MENUGIA> frm)
        {
            frm.Update(item.MenuGia);
            return item.MenuGia.GiaID;
        }

        public static void Luu(List<BOMenuGia> lsArray, Transit mTransit)
        {
            FrameworkRepository<MENUGIA> frm = new FrameworkRepository<MENUGIA>(mTransit.KaraokeEntities);
            foreach (BOMenuGia item in lsArray)
            {
                Sua(item, mTransit, frm);
            }
            mTransit.KaraokeEntities.SaveChanges();
        }
    }
}
