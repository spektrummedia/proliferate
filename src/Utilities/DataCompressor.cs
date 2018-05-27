using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Proliferate.Utilities
{
    public static class DataCompressor
    {
        public static string Compress(string uncompressedValue)
        {
            var inputBytes = Encoding.UTF8.GetBytes(uncompressedValue);
            using (var outputStream = new MemoryStream())
            {
                using (var gZipStream = new GZipStream(outputStream, CompressionMode.Compress))
                {
                    gZipStream.Write(inputBytes, 0, inputBytes.Length);
                }

                var outputBytes = outputStream.ToArray();

                var outputbase64 = Convert.ToBase64String(outputBytes);
                return outputbase64;
            }
        }

        public static string Decompress(string compressedValue)
        {
            var inputBytes = Convert.FromBase64String(compressedValue);
            using (var inputStream = new MemoryStream(inputBytes))
            using (var gZipStream = new GZipStream(inputStream, CompressionMode.Decompress))
            using (var streamReader = new StreamReader(gZipStream))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}