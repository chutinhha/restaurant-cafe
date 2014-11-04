using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.IO;

namespace Utilities
{
    public static class ImageHandler
    {
        public static BitmapFrame CreateResizedImage(ImageSource source, int width, int height, int margin)
        {            
            var rect = new Rect(margin, margin, width - margin * 2, height - margin * 2);
            var group = new DrawingGroup();
            RenderOptions.SetBitmapScalingMode(group, BitmapScalingMode.HighQuality);
            group.Children.Add(new ImageDrawing(source, rect));
            var drawingVisual = new DrawingVisual();
            using (var drawingContext = drawingVisual.RenderOpen()) drawingContext.DrawDrawing(group);
            var resizedImage = new RenderTargetBitmap(
                width, height,         // Resized dimensions
                96, 96,                // Default DPI values
                PixelFormats.Default); // Default pixel format
            resizedImage.Render(drawingVisual);
            return BitmapFrame.Create(resizedImage);
        }
        public static byte[] ImageToByte(BitmapImage img)
        {
            byte[] imageData = new byte[img.StreamSource.Length];
            img.StreamSource.Seek(0, System.IO.SeekOrigin.Begin);
            img.StreamSource.Read(imageData, 0, imageData.Length);
            return imageData;
        }
        //public static Byte[] ImageToByte(BitmapImage imageSource)
        //{
        //    Stream stream = imageSource.StreamSource;
        //    Byte[] buffer = null;
        //    if (stream != null && stream.Length > 0)
        //    {
        //        using (BinaryReader br = new BinaryReader(stream))
        //        {
        //            buffer = br.ReadBytes((Int32)stream.Length);
        //        }
        //    }
        //    return buffer;
        //}


        public static BitmapImage BitmapImageFromByteArray(Byte[] bytes)
        {
            if (bytes!=null && bytes.Length>0)
            {
                MemoryStream stream = new MemoryStream(bytes);
                stream.Seek(0, SeekOrigin.Begin);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.EndInit();
                return image;
            }
            return null;
        }

        public static BitmapImage BitmapImageCopy(BitmapImage img)
        {
            BitmapImage imgNew = new BitmapImage();
            BitmapEncoder encode = new PngBitmapEncoder();
            encode.Frames.Add(BitmapFrame.Create(img));
            using (var stream=new MemoryStream())
            {
                encode.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);

                imgNew.BeginInit();
                imgNew.CacheOption = BitmapCacheOption.OnLoad;
                imgNew.StreamSource = stream;
                imgNew.EndInit();
            }
            return imgNew;
        }

    }
}
