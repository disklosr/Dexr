using Dex.Core.DataAccess;
using Dex.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dex.Uwp.DataAccess
{
    public class LocalFileDataSource : IPokemonsDataSource, IMovesDataSource
    {
        public Task<IEnumerable<Move>> LoadAllMovesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Pokemon>> LoadAllPokemonsAsync()
        {
            throw new NotImplementedException();
        }
    }
}