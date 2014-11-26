using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOCaiDatMayInBep
    {
        public static CAIDATMAYINBEP GetCaiDat(Transit transit)
        {
            CAIDATMAYINBEP bep= FrameworkRepository<CAIDATMAYINBEP>.QueryNoTracking(transit.KaraokeEntities.CAIDATMAYINBEPs).FirstOrDefault();
            if (bep == null)
            {
                bep = new Data.CAIDATMAYINBEP();
            }
            return bep;
        }
    }
}
