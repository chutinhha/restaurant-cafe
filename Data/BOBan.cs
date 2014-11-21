using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOBan
    {
        private FrameworkRepository<BAN> frBan;
        private Transit mTransit;        
        public BOBan(Transit transit)
        {
            mTransit = transit;
            frBan = new FrameworkRepository<BAN>(mTransit.KaraokeEntities, mTransit.KaraokeEntities.BANs);
        }                
        public IQueryable<BAN> GetAllTablePerArea(KHU khu)
        {
            return frBan.Query().Where(o => o.KhuID == khu.KhuID && o.Deleted == false);
        }
        public IQueryable<BAN> GetVisualTablePerArea(KHU khu)
        {
            return frBan.Query().Where(o => o.KhuID == khu.KhuID && o.Visual==true && o.Deleted==false);
        }        
        public void Them(BAN ban)
        {
            frBan.AddObject(ban);
        }
        public void Xoa(BAN ban)
        {
            ban.Deleted = true;
            frBan.Update(ban);            
        }
        public void Sua(BAN ban)
        {
            frBan.Update(ban);
        }
        public void Commit()
        {
            frBan.Commit();
        }
    }
}
