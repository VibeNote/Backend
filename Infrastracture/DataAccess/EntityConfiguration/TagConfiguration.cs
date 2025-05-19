using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    private const string JoyId = "a8d5707f-2880-4d22-9561-7d13061b9930";
    private const string CalmnessId = "583b8346-1237-4de6-b014-afaf1812c061";
    private const string AngerId = "f7b844fd-9d26-4603-971f-674bafabac2f";
    private const string SadnessId = "10de9f43-9383-4549-85fd-a152f67981bf";
    private const string AnxietyId = "73cd3672-328b-48c1-a507-449823a264dc";
    private const string ConfusionId = "309a9bb6-2b54-409c-995d-693e755ce519";
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("TagTable");
        builder.HasData(
            new[]
            {
                new Tag(Guid.Parse(JoyId), "Радость"),
                new Tag(Guid.Parse(CalmnessId), "Спокойствие"),
                new Tag(Guid.Parse(AngerId), "Злость"),
                new Tag(Guid.Parse(SadnessId), "Печаль"),
                new Tag(Guid.Parse(AnxietyId), "Тревога"),
                new Tag(Guid.Parse(ConfusionId), "Растерянность")
            });
    }
}