using Fabrit.Heroes.Business.Services.Contracts;
using Fabrit.Heroes.Data;
using Fabrit.Heroes.Data.Business.Battle;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Fabrit.Heroes.Infrastructure.Common.Exceptions;
using Fabrit.Heroes.Infrastructure.Common;
using Microsoft.Extensions.Options;
using Fabrit.Heroes.Data.Entities.Battle;
using Fabrit.Heroes.Data.Entities.Badge;
using Fabrit.Heroes.Data.Business.Hero;
using Fabrit.Heroes.Data.Business.Villain;
using Fabrit.Heroes.Data.Business.Power;
using Fabrit.Heroes.Data.Entities.Hero;
using GeoCoordinatePortable;
using Fabrit.Heroes.Data.Business.Authentication.Helpers;

namespace Fabrit.Heroes.Business.Services
{
    public class BattleService : IBattleService
    {
        private readonly HeroesDbContext _context;
        private readonly IRewardService _rewardService;
        private readonly IHeroService _heroService;
        private readonly IVillainService _villainService;
        private readonly AppSettings _appSettings;



        public BattleService(HeroesDbContext context, IRewardService hashingManager, IHeroService heroService, IVillainService villainService, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _rewardService = hashingManager;
            _heroService = heroService;
            _villainService = villainService;
            _appSettings = appSettings.Value;
        }

        public IAsyncEnumerable<BattleDto> GetAll()
        {
            return _context.Battles
                .Include(b => b.Heroes)
                .ThenInclude(hb => hb.Hero)
                .Include(b => b.Heroes)
                .ThenInclude(hb => hb.Hero).ThenInclude(h => h.Powers).ThenInclude(p => p.Power).ThenInclude(p => p.Element)
                .Select(battle => new BattleDto
                {
                    Id = battle.Id,
                    InitiatorId = battle.InitiatorId,
                    Latitude = battle.Latitude,
                    Longitude = battle.Longitude,

                    Heroes = battle.Heroes
                        .Where(h => !h.Hero.IsBadGuy)
                .Select(h => new HeroDto
                {
                    Id = h.Hero.Id,
                    Name = h.Hero.Name,
                    Powers = h.Hero.Powers
                        .Where(hp => hp.Power != null && hp.Id != h.Hero.MainPower.Id)
                        .OrderByDescending(hp => hp.Power.Strength)
                        .Select(hp => new PowerDto
                        {
                            Id = hp.Power.Id,
                            Details = hp.Power.Details,
                            Element = hp.Power.Element.Type.ToString(),
                            Name = hp.Power.Name,
                            Strength = hp.Power.Strength
                        }),
                    Type = h.Hero.Type.Name,
                    MainPower = h.Hero.MainPower != null ? new PowerDto
                    {
                        Id = h.Hero.MainPower.Power.Id,
                        Details = h.Hero.MainPower.Power.Details,
                        Element = h.Hero.MainPower.Power.Element.Type.ToString(),
                        Name = h.Hero.MainPower.Power.Name,
                        Strength = h.Hero.MainPower.Power.Strength
                    } : null,
                    Latitude = h.Hero.Latitude,
                    Longitude = h.Hero.Longitude,
                    Badges = h.Hero.Badges
                }),
                    Villains = battle.Heroes
                        .Where(h => h.Hero.IsBadGuy)
                .Select(h => new VillainDto
                {
                    Id = h.Hero.Id,
                    Name = h.Hero.Name,
                    Powers = h.Hero.Powers
                        .Where(hp => hp.Power != null)
                        .OrderByDescending(hp => hp.Power.Strength)
                        .Select(hp => new PowerDto
                        {
                            Id = hp.Power.Id,
                            Details = hp.Power.Details,
                            Element = hp.Power.Element.Type.ToString(),
                            Name = hp.Power.Name,
                            Strength = hp.Power.Strength
                        }),
                    Type = h.Hero.Type.Name,
                    MainPower = h.Hero.MainPower != null ? new PowerDto
                    {
                        Id = h.Hero.MainPower.Power.Id,
                        Details = h.Hero.MainPower.Power.Details,
                        Element = h.Hero.MainPower.Power.Element.Type.ToString(),
                        Name = h.Hero.MainPower.Power.Name,
                        Strength = h.Hero.MainPower.Power.Strength
                    } : null,
                    Latitude = h.Hero.Latitude,
                    Longitude = h.Hero.Longitude
                }),
                    HeroesStrength = battle.Heroes.Where(h => !h.Hero.IsBadGuy).Sum(v => v.Hero.OverallStrength),
                    VillainsStrength = battle.Heroes.Where(h => h.Hero.IsBadGuy).Sum(v => v.Hero.OverallStrength)
                })
                .AsAsyncEnumerable();
        }

        public ICollection<HeroDto> GetHeroesOfBattle(Battle battle)
        {
            return battle.Heroes.Where(hb => !hb.Hero.IsBadGuy)
                .Select(
                    hb => new HeroDto
                    {
                        Id = hb.Hero.Id,
                        Name = hb.Hero.Name,
                        Powers = hb.Hero.Powers
                        .Where(hp => hp.Power != null)
                        .OrderByDescending(hp => hp.Power.Strength)
                        .Select(hp => new PowerDto
                        {
                            Id = hp.Power.Id,
                            Details = hp.Power.Details,
                            Name = hp.Power.Name,
                            Strength = hp.Power.Strength
                        }),
                        Allies = null,
                        OverallStrength = hb.Hero.OverallStrength,
                        Badges = null
                    }).ToList();
        }

        public ICollection<VillainDto> GetVillainsDtosFromHeroes(ICollection<Hero> heroes)
        {
            return heroes.Where(hb => hb.IsBadGuy)
                .Select(
                    hb => new VillainDto
                    {
                        Id = hb.Id,
                        Name = hb.Name,
                        Powers = hb.Powers
                        .Where(hp => hp.Power != null)
                        .OrderByDescending(hp => hp.Power.Strength)
                        .Select(hp => new PowerDto
                        {
                            Id = hp.Power.Id,
                            Details = hp.Power.Details,
                            Name = hp.Power.Name,
                            Strength = hp.Power.Strength
                        }),
                        Allies = null,
                        OverallStrength = hb.OverallStrength,
                        Badges = null
                    }).ToList();
        }
        
        public ICollection<HeroDto> GetHeroesDtosFromHeroes(ICollection<Hero> heroes)
        {
            return heroes.Where(hb => !hb.IsBadGuy)
                .Select(
                    hb => new HeroDto
                    {
                        Id = hb.Id,
                        Name = hb.Name,
                        Powers = hb.Powers
                        .Where(hp => hp.Power != null)
                        .OrderByDescending(hp => hp.Power.Strength)
                        .Select(hp => new PowerDto
                        {
                            Id = hp.Power.Id,
                            Details = hp.Power.Details,
                            Name = hp.Power.Name,
                            Strength = hp.Power.Strength
                        }),
                        Type = hb.Type.Name,
                        MainPower = hb.MainPower != null ? new PowerDto
                        {
                            Id = hb.MainPower.Power.Id,
                            Details = hb.MainPower.Power.Details,
                            Name = hb.MainPower.Power.Name,
                            Strength = hb.MainPower.Power.Strength
                        } : null,
                        Latitude = hb.Latitude,
                        Longitude = hb.Longitude,
                        OverallStrength = hb.OverallStrength,
                        Allies = null,
                        Badges = null
                    }).ToList();
        }

        public ICollection<VillainDto> GetVillainsOfBattle(Battle battle)
        {
            return battle.Heroes.Where(hb => hb.Hero.IsBadGuy)
                 .Select(
                     hb => new VillainDto
                     {
                         Id = hb.Hero.Id,
                         Name = hb.Hero.Name,
                         Powers = hb.Hero.Powers
                         .Where(hp => hp.Power != null)
                         .OrderByDescending(hp => hp.Power.Strength)
                         .Select(hp => new PowerDto
                         {
                             Id = hp.Power.Id,
                             Details = hp.Power.Details,
                             Name = hp.Power.Name,
                             Strength = hp.Power.Strength
                         }),
                         OverallStrength = hb.Hero.OverallStrength,
                         Badges = null
                     }).ToList();
        }

        private ICollection<HeroBattle> CreateHeroBattles(ICollection<Hero> participants, Battle battle)
        {
            var heroBattles = new List<HeroBattle>();

            var heroes = _context.Heroes
                .Include(h => h.Powers).ThenInclude(hp => hp.Power)
                .Include(h => h.Type)
                .Include(h => h.Badges).ThenInclude(hp => hp.Badge);

            foreach (var hero in participants)
            {
                heroBattles.Add(new HeroBattle
                {
                    Hero = heroes.FirstOrDefault(h => h.Id == hero.Id),
                    Battle = battle
                });
            }

            return heroBattles;
        }

        public async Task<BattleDto> CreateBattle(int initiatorId, int opponentId)
        {
            var initiator = await _heroService.FindById(initiatorId);
            var opponent = await _heroService.FindById(opponentId);

            var diff = DateTime.Now.Subtract(initiator.LastTimeMoved);
            if (diff.TotalMinutes < _appSettings.MinutesToWait)
            {
                var remainingSeconds = 300 - Math.Floor(diff.TotalSeconds);
                throw new InvalidTravel($"You still need to wait {remainingSeconds} seconds");
            }

            Battle newBattle = new Battle
            {
                InitiatorId = initiatorId,
                Latitude = opponent.Latitude,
                Longitude = opponent.Longitude
            };

            

            _context.Heroes.Update(initiator);
            await _context.SaveChangesAsync();

            var participants = await _heroService.GetHeroesInRange(opponentId);
            var heroInitiator = participants.FirstOrDefault(participant => participant.Id == initiatorId);
            if (heroInitiator==null)
            {
                participants.Add(initiator);
            }

            newBattle.Heroes = CreateHeroBattles(participants, newBattle);

            await _context.AddAsync(newBattle);
            await _context.SaveChangesAsync();

            BattleDto battleDto = new BattleDto
            {
                Id = newBattle.Id,
                InitiatorId = newBattle.InitiatorId,
                OpponentId = opponentId,
                Latitude = newBattle.Latitude,
                Longitude = newBattle.Longitude
            };

            var participantsList = participants.ToList();
            participantsList.ForEach(participant => participant.Badges = null);
            participantsList.ForEach(participant => participant.Battles = null);

            battleDto.Heroes = GetHeroesOfBattle(newBattle);
            battleDto.Villains = GetVillainsOfBattle(newBattle);

            battleDto.HeroesStrength = battleDto.Heroes.Sum(v => v.OverallStrength);
            battleDto.VillainsStrength = battleDto.Villains.Sum(v => v.OverallStrength);

            return battleDto;
        }

        public async Task DeleteBattle(int id)
        {
            Battle battle = _context.Battles.Where(h => h.Id == id).FirstOrDefault();

            if (battle == null)
            {
                throw new EntityNotFoundException($"There is no battle with the given id: {id}");
            }

            if (battle.Heroes != null)
            {
                var heroBattles = _context.HeroBattles
               .Where(hb => hb.HeroId == battle.InitiatorId)
               .AsAsyncEnumerable();

                _context.RemoveRange(heroBattles);
                await _context.SaveChangesAsync();
            }

            _context.Battles.Remove(battle);
            await _context.SaveChangesAsync();
        }

        public async Task SetWinner(int winnerId, int battleId)
        {
            HeroBattle heroBattle = _context.HeroBattles
              .Where(hb => hb.Hero.Id == winnerId && hb.Battle.Id == battleId).FirstOrDefault();

            heroBattle.HasWon = true;
            await _context.SaveChangesAsync();
        }

        public async Task LoseMostValuableBadge (Hero hero)
        {
            BadgeType mostValuableBadgeType = _rewardService.GetMostValuableBadgeType(hero);
            await _rewardService.DeleteBadge(mostValuableBadgeType, hero);
            await _context.SaveChangesAsync();
        }

        public async Task<BattleResultDto> PlayGame(BattleDto battleDto)
        {
            Task.Delay(3000).Wait();

            var heroes = _context.Heroes
                .Include(h => h.Powers).ThenInclude(hp => hp.Power)
                .Include(h => h.Type)
                .Include(h => h.Badges).ThenInclude(hp => hp.Badge);

            var initiator = await _heroService.FindById(battleDto.InitiatorId);
            var opponent = await _heroService.FindById(battleDto.OpponentId);
            initiator.Latitude = opponent.Latitude + 0.1;
            initiator.Longitude = opponent.Longitude + 0.1;
            initiator.LastTimeMoved = DateTime.Now;

            await _rewardService.AddBadge(BadgeType.TierCategory2, initiator);

            battleDto.Heroes = _heroService.ModifyStrengthOfHeroParty(battleDto.Heroes.ToList());
            battleDto.Villains = _villainService.ModifyStrengthOfHeroParty(battleDto.Villains.ToList());
            battleDto.HeroesStrength = battleDto.Heroes.Sum(v => v.OverallStrength);
            battleDto.VillainsStrength = battleDto.Villains.Sum(v => v.OverallStrength);

            if (battleDto.VillainsStrength > battleDto.HeroesStrength)
            {
                foreach (var villain in battleDto.Villains)
                {
                    await SetWinner(villain.Id, battleDto.Id);
                    var dbHero = heroes.Where(h => h.IsBadGuy).FirstOrDefault(h => h.Id == villain.Id);
                    await _rewardService.AddBadge(BadgeType.TierCategory1, dbHero);
                }

                foreach (var hero in battleDto.Heroes)
                {
                    var dbHero = heroes.Where(h => !h.IsBadGuy).FirstOrDefault(h => h.Id == hero.Id);
                    var mostValuableBadgeType = _rewardService.GetMostValuableBadgeType(dbHero);
                    await _rewardService.DeleteBadge(mostValuableBadgeType, dbHero);
                }

                return new BattleResultDto
                {
                    Result = Constants.VILLAINS_WON_MESSAGE,
                    HeroesStrength = battleDto.HeroesStrength,
                    VillainStrength = battleDto.VillainsStrength
                };
            }
            else if (battleDto.HeroesStrength == battleDto.VillainsStrength)
            {
                return new BattleResultDto
                {
                    Result = Constants.EQUAL_SCORE_MESSAGE,
                    HeroesStrength = battleDto.HeroesStrength,
                    VillainStrength = battleDto.VillainsStrength
                };
            }
            else
            {
                foreach (var hero in battleDto.Heroes)
                {
                    await SetWinner(hero.Id, battleDto.Id);
                    var dbHero = heroes.Where(h => !h.IsBadGuy).FirstOrDefault(h => h.Id == hero.Id);
                    await _rewardService.AddBadge(BadgeType.TierCategory1, dbHero);
                }

                foreach (var villain in battleDto.Villains)
                {
                    var dbHero = heroes.Where(h => h.IsBadGuy).FirstOrDefault(h => h.Id == villain.Id);
                    var mostValuableBadgeType = _rewardService.GetMostValuableBadgeType(dbHero);
                    await _rewardService.DeleteBadge(mostValuableBadgeType, dbHero);
                }

                return new BattleResultDto
                {
                    Result = Constants.HEROES_WON_MESSAGE,
                    HeroesStrength = battleDto.HeroesStrength,
                    VillainStrength = battleDto.VillainsStrength
                };
            }
        }

        public ICollection<BattleRecordDto> GetBattleHistory(int id)
        {
            return _context.HeroBattles.Include(h=>h.Battle).Where(hb => hb.HeroId == id)
                .Select(h => new BattleRecordDto
                {
                    PlayerId = h.Hero.Id,
                    Battle = new BattleDto
                    {
                        Id = h.Battle.Id,
                        Latitude = h.Battle.Latitude,
                        Longitude = h.Battle.Longitude,
                        InitiatorId = h.Battle.InitiatorId
                    },
                    HasWon = h.HasWon
                }).ToList();
        }

        public async Task<int> GetNrOfBattlesForHero(int id)
        {
            return await _context.HeroBattles.Where(hb => hb.HeroId == id).CountAsync();
        }

        public async Task<BadgesCountDto> GetBadgesCountDto(int id)
        {
            var badges = _context.HeroBadges.Include(hb => hb.Badge).Include(hb => hb.Hero).Where(hb => hb.HeroId == id);
            return new BadgesCountDto()
            {
                Tier1 = await badges.Where(b => b.Badge.Tier == BadgeType.TierCategory1).CountAsync(),
                Tier2 = await badges.Where(b => b.Badge.Tier == BadgeType.TierCategory2).CountAsync(),
                Tier3 = await badges.Where(b => b.Badge.Tier == BadgeType.TierCategory3).CountAsync(),
                Tier4 = await badges.Where(b => b.Badge.Tier == BadgeType.TierCategory4).CountAsync(),
                Tier5 = await badges.Where(b => b.Badge.Tier == BadgeType.TierCategory5).CountAsync(),

            };
         }
    }
}
