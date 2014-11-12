using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BONhapKho
    {
        public static List<NHAPKHO> GetAll(Transit mTransit)
        {
            return mTransit.KaraokeEntities.NHAPKHOes.Where(s => s.Deleted == false).ToList();
        }

        public static int Them(NHAPKHO item, List<CHITIETNHAPKHO> lsArray, Transit mTransit)
        {
            foreach (CHITIETNHAPKHO line in lsArray)
            {
                line.TONKHO = new TONKHO();
                line.TONKHO.KhoID = item.KhoID;
                line.TONKHO.PhatSinhTuTonKhoID = item.KhoID;
                line.TONKHO.NgayHetHan = line.NgayHetHan;
                line.TONKHO.NgaySanXuat = line.NgaySanXuat;
                line.TONKHO.LoaiPhatSinhID = (int)Data.TypeLoaiPhatSinh.NhapKho;
                line.TONKHO.MonID = line.MonID;
                line.TONKHO.LoaiBanID = line.LoaiBanID;
                line.TONKHO.KichThuocBan = line.KichThuocBan;
                line.TONKHO.SoLuongNhap = line.SoLuong;
                line.TONKHO.SoLuongTon = line.TONKHO.SoLuongNhap;
                line.TONKHO.Visual = true;
                line.TONKHO.Deleted = false;
                line.TONKHO.Edit = false;
                line.TONKHO.SoLuongPhatSinh = 0;
                line.TONKHO.GiaNhap = line.GiaMua;
                item.CHITIETNHAPKHOes.Add(line);
                mTransit.KaraokeEntities.TONKHOes.AddObject(line.TONKHO);
            }
            item.TongTien = lsArray.Sum(s => s.SoLuong * s.GiaMua);

            mTransit.KaraokeEntities.NHAPKHOes.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.NhapKhoID;
        }
        public static int Them(NHAPKHO item, Transit mTransit)
        {
            mTransit.KaraokeEntities.NHAPKHOes.AddObject(item);
            return item.NhapKhoID;
        }

        public static int Xoa(int NhapKhoID, Transit mTransit)
        {
            NHAPKHO item = (from x in mTransit.KaraokeEntities.NHAPKHOes where x.NhapKhoID == NhapKhoID select x).First();
            mTransit.KaraokeEntities.NHAPKHOes.Attach(item);
            mTransit.KaraokeEntities.NHAPKHOes.DeleteObject(item);
            return item.NhapKhoID;
        }

        public static int Sua(NHAPKHO item, Transit mTransit)
        {
            NHAPKHO m = (from x in mTransit.KaraokeEntities.NHAPKHOes where x.NhapKhoID == item.NhapKhoID select x).First();
            mTransit.KaraokeEntities.NHAPKHOes.Attach(m);
            m.NhaCungCapID = item.NhaCungCapID;
            m.KhoID = item.KhoID;
            m.ThoiGian = item.ThoiGian;
            m.TongTien = item.TongTien;
            m.Visual = item.Visual;
            m.Edit = false;

            return item.NhapKhoID;
        }

        public static void Luu(List<NHAPKHO> lsArray, List<NHAPKHO> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (NHAPKHO item in lsArray)
                {
                    if (item.NhapKhoID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (NHAPKHO item in lsArrayDeleted)
                {
                    Xoa(item.NhapKhoID, mTransit);
                }
            mTransit.KaraokeEntities.SaveChanges();
        }
    }
}
