using Fabrit.Heroes.Infrastructure.Common.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fabrit.Heroes.Data.Entities.Power
{
    public class Element : IDataEntity
    {
        public int Id { get; set; }
        public ElementType Type { set; get; }
    }
}
