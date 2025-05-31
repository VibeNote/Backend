using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Enums;
using Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Core.Entities;

[Table("TagTable")]
[PrimaryKey(nameof(Id))]
public class Tag: IEntity<Guid>
{
    public Tag(Guid id, TagsEnum enumValue, string ruName, string engName)
    {
        Id = id;
        EnumValue = enumValue;
        RuName = ruName;
        EngName = engName;
    }

    protected Tag() : this(Guid.Empty, 0,string.Empty, string.Empty) {}
    [Key, Column("Id")]
    public Guid Id { get; }
    [Column("EnumValue")]
    public TagsEnum EnumValue { get; set; }
    [Column("RuName")]
    public string RuName { get; set; }    
    [Column("EngName")]
    public string EngName { get; set; }
}