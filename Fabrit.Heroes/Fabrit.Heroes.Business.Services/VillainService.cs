using Fabrit.Heroes.Business.Services.Contracts;
using Fabrit.Heroes.Data;
using Fabrit.Heroes.Data.Business.Power;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Fabrit.Heroes.Infrastructure.Common;
using Fabrit.Heroes.Data.Business.Villain;
using System.Threading.Tasks;
using Fabrit.Heroes.Infrastructure.Common.Exceptions;
using Fabrit.Heroes.Data.Entities.Hero;
using System;
using Microsoft.Extensions.Options;
using Fabrit.Heroes.Data.Business.Authentication.Helpers;
using Fabrit.Heroes.Data.Entities.User;

namespace Fabrit.Heroes.Business.Services
{
    public class VillainService : IVillainService
    {
        private readonly HeroesDbContext _context;
        private readonly IPowerService _powerService;
        private readonly AppSettings _appSettings;
        private readonly IUserService _userService;

        public VillainService(HeroesDbContext context, IPowerService powerService, IOptions<AppSettings> appSettings, IUserService userService)
        {
            _context = context;
            _powerService = powerService;
            _appSettings = appSettings.Value;
            _userService = userService;
        }

        public IAsyncEnumerable<VillainDto> GetAll()
        {
            return _context.Heroes
                .Where(h => h.IsBadGuy)
                .Include(h => h.Powers).ThenInclude(hp => hp.Power)
                .Include(h => h.Type)
                .Include(h => h.Badges)
                .Select(hero => new VillainDto
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
                    Badges = hero.Badges
                })
                .AsAsyncEnumerable();
        }

        public IAsyncEnumerable<RowVillainData> GetVillainRowData()
        {
            return _context.Heroes
                .Where(h => h.IsBadGuy)
                .Include(h => h.Powers).ThenInclude(hp => hp.Power)
                .Include(h => h.Type)
                .Select(hero => new RowVillainData
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
                    MainPower = hero.MainPower.Power.Name
                })
                .AsAsyncEnumerable();
        }



        public async Task<Hero> GetVillainById(int id)
        {
            if (id < 0)
            {
                throw new InvalidParameterException("Invalid id");
            }

            var villain = await _context.Heroes
                               .Where(h => h.IsBadGuy)
                               .Include(h => h.Powers).ThenInclude(hp => hp.Power)
                               .Include(h => h.Type)
                               .FirstOrDefaultAsync(e => e.Id == id);
            if (villain == null)
            {
                throw new EntityNotFoundException("Villain not found");
            }

            return villain;
        }

        public async Task<VillainDto> GetVillainDtoById(int id)
        {
            var villain = await _context.Heroes
                               .Where(h => h.IsBadGuy)
                               .Include(h => h.Powers).ThenInclude(hp => hp.Power)
                               .Include(h => h.Type)
                               .Include(h => h.Allies)
                               .Select(hero => new VillainDto
                               {
                                   Id = hero.Id,
                                   Name = hero.Name,
                                   Powers = hero.Powers
                        .Where(hp => hp.Power != null && hp.Id != hero.MainPower.Id)
                        .Select(hp => new PowerDto
                        {
                            Id = hp.Power.Id,
                            Details = hp.Power.Details,
                            Element = hp.Power.Element.ToString(),
                            Name = hp.Power.Name,
                            Strength = hp.Power.Strength
                        }),
                                   Type = hero.Type.Name,
                                   TypeId = hero.Type.Id,
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
                                   Birthday = hero.Birthday.ToString(),
                                   OverallStrength = hero.OverallStrength,
                                   AvatarPath = hero.AvatarPath,
                                    Allies = hero.Allies.Where(ha => ha.AllyTo != null).Select(ha => new VillainDto
                                    {
                                        Id = ha.AllyTo.Id,
                                        Name = ha.AllyTo.Name,
                                        Type = ha.AllyTo.Type.Name,
                                    }),

                               })
                               .FirstOrDefaultAsync(e => e.Id == id);
            if (villain == null)
            {
                throw new EntityNotFoundException("Villain not found");
            }

            return villain;
        }

        public async Task ChangeImage(int id, string avatarPath)
        {
            if (string.IsNullOrEmpty(avatarPath))
            {
                throw new InvalidParameterException();
            }

            var dbHero = await GetVillainById(id);
            dbHero.AvatarPath = avatarPath;

            _context.Heroes.Update(dbHero);
            await _context.SaveChangesAsync();
        }

        private void ValidateVillainDto(VillainDto villainDto)
        {
            if (string.IsNullOrWhiteSpace(villainDto.Name))
            {
                throw new InvalidParameterException("Villain is invalid");
            }
        }

        public async Task<Hero> ToEntity(VillainDto villainDto)
        {
            ValidateVillainDto(villainDto);

            var dbType = await _context.HeroTypes.FirstOrDefaultAsync(ht => ht.Id == villainDto.TypeId);
            var dbMainPower = await _context.Powers.FirstOrDefaultAsync(power => power.Name == villainDto.MainPower.Name);

            var hero = new Hero
            {
                Id = villainDto.Id,
                Name = villainDto.Name,
                Type = dbType
            };

            hero.MainPower = villainDto.MainPower.Id == 0 ? null : new HeroPower
            {
                Hero = hero,
                Power = await _powerService.FindById(villainDto.MainPower.Id)
            };

            hero.Powers = new List<HeroPower>();
            if (villainDto.Powers != null)
            {
                foreach (var power in villainDto.Powers)
                {
                    hero.Powers.Add(new HeroPower
                    {
                        Hero = hero,
                        Power = await _powerService.FindById(power.Id)
                    });
                }
            }
            else
            {
                hero.Powers = null;
            }

            hero.Allies = new List<HeroAlly>();
            if (villainDto.Allies != null)
            {
                foreach (var ally in villainDto.Allies)
                {
                    hero.Allies.Add(new HeroAlly
                    {
                        AllyFrom = hero,
                        AllyTo = await GetVillainById(ally.Id)
                    });
                }
            }

            hero.IsBadGuy = true;

            return hero;
        }

        public async Task<VillainDto> AddVillain(VillainDto villainDto)
        {
            ValidateVillainDto(villainDto);

            var villain = await ToEntity(villainDto);
            villain.OverallStrength = villain.Powers.Aggregate(0, (acc, power) => acc += power.Power.Strength);
            villain.OverallStrength += villain.MainPower == null ? 0 : villain.MainPower.Power.Strength;
            villain.AvatarPath = Constants.DEFAULT_IMAGE_VILLAIN;

            if (!string.IsNullOrEmpty(villainDto.Email))
            {
                var user = ConvertVillainToUser(villainDto);

                await _userService.CreateHeroUser(user);
                villain.User = user;
            }

            await _context.Heroes.AddAsync(villain);
            await _context.SaveChangesAsync();
            villainDto.Id = villain.Id;

            return villainDto;
        }

        private User ConvertVillainToUser(VillainDto villain)
        {
            return new User()
            {
                Email = villain.Email,
                FullName = villain.Name,
                Username = villain.Email,
                Age = 18,
            };
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

        public async Task DeleteVillain(int id)
        {
            var villain = await _context.Heroes.Include(h=>h.User).Where(h => h.IsBadGuy).FirstOrDefaultAsync(h => h.Id == id);

            if (villain == null)
            {
                throw new EntityNotFoundException("hero doesn't exist");
            }
            if (villain.User != null)
            {
                _context.Users.Remove(villain.User);
                await _context.SaveChangesAsync();
            }

            await DeleteAllyTo(villain);
            _context.Heroes.Remove(villain);
            await _context.SaveChangesAsync();
        }

        private async Task<ICollection<HeroPower>> CreateVillainPowers(VillainDto villainDto, Hero hero)
        {
            var heroPowers = new List<HeroPower>();

            foreach (var power in villainDto.Powers)
            {
                heroPowers.Add(new HeroPower
                {
                    Hero = hero,
                    Power = await _powerService.FindById(power.Id)
                });
            }

            return heroPowers;
        }

        private async Task<ICollection<HeroAlly>> CreateVillainAllies(VillainDto villainDto, Hero hero)
        {
            var heroAllies = new List<HeroAlly>();

            foreach (var ally in villainDto.Allies)
            {
                heroAllies.Add(new HeroAlly
                {
                    AllyFrom = hero,
                    AllyTo = await GetVillainById(ally.Id)
                });
            }

            return heroAllies;
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

        public async Task UpdateVillain(VillainDto villainDto)
        {
            ValidateVillainDto(villainDto);

            var villain = await _context.Heroes
                 .FirstOrDefaultAsync(h => h.Id == villainDto.Id);

            if (villain == null)
            {
                throw new EntityNotFoundException($"There is no hero with id: {villainDto.Id}");
            }

            await DeleteAllHeroPowers(villain);
            await DeleteAllHeroAllies(villain);
            villain.Powers = null;

            villain.Name = villainDto.Name;
            villain.TypeId = villainDto.TypeId;
            villain.Powers = await CreateVillainPowers(villainDto, villain);
            villain.Allies = await CreateVillainAllies(villainDto, villain);
            villain.OverallStrength = villain.Powers.Aggregate(0, (acc, power) => acc += power.Power.Strength);
            villain.MainPower = villainDto.MainPower.Id == 0 ? null : new HeroPower
            {
                Hero = villain,
                Power = await _powerService.FindById(villainDto.MainPower.Id)
            };
            villain.Latitude = villainDto.Latitude;
            villain.Longitude = villainDto.Longitude;
            villain.OverallStrength += villain.MainPower == null ? 0 : villain.MainPower.Power.Strength;
            villain.Birthday = DateTime.Parse(villainDto.Birthday).AddHours(5);

            _context.Heroes.Update(villain);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeVillainLocation(int id, double latitude, double longitude)
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
            var villain = await GetVillainById(id);
            try
            {
                villain.Latitude = latitude;
                villain.Longitude = longitude;


                _context.Heroes.Update(villain);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new InvalidParameterException(e.Message);
            }
        }

        public async Task AddBonusStrength(Hero villain)
        {
            if (villain.Powers != null)
            {
                foreach (var power in villain.Powers)
                {
                    if (power.Power.Name == Constants.STRENGTH_POWER)
                    {
                        villain.OverallStrength += Convert.ToInt32(_appSettings.TEN_PERCENT_BONUS * Convert.ToDouble(villain.OverallStrength));
                    }
                    else if (power.Power.Name == Constants.SPEED_POWER || power.Power.Name == Constants.INVISIBILITY_POWER)
                    {
                        villain.OverallStrength += Convert.ToInt32(_appSettings.FIVE_PERCENT_BONUS * Convert.ToDouble(villain.OverallStrength));
                    }
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task ModifyStrengthOfParty(List<Hero> villains, double percentage)
        {
            foreach (Hero hero in villains)
            {

                hero.OverallStrength += Convert.ToInt32(percentage * Convert.ToDouble(hero.OverallStrength));
            }
            await _context.SaveChangesAsync();
        }

        public HeroPower FindPowerOfVillain(Hero villain, string powerName)
        {
            if (villain.Powers != null)
            {
                return villain.Powers.FirstOrDefault(power => power.Power.Name == powerName);
            }

            return null;
        }

        public async Task ModifyStrengthOfVillainParty(List<Hero> villains)
        {
            foreach (Hero villain in villains)
            {
                await AddBonusStrength(villain);

                if (villain.Powers != null)
                {
                    if (FindPowerOfVillain(villain, Constants.TELEPATHY_POWER) != null)
                    {
                        await ModifyStrengthOfParty(villains, _appSettings.FIVE_PERCENT_BONUS);
                    }

                    if (FindPowerOfVillain(villain, Constants.MADDJSKILLZS_POWER) != null)
                    {
                        await ModifyStrengthOfParty(villains, _appSettings.TEN_PERCENT_BONUS);
                    }

                    if (FindPowerOfVillain(villain, Constants.CURSEOFBADPROGRAMMING_POWER) != null)
                    {
                        await ModifyStrengthOfParty(villains, _appSettings.FIVE_PERCENT_PENALTY);
                    }
                }

                await _context.SaveChangesAsync();
            }
        }

        public VillainDto AddBonusStrength(VillainDto hero)
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

        public ICollection<VillainDto> ModifyStrengthOfParty(ICollection<VillainDto> heroes, double percentage)
        {
            foreach (VillainDto hero in heroes)
            {
                hero.OverallStrength += Convert.ToInt32(percentage * Convert.ToDouble(hero.OverallStrength));
            }
            return heroes;
        }

        public PowerDto FindPowerOfHero(VillainDto hero, string powerName)
        {
            if (hero.Powers != null)
            {
                return hero.Powers.FirstOrDefault(power => power.Name == powerName);
            }

            return null;
        }

        public ICollection<VillainDto> ModifyStrengthOfHeroParty(ICollection<VillainDto> heroes)
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
    }
}
