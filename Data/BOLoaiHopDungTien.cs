using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLoaiHopDungTien
    {
        public static List<LOAIHOPDUNGTIEN> GetAll(Transit mTransit)
        {
            return mTransit.KaraokeEntities.LOAIHOPDUNGTIENs.Where(s => s.Deleted == false).ToList();
        }
    }
}
