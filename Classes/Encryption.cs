using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;

namespace SchoolRecognition.Classes
{
    public class Encryption
    {


        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        private static byte[] HashKey(string key, int length)
        {
            var sha = new SHA1CryptoServiceProvider();
            byte[] keyByte = Encoding.UTF8.GetBytes(key);
            byte[] hash = sha.ComputeHash(keyByte);
            byte[] truncateHash = new byte[length];
            Array.Copy(hash, 0, truncateHash, 0, length);
            return truncateHash;
        }

        public static byte[] EncryptPassword(string password)
        {
            byte[] bytes = new UnicodeEncoding().GetBytes(password);
            SHA1CryptoServiceProvider provider = new SHA1CryptoServiceProvider();
            return provider.ComputeHash(bytes);
        }

        public static string GenerateUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte item in data)
            {
                result.Append(chars[item % (chars.Length)]);
            }
            return result.ToString();
        }

        public static string SentenceCase(string str)
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        private static void CompressImage(Image sourceImage, int imageQuality, string savePath)
        {
            try
            {
                //Create an ImageCodecInfo-object for the codec information
                ImageCodecInfo jpegCodec = null;

                //Set quality factor for compression
                EncoderParameter imageQualitysParameter = new EncoderParameter(
                            System.Drawing.Imaging.Encoder.Quality, imageQuality);

                //List all avaible codecs (system wide)
                ImageCodecInfo[] alleCodecs = ImageCodecInfo.GetImageEncoders();

                EncoderParameters codecParameter = new EncoderParameters(1);
                codecParameter.Param[0] = imageQualitysParameter;

                //Find and choose JPEG codec
                for (int i = 0; i < alleCodecs.Length; i++)
                {
                    if (alleCodecs[i].MimeType == "image/jpeg" || alleCodecs[i].MimeType == "image/jpg" || alleCodecs[i].MimeType == "image/png")
                    {
                        jpegCodec = alleCodecs[i];
                        break;
                    }
                }

                //Save compressed image
                sourceImage.Save(savePath, jpegCodec, codecParameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
