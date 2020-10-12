using Fabrit.Heroes.Data.Business.Hero;
using Fabrit.Heroes.Data.Entities.Hero;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fabrit.Heroes.Business.Services.Contracts
{
    public interface IHeroTypeService
    {
        Task<HeroType> FindById(int id);
        IAsyncEnumerable<HeroTypeDto> GetAll();
    }
}
