using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLoaiPhatSinh
    {
        public static List<LOAIPHATSINH> GetAll(Transit mTransit)
        {
            return mTransit.KaraokeEntities.LOAIPHATSINHs.Where(s => s.Deleted == false).ToList();
        }
    }

    public enum TypeLoaiPhatSinh
    {
        NhapKho = 1,
        XuatKho,
        ChuyenKho,
        MatKho,
        ChinhKho
    }
}
