using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
namespace Utilities
{
    public class FontConverter
    {
        public static FontStyle ConvertToFont(int fontStyle)
        {            
            switch (fontStyle)
            {
                case 1:
                    return FontStyles.Oblique;                    
                case 2:
                    return FontStyles.Italic;
                default:
                    return FontStyles.Normal;                    
            }
        }
        public static int ConvertToInt(FontStyle font)
        {
            if (font.Equals(FontStyles.Oblique))
            {
                return 1;
            }
            if (font.Equals(FontStyles.Italic))
            {
                return 2;
            }
            return 0;
        }
    }
}
