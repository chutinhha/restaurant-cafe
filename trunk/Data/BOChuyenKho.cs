using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOChuyenKho
    {
        public Data.CHUYENKHO ChuyenKho { get; set; }
        public Data.KHO KhoDen { get; set; }
        public Data.KHO KhoDi { get; set; }
        public Data.NHANVIEN NhanVien { get; set; }
        private KaraokeEntities mKaraokeEntities;

        public BOChuyenKho(Data.Transit transit,KaraokeEntities kara)
        {
            mKaraokeEntities = kara;
            transit.KaraokeEntities = new KaraokeEntities();            
        }
        public BOChuyenKho()
        {
            KhoDen = new KHO();
            KhoDi = new KHO();
            ChuyenKho = new CHUYENKHO();
            NhanVien = new NHANVIEN();            
        }

        public static IQueryable<BOChuyenKho> GetAll(KaraokeEntities kara,KHO kho)
        {
            return null;
        }
        
    }
}
