using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOTableStatus
    {
        public int TableID { get; set; }
        public int Status { get; set; }

        public static IQueryable<BOTableStatus> GetAll(KaraokeEntities kara)
        {
            return from a in BOBanHang.GetAllNotCompleted(kara)
                   select new BOTableStatus
                   {
                       TableID=(int)a.BanID,
                       Status=(int)a.TrangThaiID
                   };
                   
        }
    }
}
