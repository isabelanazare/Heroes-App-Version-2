using Fabrit.Heroes.Infrastructure.Common.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fabrit.Heroes.Data.Entities.User
{
    public class User : IDataEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string AvatarPath { get; set; }
        public bool IsActivated { get; set; }
        public int Age { get; set; }
        public bool WasPasswordForgotten { get; set; }
        public bool WasPasswordChanged { get; set; }
        public UserRole Role { get; set; }
        public int RoleId { get; set; }
    }

    public class UserDataConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);
            builder.Property(user => user.Username).IsRequired();
            builder.Property(user => user.Password).IsRequired();
            builder.HasIndex(nameof(User.Username)).IsUnique();
            builder.HasOne(user => user.Role);
        }
    }
}
