using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class Transit
    {
        public Data.NHANVIEN NhanVien { get; set; }
        public BAN Ban { get; set; }
        public ProcessOrder.BanHang BanHang { get; set; }
        public string HashMD5 { get; set; }
        public KaraokeEntities KaraokeEntities { get; set; }
        public THAMSO ThamSo { get; set; }
        public ClassStringButton StringButton { get; set; }
        public List<DONVI> ListDonVi { get; set; }
        public Transit()
        {
            StringButton = new ClassStringButton();
            HashMD5 = "KTr";
            KaraokeEntities = new KaraokeEntities();
            KaraokeEntities.ContextOptions.LazyLoadingEnabled = false;
            var list = KaraokeEntities.THAMSOes.Where(o => o.SoMay == 1).ToList<THAMSO>();            
            if (list.Count > 0)
            {
                ThamSo = list[0];
            }
            ListDonVi = BODonVi.GetAll(this);
        }

        public class ClassStringButton
        {
            public string ThemMoi = "Thêm mới";
            public string CapNhat = "Cập nhật";
            public string Huy = "Hủy";
            public string Luu = "Lưu";
            public string Them = "Thêm";


        }
    }
}
