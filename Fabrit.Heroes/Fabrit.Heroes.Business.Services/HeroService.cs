using Fabrit.Heroes.Business.Services.Contracts;
using Fabrit.Heroes.Data;
using Fabrit.Heroes.Data.Business.Hero;
using Fabrit.Heroes.Data.Business.Power;
using Fabrit.Heroes.Data.Entities.Hero;
using Fabrit.Heroes.Data.Entities.Power;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Fabrit.Heroes.Infrastructure.Common.Exceptions;
using Fabrit.Heroes.Infrastructure.Common;
using Fabrit.Heroes.Data.Entities.User;
using Fabrit.Heroes.Data.Business.Authentication.Helpers;
using Microsoft.Extensions.Options;
using GeoCoordinatePortable;
using Fabrit.Heroes.Data.Entities.Badge;

namespace Fabrit.Heroes.Business.Services
{
    public class HeroService : IHeroService
    {
        private readonly HeroesDbContext _context;
        private readonly IPowerService _powerService;
        private readonly IHeroTypeService _heroTypeService;
        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;
        private readonly IRewardService _rewardService;

        public HeroService(HeroesDbContext context, IPowerService powerService, IHeroTypeService heroTypeService, IUserService userService, IOptions<AppSettings> appSettings, IRewardService rewardService)
        {
            _context = context;
            _powerService = powerService;
            _heroTypeService = heroTypeService;
            _userService = userService;
            _appSettings = appSettings.Value;
            _rewardService = rewardService;
        }

        public async Task ChangeHeroAvatar(int heroId, string avatarPath)
        {
            if (string.IsNullOrEmpty(avatarPath))
            {
                throw new InvalidParameterException();
            }
            var dbHero = await FindById(heroId);
            dbHero.AvatarPath = avatarPath;
            _context.Heroes.Update(dbHero);
            await _context.SaveChangesAsync();

            if (dbHero.User == null)
            {
                return;
            }

            var user = dbHero.User;
            user.AvatarPath = avatarPath;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();


        }

        public async Task<EntityTypeDto> GetEntityTypeDto(int heroId)
        {
            var hero = await this.FindById(heroId);
            EntityTypeDto entityTypeDto = new EntityTypeDto
            {
                Id = hero.Id,
                Name = hero.Name,
                IsBadGuy = hero.IsBadGuy,
                Latitude = hero.Latitude,
                Longitude = hero.Longitude

            };
            return entityTypeDto;
        }

        public IAsyncEnumerable<HeroDto> GetAllHeroesWithPowers()
        {
            return _context.Heroes
                .Include(h => h.Powers).ThenInclude(hp => hp.Power)
                .Include(h => h.Type)
                .Where(h => !h.IsBadGuy)
                .Select(hero => new HeroDto
                {
                    Id = hero.Id,
                    Name = hero.Name,
                    Powers = hero.Powers.Where(hp => hp.Power != null)
                    .Select(hp => new PowerDto
                    {
                        Id = hp.Power.Id,
                        Details = hp.Power.Details,
                        Element = hp.Power.Element.Type.ToString(),
                        Name = hp.Power.Name,
                        Strength = hp.Power.Strength
                    })
                    .OrderByDescending(hp => hp.Strength),
                    Type = hero.Type.Name,
                    Latitude = hero.Latitude,
                    Longitude = hero.Longitude
                })
                .AsAsyncEnumerable();
        }

        public IAsyncEnumerable<HeroDto> GetAll()
        {
            return _context.Heroes
                .Include(h => h.Powers).ThenInclude(hp => hp.Power)
                .Include(h => h.Type)
                .Include(h => h.Badges).ThenInclude(hp => hp.Badge)
                .Where(h => !h.IsBadGuy)
                .Select(hero => new HeroDto
                {
                    Id = hero.Id,
                    Name = hero.Name,
                    Powers = hero.Powers
                        .Where(hp => hp.Power != null && hp.Id != hero.MainPower.Id)
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
                    MainPower = hero.MainPower != null ? new PowerDto
                    {
                        Id = hero.MainPower.Power.Id,
                        Details = hero.MainPower.Power.Details,
                        Element = hero.MainPower.Power.Element.ToString(),
                        Name = hero.MainPower.Power.Name,
                        Strength = hero.MainPower.Power.Strength
                    } : null,
                    Latitude = hero.Latitude,
                    Longitude = hero.Longitude,
                    Badges = hero.Badges,
                    Birthday = hero.Birthday.ToString()
                })
                .AsAsyncEnumerable();
        }

        public IAsyncEnumerable<HeroDto> GetAllPlayers()
        {
            return _context.Heroes
                .Include(h => h.Powers).ThenInclude(hp => hp.Power)
                .Include(h => h.Type)
                .Include(h => h.Badges).ThenInclude(hp => hp.Badge)
                .Select(hero => new HeroDto
                {
                    Id = hero.Id,
                    Name = hero.Name,
                    Powers = hero.Powers
                        .Where(hp => hp.Power != null && hp.Id != hero.MainPower.Id)
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
                    MainPower = hero.MainPower != null ? new PowerDto
                    {
                        Id = hero.MainPower.Power.Id,
                        Details = hero.MainPower.Power.Details,
                        Element = hero.MainPower.Power.Element.ToString(),
                        Name = hero.MainPower.Power.Name,
                        Strength = hero.MainPower.Power.Strength
                    } : null,
                    Latitude = hero.Latitude,
                    Longitude = hero.Longitude,
                    Badges = hero.Badges,
                    isBadGuy = hero.IsBadGuy,
                    AvatarPath = Constants.APP_URL + hero.AvatarPath,
                    IsGod = hero.IsGod,
                    OverallStrength = hero.OverallStrength,
                    Birthday = hero.Birthday.ToString()
                })
                .AsAsyncEnumerable();
        }

        public IAsyncEnumerable<RowHeroData> GetAdminMapRowData()
        {
            return _context.Heroes
                .Include(h => h.Powers).ThenInclude(hp => hp.Power)
                .Include(h => h.Type)
                .Select(hero => new RowHeroData
                {
                    Id = hero.Id,
                    Name = hero.Name,
                    OtherPowers = hero.Powers
                        .Where(hp => hp.Id != hero.MainPower.Id)
                        .OrderByDescending(hp => hp.Power.Strength)
                        .Select(hp => hp.Power.Name),
                    Ally = hero.Allies.Where(a => a.AllyTo != null).Select(ha => ha.AllyTo.Name),
                    AvatarPath = Constants.APP_URL + hero.AvatarPath,
                    Birthday = hero.Birthday.Date.ToString(Constants.DATE_FORMAT),
                    OverallStrength = hero.OverallStrength,
                    MainPower = hero.MainPower.Power.Name,
                    Latitude = hero.Latitude,
                    Longitude = hero.Longitude,
                    IsBadGuy = hero.IsBadGuy
                })
                .AsAsyncEnumerable();
        }

        public IAsyncEnumerable<RowHeroData> GetHeroRowData()
        {
            return _context.Heroes
                .Include(h => h.Powers).ThenInclude(hp => hp.Power)
                .Include(h => h.Type)
                .Where(h => !h.IsBadGuy)
                .Select(hero => new RowHeroData
                {
                    Id = hero.Id,
                    Name = hero.Name,
                    OtherPowers = hero.Powers
                        .Where(hp => hp.Id != hero.MainPower.Id)
                        .OrderByDescending(hp => hp.Power.Strength)
                        .Select(hp => hp.Power.Name),
                    Ally = hero.Allies.Where(a => a.AllyTo != null).Select(ha => ha.AllyTo.Name),
                    AvatarPath = Constants.APP_URL + hero.AvatarPath,
                    Birthday = hero.Birthday.Date.ToString(Constants.DATE_FORMAT),
                    OverallStrength = hero.OverallStrength,
                    MainPower = hero.MainPower.Power.Name,
                    Latitude = hero.Latitude,
                    Longitude = hero.Longitude
                })
                .AsAsyncEnumerable();
        }

        public async Task DeleteHeroById(int id)
        {
            Hero hero = _context.Heroes.Include(h => h.User).Where(h => h.Id == id).FirstOrDefault();
            if (hero == null)
            {
                throw new EntityNotFoundException($"There is no hero with id: {id}");
            }
            if (hero.User != null)
            {
                _context.Users.Remove(hero.User);
                await _context.SaveChangesAsync();
            }
            var battles = _context.Battles.Where(b => b.InitiatorId == id);
            foreach(var battle in battles)
            {
                battle.InitiatorId = 0;
            }
            _context.UpdateRange(battles);
            await DeleteAllyTo(hero);
            _context.Heroes.Remove(hero);
            await _context.SaveChangesAsync();
        }

        public async Task CreateHero(HeroDto heroDto)
        {
            await CheckHeroFields(heroDto);
            Hero newHero = new Hero
            {
                Name = heroDto.Name,
            };

            newHero.TypeId = heroDto.TypeId;
            newHero.Powers = await CreateHeroPowers(heroDto, newHero);
            newHero.Allies = await CreateHeroAllies(heroDto, newHero);
            newHero.AvatarPath = Constants.DEFAULT_IMAGE_HERO;
            newHero.OverallStrength = newHero.Powers.Aggregate(0, (acc, power) => acc += power.Power.Strength);
            Power mainPower = await _context.Powers.FirstOrDefaultAsync(power => power.Id == heroDto.MainPower.Id);

            newHero.MainPower = mainPower == null ? null : new HeroPower
            {
                Hero = newHero,
                Power = mainPower,
                Strength = mainPower.Strength
            };
            newHero.OverallStrength += newHero.MainPower == null ? 0 : newHero.MainPower.Power.Strength;

            newHero.Latitude = heroDto.Latitude;
            newHero.Longitude = heroDto.Longitude;

            if (!string.IsNullOrEmpty(heroDto.Email))
            {
                var user = ConvertHeroToUser(heroDto);

                await _userService.CreateHeroUser(user);
                newHero.User = user;
            }

            await _context.AddAsync(newHero);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateHero(HeroDto heroDto)
        {
            await CheckHeroFields(heroDto);
            var heroForUpdate = await _context.Heroes
                 .FirstOrDefaultAsync(h => h.Id == heroDto.Id);

            if (heroForUpdate == null)
            {
                throw new EntityNotFoundException($"There is no hero with id: {heroDto.Id}");
            }

            await DeleteAllHeroPowers(heroForUpdate);
            await DeleteAllHeroAllies(heroForUpdate);
            heroForUpdate.Powers = null;

            heroForUpdate.Name = heroDto.Name;
            heroForUpdate.TypeId = heroDto.TypeId;
            heroForUpdate.Powers = await CreateHeroPowers(heroDto, heroForUpdate);
            heroForUpdate.Allies = await CreateHeroAllies(heroDto, heroForUpdate);
            heroForUpdate.OverallStrength = heroForUpdate.Powers.Aggregate(0, (acc, power) => acc += power.Power.Strength);

            Power mainPower = await _context.Powers.FirstOrDefaultAsync(power => power.Id == heroDto.MainPower.Id);
            heroForUpdate.MainPower = mainPower != null ? new HeroPower
            {
                Hero = heroForUpdate,
                Power = mainPower,
                Strength = mainPower.Strength
            } : null;
            heroForUpdate.OverallStrength += heroForUpdate.MainPower == null ? 0 : heroForUpdate.MainPower.Power.Strength;


            heroForUpdate.Latitude = heroDto.Latitude;
            heroForUpdate.Longitude = heroDto.Longitude;


            _context.Heroes.Update(heroForUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateHeroBirthday(int heroId, string date)
        {
            var dbHero = await FindById(heroId);
            try
            {
                DateTime birthday = DateTime.ParseExact(date, Constants.DATE_FORMAT, null);
                dbHero.Birthday = birthday;

                _context.Heroes.Update(dbHero);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new InvalidParameterException(e.Message);
            }
        }

        public async Task<HeroDto> FindHeroDtoById(int id)
        {
            var hero = await _context.Heroes.Where(h => h.Id == id)
                                        .Include(h => h.Powers).ThenInclude(hp => hp.Power)
                                        .Include(h => h.MainPower).ThenInclude(hp => hp.Power)
                                        .Include(h => h.Type)
                                        .Include(h => h.Allies).ThenInclude(ha => ha.AllyTo).ThenInclude(h => h.Type)
                                        .Select(hero => new HeroDto
                                        {
                                            Id = hero.Id,
                                            Name = hero.Name,
                                            Type = hero.Type.Name,
                                            TypeId = hero.TypeId,
                                            Powers = hero.Powers.Where(hp => hp.Power != null).Select(hp => new PowerDto
                                            {
                                                Id = hp.Power.Id,
                                                Details = hp.Power.Details,
                                                Element = hp.Power.Element.ToString(),
                                                Name = hp.Power.Name,
                                                Strength = hp.Strength
                                            }),
                                            Allies = hero.Allies.Where(ha => ha.AllyTo != null).Select(ha => new HeroDto
                                            {
                                                Id = ha.AllyTo.Id,
                                                Name = ha.AllyTo.Name,
                                                Type = ha.AllyTo.Type.Name,
                                            }),
                                            MainPower = hero.MainPower == null ? null : new PowerDto
                                            {
                                                Id = hero.MainPower.Power.Id,
                                                Details = hero.MainPower.Power.Details,
                                                Element = hero.MainPower.Power.Element.ToString(),
                                                Name = hero.MainPower.Power.Name,
                                                Strength = hero.MainPower.Power.Strength
                                            },
                                            Latitude = hero.Latitude,
                                            Longitude = hero.Longitude,
                                            Role = hero.IsBadGuy ? Constants.VILLAIN_ROLE : Constants.HERO_ROLE,
                                            Birthday = hero.Birthday.ToString(),
                                            OverallStrength = hero.OverallStrength,
                                            AvatarPath = hero.AvatarPath,
                                            IsGod = hero.IsGod
                                        })
                                        .FirstOrDefaultAsync();
            return hero != null
                ? hero
                : throw new EntityNotFoundException($"There is no hero with id: {id}");
        }

        private async Task DeleteAllHeroPowers(Hero hero)
        {
            var heroPowersCollection = _context.HeroPowers
                .Where(hp => hp.HeroId == hero.Id)
                .AsAsyncEnumerable();

            await foreach (var hp in heroPowersCollection)
            {
                _context.Remove(hp);
            }

            await _context.SaveChangesAsync();
        }

        private async Task DeleteAllyTo(Hero hero)
        {
            var heroAlliesCollection = _context.HeroAllies
                .Where(ha => ha.AllyToId == hero.Id)
                .AsAsyncEnumerable();
            await foreach (var ha in heroAlliesCollection)
            {
                _context.Remove(ha);
            }

            await _context.SaveChangesAsync();
        }

        private async Task DeleteAllHeroAllies(Hero hero)
        {
            var heroAlliesCollection = _context.HeroAllies
                .Where(ha => ha.AllyFromId == hero.Id)
                .AsAsyncEnumerable();

            await foreach (var ha in heroAlliesCollection)
            {
                _context.Remove(ha);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Hero> FindById(int id)
        {
            Hero hero = await _context.Heroes.Where(h => h.Id == id)
                                        .Include(h => h.Powers).ThenInclude(hp => hp.Power)
                                        .Include(h => h.Type)
                                        .Include(h => h.Badges).ThenInclude(hp => hp.Badge)
                                        .Include(h => h.Allies).ThenInclude(ha => ha.AllyTo)
                                        .Include(h => h.Battles).ThenInclude(hb => hb.Battle)
                                        .Include(h => h.User)
                                        .FirstOrDefaultAsync();

            return hero != null
                ? hero
                : throw new EntityNotFoundException($"There is no hero with id: {id}");
        }

        private async Task<Hero> FindByName(string heroName)
        {
            var hero = await _context.Heroes
                .Where(h => h.Name.Equals(heroName))
                .FirstOrDefaultAsync();

            return hero != null
                ? hero
                : throw new EntityNotFoundException($"There is no hero with name: {heroName}");
        }

        private async Task CheckHeroFields(HeroDto heroDto)
        {
            await _heroTypeService.FindById(heroDto.TypeId);

            if (string.IsNullOrEmpty(heroDto.Name) || string.IsNullOrEmpty(heroDto.TypeId.ToString()))
            {
                throw new InvalidParameterException();
            }

            try
            {
                var heroFromDb = await FindByName(heroDto.Name);
                if (heroFromDb != null && heroFromDb.Id != heroDto.Id)
                {
                    throw new DuplicateException($"There is a hero with the same name: {heroDto.Name}");
                }
            }
            catch { }
        }

        private async Task<ICollection<HeroPower>> CreateHeroPowers(HeroDto heroDto, Hero hero)
        {
            var heroPowers = new List<HeroPower>();

            foreach (var power in heroDto.Powers)
            {
                heroPowers.Add(new HeroPower
                {
                    Hero = hero,
                    Power = await _powerService.FindById(power.Id),
                    Strength = (await _powerService.FindById(power.Id)).Strength
                });
            }

            return heroPowers;
        }

        private async Task<ICollection<HeroAlly>> CreateHeroAllies(HeroDto heroDto, Hero hero)
        {
            var heroAllies = new List<HeroAlly>();

            foreach (var ally in heroDto.Allies)
            {
                heroAllies.Add(new HeroAlly
                {
                    AllyFrom = hero,
                    AllyTo = await FindById(ally.Id)
                });
            }

            return heroAllies;
        }

        public async Task ChangeHeroLocation(int id, double latitude, double longitude)
        {
            if (id < 1)
            {
                throw new ArgumentException("Invalid Id");
            }
            if (latitude > 90 || latitude < -90)
            {
                throw new ArgumentException("Invalid latitude");
            }
            if (longitude > 180 || longitude < -180)
            {
                throw new ArgumentException("Invalid longitude");
            }
            var dbHero = await FindById(id);
            try
            {
                dbHero.Latitude = latitude;
                dbHero.Longitude = longitude;


                _context.Heroes.Update(dbHero);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new InvalidParameterException(e.Message);
            }
        }

        private User ConvertHeroToUser(HeroDto hero)
        {
            return new User()
            {
                Email = hero.Email,
                FullName = hero.Name,
                Username = hero.Email,
                Age = 18,
            };
        }

        public async Task<ICollection<Hero>> GetHeroesInRange(int heroId)
        {
            var heroFound = await FindById(heroId);
            var heroPosition = new GeoCoordinate(heroFound.Latitude, heroFound.Longitude);
            var res = new List<Hero>();

            if (!_rewardService.HasBadgeByType(BadgeType.TierCategory5, heroFound))
            {
                var heroes = await _context.Heroes.ToListAsync();

                foreach (var hero in heroes)
                {
                    if (CheckDistance(heroPosition, hero.Latitude, hero.Longitude))
                    {
                        res.Add(hero);
                    }
                }
            }
            else
            {
                var heroes = await _context.Heroes.Where(h => h.IsGod).ToListAsync();

                foreach (var hero in heroes)
                {
                    if (CheckDistance(heroPosition, hero.Latitude, hero.Longitude))
                    {
                        res.Add(hero);
                    }
                }
            }
            return res;
        }

        private bool CheckDistance(GeoCoordinate startLocation, double endLatitude, double endLongitude)
        {
            var endLocation = new GeoCoordinate(endLatitude, endLongitude);
            var distanceWanted = _appSettings.Range;
            var x = startLocation.GetDistanceTo(endLocation);
            return startLocation.GetDistanceTo(endLocation) < distanceWanted;
        }

        public HeroDto AddBonusStrength(HeroDto hero)
        {
            if (hero.Powers != null)
            {
                int tenPercentIncrease = hero.OverallStrength + Convert.ToInt32(_appSettings.TEN_PERCENT_BONUS * Convert.ToDouble(hero.OverallStrength));
                int fivePercentIncrease = hero.OverallStrength + Convert.ToInt32(_appSettings.FIVE_PERCENT_BONUS * Convert.ToDouble(hero.OverallStrength));

                foreach (var power in hero.Powers)
                {
                    if (power.Name == Constants.STRENGTH_POWER)
                    {
                        hero.OverallStrength = tenPercentIncrease;
                    }
                    else if (power.Name == Constants.SPEED_POWER || power.Name == Constants.INVISIBILITY_POWER)
                    {
                        hero.OverallStrength = fivePercentIncrease;
                    }
                }
            }
            return hero;
        }

        public ICollection<HeroDto> ModifyStrengthOfParty(ICollection<HeroDto> heroes, double percentage)
        {
            foreach (HeroDto hero in heroes)
            {
                hero.OverallStrength += Convert.ToInt32(percentage * Convert.ToDouble(hero.OverallStrength));
            }
            return heroes;
        }

        public PowerDto FindPowerOfHero(HeroDto hero, string powerName)
        {
            if (hero.Powers != null)
            {
                return hero.Powers.FirstOrDefault(power => power.Name == powerName);
            }

            return null;
        }

        public ICollection<HeroDto> ModifyStrengthOfHeroParty(ICollection<HeroDto> heroes)
        {
            heroes = heroes.Select(h => AddBonusStrength(h)).ToList();

            foreach (var hero in heroes)
            {
                if (hero.Powers != null)
                {
                    if (FindPowerOfHero(hero, Constants.TELEPATHY_POWER) != null)
                    {
                        heroes = ModifyStrengthOfParty(heroes, _appSettings.FIVE_PERCENT_BONUS);
                    }

                    if (FindPowerOfHero(hero, Constants.MADDJSKILLZS_POWER) != null)
                    {
                        heroes = ModifyStrengthOfParty(heroes, _appSettings.TEN_PERCENT_BONUS);
                    }

                    if (FindPowerOfHero(hero, Constants.CURSEOFBADPROGRAMMING_POWER) != null)
                    {
                        heroes = ModifyStrengthOfParty(heroes, _appSettings.FIVE_PERCENT_PENALTY);
                    }
                }
            }
            return heroes;
        }

        public async Task<HeroDto> GetHeroByUserId(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Invalid User ID");
            }
            var hero = await _context.Heroes.FirstOrDefaultAsync(hero => hero.UserId == id);
            return await FindHeroDtoById(hero.Id);
        }

        public async Task ChangeEntityDetails(HeroDto entity)
        {
            var entityForUpdate = await _context.Heroes
                 .FirstOrDefaultAsync(h => h.Id == entity.Id);

            if (entityForUpdate == null)
            {
                throw new EntityNotFoundException($"There is no entity with id: {entity.Id}");
            }

            entityForUpdate.Name = entity.Name;
            entityForUpdate.Birthday = DateTime.Parse(entity.Birthday);

            _context.Heroes.Update(entityForUpdate);
            await _context.SaveChangesAsync();
        }

        public void ChangeOverallStrength(int id)
        {
            var hero = _context.Heroes.Include(h => h.Powers).Include(h => h.MainPower).First(h => h.Id == id);
            hero.OverallStrength = hero.Powers.Aggregate(0, (acc, power) => acc += power.Strength);
            _context.Update(hero);
            _context.SaveChanges();
        }

        public async Task<HeroDto> ChangeHeroTravel(int id, double latitude, double longitude)
        {
            if (id < 1)
            {
                throw new ArgumentException("Invalid Id");
            }
            if (latitude > 90 || latitude < -90)
            {
                throw new ArgumentException("Invalid latitude");
            }
            if (longitude > 180 || longitude < -180)
            {
                throw new ArgumentException("Invalid longitude");
            }
            var dbHero = await FindById(id);
            var diff = DateTime.Now.Subtract(dbHero.LastTimeMoved);
            if (diff.TotalMinutes < _appSettings.MinutesToWait)
            {
                var remainingSeconds = 300 - Math.Floor(diff.TotalSeconds);
                throw new InvalidTravel($"You still need to wait {remainingSeconds} seconds");
            }
            try
            {
                dbHero.Latitude = latitude;
                dbHero.Longitude = longitude;
                dbHero.LastTimeMoved = DateTime.Now;


                _context.Heroes.Update(dbHero);
                await _context.SaveChangesAsync();

                return new HeroDto
                {
                    Id = dbHero.Id,
                    Name = dbHero.Name,
                    Type = dbHero.Type.Name,
                    TypeId = dbHero.TypeId,
                    Latitude = dbHero.Latitude,
                    Longitude = dbHero.Longitude,
                    Role = dbHero.IsBadGuy ? Constants.VILLAIN_ROLE : Constants.HERO_ROLE,
                    Birthday = dbHero.Birthday.ToString(),
                    OverallStrength = dbHero.OverallStrength,
                    AvatarPath = dbHero.AvatarPath,
                    IsGod = dbHero.IsGod
                };
            }
            catch (Exception e)
            {
                throw new InvalidParameterException(e.Message);
            }
        }
    }
}
