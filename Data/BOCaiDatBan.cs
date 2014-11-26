using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOCaiDatBan
    {        
        public static CAIDATBAN GetCaiDatBan(Transit transit)
        {
            CAIDATBAN caidat = FrameworkRepository<CAIDATBAN>.QueryNoTracking(transit.KaraokeEntities.CAIDATBANs).FirstOrDefault();
            if (caidat==null)
            {
                caidat = new CAIDATBAN();
                caidat.TableWidth=(decimal)0.0735294000;
                caidat.TableHeight=(decimal)0.0938086000;
                caidat.TableFontSize = 12;
                caidat.TableFontStyle = 0;
            }
            return caidat;
        }
    }
}
