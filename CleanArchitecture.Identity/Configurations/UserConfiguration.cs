using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            builder.HasData(
                new ApplicationUser()
                {
                    Id = "74be9696-30c5-44f3-905b-49715801763f",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "admin@localhost.com",
                    Name = "Carlos",
                    Surnames = "GB",
                    UserName = "cgimenez",
                    NormalizedUserName = "cgimenez",
                    PasswordHash = hasher.HashPassword(null, "Passwor0123!"),
                    EmailConfirmed = true
                },
                   new ApplicationUser()
                   {
                       Id = "5d1735f6-7930-4374-8c48-1c267e60037a",
                       Email = "johndoe@localhost.com",
                       NormalizedEmail = "johndoe@localhost.com",
                       Name = "John",
                       Surnames = "Doe",
                       UserName = "johndoe",
                       NormalizedUserName = "johndoe",
                       PasswordHash = hasher.HashPassword(null, "Passwor0123!"),
                       EmailConfirmed = true
                   }
                );
        }
    }
}
