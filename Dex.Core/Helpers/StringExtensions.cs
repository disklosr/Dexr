using Windows.Security.Cryptography;
using Windows.Storage.Streams;

namespace Dex.Core.Helpers
{
    public static class StringExtensions
    {
        public static IBuffer ConvertToBuffer(this string stringToConvert, BinaryStringEncoding encoding = BinaryStringEncoding.Utf8)
        {
            return CryptographicBuffer.ConvertStringToBinary(stringToConvert, encoding);
        }
    }
}