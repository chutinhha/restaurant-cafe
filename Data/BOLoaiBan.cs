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
        public static List<LOAIBAN> GetAll(int[] IDs)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                if (IDs != null)
                    return ke.LOAIBANs.Where(s => !IDs.Contains(s.LoaiBanID)).ToList();
                else
                    return ke.LOAIBANs.ToList();
            }
        }

        
    }
}
