using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlowersStore.DataAccess.MSSQL.Entities;

namespace FlowersStore.DataAccess.MSSQL.Configurations
{
    class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User)
                  .WithOne(x => x.Basket)
                  .OnDelete(DeleteBehavior.NoAction)
                  .HasForeignKey<Basket>(x => x.UserId);

            builder.HasMany(x => x.ProductCards)
                .WithOne(x => x.Basket)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(x => x.BasketId)
                .IsRequired();
        }
    }
}