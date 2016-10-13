using System;
using System.Linq;
using System.Collections.Generic;
using Dex.Core.Entities;
using System.Threading.Tasks;

namespace Dex.Core.DataAccess
{

    public interface IMovesDataSource
    {
        Task<IEnumerable<Move>> LoadAllMovesAsync();
    }

}