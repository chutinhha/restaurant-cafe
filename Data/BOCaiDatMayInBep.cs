using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOCaiDatMayInBep
    {
        KaraokeEntities mKaraokeEntities = null;
        public BOCaiDatMayInBep(Data.Transit transit)
        {
            mKaraokeEntities = new KaraokeEntities();
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

        public void Luu()
        {
            mKaraokeEntities.SaveChanges();
        }

        public CAIDATMAYINBEP GetAll(Data.Transit transit)
        {
            CAIDATMAYINBEP item = mKaraokeEntities.CAIDATMAYINBEPs.FirstOrDefault();
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
                mKaraokeEntities.CAIDATMAYINBEPs.AddObject(item);
                mKaraokeEntities.SaveChanges();
            }

            return item;
        }
    }
}
