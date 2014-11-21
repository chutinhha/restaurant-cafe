using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOKhu
    {        
        private Transit mTransit;
        private FrameworkRepository<KHU> frKhu;
        public BOKhu(Transit tran)
        {            
            mTransit = tran;
            frKhu = new FrameworkRepository<KHU>(mTransit.KaraokeEntities, mTransit.KaraokeEntities.KHUs);
        }

        public static IQueryable<KHU> GetAllVisual(Transit transit)
        {
            var query = FrameworkRepository<KHU>.QueryNoTracking(transit.KaraokeEntities.KHUs).Where(o => o.Deleted == false && o.Visual == true);            
            return query;
        }        
        public static IQueryable<KHU> GetAll(Transit transit)
        {
            return FrameworkRepository<KHU>.QueryNoTracking(transit.KaraokeEntities.KHUs).Where(k => k.Deleted == false);            
        }        
        public void Sua(KHU khu)
        {
            frKhu.Update(khu);
        }

        public void Them(KHU item)
        {
            frKhu.AddObject(item);
        }

        public void Xoa(KHU khu)
        {
            frKhu.DeleteObject(khu);
        }

        public void Commit()
        {
            frKhu.Commit();
        }
        public void Refresh()
        {
            frKhu.Refresh();
        }
    }
}
