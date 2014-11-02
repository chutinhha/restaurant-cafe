using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOKhu
    {
        public static List<KHU> GetAllVisual()
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                return ke.KHUs.Where(k => k.Deleted == false).Where(k=>k.Visual==true).ToList();
            }
        }        
        public static List<KHU> GetAll()
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                return ke.KHUs.Where(k=>k.Deleted==false).ToList();
            }
        }                
    }
}
