using Fabrit.Heroes.Business.Services.Contracts;
using Fabrit.Heroes.Data;
using Fabrit.Heroes.Data.Entities.Hero;
using Fabrit.Heroes.Infrastructure.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fabrit.Heroes.Business.Services.BackgroundService
{
    public class CheckPowerTask : ScheduledProcessor
    {
        public CheckPowerTask(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {

        }

        protected override string Schedule => Constants.CRON_JOB_SCHEDULE;

        private Task UpdatePowers(HeroesDbContext context,IHeroService heroService)
        {
            var powers = context.HeroPowers.Include(hp=>hp.Power).Where(hp => hp.LastTrainingTime != null || hp.LastChangeTime!=null).ToList();
            foreach (var heroPower in powers)
            {
                var timePassed = DateTime.Now.Subtract(heroPower.LastChangeTime.GetValueOrDefault()).TotalHours;
                var timeChanged = Convert.ToInt32(Math.Floor(timePassed));

                if (timeChanged < Constants.HERO_POWER_MIN_CHANGE_TIME)
                {
                    continue;
                }

                var percentage = Math.Pow(0.8, timeChanged);
                var newStrength = Convert.ToInt32(Convert.ToDouble(heroPower.Strength) * percentage);
                
                heroPower.Strength = newStrength;
                heroPower.LastChangeTime = DateTime.Now;
                
                context.HeroPowers.Update(heroPower);
                context.SaveChanges();
                heroService.ChangeOverallStrength(heroPower.HeroId);
            }
            return Task.CompletedTask;

        }

        public override Task ProcessInScope(IServiceProvider scopeServiceProvider)
        {
            UpdatePowers(scopeServiceProvider.GetRequiredService<HeroesDbContext>(), scopeServiceProvider.GetRequiredService<IHeroService>());
            return Task.CompletedTask;
        }
    }
}
