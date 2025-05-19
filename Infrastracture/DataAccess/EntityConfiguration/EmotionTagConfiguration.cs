using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration;

public class EmotionTagConfiguration : IEntityTypeConfiguration<EmotionTag>
{
    public void Configure(EntityTypeBuilder<EmotionTag> builder)
    {
        builder.ToTable("EmotionTag");
        builder.HasAlternateKey(["AnalysisId", "TagId"]);
        builder.HasMany(et => et.TriggerWords);
        builder
            .HasKey(o => o.Id);
    }
}