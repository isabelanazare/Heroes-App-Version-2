using System;
using Fabrit.Heroes.Business.Services.Contracts;
using Fabrit.Heroes.Data;
using Fabrit.Heroes.Infrastructure.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Fabrit.Heroes.Business.Services.BackgroundService
{
    public class UpdateBadgesTask : ScheduledProcessor
    {
        public UpdateBadgesTask(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {

        }

        protected override string Schedule => Constants.CRON_JOB_SCHEDULE;

        private Task UpdateBadges(HeroesDbContext context, IRewardService rewardService)
        {
            var heroes = rewardService.GetAllHeroesWithBadges();

            foreach (var hero in heroes)
            {
                rewardService.UpdateHeroBadges(hero);
            }

            return Task.CompletedTask;

        }

        public override Task ProcessInScope(IServiceProvider scopeServiceProvider)
        {
            UpdateBadges(scopeServiceProvider.GetRequiredService<HeroesDbContext>(), scopeServiceProvider.GetRequiredService<IRewardService>());
            return Task.CompletedTask;
        }
    }
}
