using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLoaiPhatSinh
    {
        public static List<LOAIPHATSINH> GetAll()
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                return ke.LOAIPHATSINHs.Where(s => s.Deleted == false).ToList();
            }
        }
    }
}
