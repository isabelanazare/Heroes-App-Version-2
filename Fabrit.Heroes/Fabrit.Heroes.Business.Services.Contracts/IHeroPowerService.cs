using Fabrit.Heroes.Data.Business.Hero;
using Fabrit.Heroes.Data.Business.Power;
using Fabrit.Heroes.Data.Business.Villain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fabrit.Heroes.Business.Services.Contracts
{
    public interface IHeroPowerService
    {
        public IAsyncEnumerable<HeroPowerDto> GetHeroPowersForHero(int heroId);
        public Task TrainPower(int id);
        public Task ChangeMainPower(MainPowerChangeDto dto);
        public Task DeletePowerById(int id);
        public Task AddVillainPowers(VillainPowersDto dto);
        public Task<HeroPowerDto> GetHeroPowerById(int id);
        public Task UpdateHeroPower(HeroPowerDto dto);
    }
}
