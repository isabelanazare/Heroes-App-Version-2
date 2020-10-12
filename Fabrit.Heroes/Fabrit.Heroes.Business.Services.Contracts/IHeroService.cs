using Fabrit.Heroes.Data.Business.Hero;
using Fabrit.Heroes.Data.Entities.Hero;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fabrit.Heroes.Business.Services.Contracts
{
    public interface IHeroService
    {
        IAsyncEnumerable<HeroDto> GetAll();
        IAsyncEnumerable<HeroDto> GetAllPlayers();
        IAsyncEnumerable<HeroDto> GetAllHeroesWithPowers();
        IAsyncEnumerable<RowHeroData> GetHeroRowData();
        IAsyncEnumerable<RowHeroData> GetAdminMapRowData();
        Task DeleteHeroById(int id);
        Task<Hero> FindById(int id);
        Task CreateHero(HeroDto heroDto);
        Task UpdateHero(HeroDto heroDto);
        Task<HeroDto> FindHeroDtoById(int id);
        Task ChangeHeroAvatar(int heroId, string avatarPath);
        Task UpdateHeroBirthday(int heroId, string date);
        Task ChangeHeroLocation(int id, double latitude, double longitude);
        Task<ICollection<Hero>> GetHeroesInRange(int heroId);
        Task<EntityTypeDto> GetEntityTypeDto(int heroId);
        Task<HeroDto> GetHeroByUserId(int id);
        Task ChangeEntityDetails(HeroDto entity);
        void ChangeOverallStrength(int id);
        ICollection<HeroDto> ModifyStrengthOfHeroParty(ICollection<HeroDto> heroes);
        Task<HeroDto> ChangeHeroTravel(int id, double latitude, double longitude);
    }
}