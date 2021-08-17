using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlowersStore.DataAccess.MSSQL.Entities;

namespace FlowersStore.DataAccess.MSSQL.Configurations
{
    class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FlowersType).HasMaxLength(40);

            builder.HasMany(x => x.Products)
                  .WithOne(x => x.Category)
                  .OnDelete(DeleteBehavior.NoAction)
                  .HasForeignKey(x => x.CategoryId)
                  .IsRequired();
        }
    }
}