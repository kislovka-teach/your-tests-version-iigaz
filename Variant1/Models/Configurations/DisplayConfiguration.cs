using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Variant1.Models.Configurations;

public class DisplayConfiguration : IEntityTypeConfiguration<Display>
{
    public void Configure(EntityTypeBuilder<Display> builder)
    {
        builder
            .Property(display => display.Title)
            .HasMaxLength(Meta.DisplayTitleMaxLength);
    }
}