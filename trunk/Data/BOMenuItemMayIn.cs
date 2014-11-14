using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuItemMayIn
    {
        private FrameworkRepository<MENUITEMMAYIN> frmMenuItemMayIn = null;
        private FrameworkRepository<MAYIN> frmMayIn = null;
        private FrameworkRepository<MENUMON> frmMenuMon = null;

        public MENUITEMMAYIN MenuItemMayIn { get; set; }
        public MAYIN MayIn { get; set; }
        public MENUMON MenuMon { get; set; }

        public BOMenuItemMayIn(Data.Transit transit)
        {
            transit.KaraokeEntities = new KaraokeEntities();
            frmMenuItemMayIn = new FrameworkRepository<MENUITEMMAYIN>(transit.KaraokeEntities, transit.KaraokeEntities.MENUITEMMAYINs);
            frmMayIn = new FrameworkRepository<MAYIN>(transit.KaraokeEntities, transit.KaraokeEntities.MAYINs);
            frmMenuMon = new FrameworkRepository<MENUMON>(transit.KaraokeEntities, transit.KaraokeEntities.MENUMONs);
        }

        public BOMenuItemMayIn()
        {
            MenuItemMayIn = new MENUITEMMAYIN();
            MayIn = new MAYIN();
            MenuMon = new MENUMON();
        }

        public IQueryable<BOMenuItemMayIn> GetAll(int MonID, Transit mTransit)
        {
            return (from mim in frmMenuItemMayIn.Query()
                    join mi in frmMayIn.Query() on mim.MayInID equals mi.MayInID
                    join m in frmMenuMon.Query() on mim.MonID equals m.MonID
                    where mim.Deleted == false && mim.MonID == MonID
                    select new BOMenuItemMayIn
                    {
                        MenuItemMayIn = mim,
                        MayIn = mi,
                        MenuMon = m
                    });
        }

        public void Luu(List<BOMenuItemMayIn> lsArray, List<BOMenuItemMayIn> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (BOMenuItemMayIn item in lsArray)
                {
                    Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (BOMenuItemMayIn item in lsArrayDeleted)
                {
                    Xoa(item, mTransit);
                }
            frmMenuItemMayIn.Commit();
        }

        private int Them(BOMenuItemMayIn item, Transit mTransit)
        {
            frmMenuItemMayIn.AddObject(item.MenuItemMayIn);
            return item.MenuItemMayIn.MayInID;
        }

        private int Xoa(BOMenuItemMayIn item, Transit mTransit)
        {
            frmMenuItemMayIn.DeleteObject(item.MenuItemMayIn);
            return item.MenuItemMayIn.MayInID;
        }
    }
}
