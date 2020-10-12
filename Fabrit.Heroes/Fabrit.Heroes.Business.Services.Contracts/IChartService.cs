using Fabrit.Heroes.Data.Business.Hero;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fabrit.Heroes.Business.Services.Contracts
{
    public interface IChartService
    {
        Task<HeroPowerChartData> GetHeroChartData(ChartType xData);
    }
}
