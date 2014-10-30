using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOTheLoaiLichBieu
    {
        public static List<THELOAILICHBIEU> GetAll()
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                return ke.THELOAILICHBIEUx.ToList();
            }
        }
    }
}
