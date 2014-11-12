using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BODonVi
    {
        public static List<DONVI> GetAll(Transit mTransit)
        {
            return mTransit.KaraokeEntities.DONVIs.ToList();
        }
    }
}
