using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlowersStore.DataAccess.MSSQL.Entities;

namespace FlowersStore.DataAccess.MSSQL.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(40);
            builder.Property(x => x.Description).HasMaxLength(250);
            builder.Property(x => x.Color).HasMaxLength(40);
            builder.Property(x => x.Price).HasPrecision(38, 18);

            builder.HasMany(x => x.ProductCards)
                   .WithOne(x => x.Product)
                   .OnDelete(DeleteBehavior.NoAction)
                   .HasForeignKey(x => x.ProductId)
                   .IsRequired();

            builder.HasOne(x => x.Category)
                   .WithMany(x => x.Products)
                   .OnDelete(DeleteBehavior.NoAction)
                   .HasForeignKey(x => x.CategoryId);
        }
    }
}