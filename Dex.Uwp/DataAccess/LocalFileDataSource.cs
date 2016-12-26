using Dex.Core.DataAccess;
using Dex.Core.Entities;
using Dex.Uwp.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;

namespace Dex.Uwp.DataAccess
{
    public class LocalFileDataSource : IPokemonsDataSource, IMovesDataSource, IEvolutionsDataSource
    {
        private const string contentPrefix = "ms-appx:///";
        private const string evolutionsDbFilePath = "Data/evolutions.db.json";
        private const string movesDbFilePath = "Data/moves.db.json";
        private const string pokemonsDbFilePath = "Data/Pokemons.db.json";
        private readonly IJsonService jsonService;

        public LocalFileDataSource(IJsonService jsonService)
        {
            this.jsonService = jsonService;
        }

        public async Task<List<ushort[]>> LoadAllEvolutionsAsync()
        {
            var json = await ReadFileAsTextAsync(evolutionsDbFilePath);
            return jsonService.Deserialize<List<ushort[]>>(json);
        }

        public async Task<PokemonMoves> LoadAllMovesAsync()
        {
            var json = await ReadFileAsTextAsync(movesDbFilePath);
            return jsonService.Deserialize<PokemonMoves>(json);
        }

        public async Task<IEnumerable<Pokemon>> LoadAllPokemonsAsync()
        {
            var json = await ReadFileAsTextAsync(pokemonsDbFilePath);
            return jsonService.Deserialize<List<Pokemon>>(json);
        }

        public async Task<string> ReadFileAsTextAsync(string rootUri)
        {
            var uri = new Uri(contentPrefix + rootUri);
            var file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            return await FileIO.ReadTextAsync(file);
        }
    }
}