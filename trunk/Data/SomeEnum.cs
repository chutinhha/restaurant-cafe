using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class SomeEnum
    {
        public string Name { get; set; }

        public int Value { get; set; }

        #region FontWeights

        public enum FontWeights
        {
            Black = 1,
            Bold,
            DemiBold,
            ExtraBlack,
            ExtraBold,
            ExtraLight,
            Heavy,
            Light,
            Medium,
            Normal,
            Regular,
            SemiBold,
            Thin,
            UltraBlack,
            UltraBold,
            UltraLight
        }

        public static List<SomeEnum> GetFontWeights()
        {
            List<SomeEnum> lsArray = new List<SomeEnum>();
            var ls = Enum.GetValues(typeof(FontWeights)).Cast<FontWeights>().ToList();
            foreach (var item in ls)
            {
                lsArray.Add(new SomeEnum() { Value = (int)item, Name = item.ToString() });
            }
            return lsArray;
        }

        public static System.Windows.FontWeight GetFontWeight(int n)
        {
            System.Windows.FontWeight f = System.Windows.FontWeights.Normal;
            switch (n)
            {
                case (int)FontWeights.Black:
                    f = System.Windows.FontWeights.Black;
                    break;
                case (int)FontWeights.Bold:
                    f = System.Windows.FontWeights.Bold;
                    break;
                case (int)FontWeights.DemiBold:
                    f = System.Windows.FontWeights.DemiBold;
                    break;
                case (int)FontWeights.ExtraBlack:
                    f = System.Windows.FontWeights.ExtraBlack;
                    break;
                case (int)FontWeights.ExtraBold:
                    f = System.Windows.FontWeights.ExtraBold;
                    break;
                case (int)FontWeights.ExtraLight:
                    f = System.Windows.FontWeights.ExtraLight;
                    break;
                case (int)FontWeights.Heavy:
                    f = System.Windows.FontWeights.Heavy;
                    break;
                case (int)FontWeights.Light:
                    f = System.Windows.FontWeights.Light;
                    break;
                case (int)FontWeights.Medium:
                    f = System.Windows.FontWeights.Medium;
                    break;
                case (int)FontWeights.Normal:
                    f = System.Windows.FontWeights.Normal;
                    break;
                case (int)FontWeights.Regular:
                    f = System.Windows.FontWeights.Regular;
                    break;
                case (int)FontWeights.SemiBold:
                    f = System.Windows.FontWeights.SemiBold;
                    break;
                case (int)FontWeights.Thin:
                    f = System.Windows.FontWeights.Thin;
                    break;
                case (int)FontWeights.UltraBlack:
                    f = System.Windows.FontWeights.UltraBlack;
                    break;
                case (int)FontWeights.UltraBold:
                    f = System.Windows.FontWeights.UltraBold;
                    break;
                case (int)FontWeights.UltraLight:
                    f = System.Windows.FontWeights.UltraLight;
                    break;
                default:
                    break;
            }
            return f;
        }

        #endregion FontWeights

        #region FontStyle

        public enum FontStyles
        {
            Italic = 1,
            Normal = 2,
            Oblique = 3
        }

        public static List<SomeEnum> GetFontStyles()
        {
            List<SomeEnum> lsArray = new List<SomeEnum>();
            var ls = Enum.GetValues(typeof(FontStyles)).Cast<FontStyles>().ToList();
            foreach (var item in ls)
            {
                lsArray.Add(new SomeEnum() { Value = (int)item, Name = item.ToString() });
            }
            return lsArray;
        }

        public static System.Windows.FontStyle GetFontStyle(int n)
        {
            System.Windows.FontStyle f = System.Windows.FontStyles.Normal;
            switch (n)
            {
                case (int)FontStyles.Italic:
                    f = System.Windows.FontStyles.Italic;
                    break;

                case (int)FontStyles.Normal:
                    f = System.Windows.FontStyles.Normal;
                    break;

                case (int)FontStyles.Oblique:
                    f = System.Windows.FontStyles.Oblique;
                    break;

                default:
                    break;
            }

            return f;
        }

        #endregion FontStyle

        #region FontStylePrinter
        public static List<SomeEnum> GetFontStylesPrinter()
        {
            List<SomeEnum> lsArray = new List<SomeEnum>();
            var ls = Enum.GetValues(typeof(System.Drawing.FontStyle)).Cast<System.Drawing.FontStyle>().ToList();
            foreach (var item in ls)
            {
                lsArray.Add(new SomeEnum() { Value = (int)item, Name = item.ToString() });
            }
            return lsArray;
        }
        public static System.Drawing.FontStyle GetFontStylesPrinter(int n)
        {
            System.Drawing.FontStyle f = System.Drawing.FontStyle.Regular;
            switch (f)
            {
                case System.Drawing.FontStyle.Bold:
                    f = System.Drawing.FontStyle.Bold;
                    break;
                case System.Drawing.FontStyle.Italic:
                    f = System.Drawing.FontStyle.Italic;
                    break;
                case System.Drawing.FontStyle.Regular:
                    f = System.Drawing.FontStyle.Regular;
                    break;
                case System.Drawing.FontStyle.Strikeout:
                    f = System.Drawing.FontStyle.Strikeout;
                    break;
                case System.Drawing.FontStyle.Underline:
                    f = System.Drawing.FontStyle.Underline;
                    break;
                default:
                    break;
            }
            return f;
        }
        #endregion
    }
}
