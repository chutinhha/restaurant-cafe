﻿using System;
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
        public static BitmapFrame CreateBitMap(string url)
        {
            BitmapImage img = new BitmapImage(new Uri(url));
            return CreateResizedImage(img, 64, 64, 0);
        }
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
        public static byte[] ImageToByte(BitmapFrame bfResize)
        {
            using (MemoryStream msStream = new MemoryStream())
            {
                PngBitmapEncoder pbdDecoder = new PngBitmapEncoder();
                pbdDecoder.Frames.Add(bfResize);
                pbdDecoder.Save(msStream);
                return msStream.ToArray();
            }
        }
        //public static BitmapImage GetBitmap(string url)
        //{
        //    Uri uri = new Uri(url);
        //    BitmapImage source = new BitmapImage();
        //    source.BeginInit();
        //    source.UriSource = uri;
        //    source.DecodePixelHeight = 10;
        //    source.DecodePixelWidth = 10;
        //    source.EndInit();
        //    return source;
        //}
        public static BitmapImage BitmapImageFromByteArray(Byte[] bytes)
        {
            if (bytes != null && bytes.Length > 0)
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
        public static System.Drawing.Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            if (bitmapImage==null)
            {
                return null;
            }
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();                
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));                
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new System.Drawing.Bitmap(bitmap);
            }
        }
        public static System.Drawing.Bitmap BitmapImage2Bitmap(Byte[] bytes)
        {
            BitmapImage img = BitmapImageFromByteArray(bytes);
            return BitmapImage2Bitmap(img);
        }
        public static BitmapImage BitmapImageCopy(BitmapImage img)
        {
            BitmapImage imgNew = new BitmapImage();
            BitmapEncoder encode = new PngBitmapEncoder();
            encode.Frames.Add(BitmapFrame.Create(img));
            using (var stream = new MemoryStream())
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
        public static byte[] GetByteFromUrl(string url)
        {            
            System.IO.FileStream fs = new System.IO.FileStream(url, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] buff = new byte[fs.Length];
            fs.Read(buff, 0, buff.Length);
            return buff;
        }
        public static System.Drawing.Icon GetIcon(string url)
        {
            Stream iconStream = Application.GetResourceStream(new Uri(url,UriKind.Relative)).Stream;
            if (iconStream!=null)
            {
                return new System.Drawing.Icon(iconStream);
            }
            return null;
        }
    }
}
