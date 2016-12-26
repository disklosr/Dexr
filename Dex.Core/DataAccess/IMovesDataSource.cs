using Dex.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dex.Core.DataAccess
{
    public interface IMovesDataSource
    {
        Task<PokemonMoves> LoadAllMovesAsync();
    }
}