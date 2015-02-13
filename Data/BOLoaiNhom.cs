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
            return (from a in mTransit.KaraokeEntities.MENULOAINHOMs
                   orderby a.LoaiNhomID
                   select a).ToList();
        }
        public static IQueryable<MENULOAINHOM> GetAll(KaraokeEntities kara, int limit)
        {
            return (from x in kara.MENULOAINHOMs
                    where x.Deleted == false && x.Visual == true
                    orderby x.LoaiNhomID
                    select x).Take(limit);
        }
    }
}
