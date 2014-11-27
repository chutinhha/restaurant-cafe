using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    /// <summary>
    /// Loại bán
    /// </summary>
    public class BOLoaiBan
    {
        public static IQueryable<LOAIBAN> GetAllNoTracking(Transit mTransit)
        {
            return FrameworkRepository<LOAIBAN>.QueryNoTracking(mTransit.KaraokeEntities.LOAIBANs);
        }
        public static IQueryable<LOAIBAN> GetAllNoTracking(Transit mTransit, int DonViID)
        {
            return FrameworkRepository<LOAIBAN>.QueryNoTracking(mTransit.KaraokeEntities.LOAIBANs).Where(s => s.DonViID == DonViID);
        }

    }
}
