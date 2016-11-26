using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace Dex.Core.Services
{
    public interface IEncryptionService
    {
        IBuffer Encrypt(IBuffer bytes, string password);
        IBuffer Decrypt(IBuffer bytes, string password);
    }

    public class SimpleSymmetricEncryptionService : IEncryptionService
    {
        private readonly IBuffer iv; //Required for CBC Algorithms
        private readonly string AlgorithmName;
        private readonly SymmetricKeyAlgorithmProvider EncryptionAlgorithmProvider;
        private readonly HashAlgorithmProvider HashAlgorithmProvider;

        public SimpleSymmetricEncryptionService()
        {
            //Requires a key of 128 bits. We drive the key from password using MD5
            AlgorithmName = SymmetricAlgorithmNames.AesCbcPkcs7;    
            iv = new Buffer(5);
            EncryptionAlgorithmProvider = SymmetricKeyAlgorithmProvider.OpenAlgorithm(AlgorithmName);
            HashAlgorithmProvider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
        }

        public IBuffer Encrypt(IBuffer bytes, string password)
        {
            var key = GetCryptographicKey(password);
            return CryptographicEngine.Encrypt(key, bytes, iv);
        }

        public IBuffer Decrypt(IBuffer bytes, string password)
        {
            var key = GetCryptographicKey(password);
            return CryptographicEngine.Decrypt(key, bytes, iv);
        }

        private CryptographicKey GetCryptographicKey(string password)
        {
            var passwordHash = ComputeMD5(password); 
            return EncryptionAlgorithmProvider.CreateSymmetricKey(passwordHash);
        }

        private IBuffer ComputeMD5(string input)
        {
            IBuffer buff = CryptographicBuffer.ConvertStringToBinary(input, BinaryStringEncoding.Utf8);
            return HashAlgorithmProvider.HashData(buff);
        }
    }
}
