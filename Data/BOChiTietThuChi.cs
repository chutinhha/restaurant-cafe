using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOChiTietThuChi
    {
        public CHITIETTHUCHI ChiTietThuChi { get; set; }
        public static IQueryable<BOChiTietThuChi> GetAllByThuChiID(int thuChiID, KaraokeEntities kara)
        {
            return from a in kara.CHITIETTHUCHIs
                   where a.ThuChiID == thuChiID
                   select new BOChiTietThuChi
                   {
                       ChiTietThuChi=a
                   };
        }
    }
}
