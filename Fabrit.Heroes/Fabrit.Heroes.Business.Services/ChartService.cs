using Fabrit.Heroes.Data.Business.Hero;
using Fabrit.Heroes.Data.Entities.Hero;
using Fabrit.Heroes.Data.Entities.Power;
using Fabrit.Heroes.Infrastructure.Common.Exceptions;
using Fabrit.Heroes.Business.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fabrit.Heroes.Data;
using Fabrit.Heroes.Infrastructure.Common;

namespace Fabrit.Heroes.Business.Services
{
    public class ChartService : IChartService
    {
        private readonly HeroesDbContext _context;

        public ChartService(HeroesDbContext context)
        {
            _context = context;
        }

        public async Task<HeroPowerChartData> GetHeroChartData(ChartType chartType)
        {
            var heroes = await _context.Heroes
                .Include(h => h.Powers).ThenInclude(h => h.Power)
                .ToListAsync();

            var powers = heroes
                .SelectMany(h => h.Powers)
                .Select(hp => hp.Power)
                .OrderByDescending(p => p.Strength)
                .Distinct();

            switch (chartType)
            {
                case ChartType.XAxisHero:
                    return HeroChartData(heroes, powers);
                case ChartType.XAxisPower:
                    return PowerChartData(heroes, powers);
                default:
                    throw new EnumTypeNotFoundException("Unrecognised XChartType");
            }
        }

        private HeroPowerChartData HeroChartData(IEnumerable<Hero> heroes, IEnumerable<Power> powers)
        {
            HeroPowerChartData results = new HeroPowerChartData()
            {
                ChartXData = heroes.Select(h => h.Name)
            };

            var barChartData = InitializeBarChart(powers).ToList();

            foreach (var power in powers)
            {
                foreach (var hero in heroes)
                {
                    var heroPower = hero.Powers.FirstOrDefault(hp => hp.Power.Id == power.Id);
                    var powerIndex = powers.IndexOf(power);

                    if (heroPower != null)
                    {
                        barChartData[powerIndex].Data.Add(heroPower.Power.Strength);
                    }
                    else
                    {
                        barChartData[powerIndex].Data.Add(0);
                    }
                }
            }

            results.Data = barChartData;
            return results;
        }

        private IEnumerable<ChartData> InitializeBarChart(IEnumerable<Power> powers)
        {
            var barChartData = new List<ChartData>();

            foreach (var power in powers)
            {
                var chartData = new ChartData
                {
                    Data = new List<int>(),
                    Label = power.Name,
                    Stack = Constants.HERO_CHART_STACK
                };

                barChartData.Add(chartData);
            }

            return barChartData;
        }

        private HeroPowerChartData PowerChartData(IEnumerable<Hero> heroes, IEnumerable<Power> powers)
        {
            HeroPowerChartData results = new HeroPowerChartData()
            {
                ChartXData = powers.Select(p => p.Name)
            };

            var barChartData = heroes.Select(hero => new ChartData { Data = new List<int>() })
                                     .ToList();

            foreach (var power in powers)
            {
                var powerIndex = powers.IndexOf(power);
                barChartData[powerIndex].Data.Add(power.Heroes.ToList().Count);
            }

            results.Data = barChartData;
            return results;
        }
    }
}
