using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlowersStore.DataAccess.MSSQL.Entities;

namespace FlowersStore.DataAccess.MSSQL.Configurations
{
    public class ShopingCartConfiguration : IEntityTypeConfiguration<ShopingCart>
    {
        public void Configure(EntityTypeBuilder<ShopingCart> builder)
        {
            builder.HasKey(x => x.CartId);

            builder.HasIndex(p => new { p.BasketId, p.ProductId })
                   .IsUnique();

            builder.HasOne(x => x.Basket)
                .WithMany(x => x.ShopingCarts)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(x => x.Product)
                .WithMany(x => x.ShopingCarts)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(x => x.ProductId)
                .IsRequired();
        }
    }
}