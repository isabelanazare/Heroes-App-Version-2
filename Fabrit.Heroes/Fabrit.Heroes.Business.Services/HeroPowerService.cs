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
using Fabrit.Heroes.Data.Business.Villain;

namespace Fabrit.Heroes.Business.Services
{
    public class HeroPowerService : IHeroPowerService
    {
        private readonly HeroesDbContext _context;
        private readonly AppSettings _appSettings;
        private readonly IHeroService _heroService;

        public HeroPowerService(HeroesDbContext context, IOptions<AppSettings> appSettings, IHeroService heroService)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _heroService = heroService;
        }

        public async Task AddVillainPowers(VillainPowersDto dto)
        {
            if (dto.VillainId < 1)
            {
                throw new ArgumentException("Invalid id");
            }
            var villain = await _context.Heroes.Include(h => h.Powers).FirstOrDefaultAsync(villain => villain.Id == dto.VillainId);
            if (!villain.IsBadGuy)
            {
                throw new EntityNotFoundException("The id is not corresponding to a villain");
            }

            foreach (var powerId in dto.PowerIds)
            {
                var dbPower = await _context.Powers.FirstOrDefaultAsync(p => p.Id == powerId);
                villain.Powers.Add(new HeroPower
                {
                    Hero = villain,
                    HeroId = villain.Id,
                    Power = dbPower,
                    PowerId = dbPower.Id,
                    Strength = dbPower.Strength,
                    LastChangeTime = null,
                    LastTrainingTime = null
                });

            }

            _context.Update(villain);
            await _context.SaveChangesAsync();
            _heroService.ChangeOverallStrength(villain.Id);
        }

        public async Task ChangeMainPower(MainPowerChangeDto dto)
        {
            if (dto.HeroPowerId < 1)
            {
                throw new ArgumentException("Invalid id");
            }
            var heroPower = await _context.HeroPowers.Include(hp => hp.Hero).ThenInclude(h => h.MainPower).FirstOrDefaultAsync(hp => hp.Id == dto.HeroPowerId);
            var hero = heroPower.Hero;
            HeroPower mainPower = null;
            if (hero.MainPower != null)
            {
                mainPower = new HeroPower()
                {
                    Strength = hero.MainPower.Strength,
                    Hero = hero,
                    HeroId = hero.Id,
                    Power = hero.MainPower.Power,
                    PowerId = hero.MainPower.PowerId,
                    LastTrainingTime = hero.MainPower.LastTrainingTime
                };
            }
            hero.MainPower = null;
            _context.Update(hero);
            await _context.SaveChangesAsync();
            if (!dto.IsMainPower)
            {
                return;
            }
            hero.MainPower = heroPower;
            if (mainPower != null) { hero.Powers.Append(mainPower); }
            _context.Update(hero);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePowerById(int id)
        {
            HeroPower power = await _context.HeroPowers.Where(p => p.Id == id).FirstOrDefaultAsync();
            int heroId = power.HeroId;

            if (power == null)
            {
                throw new EntityNotFoundException($"There is no Hero Power with id: {id}");
            }

            _context.HeroPowers.Remove(power);
            await _context.SaveChangesAsync();
            _heroService.ChangeOverallStrength(heroId);

        }

        public async Task<HeroPowerDto> GetHeroPowerById(int id)
        {
            var hp = await _context.HeroPowers.Include(h => h.Power).ThenInclude(p => p.Element)
                .Include(h => h.Hero).ThenInclude(h=>h.MainPower)
                .FirstOrDefaultAsync(h => h.Id == id);
            return new HeroPowerDto()
            {
                Details = hp.Power.Details,
                Element = hp.Power.Element.Type.ToString(),
                ElementId = hp.Power.ElementId,
                Strength = hp.Strength,
                Id = hp.Id,
                LastTrained = hp.LastTrainingTime.GetValueOrDefault().ToString(),
                MainTrait = hp.Power.MainTrait,
                Name = hp.Power.Name,
                PowerId = hp.PowerId,
                IsMainPower = hp.Hero.MainPower.Id == hp.Id
            };
        }

        public IAsyncEnumerable<HeroPowerDto> GetHeroPowersForHero(int heroId)
        {
            var hero = _context.Heroes.Include(h => h.Powers).FirstOrDefault(hero => hero.Id == heroId);
            return _context.HeroPowers.Include(h => h.Hero).Include(h => h.Power).Where(hp => hp.HeroId == heroId).Select(hp => new HeroPowerDto()
            {
                Details = hp.Power.Details,
                Element = hp.Power.Element.Type.ToString(),
                Strength = hp.Strength,
                Id = hp.Id,
                LastTrained = hp.LastTrainingTime.GetValueOrDefault().ToString(),
                MainTrait = hp.Power.MainTrait,
                Name = hp.Power.Name,
                PowerId = hp.PowerId,
                IsMainPower = hero.MainPower != null ? hero.MainPower.Id == hp.Id : false
            }).AsAsyncEnumerable();
        }

        public async Task TrainPower(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Invalid id!");
            }
            var heroPower = await _context.HeroPowers.FirstOrDefaultAsync(hp => hp.Id == id);
            if (heroPower == null)
            {
                throw new EntityNotFoundException("Hero Power does not exist");
            }
            heroPower.Strength = Convert.ToInt32(heroPower.Strength * _appSettings.TrainingBonus);
            heroPower.LastTrainingTime = DateTime.Now;
            heroPower.LastChangeTime = DateTime.Now;
            _context.Update(heroPower);
            await _context.SaveChangesAsync();
            _heroService.ChangeOverallStrength(heroPower.HeroId);

        }

        public async Task UpdateHeroPower(HeroPowerDto dto)
        {
            if (dto.Id < 1)
            {
                throw new ArgumentException("Invalid id");
            }
            var heroPower = await _context.HeroPowers.Include(h=>h.Power).ThenInclude(p=>p.Element).FirstOrDefaultAsync(hp => hp.Id == dto.Id);
            heroPower.Strength = dto.Strength;
            heroPower.Power.ElementId = dto.ElementId;
            _context.Update(heroPower);
            await _context.SaveChangesAsync();
            _heroService.ChangeOverallStrength(heroPower.HeroId);
        }
    }
}
