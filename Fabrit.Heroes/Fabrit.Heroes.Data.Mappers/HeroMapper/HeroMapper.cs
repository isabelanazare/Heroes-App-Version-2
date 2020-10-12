using Fabrit.Heroes.Data.Business.Hero;
using Fabrit.Heroes.Data.Business.Power;
using Fabrit.Heroes.Data.Entities.Hero;
using Fabrit.Heroes.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fabrit.Heroes.Data.Mappers
{
    public class HeroMapper : IHeroMapper
    {
        public HeroDto ConvertHeroToHeroDto(Hero hero)
        {
            return new HeroDto
            {
                Id = hero.Id,
                Name = hero.Name,
                Powers = hero.Powers
                        .Where(hp => hp.Power != null)
                        .OrderByDescending(hp => hp.Power.Strength)
                        .Select(hp => new PowerDto
                        {
                            Id = hp.Power.Id,
                            Details = hp.Power.Details,
                            Element = hp.Power.Element.ToString(),
                            Name = hp.Power.Name,
                            Strength = hp.Power.Strength
                        }),
                Type = hero.Type.Name,

            };
        }

        public RowHeroData ConvertHeroToRowHero(Hero hero)
        {
            return new RowHeroData
            {
                Id = hero.Id,
                Name = hero.Name,
                AllOrderedPowers = hero.Powers
                        .OrderByDescending(hp => hp.Power.Strength)
                        .Select(hp => hp.Power.Name),
                Ally = hero.Allies.Where(a => a.AllyTo != null).Select(ha => ha.AllyTo.Name),
                AvatarPath = Constants.APP_URL + hero.AvatarPath,
                Birthday = hero.Birthday.Date.ToString(Constants.DATE_FORMAT),
                OverallStrength = hero.OverallStrength
            };
        }
    }
}
