using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    public class NumberFormat
    {        
        public static string FormatToString(decimal data)
        {
            return String.Format("{0}", data);
        }
        
    }
}
