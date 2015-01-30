using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BODonVi
    {
        public static IQueryable<DONVI> GetAll(Transit mTransit)
        {
            return mTransit.KaraokeEntities.DONVIs.Where(o => o.Deleted == false);
        }
    }
}
