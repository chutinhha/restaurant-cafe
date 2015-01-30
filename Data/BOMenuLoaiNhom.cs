using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuLoaiNhom
    {
        public static List<MENULOAINHOM> GetAll(Transit mTransit)
        {
            return mTransit.KaraokeEntities.MENULOAINHOMs.ToList();
        }
        public static IQueryable<MENULOAINHOM> GetAll(KaraokeEntities kara)
        {
            return kara.MENULOAINHOMs;
        }        
    }
}
