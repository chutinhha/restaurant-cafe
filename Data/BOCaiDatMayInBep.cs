using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOCaiDatMayInBep
    {
        private FrameworkRepository<CAIDATMAYINBEP> fr = null;

        public BOCaiDatMayInBep(Data.Transit transit)
        {
            fr = new FrameworkRepository<CAIDATMAYINBEP>(transit.KaraokeEntities, transit.KaraokeEntities.CAIDATMAYINBEPs);
        }

        public static CAIDATMAYINBEP GetQueryNoTracking(Transit transit)
        {
            CAIDATMAYINBEP item = FrameworkRepository<CAIDATMAYINBEP>.QueryNoTracking(transit.KaraokeEntities.CAIDATMAYINBEPs).FirstOrDefault();
            if (item == null)
            {
                item = new CAIDATMAYINBEP();
                item.TitleTextFontSize = 12;
                item.TitleTextFontStyle = (int)System.Drawing.FontStyle.Regular;
                item.TitleTextFontWeights = (int)SomeEnum.FontWeights.Normal;
                item.InfoTextFontSize = 12;
                item.InfoTextFontStyle = (int)System.Drawing.FontStyle.Regular;
                item.InfoTextFontWeights = (int)SomeEnum.FontWeights.Normal;
                item.ItemTextFontSize = 12;
                item.ItemTextFontStyle = (int)System.Drawing.FontStyle.Regular;
                item.ItemTextFontWeights = (int)SomeEnum.FontWeights.Normal;
                item.SumTextFontSize = 12;
                item.SumTextFontStyle = (int)System.Drawing.FontStyle.Regular;
                item.SumTextFontWeights = (int)SomeEnum.FontWeights.Normal;
            }
            return item;
        }

        public void CapNhat(Data.CAIDATMAYINBEP item, Data.Transit transit)
        {
            fr.Update(item);
            fr.Commit();
        }

        public CAIDATMAYINBEP GetAll(Data.Transit transit)
        {
            CAIDATMAYINBEP item = fr.Query().FirstOrDefault();
            if (item == null)
            {
                item = new CAIDATMAYINBEP();
                item.TitleTextFontSize = 12;
                item.TitleTextFontStyle = (int)SomeEnum.FontStyles.Normal;
                item.TitleTextFontWeights = (int)SomeEnum.FontWeights.Normal;
                item.InfoTextFontSize = 12;
                item.InfoTextFontStyle = (int)SomeEnum.FontStyles.Normal;
                item.InfoTextFontWeights = (int)SomeEnum.FontWeights.Normal;
                item.ItemTextFontSize = 12;
                item.ItemTextFontStyle = (int)SomeEnum.FontStyles.Normal;
                item.ItemTextFontWeights = (int)SomeEnum.FontWeights.Normal;
                item.SumTextFontSize = 12;
                item.SumTextFontStyle = (int)SomeEnum.FontStyles.Normal;
                item.SumTextFontWeights = (int)SomeEnum.FontWeights.Normal;
                fr.AddObject(item);
                fr.Commit();
            }

            return item;
        }
    }
}
