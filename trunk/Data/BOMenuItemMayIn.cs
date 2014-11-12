using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuItemMayIn
    {
        public static List<MENUITEMMAYIN> GetAll(int MonID, Transit mTransit)
        {
            var res = (from mi in mTransit.KaraokeEntities.MENUITEMMAYINs
                       join m in mTransit.KaraokeEntities.MENUMONs on mi.MonID equals m.MonID
                       join i in mTransit.KaraokeEntities.MAYINs on mi.MayInID equals i.MayInID
                       where mi.Deleted == false && mi.Deleted == false && mi.MonID == MonID
                       select new
                       {
                           MENUITEMMAYIN = mi,
                           MENUMONs = m,
                           MAYINs = i
                       }).ToList().Select(s => s.MENUITEMMAYIN);
            return res.ToList();
        }

        public static int Them(MENUITEMMAYIN item, Transit mTransit)
        {
            mTransit.KaraokeEntities.MENUITEMMAYINs.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.MayInID;
        }

        public static int Xoa(int MayInID, int MonID, Transit mTransit)
        {
            if ((from x in mTransit.KaraokeEntities.MENUITEMMAYINs where x.MayInID == MayInID && x.MonID == MonID select x).Count() > 0)
            {
                MENUITEMMAYIN item = (from x in mTransit.KaraokeEntities.MENUITEMMAYINs where x.MayInID == MayInID && x.MonID == MonID select x).First();
                mTransit.KaraokeEntities.MENUITEMMAYINs.DeleteObject(item);
                mTransit.KaraokeEntities.SaveChanges();
            }
            return MayInID;
        }


        public static void Luu(List<MENUITEMMAYIN> lsArray, Transit mTransit)
        {
            foreach (MENUITEMMAYIN item in lsArray)
            {
                if (item.Deleted == true)
                {
                    Xoa(item.MayInID, item.MonID, mTransit);
                }
                else
                {
                    int count = (from x in mTransit.KaraokeEntities.MENUITEMMAYINs where x.MayInID == item.MayInID && x.MonID == item.MonID select x).Count();
                    if (count == 0)
                    {
                        Them(item, mTransit);
                    }
                }
            }
        }
    }
}
