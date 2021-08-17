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

            builder.HasIndex(x => x.Name)
                   .IsUnique();

            builder.Property(x => x.Name).HasMaxLength(30);
            builder.Property(x => x.SecondName).HasMaxLength(50);

            builder.HasOne(x => x.Basket)
                   .WithOne(x => x.User)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}