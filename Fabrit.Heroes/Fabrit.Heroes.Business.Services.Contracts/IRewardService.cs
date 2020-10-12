using Fabrit.Heroes.Data.Entities.Badge;
using Fabrit.Heroes.Data.Entities.Hero;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fabrit.Heroes.Business.Services.Contracts
{
    public interface IRewardService
    {
        Task<Badge> FindBadgeByType(BadgeType badgeType);
        Task AddBadge(BadgeType badgeType, Hero hero);
        Task AddBadges(int nrOfBadges, BadgeType badgeType, Hero hero);
        Task DeleteBadge(BadgeType badgeType, Hero hero);
        Task DeleteBadges(int nrOfBadges, BadgeType badgeType, Hero hero);
        bool HasBadgeByType(BadgeType badgeType, Hero hero);
        Task SwitchBadges(int nrOfBadges, BadgeType initialBadgeType, BadgeType badgeType, Hero hero);
        Task UpdateHeroBadges(Hero hero);
        ICollection<Hero> GetAllHeroesWithBadges();
        BadgeType GetMostValuableBadgeType(Hero hero);
    }
}
