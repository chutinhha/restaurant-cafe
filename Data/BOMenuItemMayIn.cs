using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuItemMayIn
    {
        public static List<MENUITEMMAYIN> GetAll(int MonID)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                var res = (from mi in ke.MENUITEMMAYINs
                           join m in ke.MENUMONs on mi.MonID equals m.MonID
                           join i in ke.MAYINs on mi.MayInID equals i.MayInID
                           where mi.Deleted == false && mi.Deleted == false && mi.MonID == MonID
                           select new
                           {
                               MENUITEMMAYIN = mi,
                               MENUMONs = m,
                               MAYINs = i
                           }).ToList().Select(s => s.MENUITEMMAYIN);
                return res.ToList();
            }
        }

        public static int Them(MENUITEMMAYIN item)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                ke.MENUITEMMAYINs.AddObject(item);
                ke.SaveChanges();
                return item.MayInID;
            }
        }

        public static int Xoa(int MayInID, int MonID)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                MENUITEMMAYIN item = (from x in ke.MENUITEMMAYINs where x.MayInID == MayInID && x.MonID == MonID select x).First();
                ke.MENUITEMMAYINs.DeleteObject(item);
                ke.SaveChanges();
                return item.MayInID;
            }
        }
    }
}
