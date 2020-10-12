using System;
using System.Collections.Generic;
using System.Text;

namespace Fabrit.Heroes.Data.Business.Battle
{
   public class BattleRecordDto
    {
        public int PlayerId { get; set; }
        public BattleDto Battle { get; set; }
        public bool HasWon { get; set; }
    }
}
