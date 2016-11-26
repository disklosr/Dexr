using Dex.Core.Services;
using NUnit.Framework;
using Dex.Core.Helpers;

namespace Dex.Core.Test.Services
{
    [TestFixture]
    public class EncryptionServiceTest
    {
        [Test]
        public void DecryptedMessageShouldBeEqualToOriginalMessage()
        {
            var messageToEncrypt = "Hello World!";
            var password = "ASamplePassword";

            IEncryptionService encryptionService = new SimpleSymmetricEncryptionService();


            var encryptedBuffer = encryptionService.Encrypt(messageToEncrypt.ConvertToBuffer(), password);

            var decryptedBuffer = encryptionService.Decrypt(encryptedBuffer, password);

            Assert.AreEqual(messageToEncrypt, decryptedBuffer.ConvertToString());
        }
    }
}
