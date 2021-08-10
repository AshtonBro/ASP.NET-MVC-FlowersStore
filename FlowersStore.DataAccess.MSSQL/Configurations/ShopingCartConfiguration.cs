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
        }
    }
}