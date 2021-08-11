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

            builder.HasOne(b => b.Basket)
                   .WithOne(u => u.User)
                   .HasForeignKey<Basket>(b => b.Id);

            builder.HasIndex(p => p.Name)
                   .IsUnique();
        }
    }
}