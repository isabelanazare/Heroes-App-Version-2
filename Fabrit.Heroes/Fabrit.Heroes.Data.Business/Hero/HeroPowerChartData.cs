using System;
using System.Collections.Generic;
using System.Text;

namespace Fabrit.Heroes.Data.Business.Hero
{
    public class HeroPowerChartData
    {
        public IEnumerable<string> ChartXData { get; set; }
        public List<ChartData> Data { get; set; }
    }
}
