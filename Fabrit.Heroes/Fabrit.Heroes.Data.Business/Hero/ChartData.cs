using System.Collections.Generic;


namespace Fabrit.Heroes.Data.Business.Hero
{
    public class ChartData
    {
        public List<int> Data { get; set; }
        public string Label { get; set; }
        public string Stack { get; set; }
    }

    public enum ChartType
    {
        XAxisHero = 0,
        XAxisPower = 1
    }
}
