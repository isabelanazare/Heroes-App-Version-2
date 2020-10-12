using Fabrit.Heroes.Data.Business.Battle;
using Fabrit.Heroes.Data.Entities.Battle;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fabrit.Heroes.Business.Services.Contracts
{
    public interface IBattleService
    {
        IAsyncEnumerable<BattleDto> GetAll();
        Task<BattleDto> CreateBattle(int initiatorId, int opponentId);
        Task DeleteBattle(int id);
        Task<BattleResultDto> PlayGame(BattleDto battleDto);
        ICollection<BattleRecordDto> GetBattleHistory(int id);
        Task<int> GetNrOfBattlesForHero(int id);
        Task<BadgesCountDto> GetBadgesCountDto(int id);
    }
}
