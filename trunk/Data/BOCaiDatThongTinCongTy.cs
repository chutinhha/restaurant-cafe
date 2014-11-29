using System.Linq;

namespace Data
{
    public class BOCaiDatThongTinCongTy
    {
        private FrameworkRepository<CAIDATTHONGTINCONGTY> fr = null;

        public BOCaiDatThongTinCongTy(Data.Transit transit)
        {
            fr = new FrameworkRepository<CAIDATTHONGTINCONGTY>(transit.KaraokeEntities, transit.KaraokeEntities.CAIDATTHONGTINCONGTies);
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

        public void CapNhat(Data.CAIDATTHONGTINCONGTY item, Data.Transit transit)
        {
            fr.Update(item);
            fr.Commit();
        }

        public CAIDATTHONGTINCONGTY GetAll(Data.Transit transit)
        {
            CAIDATTHONGTINCONGTY item = fr.Query().FirstOrDefault();
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
                fr.AddObject(item);
                fr.Commit();
            }

            return item;
        }
    }
}