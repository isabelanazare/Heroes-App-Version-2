using Fabrit.Heroes.Data.Business.Villain;
using Fabrit.Heroes.Data.Entities.Hero;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fabrit.Heroes.Business.Services.Contracts
{
    public interface IVillainService
    {
        IAsyncEnumerable<VillainDto> GetAll();
        IAsyncEnumerable<RowVillainData> GetVillainRowData();
        Task<Hero> GetVillainById(int id);
        Task<VillainDto> GetVillainDtoById(int id);
        Task ChangeImage(int villainId, string avatarPath);
        Task<VillainDto> AddVillain(VillainDto villainDto);
        Task UpdateVillain(VillainDto villainDto);
        Task DeleteVillain(int id);
        Task ChangeVillainLocation(int id, double latitude, double longitude);
        ICollection<VillainDto> ModifyStrengthOfHeroParty(ICollection<VillainDto> heroes);
    }
}
