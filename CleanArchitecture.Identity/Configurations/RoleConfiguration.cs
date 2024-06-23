using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole()
                {
                    Id = "12be118f-54b1-42ea-a833-eec3dd19ff06",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                },
                new IdentityRole()
                {
                    Id = "9899af15-d80a-4a05-841d-6ebe2e55a8f6",
                    Name = "Operator",
                    NormalizedName = "OPERATOR",
                }
                );
        }
    }
}
