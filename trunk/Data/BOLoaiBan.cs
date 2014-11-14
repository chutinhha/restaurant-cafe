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
        public static List<LOAIBAN> GetAllNoTracking(Transit mTransit)
        {
            return FrameworkRepository<LOAIBAN>.QueryNoTracking(mTransit.KaraokeEntities.LOAIBANs).ToList();
        }


    }
}
