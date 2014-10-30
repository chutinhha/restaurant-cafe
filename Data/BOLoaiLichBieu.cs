using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLoaiLichBieu
    {
        public static List<LOAILICHBIEU> GetAll(int TheLoaiID)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                return ke.LOAILICHBIEUx.Where(s => s.Deleted == false && s.TheLoaiID == TheLoaiID).ToList();
            }
        }
    }
}
