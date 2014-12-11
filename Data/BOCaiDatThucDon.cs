using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOCaiDatThucDon
    {
        KaraokeEntities mKaraokeEntities = null;
        public BOCaiDatThucDon(Data.Transit transit)
        {
            mKaraokeEntities = new KaraokeEntities();
        }

        public static CAIDATTHUCDON GetQueryNoTracking(Transit transit)
        {
            CAIDATTHUCDON item = FrameworkRepository<CAIDATTHUCDON>.QueryNoTracking(transit.KaraokeEntities.CAIDATTHUCDONs).FirstOrDefault();
            if (item == null)
            {
                item = new CAIDATTHUCDON();
                item.NhomTextFontSize = 12;
                item.NhomTextFontStyle = (int)SomeEnum.FontStyles.Normal;
                item.NhomTextFontWeights = (int)SomeEnum.FontWeights.Normal;
                item.MonTextFontSize = 12;
                item.MonTextFontStyle = (int)SomeEnum.FontStyles.Normal;
                item.MonTextFontWeights = (int)SomeEnum.FontWeights.Normal;
                item.LoaiNhomTextFontSize = 12;
                item.LoaiNhomTextFontStyle = (int)SomeEnum.FontStyles.Normal;
                item.LoaiNhomTextFontWeights = (int)SomeEnum.FontWeights.Normal;
            }
            return item;
        }

        public void Luu()
        {
            mKaraokeEntities.SaveChanges();
        }

        public CAIDATTHUCDON GetAll(Data.Transit transit)
        {
            CAIDATTHUCDON item = mKaraokeEntities.CAIDATTHUCDONs.FirstOrDefault();
            if (item == null)
            {
                item = new CAIDATTHUCDON();
                item.NhomTextFontSize = 12;
                item.NhomTextFontStyle = (int)SomeEnum.FontStyles.Normal;
                item.NhomTextFontWeights = (int)SomeEnum.FontWeights.Normal;
                item.MonTextFontSize = 12;
                item.MonTextFontStyle = (int)SomeEnum.FontStyles.Normal;
                item.MonTextFontWeights = (int)SomeEnum.FontWeights.Normal;
                item.LoaiNhomTextFontSize = 12;
                item.LoaiNhomTextFontStyle = (int)SomeEnum.FontStyles.Normal;
                item.LoaiNhomTextFontWeights = (int)SomeEnum.FontWeights.Normal;
                mKaraokeEntities.CAIDATTHUCDONs.AddObject(item);
                mKaraokeEntities.SaveChanges();
            }
            return item;
        }
    }
}
