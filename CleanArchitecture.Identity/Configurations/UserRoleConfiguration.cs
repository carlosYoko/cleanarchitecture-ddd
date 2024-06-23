using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = "12be118f-54b1-42ea-a833-eec3dd19ff06",
                    UserId = "74be9696-30c5-44f3-905b-49715801763f"
                },
                new IdentityUserRole<string>()
                {
                    RoleId = "9899af15-d80a-4a05-841d-6ebe2e55a8f6",
                    UserId = "5d1735f6-7930-4374-8c48-1c267e60037a"
                }
                );
        }
    }
}
