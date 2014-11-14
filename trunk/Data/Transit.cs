using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class Transit
    {
        public class BOTest
        {
            public NHANVIEN NV { get; set; }
            public string Loai { get; set; }
        }
        public Data.NHANVIEN NhanVien { get; set; }
        public BAN Ban { get; set; }        
        //=================
        public KHACHHANG KhachHang { get; set; }
        public THE The { get; set; }
        //================
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
            KaraokeEntities.MENUKICHTHUOCMONs.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            KaraokeEntities.MENUMONs.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            ThamSo = KaraokeEntities.THAMSOes.Where(o => o.SoMay == 1).FirstOrDefault();                        
            NhanVien = KaraokeEntities.NHANVIENs.Where(o => o.NhanVienID == 1).FirstOrDefault();
            KhachHang = KaraokeEntities.KHACHHANGs.FirstOrDefault();            
            The = KaraokeEntities.THEs.FirstOrDefault();            
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
        public static string ConvertDateTimeToString(DateTime dt)
        {
            return String.Format("{0:0000}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}",dt.Year,dt.Month,dt.Day,dt.Hour,dt.Minute,dt.Second);
        }
        public static DateTime ConvertStringToDateTime(string str)
        {
            return Convert.ToDateTime(str);
            //return DateTime.ParseExact(str, "yyyy-MM-dd hh:MM:ss", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
