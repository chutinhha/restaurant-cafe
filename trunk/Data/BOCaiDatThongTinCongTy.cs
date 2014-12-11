using System.Linq;

namespace Data
{
    public class BOCaiDatThongTinCongTy
    {
        KaraokeEntities mKaraokeEntities = null;
        public BOCaiDatThongTinCongTy(Data.Transit transit)
        {
            mKaraokeEntities = new KaraokeEntities();
        }

        public static CAIDATTHONGTINCONGTY GetQueryNoTracking(Transit transit)
        {
            CAIDATTHONGTINCONGTY item = FrameworkRepository<CAIDATTHONGTINCONGTY>.QueryNoTracking(transit.KaraokeEntities.CAIDATTHONGTINCONGTies).FirstOrDefault();
            if (item == null)
            {
                item = new CAIDATTHONGTINCONGTY();
                item.TenCongTy = "";
                item.TenVietTat = "";
                item.MaSoThue = "";
                item.NguoiDaiDien = "";
                item.DiaChi = "";
                item.DienThoaiBan = "";
                item.DienThoaiDiDong = "";
                item.Email = "";
                item.Fax = "";
            }
            return item;
        }

        public void Luu()
        {
            mKaraokeEntities.SaveChanges();
        }

        public CAIDATTHONGTINCONGTY GetAll()
        {
            CAIDATTHONGTINCONGTY item = mKaraokeEntities.CAIDATTHONGTINCONGTies.FirstOrDefault();
            if (item == null)
            {
                item = new CAIDATTHONGTINCONGTY();
                item.TenCongTy = "";
                item.TenVietTat = "";
                item.MaSoThue = "";
                item.NguoiDaiDien = "";
                item.DiaChi = "";
                item.DienThoaiBan = "";
                item.DienThoaiDiDong = "";
                item.Email = "";
                item.Fax = "";
                mKaraokeEntities.CAIDATTHONGTINCONGTies.AddObject(item);
                mKaraokeEntities.SaveChanges();
            }

            return item;
        }
    }
}