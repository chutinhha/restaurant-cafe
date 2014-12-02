using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOXuLyKhoLoai
    {
        public IQueryable<XULYKHOLOAI> GetQueryNoTracking(Data.Transit mTransit)
        {
            return FrameworkRepository<XULYKHOLOAI>.QueryNoTracking(mTransit.KaraokeEntities.XULYKHOLOAIs);
        }
    }
}
