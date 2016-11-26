using Windows.Security.Cryptography;
using Windows.Storage.Streams;

namespace Dex.Uwp.Helpers
{
    public static class BufferExtensions
    {
        public static string ConvertToString(this IBuffer bufferToConvert, BinaryStringEncoding encoding = BinaryStringEncoding.Utf8)
        {
            return CryptographicBuffer.ConvertBinaryToString(encoding, bufferToConvert);
        }
    }
}
