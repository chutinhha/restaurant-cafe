using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class KaraokeEntities : KaraokeEntitiesBase
    {
        public KaraokeEntities()
            : base()
        {
            this.ContextOptions.LazyLoadingEnabled = false;
        }
    }
}
