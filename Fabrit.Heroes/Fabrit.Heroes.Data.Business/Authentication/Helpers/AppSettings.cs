using System;
using System.Collections.Generic;
using System.Text;

namespace Fabrit.Heroes.Data.Business.Authentication.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int Range { get; set; }
        public double TEN_PERCENT_BONUS { get; set; }
        public double FIVE_PERCENT_BONUS { get; set; }
        public double FIVE_PERCENT_PENALTY { get; set; }
        public double TrainingBonus { get; set; }
        public int MinutesToWait { get; set; }
    }
}
