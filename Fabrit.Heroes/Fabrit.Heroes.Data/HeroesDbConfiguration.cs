
using Fabrit.Heroes.Data.Entities.Hero;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using Fabrit.Heroes.Data.Entities.Power;
using Microsoft.EntityFrameworkCore;
using Fabrit.Heroes.Data.Entities.User;
using Fabrit.Heroes.Data.Entities.Badge;

namespace Fabrit.Heroes.Data
{
    public class HeroesDbConfiguration
    {
        private readonly HeroesDbContext _context;

        public HeroesDbConfiguration(HeroesDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This method is called at startup to ensure the database is created and to seed any data that is required on a first run (static data for example).
        /// </summary>
        public void Seed()
        {
            _context.Database.Migrate();

            // seed functionality
            if (!_context.Heroes.Any()) AddHeroes();
            if (!_context.Badges.Any()) AddBadges();
            if (!_context.Roles.Any()) AddRoles();
            if (!_context.Users.Any()) AddUsers();



            _context.SaveChanges();
        }

        private void AddRoles()
        {
            _context.Add(new UserRole
            {
                Id = (int)RoleType.Admin,
                Name = "Admin",
            });
            _context.Add(new UserRole
            {
                Id = (int)RoleType.Regular,
                Name = "Regular",
            });
        }

        private void AddUsers()
        {
            _context.Add(new User
            {
                Age = 22,
                IsActivated = true,
                FullName = "Admin",
                RoleId = 2,
                WasPasswordChanged = false,
                WasPasswordForgotten = false,
                Username = "fabrit.heroes.adm@gmail.com",
                Email = "fabrit.heroes.adm@gmail.com",
                Password = "Fabrit@99"
            });
        }

        private void AddHeroes()
        {
            var hulk = new Hero
            {
                Name = "Hulk",
                Type = new HeroType
                {
                    Name = "Real"
                }
            };

            var flash = new Hero
            {
                Name = "Flash",
                Type = new HeroType
                {
                    Name = "Fictional"
                },
            };

            hulk.Allies = new List<HeroAlly>
            {
                new HeroAlly
                {
                    AllyFrom = hulk,
                    AllyTo = flash
                }
            };

            hulk.Powers = new List<HeroPower>
            {
                new HeroPower
                {
                    Hero = hulk,
                    Power = new Power
                    {
                        Name = "Strength",
                        Details = "incresed strength",
                        Strength = 70,
                        Element = new Element
                        {
                            Type = ElementType.Water
                        }

                    }
                }
            };

            flash.Powers = new List<HeroPower>
            {
                new HeroPower
                {
                    Hero = flash,
                    Power = new Power
                    {
                        Name = "Speed",
                        Details = "fast running",
                        Strength =105,
                        Element = new Element
                        {
                            Type = ElementType.Earth
                        }
                    }
                }
            };

            Element airElement = new Element
            {
                Type = ElementType.Air
            };

            Element fireElement = new Element
            {
                Type = ElementType.Fire
            };

            _context.Add(airElement);
            _context.Add(fireElement);
            _context.Add(hulk);
            _context.Add(flash);
            _context.SaveChanges();
        }

        private void AddBadges()
        {
            var tier1Badge = new Badge
            {
                Tier = BadgeType.TierCategory1
            };

            var tier2Badge = new Badge
            {
                Tier = BadgeType.TierCategory2

            };

            var tier3Badge = new Badge
            {
                Tier = BadgeType.TierCategory3

            };

            var tier4Badge = new Badge
            {
                Tier = BadgeType.TierCategory4

            };

            var tier5Badge = new Badge
            {
                Tier = BadgeType.TierCategory5

            };
            
            _context.Add(tier1Badge);
            _context.Add(tier2Badge);
            _context.Add(tier3Badge);
            _context.Add(tier4Badge);
            _context.Add(tier5Badge);
            _context.SaveChanges();
        }
     }
}