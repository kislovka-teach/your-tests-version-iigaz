using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Variant2.Models;

public class Revision : IEntityTypeConfiguration<Revision>
{
    public int Id { get; set; }

    [MaxLength(Meta.RevisionTextMaxLength)]
    public string Text { get; set; } = null!;

    public DateTime DateTime { get; set; }

    public int AuthorId { get; set; }
    public User Author { get; set; } = null!;

    public int? PreviousRevisionId { get; set; }
    public Revision? PreviousRevision { get; set; }

    public int? NextRevisionId { get; set; }
    public Revision? NextRevision { get; set; }


    public void Configure(EntityTypeBuilder<Revision> builder)
    {
        builder.HasOne(revision => revision.PreviousRevision)
            .WithOne(revision => revision.NextRevision)
            .HasForeignKey((Revision revision) => revision.NextRevisionId)
            .HasForeignKey((Revision revision) => revision.PreviousRevisionId);
    }
}