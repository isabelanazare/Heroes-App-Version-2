using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fabrit.Heroes.Business.Services.Contracts;
using Fabrit.Heroes.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Fabrit.Heroes.Business.Services.BackgroundService
{
    public abstract class ScopedProcessor : BackgroundService
    {
        private IServiceScopeFactory _serviceScopeFactory;

        public ScopedProcessor(IServiceScopeFactory serviceScopeFactory) : base()
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task Process()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var scopedProcessingService =
                scope.ServiceProvider
                    .GetRequiredService<HeroesDbContext>();
                scope.ServiceProvider
                    .GetRequiredService<IHeroService>();
                scope.ServiceProvider
                   .GetRequiredService<IRewardService>();
                await ProcessInScope(scope.ServiceProvider);
            }
        }

        public abstract Task ProcessInScope(IServiceProvider scopeServiceProvider);
    }
}
