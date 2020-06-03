using ImageMagick;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Helpers
{
    public static class ImageFileHelper
    {
        public static Image ByteToImage(byte[] imageByte)
        {
            MemoryStream ms = new MemoryStream(imageByte);
            //
            ImageOptimizer optimizer = new ImageOptimizer();
            ms.Position = 0; // The position needs to be reset.
            optimizer.LosslessCompress(ms);

            Image image = Image.FromStream(ms);

            return image;
        }
        public static byte[] CompressImageFromByte(byte[] imageByte)
        {
            MemoryStream ms = new MemoryStream(imageByte);
            //
            ImageOptimizer optimizer = new ImageOptimizer();
            ms.Position = 0; // The position needs to be reset.
            optimizer.LosslessCompress(ms);

            byte[] image = ms.ToArray();

            return image;
        }
        public static byte[] CompressAndResizeImageFromMemoryStream(byte[] imageByte, int cropWidth)
        {
            int _cropWidth = 100;
            if (cropWidth > _cropWidth)
                _cropWidth = cropWidth;
            //
            byte[] image = new byte[] { };

            using (MemoryStream stream = new MemoryStream(imageByte))
            {

                Image imageFile = Image.FromStream(stream);
                int _cropHeight = imageFile.Height;

                if (_cropWidth < imageFile.Width)
                {
                    _cropHeight = (_cropWidth * cropWidth) / _cropWidth;
                }
                else
                {
                    _cropWidth = imageFile.Width;
                }

                Image resizedImage = ResizeImage(imageFile, new Size(cropWidth, _cropHeight));

                using (MemoryStream mStream = new MemoryStream())
                {

                    ImageOptimizer optimizer = new ImageOptimizer();

                    mStream.Position = 0; // The position needs to be reset.
                    optimizer.LosslessCompress(mStream);

                    resizedImage.Save(mStream, imageFile.RawFormat);

                    image = mStream.ToArray();
                }

            }

            return image;
        }

        private static Image ResizeImage(Image image, Size size,
            bool preserveAspectRatio = true)
        {
            int newWidth;
            int newHeight;
            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = (float)size.Width / (float)originalWidth;
                float percentHeight = (float)size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }
            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }
    }
}
