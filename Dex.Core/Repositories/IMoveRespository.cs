using Dex.Core.DataAccess;
using Dex.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Type = Dex.Core.Entities.Type;

namespace Dex.Core.Repositories
{
    public interface IMoveRepository
    {
        Task<IEnumerable<Move>> GetAllMoves();

        Task<IEnumerable<Move>> GetAllMovesByType(Type moveType);
    }

    public class MoveRepository : IMoveRepository
    {
        private readonly IMovesDataSource dataSource;

        private IEnumerable<Move> movesCache;

        public MoveRepository(IMovesDataSource dataSource)
        {
            this.dataSource = dataSource;
        }

        public async Task<IEnumerable<Move>> GetAllMoves()
        {
            await EnsureCacheIsValid();

            return movesCache;
        }

        public async Task<IEnumerable<Move>> GetAllMovesByType(Type moveType)
        {
            await EnsureCacheIsValid();
            return movesCache.Where(move => move.Type == moveType).ToList();
        }

        private async Task EnsureCacheIsValid()
        {
            if (movesCache == null)
                movesCache = await dataSource.LoadAllMovesAsync();
        }
    }
}