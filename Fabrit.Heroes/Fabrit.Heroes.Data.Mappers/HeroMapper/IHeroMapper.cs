using Fabrit.Heroes.Data.Business.Hero;
using Fabrit.Heroes.Data.Entities.Hero;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fabrit.Heroes.Data.Mappers
{
    public interface IHeroMapper
    {
        public RowHeroData ConvertHeroToRowHero(Hero hero);
        public HeroDto ConvertHeroToHeroDto(Hero hero);

    }
}
