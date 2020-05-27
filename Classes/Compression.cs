using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRecognition.Classes
{
    public class Compression
    {
        public static void Compress(FileInfo fi, string extension)
        {
            using (FileStream stream = fi.OpenRead())
            {
                string fullName = (fi.FullName);
                if ((((File.GetAttributes(fullName) & FileAttributes.Hidden) != FileAttributes.Hidden) & (fi.Extension.ToLower() != extension)))
                {
                    using (FileStream stream2 = File.Create(fullName.Replace(fi.Extension, extension)))
                    {
                        using (DeflateStream stream3 = new DeflateStream(stream2, CompressionMode.Compress))
                        {
                            byte[] array = new byte[0x1001];
                            int i = stream.Read(array, 0, array.Length);
                            while ((i != 0))
                            {
                                stream3.Write(array, 0, i);
                                i = stream.Read(array, 0, array.Length);
                            }
                        }
                    }
                }
            }
        }
        public static void Decompress(FileInfo fi)
        {
            using (FileStream stream = fi.OpenRead())
            {
                string fullName = (fi.FullName);
                object obj2 = (fullName.Remove((fullName.Length - fi.Extension.Length)) + ".xml");
                using (FileStream stream2 = File.Create((obj2).ToString()))
                {
                    using (DeflateStream stream3 = new DeflateStream(stream, CompressionMode.Decompress))
                    {
                        byte[] array = new byte[0x1001];
                        int i = stream3.Read(array, 0, array.Length);
                        while ((i != 0))
                        {
                            stream2.Write(array, 0, i);
                            i = stream3.Read(array, 0, array.Length);
                        }
                    }
                }
            }
        }
    }
}
