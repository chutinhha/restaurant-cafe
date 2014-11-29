using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOCaiDatThucDon
    {
        private FrameworkRepository<CAIDATTHUCDON> fr = null;

        public BOCaiDatThucDon(Data.Transit transit)
        {
            fr = new FrameworkRepository<CAIDATTHUCDON>(transit.KaraokeEntities, transit.KaraokeEntities.CAIDATTHUCDONs);
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

        public void CapNhat(Data.CAIDATTHUCDON item, Data.Transit transit)
        {
            fr.Update(item);
            fr.Commit();
        }

        public CAIDATTHUCDON GetAll(Data.Transit transit)
        {
            CAIDATTHUCDON item = fr.Query().FirstOrDefault();
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
                fr.AddObject(item);
                fr.Commit();
            }

            return item;
        }
    }
}
