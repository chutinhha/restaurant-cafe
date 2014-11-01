using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOTableArea
    {
        public static List<KHU> GetAll()
        {
            using (KaraokeEntities ke=new KaraokeEntities())
            {
                return ke.KHUs.ToList();
            }
        }
    }
}
