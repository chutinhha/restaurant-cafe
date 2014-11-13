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
        public BOBanHang BanHang { get; set; }
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
            //KaraokeEntities.NHANVIENs.MergeOption = System.Data.Objects.MergeOption.NoTracking;            
            KaraokeEntities.MENUKICHTHUOCMONs.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            KaraokeEntities.MENUMONs.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            ThamSo = KaraokeEntities.THAMSOes.Where(o => o.SoMay == 1).FirstOrDefault();                        
            NhanVien = KaraokeEntities.NHANVIENs.Where(o => o.NhanVienID == 1).FirstOrDefault();
            KhachHang = KaraokeEntities.KHACHHANGs.FirstOrDefault();            
            The = KaraokeEntities.THEs.FirstOrDefault();

            Data.FrameworkRepository<NHANVIEN> fr1 = new FrameworkRepository<NHANVIEN>();
            NHANVIEN nv = fr1.Query().Where(o => o.NhanVienID == 4).FirstOrDefault();
            nv.TenNhanVien = "Khoa1";
            fr1.Update(nv);            

            nv.TenNhanVien = "Hoc1";
            fr1.Update(nv);
            fr1.Commit();

            fr1.DeleteObject(nv);
            fr1.Commit();

            fr1.AddObject(nv);
            fr1.Commit();
            //NHANVIEN nv;
            //using (var en=new KaraokeEntities())
            //{
            //    nv = en.NHANVIENs.Where(o => o.NhanVienID == 2).FirstOrDefault();                
            //}
            //NhanVien.TenNhanVien = "NV111";            
            //KaraokeEntities.NHANVIENs.Attach(nv);
            //nv.TenNhanVien = "NV222";
            //KaraokeEntities.SaveChanges();
            //NHANVIEN nv = new NHANVIEN();
            //nv.TenDangNhap = "ABC";
            //KaraokeEntities.NHANVIENs.AddObject(nv);
            //KaraokeEntities.SaveChanges();


            //NhanVien.TenNhanVien = "Khoa2";
            //KaraokeEntities.SaveChanges();

            //NhanVien.TenNhanVien = "Khoa1";
            //KaraokeEntities.NHANVIENs.Attach(NhanVien);
            //KaraokeEntities.SaveChanges();
            
            //KaraokeEntities.NHANVIENs.Attach(NhanVien);
            //NhanVien.TenNhanVien = "Khoa3";
            //KaraokeEntities.SaveChanges();
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
