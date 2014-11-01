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
        public static List<LOAIBAN> GetAll(int[] IDs, Transit mTransit)
        {
            if (IDs != null)
                return mTransit.KaraokeEntities.LOAIBANs.Where(s => !IDs.Contains(s.LoaiBanID)).ToList();
            else
                return mTransit.KaraokeEntities.LOAIBANs.ToList();
        }


    }
}
