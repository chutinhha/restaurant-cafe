using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class Transit
    {
        public Data.NHANVIEN NhanVien { get; set; }
        public string HashMD5 { get; set; }
        public KaraokeEntities KaraokeEntities { get; set; }
        public Transit()
        {
            HashMD5 = "KTr";
            KaraokeEntities = new KaraokeEntities();
        }
    }
}
