using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOCaiDatMayInHoaDon
    {
        public static CAIDATMAYINHOADON GetCaiDat(Transit transit)
        {
            CAIDATMAYINHOADON cai=FrameworkRepository<CAIDATMAYINHOADON>.QueryNoTracking(transit.KaraokeEntities.CAIDATMAYINHOADONs).FirstOrDefault();
            if (cai==null)
            {
                cai = new CAIDATMAYINHOADON();
            }
            return cai;
        }
    }
}
