using Fabrit.Heroes.Data.Entities.Hero;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Fabrit.Heroes.Data.Entities.Power;
using Fabrit.Heroes.Data.Entities.User;
using Fabrit.Heroes.Data.Entities.Badge;
using System.Linq;
using Fabrit.Heroes.Data.Entities.Battle;

namespace Fabrit.Heroes.Data
{
    public class HeroesDbContext : DbContext
    {
        #region Constructor and Config

        public HeroesDbContext(DbContextOptions<HeroesDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Hero).Assembly);

        }

        #endregion

        #region Save

        /// <summary>
        /// Override of SaveChangesAsync method enables us to write custom code to be executed when SaveChangesAsync is called
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // custom code

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        /// <summary>
        /// Override of SaveChanges method enables us to write custom code to be executed when SaveChanges is called
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            // custom code

            var result = base.SaveChanges();

            return result;
        }

        #endregion

        #region Db Sets

        public virtual DbSet<Hero> Heroes { get; set; }
        public virtual DbSet<HeroType> HeroTypes { get; set; }
        public virtual DbSet<HeroPower> HeroPowers { get; set; }
        public virtual DbSet<Power> Powers { get; set; }
        public virtual DbSet<HeroAlly> HeroAllies { get; set; }
        public virtual DbSet<Element> Elements { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Badge> Badges { get; set; }
        public virtual DbSet<HeroBadge> HeroBadges { get; set; }
        public virtual DbSet<UserRole> Roles { get; set; }
        public virtual DbSet<HeroBattle> HeroBattles { get; set; }
        public virtual DbSet<Battle> Battles{ get; set; }

        #endregion
    }
}