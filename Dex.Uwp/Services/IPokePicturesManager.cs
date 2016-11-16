using System;
using System.Threading.Tasks;

namespace Dex.Uwp.Services
{
    public interface IPokePicturesManager
    {
        Task<bool> DeleteLocalPictures();

        Task<bool> DownloadAllPictures();
    }

    public class PokePicturesManager : IPokePicturesManager
    {
        public Task<bool> DeleteLocalPictures()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DownloadAllPictures()
        {
            throw new NotImplementedException();
        }

        private async Task downloadSinglePicture(int index)
        {
            throw new NotImplementedException();
        }
    }
}