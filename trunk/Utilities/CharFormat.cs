using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    public class CharFormat
    {
        public static int ConvertToBarcodeChar(System.Windows.Input.Key key)
        {
            int keyInt=(int)key;
            if (keyInt>=34 && keyInt<=43)
            {
                return '0' + (keyInt - 34);
            }
            else if (keyInt>=44 && keyInt<=69)
            {
                return 'A' + (keyInt - 44);
            }
            return 0;
        }
    }
}
