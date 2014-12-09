using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOThongTinCongTy
    {
        private string mHinh64;
        private byte[] mHinh;
        public string TenCongTy { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public byte[] Hinh 
        {
            set 
            { 
                mHinh = value;
                if (mHinh!=null)
                {
                    mHinh64 = Convert.ToBase64String(mHinh);
                }
            }
        }
        public string Hinh64 
        {
            get { return mHinh64; }
        }
        public BOThongTinCongTy() { }
        public static BOThongTinCongTy GetQueryNoTracking(Transit transit)
        {
            BOThongTinCongTy item = (from x in FrameworkRepository<CAIDATTHONGTINCONGTY>.QueryNoTracking(transit.KaraokeEntities.CAIDATTHONGTINCONGTies)
                                    select new BOThongTinCongTy
                                    {
                                        TenCongTy=x.TenCongTy,
                                        DiaChi=x.DiaChi,
                                        DienThoai=x.DienThoaiBan,
                                        Hinh = x.Logo
                                    }).FirstOrDefault();
            if (item == null)
            {
                item = new BOThongTinCongTy();
                item.TenCongTy = "";
                item.DienThoai = "";
                item.DiaChi = "";
            }
            return item;
        }
    }
}
