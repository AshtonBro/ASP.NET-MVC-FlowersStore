using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlowersStore.DataAccess.MSSQL.Entities;

namespace FlowersStore.DataAccess.MSSQL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Basket)
                   .WithOne(x => x.User)
                   .HasForeignKey<Basket>(x => x.Id);

            builder.HasIndex(x => x.Name)
                   .IsUnique();

        }
    }
}