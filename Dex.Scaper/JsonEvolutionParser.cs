using Dex.Core.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Dex.Scaper
{
    public class JsonEvolutionParser
    {
        private Dictionary<string, Pok> pokes;

        public void MergeWith(List<Pokemon> pokemons)
        {
            pokemons.ForEach(pokemon =>
            {
                var pok = pokes.Values.First(item => item.Id == pokemon.DexNumber);
                pokemon.EvolvesFrom = pok.evolveFrom == null ? (ushort)0 : pokes[pok.evolveFrom].Id;
                pokemon.EvolvesTo = pok.evolveTo == null ? (ushort)0 : pokes[pok.evolveTo].Id;
                pokemon.Moves = new PokemonMovesIds();
                pokemon.Moves.QuickMovesIds = pok.QuickMoves;
                pokemon.Moves.ChargeMovesIds = pok.ChargeMoves;
            });
        }

        public void Parse(string json)
        {
            pokes = JsonConvert.DeserializeObject<Dictionary<string, Pok>>(json);
        }
    }

    public class Pok
    {
        public List<string> ChargeMoves { get; set; }
        public string evolveFrom { get; set; }
        public string evolveTo { get; set; }
        public ushort Id { get; set; }
        public string Name { get; set; }

        public List<string> QuickMoves { get; set; }
    }
}