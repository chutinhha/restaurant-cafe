using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLoaiNhom
    {
        public static List<MENULOAINHOM> GetAll(Transit mTransit)
        {
            return mTransit.KaraokeEntities.MENULOAINHOMs.ToList();
        }
    }
}
