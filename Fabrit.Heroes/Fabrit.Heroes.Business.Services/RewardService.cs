using Fabrit.Heroes.Business.Services.Contracts;
using Fabrit.Heroes.Data;
using Fabrit.Heroes.Data.Entities.Badge;
using Fabrit.Heroes.Data.Entities.Hero;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace Fabrit.Heroes.Business.Services
{
    public class RewardService : IRewardService
    {
        private readonly HeroesDbContext _context;

        public RewardService(HeroesDbContext context)
        {
            _context = context;
        }

        public ICollection<Hero> GetAllHeroesWithBadges()
        {
            return _context.Heroes
                .Include(h => h.Badges).ThenInclude(hp => hp.Badge).ToList();
        }

        public async Task<Badge> FindBadgeByType(BadgeType badgeType)
        {
            return await _context.Badges
               .Where(h => h.Tier.Equals(badgeType))
               .FirstOrDefaultAsync();
        }

        public async Task AddBadge(BadgeType badgeType, Hero hero)
        {
            if (hero.Badges == null)
            {
                hero.Badges = new List<HeroBadge>();
            }

            hero.Badges.Add(new HeroBadge
            {
                Badge = await FindBadgeByType(badgeType),
                Hero = hero
            });

            _context.Heroes.Update(hero);
            await _context.SaveChangesAsync();
        }

        public async Task AddBadges(int nrOfBadges, BadgeType badgeType, Hero hero)
        {
            var count = 0;
            if (hero.Badges == null)
            {
                hero.Badges = new List<HeroBadge>();
            }

            while (count < nrOfBadges)
            {
                hero.Badges.Add(new HeroBadge
                {
                    Badge = await FindBadgeByType(badgeType),
                    Hero = hero
                });

                count++;

                _context.Heroes.Update(hero);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteBadge(BadgeType badgeType, Hero hero)
        {
            if(hero.Badges.Count() > 0)
            {
                if (!HasBadgeByType(BadgeType.TierCategory5, hero))
                {
                    HeroBadge badgeToDelete = hero.Badges.Where(b => b.Badge.Tier == badgeType).FirstOrDefault();

                    _context.Remove(badgeToDelete);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteBadges(int nrOfBadges, BadgeType badgeType, Hero hero)
        {
            if (!HasBadgeByType(BadgeType.TierCategory5, hero))
            {
                IEnumerable<HeroBadge> badgesToDelete = hero.Badges.Where(b => b.Badge.Tier == badgeType).Take(nrOfBadges);

                _context.RemoveRange(badgesToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public bool HasBadgeByType(BadgeType badgeType, Hero hero)
        {
            return hero.Badges.FirstOrDefault(b => b.Badge.Tier == badgeType) != default;
        }

        public async Task SwitchBadges(int nrOfBadges, BadgeType initialBadgeType, BadgeType badgeType, Hero hero)
        {
            if (nrOfBadges >= 3)
            {
                var nrOfBadgesToAdd = nrOfBadges / 3;
                var nrOfBadgesToDelete = nrOfBadges - nrOfBadges % 3;

                await DeleteBadges(nrOfBadgesToDelete, initialBadgeType, hero);
                await AddBadges(nrOfBadgesToAdd, badgeType, hero);
                await _context.SaveChangesAsync();
            }
        }

        public BadgeType GetMostValuableBadgeType(Hero hero)
        {
            if (!HasBadgeByType(BadgeType.TierCategory5, hero))
            {
                var nrOfTier1Badges = 0;
                var nrOfTier2Badges = 0;
                var nrOfTier3Badges = 0;
                var nrOfTier4Badges = 0;

                if (hero.Badges != null)
                {
                    foreach (HeroBadge hp in hero.Badges)
                    {
                        if (hp.Badge.Tier == BadgeType.TierCategory1)
                        {
                            nrOfTier1Badges++;
                        }
                        else if (hp.Badge.Tier == BadgeType.TierCategory2)
                        {
                            nrOfTier2Badges++;
                        }
                        else if (hp.Badge.Tier == BadgeType.TierCategory3)
                        {
                            nrOfTier3Badges++;
                        }
                        else if (hp.Badge.Tier == BadgeType.TierCategory4)
                        {
                            nrOfTier4Badges++;
                        }
                    }
                }
                var max = Math.Max(Math.Max(nrOfTier3Badges, nrOfTier4Badges), Math.Max(nrOfTier1Badges, nrOfTier2Badges));

                if (max == nrOfTier4Badges)
                {
                    return BadgeType.TierCategory4;
                }
                else if (max == nrOfTier3Badges)
                {
                    return BadgeType.TierCategory3;
                }
                else if (max == nrOfTier2Badges)
                {
                    return BadgeType.TierCategory2;
                }
                else
                {
                    return BadgeType.TierCategory1;
                }
            }
            return BadgeType.TierCategory5;
        }

        public async Task UpdateHeroBadges(Hero hero)
        {  if (hero.Badges.Count() > 0)
            {
                if (!HasBadgeByType(BadgeType.TierCategory5, hero))
                {
                    var nrOfTier1Badges = 0;
                    var nrOfTier2Badges = 0;
                    var nrOfTier3Badges = 0;
                    var nrOfTier4Badges = 0;

                    if (hero.Badges != null)
                    {
                        foreach (HeroBadge hp in hero.Badges)
                        {
                            if (hp.Badge.Tier == BadgeType.TierCategory1)
                            {
                                nrOfTier1Badges++;
                            }
                            else if (hp.Badge.Tier == BadgeType.TierCategory2)
                            {
                                nrOfTier2Badges++;
                            }
                            else if (hp.Badge.Tier == BadgeType.TierCategory3)
                            {
                                nrOfTier3Badges++;
                            }
                            else if (hp.Badge.Tier == BadgeType.TierCategory4)
                            {
                                nrOfTier4Badges++;
                            }
                        }
                    }

                    await SwitchBadges(nrOfTier1Badges, BadgeType.TierCategory1, BadgeType.TierCategory2, hero);
                    await SwitchBadges(nrOfTier2Badges, BadgeType.TierCategory2, BadgeType.TierCategory3, hero);

                    if (hero.OverallStrength >= 700)
                    {
                        await SwitchBadges(nrOfTier3Badges, BadgeType.TierCategory3, BadgeType.TierCategory4, hero);
                    }

                    if (hero.OverallStrength >= 1000)
                    {
                        await SwitchBadges(nrOfTier4Badges, BadgeType.TierCategory4, BadgeType.TierCategory5, hero);
                        hero.IsGod = true;
                        await _context.SaveChangesAsync();
                    }

                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
