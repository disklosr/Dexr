using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace Dex.Uwp.Services
{
    public interface IPokePicturesManager
    {
        Task InitPokemonPictures();
    }

    public class PokePicturesManager : IPokePicturesManager
    {
        public async Task InitPokemonPictures()
        {
            //find Zip file in appx folder
            var folder = ApplicationData.Current.LocalFolder;
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(""));
            var fileBuffer = await FileIO.ReadBufferAsync(file);

            //Clean local storage folder
            //Unzip file in local storage folder
            //Done

            return;
        }
    }
}