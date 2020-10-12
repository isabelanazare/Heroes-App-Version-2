using Fabrit.Heroes.Infrastructure.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fabrit.Heroes.Data.Entities.User
{
    public enum RoleType
    {
        Regular = 1,
        Admin = 2,
        General = 3
    }

    public class UserRole : IDataEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
