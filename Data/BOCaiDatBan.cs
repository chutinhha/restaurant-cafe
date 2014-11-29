using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOCaiDatBan
    {
        private FrameworkRepository<CAIDATBAN> fr = null;

        public BOCaiDatBan(Data.Transit transit)
        {
            fr = new FrameworkRepository<CAIDATBAN>(transit.KaraokeEntities, transit.KaraokeEntities.CAIDATBANs);
        }

        public static CAIDATBAN GetQueryNoTracking(Transit transit)
        {
            CAIDATBAN item = FrameworkRepository<CAIDATBAN>.QueryNoTracking(transit.KaraokeEntities.CAIDATBANs).FirstOrDefault();
            if (item == null)
            {
                item = new CAIDATBAN();
                item.TableWidth = (decimal)0.0735294000;
                item.TableHeight = (decimal)0.0938086000;
                item.TableFontSize = 12;
                item.TableFontStyle = (int)SomeEnum.FontStyles.Normal;
                item.TableFontWeights = (int)SomeEnum.FontWeights.Normal;
            }
            return item;
        }

        public void CapNhat(Data.CAIDATBAN item, Data.Transit transit)
        {
            fr.Update(item);
            fr.Commit();
        }

        public CAIDATBAN GetAll(Data.Transit transit)
        {
            CAIDATBAN item = fr.Query().FirstOrDefault();
            if (item == null)
            {
                item = new CAIDATBAN();
                item.TableWidth = (decimal)0.0735294000;
                item.TableHeight = (decimal)0.0938086000;
                item.TableFontStyle = (int)SomeEnum.FontStyles.Normal;
                item.TableFontWeights = (int)SomeEnum.FontWeights.Normal;
                fr.AddObject(item);
                fr.Commit();
            }
            return item;
        }


    }
}
