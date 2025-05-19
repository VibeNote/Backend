using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration;

public class TriggerWordConfiguration : IEntityTypeConfiguration<TriggerWord>
{
    public void Configure(EntityTypeBuilder<TriggerWord> builder)
    {
        builder.ToTable("TriggerWordTable");
        builder.HasKey(t => t.Id);
    }
}